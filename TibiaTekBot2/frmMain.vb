'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.

Imports System.Windows, TibiaTekBot.PProxy2, System.Runtime.InteropServices, _
 System.ComponentModel, System.IO, System.Xml, System.Text.RegularExpressions, _
 System.Windows.Forms, Scripting

Public Class frmMain

    Dim LoginSelectForm As frmLoginSelectDialog
    Dim SC As frmSplashScreen
    Dim IsVisible As Boolean = True
    Public LoginServer As String

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim R As New Random(System.DateTime.Now.Millisecond)
            Select Case R.Next(0, 5)
                Case 0
                    Me.PictureBox1.Image = My.Resources.ttb_splash0
                Case 1
                    Me.PictureBox1.Image = My.Resources.ttb_splash1
                Case 2
                    Me.PictureBox1.Image = My.Resources.ttb_splash2
                Case 3
                    Me.PictureBox1.Image = My.Resources.ttb_splash3
                Case 4
                    Me.PictureBox1.Image = My.Resources.ttb_splash4
            End Select
            'MsgBox(System.Text.Encoding.GetEncoding("iso-8859-1").EncodingName)
            'For Each Encoding As System.Text.EncodingInfo In System.Text.Encoding.GetEncodings
            'MsgBox(Encoding.Name)
            'Next
            SC = New frmSplashScreen
            SC.ShowDialog()
            LoadTibiaEXE()
            System.GC.Collect()
            InitializeControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub InitializeControls()
        Try
            ' Spell Caster
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind <> SpellKind.Rune Then
                    SpellCasterSpell.Items.Add(Spell.Words)
                End If
            Next
            If SpellCasterSpell.Items.Count > 0 Then
                SpellCasterSpell.SelectedIndex = 0
            End If
            'Changers
            'CHANGE THIS WTF!
            For Each Item As IItems.ItemDefinition In Core.Client.Items.ItemsList
                If Core.Client.Items.IsRing(Item.ItemID) Then ChangerRingType.Items.Add(Item.Name)
                If Core.Client.Items.IsNeck(Item.ItemID) Then ChangerAmuletType.Items.Add(Item.Name)
            Next
            If ChangerRingType.Items.Count > 0 Then ChangerRingType.SelectedIndex = 0
            If ChangerAmuletType.Items.Count > 0 Then ChangerAmuletType.SelectedIndex = 0
            ' Runemaker
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind = SpellKind.Rune Then
                    RunemakerSpell.Items.Add(Spell.Name)
                End If
            Next
            If RunemakerSpell.Items.Count > 0 Then
                RunemakerSpell.SelectedIndex = 0
            End If

            ' Auto Stacker
            AutoStackerDelay.Value = Consts.AutoStackerDelay
            ' Auto Eater
            AutoEaterInterval.Value = Consts.AutoEaterInterval
            AutoEaterEatFromFloor.Checked = Consts.EatFromFloor
            AutoEaterEatFromFloorFirst.Checked = Consts.EatFromFloorFirst
            ' Auto Looter
            AutoLooterMinCap.Value = Consts.CavebotLootMinCap
            AutoLooterDelay.Value = Consts.LootDelay
            AutoLooterEatFromCorpse.Checked = Consts.LootEatFromCorpse
            ' FPS changer
            FpsActive.Value = Consts.FPSWhenActive
            FpsInactive.Value = Consts.FPSWhenInactive
            FpsMinimized.Value = Consts.FPSWhenMinimized
            FPSHidden.Value = Consts.FPSWhenHidden
            ' Light Effect
            LightEffect.SelectedIndex = 0
            'Stats Uploader
            StatsUploaderUrl.Text = Consts.StatsUploaderUrl
            StatsUploaderFilename.Text = Consts.StatsUploaderFilename
            StatsUploaderPath.Text = Consts.StatsUploaderPath
            StatsUploaderUser.Text = Consts.StatsUploaderUserID
            StatsUploaderPassword.Text = Consts.StatsUploaderPassword
            StatsUploaderSaveToDisk.Checked = Consts.StatsUploaderSaveOnDiskOnly
            'Healer
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind = SpellKind.Healing Then
                    HealSpellName.Items.Add(Spell.Words)
                End If
            Next
            If HealRuneType.Items.Count > 0 Then HealRuneType.SelectedIndex = 0
            If HealSpellName.Items.Count > 0 Then HealSpellName.SelectedIndex = 0
            If HealPotionName.Items.Count > 0 Then HealPotionName.SelectedIndex = 0
            If ManaPotionName.Items.Count > 0 Then ManaPotionName.SelectedIndex = 0
            HealRuneUseHp.Checked = True
            HealPotionUseHp.Checked = True
            HealSpellUseHP.Checked = True
            'Friend-Healer
            If HealFType.Items.Count > 0 Then HealFType.SelectedIndex = 0
            'Party Healer
            If HealPType.Items.Count > 0 Then HealPType.SelectedIndex = 0
            'Chameleon
            Dim Outfits() As OutfitDefinition = CoreModule.Outfits.GetOutfits
            For Each Outfit As OutfitDefinition In Outfits
                If Not String.IsNullOrEmpty(Outfit.Name) Then
                    ChameleonOutfit.Items.Add(Outfit.Name)
                End If
            Next
            If ChameleonOutfit.Items.Count > 0 Then
                ChameleonOutfit.SelectedIndex = 0
            End If
            'Open Website
            If WebsiteName.Items.Count > 0 Then WebsiteName.SelectedIndex = 0
            'Auto Attacker
            If AttackerFightingMode.Items.Count > 0 Then AttackerFightingMode.SelectedIndex = 0
            If AttackChasingMode.Items.Count > 0 Then AttackChasingMode.SelectedIndex = 0
            'Dancer
            If DancerSpeed.Items.Count > 0 Then DancerSpeed.SelectedIndex = 0
            'Ammo Maker
            For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                If Spell.Kind = SpellKind.Ammunition Or Spell.Kind = SpellKind.Incantation Then
                    AmmoMakerSpell.Items.Add(Spell.Words)
                End If
            Next
            If AmmoMakerSpell.Items.Count > 0 Then AmmoMakerSpell.SelectedIndex = 0
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
                Me.NotifyIcon.Visible = False
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            If Core.Client.IsConnected Then
                Me.Text = "TibiaTek Bot - " & Core.Client.CharacterName
                FunctionsToolStripMenuItem.Enabled = True
                AboutToolStripMenuItem.Enabled = True
                RefreshControls()
                MainTabControl.Enabled = True
            Else
                If Not (Core.Proxy Is Nothing OrElse Core.Client Is Nothing) Then
                    Me.Text = "TibiaTek Bot - " & Hex(Core.Client.GetWindowHandle)
                Else
                    Me.Text = "TibiaTek Bot"
                End If
                FunctionsToolStripMenuItem.Enabled = False
                AboutToolStripMenuItem.Enabled = False
                MainTabControl.Enabled = False
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#Region " Refresh Controls "

    Private Sub RefreshControls()
        Try
            RefreshSpellCasterControls()
            RefreshRunemakerControls()
            RefreshAutoEaterControls()
            RefreshAutoLooterControls()
            RefreshAutoStackerControls()
            RefreshLightEffectsControls()
            RefreshAmmoRestackerControls()
            RefreshComboBotControls()
            RefreshAutoFisherControls()
            RefreshCavebotControls()
            RefreshTradeChannelWatcherControls()
            RefreshStatsUploaderControls()
            RefreshFpsChangerControls()
            RefreshAdvertiserControls()
            RefreshHealerControls()
            RefreshHealFriendControls()
            RefreshHealPartyControls()
            RefreshDrinkerControls()
            RefreshFakeTitleControls()
            RefreshChameleonControls()
            RefreshExpCheckerControls()
            RefreshNameSpyControls()
            RefreshTrainerControls()
            RefreshAutoAttackerControls()
            RefreshPickuperControls()
            RefreshChangerControls()
            RefreshDancerControls()
            RefreshAmmoMakerControls()
            RefreshAntiIdlerControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAmmoMakerControls()
        Try
            AmmoMakerTrigger.Checked = Core.AmmoMakerTimerObj.State = ThreadTimerState.Running

            If AmmoMakerTrigger.Checked Then
                AmmoMakerSpell.Text = Core.AmmoMakerSpell.Words
                AmmoMakerMinCap.Value = Core.AmmoMakerMinCap
                AmmoMakerMinMana.Value = Core.AmmoMakerMinMana

                AmmoMakerSpell.Enabled = False
                AmmoMakerMinCap.Enabled = False
                AmmoMakerMinMana.Enabled = False
            Else
                AmmoMakerSpell.Enabled = True
                AmmoMakerMinCap.Enabled = True
                AmmoMakerMinMana.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshDancerControls()
        Try
            DancerTrigger.Checked = Core.DancerTimerObj.State = ThreadTimerState.Running

            If DancerTrigger.Checked Then
                Select Case Core.DancerTimerObj.Interval
                    Case 500
                        DancerSpeed.Text = "Slow"
                    Case 100
                        DancerSpeed.Text = "Fast"
                    Case 10
                        DancerSpeed.Text = "Turbo"
                    Case Else
                        DancerSpeed.Text = "Unknown"
                End Select
                DancerSpeed.Enabled = False
            Else
                DancerSpeed.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshChangerControls()
        Try
            RingChangerTrigger.Checked = Core.RingChangerTimerObj.State = ThreadTimerState.Running
            AmuletChangerTrigger.Checked = Core.AmuletChangerTimerObj.State = ThreadTimerState.Running

            If RingChangerTrigger.Checked Then
                ChangerRingType.Text = Core.Client.Items.GetItemName(Core.RingID)
                ChangerRingType.Enabled = False
            Else
                ChangerRingType.Enabled = True
            End If
            If AmuletChangerTrigger.Checked Then
                ChangerAmuletType.Text = Core.Client.Items.GetItemName(Core.AmuletID)
                ChangerAmuletType.Enabled = False
            Else
                ChangerAmuletType.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAntiIdlerControls()
        AntiIdlerTrigger.Checked = Core.AntiLogoutObj.State = ThreadTimerState.Running
    End Sub
    Private Sub RefreshPickuperControls()
        Try
            PickuperTrigger.Checked = Core.PickUpTimerObj.State = ThreadTimerState.Running
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAutoAttackerControls()
        Try
            If Core.AutoAttackerTimerObj.State = ThreadTimerState.Running Or Core.AutoAttackerActivated Then
                AutoAttackerTrigger.Checked = True
            Else
                AutoAttackerTrigger.Checked = False
            End If
            If AutoAttackerTrigger.Checked Then
                If Core.AutoAttackerTimerObj.State = ThreadTimerState.Running Then
                    AttackAutomatically.Checked = True
                Else
                    AttackAutomatically.Checked = False
                End If
                AttackerFightingMode.Enabled = False
                AttackChasingMode.Enabled = False
                AttackAutomatically.Enabled = False
            Else
                AttackerFightingMode.Enabled = True
                AttackChasingMode.Enabled = True
                AttackAutomatically.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshTrainerControls()
        Try
            TrainerTrigger.Checked = Core.AutoTrainerTimerObj.State = ThreadTimerState.Running

            If TrainerTrigger.Checked Then
                MinPercentageHP.Value = Core.AutoTrainerMinHPPercent
                MaxPercentageHP.Value = Core.AutoTrainerMaxHPPercent
                TrainerAdd.Enabled = False
                TrainerRemove.Enabled = False
                TrainerClear.Enabled = False
                MinPercentageHP.Enabled = False
                MaxPercentageHP.Enabled = False
            Else
                TrainerAdd.Enabled = True
                TrainerRemove.Enabled = True
                TrainerClear.Enabled = True
                MinPercentageHP.Enabled = True
                MaxPercentageHP.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshChameleonControls()
        Try
            Dim BL As New BattleList
            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
            Dim OD As New OutfitDefinition
            Dim ODFound As Boolean = CoreModule.Outfits.GetOutfitByID(BL.OutfitID, OD)
            If ODFound Then
                ChameleonOutfit.SelectedIndex = ChameleonOutfit.Items.IndexOf(OD.Name)
            End If
            Select Case BL.OutfitAddons
                Case IBattlelist.OutfitAddons.First
                    ChameleonFirst.Checked = True
                Case IBattlelist.OutfitAddons.Second
                    ChameleonSecond.Checked = True
                Case IBattlelist.OutfitAddons.Both
                    ChameleonBoth.Checked = True
                Case Else
                    ChameleonNone.Checked = True
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshFakeTitleControls()
        Try
            FakeTitleTrigger.Checked = Core.FakingTitle
            If FakeTitleTrigger.Checked Then
                FakeTitle.Enabled = False
            Else
                FakeTitle.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshNameSpyControls()
        Try
            If Core.NameSpyActivated Then
                NameSpyTrigger.Checked = True
            Else
                NameSpyTrigger.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshExpCheckerControls()
        Try
            If Core.ExpCheckerActivated Or Core.ShowCreaturesUntilNextLevel Then
                ExpCheckerTrigger.Checked = True
            Else
                ExpCheckerTrigger.Checked = False
            End If
            If ExpCheckerTrigger.Checked Then
                ExpShowNext.Checked = Core.ExpCheckerActivated = True
                ExpShowCreatures.Checked = Core.ShowCreaturesUntilNextLevel = True

                ExpShowNext.Enabled = False
                ExpShowCreatures.Enabled = False
            Else
                ExpShowNext.Enabled = True
                ExpShowCreatures.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshDrinkerControls()
        Try
            ' DrinkerTrigger.Checked = Core.AutoDrinkerTimerObj.State = ThreadTimerState.Running
            '  If DrinkerTrigger.Checked Then
            DrinkerManaPoints.Value = Core.DrinkerManaRequired
            'DrinkerManaPoints.Enabled = True
            '  Else
            '  DrinkerManaPoints.Enabled = True
            '  End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshHealPartyControls()
        Try
            HealPartyTrigger.Checked = Core.HealPartyTimerObj.State = ThreadTimerState.Running
            If HealPartyTrigger.Checked Then
                Select Case Core.HealPartyHealType
                    Case HealTypes.UltimateHealingRune
                        HealPType.Text = "Ultimate Healing Rune"
                    Case HealTypes.ExuraSio
                        HealPType.Text = "Exura Sio - Spell"
                    Case HealTypes.Both
                        HealPType.Text = "Both"
                End Select
                HealPHp.Value = Core.HealPartyMinimumHPPercentage
                HealPType.Enabled = False
                HealPHp.Enabled = False
            Else
                HealPType.Enabled = True
                HealPHp.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshHealFriendControls()
        Try
            HealFriendTrigger.Checked = Core.HealFriendTimerObj.State = ThreadTimerState.Running
            If HealFriendTrigger.Checked Then
                HealFName.Text = Core.HealFriendCharacterName
                Select Case Core.HealFriendHealType
                    Case HealTypes.UltimateHealingRune
                        HealFType.Text = "Ultimate Healing Rune"
                    Case HealTypes.ExuraSio
                        HealFType.Text = "Exura Sio - Spell"
                    Case HealTypes.Both
                        HealFType.Text = "Both"
                End Select
                HealFHp.Value = Core.HealFriendHealthPercentage
                HealFName.Enabled = False
                HealFType.Enabled = False
                HealFHp.Enabled = False
            Else
                HealFName.Enabled = True
                HealFType.Enabled = True
                HealFHp.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshHealerControls()
        Try
            HealWithRune.Checked = Core.UHTimerObj.State = ThreadTimerState.Running
            HealWithSpell.Checked = Core.HealTimerObj.State = ThreadTimerState.Running
            Dim MaxHitPoints As Integer = 0
            Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 4)
            If HealWithRune.Checked Then
                Select Case Core.Client.Items.GetItemName(Core.UHId)
                    Case "Ultimate Healing"
                        HealRuneType.Text = "UH Rune"
                    Case "Intense Healing"
                        HealRuneType.Text = "IH Rune"
                End Select
                HealRuneHP.Value = Core.UHHPRequired
                If Core.UHHPRequired <= MaxHitPoints Then
                    HealRunePercent.Value = CInt((Core.UHHPRequired / MaxHitPoints) * 100)
                Else
                    HealRunePercent.Value = 100
                End If
                HealRuneType.Enabled = False
                HealRuneUseHp.Enabled = False
                HealRuneUsePercent.Enabled = False
                HealRuneHP.Enabled = False
                HealRunePercent.Enabled = False
            Else

                HealRuneType.Enabled = True
                HealRuneUseHp.Enabled = True
                HealRuneUsePercent.Enabled = True
                HealRunePercent.Enabled = True
                If HealRuneUseHp.Checked Then
                    HealRuneHP.Enabled = True
                    HealRunePercent.Enabled = False
                Else
                    HealRuneHP.Enabled = False
                    HealRunePercent.Enabled = True
                End If
            End If
            If HealWithSpell.Checked Then
                Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 4)
                HealSpellName.Text = Core.HealSpell.Words
                HealSpellHp.Value = Core.HealMinimumHP

                If (Core.HealMinimumHP <= MaxHitPoints) Then
                    HealSpellPercent.Value = CInt((Core.HealMinimumHP / MaxHitPoints) * 100)
                Else
                    HealSpellPercent.Value = 100
                End If

                HealSpellName.Enabled = False
                HealSpellUseHP.Enabled = False
                HealSpellUsePercent.Enabled = False
                HealSpellHp.Enabled = False
                HealSpellPercent.Enabled = False
            Else
                HealSpellName.Enabled = True
                HealSpellUseHP.Enabled = True
                HealSpellUsePercent.Enabled = True
                HealSpellPercent.Enabled = True
                If HealSpellUseHP.Checked Then
                    HealSpellHp.Enabled = True
                    HealSpellPercent.Enabled = False
                Else
                    HealSpellHp.Enabled = False
                    HealSpellPercent.Enabled = True
                End If
            End If

            If RestoreManaWith.Checked Then

                ManaPotionName.Enabled = False
                DrinkerManaPoints.Enabled = False
            Else
                ManaPotionName.Enabled = True
                DrinkerManaPoints.Enabled = True

            End If

            If HealWithPotion.Checked Then
                Select Case Core.Client.Items.GetItemName(Core.PotionID)
                    Case "Health Potion"
                        HealPotionName.Text = "Health Potion"
                    Case "Strong Health Potion"
                        HealPotionName.Text = "Strong Health Potion"
                    Case "Great Health Potion"
                        HealPotionName.Text = "Great Health Potion"
                End Select
                HealRuneHP.Value = Core.PotionHPRequired
                If Core.PotionHPRequired <= MaxHitPoints Then
                    HealPotionPercent.Value = CInt((Core.PotionHPRequired / MaxHitPoints) * 100)
                Else
                    HealPotionPercent.Value = 100
                End If

                HealPotionName.Enabled = False
                HealPotionUseHp.Enabled = False
                HealPotionUsePercent.Enabled = False
                HealPotionHp.Enabled = False
                HealPotionPercent.Enabled = False
            Else
                HealPotionName.Enabled = True
                HealPotionUseHp.Enabled = True
                HealPotionUsePercent.Enabled = True
                HealPotionPercent.Enabled = True

                If HealPotionUseHp.Checked Then
                    HealPotionHp.Enabled = True
                    HealPotionPercent.Enabled = False
                Else
                    HealPotionHp.Enabled = False
                    HealPotionPercent.Enabled = True
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAdvertiserControls()
        Try
            TradeChannelAdvertiserTrigger.Checked = Core.AdvertiseTimerObj.State = ThreadTimerState.Running
            If TradeChannelAdvertiserTrigger.Checked Then
                TradeChannelAdvertiserAdvertisement.Text = Core.AdvertiseMsg
                TradeChannelAdvertiserAdvertisement.Enabled = False
            Else
                TradeChannelAdvertiserAdvertisement.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshFpsChangerControls()
        Try
            FpsChangerTrigger.Checked = Core.FPSChangerTimerObj.State = ThreadTimerState.Running
            If FpsChangerTrigger.Checked Then
                FpsActive.Value = Core.FrameRateActive
                FpsInactive.Value = Core.FrameRateInactive
                FpsMinimized.Value = Core.FrameRateMinimized
                FPSHidden.Value = Core.FrameRateHidden
                FpsActive.Enabled = False
                FpsInactive.Enabled = False
                FpsMinimized.Enabled = False
                FPSHidden.Enabled = False
            Else
                FpsActive.Enabled = True
                FpsInactive.Enabled = True
                FpsMinimized.Enabled = True
                FPSHidden.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshStatsUploaderControls()
        Try
            StatsUploaderTrigger.Checked = Core.StatsUploaderTimerObj.State = ThreadTimerState.Running
            If StatsUploaderTrigger.Checked Then
                StatsUploaderUrl.Enabled = False
                StatsUploaderFilename.Enabled = False
                StatsUploaderPath.Enabled = False
                StatsUploaderUser.Enabled = False
                StatsUploaderPassword.Enabled = False
                StatsUploaderSaveToDisk.Enabled = False
            Else
                StatsUploaderUrl.Enabled = True
                StatsUploaderFilename.Enabled = True
                StatsUploaderPath.Enabled = True
                StatsUploaderUser.Enabled = True
                StatsUploaderPassword.Enabled = True
                StatsUploaderSaveToDisk.Enabled = True
            End If
            StatsUploaderUrl.Enabled = Not StatsUploaderSaveToDisk.Checked
            StatsUploaderUser.Enabled = Not StatsUploaderSaveToDisk.Checked
            StatsUploaderPassword.Enabled = Not StatsUploaderSaveToDisk.Checked
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshTradeChannelWatcherControls()
        Try
            TradeChannelWatcherTrigger.Checked = Core.TradeWatcherActive = True
            If TradeChannelWatcherTrigger.Checked Then
                TradeChannelWatcherExpression.Text = Core.TradeWatcherRegex
                TradeChannelWatcherExpression.Enabled = False
            Else
                TradeChannelWatcherExpression.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshCavebotControls()
        Try
            CavebotTrigger.Checked = Core.CaveBotTimerObj.State = ThreadTimerState.Running
            If CavebotTrigger.Checked Then
                CavebotConfigure.Enabled = False
            Else
                CavebotConfigure.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshAutoFisherControls()
        Try
            AutoFisherTrigger.Checked = Core.FisherTimerObj.State = ThreadTimerState.Running
            If AutoFisherTrigger.Checked Then
                AutoFisherMinimumCapacity.Value = Core.FisherMinimumCapacity
                If Core.FisherTurbo Then
                    AutoFisherTurbo.Checked = True
                Else
                    AutoFisherTurbo.Checked = False
                End If
                AutoFisherTurbo.Enabled = False
                AutoFisherMinimumCapacity.Enabled = False
            Else
                AutoFisherTurbo.Enabled = True
                AutoFisherMinimumCapacity.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshComboBotControls()
        Try
            ComboBotTrigger.Checked = Core.ComboBotEnabled = True
            If ComboBotTrigger.Checked Then
                ComboLeader.Text = Core.ComboBotLeader
                ComboLeader.Enabled = False
            Else
                ComboLeader.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshLightEffectsControls()
        Try
            LightEffectsTrigger.Checked = Core.LightTimerObj.State = ThreadTimerState.Running
            If LightEffectsTrigger.Checked Then
                Select Case Core.LightI
                    Case ITibia.LightIntensity.Huge
                        LightEffect.SelectedItem = "Ultimate Torch"
                    Case ITibia.LightIntensity.Large
                        If Core.LightC = ITibia.LightColor.UtevoLux Then
                            LightEffect.SelectedItem = "Utevo Gran Lux"
                        Else
                            LightEffect.SelectedItem = "Light Wand"
                        End If
                    Case ITibia.LightIntensity.Medium
                        If Core.LightC = ITibia.LightColor.Torch Then
                            LightEffect.SelectedItem = "Torch"
                        Else
                            LightEffect.SelectedItem = "Utevo Lux"
                        End If
                    Case ITibia.LightIntensity.VeryLarge
                        If Core.LightC = ITibia.LightColor.Torch Then
                            LightEffect.SelectedItem = "Great Torch"
                        Else
                            LightEffect.SelectedItem = "Utevo Vis Lux"
                        End If
                    Case ITibia.LightIntensity.Huge + 2
                        LightEffect.SelectedItem = "On"
                    Case Else
                        LightEffect.SelectedIndex = -1
                End Select
                LightEffect.Enabled = False
            Else
                LightEffect.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshSpellCasterControls()
        Try
            SpellCasterTrigger.Checked = Core.SpellTimerObj.State = ThreadTimerState.Running
            If SpellCasterTrigger.Checked Then
                SpellCasterSpell.Text = Core.SpellMsg
                SpellCasterMinimumManaPoints.Value = Core.SpellManaRequired
                SpellCasterSpell.Enabled = False
                SpellCasterMinimumManaPoints.Enabled = False
            Else
                SpellCasterSpell.Enabled = True
                SpellCasterMinimumManaPoints.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshAutoLooterControls()
        Try
            AutoLooterTrigger.Checked = Core.LooterTimerObj.State = ThreadTimerState.Running
            If AutoLooterTrigger.Checked Then
                AutoLooterMinCap.Value = Core.LooterMinimumCapacity
                AutoLooterMinCap.Enabled = False
                AutoLooterConfigure.Enabled = False
                AutoLooterDelay.Enabled = False
                AutoLooterEatFromCorpse.Enabled = False
            Else
                AutoLooterMinCap.Enabled = True
                AutoLooterConfigure.Enabled = True
                AutoLooterDelay.Enabled = True
                AutoLooterEatFromCorpse.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshAmmoRestackerControls()
        Try
            AmmunitionRestackerTrigger.Checked = Core.AmmoRestackerTimerObj.State = ThreadTimerState.Running
            If AmmunitionRestackerTrigger.Checked Then
                AmmunitionRestackerMinAmmo.Value = Core.AmmoRestackerMinimumItemCount
                AmmunitionRestackerMinAmmo.Enabled = False
            Else
                AmmunitionRestackerMinAmmo.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshAutoStackerControls()
        Try
            AutoStackerTrigger.Checked = Core.StackerTimerObj.State = ThreadTimerState.Running
            If AutoStackerTrigger.Checked Then
                AutoStackerDelay.Enabled = False
            Else
                AutoStackerDelay.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshRunemakerControls()
        Try
            RunemakerTrigger.Checked = Core.RunemakerTimerObj.State = ThreadTimerState.Running
            If RunemakerTrigger.Checked Then
                RunemakerSpell.Text = Core.RunemakerSpell.Name
                RunemakerMinimumManaPoints.Value = Core.RunemakerManaPoints
                RunemakerMinimumSoulPoints.Value = Core.RunemakerSoulPoints
                RunemakerSpell.Enabled = False
                RunemakerMinimumManaPoints.Enabled = False
                RunemakerMinimumSoulPoints.Enabled = False
            Else
                RunemakerSpell.Enabled = True
                RunemakerMinimumManaPoints.Enabled = True
                RunemakerMinimumSoulPoints.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub RefreshAutoEaterControls()
        Try
            AutoEaterTrigger.Checked = Core.EaterTimerObj.State = ThreadTimerState.Running
            If AutoEaterTrigger.Checked Then
                If Core.AutoEaterSmart > 0 Then
                    AutoEaterSmart.Checked = True
                Else
                    AutoEaterSmart.Checked = False
                End If
                If AutoEaterSmart.Checked Then
                    AutoEaterMinimumHitPoints.Value = Core.AutoEaterSmart
                    'AutoEaterDelay.Value = Consts.AutoEaterSmartInterval
                Else
                    AutoEaterMinimumHitPoints.Value = 0
                    'AutoEaterDelay.Value = Consts.AutoEaterInterval
                End If
                AutoEaterEatFromFloor.Enabled = False
                AutoEaterEatFromFloorFirst.Enabled = False
                AutoEaterSmart.Enabled = False
                AutoEaterMinimumHitPoints.Enabled = False
                AutoEaterInterval.Enabled = False
                AutoEaterInterval.Value = CInt(Core.EaterTimerObj.Interval)
            Else
                AutoEaterEatFromFloor.Enabled = True
                AutoEaterEatFromFloorFirst.Enabled = True
                AutoEaterSmart.Enabled = True
                AutoEaterMinimumHitPoints.Enabled = True
                AutoEaterInterval.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#End Region

