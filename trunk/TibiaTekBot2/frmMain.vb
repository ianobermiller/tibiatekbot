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
 System.Windows.Forms, Scripting, System.Net.Sockets, System.Net

Public Class frmMain

    Dim LoginSelectForm As frmLoginSelectDialog
    Dim SC As frmSplashScreen
    Dim IsVisible As Boolean = True
    Public LoginServer As String

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim R As New Random(System.DateTime.Now.Millisecond)
            Select Case R.Next(0, 7)
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
                Case 5
                    Me.PictureBox1.Image = My.Resources.ttb_splash5
                Case 6
                    Me.PictureBox1.Image = My.Resources.ttb_splash6
            End Select
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
            For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                If Spell.Kind <> ISpells.SpellKind.Rune Then
                    SpellCasterSpell.Items.Add(Spell.Words)
                End If
            Next
            If SpellCasterSpell.Items.Count > 0 Then
                SpellCasterSpell.SelectedIndex = 0
            End If
            'Changers
            'CHANGE THIS WTF!
            For Each Item As IObjects.ObjectDefinition In CType(Kernel.Client.Objects, Objects).Objects
                If Kernel.Client.Objects.IsKind(Item.ItemID, IObjects.ObjectKind.Ring) Then ChangerRingType.Items.Add(Item.Name)
                If Kernel.Client.Objects.IsKind(Item.ItemID, IObjects.ObjectKind.Neck) Then ChangerAmuletType.Items.Add(Item.Name)
            Next
            If ChangerRingType.Items.Count > 0 Then ChangerRingType.SelectedIndex = 0
            If ChangerAmuletType.Items.Count > 0 Then ChangerAmuletType.SelectedIndex = 0
            ' Runemaker
            For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                If Spell.Kind = ISpells.SpellKind.Rune Then
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
            For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                If Spell.Kind = ISpells.SpellKind.Healing Then
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
            Dim Outfits() As IOutfits.OutfitDefinition = Kernel.Outfits.GetOutfits
            For Each Outfit As IOutfits.OutfitDefinition In Outfits
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
            For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                If Spell.Kind = ISpells.SpellKind.Ammunition Or Spell.Kind = ISpells.SpellKind.Incantation Then
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
                'Me.NotifyIcon.Visible = False
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
            Static FirstTime As Boolean = True
            If FirstTime Then
                Kernel.Client.BringToFront()
                FirstTime = False
            End If
            If Kernel.Client.IsConnected Then
                Me.Text = "TibiaTek Bot v" & BotVersion & " - " & Kernel.Client.CharacterName

                FunctionsToolStripMenuItem.Enabled = True
                AboutToolStripMenuItem.Enabled = True
                RefreshControls()
                MainTabControl.Enabled = True
            Else
                If Not (Kernel.Proxy Is Nothing OrElse Kernel.Client Is Nothing) Then
                    Me.Text = "TibiaTek Bot v" & BotVersion & " - " & Hex(Kernel.Client.ProcessHandle)
                Else
                    Me.Text = "TibiaTek Bot v" & BotVersion
                End If
                FunctionsToolStripMenuItem.Enabled = False
                'AboutToolStripMenuItem.Enabled = False
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
            AmmoMakerTrigger.Checked = Kernel.AmmoMakerTimerObj.State = IThreadTimer.ThreadTimerState.Running

            If AmmoMakerTrigger.Checked Then
                AmmoMakerSpell.Text = Kernel.AmmoMakerSpell.Words
                AmmoMakerMinCap.Value = Kernel.AmmoMakerMinCap
                AmmoMakerMinMana.Value = Kernel.AmmoMakerMinMana

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
            DancerTrigger.Checked = Kernel.DancerTimerObj.State = IThreadTimer.ThreadTimerState.Running

            If DancerTrigger.Checked Then
                Select Case Kernel.DancerTimerObj.Interval
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
            RingChangerTrigger.Checked = Kernel.RingChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running
            AmuletChangerTrigger.Checked = Kernel.AmuletChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running

            If RingChangerTrigger.Checked Then
                ChangerRingType.Text = Kernel.Client.Objects.Name(Kernel.RingID)
                ChangerRingType.Enabled = False
            Else
                ChangerRingType.Enabled = True
            End If
            If AmuletChangerTrigger.Checked Then
                ChangerAmuletType.Text = Kernel.Client.Objects.Name(Kernel.AmuletID)
                ChangerAmuletType.Enabled = False
            Else
                ChangerAmuletType.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAntiIdlerControls()
        AntiIdlerTrigger.Checked = Kernel.AntiLogoutObj.State = IThreadTimer.ThreadTimerState.Running
    End Sub
    Private Sub RefreshPickuperControls()
        Try
            PickuperTrigger.Checked = Kernel.PickUpTimerObj.State = IThreadTimer.ThreadTimerState.Running
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshAutoAttackerControls()
        Try
            If Kernel.AutoAttackerTimerObj.State = IThreadTimer.ThreadTimerState.Running Or Kernel.AutoAttackerActivated Then
                AutoAttackerTrigger.Checked = True
            Else
                AutoAttackerTrigger.Checked = False
            End If
            If AutoAttackerTrigger.Checked Then
                If Kernel.AutoAttackerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
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
            TrainerTrigger.Checked = Kernel.AutoTrainerTimerObj.State = IThreadTimer.ThreadTimerState.Running

            If TrainerTrigger.Checked Then
                MinPercentageHP.Value = Kernel.AutoTrainerMinHPPercent
                MaxPercentageHP.Value = Kernel.AutoTrainerMaxHPPercent
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
            Dim OD As New IOutfits.OutfitDefinition
            Dim ODFound As Boolean = Kernel.Outfits.GetOutfitByID(BL.OutfitID, OD)
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
            FakeTitleTrigger.Checked = Kernel.FakingTitle
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
            If Kernel.NameSpyActivated Then
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
            If Kernel.ExpCheckerActivated Or Kernel.ShowCreaturesUntilNextLevel Then
                ExpCheckerTrigger.Checked = True
            Else
                ExpCheckerTrigger.Checked = False
            End If
            If ExpCheckerTrigger.Checked Then
                ExpShowNext.Checked = Kernel.ExpCheckerActivated = True
                ExpShowCreatures.Checked = Kernel.ShowCreaturesUntilNextLevel = True

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
            DrinkerManaPoints.Value = Kernel.DrinkerManaRequired
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
            HealPartyTrigger.Checked = Kernel.HealPartyTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If HealPartyTrigger.Checked Then
                Select Case Kernel.HealPartyHealType
                    Case HealTypes.UltimateHealingRune
                        HealPType.Text = "Ultimate Healing Rune"
                    Case HealTypes.ExuraSio
                        HealPType.Text = "Exura Sio - Spell"
                    Case HealTypes.Both
                        HealPType.Text = "Both"
                End Select
                HealPHp.Value = Kernel.HealPartyMinimumHPPercentage
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
            HealFriendTrigger.Checked = Kernel.HealFriendTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If HealFriendTrigger.Checked Then
                HealFName.Text = Kernel.HealFriendCharacterName
                Select Case Kernel.HealFriendHealType
                    Case HealTypes.UltimateHealingRune
                        HealFType.Text = "Ultimate Healing Rune"
                    Case HealTypes.ExuraSio
                        HealFType.Text = "Exura Sio - Spell"
                    Case HealTypes.Both
                        HealFType.Text = "Both"
                End Select
                HealFHp.Value = Kernel.HealFriendHealthPercentage
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
            HealWithRune.Checked = Kernel.UHTimerObj.State = IThreadTimer.ThreadTimerState.Running
            HealWithSpell.Checked = Kernel.HealTimerObj.State = IThreadTimer.ThreadTimerState.Running
            Dim MaxHitPoints As Integer = 0
            Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 4)
            If HealWithRune.Checked Then
                Select Case Kernel.Client.Objects.Name(Kernel.UHId)
                    Case "Ultimate Healing"
                        HealRuneType.Text = "UH Rune"
                    Case "Intense Healing"
                        HealRuneType.Text = "IH Rune"
                End Select
                HealRuneHP.Value = Kernel.UHHPRequired
                If Kernel.UHHPRequired <= MaxHitPoints Then
                    HealRunePercent.Value = CInt((Kernel.UHHPRequired / MaxHitPoints) * 100)
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
                Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 4)
                HealSpellName.Text = Kernel.HealSpell.Words
                HealSpellHp.Value = Kernel.HealMinimumHP

                If (Kernel.HealMinimumHP <= MaxHitPoints) Then
                    HealSpellPercent.Value = CInt((Kernel.HealMinimumHP / MaxHitPoints) * 100)
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
                Select Case Kernel.Client.Objects.Name(Kernel.PotionID)
                    Case "Health Potion"
                        HealPotionName.Text = "Health Potion"
                    Case "Strong Health Potion"
                        HealPotionName.Text = "Strong Health Potion"
                    Case "Great Health Potion"
                        HealPotionName.Text = "Great Health Potion"
                End Select
                HealRuneHP.Value = Kernel.PotionHPRequired
                If Kernel.PotionHPRequired <= MaxHitPoints Then
                    HealPotionPercent.Value = CInt((Kernel.PotionHPRequired / MaxHitPoints) * 100)
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
            TradeChannelAdvertiserTrigger.Checked = Kernel.AdvertiseTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If TradeChannelAdvertiserTrigger.Checked Then
                TradeChannelAdvertiserAdvertisement.Text = Kernel.AdvertiseMsg
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
            FpsChangerTrigger.Checked = Kernel.FPSChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If FpsChangerTrigger.Checked Then
                FpsActive.Value = Kernel.FrameRateActive
                FpsInactive.Value = Kernel.FrameRateInactive
                FpsMinimized.Value = Kernel.FrameRateMinimized
                FPSHidden.Value = Kernel.FrameRateHidden
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
            StatsUploaderTrigger.Checked = Kernel.StatsUploaderTimerObj.State = IThreadTimer.ThreadTimerState.Running
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
            TradeChannelWatcherTrigger.Checked = Kernel.TradeWatcherActive = True
            If TradeChannelWatcherTrigger.Checked Then
                TradeChannelWatcherExpression.Text = Kernel.TradeWatcherRegex
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
            CavebotTrigger.Checked = Kernel.CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running
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
            AutoFisherTrigger.Checked = Kernel.FisherTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If AutoFisherTrigger.Checked Then
                AutoFisherMinimumCapacity.Value = Kernel.FisherMinimumCapacity
                If Kernel.FisherTurbo Then
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
            ComboBotTrigger.Checked = Kernel.ComboBotEnabled = True
            If ComboBotTrigger.Checked Then
                ComboLeader.Text = Kernel.ComboBotLeader
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
            LightEffectsTrigger.Checked = Kernel.LightTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If LightEffectsTrigger.Checked Then
                Select Case Kernel.LightI
                    Case ITibia.LightIntensity.Huge
                        LightEffect.SelectedItem = "Ultimate Torch"
                    Case ITibia.LightIntensity.Large
                        If Kernel.LightC = ITibia.LightColor.UtevoLux Then
                            LightEffect.SelectedItem = "Utevo Gran Lux"
                        Else
                            LightEffect.SelectedItem = "Light Wand"
                        End If
                    Case ITibia.LightIntensity.Medium
                        If Kernel.LightC = ITibia.LightColor.Torch Then
                            LightEffect.SelectedItem = "Torch"
                        Else
                            LightEffect.SelectedItem = "Utevo Lux"
                        End If
                    Case ITibia.LightIntensity.VeryLarge
                        If Kernel.LightC = ITibia.LightColor.Torch Then
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
            SpellCasterTrigger.Checked = Kernel.SpellTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If SpellCasterTrigger.Checked Then
                SpellCasterSpell.Text = Kernel.SpellMsg
                SpellCasterMinimumManaPoints.Value = Kernel.SpellManaRequired
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
            AutoLooterTrigger.Checked = Kernel.LooterTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If AutoLooterTrigger.Checked Then
                AutoLooterMinCap.Value = Kernel.LooterMinimumCapacity
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
            AmmunitionRestackerTrigger.Checked = Kernel.AmmoRestackerTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If AmmunitionRestackerTrigger.Checked Then
                AmmunitionRestackerMinAmmo.Value = Kernel.AmmoRestackerMinimumItemCount
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
            AutoStackerTrigger.Checked = Kernel.StackerTimerObj.State = IThreadTimer.ThreadTimerState.Running
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
            RunemakerTrigger.Checked = Kernel.RunemakerTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If RunemakerTrigger.Checked Then
                RunemakerSpell.Text = Kernel.RunemakerSpell.Name
                RunemakerMinimumManaPoints.Value = Kernel.RunemakerManaPoints
                RunemakerMinimumSoulPoints.Value = Kernel.RunemakerSoulPoints
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
            AutoEaterTrigger.Checked = Kernel.EaterTimerObj.State = IThreadTimer.ThreadTimerState.Running
            If AutoEaterTrigger.Checked Then
                If Kernel.AutoEaterSmart > 0 Then
                    AutoEaterSmart.Checked = True
                Else
                    AutoEaterSmart.Checked = False
                End If
                If AutoEaterSmart.Checked Then
                    AutoEaterMinimumHitPoints.Value = Kernel.AutoEaterSmart
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
                AutoEaterInterval.Value = CInt(Kernel.EaterTimerObj.Interval)
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
                    Kernel.ExecutablePath = Strings.Left(Application.ExecutablePath, I)
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
            Kernel.Client = New Tibia(strFilename, strDirectory)
            Kernel.Client.Start()
            If Not File.Exists(Application.StartupPath & "\TibiaTekBot Injected DLL.dll") Then
                Throw New Exception("Unable to locate """ & Application.StartupPath & "\TibiaTekBot Injected DLL.dll"". Please re-install the application.")
            End If
            If Not Kernel.Client.InjectDLL(Application.StartupPath & "\TibiaTekBot Injected DLL.dll") Then MessageBox.Show("Couldn't Inject DLL")
            Kernel.Proxy = New PProxy2(Kernel.Client)

            System.Threading.Thread.Sleep(1000)
            Kernel.WindowTimerObj.StartTimer()
            Dim TempInt As Integer = 0
            Do
                Kernel.Client.ReadMemory(Consts.ptrServerAddressBegin, TempInt, 1)
            Loop Until TempInt <> 0
            For I As Integer = 0 To Consts.ServerAddressCount - 1
                Kernel.Client.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "127.0.0.1")
                Kernel.Client.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Kernel.Proxy.sckLListen.LocalPort, 2)
            Next
            Me.NotifyIcon.Visible = True
            CType(Kernel.Client.Objects, Objects).Initialize()
            Kernel.Proxy.LoginPort = Kernel.LoginPort
            Kernel.TibiaClientStateTimerObj.StartTimer()
            Kernel.Client.ReadMemory(Consts.ptrScreenInfoBegin, Kernel.FrameRateBegin, 4)
            InjectLastAttackedId()
            If Kernel.IsOpenTibiaServer Then
                Kernel.Client.UnprotectMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length)
                Kernel.Client.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
            End If
            Kernel._NotifyIcon = Me.NotifyIcon
            Kernel.NotifyIcon.Text = "TibiaTek Bot v" & BotVersion & " - Not logged in"
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
            If Kernel.CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                CavebotMenuItem.Enabled = False
            Else
                CavebotMenuItem.Enabled = True
            End If
            If Kernel.TibiaClientIsVisible Then
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
                    If Not Kernel.Proxy Is Nothing Then
                        If Not Kernel.Client Is Nothing Then
                            Kernel.Client.Close()
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
            If Not Kernel.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            Kernel.AlarmsForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ShowHideTibiaWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHideTibiaWindow.Click
        Try
            If Not Kernel.Proxy Is Nothing Then
                If Not Kernel.Client Is Nothing Then
                    If Kernel.Client.WindowHandle = 0 Then Exit Sub
                    If Kernel.TibiaClientIsVisible Then
                        Kernel.Client.Hide()
                        'Win32API.ShowWindow(Core.Client.GetWindowHandle, Win32API.ShowState.SW_HIDE)
                    Else
                        Kernel.Client.Show()
                        'Win32API.ShowWindow(Core.Client.GetWindowHandle, Win32API.ShowState.SW_SHOW)
                    End If
                    Kernel.TibiaClientIsVisible = Not Kernel.TibiaClientIsVisible
                End If
            End If
        Catch
        Finally
        End Try
    End Sub

    Private Sub ConstantsEditorMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConstantsEditorMenuItem.Click
        Try
            Kernel.ConstantsEditorForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub MCPatcher()
        Try
            System.IO.File.Copy(Kernel.Client.Directory & "\" & Kernel.Client.Filename, Kernel.Client.Directory & "\_Tibia.exe.tmp")
            Dim FSR As New FileStream(Kernel.Client.Directory & "\_Tibia.exe.tmp", FileMode.Open, FileAccess.Read)
            Dim FSW As New FileStream(Kernel.Client.Directory & "\TibiaMC.exe", FileMode.OpenOrCreate, FileAccess.Write)
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
            MessageBox.Show("The new Tibia Client with Multi-Client is now saved at: " & Kernel.Client.Directory & "\" & "TibiaMC.exe")

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
            If System.IO.File.Exists(Kernel.Client.Directory & "\_Tibia.exe.tmp") Then
                System.IO.File.Delete(Kernel.Client.Directory & "\_Tibia.exe.tmp")
            End If
        End Try
    End Sub

    Private Sub MCPatchMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCPatchMenuItem.Click, PatchToolStripMenuItem.Click
        MCPatcher()
    End Sub

    Private Sub CavebotMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CavebotMenuItem.Click
        Try
            If Not Kernel.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            If Kernel.CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                MessageBox.Show("Cavebot is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Kernel.CavebotForm.Show()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CharacterStatisticsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CharacterStatisticsMenuItem.Click
        Try
            If Not Kernel.Client.IsConnected Then
                Beep()
                Exit Sub
            End If
            Kernel.CharacterStatisticsForm.Show()
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
            If Not Kernel.Client.IsConnected Then
                LoginSelectForm = New frmLoginSelectDialog()
                If LoginSelectForm.ShowDialog() <> Forms.DialogResult.OK Then Exit Sub
                For I As Integer = 0 To 3
                    Kernel.Client.WriteMemory(Consts.ptrServerAddressBegin + (Consts.ServerAddressDist * I), "localhost")
                    Kernel.Client.WriteMemory(Consts.ptrServerPortBegin + (Consts.ServerAddressDist * I), Kernel.Proxy.sckLListen.LocalPort, 2)
                Next
                If Kernel.IsOpenTibiaServer Then
                    Kernel.Client.UnprotectMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia.Length)
                    Kernel.Client.WriteMemory(Consts.ptrRSAKey, Consts.RSAKeyOpenTibia)
                End If
            Else
                MessageBox.Show("You must be logged out to change the login server.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Tool Menu "

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
            Try
                If Not Kernel.Proxy Is Nothing Then
                    If Not Kernel.Client Is Nothing Then
                        Kernel.Client.Close()
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
                If Kernel.SpellTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
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
                Kernel.SpellMsg = SpellCasterSpell.Text
                Kernel.SpellManaRequired = SpellCasterMinimumManaPoints.Value
                Kernel.SpellTimerObj.StartTimer()
            Else
                Kernel.SpellTimerObj.StopTimer()
                Kernel.SpellMsg = String.Empty
                Kernel.SpellManaRequired = 0
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
                If Kernel.RunemakerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
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
                For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                    If Spell.Name.Equals(RunemakerSpell.Text) Then
                        Kernel.RunemakerSpell = Spell
                        Found = True
                        Exit For
                    End If
                Next
                If Not Found Then
                    RunemakerTrigger.Checked = False
                    MessageBox.Show("The runemaker spell was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Kernel.RunemakerManaPoints = RunemakerMinimumManaPoints.Value
                Kernel.RunemakerSoulPoints = RunemakerMinimumSoulPoints.Value
                Kernel.RunemakerTimerObj.StartTimer()
            Else
                Kernel.RunemakerTimerObj.StopTimer()
                Kernel.RunemakerManaPoints = 0
                Kernel.RunemakerSoulPoints = 0
                Kernel.RunemakerSpell = Nothing
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
                If Kernel.EaterTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                If AutoEaterSmart.Checked Then
                    If AutoEaterMinimumHitPoints.Value = 0 Then
                        AutoEaterTrigger.Checked = False
                        MessageBox.Show("The minimum hit points when the Auto Smart Eater feature is on must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Kernel.AutoEaterSmart = AutoEaterMinimumHitPoints.Value
                Else
                    Kernel.AutoEaterSmart = 0
                End If
                If AutoEaterInterval.Value = 0 Then
                    AutoEaterTrigger.Checked = False
                    MessageBox.Show("The auto eater delay must not be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Kernel.EaterTimerObj.Interval = AutoEaterInterval.Value
                Kernel.EaterTimerObj.StartTimer()
            Else
                Kernel.AutoEaterSmart = 0
                Kernel.EaterTimerObj.StopTimer()
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
            Reader = IO.File.OpenText(Kernel.GetProfileDirectory() & "\config.txt")
            Data = Reader.ReadToEnd
            Dim MCollection As MatchCollection
            Dim GroupMatch As Match
            MCollection = [Regex].Matches(Data, "&([^\n;]+)[;]?")
            For Each GroupMatch In MCollection
                'Kernel.CommandParser.
                Kernel.CommandParser.Invoke(GroupMatch.Groups(1).Value)
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
            IO.File.Delete(Kernel.GetProfileDirectory() & "\config.txt")
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
                Kernel.LooterMinimumCapacity = AutoLooterMinCap.Value
                Kernel.LooterTimerObj.StartTimer()
            Else
                Kernel.LooterTimerObj.StopTimer()
                Kernel.LooterMinimumCapacity = 0
            End If
            RefreshAutoLooterControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AutoLooterEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoLooterConfigure.Click
        'Kernel.LootItems.ShowLootCategories()
        'Kernel.Client.BringToFront()
        Kernel.EditLootForm.Show()
    End Sub

    Private Sub AutoStackerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoStackerTrigger.CheckedChanged
        Try
            If AutoStackerTrigger.Checked Then
                Kernel.StackerTimerObj.Interval = Consts.AutoStackerDelay
                Kernel.StackerTimerObj.StartTimer()
            Else
                Kernel.StackerTimerObj.StopTimer()
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
                If Kernel.LightTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Select Case LightEffect.Text.ToLower
                    Case "on"
                        Kernel.LightC = ITibia.LightColor.BrightSword
                        Kernel.LightI = ITibia.LightIntensity.Huge + 2
                    Case "torch"
                        Kernel.LightI = ITibia.LightIntensity.Medium
                        Kernel.LightC = ITibia.LightColor.Torch
                    Case "great torch"
                        Kernel.LightI = ITibia.LightIntensity.VeryLarge
                        Kernel.LightC = ITibia.LightColor.Torch
                    Case "ultimate torch"
                        Kernel.LightI = ITibia.LightIntensity.Huge
                        Kernel.LightC = ITibia.LightColor.Torch
                    Case "utevo lux"
                        Kernel.LightI = ITibia.LightIntensity.Medium
                        Kernel.LightC = ITibia.LightColor.UtevoLux
                    Case "utevo gran lux"
                        Kernel.LightI = ITibia.LightIntensity.Large
                        Kernel.LightC = ITibia.LightColor.UtevoLux
                    Case "utevo vis lux"
                        Kernel.LightI = ITibia.LightIntensity.VeryLarge
                        Kernel.LightC = ITibia.LightColor.UtevoLux
                    Case "light wand"
                        Kernel.LightI = ITibia.LightIntensity.Large
                        Kernel.LightC = ITibia.LightColor.LightWand
                    Case Else
                        MessageBox.Show("You must select a Light Effect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        LightEffectsTrigger.Checked = False
                        Exit Sub
                End Select
                Kernel.LightTimerObj.StartTimer()
            Else
                Kernel.SetLight(ITibia.LightIntensity.Small, ITibia.LightColor.UtevoLux)
                Kernel.LightTimerObj.StopTimer()
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
                If Kernel.AmmoRestackerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Dim ItemID As Integer
                Dim ItemCount As Integer
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), ItemID, 2)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, ItemCount, 1)
                If ItemID = 0 OrElse Not Kernel.Client.Objects.HasFlags(ItemID, IObjects.ObjectFlags.IsStackable) Then
                    MessageBox.Show("You must place some of your ammunition on the Belt/Arrow Slot first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Kernel.AmmoRestackerItemID = ItemID
                Kernel.AmmoRestackerOutOfAmmo = False
                Kernel.AmmoRestackerMinimumItemCount = AmmunitionRestackerMinAmmo.Value
                Kernel.AmmoRestackerTimerObj.StartTimer()
            Else
                Kernel.AmmoRestackerItemID = 0
                Kernel.AmmoRestackerTimerObj.StopTimer()
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
                If Kernel.ComboBotEnabled = True Then Exit Sub
                'Kernel.ComboBotLeader = ComboLeader.Text
                Dim i As Integer
                Kernel.Combobotleaders.Clear()
                For i = 0 To ComboLeaders.Items.Count - 1
                    Kernel.Combobotleaders.Add(ComboLeaders.Items(i).ToString.ToLower)
                Next

                Kernel.ComboBotEnabled = True
            Else
                Kernel.ComboBotEnabled = False
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
                If Kernel.FisherTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                If AutoFisherMinimumCapacity.Value = vbNull Then
                    MessageBox.Show("Please give the minimium capacity for fisher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    AutoFisherTrigger.Checked = False
                    Exit Sub
                End If
                If AutoFisherTurbo.Checked Then
                    Kernel.FisherMinimumCapacity = AutoFisherMinimumCapacity.Value
                    Kernel.FisherTurbo = True
                    Kernel.FisherSpeed = 500
                    Kernel.FisherTimerObj.StartTimer()
                Else
                    Kernel.FisherMinimumCapacity = AutoFisherMinimumCapacity.Value
                    Kernel.FisherSpeed = 0
                    Kernel.FisherTurbo = False
                    Kernel.FisherTimerObj.StartTimer()
                End If
            Else
                Kernel.FisherMinimumCapacity = 0
                Kernel.FisherSpeed = 0
                Kernel.FisherTurbo = False
                Kernel.FisherTimerObj.StopTimer()
            End If
            RefreshAutoFisherControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CavebotTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CavebotTrigger.CheckedChanged
        Try
            Dim SP As New ServerPacketBuilder(Kernel.Proxy)
            If CavebotTrigger.Checked Then
                Kernel.WaypointIndex = SelectNearestWaypoint(Kernel.Walker_Waypoints)
                If Kernel.WaypointIndex = -1 Then
                    MessageBox.Show("No waypoints found or they are not in current floor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    CavebotTrigger.Checked = False
                    Exit Sub
                End If
                If Consts.LootWithCavebot Then
                    Kernel.LooterMinimumCapacity = Consts.CavebotLootMinCap
                    Kernel.LooterTimerObj.StartTimer()
                End If
                Kernel.AutoAttackerTimerObj.StartTimer()
                Kernel.CaveBotTimerObj.StartTimer()
                Kernel.AutoEaterSmart = 0
                Kernel.EaterTimerObj.Interval = 20000
                Kernel.EaterTimerObj.StartTimer()
                Kernel.IsOpeningReady = True
                Kernel.CBCreatureDied = False
                Kernel.WaypointIndex = 0
                Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)

                SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                Kernel.CBState = CavebotState.Walking
            Else
                Kernel.LooterTimerObj.StopTimer()
                Kernel.AutoAttackerTimerObj.StopTimer()
                Kernel.CaveBotTimerObj.StopTimer()
                Kernel.EaterTimerObj.StopTimer()
                Kernel.EaterTimerObj.Interval = 0
                Kernel.WaypointIndex = 0
                Kernel.IsOpeningReady = True
                SP.StopEverything()
                'Core.Proxy.SendPacketToServer(PacketUtils.AttackEntity(0))
                Kernel.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
            End If
            RefreshCavebotControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub CavebotConfigure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CavebotConfigure.Click
        Kernel.CavebotForm.Show()
    End Sub

    Private Sub TradeChannelWatcherTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TradeChannelWatcherTrigger.CheckedChanged
        Try
            If TradeChannelWatcherTrigger.Checked Then
                If Kernel.TradeWatcherActive = True Then Exit Sub
                If String.IsNullOrEmpty(TradeChannelWatcherExpression.Text) Then
                    MessageBox.Show("Please give Expression!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Dim RegExp As Regex
                Try
                    RegExp = New Regex(TradeChannelWatcherExpression.Text)
                    Kernel.TradeWatcherRegex = TradeChannelWatcherExpression.Text
                    Kernel.TradeWatcherActive = True
                    Kernel.ConsoleWrite("Make sure you have the Trade-Channel opened.")
                Catch exep As Exception
                    MessageBox.Show("Sorry, but this is not a valid regular expression." & ControlChars.NewLine & _
                    "See http://en.wikipedia.org/wiki/Regular_expression for more information on regular expressions.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                Kernel.TradeWatcherActive = False
                Kernel.TradeWatcherRegex = ""
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
                If Kernel.StatsUploaderTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                If StatsUploaderSaveToDisk.Checked Then
                    If StatsUploaderPath.Text.Length = 0 OrElse StatsUploaderFilename.Text.Length = 0 Then
                        MessageBox.Show("Please don't leave empty values to text boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshStatsUploaderControls()
                        Exit Sub
                    End If
                    Kernel.UploaderUrl = StatsUploaderUrl.Text
                    Kernel.UploaderFilename = StatsUploaderFilename.Text
                    Kernel.UploaderPath = StatsUploaderPath.Text
                    Kernel.UploaderUserId = StatsUploaderUser.Text
                    Kernel.UploaderPassword = StatsUploaderPassword.Text
                    Kernel.UploaderSaveToDiskOnly = StatsUploaderSaveToDisk.Checked
                    Kernel.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                    Kernel.StatsUploaderTimerObj.StartTimer()
                Else
                    If StatsUploaderUrl.Text.Length = 0 _
                     OrElse StatsUploaderUser.Text.Length = 0 _
                     OrElse StatsUploaderPassword.Text.Length = 0 _
                     OrElse Consts.StatsUploaderFrequency = 0 Then
                        MessageBox.Show("Don't leave empty values on text boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshStatsUploaderControls()
                        Exit Sub
                    End If
                    Kernel.UploaderUrl = StatsUploaderUrl.Text
                    Kernel.UploaderFilename = StatsUploaderFilename.Text
                    Kernel.UploaderPath = StatsUploaderPath.Text
                    Kernel.UploaderUserId = StatsUploaderUser.Text
                    Kernel.UploaderPassword = StatsUploaderPassword.Text
                    Kernel.UploaderSaveToDiskOnly = StatsUploaderSaveToDisk.Checked
                    Kernel.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                    Kernel.StatsUploaderTimerObj.StartTimer()
                End If
            Else
                Kernel.StatsUploaderTimerObj.StopTimer()
            End If
            RefreshStatsUploaderControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not Kernel.IRCClient Is Nothing Then
            If Kernel.IRCClient.IsConnected Then
                Kernel.IRCClient.Quit()
            End If
            If Not Kernel.IRCClient.MainThread Is Nothing Then
                Kernel.IRCClient.MainThread.Abort()
            End If
            If Not Kernel.Client Is Nothing Then
                Kernel.Client.Close()
            End If
        End If
        NotifyIcon.Visible = False
    End Sub

    Private Sub FpsChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FpsChangerTrigger.CheckedChanged
        Try
            If FpsChangerTrigger.Checked Then
                If Kernel.FPSChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Kernel.FrameRateActive = FpsActive.Value
                Kernel.FrameRateInactive = FpsInactive.Value
                Kernel.FrameRateMinimized = FpsMinimized.Value
                Kernel.FrameRateHidden = FPSHidden.Value
                Kernel.FPSChangerTimerObj.StartTimer()
            Else
                Kernel.FPSChangerTimerObj.StopTimer()
                Kernel.Client.SetFramesPerSecond(Kernel.FrameRateActive)
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
                If Kernel.AdvertiseTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(TradeChannelAdvertiserAdvertisement.Text) Then Exit Sub
                'OpenChannel("Trade", ChannelType.Trade)
                Kernel.AdvertiseMsg = TradeChannelAdvertiserAdvertisement.Text
                Kernel.AdvertiseTimerObj.StartTimer(1000)
            Else
                Kernel.AdvertiseMsg = ""
                Kernel.AdvertiseTimerObj.StopTimer()
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
            Kernel.Spells.LoadSpells()
            MessageBox.Show("Done loading the Spells configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadItemsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadItemsButton.Click
        Try
            CType(Kernel.Client.Objects, Objects).Refresh()
            MessageBox.Show("Done loading the Objects configuration file.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MiscReloadOutfitsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscReloadOutfitsButton.Click
        Try

            Kernel.Outfits.LoadOutfits()
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
                Kernel.LastExperience = 0
                If Kernel.ExpCheckerActivated Then
                    Kernel.ExpCheckerActivated = False
                End If
                Kernel.FakingTitle = True
                Kernel.Client.Title = FakeTitle.Text
            Else
                Kernel.FakingTitle = False
                Kernel.Client.Title = BotName & " - " & Kernel.Client.CharacterName
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
                Dim OD As New IOutfits.OutfitDefinition
                Dim ODFound As Boolean = Kernel.Outfits.GetOutfitByID(BL.OutfitID, OD)
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
            Dim OD As New IOutfits.OutfitDefinition
            Dim ODFound As Boolean = Kernel.Outfits.GetOutfitByName(ChameleonOutfit.Text, OD)
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
        If Kernel.Client Is Nothing OrElse Not Kernel.Client.IsConnected Then Exit Sub
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
                    If Kernel.FakingTitle Then
                        Kernel.FakingTitle = False
                        MessageBox.Show("Fake Title is now Disabled.")
                    End If
                    Kernel.LastExperience = 0
                    Kernel.ExpCheckerActivated = True
                End If
                If ExpShowCreatures.Checked Then
                    Kernel.ShowCreaturesUntilNextLevel = True
                End If
            Else
                Kernel.ShowCreaturesUntilNextLevel = False
                Kernel.ExpCheckerActivated = False
                Kernel.LastExperience = 0
                Kernel.Client.Title = BotName & " - " & Kernel.Client.CharacterName
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
                If BL.IsMyself OrElse BL.GetFloor <> Kernel.CharacterLoc.Z + Floor Then Continue Do
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
                Kernel.Client.WriteMemory(Consts.ptrNameSpy, &H9090, 2)
                Kernel.Client.WriteMemory(Consts.ptrNameSpy2, &H9090, 2)
                Kernel.NameSpyActivated = True
            Else
                Kernel.Client.WriteMemory(Consts.ptrNameSpy, Consts.NameSpyDefault, 2)
                Kernel.Client.WriteMemory(Consts.ptrNameSpy2, Consts.NameSpy2Default, 2)
                Kernel.NameSpyActivated = False
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
                    Kernel.OpenCommand = WebsiteName.Text
                    If Not String.IsNullOrEmpty(SearchFor.Text) Then MessageBox.Show("Note: Search criteria works only with pre-defined urls.")
                    If Not Kernel.BGWOpenCommand.IsBusy Then
                        Kernel.BGWOpenCommand.RunWorkerAsync()
                        Exit Sub
                    Else
                        MessageBox.Show("Busy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
            End Select
            If WebsiteName.Text.ToLower = "tibia wiki" Then
                Kernel.OpenCommand = Prepend & SearchFor.Text.Replace(" ", "_")
            Else
                Kernel.OpenCommand = Prepend & SearchFor.Text
            End If
            If Not Kernel.BGWOpenCommand.IsBusy Then
                Kernel.BGWOpenCommand.RunWorkerAsync()
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
            If Not Kernel.BGWSendLocation.IsBusy Then
                Kernel.SendLocationDestinatary = SendLocationTo.Text
                Kernel.BGWSendLocation.RunWorkerAsync()
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
            If Kernel.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                MessageBox.Show("This entity is already in your list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Kernel.AutoTrainerEntities.Add(BL.GetEntityID)
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
            If Kernel.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                Kernel.AutoTrainerEntities.Remove(BL.GetEntityID)
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
            Kernel.AutoTrainerEntities.Clear()
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
                If Kernel.AutoTrainerEntities.Count = 0 Then
                    MessageBox.Show("You have to add entities to the training list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                Kernel.AutoTrainerMinHPPercent = MinPercentageHP.Value
                Kernel.AutoTrainerMaxHPPercent = MaxPercentageHP.Value
                Kernel.AutoTrainerTimerObj.StartTimer()
            Else
                Kernel.AutoTrainerMinHPPercent = 0
                Kernel.AutoTrainerMaxHPPercent = 0
                Kernel.AutoTrainerTimerObj.StopTimer()
            End If
            RefreshTrainerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AutoAttackerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoAttackerTrigger.CheckedChanged
        Try
            Dim SP As New ServerPacketBuilder(Kernel.Proxy)
            If AutoAttackerTrigger.Checked Then

                Select Case AttackerFightingMode.Text.ToLower
                    Case "offensive"
                        Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Offensive, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Offensive)
                    Case "balanced"
                        Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Balanced, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Balanced)
                        'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Balanced))
                    Case "defensive"
                        Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Defensive, 1)
                        SP.ChangeFightingMode(ITibia.FightingMode.Defensive)
                        'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Defensive))
                End Select
                Select Case AttackChasingMode.Text.ToLower
                    Case "chase"
                        Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                    Case "stand"
                        Kernel.Client.WriteMemory(Consts.ptrChasingMode, 0, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Standing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Standing))
                End Select
                If AttackAutomatically.Checked Then
                    Kernel.AutoAttackerTimerObj.StartTimer()
                End If
                Kernel.AutoAttackerActivated = True
            Else
                Kernel.AutoAttackerActivated = False
                Kernel.AutoAttackerIgnoredID = 0
                Kernel.AutoAttackerTimerObj.StopTimer()
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
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                If RightHandItemID = 0 OrElse Not Kernel.Client.Objects.IsKind(RightHandItemID, IObjects.ObjectKind.RangedWeapon) Then
                    MessageBox.Show("You must have a throwable item in your right hand, like a spear, throwing knife, etc.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                With Kernel
                    .PickUpItemID = CUShort(RightHandItemID)
                    .PickUpTimerObj.Interval = Consts.AutoPickUpDelay
                    .PickUpTimerObj.StartTimer()
                End With
            Else
                With Kernel
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
                If Kernel.AntiLogoutObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Kernel.LastActivity = Date.Now
                Kernel.AntiLogoutObj.Interval = Consts.AntiLogoutInterval
                Kernel.AntiLogoutObj.StartTimer()
            Else
                Kernel.AntiLogoutObj.StopTimer()
            End If
            RefreshAntiIdlerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RingChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RingChangerTrigger.CheckedChanged
        Try
            If RingChangerTrigger.Checked Then
                Kernel.RingID = Kernel.Client.Objects.ID(ChangerRingType.Text)
                If Kernel.RingID = 0 Then 'AndAlso Core.Client.Items.IsRing(Core.RingID) Then <-- WTF O.o
                    MessageBox.Show("Invalid Ring Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshChangerControls()
                    Exit Sub
                End If
                Kernel.RingChangerTimerObj.StartTimer()
            Else
                Kernel.RingChangerTimerObj.StopTimer()
                Kernel.RingID = 0
            End If
            RefreshChangerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AmuletChangerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmuletChangerTrigger.CheckedChanged
        Try
            If AmuletChangerTrigger.Checked Then
                Kernel.AmuletID = Kernel.Client.Objects.ID(ChangerAmuletType.Text)
                If Kernel.AmuletID = 0 Then ' AndAlso Core.Client.Items.IsNeck(Core.AmuletID) Then <-- WTF O.o
                    MessageBox.Show("Invalid Amulet/Necklace Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshChangerControls()
                    Exit Sub
                End If
                Kernel.AmuletChangerTimerObj.StartTimer()
            Else
                Kernel.AmuletChangerTimerObj.StopTimer()
                Kernel.AmuletID = 0
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
                If Kernel.PotionTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealPotionUseHp.Checked Then
                    Kernel.PotionHPRequired = HealPotionHp.Value
                Else
                    Kernel.PotionHPRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case HealPotionName.Text
                    Case "Health Potion"
                        Kernel.PotionID = Kernel.Client.Objects.ID("Health Potion")
                    Case "Strong Health Potion"
                        Kernel.PotionID = Kernel.Client.Objects.ID("Strong Health Potion")
                    Case "Great Health Potion"
                        Kernel.PotionID = Kernel.Client.Objects.ID("Great Health Potion")
                    Case Else
                        MessageBox.Show("You must select the Potion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Kernel.PotionID = 0 Then
                    MessageBox.Show("Unknown error occured selecting Potion Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Kernel.PotionTimerObj.StartTimer()
                End If
            Else
                Kernel.PotionTimerObj.StopTimer()
                Kernel.PotionHPRequired = 0
                Kernel.PotionID = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealWithRune_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealWithRune.CheckedChanged
        Try
            If HealWithRune.Checked Then
                If Kernel.UHTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealRuneUseHp.Checked Then
                    Kernel.UHHPRequired = HealRuneHP.Value
                Else
                    Kernel.UHHPRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case HealRuneType.Text
                    Case "UH Rune"
                        Kernel.UHId = Kernel.Client.Objects.ID("Ultimate Healing")
                    Case "IH Rune"
                        Kernel.UHId = Kernel.Client.Objects.ID("Intense Healing")
                    Case Else
                        MessageBox.Show("You must select the Rune.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Kernel.UHId = 0 Then
                    MessageBox.Show("Unknown error occured selecting Rune Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Kernel.UHTimerObj.StartTimer()
                End If
            Else
                Kernel.UHTimerObj.StopTimer()
                Kernel.UHHPRequired = 0
                Kernel.UHId = 0
            End If
            RefreshHealerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealWithSpell_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealWithSpell.CheckedChanged
        Try
            If HealWithSpell.Checked Then
                If Kernel.HealTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If HealSpellUseHP.Checked Then
                    Kernel.HealMinimumHP = HealSpellHp.Value
                Else
                    Kernel.HealMinimumHP = MaxHitPoints * (HealSpellPercent.Value / 100)
                End If
                For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                    If Spell.Name.Equals(HealSpellName.Text, StringComparison.CurrentCultureIgnoreCase) OrElse Spell.Words.Equals(HealSpellName.Text, StringComparison.CurrentCultureIgnoreCase) Then
                        Select Case Spell.Name.ToLower
                            Case "light healing", "heal friend", "mass healing", "intense healing", "ultimate healing", "divine healing", "wound cleansing"
                                Kernel.HealSpell = Spell
                                Exit For
                            Case Else
                                MessageBox.Show("Please select a healing spell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                RefreshHealerControls()
                                Exit Sub
                        End Select
                    End If
                Next
                Kernel.HealComment = ""
                Kernel.HealTimerObj.StartTimer()
            Else
                Kernel.HealTimerObj.StopTimer()
                Kernel.HealMinimumHP = 0
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
                If Kernel.ManaPotionTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    RefreshHealerControls()
                    Exit Sub
                End If
                Dim MaxHitPoints As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                If RestoreManaWith.Checked Then
                    Kernel.DrinkerManaRequired = DrinkerManaPoints.Value
                Else
                    Kernel.DrinkerManaRequired = MaxHitPoints * (HealRunePercent.Value / 100)
                End If
                Select Case ManaPotionName.Text
                    Case "Mana Potion"
                        Kernel.ManaPotionID = Kernel.Client.Objects.ID("Mana Potion")
                    Case "Strong Mana Potion"
                        Kernel.ManaPotionID = Kernel.Client.Objects.ID("Strong Mana Potion")
                    Case "Great Mana Potion"
                        Kernel.ManaPotionID = Kernel.Client.Objects.ID("Great Mana Potion")
                    Case Else
                        MessageBox.Show("You must select the Mana Potion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        RefreshHealerControls()
                        Exit Sub
                End Select
                If Kernel.ManaPotionID = 0 Then
                    MessageBox.Show("Unknown error occured selecting Potion Type. Please notify the Development Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealerControls()
                    Exit Sub
                Else
                    Kernel.ManaPotionTimerObj.StartTimer()
                End If
            Else
                Kernel.ManaPotionTimerObj.StopTimer()
                Kernel.DrinkerManaRequired = 0
                Kernel.ManaPotionID = 0
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
                If Kernel.DancerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Select Case DancerSpeed.Text
                    Case "Slow"
                        Kernel.DancerTimerObj.Interval = 500
                    Case "Fast"
                        Kernel.DancerTimerObj.Interval = 100
                    Case "Turbo"
                        Kernel.DancerTimerObj.Interval = 10
                End Select
                Kernel.DancerTimerObj.StartTimer()
            Else
                Kernel.DancerTimerObj.StopTimer()
            End If
            RefreshDancerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AmmoMakerTrigger_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmoMakerTrigger.CheckedChanged
        Try
            If AmmoMakerTrigger.Checked Then
                If Kernel.AmmoMakerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub

                Dim Found As Boolean = False
                Dim S As New ISpells.SpellDefinition
                For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                    If (Spell.Name.Equals(AmmoMakerSpell.Text, StringComparison.CurrentCultureIgnoreCase) _
                    OrElse Spell.Words.Equals(AmmoMakerSpell.Text.ToString, StringComparison.CurrentCultureIgnoreCase)) _
                    AndAlso (Spell.Kind = ISpells.SpellKind.Ammunition Or Spell.Kind = ISpells.SpellKind.Incantation) Then
                        S = Spell
                        Found = True
                        Exit For
                    End If
                Next
                If Found Then
                    Kernel.AmmoMakerSpell = S
                    Kernel.AmmoMakerMinMana = AmmoMakerMinMana.Value
                    Kernel.AmmoMakerMinCap = AmmoMakerMinCap.Value
                    Kernel.AmmoMakerTimerObj.StartTimer()
                Else
                    MessageBox.Show("You cant make ammunitions with the selected spell. Please choose another spell.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                Kernel.AmmoMakerMinMana = 0
                Kernel.AmmoMakerMinCap = 0
                Kernel.AmmoMakerTimerObj.StopTimer()
            End If
            RefreshAmmoMakerControls()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HealFriendTrigger_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealFriendTrigger.CheckedChanged
        Try
            If HealFriendTrigger.Checked Then
                If Kernel.HealFriendTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                If String.IsNullOrEmpty(HealFName.Text) Then
                    MessageBox.Show("You must enter the friend's name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    RefreshHealFriendControls()
                    Exit Sub
                End If
                Kernel.HealFriendHealthPercentage = HealFHp.Value
                Select Case HealFType.Text
                    Case "Ultimate Healing Rune"
                        Kernel.HealFriendHealType = HealTypes.UltimateHealingRune
                    Case "Exura Sio - Spell"
                        Kernel.HealFriendHealType = HealTypes.ExuraSio
                    Case "Both"
                        Kernel.HealFriendHealType = HealTypes.Both
                    Case Else
                        MessageBox.Show("You must select the type for healer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                Kernel.HealFriendCharacterName = HealFName.Text
                Kernel.HealFriendTimerObj.StartTimer()
            Else
                Kernel.HealFriendCharacterName = ""
                Kernel.HealFriendHealthPercentage = 0
                Kernel.HealFriendTimerObj.StopTimer()
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
                If Kernel.HealPartyTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                Kernel.HealPartyMinimumHPPercentage = HealPHp.Value
                Select Case HealPType.Text
                    Case "Ultimate Healing Rune"
                        Kernel.HealPartyHealType = HealTypes.UltimateHealingRune
                    Case "Exura Sio - Spell"
                        Kernel.HealPartyHealType = HealTypes.ExuraSio
                    Case "Both"
                        Kernel.HealPartyHealType = HealTypes.Both
                    Case Else
                        MessageBox.Show("You must select the type for healer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                Kernel.HealPartyTimerObj.StartTimer()
            Else
                Kernel.HealPartyMinimumHPPercentage = 0
                Kernel.HealPartyTimerObj.StopTimer()
            End If
            RefreshHealPartyControls()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region

    Private Sub ScriptsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptsToolStripMenuItem.Click
        Kernel.ScriptsForm.Show()
    End Sub

    Private Sub TestToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Kernel.Client.BringToFront()
    End Sub

    Private Sub KeyboardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyboardToolStripMenuItem.Click
        Kernel.KeyboardForm.ShowDialog()
    End Sub

    Private Sub TestToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Dim PPB As New PipePacketBuilder(Kernel.Client.Pipe)
        Dim BL As New BattleList
        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
        Dim Loc As New ITibia.LocationDefinition
        Loc.X = 0
        Loc.Y = 10
        Loc.Z = 0 'No need though
        PPB.DisplayTextAboveCreature(Kernel.CharacterID, Loc, &H55, &H55, &HFF, 1, "PWNS")
        'Kernel.Client.TestPipe()

        'Dim Img As System.Drawing.Image = Kernel.Client.Screenshot(True)
        'If Not Img Is Nothing Then
        '    Img.Save("c:\test.png", Drawing.Imaging.ImageFormat.Png)
        'End If
        ''Kernel.Client.Screenshot("c:\test.jpg")

        'Dim PPB As New PipePacketBuilder(Kernel.Client.Pipe)
        'PPB.Keyboard(True)
        'Dim KVKEs(0 To 0) As IKernel.KeyboardVKEntry
        'Dim KVKE As New IKernel.KeyboardVKEntry
        'KVKE.VirtualKeyOriginalCode = IKernel.VirtualKey.Q 
        'KVKE.VirtualKeyNewCode = IKernel.VirtualKey.Up
        'KVKEs(0) = KVKE
        'PPB.KeyboardPopulateVKEntries(KVKEs)

        'Dim Location As New ITibia.LocationDefinition
        'Location.X = 300
        'Location.Y = 300
        'Location.Z = 0 'Not used
        'Dim S As Integer = 0
        'For I As Integer = 1 To 30
        '    PPB.DisplayText(S, Location, 255, 0, 0, 2, "TTB ROX!")
        '    S += 1
        '    Location.Y += 10
        'Next
        'MessageBox.Show("DONE")
        'PPB.RemoveAllText()

        'Dim T As New System.Threading.Thread(AddressOf AutoLogin)
        ''T.Start()
    End Sub

    <STAThread()> Private Sub AutoLogin()
        With Kernel
            If Not .Client.IsConnected Then
                Dim Persist As Boolean = True

                While Persist
                    ' Log in first to gameworld once
                    '.Client.WriteMemory(Consts.ptrAutoLoginAccountNumeric, 111111, 4)
                    '.Client.WriteMemory(Consts.ptrAutoLoginAccountString, "111111")
                    '.Client.WriteMemory(Consts.ptrAutoLoginPassword, "platano")
                    .Client.WriteMemory(Consts.ptrAutoLoginPatch, Consts.AutoLoginPatch)

                    .Client.BringToFront()
                    .Client.Click(100, .Client.ScreenHeight - 220)
                    '220
                    While Not .Client.DialogIsOpened
                        System.Threading.Thread.Sleep(25) '25ms
                    End While

                    Dim P As System.Drawing.Point = .Client.DialogLocation
                    P.X += 150
                    P.Y += 155
                    .Client.Click(P)
                    While Not .Client.DialogCaption.Equals("Connecting")
                        System.Threading.Thread.Sleep(25) '25ms
                    End While
                    .Client.WriteMemory(Consts.ptrAutoLoginPatch, Consts.AutoLoginPatchOriginal)
                    'SendMessage(frm1.hwnd, WM_KEYDOWN, &H59, 0)
                    While .Client.DialogCaption.Equals("Connecting")
                        System.Threading.Thread.Sleep(25) '25ms
                    End While
                    While Not .Client.DialogCaption.Equals("Message of the Day")
                        System.Threading.Thread.Sleep(25) '25ms
                    End While
                    .Client.BringToFront()
                    '.Client.WriteMemory(Consts.ptrCharacterSelectionIndex, 2, 4)
                    .Computer.Keyboard.SendKeys("{ENTER}", True)
                    ' Message of the Day
                    ' Select Character

                    Persist = False
                End While
            End If
        End With
    End Sub

    Private Sub AddLeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLeader.Click
        Try
            Dim LeaderName As String
            LeaderName = InputBox("Enter the Leader name", "ComboBot - Add Leader")
            If LeaderName.Length > 0 Then
                If ComboLeaders.Items.Contains(LeaderName) Then
                    MsgBox("That Leader name already exists", MsgBoxStyle.Critical, "Combobot - Add leader")
                Else
                    ComboLeaders.Items.Add(LeaderName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RemoveLeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveLeader.Click
        Try
            If ComboLeaders.Items.Count > 0 Then
                ComboLeaders.Items.RemoveAt(ComboLeaders.SelectedIndex)
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NotifyIcon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon.Click
        Try
            If Not My.Computer.Keyboard.CtrlKeyDown Then Exit Sub
            If Me.Visible Then
                Me.Hide()
                IsVisible = False
            Else
                Me.Show()
                Me.Activate()
                IsVisible = True
            End If
        Catch ex As Exception
            'MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NotifyIcon_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon.DoubleClick
        Try
            If My.Computer.Keyboard.CtrlKeyDown Then Exit Sub
            If Kernel.Client.WindowState = ITibia.WindowStates.Hidden Then
                Kernel.Client.Show()
                Kernel.Client.Activate()
            Else
                Kernel.Client.Hide()
            End If
        Catch ex As Exception
            'MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Kernel.LagBarForm.Show()
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Kernel.Client Is Nothing Then Exit Sub
        If Kernel.Client.IsConnected Then
            Kernel.NotifyIcon.Text = "TibiaTek Bot v" & BotVersion & " - " & Kernel.Client.CharacterName
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub AutoResponderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoResponderToolStripMenuItem.Click
        If Not Kernel.AutoResponderForm Is Nothing Then
            Kernel.AutoResponderForm.Show()
        End If
    End Sub
#Region "Irc Location"
    Private Sub ILActivateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILActivateButton.Click
        Try
            If ILActivateButton.Text = "Deactivate" Then
                Kernel.ILBroadcasterTimerObj.StopTimer()
                Kernel.ILProcessTimerObj.StopTimer()
                Kernel.ILChannelName = ""
                Kernel.ILNumberofPlayers = 0
                Kernel.IRCClient.Part(ILChannelTextbox.Text, "Deactivating broadcaster, bye")
                ILChannelTextbox.ReadOnly = False
                frmILMapShowButton.Enabled = False
                ILActivateButton.Text = "Activate"
                frmILMap.ILMapUpdate.Stop()
                frmILMap.Close()
            Else
                If Kernel.ILBroadcasterTimerObj.State = IThreadTimer.ThreadTimerState.Running Then Exit Sub
                ILPlayer1Namebox.Text = Kernel.Client.CharacterName
                Dim nop As Integer = 0
                If ILPlayer1Namebox.Text <> "" Then
                    Kernel.ILPlayer1Name = ILPlayer1Namebox.Text
                    nop = nop + 1
                    If ILPlayer2Namebox.Text <> "" Then
                        Kernel.ILPlayer2Name = ILPlayer2Namebox.Text
                        nop = nop + 1
                        If ILPlayer3Namebox.Text <> "" Then
                            Kernel.ILPlayer3Name = ILPlayer3Namebox.Text
                            nop = nop + 1
                            If ILPlayer4Namebox.Text <> "" Then
                                Kernel.ILPlayer4Name = ILPlayer4Namebox.Text
                                nop = nop + 1
                                If ILPlayer5Namebox.Text <> "" Then
                                    Kernel.ILPlayer5Name = ILPlayer5Namebox.Text
                                    nop = nop + 1
                                    If ILPlayer6Namebox.Text <> "" Then
                                        Kernel.ILPlayer6Name = ILPlayer6Namebox.Text
                                        nop = nop + 1
                                        If ILPlayer7Namebox.Text <> "" Then
                                            Kernel.ILPlayer7Name = ILPlayer7Namebox.Text
                                            nop = nop + 1
                                            If ILPlayer8Namebox.Text <> "" Then
                                                Kernel.ILPlayer8Name = ILPlayer8Namebox.Text
                                                nop = nop + 1
                                                If ILPlayer9Namebox.Text <> "" Then
                                                    Kernel.ILPlayer9Name = ILPlayer9Namebox.Text
                                                    nop = nop + 1
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If ILChannelTextbox.Text = "" Or ILChannelTextbox.Text = "#" Then
                    MessageBox.Show("You must choose a IRC channel to use.")
                    Exit Sub
                End If
                Kernel.IRCClient.Join(ILChannelTextbox.Text)
                ILChannelTextbox.ReadOnly = True
                Kernel.ILNumberofPlayers = nop
                Kernel.ILChannelName = ILChannelTextbox.Text
                Kernel.ILBroadcasterTimerObj.StartTimer()
                frmILMapShowButton.Enabled = True
                ILActivateButton.Text = "Deactivate"
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Kernel.ConnectToIrc()
    End Sub

    Private Sub frmILMapShowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmILMapShowButton.Click
        Try
            frmILMap.Show()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region
End Class
