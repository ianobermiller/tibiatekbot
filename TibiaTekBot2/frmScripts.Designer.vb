<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScripts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScripts))
        Me.ScriptsView = New System.Windows.Forms.DataGridView
        Me.ScriptStatus = New System.Windows.Forms.DataGridViewImageColumn
        Me.Filename = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ResumeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectedToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenScriptDialog = New System.Windows.Forms.OpenFileDialog
        CType(Me.ScriptsView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ScriptsView
        '
        Me.ScriptsView.AllowUserToAddRows = False
        Me.ScriptsView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScriptsView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.ScriptsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ScriptsView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ScriptStatus, Me.Filename})
        Me.ScriptsView.Location = New System.Drawing.Point(12, 43)
        Me.ScriptsView.Name = "ScriptsView"
        Me.ScriptsView.ReadOnly = True
        Me.ScriptsView.RowHeadersVisible = False
        Me.ScriptsView.RowHeadersWidth = 10
        Me.ScriptsView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ScriptsView.ShowCellErrors = False
        Me.ScriptsView.ShowCellToolTips = False
        Me.ScriptsView.ShowEditingIcon = False
        Me.ScriptsView.ShowRowErrors = False
        Me.ScriptsView.Size = New System.Drawing.Size(507, 178)
        Me.ScriptsView.TabIndex = 0
        '
        'ScriptStatus
        '
        Me.ScriptStatus.HeaderText = ""
        Me.ScriptStatus.Name = "ScriptStatus"
        Me.ScriptStatus.ReadOnly = True
        Me.ScriptStatus.Width = 20
        '
        'Filename
        '
        Me.Filename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Filename.HeaderText = "Filename"
        Me.Filename.Name = "Filename"
        Me.Filename.ReadOnly = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(531, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddScriptToolStripMenuItem, Me.EditToolStripMenuItem2, Me.RemoveToolStripMenuItem1, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        Me.AddToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.AddToolStripMenuItem.Text = "&File"
        '
        'AddScriptToolStripMenuItem
        '
        Me.AddScriptToolStripMenuItem.Image = CType(resources.GetObject("AddScriptToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddScriptToolStripMenuItem.Name = "AddScriptToolStripMenuItem"
        Me.AddScriptToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AddScriptToolStripMenuItem.Text = "&Add"
        '
        'EditToolStripMenuItem2
        '
        Me.EditToolStripMenuItem2.Image = CType(resources.GetObject("EditToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.EditToolStripMenuItem2.Name = "EditToolStripMenuItem2"
        Me.EditToolStripMenuItem2.Size = New System.Drawing.Size(152, 22)
        Me.EditToolStripMenuItem2.Text = "&Edit"
        '
        'RemoveToolStripMenuItem1
        '
        Me.RemoveToolStripMenuItem1.Image = CType(resources.GetObject("RemoveToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.RemoveToolStripMenuItem1.Name = "RemoveToolStripMenuItem1"
        Me.RemoveToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.RemoveToolStripMenuItem1.Text = "&Remove"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = CType(resources.GetObject("ExitToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "&Close"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartToolStripMenuItem, Me.ResumeToolStripMenuItem1})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedToolStripMenuItem, Me.AllToolStripMenuItem})
        Me.StartToolStripMenuItem.Image = Global.TibiaTekBot.My.Resources.Resources.script_pause
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.StartToolStripMenuItem.Text = "&Pause"
        '
        'SelectedToolStripMenuItem
        '
        Me.SelectedToolStripMenuItem.Name = "SelectedToolStripMenuItem"
        Me.SelectedToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SelectedToolStripMenuItem.Text = "&Selected"
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AllToolStripMenuItem.Text = "&All"
        '
        'ResumeToolStripMenuItem1
        '
        Me.ResumeToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedToolStripMenuItem1, Me.AllToolStripMenuItem1})
        Me.ResumeToolStripMenuItem1.Image = CType(resources.GetObject("ResumeToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ResumeToolStripMenuItem1.Name = "ResumeToolStripMenuItem1"
        Me.ResumeToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.ResumeToolStripMenuItem1.Text = "&Resume"
        '
        'SelectedToolStripMenuItem1
        '
        Me.SelectedToolStripMenuItem1.Name = "SelectedToolStripMenuItem1"
        Me.SelectedToolStripMenuItem1.Size = New System.Drawing.Size(126, 22)
        Me.SelectedToolStripMenuItem1.Text = "&Selected"
        '
        'AllToolStripMenuItem1
        '
        Me.AllToolStripMenuItem1.Name = "AllToolStripMenuItem1"
        Me.AllToolStripMenuItem1.Size = New System.Drawing.Size(126, 22)
        Me.AllToolStripMenuItem1.Text = "&All"
        '
        'OpenScriptDialog
        '
        Me.OpenScriptDialog.Filter = "TibiaTek Script Files (VB Syntax)|*.tts.vb"
        '
        'frmScripts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 233)
        Me.Controls.Add(Me.ScriptsView)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = Global.TibiaTekBot.My.Resources.Resources.ttb
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmScripts"
        Me.Text = "Scripts Manager"
        CType(Me.ScriptsView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ScriptsView As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddScriptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenScriptDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ScriptStatus As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Filename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EditToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResumeToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectedToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class
