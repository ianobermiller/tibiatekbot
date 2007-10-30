<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCavebot
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
        Me.AddWp = New System.Windows.Forms.GroupBox
        Me.AddWaypointcmd = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Typecmb = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Waypointslst = New System.Windows.Forms.ListBox
        Me.Direction = New System.Windows.Forms.GroupBox
        Me.dRight = New System.Windows.Forms.RadioButton
        Me.dDown = New System.Windows.Forms.RadioButton
        Me.dLeft = New System.Windows.Forms.RadioButton
        Me.dUp = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Loadcmd = New System.Windows.Forms.Button
        Me.Savecmd = New System.Windows.Forms.Button
        Me.WPClearcmd = New System.Windows.Forms.Button
        Me.WPDeletecmd = New System.Windows.Forms.Button
        Me.Infobox = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Infotxt = New System.Windows.Forms.TextBox
        Me.Waitbox = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Waittxt = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RemoveMonster = New System.Windows.Forms.Button
        Me.AddMonster = New System.Windows.Forms.Button
        Me.MonsterList = New System.Windows.Forms.ListBox
        Me.EnableMonsterList = New System.Windows.Forms.CheckBox
        Me.EatFromCorpses = New System.Windows.Forms.CheckBox
        Me.LootMinimumCap = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.LootFromCorpses = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.AddWp.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Direction.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Infobox.SuspendLayout()
        Me.Waitbox.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.LootMinimumCap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AddWp
        '
        Me.AddWp.Controls.Add(Me.AddWaypointcmd)
        Me.AddWp.Controls.Add(Me.Label1)
        Me.AddWp.Controls.Add(Me.Typecmb)
        Me.AddWp.Location = New System.Drawing.Point(12, 11)
        Me.AddWp.Name = "AddWp"
        Me.AddWp.Size = New System.Drawing.Size(146, 105)
        Me.AddWp.TabIndex = 0
        Me.AddWp.TabStop = False
        Me.AddWp.Text = "Add Waypoint"
        '
        'AddWaypointcmd
        '
        Me.AddWaypointcmd.Enabled = False
        Me.AddWaypointcmd.Location = New System.Drawing.Point(9, 68)
        Me.AddWaypointcmd.Name = "AddWaypointcmd"
        Me.AddWaypointcmd.Size = New System.Drawing.Size(53, 23)
        Me.AddWaypointcmd.TabIndex = 2
        Me.AddWaypointcmd.Text = "Add"
        Me.AddWaypointcmd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Type:"
        '
        'Typecmb
        '
        Me.Typecmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Typecmb.FormattingEnabled = True
        Me.Typecmb.Items.AddRange(New Object() {"Walk", "Stairs/Hole", "Rope", "Ladder", "Say", "Wait", "Sewer", "Shovel"})
        Me.Typecmb.Location = New System.Drawing.Point(9, 32)
        Me.Typecmb.Name = "Typecmb"
        Me.Typecmb.Size = New System.Drawing.Size(131, 21)
        Me.Typecmb.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Waypointslst)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 123)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(146, 136)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Waypoints"
        '
        'Waypointslst
        '
        Me.Waypointslst.FormattingEnabled = True
        Me.Waypointslst.Location = New System.Drawing.Point(6, 19)
        Me.Waypointslst.Name = "Waypointslst"
        Me.Waypointslst.Size = New System.Drawing.Size(134, 108)
        Me.Waypointslst.TabIndex = 0
        '
        'Direction
        '
        Me.Direction.Controls.Add(Me.dRight)
        Me.Direction.Controls.Add(Me.dDown)
        Me.Direction.Controls.Add(Me.dLeft)
        Me.Direction.Controls.Add(Me.dUp)
        Me.Direction.Location = New System.Drawing.Point(167, 12)
        Me.Direction.Name = "Direction"
        Me.Direction.Size = New System.Drawing.Size(123, 105)
        Me.Direction.TabIndex = 2
        Me.Direction.TabStop = False
        Me.Direction.Text = "Select Direction"
        Me.Direction.Visible = False
        '
        'dRight
        '
        Me.dRight.Appearance = System.Windows.Forms.Appearance.Button
        Me.dRight.AutoSize = True
        Me.dRight.Location = New System.Drawing.Point(73, 42)
        Me.dRight.Name = "dRight"
        Me.dRight.Size = New System.Drawing.Size(23, 23)
        Me.dRight.TabIndex = 3
        Me.dRight.TabStop = True
        Me.dRight.Text = ">"
        Me.dRight.UseVisualStyleBackColor = True
        '
        'dDown
        '
        Me.dDown.Appearance = System.Windows.Forms.Appearance.Button
        Me.dDown.AutoSize = True
        Me.dDown.Location = New System.Drawing.Point(43, 68)
        Me.dDown.Name = "dDown"
        Me.dDown.Size = New System.Drawing.Size(27, 23)
        Me.dDown.TabIndex = 2
        Me.dDown.TabStop = True
        Me.dDown.Text = "\/"
        Me.dDown.UseVisualStyleBackColor = True
        '
        'dLeft
        '
        Me.dLeft.Appearance = System.Windows.Forms.Appearance.Button
        Me.dLeft.AutoSize = True
        Me.dLeft.Location = New System.Drawing.Point(18, 42)
        Me.dLeft.Name = "dLeft"
        Me.dLeft.Size = New System.Drawing.Size(23, 23)
        Me.dLeft.TabIndex = 1
        Me.dLeft.TabStop = True
        Me.dLeft.Text = "<"
        Me.dLeft.UseVisualStyleBackColor = True
        '
        'dUp
        '
        Me.dUp.Appearance = System.Windows.Forms.Appearance.Button
        Me.dUp.AutoSize = True
        Me.dUp.Location = New System.Drawing.Point(43, 17)
        Me.dUp.Name = "dUp"
        Me.dUp.Size = New System.Drawing.Size(27, 23)
        Me.dUp.TabIndex = 0
        Me.dUp.TabStop = True
        Me.dUp.Text = "/\"
        Me.dUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.dUp.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Loadcmd)
        Me.GroupBox2.Controls.Add(Me.Savecmd)
        Me.GroupBox2.Controls.Add(Me.WPClearcmd)
        Me.GroupBox2.Controls.Add(Me.WPDeletecmd)
        Me.GroupBox2.Location = New System.Drawing.Point(172, 123)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(118, 135)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Manage Waypoints"
        '
        'Loadcmd
        '
        Me.Loadcmd.Location = New System.Drawing.Point(17, 102)
        Me.Loadcmd.Name = "Loadcmd"
        Me.Loadcmd.Size = New System.Drawing.Size(82, 22)
        Me.Loadcmd.TabIndex = 3
        Me.Loadcmd.Text = "Load"
        Me.Loadcmd.UseVisualStyleBackColor = True
        '
        'Savecmd
        '
        Me.Savecmd.Location = New System.Drawing.Point(17, 74)
        Me.Savecmd.Name = "Savecmd"
        Me.Savecmd.Size = New System.Drawing.Size(82, 22)
        Me.Savecmd.TabIndex = 2
        Me.Savecmd.Text = "Save"
        Me.Savecmd.UseVisualStyleBackColor = True
        '
        'WPClearcmd
        '
        Me.WPClearcmd.Location = New System.Drawing.Point(17, 46)
        Me.WPClearcmd.Name = "WPClearcmd"
        Me.WPClearcmd.Size = New System.Drawing.Size(82, 22)
        Me.WPClearcmd.TabIndex = 1
        Me.WPClearcmd.Text = "Clear"
        Me.WPClearcmd.UseVisualStyleBackColor = True
        '
        'WPDeletecmd
        '
        Me.WPDeletecmd.Location = New System.Drawing.Point(17, 19)
        Me.WPDeletecmd.Name = "WPDeletecmd"
        Me.WPDeletecmd.Size = New System.Drawing.Size(82, 21)
        Me.WPDeletecmd.TabIndex = 0
        Me.WPDeletecmd.Text = "Delete"
        Me.WPDeletecmd.UseVisualStyleBackColor = True
        '
        'Infobox
        '
        Me.Infobox.Controls.Add(Me.Label2)
        Me.Infobox.Controls.Add(Me.Infotxt)
        Me.Infobox.Location = New System.Drawing.Point(167, 12)
        Me.Infobox.Name = "Infobox"
        Me.Infobox.Size = New System.Drawing.Size(123, 105)
        Me.Infobox.TabIndex = 4
        Me.Infobox.TabStop = False
        Me.Infobox.Text = "Say"
        Me.Infobox.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Text to Say:"
        '
        'Infotxt
        '
        Me.Infotxt.Location = New System.Drawing.Point(8, 45)
        Me.Infotxt.Name = "Infotxt"
        Me.Infotxt.Size = New System.Drawing.Size(111, 20)
        Me.Infotxt.TabIndex = 0
        '
        'Waitbox
        '
        Me.Waitbox.Controls.Add(Me.Label3)
        Me.Waitbox.Controls.Add(Me.Waittxt)
        Me.Waitbox.Location = New System.Drawing.Point(167, 12)
        Me.Waitbox.Name = "Waitbox"
        Me.Waitbox.Size = New System.Drawing.Size(123, 105)
        Me.Waitbox.TabIndex = 2
        Me.Waitbox.TabStop = False
        Me.Waitbox.Text = "Wait"
        Me.Waitbox.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Seconds to Wait:"
        '
        'Waittxt
        '
        Me.Waittxt.Location = New System.Drawing.Point(28, 45)
        Me.Waittxt.Name = "Waittxt"
        Me.Waittxt.Size = New System.Drawing.Size(68, 20)
        Me.Waittxt.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RemoveMonster)
        Me.GroupBox3.Controls.Add(Me.AddMonster)
        Me.GroupBox3.Controls.Add(Me.MonsterList)
        Me.GroupBox3.Controls.Add(Me.EnableMonsterList)
        Me.GroupBox3.Controls.Add(Me.EatFromCorpses)
        Me.GroupBox3.Controls.Add(Me.LootMinimumCap)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.LootFromCorpses)
        Me.GroupBox3.Location = New System.Drawing.Point(302, 11)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(136, 248)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cavebot Options"
        '
        'RemoveMonster
        '
        Me.RemoveMonster.Location = New System.Drawing.Point(70, 218)
        Me.RemoveMonster.Name = "RemoveMonster"
        Me.RemoveMonster.Size = New System.Drawing.Size(59, 24)
        Me.RemoveMonster.TabIndex = 7
        Me.RemoveMonster.Text = "Remove"
        Me.RemoveMonster.UseVisualStyleBackColor = True
        '
        'AddMonster
        '
        Me.AddMonster.Location = New System.Drawing.Point(6, 218)
        Me.AddMonster.Name = "AddMonster"
        Me.AddMonster.Size = New System.Drawing.Size(59, 24)
        Me.AddMonster.TabIndex = 6
        Me.AddMonster.Text = "Add"
        Me.AddMonster.UseVisualStyleBackColor = True
        '
        'MonsterList
        '
        Me.MonsterList.Enabled = False
        Me.MonsterList.FormattingEnabled = True
        Me.MonsterList.Location = New System.Drawing.Point(6, 131)
        Me.MonsterList.Name = "MonsterList"
        Me.MonsterList.Size = New System.Drawing.Size(124, 82)
        Me.MonsterList.TabIndex = 5
        '
        'EnableMonsterList
        '
        Me.EnableMonsterList.Location = New System.Drawing.Point(6, 94)
        Me.EnableMonsterList.Name = "EnableMonsterList"
        Me.EnableMonsterList.Size = New System.Drawing.Size(114, 31)
        Me.EnableMonsterList.TabIndex = 4
        Me.EnableMonsterList.Text = "Attack Only Monsters In List"
        Me.EnableMonsterList.UseVisualStyleBackColor = True
        '
        'EatFromCorpses
        '
        Me.EatFromCorpses.AutoSize = True
        Me.EatFromCorpses.Location = New System.Drawing.Point(6, 19)
        Me.EatFromCorpses.Name = "EatFromCorpses"
        Me.EatFromCorpses.Size = New System.Drawing.Size(109, 17)
        Me.EatFromCorpses.TabIndex = 3
        Me.EatFromCorpses.Text = "Eat From Corpses"
        Me.EatFromCorpses.UseVisualStyleBackColor = True
        '
        'LootMinimumCap
        '
        Me.LootMinimumCap.Increment = New Decimal(New Integer() {0, 0, 0, 0})
        Me.LootMinimumCap.Location = New System.Drawing.Point(88, 67)
        Me.LootMinimumCap.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.LootMinimumCap.Name = "LootMinimumCap"
        Me.LootMinimumCap.Size = New System.Drawing.Size(41, 20)
        Me.LootMinimumCap.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Minimum Cap.:"
        '
        'LootFromCorpses
        '
        Me.LootFromCorpses.AutoSize = True
        Me.LootFromCorpses.Location = New System.Drawing.Point(6, 43)
        Me.LootFromCorpses.Name = "LootFromCorpses"
        Me.LootFromCorpses.Size = New System.Drawing.Size(114, 17)
        Me.LootFromCorpses.TabIndex = 0
        Me.LootFromCorpses.Text = "Loot From Corpses"
        Me.LootFromCorpses.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(373, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "To start the cavebot, type ""&&cavebot on"" in the console (Without the quotes)."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 278)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(357, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "To start the walker, type ""&&walker on"" in the console (Without the quotes)."
        '
        'frmCavebot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(450, 300)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Waitbox)
        Me.Controls.Add(Me.Infobox)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Direction)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.AddWp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb2
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCavebot"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Cavebot/Walker"
        Me.AddWp.ResumeLayout(False)
        Me.AddWp.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Direction.ResumeLayout(False)
        Me.Direction.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.Infobox.ResumeLayout(False)
        Me.Infobox.PerformLayout()
        Me.Waitbox.ResumeLayout(False)
        Me.Waitbox.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.LootMinimumCap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AddWp As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Typecmb As System.Windows.Forms.ComboBox
    Friend WithEvents Waypointslst As System.Windows.Forms.ListBox
    Friend WithEvents AddWaypointcmd As System.Windows.Forms.Button
    Friend WithEvents Direction As System.Windows.Forms.GroupBox
    Friend WithEvents dRight As System.Windows.Forms.RadioButton
    Friend WithEvents dDown As System.Windows.Forms.RadioButton
    Friend WithEvents dLeft As System.Windows.Forms.RadioButton
    Friend WithEvents dUp As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents WPDeletecmd As System.Windows.Forms.Button
    Friend WithEvents WPClearcmd As System.Windows.Forms.Button
    Friend WithEvents Infobox As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Infotxt As System.Windows.Forms.TextBox
    Friend WithEvents Waitbox As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Waittxt As System.Windows.Forms.TextBox
    Friend WithEvents Loadcmd As System.Windows.Forms.Button
    Friend WithEvents Savecmd As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents LootMinimumCap As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LootFromCorpses As System.Windows.Forms.CheckBox
    Friend WithEvents EatFromCorpses As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents EnableMonsterList As System.Windows.Forms.CheckBox
    Friend WithEvents RemoveMonster As System.Windows.Forms.Button
    Friend WithEvents AddMonster As System.Windows.Forms.Button
    Friend WithEvents MonsterList As System.Windows.Forms.ListBox
End Class
