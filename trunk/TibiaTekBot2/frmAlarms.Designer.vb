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
Partial Class frmAlarms
    Inherits System.Windows.Forms.Form

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Visible = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub

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
        Dim tablalala As System.Windows.Forms.TabPage
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ItemsCheckInventory = New System.Windows.Forms.CheckBox
        Me.ItemsCheckFloor = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.ItemsCount = New System.Windows.Forms.NumericUpDown
        Me.ItemsCondition = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.ItemsList = New System.Windows.Forms.CheckedListBox
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.ItemsPauseBot = New System.Windows.Forms.CheckBox
        Me.ItemsMessagePlayer = New System.Windows.Forms.CheckBox
        Me.ItemsLogOut = New System.Windows.Forms.CheckBox
        Me.ItemsPlaySound = New System.Windows.Forms.CheckBox
        Me.ItemsMessagePlayerName = New System.Windows.Forms.TextBox
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.MultiFloorGroupBox = New System.Windows.Forms.GroupBox
        Me.BattlelistMultiFloorAbove = New System.Windows.Forms.CheckBox
        Me.BattlelistMultiFloorBelow = New System.Windows.Forms.CheckBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.BattlelistGMCM = New System.Windows.Forms.CheckBox
        Me.BattlelistMonsterNPC = New System.Windows.Forms.CheckBox
        Me.BattlelistPlayerKiller = New System.Windows.Forms.CheckBox
        Me.BattlelistPlayer = New System.Windows.Forms.CheckBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.BattlelistIgnoredPlayerRemove = New System.Windows.Forms.Button
        Me.BattlelistIgnoredPlayerAdd = New System.Windows.Forms.Button
        Me.BattlelistIgnoredPlayersInput = New System.Windows.Forms.TextBox
        Me.BattlelistIgnoredPlayers = New System.Windows.Forms.ListBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BattleListPauseBot = New System.Windows.Forms.CheckBox
        Me.BattlelistMessagePlayer = New System.Windows.Forms.CheckBox
        Me.BattlelistLogout = New System.Windows.Forms.CheckBox
        Me.BattlelistPlaySound = New System.Windows.Forms.CheckBox
        Me.BattlelistMessagePlayerInput = New System.Windows.Forms.TextBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.MessageIgnoredPlayersRemove = New System.Windows.Forms.Button
        Me.MessageIgnoredPlayersAdd = New System.Windows.Forms.Button
        Me.MessageIgnoredPlayersInput = New System.Windows.Forms.TextBox
        Me.MessageIgnoredPlayers = New System.Windows.Forms.ListBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.MessageForwardMessage = New System.Windows.Forms.CheckBox
        Me.MessageLogOut = New System.Windows.Forms.CheckBox
        Me.MessagePlaySound = New System.Windows.Forms.CheckBox
        Me.MessageForwardMessageInput = New System.Windows.Forms.TextBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.MessagePrivate = New System.Windows.Forms.CheckBox
        Me.MessagePublic = New System.Windows.Forms.CheckBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.StatusConditionCombatSign = New System.Windows.Forms.CheckBox
        Me.StatusConditionDrowning = New System.Windows.Forms.CheckBox
        Me.StatusConditionParalized = New System.Windows.Forms.CheckBox
        Me.StatusConditionElectrified = New System.Windows.Forms.CheckBox
        Me.StatusConditionBurnt = New System.Windows.Forms.CheckBox
        Me.StatusConditionPoisoned = New System.Windows.Forms.CheckBox
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.StatusPauseBot = New System.Windows.Forms.CheckBox
        Me.StatusMessagePlayer = New System.Windows.Forms.CheckBox
        Me.StatusLogOut = New System.Windows.Forms.CheckBox
        Me.StatusPlaySound = New System.Windows.Forms.CheckBox
        Me.StatusMessagePlayerName = New System.Windows.Forms.TextBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.StatusCapacity = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.StatusSoulPoints = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.StatusManaPoints = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.StatusHitPoints = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.AlarmsSave = New System.Windows.Forms.Button
        Me.AlarmsHide = New System.Windows.Forms.Button
        Me.AlarmsLoad = New System.Windows.Forms.Button
        Me.AlarmsActivate = New System.Windows.Forms.Button
        Me.BattlelistAlarmTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusAlarmTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ItemsAlarmTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MessagePauseBot = New System.Windows.Forms.CheckBox
        tablalala = New System.Windows.Forms.TabPage
        tablalala.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        CType(Me.ItemsCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox14.SuspendLayout()
        Me.Tabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MultiFloorGroupBox.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        CType(Me.StatusCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusSoulPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusManaPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusHitPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tablalala
        '
        tablalala.Controls.Add(Me.GroupBox13)
        tablalala.Location = New System.Drawing.Point(4, 22)
        tablalala.Name = "tablalala"
        tablalala.Padding = New System.Windows.Forms.Padding(3)
        tablalala.Size = New System.Drawing.Size(360, 249)
        tablalala.TabIndex = 4
        tablalala.Text = "Items"
        tablalala.UseVisualStyleBackColor = True
        '
        'GroupBox13
        '
        Me.GroupBox13.Controls.Add(Me.GroupBox15)
        Me.GroupBox13.Controls.Add(Me.GroupBox14)
        Me.GroupBox13.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(348, 237)
        Me.GroupBox13.TabIndex = 0
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Items Alarm Settings"
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.Label8)
        Me.GroupBox15.Controls.Add(Me.ItemsCheckInventory)
        Me.GroupBox15.Controls.Add(Me.ItemsCheckFloor)
        Me.GroupBox15.Controls.Add(Me.Label10)
        Me.GroupBox15.Controls.Add(Me.ItemsCount)
        Me.GroupBox15.Controls.Add(Me.ItemsCondition)
        Me.GroupBox15.Controls.Add(Me.Label9)
        Me.GroupBox15.Controls.Add(Me.ItemsList)
        Me.GroupBox15.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(333, 134)
        Me.GroupBox15.TabIndex = 27
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Items List"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(224, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "and"
        '
        'ItemsCheckInventory
        '
        Me.ItemsCheckInventory.AutoSize = True
        Me.ItemsCheckInventory.Enabled = False
        Me.ItemsCheckInventory.Location = New System.Drawing.Point(149, 75)
        Me.ItemsCheckInventory.Name = "ItemsCheckInventory"
        Me.ItemsCheckInventory.Size = New System.Drawing.Size(72, 17)
        Me.ItemsCheckInventory.TabIndex = 6
        Me.ItemsCheckInventory.Text = " inventory"
        Me.ItemsCheckInventory.UseVisualStyleBackColor = True
        '
        'ItemsCheckFloor
        '
        Me.ItemsCheckFloor.AutoSize = True
        Me.ItemsCheckFloor.Enabled = False
        Me.ItemsCheckFloor.Location = New System.Drawing.Point(255, 75)
        Me.ItemsCheckFloor.Name = "ItemsCheckFloor"
        Me.ItemsCheckFloor.Size = New System.Drawing.Size(49, 17)
        Me.ItemsCheckFloor.TabIndex = 5
        Me.ItemsCheckFloor.Text = "floor."
        Me.ItemsCheckFloor.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(147, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(160, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "of the selected items, find also in"
        '
        'ItemsCount
        '
        Me.ItemsCount.Enabled = False
        Me.ItemsCount.Location = New System.Drawing.Point(205, 35)
        Me.ItemsCount.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.ItemsCount.Name = "ItemsCount"
        Me.ItemsCount.Size = New System.Drawing.Size(66, 20)
        Me.ItemsCount.TabIndex = 3
        '
        'ItemsCondition
        '
        Me.ItemsCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItemsCondition.Enabled = False
        Me.ItemsCondition.FormattingEnabled = True
        Me.ItemsCondition.Items.AddRange(New Object() {"=", "<", ">", "<=", ">=", "!="})
        Me.ItemsCondition.Location = New System.Drawing.Point(150, 35)
        Me.ItemsCondition.Name = "ItemsCondition"
        Me.ItemsCondition.Size = New System.Drawing.Size(49, 21)
        Me.ItemsCondition.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(146, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Fire alarm when you have:"
        '
        'ItemsList
        '
        Me.ItemsList.FormattingEnabled = True
        Me.ItemsList.Items.AddRange(New Object() {"Food", "Blank Runes", "Worms", "Throwables (Spears)", "Ammunition"})
        Me.ItemsList.Location = New System.Drawing.Point(6, 19)
        Me.ItemsList.Name = "ItemsList"
        Me.ItemsList.Size = New System.Drawing.Size(134, 109)
        Me.ItemsList.TabIndex = 0
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.ItemsPauseBot)
        Me.GroupBox14.Controls.Add(Me.ItemsMessagePlayer)
        Me.GroupBox14.Controls.Add(Me.ItemsLogOut)
        Me.GroupBox14.Controls.Add(Me.ItemsPlaySound)
        Me.GroupBox14.Controls.Add(Me.ItemsMessagePlayerName)
        Me.GroupBox14.Location = New System.Drawing.Point(6, 159)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(333, 72)
        Me.GroupBox14.TabIndex = 26
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "Actions"
        '
        'ItemsPauseBot
        '
        Me.ItemsPauseBot.AutoSize = True
        Me.ItemsPauseBot.Location = New System.Drawing.Point(10, 42)
        Me.ItemsPauseBot.Name = "ItemsPauseBot"
        Me.ItemsPauseBot.Size = New System.Drawing.Size(75, 17)
        Me.ItemsPauseBot.TabIndex = 13
        Me.ItemsPauseBot.Text = "Pause Bot"
        Me.ItemsPauseBot.UseVisualStyleBackColor = True
        '
        'ItemsMessagePlayer
        '
        Me.ItemsMessagePlayer.AutoSize = True
        Me.ItemsMessagePlayer.Location = New System.Drawing.Point(184, 19)
        Me.ItemsMessagePlayer.Name = "ItemsMessagePlayer"
        Me.ItemsMessagePlayer.Size = New System.Drawing.Size(104, 17)
        Me.ItemsMessagePlayer.TabIndex = 12
        Me.ItemsMessagePlayer.Text = "Message Player:"
        Me.ItemsMessagePlayer.UseVisualStyleBackColor = True
        '
        'ItemsLogOut
        '
        Me.ItemsLogOut.AutoSize = True
        Me.ItemsLogOut.Location = New System.Drawing.Point(96, 19)
        Me.ItemsLogOut.Name = "ItemsLogOut"
        Me.ItemsLogOut.Size = New System.Drawing.Size(59, 17)
        Me.ItemsLogOut.TabIndex = 11
        Me.ItemsLogOut.Text = "Logout"
        Me.ItemsLogOut.UseVisualStyleBackColor = True
        '
        'ItemsPlaySound
        '
        Me.ItemsPlaySound.AutoSize = True
        Me.ItemsPlaySound.Location = New System.Drawing.Point(10, 19)
        Me.ItemsPlaySound.Name = "ItemsPlaySound"
        Me.ItemsPlaySound.Size = New System.Drawing.Size(80, 17)
        Me.ItemsPlaySound.TabIndex = 10
        Me.ItemsPlaySound.Text = "Play Sound"
        Me.ItemsPlaySound.UseVisualStyleBackColor = True
        '
        'ItemsMessagePlayerName
        '
        Me.ItemsMessagePlayerName.Location = New System.Drawing.Point(184, 42)
        Me.ItemsMessagePlayerName.Name = "ItemsMessagePlayerName"
        Me.ItemsMessagePlayerName.Size = New System.Drawing.Size(123, 20)
        Me.ItemsMessagePlayerName.TabIndex = 9
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.TabPage1)
        Me.Tabs.Controls.Add(Me.TabPage3)
        Me.Tabs.Controls.Add(Me.TabPage2)
        Me.Tabs.Controls.Add(tablalala)
        Me.Tabs.Location = New System.Drawing.Point(12, 12)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(368, 275)
        Me.Tabs.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(360, 249)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Battlelist"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MultiFloorGroupBox)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(348, 237)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Battlelist Alarm Settings"
        '
        'MultiFloorGroupBox
        '
        Me.MultiFloorGroupBox.Controls.Add(Me.BattlelistMultiFloorAbove)
        Me.MultiFloorGroupBox.Controls.Add(Me.BattlelistMultiFloorBelow)
        Me.MultiFloorGroupBox.Location = New System.Drawing.Point(175, 65)
        Me.MultiFloorGroupBox.Name = "MultiFloorGroupBox"
        Me.MultiFloorGroupBox.Size = New System.Drawing.Size(167, 49)
        Me.MultiFloorGroupBox.TabIndex = 17
        Me.MultiFloorGroupBox.TabStop = False
        Me.MultiFloorGroupBox.Text = "Multi-Floor Triggers"
        '
        'BattlelistMultiFloorAbove
        '
        Me.BattlelistMultiFloorAbove.Appearance = System.Windows.Forms.Appearance.Button
        Me.BattlelistMultiFloorAbove.AutoSize = True
        Me.BattlelistMultiFloorAbove.Location = New System.Drawing.Point(107, 19)
        Me.BattlelistMultiFloorAbove.Name = "BattlelistMultiFloorAbove"
        Me.BattlelistMultiFloorAbove.Size = New System.Drawing.Size(54, 23)
        Me.BattlelistMultiFloorAbove.TabIndex = 1
        Me.BattlelistMultiFloorAbove.Text = " Above "
        Me.BattlelistMultiFloorAbove.UseVisualStyleBackColor = True
        '
        'BattlelistMultiFloorBelow
        '
        Me.BattlelistMultiFloorBelow.Appearance = System.Windows.Forms.Appearance.Button
        Me.BattlelistMultiFloorBelow.AutoSize = True
        Me.BattlelistMultiFloorBelow.Location = New System.Drawing.Point(6, 19)
        Me.BattlelistMultiFloorBelow.Name = "BattlelistMultiFloorBelow"
        Me.BattlelistMultiFloorBelow.Size = New System.Drawing.Size(52, 23)
        Me.BattlelistMultiFloorBelow.TabIndex = 0
        Me.BattlelistMultiFloorBelow.Text = " Below "
        Me.BattlelistMultiFloorBelow.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.BattlelistGMCM)
        Me.GroupBox6.Controls.Add(Me.BattlelistMonsterNPC)
        Me.GroupBox6.Controls.Add(Me.BattlelistPlayerKiller)
        Me.GroupBox6.Controls.Add(Me.BattlelistPlayer)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox6.TabIndex = 16
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Activate Alarm If"
        '
        'BattlelistGMCM
        '
        Me.BattlelistGMCM.AutoSize = True
        Me.BattlelistGMCM.Location = New System.Drawing.Point(250, 17)
        Me.BattlelistGMCM.Name = "BattlelistGMCM"
        Me.BattlelistGMCM.Size = New System.Drawing.Size(64, 17)
        Me.BattlelistGMCM.TabIndex = 7
        Me.BattlelistGMCM.Text = "GM/CM"
        Me.BattlelistGMCM.UseVisualStyleBackColor = True
        '
        'BattlelistMonsterNPC
        '
        Me.BattlelistMonsterNPC.AutoSize = True
        Me.BattlelistMonsterNPC.Location = New System.Drawing.Point(67, 17)
        Me.BattlelistMonsterNPC.Name = "BattlelistMonsterNPC"
        Me.BattlelistMonsterNPC.Size = New System.Drawing.Size(91, 17)
        Me.BattlelistMonsterNPC.TabIndex = 6
        Me.BattlelistMonsterNPC.Text = "Monster/NPC"
        Me.BattlelistMonsterNPC.UseVisualStyleBackColor = True
        '
        'BattlelistPlayerKiller
        '
        Me.BattlelistPlayerKiller.AutoSize = True
        Me.BattlelistPlayerKiller.Location = New System.Drawing.Point(164, 17)
        Me.BattlelistPlayerKiller.Name = "BattlelistPlayerKiller"
        Me.BattlelistPlayerKiller.Size = New System.Drawing.Size(80, 17)
        Me.BattlelistPlayerKiller.TabIndex = 5
        Me.BattlelistPlayerKiller.Text = "Player Killer"
        Me.BattlelistPlayerKiller.UseVisualStyleBackColor = True
        '
        'BattlelistPlayer
        '
        Me.BattlelistPlayer.AutoSize = True
        Me.BattlelistPlayer.Location = New System.Drawing.Point(6, 17)
        Me.BattlelistPlayer.Name = "BattlelistPlayer"
        Me.BattlelistPlayer.Size = New System.Drawing.Size(55, 17)
        Me.BattlelistPlayer.TabIndex = 4
        Me.BattlelistPlayer.Text = "Player"
        Me.BattlelistPlayer.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.BattlelistIgnoredPlayerRemove)
        Me.GroupBox5.Controls.Add(Me.BattlelistIgnoredPlayerAdd)
        Me.GroupBox5.Controls.Add(Me.BattlelistIgnoredPlayersInput)
        Me.GroupBox5.Controls.Add(Me.BattlelistIgnoredPlayers)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 65)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(163, 166)
        Me.GroupBox5.TabIndex = 15
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Ignored Players"
        '
        'BattlelistIgnoredPlayerRemove
        '
        Me.BattlelistIgnoredPlayerRemove.Location = New System.Drawing.Point(95, 140)
        Me.BattlelistIgnoredPlayerRemove.Name = "BattlelistIgnoredPlayerRemove"
        Me.BattlelistIgnoredPlayerRemove.Size = New System.Drawing.Size(62, 20)
        Me.BattlelistIgnoredPlayerRemove.TabIndex = 17
        Me.BattlelistIgnoredPlayerRemove.Text = "Remove"
        Me.BattlelistIgnoredPlayerRemove.UseVisualStyleBackColor = True
        '
        'BattlelistIgnoredPlayerAdd
        '
        Me.BattlelistIgnoredPlayerAdd.Location = New System.Drawing.Point(6, 140)
        Me.BattlelistIgnoredPlayerAdd.Name = "BattlelistIgnoredPlayerAdd"
        Me.BattlelistIgnoredPlayerAdd.Size = New System.Drawing.Size(40, 20)
        Me.BattlelistIgnoredPlayerAdd.TabIndex = 16
        Me.BattlelistIgnoredPlayerAdd.Text = "Add"
        Me.BattlelistIgnoredPlayerAdd.UseVisualStyleBackColor = True
        '
        'BattlelistIgnoredPlayersInput
        '
        Me.BattlelistIgnoredPlayersInput.Location = New System.Drawing.Point(6, 114)
        Me.BattlelistIgnoredPlayersInput.Name = "BattlelistIgnoredPlayersInput"
        Me.BattlelistIgnoredPlayersInput.Size = New System.Drawing.Size(151, 20)
        Me.BattlelistIgnoredPlayersInput.TabIndex = 15
        '
        'BattlelistIgnoredPlayers
        '
        Me.BattlelistIgnoredPlayers.FormattingEnabled = True
        Me.BattlelistIgnoredPlayers.Location = New System.Drawing.Point(6, 19)
        Me.BattlelistIgnoredPlayers.Name = "BattlelistIgnoredPlayers"
        Me.BattlelistIgnoredPlayers.Size = New System.Drawing.Size(151, 82)
        Me.BattlelistIgnoredPlayers.Sorted = True
        Me.BattlelistIgnoredPlayers.TabIndex = 14
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.BattleListPauseBot)
        Me.GroupBox4.Controls.Add(Me.BattlelistMessagePlayer)
        Me.GroupBox4.Controls.Add(Me.BattlelistLogout)
        Me.GroupBox4.Controls.Add(Me.BattlelistPlaySound)
        Me.GroupBox4.Controls.Add(Me.BattlelistMessagePlayerInput)
        Me.GroupBox4.Location = New System.Drawing.Point(175, 120)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(167, 111)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Actions"
        '
        'BattleListPauseBot
        '
        Me.BattleListPauseBot.AutoSize = True
        Me.BattleListPauseBot.Location = New System.Drawing.Point(10, 39)
        Me.BattleListPauseBot.Name = "BattleListPauseBot"
        Me.BattleListPauseBot.Size = New System.Drawing.Size(75, 17)
        Me.BattleListPauseBot.TabIndex = 13
        Me.BattleListPauseBot.Text = "Pause Bot"
        Me.BattleListPauseBot.UseVisualStyleBackColor = True
        '
        'BattlelistMessagePlayer
        '
        Me.BattlelistMessagePlayer.AutoSize = True
        Me.BattlelistMessagePlayer.Location = New System.Drawing.Point(10, 59)
        Me.BattlelistMessagePlayer.Name = "BattlelistMessagePlayer"
        Me.BattlelistMessagePlayer.Size = New System.Drawing.Size(104, 17)
        Me.BattlelistMessagePlayer.TabIndex = 12
        Me.BattlelistMessagePlayer.Text = "Message Player:"
        Me.BattlelistMessagePlayer.UseVisualStyleBackColor = True
        '
        'BattlelistLogout
        '
        Me.BattlelistLogout.AutoSize = True
        Me.BattlelistLogout.Location = New System.Drawing.Point(96, 19)
        Me.BattlelistLogout.Name = "BattlelistLogout"
        Me.BattlelistLogout.Size = New System.Drawing.Size(59, 17)
        Me.BattlelistLogout.TabIndex = 11
        Me.BattlelistLogout.Text = "Logout"
        Me.BattlelistLogout.UseVisualStyleBackColor = True
        '
        'BattlelistPlaySound
        '
        Me.BattlelistPlaySound.AutoSize = True
        Me.BattlelistPlaySound.Location = New System.Drawing.Point(10, 19)
        Me.BattlelistPlaySound.Name = "BattlelistPlaySound"
        Me.BattlelistPlaySound.Size = New System.Drawing.Size(80, 17)
        Me.BattlelistPlaySound.TabIndex = 10
        Me.BattlelistPlaySound.Text = "Play Sound"
        Me.BattlelistPlaySound.UseVisualStyleBackColor = True
        '
        'BattlelistMessagePlayerInput
        '
        Me.BattlelistMessagePlayerInput.Location = New System.Drawing.Point(10, 82)
        Me.BattlelistMessagePlayerInput.Name = "BattlelistMessagePlayerInput"
        Me.BattlelistMessagePlayerInput.Size = New System.Drawing.Size(120, 20)
        Me.BattlelistMessagePlayerInput.TabIndex = 9
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(360, 249)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Message"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.GroupBox9)
        Me.GroupBox3.Controls.Add(Me.GroupBox7)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(348, 237)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Received Messages Alarm Settings"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MessageIgnoredPlayersRemove)
        Me.GroupBox2.Controls.Add(Me.MessageIgnoredPlayersAdd)
        Me.GroupBox2.Controls.Add(Me.MessageIgnoredPlayersInput)
        Me.GroupBox2.Controls.Add(Me.MessageIgnoredPlayers)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 65)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(163, 166)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Ignored Players"
        '
        'MessageIgnoredPlayersRemove
        '
        Me.MessageIgnoredPlayersRemove.Location = New System.Drawing.Point(95, 140)
        Me.MessageIgnoredPlayersRemove.Name = "MessageIgnoredPlayersRemove"
        Me.MessageIgnoredPlayersRemove.Size = New System.Drawing.Size(62, 20)
        Me.MessageIgnoredPlayersRemove.TabIndex = 17
        Me.MessageIgnoredPlayersRemove.Text = "Remove"
        Me.MessageIgnoredPlayersRemove.UseVisualStyleBackColor = True
        '
        'MessageIgnoredPlayersAdd
        '
        Me.MessageIgnoredPlayersAdd.Location = New System.Drawing.Point(6, 140)
        Me.MessageIgnoredPlayersAdd.Name = "MessageIgnoredPlayersAdd"
        Me.MessageIgnoredPlayersAdd.Size = New System.Drawing.Size(40, 20)
        Me.MessageIgnoredPlayersAdd.TabIndex = 16
        Me.MessageIgnoredPlayersAdd.Text = "Add"
        Me.MessageIgnoredPlayersAdd.UseVisualStyleBackColor = True
        '
        'MessageIgnoredPlayersInput
        '
        Me.MessageIgnoredPlayersInput.Location = New System.Drawing.Point(6, 114)
        Me.MessageIgnoredPlayersInput.Name = "MessageIgnoredPlayersInput"
        Me.MessageIgnoredPlayersInput.Size = New System.Drawing.Size(151, 20)
        Me.MessageIgnoredPlayersInput.TabIndex = 15
        '
        'MessageIgnoredPlayers
        '
        Me.MessageIgnoredPlayers.FormattingEnabled = True
        Me.MessageIgnoredPlayers.Location = New System.Drawing.Point(6, 19)
        Me.MessageIgnoredPlayers.Name = "MessageIgnoredPlayers"
        Me.MessageIgnoredPlayers.Size = New System.Drawing.Size(151, 82)
        Me.MessageIgnoredPlayers.TabIndex = 14
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.MessagePauseBot)
        Me.GroupBox9.Controls.Add(Me.MessageForwardMessage)
        Me.GroupBox9.Controls.Add(Me.MessageLogOut)
        Me.GroupBox9.Controls.Add(Me.MessagePlaySound)
        Me.GroupBox9.Controls.Add(Me.MessageForwardMessageInput)
        Me.GroupBox9.Location = New System.Drawing.Point(175, 65)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(167, 166)
        Me.GroupBox9.TabIndex = 21
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Actions"
        '
        'MessageForwardMessage
        '
        Me.MessageForwardMessage.AutoSize = True
        Me.MessageForwardMessage.Location = New System.Drawing.Point(10, 85)
        Me.MessageForwardMessage.Name = "MessageForwardMessage"
        Me.MessageForwardMessage.Size = New System.Drawing.Size(110, 17)
        Me.MessageForwardMessage.TabIndex = 12
        Me.MessageForwardMessage.Text = "Forward Message"
        Me.MessageForwardMessage.UseVisualStyleBackColor = True
        '
        'MessageLogOut
        '
        Me.MessageLogOut.AutoSize = True
        Me.MessageLogOut.Location = New System.Drawing.Point(10, 39)
        Me.MessageLogOut.Name = "MessageLogOut"
        Me.MessageLogOut.Size = New System.Drawing.Size(59, 17)
        Me.MessageLogOut.TabIndex = 11
        Me.MessageLogOut.Text = "Logout"
        Me.MessageLogOut.UseVisualStyleBackColor = True
        '
        'MessagePlaySound
        '
        Me.MessagePlaySound.AutoSize = True
        Me.MessagePlaySound.Location = New System.Drawing.Point(10, 19)
        Me.MessagePlaySound.Name = "MessagePlaySound"
        Me.MessagePlaySound.Size = New System.Drawing.Size(80, 17)
        Me.MessagePlaySound.TabIndex = 10
        Me.MessagePlaySound.Text = "Play Sound"
        Me.MessagePlaySound.UseVisualStyleBackColor = True
        '
        'MessageForwardMessageInput
        '
        Me.MessageForwardMessageInput.Location = New System.Drawing.Point(10, 108)
        Me.MessageForwardMessageInput.Name = "MessageForwardMessageInput"
        Me.MessageForwardMessageInput.Size = New System.Drawing.Size(120, 20)
        Me.MessageForwardMessageInput.TabIndex = 9
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.MessagePrivate)
        Me.GroupBox7.Controls.Add(Me.MessagePublic)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(336, 40)
        Me.GroupBox7.TabIndex = 19
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Activate Alarm If"
        '
        'MessagePrivate
        '
        Me.MessagePrivate.AutoSize = True
        Me.MessagePrivate.Location = New System.Drawing.Point(133, 17)
        Me.MessagePrivate.Name = "MessagePrivate"
        Me.MessagePrivate.Size = New System.Drawing.Size(105, 17)
        Me.MessagePrivate.TabIndex = 1
        Me.MessagePrivate.Text = "Private Message"
        Me.MessagePrivate.UseVisualStyleBackColor = True
        '
        'MessagePublic
        '
        Me.MessagePublic.AutoSize = True
        Me.MessagePublic.Location = New System.Drawing.Point(6, 17)
        Me.MessagePublic.Name = "MessagePublic"
        Me.MessagePublic.Size = New System.Drawing.Size(101, 17)
        Me.MessagePublic.TabIndex = 0
        Me.MessagePublic.Text = "Public Message"
        Me.MessagePublic.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox8)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(360, 249)
        Me.TabPage2.TabIndex = 3
        Me.TabPage2.Text = "Status"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GroupBox12)
        Me.GroupBox8.Controls.Add(Me.GroupBox11)
        Me.GroupBox8.Controls.Add(Me.GroupBox10)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(348, 237)
        Me.GroupBox8.TabIndex = 0
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Status Alarm Settings"
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.StatusConditionCombatSign)
        Me.GroupBox12.Controls.Add(Me.StatusConditionDrowning)
        Me.GroupBox12.Controls.Add(Me.StatusConditionParalized)
        Me.GroupBox12.Controls.Add(Me.StatusConditionElectrified)
        Me.GroupBox12.Controls.Add(Me.StatusConditionBurnt)
        Me.GroupBox12.Controls.Add(Me.StatusConditionPoisoned)
        Me.GroupBox12.Location = New System.Drawing.Point(6, 140)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(177, 87)
        Me.GroupBox12.TabIndex = 23
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "If Conditioned"
        '
        'StatusConditionCombatSign
        '
        Me.StatusConditionCombatSign.AutoSize = True
        Me.StatusConditionCombatSign.Location = New System.Drawing.Point(6, 19)
        Me.StatusConditionCombatSign.Name = "StatusConditionCombatSign"
        Me.StatusConditionCombatSign.Size = New System.Drawing.Size(86, 17)
        Me.StatusConditionCombatSign.TabIndex = 19
        Me.StatusConditionCombatSign.Text = "Combat Sign"
        Me.StatusConditionCombatSign.UseVisualStyleBackColor = True
        '
        'StatusConditionDrowning
        '
        Me.StatusConditionDrowning.AutoSize = True
        Me.StatusConditionDrowning.Location = New System.Drawing.Point(100, 41)
        Me.StatusConditionDrowning.Name = "StatusConditionDrowning"
        Me.StatusConditionDrowning.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusConditionDrowning.Size = New System.Drawing.Size(71, 17)
        Me.StatusConditionDrowning.TabIndex = 18
        Me.StatusConditionDrowning.Text = "Drowning"
        Me.StatusConditionDrowning.UseVisualStyleBackColor = True
        '
        'StatusConditionParalized
        '
        Me.StatusConditionParalized.AutoSize = True
        Me.StatusConditionParalized.Location = New System.Drawing.Point(6, 63)
        Me.StatusConditionParalized.Name = "StatusConditionParalized"
        Me.StatusConditionParalized.Size = New System.Drawing.Size(69, 17)
        Me.StatusConditionParalized.TabIndex = 17
        Me.StatusConditionParalized.Text = "Paralized"
        Me.StatusConditionParalized.UseVisualStyleBackColor = True
        '
        'StatusConditionElectrified
        '
        Me.StatusConditionElectrified.AutoSize = True
        Me.StatusConditionElectrified.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.StatusConditionElectrified.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.StatusConditionElectrified.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatusConditionElectrified.Location = New System.Drawing.Point(99, 63)
        Me.StatusConditionElectrified.Name = "StatusConditionElectrified"
        Me.StatusConditionElectrified.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusConditionElectrified.Size = New System.Drawing.Size(72, 17)
        Me.StatusConditionElectrified.TabIndex = 16
        Me.StatusConditionElectrified.Text = "Electrified"
        Me.StatusConditionElectrified.UseVisualStyleBackColor = True
        '
        'StatusConditionBurnt
        '
        Me.StatusConditionBurnt.AutoSize = True
        Me.StatusConditionBurnt.Location = New System.Drawing.Point(120, 19)
        Me.StatusConditionBurnt.Name = "StatusConditionBurnt"
        Me.StatusConditionBurnt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusConditionBurnt.Size = New System.Drawing.Size(51, 17)
        Me.StatusConditionBurnt.TabIndex = 15
        Me.StatusConditionBurnt.Text = "Burnt"
        Me.StatusConditionBurnt.UseVisualStyleBackColor = True
        '
        'StatusConditionPoisoned
        '
        Me.StatusConditionPoisoned.AutoSize = True
        Me.StatusConditionPoisoned.Location = New System.Drawing.Point(6, 41)
        Me.StatusConditionPoisoned.Name = "StatusConditionPoisoned"
        Me.StatusConditionPoisoned.Size = New System.Drawing.Size(70, 17)
        Me.StatusConditionPoisoned.TabIndex = 14
        Me.StatusConditionPoisoned.Text = "Poisoned"
        Me.StatusConditionPoisoned.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.StatusPauseBot)
        Me.GroupBox11.Controls.Add(Me.StatusMessagePlayer)
        Me.GroupBox11.Controls.Add(Me.StatusLogOut)
        Me.GroupBox11.Controls.Add(Me.StatusPlaySound)
        Me.GroupBox11.Controls.Add(Me.StatusMessagePlayerName)
        Me.GroupBox11.Location = New System.Drawing.Point(189, 19)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(153, 152)
        Me.GroupBox11.TabIndex = 22
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Actions"
        '
        'StatusPauseBot
        '
        Me.StatusPauseBot.AutoSize = True
        Me.StatusPauseBot.Location = New System.Drawing.Point(10, 62)
        Me.StatusPauseBot.Name = "StatusPauseBot"
        Me.StatusPauseBot.Size = New System.Drawing.Size(75, 17)
        Me.StatusPauseBot.TabIndex = 13
        Me.StatusPauseBot.Text = "Pause Bot"
        Me.StatusPauseBot.UseVisualStyleBackColor = True
        '
        'StatusMessagePlayer
        '
        Me.StatusMessagePlayer.AutoSize = True
        Me.StatusMessagePlayer.Location = New System.Drawing.Point(10, 85)
        Me.StatusMessagePlayer.Name = "StatusMessagePlayer"
        Me.StatusMessagePlayer.Size = New System.Drawing.Size(104, 17)
        Me.StatusMessagePlayer.TabIndex = 12
        Me.StatusMessagePlayer.Text = "Message Player:"
        Me.StatusMessagePlayer.UseVisualStyleBackColor = True
        '
        'StatusLogOut
        '
        Me.StatusLogOut.AutoSize = True
        Me.StatusLogOut.Location = New System.Drawing.Point(10, 39)
        Me.StatusLogOut.Name = "StatusLogOut"
        Me.StatusLogOut.Size = New System.Drawing.Size(59, 17)
        Me.StatusLogOut.TabIndex = 11
        Me.StatusLogOut.Text = "Logout"
        Me.StatusLogOut.UseVisualStyleBackColor = True
        '
        'StatusPlaySound
        '
        Me.StatusPlaySound.AutoSize = True
        Me.StatusPlaySound.Location = New System.Drawing.Point(10, 19)
        Me.StatusPlaySound.Name = "StatusPlaySound"
        Me.StatusPlaySound.Size = New System.Drawing.Size(80, 17)
        Me.StatusPlaySound.TabIndex = 10
        Me.StatusPlaySound.Text = "Play Sound"
        Me.StatusPlaySound.UseVisualStyleBackColor = True
        '
        'StatusMessagePlayerName
        '
        Me.StatusMessagePlayerName.Location = New System.Drawing.Point(10, 108)
        Me.StatusMessagePlayerName.Name = "StatusMessagePlayerName"
        Me.StatusMessagePlayerName.Size = New System.Drawing.Size(120, 20)
        Me.StatusMessagePlayerName.TabIndex = 9
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.StatusCapacity)
        Me.GroupBox10.Controls.Add(Me.Label7)
        Me.GroupBox10.Controls.Add(Me.StatusSoulPoints)
        Me.GroupBox10.Controls.Add(Me.Label6)
        Me.GroupBox10.Controls.Add(Me.StatusManaPoints)
        Me.GroupBox10.Controls.Add(Me.Label5)
        Me.GroupBox10.Controls.Add(Me.StatusHitPoints)
        Me.GroupBox10.Controls.Add(Me.Label4)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(177, 115)
        Me.GroupBox10.TabIndex = 0
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Activate Alarm If"
        '
        'StatusCapacity
        '
        Me.StatusCapacity.Location = New System.Drawing.Point(110, 87)
        Me.StatusCapacity.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.StatusCapacity.Name = "StatusCapacity"
        Me.StatusCapacity.Size = New System.Drawing.Size(47, 20)
        Me.StatusCapacity.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 89)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Capacity below:"
        '
        'StatusSoulPoints
        '
        Me.StatusSoulPoints.Location = New System.Drawing.Point(110, 65)
        Me.StatusSoulPoints.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.StatusSoulPoints.Name = "StatusSoulPoints"
        Me.StatusSoulPoints.Size = New System.Drawing.Size(47, 20)
        Me.StatusSoulPoints.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Soul Points below:"
        '
        'StatusManaPoints
        '
        Me.StatusManaPoints.Location = New System.Drawing.Point(110, 43)
        Me.StatusManaPoints.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.StatusManaPoints.Name = "StatusManaPoints"
        Me.StatusManaPoints.Size = New System.Drawing.Size(47, 20)
        Me.StatusManaPoints.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Mana Points below:"
        '
        'StatusHitPoints
        '
        Me.StatusHitPoints.Location = New System.Drawing.Point(110, 21)
        Me.StatusHitPoints.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.StatusHitPoints.Name = "StatusHitPoints"
        Me.StatusHitPoints.Size = New System.Drawing.Size(47, 20)
        Me.StatusHitPoints.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Hit Points below:"
        '
        'AlarmsSave
        '
        Me.AlarmsSave.Location = New System.Drawing.Point(12, 293)
        Me.AlarmsSave.Name = "AlarmsSave"
        Me.AlarmsSave.Size = New System.Drawing.Size(75, 23)
        Me.AlarmsSave.TabIndex = 1
        Me.AlarmsSave.Text = "Save"
        Me.AlarmsSave.UseVisualStyleBackColor = True
        '
        'AlarmsHide
        '
        Me.AlarmsHide.Location = New System.Drawing.Point(305, 293)
        Me.AlarmsHide.Name = "AlarmsHide"
        Me.AlarmsHide.Size = New System.Drawing.Size(75, 23)
        Me.AlarmsHide.TabIndex = 2
        Me.AlarmsHide.Text = "Hide"
        Me.AlarmsHide.UseVisualStyleBackColor = True
        '
        'AlarmsLoad
        '
        Me.AlarmsLoad.Location = New System.Drawing.Point(93, 293)
        Me.AlarmsLoad.Name = "AlarmsLoad"
        Me.AlarmsLoad.Size = New System.Drawing.Size(75, 23)
        Me.AlarmsLoad.TabIndex = 3
        Me.AlarmsLoad.Text = "Load"
        Me.AlarmsLoad.UseVisualStyleBackColor = True
        '
        'AlarmsActivate
        '
        Me.AlarmsActivate.Location = New System.Drawing.Point(224, 293)
        Me.AlarmsActivate.Name = "AlarmsActivate"
        Me.AlarmsActivate.Size = New System.Drawing.Size(75, 23)
        Me.AlarmsActivate.TabIndex = 4
        Me.AlarmsActivate.Text = "Activate"
        Me.AlarmsActivate.UseVisualStyleBackColor = True
        '
        'BattlelistAlarmTimer
        '
        Me.BattlelistAlarmTimer.Interval = 500
        '
        'StatusAlarmTimer
        '
        Me.StatusAlarmTimer.Interval = 500
        '
        'ItemsAlarmTimer
        '
        Me.ItemsAlarmTimer.Interval = 1000
        '
        'MessagePauseBot
        '
        Me.MessagePauseBot.AutoSize = True
        Me.MessagePauseBot.Location = New System.Drawing.Point(10, 62)
        Me.MessagePauseBot.Name = "MessagePauseBot"
        Me.MessagePauseBot.Size = New System.Drawing.Size(72, 17)
        Me.MessagePauseBot.TabIndex = 13
        Me.MessagePauseBot.Text = "PauseBot"
        Me.MessagePauseBot.UseVisualStyleBackColor = True
        '
        'frmAlarms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 328)
        Me.Controls.Add(Me.AlarmsActivate)
        Me.Controls.Add(Me.AlarmsLoad)
        Me.Controls.Add(Me.AlarmsHide)
        Me.Controls.Add(Me.AlarmsSave)
        Me.Controls.Add(Me.Tabs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb
        Me.Name = "frmAlarms"
        Me.Text = "Alarms"
        tablalala.ResumeLayout(False)
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        CType(Me.ItemsCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.Tabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.MultiFloorGroupBox.ResumeLayout(False)
        Me.MultiFloorGroupBox.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.StatusCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusSoulPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusManaPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusHitPoints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents MessagePublic As System.Windows.Forms.CheckBox
    Friend WithEvents AlarmsSave As System.Windows.Forms.Button
    Friend WithEvents AlarmsHide As System.Windows.Forms.Button
    Friend WithEvents AlarmsLoad As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents BattlelistMessagePlayer As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistLogout As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistPlaySound As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistMessagePlayerInput As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BattlelistIgnoredPlayerRemove As System.Windows.Forms.Button
    Friend WithEvents BattlelistIgnoredPlayerAdd As System.Windows.Forms.Button
    Friend WithEvents BattlelistIgnoredPlayersInput As System.Windows.Forms.TextBox
    Friend WithEvents BattlelistIgnoredPlayers As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents BattlelistMonsterNPC As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistPlayerKiller As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistPlayer As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents MessageForwardMessage As System.Windows.Forms.CheckBox
    Friend WithEvents MessageLogOut As System.Windows.Forms.CheckBox
    Friend WithEvents MessagePlaySound As System.Windows.Forms.CheckBox
    Friend WithEvents MessageForwardMessageInput As System.Windows.Forms.TextBox
    Friend WithEvents MessagePrivate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MessageIgnoredPlayersRemove As System.Windows.Forms.Button
    Friend WithEvents MessageIgnoredPlayersAdd As System.Windows.Forms.Button
    Friend WithEvents MessageIgnoredPlayersInput As System.Windows.Forms.TextBox
    Friend WithEvents MessageIgnoredPlayers As System.Windows.Forms.ListBox
    Friend WithEvents AlarmsActivate As System.Windows.Forms.Button
    Friend WithEvents BattlelistAlarmTimer As System.Windows.Forms.Timer
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusMessagePlayer As System.Windows.Forms.CheckBox
    Friend WithEvents StatusLogOut As System.Windows.Forms.CheckBox
    Friend WithEvents StatusPlaySound As System.Windows.Forms.CheckBox
    Friend WithEvents StatusMessagePlayerName As System.Windows.Forms.TextBox
    Friend WithEvents StatusSoulPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents StatusManaPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents StatusHitPoints As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents StatusAlarmTimer As System.Windows.Forms.Timer
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusConditionCombatSign As System.Windows.Forms.CheckBox
    Friend WithEvents StatusConditionDrowning As System.Windows.Forms.CheckBox
    Friend WithEvents StatusConditionParalized As System.Windows.Forms.CheckBox
    Friend WithEvents StatusConditionElectrified As System.Windows.Forms.CheckBox
    Friend WithEvents StatusConditionBurnt As System.Windows.Forms.CheckBox
    Friend WithEvents StatusConditionPoisoned As System.Windows.Forms.CheckBox
    Friend WithEvents StatusCapacity As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents ItemsMessagePlayer As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsLogOut As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsPlaySound As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsMessagePlayerName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents ItemsList As System.Windows.Forms.CheckedListBox
    Friend WithEvents ItemsCondition As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ItemsCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents ItemsCheckInventory As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsCheckFloor As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsAlarmTimer As System.Windows.Forms.Timer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents MultiFloorGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BattlelistMultiFloorAbove As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistMultiFloorBelow As System.Windows.Forms.CheckBox
    Friend WithEvents BattlelistGMCM As System.Windows.Forms.CheckBox
    Friend WithEvents BattleListPauseBot As System.Windows.Forms.CheckBox
    Friend WithEvents StatusPauseBot As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsPauseBot As System.Windows.Forms.CheckBox
    Friend WithEvents MessagePauseBot As System.Windows.Forms.CheckBox
End Class
