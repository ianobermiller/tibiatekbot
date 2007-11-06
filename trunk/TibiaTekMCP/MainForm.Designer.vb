<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.TibiaExecutableGroupBox = New System.Windows.Forms.GroupBox
        Me.BrowseButton = New System.Windows.Forms.Button
        Me.TibiaExecutableTextBox = New System.Windows.Forms.TextBox
        Me.OpenDialog = New System.Windows.Forms.OpenFileDialog
        Me.SaveDialog = New System.Windows.Forms.SaveFileDialog
        Me.CloseButton = New System.Windows.Forms.Button
        Me.PatchButton = New System.Windows.Forms.Button
        Me.BGW = New System.ComponentModel.BackgroundWorker
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.TibiaExecutableGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'TibiaExecutableGroupBox
        '
        Me.TibiaExecutableGroupBox.Controls.Add(Me.BrowseButton)
        Me.TibiaExecutableGroupBox.Controls.Add(Me.TibiaExecutableTextBox)
        Me.TibiaExecutableGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.TibiaExecutableGroupBox.Name = "TibiaExecutableGroupBox"
        Me.TibiaExecutableGroupBox.Size = New System.Drawing.Size(313, 52)
        Me.TibiaExecutableGroupBox.TabIndex = 0
        Me.TibiaExecutableGroupBox.TabStop = False
        Me.TibiaExecutableGroupBox.Text = "Tibia Executable"
        '
        'BrowseButton
        '
        Me.BrowseButton.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default, "BrowseText", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.BrowseButton.Location = New System.Drawing.Point(255, 19)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(52, 20)
        Me.BrowseButton.TabIndex = 1
        Me.BrowseButton.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.BrowseText
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'TibiaExecutableTextBox
        '
        Me.TibiaExecutableTextBox.Location = New System.Drawing.Point(6, 19)
        Me.TibiaExecutableTextBox.Name = "TibiaExecutableTextBox"
        Me.TibiaExecutableTextBox.ReadOnly = True
        Me.TibiaExecutableTextBox.Size = New System.Drawing.Size(243, 20)
        Me.TibiaExecutableTextBox.TabIndex = 0
        '
        'OpenDialog
        '
        Me.OpenDialog.DefaultExt = "exe"
        Me.OpenDialog.Filter = "Executable|*.exe"
        Me.OpenDialog.InitialDirectory = "C:\Program Files\Tibia"
        '
        'SaveDialog
        '
        Me.SaveDialog.DefaultExt = "exe"
        Me.SaveDialog.FileName = "Tibia.exe"
        Me.SaveDialog.Filter = "Executable|*.exe"
        '
        'CloseButton
        '
        Me.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CloseButton.Location = New System.Drawing.Point(250, 70)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(75, 23)
        Me.CloseButton.TabIndex = 2
        Me.CloseButton.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.CloseText
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'PatchButton
        '
        Me.PatchButton.Enabled = False
        Me.PatchButton.Location = New System.Drawing.Point(12, 70)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(75, 23)
        Me.PatchButton.TabIndex = 1
        Me.PatchButton.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.PatchText
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'BGW
        '
        Me.BGW.WorkerReportsProgress = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(93, 70)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(151, 19)
        Me.ProgressBar1.TabIndex = 3
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 101)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.PatchButton)
        Me.Controls.Add(Me.TibiaExecutableGroupBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Global.TibiaTek_Multi_Client_Patcher.My.Resources.Resources.ttmcp10
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TibiaTek Multi-Client Patcher v1.0"
        Me.TibiaExecutableGroupBox.ResumeLayout(False)
        Me.TibiaExecutableGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TibiaExecutableGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents TibiaExecutableTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OpenDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PatchButton As System.Windows.Forms.Button
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents SaveDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BGW As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