#Region " Load Stuff "

    Public Sub LoadTibiaEXE()
        Dim xmlFile As New System.Xml.XmlDocument()
        Dim strFilename As String = ""
        Dim strDirectory As String = ""
        'Dim strPort As String = ""
        Dim blIsValid As Boolean = False
        Try
            For I As Integer = Application.ExecutablePath.Length - 1 To 0 Step -1
                If Application.ExecutablePath.Chars(I) = "\" Then
                    Core.ExecutablePath = Strings.Left(Application.ExecutablePath, I)
                    Exit For
                End If
            Next
            Do
                If IO.File.Exists(GetConfigurationDirectory() & "\Data.xml") Then
                    xmlFile.Load(GetConfigurationDirectory() & "\Data.xml")
                    strFilename = xmlFile.SelectSingleNode("Client/Filename").InnerText
                    strDirectory = xmlFile.SelectSingleNode("Client/Directory").InnerText
                    If IO.File.Exists(strDirectory & "\" & strFilename) Then
                        blIsValid = True
                    Else
                        CreateLoadXML()
                    End If
                Else
                    CreateLoadXML()
                End If
            Loop Until blIsValid = True
            'Me.NotifyIcon.Visible = False
            LoginSelectForm = New frmLoginSelectDialog()
            If LoginSelectForm.ShowDialog() <> Forms.DialogResult.OK Then End
            Core.Client = New Tibia(strFilename, strDirectory)
            Core.Client.Start()
            If Not File.Exists(Application.StartupPath & "\TibiaTekBot Injected DLL.dll") Then
                Throw New Exception("Unable to locate """ & Application.StartupPath & "\TibiaTekBot Injected DLL.dll"". Please re-install the application.")
            End If
            Core.Client.InjectDLL(Application.StartupPath & "\TibiaTekBot Injected DLL.dll")
            Core.Proxy = New PProxy2(Core.Client)

            System.Threading.Thread.Sleep(1000)
            Core.WindowTimerObj.StartTimer()
            Dim TempInt As Integer = 0
            Do
                Core.Client.ReadMemory(Consts.ptrServerAddressBegin, TempInt, 1)
            Loop Until TempInt <> 0
            For I As Integer = 0 To Consts.ServerAddressCount - 1
                Core.Client.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "127.0.0.1")
                Core.Client.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Core.Proxy.sckLListen.LocalPort, 2)
            Next
            Me.NotifyIcon.Visible = True
            Core.Proxy.LoginPort = Core.LoginPort
            Core.TibiaClientStateTimerObj.StartTimer()
            Core.Client.ReadMemory(Consts.ptrFrameRateBegin, Core.FrameRateBegin, 4)
            InjectLastAttackedId()
            If Core.IsOpenTibiaServer Then
                Core.Client.UnprotectMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length)
                Core.Client.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub CreateLoadXML()
        Try
            Dim Filename As String = ""
            Dim Directory As String = ""
            Dim dlgOpen As New OpenFileDialog
            Dim Document As New XmlDocument()
            Dim xmlDeclaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "us-ascii", "")
            Dim xmlComment As XmlComment = Document.CreateComment(GNUGPLStatement)
            Dim xmlClient As XmlElement
            Dim xmlFilename As XmlElement
            Dim xmlDirectory As XmlElement
            Dim xmlAddresses As XmlElement
            Dim xmlAddress1 As XmlElement
            Dim xmlAddress2 As XmlElement
            Dim xmlAddress3 As XmlElement
            Dim xmlAddress4 As XmlElement
            Dim xmlAddress5 As XmlElement
            Dim xmlAddress6 As XmlElement
            Dim xmlAddress7 As XmlElement
            Dim xmlAddress8 As XmlElement
            Dim xmlAddress9 As XmlElement
            Dim xmlAddress10 As XmlElement
            Dim xmlAddress11 As XmlElement
            With dlgOpen
                .Title = "Tibia's Location"
                .Filter = "Executable|*.exe"
            End With
            If MessageBox.Show("Please find the location of your Tibia Client.", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Forms.DialogResult.Cancel Then
                End
            End If
            Dim Found As Boolean = False
            Do
                If dlgOpen.ShowDialog() = Forms.DialogResult.Cancel Then
                    End
                End If
                Dim FVI As FileVersionInfo = FileVersionInfo.GetVersionInfo(dlgOpen.FileName)
                If Not FVI Is Nothing AndAlso Not FVI.FileVersion Is Nothing AndAlso Not FVI.ProductName Is Nothing Then
                    Found = FVI.FileVersion.Equals(TibiaFileVersion) AndAlso FVI.ProductName.Equals(TibiaProductName)
                End If
                If Not Found Then
                    If MessageBox.Show("You must select a valid Tibia.exe.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = Forms.DialogResult.Cancel Then
                        End
                    End If
                End If
            Loop While Not Found
            For I As Integer = dlgOpen.FileName.Length - 1 To 0 Step -1
                If dlgOpen.FileName.Chars(I) = "\" Then
                    Directory = Strings.Left(dlgOpen.FileName, I)
                    Filename = Strings.Right(dlgOpen.FileName, dlgOpen.FileName.Length - I - 1)
                    Exit For
                End If
            Next
            If Directory = "" Or Filename = "" Then Exit Sub
            xmlClient = Document.CreateElement("Client")
            xmlFilename = Document.CreateElement("Filename")
            xmlFilename.InnerText = Filename
            xmlDirectory = Document.CreateElement("Directory")
            xmlDirectory.InnerText = Directory
            xmlAddresses = Document.CreateElement("Addresses")
            xmlAddress1 = Document.CreateElement("Address")
            xmlAddress1.InnerText = "login01.tibia.com:7171"
            xmlAddress2 = Document.CreateElement("Address")
            xmlAddress2.InnerText = "login02.tibia.com:7171"
            xmlAddress3 = Document.CreateElement("Address")
            xmlAddress3.InnerText = "login03.tibia.com:7171"
            xmlAddress4 = Document.CreateElement("Address")
            xmlAddress4.InnerText = "login04.tibia.com:7171"
            xmlAddress5 = Document.CreateElement("Address")
            xmlAddress5.InnerText = "login05.tibia.com:7171"
            xmlAddress6 = Document.CreateElement("Address")
            xmlAddress6.InnerText = "tibia1.cipsoft.com:7171"
            xmlAddress7 = Document.CreateElement("Address")
            xmlAddress7.InnerText = "tibia2.cipsoft.com:7171"
            xmlAddress8 = Document.CreateElement("Address")
            xmlAddress8.InnerText = "tibia3.cipsoft.com:7171"
            xmlAddress9 = Document.CreateElement("Address")
            xmlAddress9.InnerText = "tibia4.cipsoft.com:7171"
            xmlAddress10 = Document.CreateElement("Address")
            xmlAddress10.InnerText = "tibia5.cipsoft.com:7171"
            xmlAddress11 = Document.CreateElement("Address")
            xmlAddress11.InnerText = "localhost:7171"
            xmlAddresses.AppendChild(xmlAddress1)
            xmlAddresses.AppendChild(xmlAddress2)
            xmlAddresses.AppendChild(xmlAddress3)
            xmlAddresses.AppendChild(xmlAddress4)
            xmlAddresses.AppendChild(xmlAddress5)
            xmlAddresses.AppendChild(xmlAddress6)
            xmlAddresses.AppendChild(xmlAddress7)
            xmlAddresses.AppendChild(xmlAddress8)
            xmlAddresses.AppendChild(xmlAddress9)
            xmlAddresses.AppendChild(xmlAddress10)
            xmlAddresses.AppendChild(xmlAddress11)
            Document.AppendChild(xmlDeclaration)
            xmlClient.AppendChild(xmlComment)
            xmlClient.AppendChild(xmlFilename)
            xmlClient.AppendChild(xmlDirectory)
            xmlClient.AppendChild(xmlAddresses)
            Document.AppendChild(xmlClient)
            If IO.File.Exists(GetConfigurationDirectory() & "\Data.xml") Then IO.File.Delete(GetConfigurationDirectory() & "\Data.xml")
            Document.Save(GetConfigurationDirectory() & "\Data.xml")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#End Region

#Region " Popup Menu "

    Private Sub PopupMenu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PopupMenu.Opening
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                CavebotMenuItem.Enabled = False
            Else
                CavebotMenuItem.Enabled = True
            End If
            If Core.TibiaClientIsVisible Then
                ShowHideTibiaWindow.Name = "Hide Tibia Window"
            Else
                ShowHideTibiaWindow.Name = "Show Tibia Window"
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosePopupItem.Click
        Try
            If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
                Try
                    If Not Core.Proxy Is Nothing Then
                        If Not Core.Client Is Nothing Then
                            Core.Client.Close()
                            Me.NotifyIcon.Visible = False
                        End If
                    End If
                Catch
                Finally
                    End
                End Try
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AlarmsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmsToolStripMenuItem.Click
        Try
            If Not Core.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            Core.AlarmsForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ShowHideTibiaWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideTibiaWindow.Click
        Try
            If Not Core.Proxy Is Nothing Then
                If Not Core.Client Is Nothing Then
                    If Core.Client.GetWindowHandle = 0 Then Exit Sub
                    If Core.TibiaClientIsVisible Then
                        Core.Client.Hide()
                        'Win32API.ShowWindow(Core.Client.GetWindowHandle, Win32API.ShowState.SW_HIDE)
                    Else
                        Core.Client.Show()
                        'Win32API.ShowWindow(Core.Client.GetWindowHandle, Win32API.ShowState.SW_SHOW)
                    End If
                    Core.TibiaClientIsVisible = Not Core.TibiaClientIsVisible
                End If
            End If
        Catch
        Finally
        End Try
    End Sub

    Private Sub ConstantsEditorMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConstantsEditorMenuItem.Click
        Try
            Core.ConstantsEditorForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub MCPatcher()
        Try
            System.IO.File.Copy(Core.Client.Directory & "\" & Core.Client.Filename, Core.Client.Directory & "\_Tibia.exe.tmp")
            Dim FSR As New FileStream(Core.Client.Directory & "\_Tibia.exe.tmp", FileMode.Open, FileAccess.Read)
            Dim FSW As New FileStream(Core.Client.Directory & "\TibiaMC.exe", FileMode.OpenOrCreate, FileAccess.Write)
            Dim Reader As New BinaryReader(FSR)
            Dim Writer As New BinaryWriter(FSW)
            Dim CurrentByte As Byte = 0
            Try
                Do
                    CurrentByte = Reader.ReadByte()
                    If FSW.Position = Consts.MCPatchOffset Then
                        CurrentByte = CByte(Consts.MCPatchReplacement)
                    End If
                    Writer.Write(CurrentByte)
                Loop While True
            Catch
            End Try
            Writer.Close()
            Reader.Close()
            FSR.Close()
            FSW.Close()
            MessageBox.Show("The new Tibia Client with Multi-Client is now saved at: " & Core.Client.Directory & "\" & "TibiaMC.exe")

            Dim Result As DialogResult = MessageBox.Show("Would you like to use the patched Tibia Client the next time you open TibiaTek Bot?", "Information", MessageBoxButtons.YesNo)
            If Result = Forms.DialogResult.Yes Then
                Dim Document As New Xml.XmlDocument()
                Document.Load(GetConfigurationDirectory() & "\Data.xml")
                Document.Item("Client").Item("Filename").InnerText = "TibiaMC.exe"
                Document.Save(GetConfigurationDirectory() & "\Data.xml")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            If System.IO.File.Exists(Core.Client.Directory & "\_Tibia.exe.tmp") Then
                System.IO.File.Delete(Core.Client.Directory & "\_Tibia.exe.tmp")
            End If
        End Try
    End Sub

    Private Sub MCPatchMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCPatchMenuItem.Click, PatchToolStripMenuItem.Click
        MCPatcher()
    End Sub

    Private Sub CavebotMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CavebotMenuItem.Click
        Try
            If Not Core.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Core.CavebotForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CharacterStatisticsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CharacterStatisticsMenuItem.Click
        Try
            If Not Core.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            Core.CharacterStatisticsForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ShowHideToolMenuMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideToolStripMenuItem.Click
        Try
            If IsVisible Then
                Me.Hide()
                IsVisible = False
            Else
                Me.Show()
                IsVisible = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub changeloginserver_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeLoginServerPopupItem.Click
        Try
            If Not Core.Client.IsConnected Then
                LoginSelectForm = New frmLoginSelectDialog()
                If LoginSelectForm.ShowDialog() <> Forms.DialogResult.OK Then Exit Sub
                For I As Integer = 0 To 3
                    Core.Client.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "localhost")
                    Core.Client.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Core.Proxy.sckLListen.LocalPort, 2)
                Next
                If Core.IsOpenTibiaServer Then
                    Core.Client.UnprotectMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length)
                    Core.Client.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
                End If
            Else
                MessageBox.Show("You must be logged out to change the login server.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#End Region

#Region " Tool Menu "

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
            Try
                If Not Core.Proxy Is Nothing Then
                    If Not Core.Client Is Nothing Then
                        Core.Client.Close()
                        Me.NotifyIcon.Visible = False
                    End If
                End If
            Catch
            Finally
                End
            End Try
        End If
    End Sub

#End Region

    Private Sub LicenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LicenseToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("COPYING.txt")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#Region " Form Stuff "


    Private Sub SpellCasterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellCasterTrigger.CheckedChanged
        Try
            If SpellCasterTrigger.Checked Then
                If Core.SpellTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(SpellCasterSpell.Text) Then
                    SpellCasterTrigger.Checked = False
                    MessageBox.Show("The spell must not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If SpellCasterMinimumManaPoints.Value = 0 Then
                    SpellCasterTrigger.Checked = False
                    MessageBox.Show("The spell minimum mana points must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.SpellMsg = SpellCasterSpell.Text
                Core.SpellManaRequired = SpellCasterMinimumManaPoints.Value
                Core.SpellTimerObj.StartTimer()
            Else
                Core.SpellTimerObj.StopTimer()
                Core.SpellMsg = String.Empty
                Core.SpellManaRequired = 0
            End If
            RefreshSpellCasterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RunemakerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunemakerTrigger.CheckedChanged
        Try
            If RunemakerTrigger.Checked Then
                If Core.RunemakerTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(RunemakerSpell.Text) Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The spell must not be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If RunemakerMinimumManaPoints.Value = 0 Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker minimum mana points must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Dim Found As Boolean = False
                For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                    If Spell.Name.Equals(RunemakerSpell.Text) Then
                        Core.RunemakerSpell = Spell
                        Found = True
                        Exit For
                    End If
                Next
                If Not Found Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker spell was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.RunemakerManaPoints = RunemakerMinimumManaPoints.Value
                Core.RunemakerSoulPoints = RunemakerMinimumSoulPoints.Value
                Core.RunemakerTimerObj.StartTimer()
            Else
                Core.RunemakerTimerObj.StopTimer()
                Core.RunemakerManaPoints = 0
                Core.RunemakerSoulPoints = 0
                Core.RunemakerSpell = Nothing
            End If
            RefreshRunemakerControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterSmart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterSmart.CheckedChanged
        Try
            AutoEaterMinimumHitPoints.Enabled = AutoEaterSmart.Checked
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterTrigger.CheckedChanged
        Try
            If AutoEaterTrigger.Checked Then
                If Core.EaterTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If AutoEaterSmart.Checked Then
                    If AutoEaterMinimumHitPoints.Value = 0 Then
                        AutoEaterTrigger.Checked = False
                        MessageBox.Show("The minimum hit points when the Auto Smart Eater feature is on must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Core.AutoEaterSmart = AutoEaterMinimumHitPoints.Value
                Else
                    Core.AutoEaterSmart = 0
                End If
                If AutoEaterInterval.Value = 0 Then
                    AutoEaterTrigger.Checked = False
                    MessageBox.Show("The auto eater delay must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.EaterTimerObj.Interval = AutoEaterInterval.Value
                Core.EaterTimerObj.StartTimer()
            Else
                Core.AutoEaterSmart = 0
                Core.EaterTimerObj.StopTimer()
            End If
            RefreshAutoEaterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoEaterEatFromFloor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterEatFromFloor.CheckedChanged
        AutoEaterEatFromFloorFirst.Enabled = AutoEaterEatFromFloor.Checked
        Consts.EatFromFloor = AutoEaterEatFromFloor.Checked
        'If Not AutoEaterEatFromFloorFirst.Enabled Then
        '    AutoEaterEatFromFloorFirst.Checked = False
        'End If
    End Sub

    Private Sub AutoEaterDelay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterInterval.ValueChanged
        If AutoEaterSmart.Checked Then
            Consts.AutoEaterSmartInterval = AutoEaterInterval.Value
        Else
            Consts.AutoEaterInterval = AutoEaterInterval.Value
        End If
    End Sub

    Private Sub AutoEaterEatFromFloorFirst_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoEaterEatFromFloorFirst.CheckedChanged
        Consts.EatFromFloorFirst = AutoEaterEatFromFloorFirst.Checked
    End Sub

    Private Sub ConfigLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigLoad.Click
        Try
            'Core.ConsoleWrite("Please wait...")
            Dim Data As String = ""
            Dim Reader As IO.StreamReader
            Reader = IO.File.OpenText(Core.GetProfileDirectory() & "\config.txt")
            Data = Reader.ReadToEnd
            Dim MCollection As MatchCollection
            Dim GroupMatch As Match
            MCollection = [Regex].Matches(Data, "&([^\n;]+)[;]?")
            For Each GroupMatch In MCollection
                CommandParser(GroupMatch.Groups(1).Value)
            Next
            MessageBox.Show("Done loading your configuration.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show("Unable to load your configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EditConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigEdit.Click
        Try
            Dim ConfigWindow As New frmConfigEdit
            If ConfigWindow.ShowDialog() = Forms.DialogResult.OK Then
                MessageBox.Show("Your configuration has been saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch
        End Try
    End Sub

    Private Sub ClearConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigClear.Click
        Try
            IO.File.Delete(Core.GetProfileDirectory() & "\config.txt")
            MessageBox.Show("Your configuration file has been cleared.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show("Unable to clear your configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AutoLooterTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterTrigger.CheckedChanged
        'Static FirstTime As Boolean = True
        Try
            'If FirstTime Then
            'FirstTime = False
            'Exit Sub
            'End If
            If AutoLooterTrigger.Checked Then
                Core.LooterMinimumCapacity = AutoLooterMinCap.Value
                Core.LooterTimerObj.StartTimer()
            Else
                Core.LooterTimerObj.StopTimer()
                Core.LooterMinimumCapacity = 0
            End If
            RefreshAutoLooterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoLooterEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterConfigure.Click
        CoreModule.LootItems.ShowLootCategories()
    End Sub

    Private Sub AutoStackerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoStackerTrigger.CheckedChanged
        Try
            If AutoStackerTrigger.Checked Then
                Core.StackerTimerObj.Interval = Consts.AutoStackerDelay
                Core.StackerTimerObj.StartTimer()
            Else
                Core.StackerTimerObj.StopTimer()
            End If
            RefreshAutoStackerControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub LightEffectsTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightEffectsTrigger.CheckedChanged
        Try
            If LightEffectsTrigger.Checked Then
                If Core.LightTimerObj.State = ThreadTimerState.Running Then Exit Sub
                Select Case LightEffect.Text.ToLower
                    Case "on"
                        Core.LightC = ITibia.LightColor.BrightSword
                        Core.LightI = ITibia.LightIntensity.Huge + 2
                    Case "torch"
                        Core.LightI = ITibia.LightIntensity.Medium
                        Core.LightC = ITibia.LightColor.Torch
                    Case "great torch"
                        Core.LightI = ITibia.LightIntensity.VeryLarge
                        Core.LightC = ITibia.LightColor.Torch
                    Case "ultimate torch"
                        Core.LightI = ITibia.LightIntensity.Huge
                        Core.LightC = ITibia.LightColor.Torch
                    Case "utevo lux"
                        Core.LightI = ITibia.LightIntensity.Medium
                        Core.LightC = ITibia.LightColor.UtevoLux
                    Case "utevo gran lux"
                        Core.LightI = ITibia.LightIntensity.Large
                        Core.LightC = ITibia.LightColor.UtevoLux
                    Case "utevo vis lux"
                        Core.LightI = ITibia.LightIntensity.VeryLarge
                        Core.LightC = ITibia.LightColor.UtevoLux
                    Case "light wand"
                        Core.LightI = ITibia.LightIntensity.Large
                        Core.LightC = ITibia.LightColor.LightWand
                    Case Else
                        MessageBox.Show("You must select a Light Effect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        LightEffectsTrigger.Checked = False
                        Exit Sub
                End Select
                Core.LightTimerObj.StartTimer()
            Else
                Core.SetLight(ITibia.LightIntensity.Small, ITibia.LightColor.UtevoLux)
                Core.LightTimerObj.StopTimer()
            End If
            RefreshLightEffectsControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AmmoRestackerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmunitionRestackerTrigger.CheckedChanged
        Try
            If AmmunitionRestackerTrigger.Checked Then
                If Core.AmmoRestackerTimerObj.State = ThreadTimerState.Running Then Exit Sub
                Dim ItemID As Integer
                Dim ItemCount As Integer
                Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), ItemID, 2)
                Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, ItemCount, 1)
                If ItemID = 0 OrElse Not Core.Client.Dat.GetInfo(ItemID).IsStackable Then
                    MessageBox.Show("You must place some of your ammunition on the Belt/Arrow Slot first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.AmmoRestackerItemID = ItemID
                Core.AmmoRestackerOutOfAmmo = False
                Core.AmmoRestackerMinimumItemCount = AmmunitionRestackerMinAmmo.Value
                Core.AmmoRestackerTimerObj.StartTimer()
            Else
                Core.AmmoRestackerItemID = 0
                Core.AmmoRestackerTimerObj.StopTimer()
            End If
            RefreshAmmoRestackerControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ComboBotTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBotTrigger.CheckedChanged
        Try
            If ComboBotTrigger.Checked Then
                If Core.ComboBotEnabled = True Then Exit Sub
                Core.ComboBotLeader = ComboLeader.Text
                Core.ComboBotEnabled = True
            Else
                Core.ComboBotEnabled = False
            End If
            RefreshComboBotControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoFisherTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoFisherTrigger.CheckedChanged
        Try
            If AutoFisherTrigger.Checked Then
                If Core.FisherTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If AutoFisherMinimumCapacity.Value = vbNull Then
                    MessageBox.Show("Please give the minimium capacity for fisher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    AutoFisherTrigger.Checked = False
                    Exit Sub
                End If
                If AutoFisherTurbo.Checked Then
                    Core.FisherMinimumCapacity = AutoFisherMinimumCapacity.Value
                    Core.FisherTurbo = True
                    Core.FisherSpeed = 500
                    Core.FisherTimerObj.StartTimer()
                Else
                    Core.FisherMinimumCapacity = AutoFisherMinimumCapacity.Value
                    Core.FisherSpeed = 0
                    Core.FisherTurbo = False
                    Core.FisherTimerObj.StartTimer()
                End If
            Else
                Core.FisherMinimumCapacity = 0
                Core.FisherSpeed = 0
                Core.FisherTurbo = False
                Core.FisherTimerObj.StopTimer()
            End If
            RefreshAutoFisherControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CavebotTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CavebotTrigger.CheckedChanged
        Try
            Dim SP As New ServerPacketBuilder(Core.Proxy)
            If CavebotTrigger.Checked Then
                Core.WaypointIndex = SelectNearestWaypoint(Core.Walker_Waypoints)
                If Core.WaypointIndex = -1 Then
                    MessageBox.Show("No waypoints found or they are not in current floor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    CavebotTrigger.Checked = False
                    Exit Sub
                End If
                If Consts.LootWithCavebot Then
                    Core.LooterMinimumCapacity = Consts.CavebotLootMinCap
                    Core.LooterTimerObj.StartTimer()
                End If
                Core.AutoAttackerTimerObj.StartTimer()
                Core.CaveBotTimerObj.StartTimer()
                Core.AutoEaterSmart = 0
                Core.EaterTimerObj.Interval = 20000
                Core.EaterTimerObj.StartTimer()
                Core.IsOpeningReady = True
                Core.CBCreatureDied = False
                Core.WaypointIndex = 0
                Core.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)

                SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                Core.CBState = CavebotState.Walking
            Else
                Core.LooterTimerObj.StopTimer()
                Core.AutoAttackerTimerObj.StopTimer()
                Core.CaveBotTimerObj.StopTimer()
                Core.EaterTimerObj.StopTimer()
                Core.EaterTimerObj.Interval = 0
                Core.WaypointIndex = 0
                Core.IsOpeningReady = True
                SP.StopEverything()
                'Core.Proxy.SendPacketToServer(PacketUtils.AttackEntity(0))
                Core.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
            End If
            RefreshCavebotControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CavebotConfigure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CavebotConfigure.Click
        Core.CavebotForm.Show()
    End Sub

    Private Sub TradeChannelWatcherTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TradeChannelWatcherTrigger.CheckedChanged
        Try
            If TradeChannelWatcherTrigger.Checked Then
                If Core.TradeWatcherActive = True Then Exit Sub
                If String.IsNullOrEmpty(TradeChannelWatcherExpression.Text) Then
                    MessageBox.Show("Please give Expression!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Dim RegExp As Regex
                Try
                    RegExp = New Regex(TradeChannelWatcherExpression.Text)
                    Core.TradeWatcherRegex = TradeChannelWatcherExpression.Text
                    Core.TradeWatcherActive = True
                    Core.ConsoleWrite("Make sure you have the Trade-Channel opened.")
                Catch exep As Exception
                    MessageBox.Show("Sorry, but this is not a valid regular expression." & ControlChars.NewLine & _
                    "See http://en.wikipedia.org/wiki/Regular_expression for more information on regular expressions.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                Core.TradeWatcherActive = False
                Core.TradeWatcherRegex = ""
            End If
            RefreshTradeChannelWatcherControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub StatsUploaderTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatsUploaderTrigger.CheckedChanged
        Try
            If StatsUploaderTrigger.Checked Then
                If Core.StatsUploaderTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If StatsUploaderSaveToDisk.Checked Then
                    If StatsUploaderPath.Text.Length = 0 OrElse StatsUploaderFilename.Text.Length = 0 Then
                        MessageBox.Show("Please don't leave empty values to text boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshStatsUploaderControls()
                        Exit Sub
                    End If
                    Core.UploaderUrl = StatsUploaderUrl.Text
                    Core.UploaderFilename = StatsUploaderFilename.Text
                    Core.UploaderPath = StatsUploaderPath.Text
                    Core.UploaderUserId = StatsUploaderUser.Text
                    Core.UploaderPassword = StatsUploaderPassword.Text
                    Core.UploaderSaveToDiskOnly = StatsUploaderSaveToDisk.Checked
                    Core.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                Else
                    If StatsUploaderUrl.Text.Length = 0 _
                     OrElse StatsUploaderUser.Text.Length = 0 _
                     OrElse StatsUploaderPassword.Text.Length = 0 _
                     OrElse Consts.StatsUploaderFrequency = 0 Then
                        MessageBox.Show("Don't leave empty values on text boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshStatsUploaderControls()
                        Exit Sub
                    End If
                    Core.UploaderUrl = StatsUploaderUrl.Text
                    Core.UploaderFilename = StatsUploaderFilename.Text
                    Core.UploaderPath = StatsUploaderPath.Text
                    Core.UploaderUserId = StatsUploaderUser.Text
                    Core.UploaderPassword = StatsUploaderPassword.Text
                    Core.UploaderSaveToDiskOnly = StatsUploaderSaveToDisk.Checked
                    Core.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                End If
            Else
                Core.StatsUploaderTimerObj.StopTimer()
            End If
            RefreshStatsUploaderControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not Core.IRCClient Is Nothing Then
            If Core.IRCClient.IsConnected Then
                Core.IRCClient.Quit()
            End If
            If Not Core.IRCClient.DoMainLoopThread Is Nothing Then
                Core.IRCClient.DoMainLoopThread.Abort()
            End If
            If Not Core.Client Is Nothing Then
                Core.Client.Close()
            End If
        End If
        NotifyIcon.Visible = False
    End Sub

    Private Sub FpsChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FpsChangerTrigger.CheckedChanged
        Try
            If FpsChangerTrigger.Checked Then
                If Core.FPSChangerTimerObj.State = ThreadTimerState.Running Then Exit Sub
                Core.FrameRateActive = FpsActive.Value
                Core.FrameRateInactive = FpsInactive.Value
                Core.FrameRateMinimized = FpsMinimized.Value
                Core.FrameRateHidden = FPSHidden.Value
                Core.FPSChangerTimerObj.StartTimer()
            Else
                Core.FPSChangerTimerObj.StopTimer()
                Core.Client.SetFramesPerSecond(Core.FrameRateActive)
            End If
            RefreshFpsChangerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub TradeChannelAdvertiserTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TradeChannelAdvertiserTrigger.CheckedChanged
        Try
            If TradeChannelAdvertiserTrigger.Checked Then
                If Core.AdvertiseTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(TradeChannelAdvertiserAdvertisement.Text) Then Exit Sub
                'OpenChannel("Trade", ChannelType.Trade)
                Core.AdvertiseMsg = TradeChannelAdvertiserAdvertisement.Text
                Core.AdvertiseTimerObj.StartTimer(1000)
            Else
                Core.AdvertiseMsg = ""
                Core.AdvertiseTimerObj.StopTimer()
            End If
            RefreshAdvertiserControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoLooterEatFromCorpse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterEatFromCorpse.CheckedChanged
        Consts.LootEatFromCorpse = AutoLooterEatFromCorpse.Checked
    End Sub

    Private Sub AutoLooterDelay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterDelay.ValueChanged
        Consts.LootDelay = AutoLooterDelay.Value
    End Sub

    Private Sub AutoStackerDelay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoStackerDelay.ValueChanged
        Consts.AutoStackerDelay = AutoStackerDelay.Value
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Core.Client.IsConnected Then
            RefreshControls()
            MainTabControl.Enabled = True
        Else
            MainTabControl.Enabled = False
        End If

    End Sub

    Private Sub TabPage10_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage10.Enter
        Dim R As New Random(System.DateTime.Now.Millisecond)
        Select Case R.Next(0, 5)
            Case 0
                Me.PictureBox1.Image = My.Resources.ttb_splash0
            Case 1
                Me.PictureBox1.Image = My.Resources.ttb_splash1
            Case 2
                Me.PictureBox1.Image = My.Resources.ttb_splash2
            Case 3
                Me.PictureBox1.Image = My.Resources.ttb_splash3
            Case 4
                Me.PictureBox1.Image = My.Resources.ttb_splash4
        End Select
    End Sub

    Private Sub MiscReloadSpellsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadSpellsButton.Click
        Try
            CoreModule.Spells.LoadSpells()
            MessageBox.Show("Done loading the Spells configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadItemsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadItemsButton.Click
        Try
            Core.Client.Items.Refresh()
            MessageBox.Show("Done loading the Items configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadOutfitsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadOutfitsButton.Click
        Try

            CoreModule.Outfits.LoadOutfits()
            MessageBox.Show("Done loading the Outfits configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadConstantsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadConstantsButton.Click
        Try
            Consts.LoadConstants()
            MessageBox.Show("Done loading the Constants configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadTibiaDatButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadTibiaDatButton.Click
        Try
            Core.Client.Dat.Refresh()
            MessageBox.Show("Done loading the Tibia.dat file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MCPatcherButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCPatcherButton.Click
        MCPatcher()
    End Sub

    Private Sub FakeTitleTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FakeTitleTrigger.CheckedChanged
        Try
            If FakeTitleTrigger.Checked Then
                If String.IsNullOrEmpty(FakeTitle.Text) Then
                    Beep()
                    FakeTitleTrigger.Checked = False
                    Exit Sub
                End If
                Core.LastExperience = 0
                If Core.ExpCheckerActivated Then
                    Core.ExpCheckerActivated = False
                End If
                Core.FakingTitle = True
                Core.Client.Title = FakeTitle.Text
            Else
                Core.FakingTitle = False
                Core.Client.Title = BotName & " - " & Core.Client.CharacterName
            End If

        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ChameleonCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChameleonCopy.Click
        Try
            Dim BL As New BattleList
            BL.Reset()
            Dim Found As Boolean = False
            Do
                If BL.GetName.Equals(ChameleonPlayer.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    Found = True
                    Exit Do
                End If
            Loop While BL.NextEntity()
            If Found Then
                Dim OD As New OutfitDefinition
                Dim ODFound As Boolean = CoreModule.Outfits.GetOutfitByID(BL.OutfitID, OD)
                If ODFound Then
                    ChameleonOutfit.SelectedIndex = ChameleonOutfit.Items.IndexOf(OD.Name)
                    Select Case BL.OutfitAddons
                        Case IBattlelist.OutfitAddons.First
                            ChameleonFirst.Checked = True
                        Case IBattlelist.OutfitAddons.Second
                            ChameleonSecond.Checked = True
                        Case IBattlelist.OutfitAddons.Both
                            ChameleonBoth.Checked = True
                        Case Else
                            ChameleonNone.Checked = True
                    End Select
                    ChameleonPlayer.Text = String.Empty
                Else
                    MessageBox.Show("The player has an unknown outfit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Unable to copy outfit, the player was not found.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ChameleonOutfit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChameleonOutfit.SelectedIndexChanged
        Static FirstTime As Boolean = True
        If FirstTime Then
            FirstTime = False
            Exit Sub
        End If
        Try
            Dim OD As New OutfitDefinition
            Dim ODFound As Boolean = CoreModule.Outfits.GetOutfitByName(ChameleonOutfit.Text, OD)
            If ODFound Then
                Dim BL As New BattleList
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                BL.OutfitID = OD.ID
            Else
                MessageBox.Show("Unknown outfit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ChameleonNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChameleonNone.CheckedChanged, ChameleonSecond.CheckedChanged, ChameleonFirst.CheckedChanged, ChameleonBoth.CheckedChanged
        If Core.Client Is Nothing OrElse Not Core.Client.IsConnected Then Exit Sub
        Dim BL As New BattleList
        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
        If ChameleonFirst.Checked Then
            BL.OutfitAddons = IBattlelist.OutfitAddons.First
        ElseIf ChameleonSecond.Checked Then
            BL.OutfitAddons = IBattlelist.OutfitAddons.Second
        ElseIf ChameleonBoth.Checked Then
            BL.OutfitAddons = IBattlelist.OutfitAddons.Both
        Else
            BL.OutfitAddons = IBattlelist.OutfitAddons.None
        End If
    End Sub

    Private Sub frmMain_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        Beep()
    End Sub

    Private Sub ExpCheckerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpCheckerTrigger.CheckedChanged
        Try
            If ExpCheckerTrigger.Checked Then
                If ExpShowNext.Checked Then
                    If Core.FakingTitle Then
                        Core.FakingTitle = False
                        MessageBox.Show("Fake Title is now Disabled.")
                    End If
                    Core.LastExperience = 0
                    Core.ExpCheckerActivated = True
                End If
                If ExpShowCreatures.Checked Then
                    Core.ShowCreaturesUntilNextLevel = True
                End If
            Else
                Core.ShowCreaturesUntilNextLevel = False
                Core.ExpCheckerActivated = False
                Core.LastExperience = 0
                Core.Client.Title = BotName & " - " & Core.Client.CharacterName
            End If
            RefreshExpCheckerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FloorExplorerLook(ByVal Direction As String)
        Try
            Dim Floor As Short = 0
            Dim EntityCount As Integer = 0
            Dim EntityList As New List(Of String)
            Dim EntityListCount As New List(Of Integer)
            Dim EntityListIndex As Integer
            Dim EntityName As String = ""
            Dim Output As String = ""
            Dim I As Integer
            Dim BL As BattleList = New BattleList
            Dim Found As Boolean = False
            Select Case Direction.ToLower
                Case "down", "below", "downstairs", "v", "\/"
                    Floor = 1
                Case "up", "above", "upstairs", "/\", "^"
                    Floor = -1
                Case "around"
                    Floor = 0
            End Select
            BL.Reset(True)
            Do
                If BL.IsMyself OrElse BL.GetFloor <> Core.CharacterLoc.Z + Floor Then Continue Do
                EntityName = BL.GetName
                EntityListIndex = EntityList.IndexOf(EntityName)
                If EntityListIndex > -1 Then
                    EntityListCount(EntityListIndex) += 1
                Else
                    EntityList.Add(EntityName)
                    EntityListCount.Add(1)
                    EntityCount += 1
                End If
            Loop While BL.NextEntity(True)
            If EntityCount = 0 Then
                Output = "Nothing"
            Else
                For I = 0 To EntityCount - 1
                    If (I > 0) And (I <= EntityCount - 1) Then Output = Output & ", "
                    If EntityListCount(I) = 1 Then
                        Output &= EntityList(I)
                    Else
                        Output &= EntityList(I) & "(" & EntityListCount(I) & "x)"
                    End If
                Next
            End If
            MessageBox.Show(Output & ".", "Entities Found:")
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FloorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FloorUp.Click
        FloorExplorerLook("up")
    End Sub

    Private Sub FloorAround_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FloorAround.Click
        FloorExplorerLook("around")
    End Sub

    Private Sub FloorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FloorDown.Click
        FloorExplorerLook("down")
    End Sub

    Private Sub NameSpyTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameSpyTrigger.CheckedChanged
        Try
            If NameSpyTrigger.Checked Then
                Core.Client.WriteMemory(Consts.ptrNameSpy, &H9090, 2)
                Core.Client.WriteMemory(Consts.ptrNameSpy2, &H9090, 2)
                Core.NameSpyActivated = True
            Else
                Core.Client.WriteMemory(Consts.ptrNameSpy, Consts.NameSpyDefault, 2)
                Core.Client.WriteMemory(Consts.ptrNameSpy2, Consts.NameSpy2Default, 2)
                Core.NameSpyActivated = False
            End If
            RefreshNameSpyControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenWebsite.Click
        Try
            If WebsiteName.Text = vbNullString Then
                MessageBox.Show("Please enter the Url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim Prepend As String = ""
            Select Case WebsiteName.Text.ToLower
                Case "tibia wiki"
                    If String.IsNullOrEmpty(SearchFor.Text) Then
                        MessageBox.Show("Please enter search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Prepend = "http://tibia.wikia.com/wiki/"
                Case "tibia.com character"
                    If String.IsNullOrEmpty(SearchFor.Text) Then
                        MessageBox.Show("Please enter search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Prepend = "http://www.tibia.com/community/?subtopic=character&name="
                Case "tibia.com guild"
                    If String.IsNullOrEmpty(SearchFor.Text) Then
                        MessageBox.Show("Please enter search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Prepend = "http://www.tibia.com/community/?subtopic=guilds&page=view&GuildName="
                Case "erig.net highscore"
                    If String.IsNullOrEmpty(SearchFor.Text) Then
                        MessageBox.Show("Please enter search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Prepend = "http://www.erig.net/xphist.php?player="
                Case "google"
                    If String.IsNullOrEmpty(SearchFor.Text) Then
                        MessageBox.Show("Please enter search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Prepend = "http://www.google.com/search?q="
                Case Else
                    Core.OpenCommand = WebsiteName.Text
                    If Not String.IsNullOrEmpty(SearchFor.Text) Then MessageBox.Show("Note: Search criteria works only with pre-defined urls.")
                    If Not Core.BGWOpenCommand.IsBusy Then
                        Core.BGWOpenCommand.RunWorkerAsync()
                        Exit Sub
                    Else
                        MessageBox.Show("Busy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
            End Select
            If WebsiteName.Text.ToLower = "tibia wiki" Then
                Core.OpenCommand = Prepend & SearchFor.Text.Replace(" ", "_")
            Else
                Core.OpenCommand = Prepend & SearchFor.Text
            End If
            If Not Core.BGWOpenCommand.IsBusy Then
                Core.BGWOpenCommand.RunWorkerAsync()
            Else
                MessageBox.Show("Busy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SendLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendLocation.Click
        Try
            If String.IsNullOrEmpty(SendLocationTo.Text) Then
                MessageBox.Show("Please enter the name of the player.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If Not Core.BGWSendLocation.IsBusy Then
                Core.SendLocationDestinatary = SendLocationTo.Text
                Core.BGWSendLocation.RunWorkerAsync()
            Else
                MessageBox.Show("Busy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrainerAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainerAdd.Click
        Try
            Dim BL As New BattleList
            If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                    MessageBox.Show("You must be attacking or following something.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            If Core.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                MessageBox.Show("This entity is already in your list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Core.AutoTrainerEntities.Add(BL.GetEntityID)
                MessageBox.Show("This entity has been added to your list.")
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrainerRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainerRemove.Click
        Try
            Dim BL As New BattleList
            If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                    MessageBox.Show("You must be attacking or following something.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            If Core.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                Core.AutoTrainerEntities.Remove(BL.GetEntityID)
                MessageBox.Show("This entity has been removed from your list.")
            Else
                MessageBox.Show("This entity is not on your list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrainerClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainerClear.Click
        Try
            Core.AutoTrainerEntities.Clear()
            MessageBox.Show("Auto Trainer entities list cleared.")
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrainerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainerTrigger.CheckedChanged
        Try
            If TrainerTrigger.Checked Then
                If MinPercentageHP.Value >= MaxPercentageHP.Value Then
                    MessageBox.Show("Maximum Health Percent has to be higher than Minimum Health Percent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If Core.AutoTrainerEntities.Count = 0 Then
                    MessageBox.Show("You have to add entities to the training list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Core.AutoTrainerMinHPPercent = MinPercentageHP.Value
                Core.AutoTrainerMaxHPPercent = MaxPercentageHP.Value
                Core.AutoTrainerTimerObj.StartTimer()
            Else
                Core.AutoTrainerMinHPPercent = 0
                Core.AutoTrainerMaxHPPercent = 0
                Core.AutoTrainerTimerObj.StopTimer()
            End If
            RefreshTrainerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AutoAttackerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoAttackerTrigger.CheckedChanged
        Try
            Dim SP As New ServerPacketBuilder(Core.Proxy)
            If AutoAttackerTrigger.Checked Then

                Select Case AttackerFightingMode.Text.ToLower
                    Case "offensive"
                        Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Offensive, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Offensive)
                    Case "balanced"
                        Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Balanced, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Balanced)
                        'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Balanced))
                    Case "defensive"
                        Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Defensive, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Defensive)
                        'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Defensive))
                End Select
                Select Case AttackChasingMode.Text.ToLower
                    Case "chase"
                        Core.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                    Case "stand"
                        Core.Client.WriteMemory(Consts.ptrChasingMode, 0, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Standing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Standing))
                End Select
                If AttackAutomatically.Checked Then
                    Core.AutoAttackerTimerObj.StartTimer()
                End If
                Core.AutoAttackerActivated = True
            Else
                Core.AutoAttackerActivated = False
                Core.AutoAttackerIgnoredID = 0
                Core.AutoAttackerTimerObj.StopTimer()
            End If
            RefreshAutoAttackerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PickuperTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickuperTrigger.CheckedChanged
        Try
            If PickuperTrigger.Checked Then
                Dim RightHandItemID As Integer
                Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                If RightHandItemID = 0 OrElse Not Core.Client.Items.IsThrowable(RightHandItemID) Then
                    MessageBox.Show("You must have a throwable item in your right hand, like a spear, throwing knife, etc.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                With Core
                    .PickUpItemID = CUShort(RightHandItemID)
                    .PickUpTimerObj.Interval = Consts.AutoPickUpDelay
                    .PickUpTimerObj.StartTimer()
                End With
            Else
                With Core
                    .PickUpItemID = 0
                    .PickUpTimerObj.StopTimer()
                End With
            End If
            RefreshPickuperControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrainerInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainerInfo.Click
        MessageBox.Show("Train with as many monsters as you want. To add monsters, put them on follow and click Add Creature button." & Environment.NewLine & _
                        "To start training define max hp% and min hp% and press Activate, and you will hurt the creatures until <min hp%> and continue attacking after <max hp%>.", "How to use Trainer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub AntiIdlerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AntiIdlerTrigger.CheckedChanged
        Try
            If AntiIdlerTrigger.Checked Then
                If Core.AntiLogoutObj.State = ThreadTimerState.Running Then Exit Sub
                Core.LastActivity = Date.Now
                Core.AntiLogoutObj.Interval = Consts.AntiLogoutInterval
                Core.AntiLogoutObj.StartTimer()
            Else
                Core.AntiLogoutObj.StopTimer()
            End If
            RefreshAntiIdlerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RingChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RingChangerTrigger.CheckedChanged
        Try
            If RingChangerTrigger.Checked Then
                Core.RingID = Core.Client.Items.GetItemID(ChangerRingType.Text)
                If Core.RingID = 0 Then 'AndAlso Core.Client.Items.IsRing(Core.RingID) Then <-- WTF O.o
                    MessageBox.Show("Invalid Ring Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshChangerControls()
                    Exit Sub
                End If
                Core.RingChangerTimerObj.StartTimer()
            Else
                Core.RingChangerTimerObj.StopTimer()
                Core.RingID = 0
            End If
            RefreshChangerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AmuletChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmuletChangerTrigger.CheckedChanged
        Try
            If AmuletChangerTrigger.Checked Then
                Core.AmuletID = Core.Client.Items.GetItemID(ChangerAmuletType.Text)
                If Core.AmuletID = 0 Then ' AndAlso Core.Client.Items.IsNeck(Core.AmuletID) Then <-- WTF O.o
                    MessageBox.Show("Invalid Amulet/Necklace Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshChangerControls()
                    Exit Sub
                End If
                Core.AmuletChangerTimerObj.StartTimer()
            Else
                Core.AmuletChangerTimerObj.StopTimer()
                Core.AmuletID = 0
            End If
            RefreshChangerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub StatsUploaderSaveToDisk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatsUploaderSaveToDisk.CheckedChanged
        StatsUploaderPassword.Enabled = Not StatsUploaderSaveToDisk.Checked
        StatsUploaderUser.Enabled = Not StatsUploaderSaveToDisk.Checked
        StatsUploaderUrl.Enabled = Not StatsUploaderSaveToDisk.Checked
    End Sub

    Private Sub TradeChannelWatcherHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TradeChannelWatcherBuilder.Click
        Dim RegExpForm As New frmRegExpBuilder
        RegExpForm.Show()
    End Sub

    Private Sub HealWithPotion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealWithPotion.CheckedChanged
        Try
            If HealWithPotion.Checked Then
                If Core.PotionTimerObj.State = ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealPotionUseHp.Checked Then
                    Core.PotionHPRequired = HealPotionHp.Value
                Else
                    Core.PotionHPRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case HealPotionName.Text
                    Case "Health Potion"
                        Core.PotionID = Core.Client.Items.GetItemID("Health Potion")
                    Case "Strong Health Potion"
                        Core.PotionID = Core.Client.Items.GetItemID("Strong Health Potion")
                    Case "Great Health Potion"
                        Core.PotionID = Core.Client.Items.GetItemID("Great Health Potion")
                    Case Else
                        MessageBox.Show("You must select the Potion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Core.PotionID = 0 Then
                    MessageBox.Show("Unknown error occured selecting Potion Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Core.PotionTimerObj.StartTimer()
                End If
            Else
                Core.PotionTimerObj.StopTimer()
                Core.PotionHPRequired = 0
                Core.PotionID = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealWithRune_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealWithRune.CheckedChanged
        Try
            If HealWithRune.Checked Then
                If Core.UHTimerObj.State = ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealRuneUseHp.Checked Then
                    Core.UHHPRequired = HealRuneHP.Value
                Else
                    Core.UHHPRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case HealRuneType.Text
                    Case "UH Rune"
                        Core.UHId = Core.Client.Items.GetItemID("Ultimate Healing")
                    Case "IH Rune"
                        Core.UHId = Core.Client.Items.GetItemID("Intense Healing")
                    Case Else
                        MessageBox.Show("You must select the Rune.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Core.UHId = 0 Then
                    MessageBox.Show("Unknown error occured selecting Rune Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Core.UHTimerObj.StartTimer()
                End If
            Else
                Core.UHTimerObj.StopTimer()
                Core.UHHPRequired = 0
                Core.UHId = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealWithSpell_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealWithSpell.CheckedChanged
        Try
            If HealWithSpell.Checked Then
                If Core.HealTimerObj.State = ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealSpellUseHP.Checked Then
                    Core.HealMinimumHP = HealSpellHp.Value
                Else
                    Core.HealMinimumHP = MaxHitPoints * (HealSpellPercent.Value / 100)
                End If
                For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                    If Spell.Name.Equals(HealSpellName.Text, StringComparison.CurrentCultureIgnoreCase) OrElse Spell.Words.Equals(HealSpellName.Text, StringComparison.CurrentCultureIgnoreCase) Then
                        Select Case Spell.Name.ToLower
                            Case "light healing", "heal friend", "mass healing", "intense healing", "ultimate healing", "divine healing", "wound cleansing"
                                Core.HealSpell = Spell
                                Exit For
                            Case Else
                                MessageBox.Show("Please select a healing spell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                RefreshHealerControls()
                                Exit Sub
                        End Select
                    End If
                Next
                Core.HealComment = ""
                Core.HealTimerObj.StartTimer()
            Else
                Core.HealTimerObj.StopTimer()
                Core.HealMinimumHP = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DrinkerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub RestoreManaWith_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreManaWith.CheckedChanged
        Try
            If RestoreManaWith.Checked Then
                If Core.ManaPotionTimerObj.State = ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If RestoreManaWith.Checked Then
                    Core.DrinkerManaRequired = DrinkerManaPoints.Value
                Else
                    Core.DrinkerManaRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case ManaPotionName.Text
                    Case "Mana Potion"
                        Core.ManaPotionID = Core.Client.Items.GetItemID("Mana Potion")
                    Case "Strong Mana Potion"
                        Core.ManaPotionID = Core.Client.Items.GetItemID("Strong Mana Potion")
                    Case "Great Mana Potion"
                        Core.ManaPotionID = Core.Client.Items.GetItemID("Great Mana Potion")
                    Case Else
                        MessageBox.Show("You must select the Mana Potion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Core.ManaPotionID = 0 Then
                    MessageBox.Show("Unknown error occured selecting Potion Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Core.ManaPotionTimerObj.StartTimer()
                End If
            Else
                Core.ManaPotionTimerObj.StopTimer()
                Core.DrinkerManaRequired = 0
                Core.ManaPotionID = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub HealRuneUseHp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealRuneUseHp.CheckedChanged
        RefreshHealerControls()
    End Sub

    Private Sub HealSpellUseHP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealSpellUseHP.CheckedChanged
        RefreshHealerControls()
    End Sub

    Private Sub HealPotionUseHp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealPotionUseHp.CheckedChanged
        RefreshHealerControls()
    End Sub

    Private Sub DancerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DancerTrigger.CheckedChanged
        Try
            If DancerTrigger.Checked Then
                If Core.DancerTimerObj.State = ThreadTimerState.Running Then Exit Sub
                Select Case DancerSpeed.Text
                    Case "Slow"
                        Core.DancerTimerObj.Interval = 500
                    Case "Fast"
                        Core.DancerTimerObj.Interval = 100
                    Case "Turbo"
                        Core.DancerTimerObj.Interval = 10
                End Select
                Core.DancerTimerObj.StartTimer()
            Else
                Core.DancerTimerObj.StopTimer()
            End If
            RefreshDancerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AmmoMakerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmoMakerTrigger.CheckedChanged
        Try
            If AmmoMakerTrigger.Checked Then
                If Core.AmmoMakerTimerObj.State = ThreadTimerState.Running Then Exit Sub

                Dim Found As Boolean = False
                Dim S As New SpellDefinition
                For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                    If (Spell.Name.Equals(AmmoMakerSpell.Text, StringComparison.CurrentCultureIgnoreCase) _
                    OrElse Spell.Words.Equals(AmmoMakerSpell.Text.ToString, StringComparison.CurrentCultureIgnoreCase)) _
                    AndAlso (Spell.Kind = SpellKind.Ammunition Or Spell.Kind = SpellKind.Incantation) Then
                        S = Spell
                        Found = True
                        Exit For
                    End If
                Next
                If Found Then
                    Core.AmmoMakerSpell = S
                    Core.AmmoMakerMinMana = AmmoMakerMinMana.Value
                    Core.AmmoMakerMinCap = AmmoMakerMinCap.Value
                    Core.AmmoMakerTimerObj.StartTimer()
                Else
                    MessageBox.Show("You cant make ammunitions with the selected spell. Please choose another spell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                Core.AmmoMakerMinMana = 0
                Core.AmmoMakerMinCap = 0
                Core.AmmoMakerTimerObj.StopTimer()
            End If
            RefreshAmmoMakerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealFriendTrigger_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealFriendTrigger.CheckedChanged
        Try
            If HealFriendTrigger.Checked Then
                If Core.HealFriendTimerObj.State = ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(HealFName.Text) Then
                    MessageBox.Show("You must enter the friend's name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealFriendControls()
                    Exit Sub
                End If
                Core.HealFriendHealthPercentage = HealFHp.Value
                Select Case HealFType.Text
                    Case "Ultimate Healing Rune"
                        Core.HealFriendHealType = HealTypes.UltimateHealingRune
                    Case "Exura Sio - Spell"
                        Core.HealFriendHealType = HealTypes.ExuraSio
                    Case "Both"
                        Core.HealFriendHealType = HealTypes.Both
                    Case Else
                        MessageBox.Show("You must select the type for healer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                Core.HealFriendCharacterName = HealFName.Text
                Core.HealFriendTimerObj.StartTimer()
            Else
                Core.HealFriendCharacterName = ""
                Core.HealFriendHealthPercentage = 0
                Core.HealFriendTimerObj.StopTimer()
            End If
            RefreshHealFriendControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub HealPartyTrigger_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealPartyTrigger.CheckedChanged
        Try
            If HealPartyTrigger.Checked Then
                If Core.HealPartyTimerObj.State = ThreadTimerState.Running Then Exit Sub
                Core.HealPartyMinimumHPPercentage = HealPHp.Value
                Select Case HealPType.Text
                    Case "Ultimate Healing Rune"
                        Core.HealPartyHealType = HealTypes.UltimateHealingRune
                    Case "Exura Sio - Spell"
                        Core.HealPartyHealType = HealTypes.ExuraSio
                    Case "Both"
                        Core.HealPartyHealType = HealTypes.Both
                    Case Else
                        MessageBox.Show("You must select the type for healer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                Core.HealPartyTimerObj.StartTimer()
            Else
                Core.HealPartyMinimumHPPercentage = 0
                Core.HealPartyTimerObj.StopTimer()
            End If
            RefreshHealPartyControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region
End Class
