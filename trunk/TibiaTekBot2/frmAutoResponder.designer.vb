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
Partial Class frmAutoResponder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoResponder))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.OnMessage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.RespondWith = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.WaitSeconds = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExpressionType = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.MsgType = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.RighClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddAnswerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteAnswerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadBtn = New System.Windows.Forms.Button
        Me.SaveBtn = New System.Windows.Forms.Button
        Me.ClearBtn = New System.Windows.Forms.Button
        Me.ActivateBtn = New System.Windows.Forms.Button
        Me.LblChkDist = New System.Windows.Forms.Label
        Me.ChkDistance = New System.Windows.Forms.CheckBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusLblHelp = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CntChkDist = New System.Windows.Forms.TextBox
        Me.Rmlbl = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SqrLbl = New System.Windows.Forms.Label
        Me.RmtimeTxt = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TestBtn = New System.Windows.Forms.Button
        Me.TestRitchtext = New System.Windows.Forms.RichTextBox
        Me.TestTxt = New System.Windows.Forms.TextBox
        Me.TestingModeGroup = New System.Windows.Forms.GroupBox
        Me.TestHideBtn = New System.Windows.Forms.Button
        Me.TestMsgTypeCmb = New System.Windows.Forms.ComboBox
        Me.TimerChk = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RighClick.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TestingModeGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OnMessage, Me.RespondWith, Me.WaitSeconds, Me.ExpressionType, Me.MsgType})
        Me.DataGridView1.ContextMenuStrip = Me.RighClick
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.GridColor = System.Drawing.Color.CornflowerBlue
        Me.DataGridView1.Location = New System.Drawing.Point(3, 4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(678, 205)
        Me.DataGridView1.TabIndex = 0
        '
        'OnMessage
        '
        Me.OnMessage.HeaderText = "If you receive this message"
        Me.OnMessage.Name = "OnMessage"
        Me.OnMessage.Width = 200
        '
        'RespondWith
        '
        Me.RespondWith.HeaderText = "Reply with any of these responses"
        Me.RespondWith.Name = "RespondWith"
        Me.RespondWith.ToolTipText = "Tip: Right clic in the selected row to add/delete Responses, also Delete rows"
        Me.RespondWith.Width = 243
        '
        'WaitSeconds
        '
        Me.WaitSeconds.HeaderText = "Wait"
        Me.WaitSeconds.Name = "WaitSeconds"
        Me.WaitSeconds.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WaitSeconds.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.WaitSeconds.ToolTipText = "Type The Seconds for wait to send the answer"
        Me.WaitSeconds.Width = 32
        '
        'ExpressionType
        '
        Me.ExpressionType.HeaderText = "Exp. Type"
        Me.ExpressionType.Items.AddRange(New Object() {"RegExp", "Normal text"})
        Me.ExpressionType.Name = "ExpressionType"
        Me.ExpressionType.Width = 70
        '
        'MsgType
        '
        Me.MsgType.HeaderText = "Msg. Type"
        Me.MsgType.Items.AddRange(New Object() {"Private", "Default", "Private or Default"})
        Me.MsgType.Name = "MsgType"
        Me.MsgType.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MsgType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.MsgType.Width = 90
        '
        'RighClick
        '
        Me.RighClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddAnswerToolStripMenuItem, Me.DeleteAnswerToolStripMenuItem, Me.ToolStripSeparator1, Me.DeleteRowToolStripMenuItem})
        Me.RighClick.Name = "RighClick"
        Me.RighClick.Size = New System.Drawing.Size(164, 76)
        Me.RighClick.Text = "RightClick"
        '
        'AddAnswerToolStripMenuItem
        '
        Me.AddAnswerToolStripMenuItem.Name = "AddAnswerToolStripMenuItem"
        Me.AddAnswerToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.AddAnswerToolStripMenuItem.Text = "Add response"
        '
        'DeleteAnswerToolStripMenuItem
        '
        Me.DeleteAnswerToolStripMenuItem.Name = "DeleteAnswerToolStripMenuItem"
        Me.DeleteAnswerToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.DeleteAnswerToolStripMenuItem.Text = "Delete response"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(160, 6)
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.DeleteRowToolStripMenuItem.Text = "Delete row"
        '
        'LoadBtn
        '
        Me.LoadBtn.Location = New System.Drawing.Point(5, 15)
        Me.LoadBtn.Name = "LoadBtn"
        Me.LoadBtn.Size = New System.Drawing.Size(64, 25)
        Me.LoadBtn.TabIndex = 2
        Me.LoadBtn.Text = "Load"
        Me.LoadBtn.UseVisualStyleBackColor = True
        '
        'SaveBtn
        '
        Me.SaveBtn.Location = New System.Drawing.Point(75, 15)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(64, 25)
        Me.SaveBtn.TabIndex = 3
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'ClearBtn
        '
        Me.ClearBtn.Location = New System.Drawing.Point(145, 15)
        Me.ClearBtn.Name = "ClearBtn"
        Me.ClearBtn.Size = New System.Drawing.Size(64, 25)
        Me.ClearBtn.TabIndex = 4
        Me.ClearBtn.Text = "Clear"
        Me.ClearBtn.UseVisualStyleBackColor = True
        '
        'ActivateBtn
        '
        Me.ActivateBtn.Location = New System.Drawing.Point(290, 15)
        Me.ActivateBtn.Name = "ActivateBtn"
        Me.ActivateBtn.Size = New System.Drawing.Size(64, 25)
        Me.ActivateBtn.TabIndex = 1
        Me.ActivateBtn.Text = "Activate"
        Me.ActivateBtn.UseVisualStyleBackColor = True
        '
        'LblChkDist
        '
        Me.LblChkDist.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.LblChkDist.AutoSize = True
        Me.LblChkDist.Location = New System.Drawing.Point(18, 21)
        Me.LblChkDist.Name = "LblChkDist"
        Me.LblChkDist.Size = New System.Drawing.Size(57, 13)
        Me.LblChkDist.TabIndex = 9
        Me.LblChkDist.Text = "Max. Dist.:"
        '
        'ChkDistance
        '
        Me.ChkDistance.AutoSize = True
        Me.ChkDistance.Location = New System.Drawing.Point(5, 22)
        Me.ChkDistance.Name = "ChkDistance"
        Me.ChkDistance.Size = New System.Drawing.Size(13, 12)
        Me.ChkDistance.TabIndex = 11
        Me.ChkDistance.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLblHelp})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 390)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(684, 22)
        Me.StatusStrip1.TabIndex = 12
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StatusLblHelp
        '
        Me.StatusLblHelp.Name = "StatusLblHelp"
        Me.StatusLblHelp.Size = New System.Drawing.Size(0, 17)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CntChkDist)
        Me.GroupBox1.Controls.Add(Me.Rmlbl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.SqrLbl)
        Me.GroupBox1.Controls.Add(Me.ChkDistance)
        Me.GroupBox1.Controls.Add(Me.LblChkDist)
        Me.GroupBox1.Controls.Add(Me.RmtimeTxt)
        Me.GroupBox1.Location = New System.Drawing.Point(369, 216)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(311, 45)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Other options"
        '
        'CntChkDist
        '
        Me.CntChkDist.Location = New System.Drawing.Point(74, 18)
        Me.CntChkDist.Name = "CntChkDist"
        Me.CntChkDist.Size = New System.Drawing.Size(32, 20)
        Me.CntChkDist.TabIndex = 16
        Me.CntChkDist.Text = "0"
        '
        'Rmlbl
        '
        Me.Rmlbl.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.Rmlbl.AutoSize = True
        Me.Rmlbl.Location = New System.Drawing.Point(142, 21)
        Me.Rmlbl.Name = "Rmlbl"
        Me.Rmlbl.Size = New System.Drawing.Size(120, 13)
        Me.Rmlbl.TabIndex = 15
        Me.Rmlbl.Text = "Time for repeated msgs:"
        '
        'Label2
        '
        Me.Label2.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label2.Location = New System.Drawing.Point(129, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 18)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "|"
        '
        'SqrLbl
        '
        Me.SqrLbl.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.SqrLbl.AutoSize = True
        Me.SqrLbl.Location = New System.Drawing.Point(106, 21)
        Me.SqrLbl.Name = "SqrLbl"
        Me.SqrLbl.Size = New System.Drawing.Size(23, 13)
        Me.SqrLbl.TabIndex = 13
        Me.SqrLbl.Text = "Sqr"
        '
        'RmtimeTxt
        '
        Me.RmtimeTxt.Location = New System.Drawing.Point(265, 17)
        Me.RmtimeTxt.Name = "RmtimeTxt"
        Me.RmtimeTxt.Size = New System.Drawing.Size(38, 20)
        Me.RmtimeTxt.TabIndex = 12
        Me.RmtimeTxt.Text = "60"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TestBtn)
        Me.GroupBox2.Controls.Add(Me.LoadBtn)
        Me.GroupBox2.Controls.Add(Me.ClearBtn)
        Me.GroupBox2.Controls.Add(Me.SaveBtn)
        Me.GroupBox2.Controls.Add(Me.ActivateBtn)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 216)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 46)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Actions"
        '
        'TestBtn
        '
        Me.TestBtn.Location = New System.Drawing.Point(218, 15)
        Me.TestBtn.Name = "TestBtn"
        Me.TestBtn.Size = New System.Drawing.Size(64, 25)
        Me.TestBtn.TabIndex = 5
        Me.TestBtn.Text = "Test"
        Me.TestBtn.UseVisualStyleBackColor = True
        '
        'TestRitchtext
        '
        Me.TestRitchtext.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TestRitchtext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestRitchtext.ForeColor = System.Drawing.SystemColors.Info
        Me.TestRitchtext.Location = New System.Drawing.Point(7, 18)
        Me.TestRitchtext.Name = "TestRitchtext"
        Me.TestRitchtext.Size = New System.Drawing.Size(664, 72)
        Me.TestRitchtext.TabIndex = 15
        Me.TestRitchtext.Text = ""
        '
        'TestTxt
        '
        Me.TestTxt.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.TestTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TestTxt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TestTxt.Location = New System.Drawing.Point(7, 93)
        Me.TestTxt.Name = "TestTxt"
        Me.TestTxt.Size = New System.Drawing.Size(503, 20)
        Me.TestTxt.TabIndex = 16
        '
        'TestingModeGroup
        '
        Me.TestingModeGroup.Controls.Add(Me.TestHideBtn)
        Me.TestingModeGroup.Controls.Add(Me.TestMsgTypeCmb)
        Me.TestingModeGroup.Controls.Add(Me.TestTxt)
        Me.TestingModeGroup.Controls.Add(Me.TestRitchtext)
        Me.TestingModeGroup.Location = New System.Drawing.Point(3, 272)
        Me.TestingModeGroup.Name = "TestingModeGroup"
        Me.TestingModeGroup.Size = New System.Drawing.Size(677, 118)
        Me.TestingModeGroup.TabIndex = 19
        Me.TestingModeGroup.TabStop = False
        Me.TestingModeGroup.Text = "Testing Mode"
        '
        'TestHideBtn
        '
        Me.TestHideBtn.Location = New System.Drawing.Point(617, 92)
        Me.TestHideBtn.Name = "TestHideBtn"
        Me.TestHideBtn.Size = New System.Drawing.Size(53, 20)
        Me.TestHideBtn.TabIndex = 18
        Me.TestHideBtn.Text = "Exit"
        Me.TestHideBtn.UseVisualStyleBackColor = True
        '
        'TestMsgTypeCmb
        '
        Me.TestMsgTypeCmb.FormattingEnabled = True
        Me.TestMsgTypeCmb.Items.AddRange(New Object() {"Private", "Default"})
        Me.TestMsgTypeCmb.Location = New System.Drawing.Point(516, 91)
        Me.TestMsgTypeCmb.Name = "TestMsgTypeCmb"
        Me.TestMsgTypeCmb.Size = New System.Drawing.Size(96, 21)
        Me.TestMsgTypeCmb.TabIndex = 17
        Me.TestMsgTypeCmb.Text = "Private"
        '
        'TimerChk
        '
        Me.TimerChk.Enabled = True
        Me.TimerChk.Interval = 300
        '
        'frmAutoResponder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 412)
        Me.Controls.Add(Me.TestingModeGroup)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAutoResponder"
        Me.Text = "Auto Responder"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RighClick.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TestingModeGroup.ResumeLayout(False)
        Me.TestingModeGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Public WithEvents RighClick As System.Windows.Forms.ContextMenuStrip
    Public WithEvents AddAnswerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents DeleteAnswerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents LoadBtn As System.Windows.Forms.Button
    Public WithEvents SaveBtn As System.Windows.Forms.Button
    Public WithEvents ClearBtn As System.Windows.Forms.Button
    Public WithEvents ActivateBtn As System.Windows.Forms.Button
    Public WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents DeleteRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents LblChkDist As System.Windows.Forms.Label
    Public WithEvents ChkDistance As System.Windows.Forms.CheckBox
    Public WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Public WithEvents StatusLblHelp As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents TestBtn As System.Windows.Forms.Button
    Public WithEvents TestRitchtext As System.Windows.Forms.RichTextBox
    Public WithEvents TestTxt As System.Windows.Forms.TextBox
    Public WithEvents TestingModeGroup As System.Windows.Forms.GroupBox
    Public WithEvents SqrLbl As System.Windows.Forms.Label
    Public WithEvents RmtimeTxt As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Rmlbl As System.Windows.Forms.Label
    Public WithEvents TestMsgTypeCmb As System.Windows.Forms.ComboBox
    Public WithEvents TestHideBtn As System.Windows.Forms.Button
    Public WithEvents CntChkDist As System.Windows.Forms.TextBox
    Friend WithEvents TimerChk As System.Windows.Forms.Timer
    Friend WithEvents OnMessage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RespondWith As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents WaitSeconds As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpressionType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents MsgType As System.Windows.Forms.DataGridViewComboBoxColumn

End Class
