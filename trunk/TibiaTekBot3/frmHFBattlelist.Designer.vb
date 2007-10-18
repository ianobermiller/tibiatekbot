<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHFBattlelist
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
        Me.GetCharlb = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'GetCharlb
        '
        Me.GetCharlb.FormattingEnabled = True
        Me.GetCharlb.Location = New System.Drawing.Point(0, 0)
        Me.GetCharlb.Name = "GetCharlb"
        Me.GetCharlb.Size = New System.Drawing.Size(127, 147)
        Me.GetCharlb.TabIndex = 0
        '
        'frmHFBattlelist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(124, 145)
        Me.ControlBox = False
        Me.Controls.Add(Me.GetCharlb)
        Me.Name = "frmHFBattlelist"
        Me.ShowIcon = False
        Me.Text = "Select Character"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GetCharlb As System.Windows.Forms.ListBox
End Class
