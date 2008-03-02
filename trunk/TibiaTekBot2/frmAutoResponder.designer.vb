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
        Me.BgWorker = New System.ComponentModel.BackgroundWorker
        Me.LblChkDist = New System.Windows.Forms.Label
        Me.CntChkDist = New System.Windows.Forms.NumericUpDown
        Me.ChkDistance = New System.Windows.Forms.CheckBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RighClick.SuspendLayout()
        CType(Me.CntChkDist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.SaddleBrown
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OnMessage, Me.RespondWith, Me.WaitSeconds, Me.ExpressionType, Me.MsgType})
        Me.DataGridView1.ContextMenuStrip = Me.RighClick
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
        Me.RespondWith.Width = 245
        '
        'WaitSeconds
        '
        Me.WaitSeconds.HeaderText = "Wait"
        Me.WaitSeconds.Name = "WaitSeconds"
        Me.WaitSeconds.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WaitSeconds.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.WaitSeconds.ToolTipText = "Type The Seconds for wait to send the answer"
        Me.WaitSeconds.Width = 30
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
        Me.LoadBtn.Location = New System.Drawing.Point(3, 221)
        Me.LoadBtn.Name = "LoadBtn"
        Me.LoadBtn.Size = New System.Drawing.Size(80, 25)
        Me.LoadBtn.TabIndex = 2
        Me.LoadBtn.Text = "Load"
        Me.LoadBtn.UseVisualStyleBackColor = True
        '
        'SaveBtn
        '
        Me.SaveBtn.Location = New System.Drawing.Point(101, 221)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(80, 25)
        Me.SaveBtn.TabIndex = 3
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'ClearBtn
        '
        Me.ClearBtn.Location = New System.Drawing.Point(199, 221)
        Me.ClearBtn.Name = "ClearBtn"
        Me.ClearBtn.Size = New System.Drawing.Size(80, 25)
        Me.ClearBtn.TabIndex = 4
        Me.ClearBtn.Text = "Clear"
        Me.ClearBtn.UseVisualStyleBackColor = True
        '
        'ActivateBtn
        '
        Me.ActivateBtn.Location = New System.Drawing.Point(297, 221)
        Me.ActivateBtn.Name = "ActivateBtn"
        Me.ActivateBtn.Size = New System.Drawing.Size(80, 25)
        Me.ActivateBtn.TabIndex = 1
        Me.ActivateBtn.Text = "Activate"
        Me.ActivateBtn.UseVisualStyleBackColor = True
        '
        'LblChkDist
        '
        Me.LblChkDist.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor
        Me.LblChkDist.AutoSize = True
        Me.LblChkDist.Location = New System.Drawing.Point(547, 225)
        Me.LblChkDist.Name = "LblChkDist"
        Me.LblChkDist.Size = New System.Drawing.Size(78, 13)
        Me.LblChkDist.TabIndex = 9
        Me.LblChkDist.Text = "Max. Distance:"
        '
        'CntChkDist
        '
        Me.CntChkDist.Location = New System.Drawing.Point(631, 221)
        Me.CntChkDist.Name = "CntChkDist"
        Me.CntChkDist.Size = New System.Drawing.Size(47, 20)
        Me.CntChkDist.TabIndex = 10
        '
        'ChkDistance
        '
        Me.ChkDistance.AutoSize = True
        Me.ChkDistance.Location = New System.Drawing.Point(528, 226)
        Me.ChkDistance.Name = "ChkDistance"
        Me.ChkDistance.Size = New System.Drawing.Size(13, 12)
        Me.ChkDistance.TabIndex = 11
        Me.ChkDistance.UseVisualStyleBackColor = True
        '
        'frmAutoResponder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 253)
        Me.Controls.Add(Me.CntChkDist)
        Me.Controls.Add(Me.LblChkDist)
        Me.Controls.Add(Me.ChkDistance)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LoadBtn)
        Me.Controls.Add(Me.ClearBtn)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.ActivateBtn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAutoResponder"
        Me.Text = "Auto Responder"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RighClick.ResumeLayout(False)
        CType(Me.CntChkDist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RighClick As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddAnswerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAnswerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadBtn As System.Windows.Forms.Button
    Friend WithEvents SaveBtn As System.Windows.Forms.Button
    Friend WithEvents ClearBtn As System.Windows.Forms.Button
    Friend WithEvents ActivateBtn As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents OnMessage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RespondWith As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents WaitSeconds As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpressionType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents MsgType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents LblChkDist As System.Windows.Forms.Label
    Friend WithEvents CntChkDist As System.Windows.Forms.NumericUpDown
    Friend WithEvents ChkDistance As System.Windows.Forms.CheckBox

End Class
