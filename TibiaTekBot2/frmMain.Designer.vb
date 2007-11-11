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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PopupMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowHideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.AlarmsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CavebotMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CharacterStatisticsMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ChangeLoginServerPopupItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConstantsEditorMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MCPatchMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowHideTibiaWindow = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ClosePopupItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FunctionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GeneralToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfigurationManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoLooterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoStackerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.LightEffectsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TorchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GreatTorchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UltimateTorchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtevoLuxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtevoGranLuxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UtevoVisLuxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LightWandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.AmmunitionRestackerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.CommandsListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CombobotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem27 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem27 = New System.Windows.Forms.ToolStripMenuItem
        Me.HealingToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SpellCasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoEaterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.RunemakerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoFisherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.TurboToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.TradeChannelAdvertiserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
        Me.TradeChannelWatcherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.EventsLoggingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.CavebotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
        Me.StatsUploaderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.FPSChangerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem
        Me.AFKToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoHealerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoUHerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoHealFriendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoHealPartyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem
        Me.ManaFluidDrinkerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem
        Me.InfoToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExperienceCheckerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem
        Me.CreaturesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem26 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem23 = New System.Windows.Forms.ToolStripMenuItem
        Me.CharacterInformationLookupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GuildMembersLookupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FloorExplorerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AroundToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BelowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NameSpyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileWebsitesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.TibiawikiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CharacterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GuildToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ErignetHighscorePagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GoogleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MytibiacomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SendLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GetItemIDsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TrainingToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoAttackerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem23 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FollowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FightingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OffensiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BalancedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DefensiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem26 = New System.Windows.Forms.ToolStripMenuItem
        Me.TrainerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem24 = New System.Windows.Forms.ToolStripMenuItem
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem24 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoPickupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem25 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem25 = New System.Windows.Forms.ToolStripMenuItem
        Me.FunToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FakeTitleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem
        Me.ChameleonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RainbowOutfitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem
        Me.FastToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SlowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OffToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem
        Me.MiscToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FeedbackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReloadDataFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SpellsxmlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OutfitsxmlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConstantsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TibiadatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutUsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DevelopmentWebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LicenseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MainTabControl = New System.Windows.Forms.TabControl
        Me.TabPage10 = New System.Windows.Forms.TabPage
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GeneralTabControl = New System.Windows.Forms.TabControl
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.AmmoRestackerBox = New System.Windows.Forms.GroupBox
        Me.MinAmmo = New System.Windows.Forms.NumericUpDown
        Me.LightBox = New System.Windows.Forms.GroupBox
        Me.LightEffectsTrigger = New System.Windows.Forms.CheckBox
        Me.LightEffect = New System.Windows.Forms.ComboBox
        Me.LELabel = New System.Windows.Forms.Label
        Me.AutoStackerBox = New System.Windows.Forms.GroupBox
        Me.AutoStackerTrigger = New System.Windows.Forms.CheckBox
        Me.AutoLooterBox = New System.Windows.Forms.GroupBox
        Me.MinCaplbl = New System.Windows.Forms.Label
        Me.MinCap = New System.Windows.Forms.NumericUpDown
        Me.AutoEaterEdit = New System.Windows.Forms.Button
        Me.AutoLooterTrigger = New System.Windows.Forms.CheckBox
        Me.ConfigManagerbox = New System.Windows.Forms.GroupBox
        Me.ClearConfig = New System.Windows.Forms.Button
        Me.EditConfig = New System.Windows.Forms.Button
        Me.ConfigLoad = New System.Windows.Forms.Button
        Me.TabPage13 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.AFKTabControl = New System.Windows.Forms.TabControl
        Me.TabPage14 = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.AutoFisherTrigger = New System.Windows.Forms.CheckBox
        Me.AutoFisherTurbo = New System.Windows.Forms.CheckBox
        Me.AutoFisherMinimumCapacity = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.AutoEaterEatFromFloorFirst = New System.Windows.Forms.CheckBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.AutoEaterInterval = New System.Windows.Forms.NumericUpDown
        Me.Label18 = New System.Windows.Forms.Label
        Me.AutoEaterEatFromFloor = New System.Windows.Forms.CheckBox
        Me.AutoEaterMinimumHitPoints = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.AutoEaterTrigger = New System.Windows.Forms.CheckBox
        Me.AutoEaterSmart = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RunemakerTrigger = New System.Windows.Forms.CheckBox
        Me.RunemakerMinimumSoulPoints = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.RunemakerMinimumManaPoints = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.RunemakerSpell = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.SpellCasterTrigger = New System.Windows.Forms.CheckBox
        Me.SpellCasterMinimumManaPoints = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.SpellCasterSpell = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage17 = New System.Windows.Forms.TabPage
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.CavebotTrigger = New System.Windows.Forms.CheckBox
        Me.CavebotConfigure = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.TradeChannelWatcherTrigger = New System.Windows.Forms.CheckBox
        Me.TradeChannelWatcherExpression = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TabPage9 = New System.Windows.Forms.TabPage
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.TradeChannelAdvertiserTrigger = New System.Windows.Forms.CheckBox
        Me.TradeChannelAdvertiserAdvertisement = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.HealingTabControl = New System.Windows.Forms.TabControl
        Me.TabPage23 = New System.Windows.Forms.TabPage
        Me.TabPage24 = New System.Windows.Forms.TabPage
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.MiscReloadTibiaDatButton = New System.Windows.Forms.Button
        Me.MiscReloadOutfitsButton = New System.Windows.Forms.Button
        Me.MiscReloadConstantsButton = New System.Windows.Forms.Button
        Me.MiscReloadItemsButton = New System.Windows.Forms.Button
        Me.MiscReloadSpellsButton = New System.Windows.Forms.Button
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.MinAmmolbl = New System.Windows.Forms.Label
        Me.AmmoRestackerTrigger = New System.Windows.Forms.CheckBox
        Me.ComboBotBox = New System.Windows.Forms.GroupBox
        Me.Leaderlbl = New System.Windows.Forms.Label
        Me.Leader = New System.Windows.Forms.TextBox
        Me.CombBotTrigger = New System.Windows.Forms.CheckBox
        Me.PopupMenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.TabPage10.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.GeneralTabControl.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.AmmoRestackerBox.SuspendLayout()
        CType(Me.MinAmmo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LightBox.SuspendLayout()
        Me.AutoStackerBox.SuspendLayout()
        Me.AutoLooterBox.SuspendLayout()
        CType(Me.MinCap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ConfigManagerbox.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.AFKTabControl.SuspendLayout()
        Me.TabPage14.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.AutoFisherMinimumCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.AutoEaterInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AutoEaterMinimumHitPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.RunemakerMinimumSoulPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RunemakerMinimumManaPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SpellCasterMinimumManaPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage17.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.TabPage9.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.HealingTabControl.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ComboBotBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon
        '
        Me.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon.ContextMenuStrip = Me.PopupMenu
        Me.NotifyIcon.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb21_16
        resources.ApplyResources(Me.NotifyIcon, "NotifyIcon")
        '
        'PopupMenu
        '
        Me.PopupMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHideToolStripMenuItem, Me.ToolStripSeparator4, Me.AlarmsToolStripMenuItem, Me.CavebotMenuItem, Me.CharacterStatisticsMenuItem, Me.ToolStripSeparator5, Me.ChangeLoginServerPopupItem, Me.ConstantsEditorMenuItem, Me.MCPatchMenuItem, Me.ToolStripSeparator2, Me.ShowHideTibiaWindow, Me.ToolStripSeparator1, Me.ClosePopupItem})
        Me.PopupMenu.Name = "PopupMenu"
        Me.PopupMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        resources.ApplyResources(Me.PopupMenu, "PopupMenu")
        '
        'ShowHideToolStripMenuItem
        '
        resources.ApplyResources(Me.ShowHideToolStripMenuItem, "ShowHideToolStripMenuItem")
        Me.ShowHideToolStripMenuItem.Name = "ShowHideToolStripMenuItem"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'AlarmsToolStripMenuItem
        '
        resources.ApplyResources(Me.AlarmsToolStripMenuItem, "AlarmsToolStripMenuItem")
        Me.AlarmsToolStripMenuItem.Name = "AlarmsToolStripMenuItem"
        '
        'CavebotMenuItem
        '
        resources.ApplyResources(Me.CavebotMenuItem, "CavebotMenuItem")
        Me.CavebotMenuItem.Name = "CavebotMenuItem"
        '
        'CharacterStatisticsMenuItem
        '
        resources.ApplyResources(Me.CharacterStatisticsMenuItem, "CharacterStatisticsMenuItem")
        Me.CharacterStatisticsMenuItem.Name = "CharacterStatisticsMenuItem"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        resources.ApplyResources(Me.ToolStripSeparator5, "ToolStripSeparator5")
        '
        'ChangeLoginServerPopupItem
        '
        resources.ApplyResources(Me.ChangeLoginServerPopupItem, "ChangeLoginServerPopupItem")
        Me.ChangeLoginServerPopupItem.Name = "ChangeLoginServerPopupItem"
        '
        'ConstantsEditorMenuItem
        '
        resources.ApplyResources(Me.ConstantsEditorMenuItem, "ConstantsEditorMenuItem")
        Me.ConstantsEditorMenuItem.Name = "ConstantsEditorMenuItem"
        '
        'MCPatchMenuItem
        '
        resources.ApplyResources(Me.MCPatchMenuItem, "MCPatchMenuItem")
        Me.MCPatchMenuItem.Name = "MCPatchMenuItem"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ShowHideTibiaWindow
        '
        resources.ApplyResources(Me.ShowHideTibiaWindow, "ShowHideTibiaWindow")
        Me.ShowHideTibiaWindow.Name = "ShowHideTibiaWindow"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'ClosePopupItem
        '
        resources.ApplyResources(Me.ClosePopupItem, "ClosePopupItem")
        Me.ClosePopupItem.Name = "ClosePopupItem"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FunctionsToolStripMenuItem, Me.AboutToolStripMenuItem, Me.HideToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'FunctionsToolStripMenuItem
        '
        Me.FunctionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GeneralToolStripMenuItem, Me.HealingToolsToolStripMenuItem, Me.AFKToolsToolStripMenuItem, Me.InfoToolsToolStripMenuItem, Me.TrainingToolsToolStripMenuItem, Me.FunToolsToolStripMenuItem, Me.MiscToolsToolStripMenuItem})
        Me.FunctionsToolStripMenuItem.Name = "FunctionsToolStripMenuItem"
        resources.ApplyResources(Me.FunctionsToolStripMenuItem, "FunctionsToolStripMenuItem")
        '
        'GeneralToolStripMenuItem
        '
        Me.GeneralToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurationManagerToolStripMenuItem, Me.AutoLooterToolStripMenuItem, Me.AutoStackerToolStripMenuItem, Me.LightEffectsToolStripMenuItem, Me.AmmunitionRestackerToolStripMenuItem, Me.CommandsListToolStripMenuItem, Me.CombobotToolStripMenuItem})
        Me.GeneralToolStripMenuItem.Name = "GeneralToolStripMenuItem"
        resources.ApplyResources(Me.GeneralToolStripMenuItem, "GeneralToolStripMenuItem")
        '
        'ConfigurationManagerToolStripMenuItem
        '
        Me.ConfigurationManagerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.LoadToolStripMenuItem, Me.ClearToolStripMenuItem})
        Me.ConfigurationManagerToolStripMenuItem.Name = "ConfigurationManagerToolStripMenuItem"
        resources.ApplyResources(Me.ConfigurationManagerToolStripMenuItem, "ConfigurationManagerToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        resources.ApplyResources(Me.LoadToolStripMenuItem, "LoadToolStripMenuItem")
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        resources.ApplyResources(Me.ClearToolStripMenuItem, "ClearToolStripMenuItem")
        '
        'AutoLooterToolStripMenuItem
        '
        Me.AutoLooterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem, Me.EditToolStripMenuItem1, Me.OffToolStripMenuItem})
        Me.AutoLooterToolStripMenuItem.Name = "AutoLooterToolStripMenuItem"
        resources.ApplyResources(Me.AutoLooterToolStripMenuItem, "AutoLooterToolStripMenuItem")
        '
        'OnToolStripMenuItem
        '
        Me.OnToolStripMenuItem.Name = "OnToolStripMenuItem"
        resources.ApplyResources(Me.OnToolStripMenuItem, "OnToolStripMenuItem")
        '
        'EditToolStripMenuItem1
        '
        Me.EditToolStripMenuItem1.Name = "EditToolStripMenuItem1"
        resources.ApplyResources(Me.EditToolStripMenuItem1, "EditToolStripMenuItem1")
        '
        'OffToolStripMenuItem
        '
        Me.OffToolStripMenuItem.Name = "OffToolStripMenuItem"
        resources.ApplyResources(Me.OffToolStripMenuItem, "OffToolStripMenuItem")
        '
        'AutoStackerToolStripMenuItem
        '
        Me.AutoStackerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem1, Me.OffToolStripMenuItem1})
        Me.AutoStackerToolStripMenuItem.Name = "AutoStackerToolStripMenuItem"
        resources.ApplyResources(Me.AutoStackerToolStripMenuItem, "AutoStackerToolStripMenuItem")
        '
        'OnToolStripMenuItem1
        '
        Me.OnToolStripMenuItem1.Name = "OnToolStripMenuItem1"
        resources.ApplyResources(Me.OnToolStripMenuItem1, "OnToolStripMenuItem1")
        '
        'OffToolStripMenuItem1
        '
        Me.OffToolStripMenuItem1.Name = "OffToolStripMenuItem1"
        resources.ApplyResources(Me.OffToolStripMenuItem1, "OffToolStripMenuItem1")
        '
        'LightEffectsToolStripMenuItem
        '
        Me.LightEffectsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UtToolStripMenuItem, Me.TorchToolStripMenuItem, Me.GreatTorchToolStripMenuItem, Me.UltimateTorchToolStripMenuItem, Me.UtevoLuxToolStripMenuItem, Me.UtevoGranLuxToolStripMenuItem, Me.UtevoVisLuxToolStripMenuItem, Me.LightWandToolStripMenuItem, Me.OffToolStripMenuItem2})
        Me.LightEffectsToolStripMenuItem.Name = "LightEffectsToolStripMenuItem"
        resources.ApplyResources(Me.LightEffectsToolStripMenuItem, "LightEffectsToolStripMenuItem")
        '
        'UtToolStripMenuItem
        '
        Me.UtToolStripMenuItem.Name = "UtToolStripMenuItem"
        resources.ApplyResources(Me.UtToolStripMenuItem, "UtToolStripMenuItem")
        '
        'TorchToolStripMenuItem
        '
        Me.TorchToolStripMenuItem.Name = "TorchToolStripMenuItem"
        resources.ApplyResources(Me.TorchToolStripMenuItem, "TorchToolStripMenuItem")
        '
        'GreatTorchToolStripMenuItem
        '
        Me.GreatTorchToolStripMenuItem.Name = "GreatTorchToolStripMenuItem"
        resources.ApplyResources(Me.GreatTorchToolStripMenuItem, "GreatTorchToolStripMenuItem")
        '
        'UltimateTorchToolStripMenuItem
        '
        Me.UltimateTorchToolStripMenuItem.Name = "UltimateTorchToolStripMenuItem"
        resources.ApplyResources(Me.UltimateTorchToolStripMenuItem, "UltimateTorchToolStripMenuItem")
        '
        'UtevoLuxToolStripMenuItem
        '
        Me.UtevoLuxToolStripMenuItem.Name = "UtevoLuxToolStripMenuItem"
        resources.ApplyResources(Me.UtevoLuxToolStripMenuItem, "UtevoLuxToolStripMenuItem")
        '
        'UtevoGranLuxToolStripMenuItem
        '
        Me.UtevoGranLuxToolStripMenuItem.Name = "UtevoGranLuxToolStripMenuItem"
        resources.ApplyResources(Me.UtevoGranLuxToolStripMenuItem, "UtevoGranLuxToolStripMenuItem")
        '
        'UtevoVisLuxToolStripMenuItem
        '
        Me.UtevoVisLuxToolStripMenuItem.Name = "UtevoVisLuxToolStripMenuItem"
        resources.ApplyResources(Me.UtevoVisLuxToolStripMenuItem, "UtevoVisLuxToolStripMenuItem")
        '
        'LightWandToolStripMenuItem
        '
        Me.LightWandToolStripMenuItem.Name = "LightWandToolStripMenuItem"
        resources.ApplyResources(Me.LightWandToolStripMenuItem, "LightWandToolStripMenuItem")
        '
        'OffToolStripMenuItem2
        '
        Me.OffToolStripMenuItem2.Name = "OffToolStripMenuItem2"
        resources.ApplyResources(Me.OffToolStripMenuItem2, "OffToolStripMenuItem2")
        '
        'AmmunitionRestackerToolStripMenuItem
        '
        Me.AmmunitionRestackerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem2, Me.OffToolStripMenuItem3})
        Me.AmmunitionRestackerToolStripMenuItem.Name = "AmmunitionRestackerToolStripMenuItem"
        resources.ApplyResources(Me.AmmunitionRestackerToolStripMenuItem, "AmmunitionRestackerToolStripMenuItem")
        '
        'OnToolStripMenuItem2
        '
        Me.OnToolStripMenuItem2.Name = "OnToolStripMenuItem2"
        resources.ApplyResources(Me.OnToolStripMenuItem2, "OnToolStripMenuItem2")
        '
        'OffToolStripMenuItem3
        '
        Me.OffToolStripMenuItem3.Name = "OffToolStripMenuItem3"
        resources.ApplyResources(Me.OffToolStripMenuItem3, "OffToolStripMenuItem3")
        '
        'CommandsListToolStripMenuItem
        '
        Me.CommandsListToolStripMenuItem.Name = "CommandsListToolStripMenuItem"
        resources.ApplyResources(Me.CommandsListToolStripMenuItem, "CommandsListToolStripMenuItem")
        '
        'CombobotToolStripMenuItem
        '
        Me.CombobotToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem27, Me.OffToolStripMenuItem27})
        Me.CombobotToolStripMenuItem.Name = "CombobotToolStripMenuItem"
        resources.ApplyResources(Me.CombobotToolStripMenuItem, "CombobotToolStripMenuItem")
        '
        'OnToolStripMenuItem27
        '
        Me.OnToolStripMenuItem27.Name = "OnToolStripMenuItem27"
        resources.ApplyResources(Me.OnToolStripMenuItem27, "OnToolStripMenuItem27")
        '
        'OffToolStripMenuItem27
        '
        Me.OffToolStripMenuItem27.Name = "OffToolStripMenuItem27"
        resources.ApplyResources(Me.OffToolStripMenuItem27, "OffToolStripMenuItem27")
        '
        'HealingToolsToolStripMenuItem
        '
        Me.HealingToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpellCasterToolStripMenuItem, Me.AutoEaterToolStripMenuItem, Me.RunemakerToolStripMenuItem, Me.AutoFisherToolStripMenuItem, Me.TradeChannelAdvertiserToolStripMenuItem, Me.TradeChannelWatcherToolStripMenuItem, Me.EventsLoggingToolStripMenuItem, Me.CavebotToolStripMenuItem, Me.StatsUploaderToolStripMenuItem, Me.FPSChangerToolStripMenuItem})
        Me.HealingToolsToolStripMenuItem.Name = "HealingToolsToolStripMenuItem"
        resources.ApplyResources(Me.HealingToolsToolStripMenuItem, "HealingToolsToolStripMenuItem")
        '
        'SpellCasterToolStripMenuItem
        '
        Me.SpellCasterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem3, Me.OffToolStripMenuItem4})
        Me.SpellCasterToolStripMenuItem.Name = "SpellCasterToolStripMenuItem"
        resources.ApplyResources(Me.SpellCasterToolStripMenuItem, "SpellCasterToolStripMenuItem")
        '
        'OnToolStripMenuItem3
        '
        Me.OnToolStripMenuItem3.Name = "OnToolStripMenuItem3"
        resources.ApplyResources(Me.OnToolStripMenuItem3, "OnToolStripMenuItem3")
        '
        'OffToolStripMenuItem4
        '
        Me.OffToolStripMenuItem4.Name = "OffToolStripMenuItem4"
        resources.ApplyResources(Me.OffToolStripMenuItem4, "OffToolStripMenuItem4")
        '
        'AutoEaterToolStripMenuItem
        '
        Me.AutoEaterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem4, Me.OffToolStripMenuItem5})
        Me.AutoEaterToolStripMenuItem.Name = "AutoEaterToolStripMenuItem"
        resources.ApplyResources(Me.AutoEaterToolStripMenuItem, "AutoEaterToolStripMenuItem")
        '
        'OnToolStripMenuItem4
        '
        Me.OnToolStripMenuItem4.Name = "OnToolStripMenuItem4"
        resources.ApplyResources(Me.OnToolStripMenuItem4, "OnToolStripMenuItem4")
        '
        'OffToolStripMenuItem5
        '
        Me.OffToolStripMenuItem5.Name = "OffToolStripMenuItem5"
        resources.ApplyResources(Me.OffToolStripMenuItem5, "OffToolStripMenuItem5")
        '
        'RunemakerToolStripMenuItem
        '
        Me.RunemakerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem5, Me.OffToolStripMenuItem6})
        Me.RunemakerToolStripMenuItem.Name = "RunemakerToolStripMenuItem"
        resources.ApplyResources(Me.RunemakerToolStripMenuItem, "RunemakerToolStripMenuItem")
        '
        'OnToolStripMenuItem5
        '
        Me.OnToolStripMenuItem5.Name = "OnToolStripMenuItem5"
        resources.ApplyResources(Me.OnToolStripMenuItem5, "OnToolStripMenuItem5")
        '
        'OffToolStripMenuItem6
        '
        Me.OffToolStripMenuItem6.Name = "OffToolStripMenuItem6"
        resources.ApplyResources(Me.OffToolStripMenuItem6, "OffToolStripMenuItem6")
        '
        'AutoFisherToolStripMenuItem
        '
        Me.AutoFisherToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem6, Me.TurboToolStripMenuItem, Me.OffToolStripMenuItem7})
        Me.AutoFisherToolStripMenuItem.Name = "AutoFisherToolStripMenuItem"
        resources.ApplyResources(Me.AutoFisherToolStripMenuItem, "AutoFisherToolStripMenuItem")
        '
        'OnToolStripMenuItem6
        '
        Me.OnToolStripMenuItem6.Name = "OnToolStripMenuItem6"
        resources.ApplyResources(Me.OnToolStripMenuItem6, "OnToolStripMenuItem6")
        '
        'TurboToolStripMenuItem
        '
        Me.TurboToolStripMenuItem.Name = "TurboToolStripMenuItem"
        resources.ApplyResources(Me.TurboToolStripMenuItem, "TurboToolStripMenuItem")
        '
        'OffToolStripMenuItem7
        '
        Me.OffToolStripMenuItem7.Name = "OffToolStripMenuItem7"
        resources.ApplyResources(Me.OffToolStripMenuItem7, "OffToolStripMenuItem7")
        '
        'TradeChannelAdvertiserToolStripMenuItem
        '
        Me.TradeChannelAdvertiserToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem7, Me.OffToolStripMenuItem8})
        Me.TradeChannelAdvertiserToolStripMenuItem.Name = "TradeChannelAdvertiserToolStripMenuItem"
        resources.ApplyResources(Me.TradeChannelAdvertiserToolStripMenuItem, "TradeChannelAdvertiserToolStripMenuItem")
        '
        'OnToolStripMenuItem7
        '
        Me.OnToolStripMenuItem7.Name = "OnToolStripMenuItem7"
        resources.ApplyResources(Me.OnToolStripMenuItem7, "OnToolStripMenuItem7")
        '
        'OffToolStripMenuItem8
        '
        Me.OffToolStripMenuItem8.Name = "OffToolStripMenuItem8"
        resources.ApplyResources(Me.OffToolStripMenuItem8, "OffToolStripMenuItem8")
        '
        'TradeChannelWatcherToolStripMenuItem
        '
        Me.TradeChannelWatcherToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem8, Me.OffToolStripMenuItem9})
        Me.TradeChannelWatcherToolStripMenuItem.Name = "TradeChannelWatcherToolStripMenuItem"
        resources.ApplyResources(Me.TradeChannelWatcherToolStripMenuItem, "TradeChannelWatcherToolStripMenuItem")
        '
        'OnToolStripMenuItem8
        '
        Me.OnToolStripMenuItem8.Name = "OnToolStripMenuItem8"
        resources.ApplyResources(Me.OnToolStripMenuItem8, "OnToolStripMenuItem8")
        '
        'OffToolStripMenuItem9
        '
        Me.OffToolStripMenuItem9.Name = "OffToolStripMenuItem9"
        resources.ApplyResources(Me.OffToolStripMenuItem9, "OffToolStripMenuItem9")
        '
        'EventsLoggingToolStripMenuItem
        '
        Me.EventsLoggingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem9, Me.OffToolStripMenuItem10})
        Me.EventsLoggingToolStripMenuItem.Name = "EventsLoggingToolStripMenuItem"
        resources.ApplyResources(Me.EventsLoggingToolStripMenuItem, "EventsLoggingToolStripMenuItem")
        '
        'OnToolStripMenuItem9
        '
        Me.OnToolStripMenuItem9.Name = "OnToolStripMenuItem9"
        resources.ApplyResources(Me.OnToolStripMenuItem9, "OnToolStripMenuItem9")
        '
        'OffToolStripMenuItem10
        '
        Me.OffToolStripMenuItem10.Name = "OffToolStripMenuItem10"
        resources.ApplyResources(Me.OffToolStripMenuItem10, "OffToolStripMenuItem10")
        '
        'CavebotToolStripMenuItem
        '
        Me.CavebotToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem10, Me.OffToolStripMenuItem11})
        Me.CavebotToolStripMenuItem.Name = "CavebotToolStripMenuItem"
        resources.ApplyResources(Me.CavebotToolStripMenuItem, "CavebotToolStripMenuItem")
        '
        'OnToolStripMenuItem10
        '
        Me.OnToolStripMenuItem10.Name = "OnToolStripMenuItem10"
        resources.ApplyResources(Me.OnToolStripMenuItem10, "OnToolStripMenuItem10")
        '
        'OffToolStripMenuItem11
        '
        Me.OffToolStripMenuItem11.Name = "OffToolStripMenuItem11"
        resources.ApplyResources(Me.OffToolStripMenuItem11, "OffToolStripMenuItem11")
        '
        'StatsUploaderToolStripMenuItem
        '
        Me.StatsUploaderToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem11, Me.OffToolStripMenuItem12})
        Me.StatsUploaderToolStripMenuItem.Name = "StatsUploaderToolStripMenuItem"
        resources.ApplyResources(Me.StatsUploaderToolStripMenuItem, "StatsUploaderToolStripMenuItem")
        '
        'OnToolStripMenuItem11
        '
        Me.OnToolStripMenuItem11.Name = "OnToolStripMenuItem11"
        resources.ApplyResources(Me.OnToolStripMenuItem11, "OnToolStripMenuItem11")
        '
        'OffToolStripMenuItem12
        '
        Me.OffToolStripMenuItem12.Name = "OffToolStripMenuItem12"
        resources.ApplyResources(Me.OffToolStripMenuItem12, "OffToolStripMenuItem12")
        '
        'FPSChangerToolStripMenuItem
        '
        Me.FPSChangerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem12, Me.OffToolStripMenuItem13})
        Me.FPSChangerToolStripMenuItem.Name = "FPSChangerToolStripMenuItem"
        resources.ApplyResources(Me.FPSChangerToolStripMenuItem, "FPSChangerToolStripMenuItem")
        '
        'OnToolStripMenuItem12
        '
        Me.OnToolStripMenuItem12.Name = "OnToolStripMenuItem12"
        resources.ApplyResources(Me.OnToolStripMenuItem12, "OnToolStripMenuItem12")
        '
        'OffToolStripMenuItem13
        '
        Me.OffToolStripMenuItem13.Name = "OffToolStripMenuItem13"
        resources.ApplyResources(Me.OffToolStripMenuItem13, "OffToolStripMenuItem13")
        '
        'AFKToolsToolStripMenuItem
        '
        Me.AFKToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutoHealerToolStripMenuItem, Me.AutoUHerToolStripMenuItem, Me.AutoHealFriendToolStripMenuItem, Me.AutoHealPartyToolStripMenuItem, Me.ManaFluidDrinkerToolStripMenuItem})
        Me.AFKToolsToolStripMenuItem.Name = "AFKToolsToolStripMenuItem"
        resources.ApplyResources(Me.AFKToolsToolStripMenuItem, "AFKToolsToolStripMenuItem")
        '
        'AutoHealerToolStripMenuItem
        '
        Me.AutoHealerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem13, Me.OffToolStripMenuItem14})
        Me.AutoHealerToolStripMenuItem.Name = "AutoHealerToolStripMenuItem"
        resources.ApplyResources(Me.AutoHealerToolStripMenuItem, "AutoHealerToolStripMenuItem")
        '
        'OnToolStripMenuItem13
        '
        Me.OnToolStripMenuItem13.Name = "OnToolStripMenuItem13"
        resources.ApplyResources(Me.OnToolStripMenuItem13, "OnToolStripMenuItem13")
        '
        'OffToolStripMenuItem14
        '
        Me.OffToolStripMenuItem14.Name = "OffToolStripMenuItem14"
        resources.ApplyResources(Me.OffToolStripMenuItem14, "OffToolStripMenuItem14")
        '
        'AutoUHerToolStripMenuItem
        '
        Me.AutoUHerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem14, Me.OffToolStripMenuItem15})
        Me.AutoUHerToolStripMenuItem.Name = "AutoUHerToolStripMenuItem"
        resources.ApplyResources(Me.AutoUHerToolStripMenuItem, "AutoUHerToolStripMenuItem")
        '
        'OnToolStripMenuItem14
        '
        Me.OnToolStripMenuItem14.Name = "OnToolStripMenuItem14"
        resources.ApplyResources(Me.OnToolStripMenuItem14, "OnToolStripMenuItem14")
        '
        'OffToolStripMenuItem15
        '
        Me.OffToolStripMenuItem15.Name = "OffToolStripMenuItem15"
        resources.ApplyResources(Me.OffToolStripMenuItem15, "OffToolStripMenuItem15")
        '
        'AutoHealFriendToolStripMenuItem
        '
        Me.AutoHealFriendToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem15, Me.OffToolStripMenuItem16})
        Me.AutoHealFriendToolStripMenuItem.Name = "AutoHealFriendToolStripMenuItem"
        resources.ApplyResources(Me.AutoHealFriendToolStripMenuItem, "AutoHealFriendToolStripMenuItem")
        '
        'OnToolStripMenuItem15
        '
        Me.OnToolStripMenuItem15.Name = "OnToolStripMenuItem15"
        resources.ApplyResources(Me.OnToolStripMenuItem15, "OnToolStripMenuItem15")
        '
        'OffToolStripMenuItem16
        '
        Me.OffToolStripMenuItem16.Name = "OffToolStripMenuItem16"
        resources.ApplyResources(Me.OffToolStripMenuItem16, "OffToolStripMenuItem16")
        '
        'AutoHealPartyToolStripMenuItem
        '
        Me.AutoHealPartyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem16, Me.OffToolStripMenuItem17})
        Me.AutoHealPartyToolStripMenuItem.Name = "AutoHealPartyToolStripMenuItem"
        resources.ApplyResources(Me.AutoHealPartyToolStripMenuItem, "AutoHealPartyToolStripMenuItem")
        '
        'OnToolStripMenuItem16
        '
        Me.OnToolStripMenuItem16.Name = "OnToolStripMenuItem16"
        resources.ApplyResources(Me.OnToolStripMenuItem16, "OnToolStripMenuItem16")
        '
        'OffToolStripMenuItem17
        '
        Me.OffToolStripMenuItem17.Name = "OffToolStripMenuItem17"
        resources.ApplyResources(Me.OffToolStripMenuItem17, "OffToolStripMenuItem17")
        '
        'ManaFluidDrinkerToolStripMenuItem
        '
        Me.ManaFluidDrinkerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem17, Me.OffToolStripMenuItem18})
        Me.ManaFluidDrinkerToolStripMenuItem.Name = "ManaFluidDrinkerToolStripMenuItem"
        resources.ApplyResources(Me.ManaFluidDrinkerToolStripMenuItem, "ManaFluidDrinkerToolStripMenuItem")
        '
        'OnToolStripMenuItem17
        '
        Me.OnToolStripMenuItem17.Name = "OnToolStripMenuItem17"
        resources.ApplyResources(Me.OnToolStripMenuItem17, "OnToolStripMenuItem17")
        '
        'OffToolStripMenuItem18
        '
        Me.OffToolStripMenuItem18.Name = "OffToolStripMenuItem18"
        resources.ApplyResources(Me.OffToolStripMenuItem18, "OffToolStripMenuItem18")
        '
        'InfoToolsToolStripMenuItem
        '
        Me.InfoToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExperienceCheckerToolStripMenuItem, Me.CharacterInformationLookupToolStripMenuItem, Me.GuildMembersLookupToolStripMenuItem, Me.FloorExplorerToolStripMenuItem, Me.NameSpyToolStripMenuItem, Me.OpenFileWebsitesToolStripMenuItem, Me.SendLocationToolStripMenuItem, Me.GetItemIDsToolStripMenuItem})
        Me.InfoToolsToolStripMenuItem.Name = "InfoToolsToolStripMenuItem"
        resources.ApplyResources(Me.InfoToolsToolStripMenuItem, "InfoToolsToolStripMenuItem")
        '
        'ExperienceCheckerToolStripMenuItem
        '
        Me.ExperienceCheckerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem22, Me.CreaturesToolStripMenuItem, Me.OffToolStripMenuItem23})
        Me.ExperienceCheckerToolStripMenuItem.Name = "ExperienceCheckerToolStripMenuItem"
        resources.ApplyResources(Me.ExperienceCheckerToolStripMenuItem, "ExperienceCheckerToolStripMenuItem")
        '
        'OnToolStripMenuItem22
        '
        Me.OnToolStripMenuItem22.Name = "OnToolStripMenuItem22"
        resources.ApplyResources(Me.OnToolStripMenuItem22, "OnToolStripMenuItem22")
        '
        'CreaturesToolStripMenuItem
        '
        Me.CreaturesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem26, Me.OffToolStripMenuItem20})
        Me.CreaturesToolStripMenuItem.Name = "CreaturesToolStripMenuItem"
        resources.ApplyResources(Me.CreaturesToolStripMenuItem, "CreaturesToolStripMenuItem")
        '
        'OnToolStripMenuItem26
        '
        Me.OnToolStripMenuItem26.Name = "OnToolStripMenuItem26"
        resources.ApplyResources(Me.OnToolStripMenuItem26, "OnToolStripMenuItem26")
        '
        'OffToolStripMenuItem20
        '
        Me.OffToolStripMenuItem20.Name = "OffToolStripMenuItem20"
        resources.ApplyResources(Me.OffToolStripMenuItem20, "OffToolStripMenuItem20")
        '
        'OffToolStripMenuItem23
        '
        Me.OffToolStripMenuItem23.Name = "OffToolStripMenuItem23"
        resources.ApplyResources(Me.OffToolStripMenuItem23, "OffToolStripMenuItem23")
        '
        'CharacterInformationLookupToolStripMenuItem
        '
        Me.CharacterInformationLookupToolStripMenuItem.Name = "CharacterInformationLookupToolStripMenuItem"
        resources.ApplyResources(Me.CharacterInformationLookupToolStripMenuItem, "CharacterInformationLookupToolStripMenuItem")
        '
        'GuildMembersLookupToolStripMenuItem
        '
        Me.GuildMembersLookupToolStripMenuItem.Name = "GuildMembersLookupToolStripMenuItem"
        resources.ApplyResources(Me.GuildMembersLookupToolStripMenuItem, "GuildMembersLookupToolStripMenuItem")
        '
        'FloorExplorerToolStripMenuItem
        '
        Me.FloorExplorerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpToolStripMenuItem, Me.AroundToolStripMenuItem, Me.BelowToolStripMenuItem})
        Me.FloorExplorerToolStripMenuItem.Name = "FloorExplorerToolStripMenuItem"
        resources.ApplyResources(Me.FloorExplorerToolStripMenuItem, "FloorExplorerToolStripMenuItem")
        '
        'UpToolStripMenuItem
        '
        Me.UpToolStripMenuItem.Name = "UpToolStripMenuItem"
        resources.ApplyResources(Me.UpToolStripMenuItem, "UpToolStripMenuItem")
        '
        'AroundToolStripMenuItem
        '
        Me.AroundToolStripMenuItem.Name = "AroundToolStripMenuItem"
        resources.ApplyResources(Me.AroundToolStripMenuItem, "AroundToolStripMenuItem")
        '
        'BelowToolStripMenuItem
        '
        Me.BelowToolStripMenuItem.Name = "BelowToolStripMenuItem"
        resources.ApplyResources(Me.BelowToolStripMenuItem, "BelowToolStripMenuItem")
        '
        'NameSpyToolStripMenuItem
        '
        Me.NameSpyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem21, Me.OffToolStripMenuItem22})
        Me.NameSpyToolStripMenuItem.Name = "NameSpyToolStripMenuItem"
        resources.ApplyResources(Me.NameSpyToolStripMenuItem, "NameSpyToolStripMenuItem")
        '
        'OnToolStripMenuItem21
        '
        Me.OnToolStripMenuItem21.Name = "OnToolStripMenuItem21"
        resources.ApplyResources(Me.OnToolStripMenuItem21, "OnToolStripMenuItem21")
        '
        'OffToolStripMenuItem22
        '
        Me.OffToolStripMenuItem22.Name = "OffToolStripMenuItem22"
        resources.ApplyResources(Me.OffToolStripMenuItem22, "OffToolStripMenuItem22")
        '
        'OpenFileWebsitesToolStripMenuItem
        '
        Me.OpenFileWebsitesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolStripSeparator3, Me.TibiawikiToolStripMenuItem, Me.CharacterToolStripMenuItem, Me.GuildToolStripMenuItem, Me.ErignetHighscorePagesToolStripMenuItem, Me.GoogleToolStripMenuItem, Me.MytibiacomToolStripMenuItem})
        Me.OpenFileWebsitesToolStripMenuItem.Name = "OpenFileWebsitesToolStripMenuItem"
        resources.ApplyResources(Me.OpenFileWebsitesToolStripMenuItem, "OpenFileWebsitesToolStripMenuItem")
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'TibiawikiToolStripMenuItem
        '
        Me.TibiawikiToolStripMenuItem.Name = "TibiawikiToolStripMenuItem"
        resources.ApplyResources(Me.TibiawikiToolStripMenuItem, "TibiawikiToolStripMenuItem")
        '
        'CharacterToolStripMenuItem
        '
        Me.CharacterToolStripMenuItem.Name = "CharacterToolStripMenuItem"
        resources.ApplyResources(Me.CharacterToolStripMenuItem, "CharacterToolStripMenuItem")
        '
        'GuildToolStripMenuItem
        '
        Me.GuildToolStripMenuItem.Name = "GuildToolStripMenuItem"
        resources.ApplyResources(Me.GuildToolStripMenuItem, "GuildToolStripMenuItem")
        '
        'ErignetHighscorePagesToolStripMenuItem
        '
        Me.ErignetHighscorePagesToolStripMenuItem.Name = "ErignetHighscorePagesToolStripMenuItem"
        resources.ApplyResources(Me.ErignetHighscorePagesToolStripMenuItem, "ErignetHighscorePagesToolStripMenuItem")
        '
        'GoogleToolStripMenuItem
        '
        Me.GoogleToolStripMenuItem.Name = "GoogleToolStripMenuItem"
        resources.ApplyResources(Me.GoogleToolStripMenuItem, "GoogleToolStripMenuItem")
        '
        'MytibiacomToolStripMenuItem
        '
        Me.MytibiacomToolStripMenuItem.Name = "MytibiacomToolStripMenuItem"
        resources.ApplyResources(Me.MytibiacomToolStripMenuItem, "MytibiacomToolStripMenuItem")
        '
        'SendLocationToolStripMenuItem
        '
        Me.SendLocationToolStripMenuItem.Name = "SendLocationToolStripMenuItem"
        resources.ApplyResources(Me.SendLocationToolStripMenuItem, "SendLocationToolStripMenuItem")
        '
        'GetItemIDsToolStripMenuItem
        '
        Me.GetItemIDsToolStripMenuItem.Name = "GetItemIDsToolStripMenuItem"
        resources.ApplyResources(Me.GetItemIDsToolStripMenuItem, "GetItemIDsToolStripMenuItem")
        '
        'TrainingToolsToolStripMenuItem
        '
        Me.TrainingToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutoAttackerToolStripMenuItem, Me.TrainerToolStripMenuItem, Me.AutoPickupToolStripMenuItem})
        Me.TrainingToolsToolStripMenuItem.Name = "TrainingToolsToolStripMenuItem"
        resources.ApplyResources(Me.TrainingToolsToolStripMenuItem, "TrainingToolsToolStripMenuItem")
        '
        'AutoAttackerToolStripMenuItem
        '
        Me.AutoAttackerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem23, Me.AutoToolStripMenuItem, Me.StanceToolStripMenuItem, Me.FightingToolStripMenuItem, Me.OffToolStripMenuItem26})
        Me.AutoAttackerToolStripMenuItem.Name = "AutoAttackerToolStripMenuItem"
        resources.ApplyResources(Me.AutoAttackerToolStripMenuItem, "AutoAttackerToolStripMenuItem")
        '
        'OnToolStripMenuItem23
        '
        Me.OnToolStripMenuItem23.Name = "OnToolStripMenuItem23"
        resources.ApplyResources(Me.OnToolStripMenuItem23, "OnToolStripMenuItem23")
        '
        'AutoToolStripMenuItem
        '
        Me.AutoToolStripMenuItem.Name = "AutoToolStripMenuItem"
        resources.ApplyResources(Me.AutoToolStripMenuItem, "AutoToolStripMenuItem")
        '
        'StanceToolStripMenuItem
        '
        Me.StanceToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StandToolStripMenuItem, Me.FollowToolStripMenuItem})
        Me.StanceToolStripMenuItem.Name = "StanceToolStripMenuItem"
        resources.ApplyResources(Me.StanceToolStripMenuItem, "StanceToolStripMenuItem")
        '
        'StandToolStripMenuItem
        '
        Me.StandToolStripMenuItem.Name = "StandToolStripMenuItem"
        resources.ApplyResources(Me.StandToolStripMenuItem, "StandToolStripMenuItem")
        '
        'FollowToolStripMenuItem
        '
        Me.FollowToolStripMenuItem.Name = "FollowToolStripMenuItem"
        resources.ApplyResources(Me.FollowToolStripMenuItem, "FollowToolStripMenuItem")
        '
        'FightingToolStripMenuItem
        '
        Me.FightingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OffensiveToolStripMenuItem, Me.BalancedToolStripMenuItem, Me.DefensiveToolStripMenuItem})
        Me.FightingToolStripMenuItem.Name = "FightingToolStripMenuItem"
        resources.ApplyResources(Me.FightingToolStripMenuItem, "FightingToolStripMenuItem")
        '
        'OffensiveToolStripMenuItem
        '
        Me.OffensiveToolStripMenuItem.Name = "OffensiveToolStripMenuItem"
        resources.ApplyResources(Me.OffensiveToolStripMenuItem, "OffensiveToolStripMenuItem")
        '
        'BalancedToolStripMenuItem
        '
        Me.BalancedToolStripMenuItem.Name = "BalancedToolStripMenuItem"
        resources.ApplyResources(Me.BalancedToolStripMenuItem, "BalancedToolStripMenuItem")
        '
        'DefensiveToolStripMenuItem
        '
        Me.DefensiveToolStripMenuItem.Name = "DefensiveToolStripMenuItem"
        resources.ApplyResources(Me.DefensiveToolStripMenuItem, "DefensiveToolStripMenuItem")
        '
        'OffToolStripMenuItem26
        '
        Me.OffToolStripMenuItem26.Name = "OffToolStripMenuItem26"
        resources.ApplyResources(Me.OffToolStripMenuItem26, "OffToolStripMenuItem26")
        '
        'TrainerToolStripMenuItem
        '
        Me.TrainerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem24, Me.AddToolStripMenuItem, Me.RemoveToolStripMenuItem, Me.ClearToolStripMenuItem1, Me.OffToolStripMenuItem24})
        Me.TrainerToolStripMenuItem.Name = "TrainerToolStripMenuItem"
        resources.ApplyResources(Me.TrainerToolStripMenuItem, "TrainerToolStripMenuItem")
        '
        'OnToolStripMenuItem24
        '
        Me.OnToolStripMenuItem24.Name = "OnToolStripMenuItem24"
        resources.ApplyResources(Me.OnToolStripMenuItem24, "OnToolStripMenuItem24")
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        resources.ApplyResources(Me.AddToolStripMenuItem, "AddToolStripMenuItem")
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        resources.ApplyResources(Me.RemoveToolStripMenuItem, "RemoveToolStripMenuItem")
        '
        'ClearToolStripMenuItem1
        '
        Me.ClearToolStripMenuItem1.Name = "ClearToolStripMenuItem1"
        resources.ApplyResources(Me.ClearToolStripMenuItem1, "ClearToolStripMenuItem1")
        '
        'OffToolStripMenuItem24
        '
        Me.OffToolStripMenuItem24.Name = "OffToolStripMenuItem24"
        resources.ApplyResources(Me.OffToolStripMenuItem24, "OffToolStripMenuItem24")
        '
        'AutoPickupToolStripMenuItem
        '
        Me.AutoPickupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem25, Me.OffToolStripMenuItem25})
        Me.AutoPickupToolStripMenuItem.Name = "AutoPickupToolStripMenuItem"
        resources.ApplyResources(Me.AutoPickupToolStripMenuItem, "AutoPickupToolStripMenuItem")
        '
        'OnToolStripMenuItem25
        '
        Me.OnToolStripMenuItem25.Name = "OnToolStripMenuItem25"
        resources.ApplyResources(Me.OnToolStripMenuItem25, "OnToolStripMenuItem25")
        '
        'OffToolStripMenuItem25
        '
        Me.OffToolStripMenuItem25.Name = "OffToolStripMenuItem25"
        resources.ApplyResources(Me.OffToolStripMenuItem25, "OffToolStripMenuItem25")
        '
        'FunToolsToolStripMenuItem
        '
        Me.FunToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FakeTitleToolStripMenuItem, Me.ChameleonToolStripMenuItem, Me.RainbowOutfitToolStripMenuItem})
        Me.FunToolsToolStripMenuItem.Name = "FunToolsToolStripMenuItem"
        resources.ApplyResources(Me.FunToolsToolStripMenuItem, "FunToolsToolStripMenuItem")
        '
        'FakeTitleToolStripMenuItem
        '
        Me.FakeTitleToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem18, Me.OffToolStripMenuItem19})
        Me.FakeTitleToolStripMenuItem.Name = "FakeTitleToolStripMenuItem"
        resources.ApplyResources(Me.FakeTitleToolStripMenuItem, "FakeTitleToolStripMenuItem")
        '
        'OnToolStripMenuItem18
        '
        Me.OnToolStripMenuItem18.Name = "OnToolStripMenuItem18"
        resources.ApplyResources(Me.OnToolStripMenuItem18, "OnToolStripMenuItem18")
        '
        'OffToolStripMenuItem19
        '
        Me.OffToolStripMenuItem19.Name = "OffToolStripMenuItem19"
        resources.ApplyResources(Me.OffToolStripMenuItem19, "OffToolStripMenuItem19")
        '
        'ChameleonToolStripMenuItem
        '
        Me.ChameleonToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem19, Me.CopyToolStripMenuItem})
        Me.ChameleonToolStripMenuItem.Name = "ChameleonToolStripMenuItem"
        resources.ApplyResources(Me.ChameleonToolStripMenuItem, "ChameleonToolStripMenuItem")
        '
        'OnToolStripMenuItem19
        '
        Me.OnToolStripMenuItem19.Name = "OnToolStripMenuItem19"
        resources.ApplyResources(Me.OnToolStripMenuItem19, "OnToolStripMenuItem19")
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        resources.ApplyResources(Me.CopyToolStripMenuItem, "CopyToolStripMenuItem")
        '
        'RainbowOutfitToolStripMenuItem
        '
        Me.RainbowOutfitToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem20, Me.FastToolStripMenuItem, Me.SlowToolStripMenuItem, Me.OffToolStripMenuItem21})
        Me.RainbowOutfitToolStripMenuItem.Name = "RainbowOutfitToolStripMenuItem"
        resources.ApplyResources(Me.RainbowOutfitToolStripMenuItem, "RainbowOutfitToolStripMenuItem")
        '
        'OnToolStripMenuItem20
        '
        Me.OnToolStripMenuItem20.Name = "OnToolStripMenuItem20"
        resources.ApplyResources(Me.OnToolStripMenuItem20, "OnToolStripMenuItem20")
        '
        'FastToolStripMenuItem
        '
        Me.FastToolStripMenuItem.Name = "FastToolStripMenuItem"
        resources.ApplyResources(Me.FastToolStripMenuItem, "FastToolStripMenuItem")
        '
        'SlowToolStripMenuItem
        '
        Me.SlowToolStripMenuItem.Name = "SlowToolStripMenuItem"
        resources.ApplyResources(Me.SlowToolStripMenuItem, "SlowToolStripMenuItem")
        '
        'OffToolStripMenuItem21
        '
        Me.OffToolStripMenuItem21.Name = "OffToolStripMenuItem21"
        resources.ApplyResources(Me.OffToolStripMenuItem21, "OffToolStripMenuItem21")
        '
        'MiscToolsToolStripMenuItem
        '
        Me.MiscToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FeedbackToolStripMenuItem, Me.ReloadDataFilesToolStripMenuItem, Me.PatchToolStripMenuItem})
        Me.MiscToolsToolStripMenuItem.Name = "MiscToolsToolStripMenuItem"
        resources.ApplyResources(Me.MiscToolsToolStripMenuItem, "MiscToolsToolStripMenuItem")
        '
        'FeedbackToolStripMenuItem
        '
        Me.FeedbackToolStripMenuItem.Name = "FeedbackToolStripMenuItem"
        resources.ApplyResources(Me.FeedbackToolStripMenuItem, "FeedbackToolStripMenuItem")
        '
        'ReloadDataFilesToolStripMenuItem
        '
        Me.ReloadDataFilesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpellsxmlToolStripMenuItem, Me.OutfitsxmlToolStripMenuItem, Me.ItemsToolStripMenuItem, Me.ConstantsToolStripMenuItem, Me.TibiadatToolStripMenuItem})
        Me.ReloadDataFilesToolStripMenuItem.Name = "ReloadDataFilesToolStripMenuItem"
        resources.ApplyResources(Me.ReloadDataFilesToolStripMenuItem, "ReloadDataFilesToolStripMenuItem")
        '
        'SpellsxmlToolStripMenuItem
        '
        Me.SpellsxmlToolStripMenuItem.Name = "SpellsxmlToolStripMenuItem"
        resources.ApplyResources(Me.SpellsxmlToolStripMenuItem, "SpellsxmlToolStripMenuItem")
        '
        'OutfitsxmlToolStripMenuItem
        '
        Me.OutfitsxmlToolStripMenuItem.Name = "OutfitsxmlToolStripMenuItem"
        resources.ApplyResources(Me.OutfitsxmlToolStripMenuItem, "OutfitsxmlToolStripMenuItem")
        '
        'ItemsToolStripMenuItem
        '
        Me.ItemsToolStripMenuItem.Name = "ItemsToolStripMenuItem"
        resources.ApplyResources(Me.ItemsToolStripMenuItem, "ItemsToolStripMenuItem")
        '
        'ConstantsToolStripMenuItem
        '
        Me.ConstantsToolStripMenuItem.Name = "ConstantsToolStripMenuItem"
        resources.ApplyResources(Me.ConstantsToolStripMenuItem, "ConstantsToolStripMenuItem")
        '
        'TibiadatToolStripMenuItem
        '
        Me.TibiadatToolStripMenuItem.Name = "TibiadatToolStripMenuItem"
        resources.ApplyResources(Me.TibiadatToolStripMenuItem, "TibiadatToolStripMenuItem")
        '
        'PatchToolStripMenuItem
        '
        Me.PatchToolStripMenuItem.Name = "PatchToolStripMenuItem"
        resources.ApplyResources(Me.PatchToolStripMenuItem, "PatchToolStripMenuItem")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutUsToolStripMenuItem, Me.WebsiteToolStripMenuItem, Me.VersionToolStripMenuItem, Me.DevelopmentWebsiteToolStripMenuItem, Me.LicenseToolStripMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'AboutUsToolStripMenuItem
        '
        Me.AboutUsToolStripMenuItem.Name = "AboutUsToolStripMenuItem"
        resources.ApplyResources(Me.AboutUsToolStripMenuItem, "AboutUsToolStripMenuItem")
        '
        'WebsiteToolStripMenuItem
        '
        Me.WebsiteToolStripMenuItem.Name = "WebsiteToolStripMenuItem"
        resources.ApplyResources(Me.WebsiteToolStripMenuItem, "WebsiteToolStripMenuItem")
        '
        'VersionToolStripMenuItem
        '
        Me.VersionToolStripMenuItem.Name = "VersionToolStripMenuItem"
        resources.ApplyResources(Me.VersionToolStripMenuItem, "VersionToolStripMenuItem")
        '
        'DevelopmentWebsiteToolStripMenuItem
        '
        Me.DevelopmentWebsiteToolStripMenuItem.Name = "DevelopmentWebsiteToolStripMenuItem"
        resources.ApplyResources(Me.DevelopmentWebsiteToolStripMenuItem, "DevelopmentWebsiteToolStripMenuItem")
        '
        'LicenseToolStripMenuItem
        '
        Me.LicenseToolStripMenuItem.Name = "LicenseToolStripMenuItem"
        resources.ApplyResources(Me.LicenseToolStripMenuItem, "LicenseToolStripMenuItem")
        '
        'HideToolStripMenuItem
        '
        Me.HideToolStripMenuItem.Name = "HideToolStripMenuItem"
        resources.ApplyResources(Me.HideToolStripMenuItem, "HideToolStripMenuItem")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'MainTabControl
        '
        resources.ApplyResources(Me.MainTabControl, "MainTabControl")
        Me.MainTabControl.Controls.Add(Me.TabPage10)
        Me.MainTabControl.Controls.Add(Me.TabPage1)
        Me.MainTabControl.Controls.Add(Me.TabPage2)
        Me.MainTabControl.Controls.Add(Me.TabPage3)
        Me.MainTabControl.Controls.Add(Me.TabPage7)
        Me.MainTabControl.Controls.Add(Me.TabPage4)
        Me.MainTabControl.Controls.Add(Me.TabPage5)
        Me.MainTabControl.Controls.Add(Me.TabPage6)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        '
        'TabPage10
        '
        Me.TabPage10.Controls.Add(Me.PictureBox1)
        resources.ApplyResources(Me.TabPage10, "TabPage10")
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.TibiaTekBot.My.Resources.Resources.ttb_splash
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GeneralTabControl)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GeneralTabControl
        '
        resources.ApplyResources(Me.GeneralTabControl, "GeneralTabControl")
        Me.GeneralTabControl.Controls.Add(Me.TabPage8)
        Me.GeneralTabControl.Controls.Add(Me.TabPage13)
        Me.GeneralTabControl.Name = "GeneralTabControl"
        Me.GeneralTabControl.SelectedIndex = 0
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.ComboBotBox)
        Me.TabPage8.Controls.Add(Me.AmmoRestackerBox)
        Me.TabPage8.Controls.Add(Me.LightBox)
        Me.TabPage8.Controls.Add(Me.AutoStackerBox)
        Me.TabPage8.Controls.Add(Me.AutoLooterBox)
        Me.TabPage8.Controls.Add(Me.ConfigManagerbox)
        resources.ApplyResources(Me.TabPage8, "TabPage8")
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'AmmoRestackerBox
        '
        Me.AmmoRestackerBox.Controls.Add(Me.AmmoRestackerTrigger)
        Me.AmmoRestackerBox.Controls.Add(Me.MinAmmolbl)
        Me.AmmoRestackerBox.Controls.Add(Me.MinAmmo)
        resources.ApplyResources(Me.AmmoRestackerBox, "AmmoRestackerBox")
        Me.AmmoRestackerBox.Name = "AmmoRestackerBox"
        Me.AmmoRestackerBox.TabStop = False
        '
        'MinAmmo
        '
        resources.ApplyResources(Me.MinAmmo, "MinAmmo")
        Me.MinAmmo.Name = "MinAmmo"
        '
        'LightBox
        '
        Me.LightBox.Controls.Add(Me.LightEffectsTrigger)
        Me.LightBox.Controls.Add(Me.LightEffect)
        Me.LightBox.Controls.Add(Me.LELabel)
        resources.ApplyResources(Me.LightBox, "LightBox")
        Me.LightBox.Name = "LightBox"
        Me.LightBox.TabStop = False
        '
        'LightEffectsTrigger
        '
        resources.ApplyResources(Me.LightEffectsTrigger, "LightEffectsTrigger")
        Me.LightEffectsTrigger.Name = "LightEffectsTrigger"
        Me.LightEffectsTrigger.UseVisualStyleBackColor = True
        '
        'LightEffect
        '
        Me.LightEffect.FormattingEnabled = True
        Me.LightEffect.Items.AddRange(New Object() {resources.GetString("LightEffect.Items"), resources.GetString("LightEffect.Items1"), resources.GetString("LightEffect.Items2"), resources.GetString("LightEffect.Items3"), resources.GetString("LightEffect.Items4"), resources.GetString("LightEffect.Items5"), resources.GetString("LightEffect.Items6"), resources.GetString("LightEffect.Items7"), resources.GetString("LightEffect.Items8")})
        resources.ApplyResources(Me.LightEffect, "LightEffect")
        Me.LightEffect.Name = "LightEffect"
        '
        'LELabel
        '
        resources.ApplyResources(Me.LELabel, "LELabel")
        Me.LELabel.Name = "LELabel"
        '
        'AutoStackerBox
        '
        Me.AutoStackerBox.Controls.Add(Me.AutoStackerTrigger)
        resources.ApplyResources(Me.AutoStackerBox, "AutoStackerBox")
        Me.AutoStackerBox.Name = "AutoStackerBox"
        Me.AutoStackerBox.TabStop = False
        '
        'AutoStackerTrigger
        '
        resources.ApplyResources(Me.AutoStackerTrigger, "AutoStackerTrigger")
        Me.AutoStackerTrigger.Name = "AutoStackerTrigger"
        Me.AutoStackerTrigger.UseVisualStyleBackColor = True
        '
        'AutoLooterBox
        '
        Me.AutoLooterBox.Controls.Add(Me.MinCaplbl)
        Me.AutoLooterBox.Controls.Add(Me.MinCap)
        Me.AutoLooterBox.Controls.Add(Me.AutoEaterEdit)
        Me.AutoLooterBox.Controls.Add(Me.AutoLooterTrigger)
        resources.ApplyResources(Me.AutoLooterBox, "AutoLooterBox")
        Me.AutoLooterBox.Name = "AutoLooterBox"
        Me.AutoLooterBox.TabStop = False
        '
        'MinCaplbl
        '
        resources.ApplyResources(Me.MinCaplbl, "MinCaplbl")
        Me.MinCaplbl.Name = "MinCaplbl"
        '
        'MinCap
        '
        resources.ApplyResources(Me.MinCap, "MinCap")
        Me.MinCap.Name = "MinCap"
        '
        'AutoEaterEdit
        '
        resources.ApplyResources(Me.AutoEaterEdit, "AutoEaterEdit")
        Me.AutoEaterEdit.Name = "AutoEaterEdit"
        Me.AutoEaterEdit.UseVisualStyleBackColor = True
        '
        'AutoLooterTrigger
        '
        resources.ApplyResources(Me.AutoLooterTrigger, "AutoLooterTrigger")
        Me.AutoLooterTrigger.Name = "AutoLooterTrigger"
        Me.AutoLooterTrigger.UseVisualStyleBackColor = True
        '
        'ConfigManagerbox
        '
        Me.ConfigManagerbox.Controls.Add(Me.ClearConfig)
        Me.ConfigManagerbox.Controls.Add(Me.EditConfig)
        Me.ConfigManagerbox.Controls.Add(Me.ConfigLoad)
        resources.ApplyResources(Me.ConfigManagerbox, "ConfigManagerbox")
        Me.ConfigManagerbox.Name = "ConfigManagerbox"
        Me.ConfigManagerbox.TabStop = False
        '
        'ClearConfig
        '
        resources.ApplyResources(Me.ClearConfig, "ClearConfig")
        Me.ClearConfig.Name = "ClearConfig"
        Me.ClearConfig.UseVisualStyleBackColor = True
        '
        'EditConfig
        '
        resources.ApplyResources(Me.EditConfig, "EditConfig")
        Me.EditConfig.Name = "EditConfig"
        Me.EditConfig.UseVisualStyleBackColor = True
        '
        'ConfigLoad
        '
        resources.ApplyResources(Me.ConfigLoad, "ConfigLoad")
        Me.ConfigLoad.Name = "ConfigLoad"
        Me.ConfigLoad.UseVisualStyleBackColor = True
        '
        'TabPage13
        '
        resources.ApplyResources(Me.TabPage13, "TabPage13")
        Me.TabPage13.Name = "TabPage13"
        Me.TabPage13.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.AFKTabControl)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'AFKTabControl
        '
        resources.ApplyResources(Me.AFKTabControl, "AFKTabControl")
        Me.AFKTabControl.Controls.Add(Me.TabPage14)
        Me.AFKTabControl.Controls.Add(Me.TabPage17)
        Me.AFKTabControl.Controls.Add(Me.TabPage9)
        Me.AFKTabControl.Multiline = True
        Me.AFKTabControl.Name = "AFKTabControl"
        Me.AFKTabControl.SelectedIndex = 0
        '
        'TabPage14
        '
        Me.TabPage14.Controls.Add(Me.GroupBox5)
        Me.TabPage14.Controls.Add(Me.GroupBox4)
        Me.TabPage14.Controls.Add(Me.GroupBox3)
        Me.TabPage14.Controls.Add(Me.GroupBox2)
        resources.ApplyResources(Me.TabPage14, "TabPage14")
        Me.TabPage14.Name = "TabPage14"
        Me.TabPage14.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.AutoFisherTrigger)
        Me.GroupBox5.Controls.Add(Me.AutoFisherTurbo)
        Me.GroupBox5.Controls.Add(Me.AutoFisherMinimumCapacity)
        Me.GroupBox5.Controls.Add(Me.Label7)
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        '
        'AutoFisherTrigger
        '
        resources.ApplyResources(Me.AutoFisherTrigger, "AutoFisherTrigger")
        Me.AutoFisherTrigger.Name = "AutoFisherTrigger"
        Me.AutoFisherTrigger.UseVisualStyleBackColor = True
        '
        'AutoFisherTurbo
        '
        resources.ApplyResources(Me.AutoFisherTurbo, "AutoFisherTurbo")
        Me.AutoFisherTurbo.Name = "AutoFisherTurbo"
        Me.AutoFisherTurbo.UseVisualStyleBackColor = True
        '
        'AutoFisherMinimumCapacity
        '
        resources.ApplyResources(Me.AutoFisherMinimumCapacity, "AutoFisherMinimumCapacity")
        Me.AutoFisherMinimumCapacity.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.AutoFisherMinimumCapacity.Name = "AutoFisherMinimumCapacity"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.AutoEaterEatFromFloorFirst)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.AutoEaterInterval)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.AutoEaterEatFromFloor)
        Me.GroupBox4.Controls.Add(Me.AutoEaterMinimumHitPoints)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.AutoEaterTrigger)
        Me.GroupBox4.Controls.Add(Me.AutoEaterSmart)
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'AutoEaterEatFromFloorFirst
        '
        resources.ApplyResources(Me.AutoEaterEatFromFloorFirst, "AutoEaterEatFromFloorFirst")
        Me.AutoEaterEatFromFloorFirst.Name = "AutoEaterEatFromFloorFirst"
        Me.AutoEaterEatFromFloorFirst.UseVisualStyleBackColor = True
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        '
        'AutoEaterInterval
        '
        resources.ApplyResources(Me.AutoEaterInterval, "AutoEaterInterval")
        Me.AutoEaterInterval.Maximum = New Decimal(New Integer() {999000, 0, 0, 0})
        Me.AutoEaterInterval.Name = "AutoEaterInterval"
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        '
        'AutoEaterEatFromFloor
        '
        resources.ApplyResources(Me.AutoEaterEatFromFloor, "AutoEaterEatFromFloor")
        Me.AutoEaterEatFromFloor.Name = "AutoEaterEatFromFloor"
        Me.AutoEaterEatFromFloor.UseVisualStyleBackColor = True
        '
        'AutoEaterMinimumHitPoints
        '
        resources.ApplyResources(Me.AutoEaterMinimumHitPoints, "AutoEaterMinimumHitPoints")
        Me.AutoEaterMinimumHitPoints.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.AutoEaterMinimumHitPoints.Name = "AutoEaterMinimumHitPoints"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'AutoEaterTrigger
        '
        resources.ApplyResources(Me.AutoEaterTrigger, "AutoEaterTrigger")
        Me.AutoEaterTrigger.Name = "AutoEaterTrigger"
        Me.AutoEaterTrigger.UseVisualStyleBackColor = True
        '
        'AutoEaterSmart
        '
        resources.ApplyResources(Me.AutoEaterSmart, "AutoEaterSmart")
        Me.AutoEaterSmart.Name = "AutoEaterSmart"
        Me.AutoEaterSmart.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RunemakerTrigger)
        Me.GroupBox3.Controls.Add(Me.RunemakerMinimumSoulPoints)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.RunemakerMinimumManaPoints)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.RunemakerSpell)
        Me.GroupBox3.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'RunemakerTrigger
        '
        resources.ApplyResources(Me.RunemakerTrigger, "RunemakerTrigger")
        Me.RunemakerTrigger.Name = "RunemakerTrigger"
        Me.RunemakerTrigger.UseVisualStyleBackColor = True
        '
        'RunemakerMinimumSoulPoints
        '
        resources.ApplyResources(Me.RunemakerMinimumSoulPoints, "RunemakerMinimumSoulPoints")
        Me.RunemakerMinimumSoulPoints.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.RunemakerMinimumSoulPoints.Name = "RunemakerMinimumSoulPoints"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'RunemakerMinimumManaPoints
        '
        resources.ApplyResources(Me.RunemakerMinimumManaPoints, "RunemakerMinimumManaPoints")
        Me.RunemakerMinimumManaPoints.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.RunemakerMinimumManaPoints.Name = "RunemakerMinimumManaPoints"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'RunemakerSpell
        '
        Me.RunemakerSpell.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.RunemakerSpell.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RunemakerSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RunemakerSpell.FormattingEnabled = True
        resources.ApplyResources(Me.RunemakerSpell, "RunemakerSpell")
        Me.RunemakerSpell.Name = "RunemakerSpell"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SpellCasterTrigger)
        Me.GroupBox2.Controls.Add(Me.SpellCasterMinimumManaPoints)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.SpellCasterSpell)
        Me.GroupBox2.Controls.Add(Me.Label1)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'SpellCasterTrigger
        '
        resources.ApplyResources(Me.SpellCasterTrigger, "SpellCasterTrigger")
        Me.SpellCasterTrigger.Name = "SpellCasterTrigger"
        Me.SpellCasterTrigger.UseVisualStyleBackColor = True
        '
        'SpellCasterMinimumManaPoints
        '
        resources.ApplyResources(Me.SpellCasterMinimumManaPoints, "SpellCasterMinimumManaPoints")
        Me.SpellCasterMinimumManaPoints.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.SpellCasterMinimumManaPoints.Name = "SpellCasterMinimumManaPoints"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'SpellCasterSpell
        '
        Me.SpellCasterSpell.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.SpellCasterSpell.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.SpellCasterSpell.FormattingEnabled = True
        resources.ApplyResources(Me.SpellCasterSpell, "SpellCasterSpell")
        Me.SpellCasterSpell.Name = "SpellCasterSpell"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'TabPage17
        '
        Me.TabPage17.Controls.Add(Me.GroupBox10)
        Me.TabPage17.Controls.Add(Me.GroupBox9)
        Me.TabPage17.Controls.Add(Me.GroupBox8)
        Me.TabPage17.Controls.Add(Me.GroupBox7)
        resources.ApplyResources(Me.TabPage17, "TabPage17")
        Me.TabPage17.Name = "TabPage17"
        Me.TabPage17.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.CheckBox3)
        Me.GroupBox10.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox10.Controls.Add(Me.CheckBox2)
        Me.GroupBox10.Controls.Add(Me.TableLayoutPanel2)
        resources.ApplyResources(Me.GroupBox10, "GroupBox10")
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.TabStop = False
        '
        'CheckBox3
        '
        resources.ApplyResources(Me.CheckBox3, "CheckBox3")
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        resources.ApplyResources(Me.TableLayoutPanel3, "TableLayoutPanel3")
        Me.TableLayoutPanel3.Controls.Add(Me.TextBox5, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label17, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TextBox6, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label19, 0, 1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        '
        'TextBox5
        '
        resources.ApplyResources(Me.TextBox5, "TextBox5")
        Me.TextBox5.Name = "TextBox5"
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        '
        'TextBox6
        '
        resources.ApplyResources(Me.TextBox6, "TextBox6")
        Me.TextBox6.Name = "TextBox6"
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        '
        'CheckBox2
        '
        resources.ApplyResources(Me.CheckBox2, "CheckBox2")
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox3, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox2, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label14, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox1, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label15, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label16, 0, 1)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        '
        'TextBox3
        '
        resources.ApplyResources(Me.TextBox3, "TextBox3")
        Me.TextBox3.Name = "TextBox3"
        '
        'TextBox2
        '
        resources.ApplyResources(Me.TextBox2, "TextBox2")
        Me.TextBox2.Name = "TextBox2"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'TextBox1
        '
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.CheckBox1)
        Me.GroupBox9.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = False
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label13, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown3, 1, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'NumericUpDown1
        '
        resources.ApplyResources(Me.NumericUpDown1, "NumericUpDown1")
        Me.NumericUpDown1.Name = "NumericUpDown1"
        '
        'NumericUpDown2
        '
        resources.ApplyResources(Me.NumericUpDown2, "NumericUpDown2")
        Me.NumericUpDown2.Name = "NumericUpDown2"
        '
        'NumericUpDown3
        '
        resources.ApplyResources(Me.NumericUpDown3, "NumericUpDown3")
        Me.NumericUpDown3.Name = "NumericUpDown3"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.CavebotTrigger)
        Me.GroupBox8.Controls.Add(Me.CavebotConfigure)
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = False
        '
        'CavebotTrigger
        '
        resources.ApplyResources(Me.CavebotTrigger, "CavebotTrigger")
        Me.CavebotTrigger.Name = "CavebotTrigger"
        Me.CavebotTrigger.UseVisualStyleBackColor = True
        '
        'CavebotConfigure
        '
        resources.ApplyResources(Me.CavebotConfigure, "CavebotConfigure")
        Me.CavebotConfigure.Name = "CavebotConfigure"
        Me.CavebotConfigure.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.TradeChannelWatcherTrigger)
        Me.GroupBox7.Controls.Add(Me.TradeChannelWatcherExpression)
        Me.GroupBox7.Controls.Add(Me.Label9)
        resources.ApplyResources(Me.GroupBox7, "GroupBox7")
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.TabStop = False
        '
        'TradeChannelWatcherTrigger
        '
        resources.ApplyResources(Me.TradeChannelWatcherTrigger, "TradeChannelWatcherTrigger")
        Me.TradeChannelWatcherTrigger.Name = "TradeChannelWatcherTrigger"
        Me.TradeChannelWatcherTrigger.UseVisualStyleBackColor = True
        '
        'TradeChannelWatcherExpression
        '
        resources.ApplyResources(Me.TradeChannelWatcherExpression, "TradeChannelWatcherExpression")
        Me.TradeChannelWatcherExpression.Name = "TradeChannelWatcherExpression"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.GroupBox6)
        resources.ApplyResources(Me.TabPage9, "TabPage9")
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TradeChannelAdvertiserTrigger)
        Me.GroupBox6.Controls.Add(Me.TradeChannelAdvertiserAdvertisement)
        Me.GroupBox6.Controls.Add(Me.Label8)
        resources.ApplyResources(Me.GroupBox6, "GroupBox6")
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.TabStop = False
        '
        'TradeChannelAdvertiserTrigger
        '
        resources.ApplyResources(Me.TradeChannelAdvertiserTrigger, "TradeChannelAdvertiserTrigger")
        Me.TradeChannelAdvertiserTrigger.Name = "TradeChannelAdvertiserTrigger"
        Me.TradeChannelAdvertiserTrigger.UseVisualStyleBackColor = True
        '
        'TradeChannelAdvertiserAdvertisement
        '
        resources.ApplyResources(Me.TradeChannelAdvertiserAdvertisement, "TradeChannelAdvertiserAdvertisement")
        Me.TradeChannelAdvertiserAdvertisement.Name = "TradeChannelAdvertiserAdvertisement"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.HealingTabControl)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'HealingTabControl
        '
        resources.ApplyResources(Me.HealingTabControl, "HealingTabControl")
        Me.HealingTabControl.Controls.Add(Me.TabPage23)
        Me.HealingTabControl.Controls.Add(Me.TabPage24)
        Me.HealingTabControl.Name = "HealingTabControl"
        Me.HealingTabControl.SelectedIndex = 0
        '
        'TabPage23
        '
        resources.ApplyResources(Me.TabPage23, "TabPage23")
        Me.TabPage23.Name = "TabPage23"
        Me.TabPage23.UseVisualStyleBackColor = True
        '
        'TabPage24
        '
        resources.ApplyResources(Me.TabPage24, "TabPage24")
        Me.TabPage24.Name = "TabPage24"
        Me.TabPage24.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.GroupBox1)
        resources.ApplyResources(Me.TabPage7, "TabPage7")
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MiscReloadTibiaDatButton)
        Me.GroupBox1.Controls.Add(Me.MiscReloadOutfitsButton)
        Me.GroupBox1.Controls.Add(Me.MiscReloadConstantsButton)
        Me.GroupBox1.Controls.Add(Me.MiscReloadItemsButton)
        Me.GroupBox1.Controls.Add(Me.MiscReloadSpellsButton)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'MiscReloadTibiaDatButton
        '
        resources.ApplyResources(Me.MiscReloadTibiaDatButton, "MiscReloadTibiaDatButton")
        Me.MiscReloadTibiaDatButton.Name = "MiscReloadTibiaDatButton"
        Me.MiscReloadTibiaDatButton.UseVisualStyleBackColor = True
        '
        'MiscReloadOutfitsButton
        '
        resources.ApplyResources(Me.MiscReloadOutfitsButton, "MiscReloadOutfitsButton")
        Me.MiscReloadOutfitsButton.Name = "MiscReloadOutfitsButton"
        Me.MiscReloadOutfitsButton.UseVisualStyleBackColor = True
        '
        'MiscReloadConstantsButton
        '
        resources.ApplyResources(Me.MiscReloadConstantsButton, "MiscReloadConstantsButton")
        Me.MiscReloadConstantsButton.Name = "MiscReloadConstantsButton"
        Me.MiscReloadConstantsButton.UseVisualStyleBackColor = True
        '
        'MiscReloadItemsButton
        '
        resources.ApplyResources(Me.MiscReloadItemsButton, "MiscReloadItemsButton")
        Me.MiscReloadItemsButton.Name = "MiscReloadItemsButton"
        Me.MiscReloadItemsButton.UseVisualStyleBackColor = True
        '
        'MiscReloadSpellsButton
        '
        resources.ApplyResources(Me.MiscReloadSpellsButton, "MiscReloadSpellsButton")
        Me.MiscReloadSpellsButton.Name = "MiscReloadSpellsButton"
        Me.MiscReloadSpellsButton.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        resources.ApplyResources(Me.TabPage5, "TabPage5")
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        resources.ApplyResources(Me.TabPage6, "TabPage6")
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'MinAmmolbl
        '
        resources.ApplyResources(Me.MinAmmolbl, "MinAmmolbl")
        Me.MinAmmolbl.Name = "MinAmmolbl"
        '
        'AmmoRestackerTrigger
        '
        resources.ApplyResources(Me.AmmoRestackerTrigger, "AmmoRestackerTrigger")
        Me.AmmoRestackerTrigger.Name = "AmmoRestackerTrigger"
        Me.AmmoRestackerTrigger.UseVisualStyleBackColor = True
        '
        'ComboBotBox
        '
        Me.ComboBotBox.Controls.Add(Me.CombBotTrigger)
        Me.ComboBotBox.Controls.Add(Me.Leader)
        Me.ComboBotBox.Controls.Add(Me.Leaderlbl)
        resources.ApplyResources(Me.ComboBotBox, "ComboBotBox")
        Me.ComboBotBox.Name = "ComboBotBox"
        Me.ComboBotBox.TabStop = False
        '
        'Leaderlbl
        '
        resources.ApplyResources(Me.Leaderlbl, "Leaderlbl")
        Me.Leaderlbl.Name = "Leaderlbl"
        '
        'Leader
        '
        resources.ApplyResources(Me.Leader, "Leader")
        Me.Leader.Name = "Leader"
        '
        'CombBotTrigger
        '
        resources.ApplyResources(Me.CombBotTrigger, "CombBotTrigger")
        Me.CombBotTrigger.Name = "CombBotTrigger"
        Me.CombBotTrigger.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Controls.Add(Me.MainTabControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb21
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.TransparencyKey = System.Drawing.SystemColors.ActiveBorder
        Me.PopupMenu.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.MainTabControl.ResumeLayout(False)
        Me.TabPage10.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.GeneralTabControl.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.AmmoRestackerBox.ResumeLayout(False)
        Me.AmmoRestackerBox.PerformLayout()
        CType(Me.MinAmmo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LightBox.ResumeLayout(False)
        Me.LightBox.PerformLayout()
        Me.AutoStackerBox.ResumeLayout(False)
        Me.AutoLooterBox.ResumeLayout(False)
        Me.AutoLooterBox.PerformLayout()
        CType(Me.MinCap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ConfigManagerbox.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.AFKTabControl.ResumeLayout(False)
        Me.TabPage14.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.AutoFisherMinimumCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.AutoEaterInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AutoEaterMinimumHitPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.RunemakerMinimumSoulPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RunemakerMinimumManaPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.SpellCasterMinimumManaPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage17.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.TabPage9.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.HealingTabControl.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ComboBotBox.ResumeLayout(False)
        Me.ComboBotBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PopupMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ClosePopupItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents AlarmsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowHideTibiaWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConstantsEditorMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CavebotMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FunctionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GeneralToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AFKToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrainingToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FunToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MiscToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoLooterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoStackerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LightEffectsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AmmunitionRestackerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommandsListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoHealerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoUHerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoHealFriendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoHealPartyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManaFluidDrinkerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExperienceCheckerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HealingToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpellCasterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoEaterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunemakerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoFisherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TradeChannelAdvertiserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TradeChannelWatcherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EventsLoggingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CavebotToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatsUploaderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CharacterInformationLookupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuildMembersLookupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FloorExplorerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NameSpyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileWebsitesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendLocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetItemIDsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoAttackerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrainerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoPickupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FakeTitleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChameleonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RainbowOutfitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FeedbackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FPSChangerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReloadDataFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutUsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebsiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TorchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GreatTorchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltimateTorchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtevoLuxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtevoGranLuxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UtevoVisLuxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LightWandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem22 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem23 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AroundToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BelowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem22 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem23 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FollowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FightingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffensiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BalancedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DefensiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem24 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem24 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem25 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem25 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TurboToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TibiawikiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CharacterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GuildToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ErignetHighscorePagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoogleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MytibiacomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FastToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SlowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpellsxmlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OutfitsxmlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConstantsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TibiadatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowHideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreaturesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChangeLoginServerPopupItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CharacterStatisticsMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CombobotToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnToolStripMenuItem27 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem27 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MCPatchMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DevelopmentWebsiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LicenseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents HealingTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage23 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage24 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MiscReloadOutfitsButton As System.Windows.Forms.Button
    Friend WithEvents MiscReloadConstantsButton As System.Windows.Forms.Button
    Friend WithEvents MiscReloadItemsButton As System.Windows.Forms.Button
    Friend WithEvents MiscReloadSpellsButton As System.Windows.Forms.Button
    Friend WithEvents MiscReloadTibiaDatButton As System.Windows.Forms.Button
    Friend WithEvents GeneralTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage13 As System.Windows.Forms.TabPage
    Friend WithEvents AFKTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage14 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage17 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SpellCasterTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents SpellCasterMinimumManaPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SpellCasterSpell As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RunemakerSpell As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RunemakerTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents RunemakerMinimumSoulPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RunemakerMinimumManaPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents AutoEaterTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents AutoEaterSmart As System.Windows.Forms.CheckBox
    Friend WithEvents AutoEaterMinimumHitPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents AutoFisherMinimumCapacity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents AutoFisherTurbo As System.Windows.Forms.CheckBox
    Friend WithEvents AutoFisherTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents TradeChannelWatcherTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents TradeChannelWatcherExpression As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents CavebotTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents CavebotConfigure As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents AutoEaterEatFromFloor As System.Windows.Forms.CheckBox
    Friend WithEvents AutoEaterEatFromFloorFirst As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents AutoEaterInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TradeChannelAdvertiserTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents TradeChannelAdvertiserAdvertisement As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TabPage10 As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ConfigManagerbox As System.Windows.Forms.GroupBox
    Friend WithEvents ConfigLoad As System.Windows.Forms.Button
    Friend WithEvents EditConfig As System.Windows.Forms.Button
    Friend WithEvents ClearConfig As System.Windows.Forms.Button
    Friend WithEvents AutoLooterBox As System.Windows.Forms.GroupBox
    Friend WithEvents AutoLooterTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents AutoStackerBox As System.Windows.Forms.GroupBox
    Friend WithEvents AutoStackerTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents AutoEaterEdit As System.Windows.Forms.Button
    Friend WithEvents LightBox As System.Windows.Forms.GroupBox
    Friend WithEvents LightEffectsTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents LightEffect As System.Windows.Forms.ComboBox
    Friend WithEvents LELabel As System.Windows.Forms.Label
    Friend WithEvents MinCaplbl As System.Windows.Forms.Label
    Friend WithEvents MinCap As System.Windows.Forms.NumericUpDown
    Friend WithEvents AmmoRestackerBox As System.Windows.Forms.GroupBox
    Friend WithEvents MinAmmo As System.Windows.Forms.NumericUpDown
    Friend WithEvents ComboBotBox As System.Windows.Forms.GroupBox
    Friend WithEvents AmmoRestackerTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents MinAmmolbl As System.Windows.Forms.Label
    Friend WithEvents CombBotTrigger As System.Windows.Forms.CheckBox
    Friend WithEvents Leader As System.Windows.Forms.TextBox
    Friend WithEvents Leaderlbl As System.Windows.Forms.Label
End Class
