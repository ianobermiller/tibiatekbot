<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectClient
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
        Me.ExistingTibiaClientGroupBox = New System.Windows.Forms.GroupBox
        Me.ExistingClients = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.NewTibiaRadioButton = New System.Windows.Forms.RadioButton
        Me.ExistingTibiaRadioButton = New System.Windows.Forms.RadioButton
        Me.OKButton = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.ExistingTibiaClientGroupBox.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExistingTibiaClientGroupBox
        '
        Me.ExistingTibiaClientGroupBox.Controls.Add(Me.ExistingClients)
        Me.ExistingTibiaClientGroupBox.Location = New System.Drawing.Point(12, 65)
        Me.ExistingTibiaClientGroupBox.Name = "ExistingTibiaClientGroupBox"
        Me.ExistingTibiaClientGroupBox.Size = New System.Drawing.Size(285, 52)
        Me.ExistingTibiaClientGroupBox.TabIndex = 0
        Me.ExistingTibiaClientGroupBox.TabStop = False
        Me.ExistingTibiaClientGroupBox.Text = "Existing Tibia Clients"
        '
        'ExistingClients
        '
        Me.ExistingClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ExistingClients.FormattingEnabled = True
        Me.ExistingClients.Location = New System.Drawing.Point(6, 19)
        Me.ExistingClients.Name = "ExistingClients"
        Me.ExistingClients.Size = New System.Drawing.Size(273, 21)
        Me.ExistingClients.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.NewTibiaRadioButton)
        Me.GroupBox2.Controls.Add(Me.ExistingTibiaRadioButton)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(284, 53)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Start Up Method"
        '
        'NewTibiaRadioButton
        '
        Me.NewTibiaRadioButton.AutoSize = True
        Me.NewTibiaRadioButton.Enabled = False
        Me.NewTibiaRadioButton.Location = New System.Drawing.Point(147, 19)
        Me.NewTibiaRadioButton.Name = "NewTibiaRadioButton"
        Me.NewTibiaRadioButton.Size = New System.Drawing.Size(102, 17)
        Me.NewTibiaRadioButton.TabIndex = 1
        Me.NewTibiaRadioButton.Text = "New Tibia Client"
        Me.NewTibiaRadioButton.UseVisualStyleBackColor = True
        '
        'ExistingTibiaRadioButton
        '
        Me.ExistingTibiaRadioButton.AutoSize = True
        Me.ExistingTibiaRadioButton.Checked = True
        Me.ExistingTibiaRadioButton.Location = New System.Drawing.Point(6, 19)
        Me.ExistingTibiaRadioButton.Name = "ExistingTibiaRadioButton"
        Me.ExistingTibiaRadioButton.Size = New System.Drawing.Size(116, 17)
        Me.ExistingTibiaRadioButton.TabIndex = 0
        Me.ExistingTibiaRadioButton.TabStop = True
        Me.ExistingTibiaRadioButton.Text = "Existing Tibia Client"
        Me.ExistingTibiaRadioButton.UseVisualStyleBackColor = True
        '
        'OKButton
        '
        Me.OKButton.Enabled = False
        Me.OKButton.Location = New System.Drawing.Point(12, 123)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 23)
        Me.OKButton.TabIndex = 2
        Me.OKButton.Text = "&OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(222, 123)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "&Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmSelectClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(306, 154)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.ExistingTibiaClientGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = Global.TibiaTekBot3.My.Resources.Resources.ttbv3
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSelectClient"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TibiaTek Bot - Select A Tibia Client"
        Me.TopMost = True
        Me.ExistingTibiaClientGroupBox.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ExistingTibiaClientGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ExistingClients As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents NewTibiaRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ExistingTibiaRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
