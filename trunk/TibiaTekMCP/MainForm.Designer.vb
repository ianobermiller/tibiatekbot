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
'    Boston, MA 02111-1307, USA.Imports System.Math

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
        Me.OpenDialog = New System.Windows.Forms.OpenFileDialog
        Me.SaveDialog = New System.Windows.Forms.SaveFileDialog
        Me.BGW = New System.ComponentModel.BackgroundWorker
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.CloseButton = New System.Windows.Forms.Button
        Me.PatchButton = New System.Windows.Forms.Button
        Me.TibiaExecutableGroupBox = New System.Windows.Forms.GroupBox
        Me.BrowseButton = New System.Windows.Forms.Button
        Me.TibiaExecutableTextBox = New System.Windows.Forms.TextBox
        Me.TibiaExecutableGroupBox.SuspendLayout()
        Me.SuspendLayout()
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(12, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Credits:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(287, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Stiju"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(211, 100)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(70, 13)
        Me.LinkLabel2.TabIndex = 6
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.CreditsTS
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(60, 100)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(145, 13)
        Me.LinkLabel1.TabIndex = 5
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.CreditsTTDT
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
        'TibiaExecutableGroupBox
        '
        Me.TibiaExecutableGroupBox.Controls.Add(Me.BrowseButton)
        Me.TibiaExecutableGroupBox.Controls.Add(Me.TibiaExecutableTextBox)
        Me.TibiaExecutableGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.TibiaExecutableGroupBox.Name = "TibiaExecutableGroupBox"
        Me.TibiaExecutableGroupBox.Size = New System.Drawing.Size(313, 52)
        Me.TibiaExecutableGroupBox.TabIndex = 0
        Me.TibiaExecutableGroupBox.TabStop = False
        Me.TibiaExecutableGroupBox.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.TibiaExecutableText
        '
        'BrowseButton
        '
        Me.BrowseButton.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default, "BrowseText", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.BrowseButton.Location = New System.Drawing.Point(238, 19)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(69, 23)
        Me.BrowseButton.TabIndex = 1
        Me.BrowseButton.Text = Global.TibiaTek_Multi_Client_Patcher.My.MySettings.Default.BrowseText
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'TibiaExecutableTextBox
        '
        Me.TibiaExecutableTextBox.Location = New System.Drawing.Point(6, 19)
        Me.TibiaExecutableTextBox.Name = "TibiaExecutableTextBox"
        Me.TibiaExecutableTextBox.ReadOnly = True
        Me.TibiaExecutableTextBox.Size = New System.Drawing.Size(226, 20)
        Me.TibiaExecutableTextBox.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 122)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
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
        Me.PerformLayout()

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
