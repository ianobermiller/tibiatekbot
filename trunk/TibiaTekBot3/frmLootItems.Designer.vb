<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLootItems
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
        Me.Items = New System.Windows.Forms.ListBox
        Me.Loots = New System.Windows.Forms.ListBox
        Me.Add = New System.Windows.Forms.Button
        Me.Remove = New System.Windows.Forms.Button
        Me.Savecmd = New System.Windows.Forms.Button
        Me.Cancelcmd = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Items
        '
        Me.Items.FormattingEnabled = True
        Me.Items.Location = New System.Drawing.Point(12, 7)
        Me.Items.Name = "Items"
        Me.Items.Size = New System.Drawing.Size(130, 251)
        Me.Items.TabIndex = 0
        '
        'Loots
        '
        Me.Loots.FormattingEnabled = True
        Me.Loots.Location = New System.Drawing.Point(188, 7)
        Me.Loots.Name = "Loots"
        Me.Loots.Size = New System.Drawing.Size(130, 251)
        Me.Loots.TabIndex = 1
        '
        'Add
        '
        Me.Add.Location = New System.Drawing.Point(148, 87)
        Me.Add.Name = "Add"
        Me.Add.Size = New System.Drawing.Size(34, 23)
        Me.Add.TabIndex = 2
        Me.Add.Text = "->"
        Me.Add.UseVisualStyleBackColor = True
        '
        'Remove
        '
        Me.Remove.Location = New System.Drawing.Point(148, 116)
        Me.Remove.Name = "Remove"
        Me.Remove.Size = New System.Drawing.Size(34, 23)
        Me.Remove.TabIndex = 3
        Me.Remove.Text = "<-"
        Me.Remove.UseVisualStyleBackColor = True
        '
        'Savecmd
        '
        Me.Savecmd.Location = New System.Drawing.Point(89, 274)
        Me.Savecmd.Name = "Savecmd"
        Me.Savecmd.Size = New System.Drawing.Size(76, 26)
        Me.Savecmd.TabIndex = 4
        Me.Savecmd.Text = "Save"
        Me.Savecmd.UseVisualStyleBackColor = True
        '
        'Cancelcmd
        '
        Me.Cancelcmd.Location = New System.Drawing.Point(171, 274)
        Me.Cancelcmd.Name = "Cancelcmd"
        Me.Cancelcmd.Size = New System.Drawing.Size(76, 26)
        Me.Cancelcmd.TabIndex = 5
        Me.Cancelcmd.Text = "Cancel"
        Me.Cancelcmd.UseVisualStyleBackColor = True
        '
        'frmLootItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 312)
        Me.Controls.Add(Me.Cancelcmd)
        Me.Controls.Add(Me.Savecmd)
        Me.Controls.Add(Me.Remove)
        Me.Controls.Add(Me.Add)
        Me.Controls.Add(Me.Loots)
        Me.Controls.Add(Me.Items)
        Me.Name = "frmLootItems"
        Me.Text = "Select Items to Loot"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Items As System.Windows.Forms.ListBox
    Friend WithEvents Loots As System.Windows.Forms.ListBox
    Friend WithEvents Add As System.Windows.Forms.Button
    Friend WithEvents Remove As System.Windows.Forms.Button
    Friend WithEvents Savecmd As System.Windows.Forms.Button
    Friend WithEvents Cancelcmd As System.Windows.Forms.Button
End Class
