<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegExpBuilder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRegExpBuilder))
        Me.ActionBox = New System.Windows.Forms.GroupBox
        Me.Trade = New System.Windows.Forms.CheckBox
        Me.Buy = New System.Windows.Forms.CheckBox
        Me.Sell = New System.Windows.Forms.CheckBox
        Me.ItemsBox = New System.Windows.Forms.GroupBox
        Me.RemoveItems = New System.Windows.Forms.Button
        Me.AddItems = New System.Windows.Forms.Button
        Me.ItemsList = New System.Windows.Forms.ListBox
        Me.Generate = New System.Windows.Forms.Button
        Me.Cancel = New System.Windows.Forms.Button
        Me.ConfirmBox = New System.Windows.Forms.GroupBox
        Me.RegularExHelp = New System.Windows.Forms.Button
        Me.ActionBox.SuspendLayout()
        Me.ItemsBox.SuspendLayout()
        Me.ConfirmBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ActionBox
        '
        Me.ActionBox.Controls.Add(Me.Trade)
        Me.ActionBox.Controls.Add(Me.Buy)
        Me.ActionBox.Controls.Add(Me.Sell)
        Me.ActionBox.Location = New System.Drawing.Point(12, 12)
        Me.ActionBox.Name = "ActionBox"
        Me.ActionBox.Size = New System.Drawing.Size(170, 49)
        Me.ActionBox.TabIndex = 0
        Me.ActionBox.TabStop = False
        Me.ActionBox.Text = "Action"
        '
        'Trade
        '
        Me.Trade.AutoSize = True
        Me.Trade.Location = New System.Drawing.Point(112, 19)
        Me.Trade.Name = "Trade"
        Me.Trade.Size = New System.Drawing.Size(54, 17)
        Me.Trade.TabIndex = 2
        Me.Trade.Text = "Trade"
        Me.Trade.UseVisualStyleBackColor = True
        '
        'Buy
        '
        Me.Buy.AutoSize = True
        Me.Buy.Location = New System.Drawing.Point(62, 19)
        Me.Buy.Name = "Buy"
        Me.Buy.Size = New System.Drawing.Size(44, 17)
        Me.Buy.TabIndex = 1
        Me.Buy.Text = "Buy"
        Me.Buy.UseVisualStyleBackColor = True
        '
        'Sell
        '
        Me.Sell.AutoSize = True
        Me.Sell.Location = New System.Drawing.Point(13, 19)
        Me.Sell.Name = "Sell"
        Me.Sell.Size = New System.Drawing.Size(43, 17)
        Me.Sell.TabIndex = 0
        Me.Sell.Text = "Sell"
        Me.Sell.UseVisualStyleBackColor = True
        '
        'ItemsBox
        '
        Me.ItemsBox.Controls.Add(Me.RemoveItems)
        Me.ItemsBox.Controls.Add(Me.AddItems)
        Me.ItemsBox.Controls.Add(Me.ItemsList)
        Me.ItemsBox.Location = New System.Drawing.Point(12, 67)
        Me.ItemsBox.Name = "ItemsBox"
        Me.ItemsBox.Size = New System.Drawing.Size(170, 174)
        Me.ItemsBox.TabIndex = 1
        Me.ItemsBox.TabStop = False
        Me.ItemsBox.Text = "Items"
        '
        'RemoveItems
        '
        Me.RemoveItems.Location = New System.Drawing.Point(96, 141)
        Me.RemoveItems.Name = "RemoveItems"
        Me.RemoveItems.Size = New System.Drawing.Size(66, 24)
        Me.RemoveItems.TabIndex = 2
        Me.RemoveItems.Text = "Remove"
        Me.RemoveItems.UseVisualStyleBackColor = True
        '
        'AddItems
        '
        Me.AddItems.Location = New System.Drawing.Point(6, 141)
        Me.AddItems.Name = "AddItems"
        Me.AddItems.Size = New System.Drawing.Size(66, 24)
        Me.AddItems.TabIndex = 1
        Me.AddItems.Text = "Add"
        Me.AddItems.UseVisualStyleBackColor = True
        '
        'ItemsList
        '
        Me.ItemsList.FormattingEnabled = True
        Me.ItemsList.Location = New System.Drawing.Point(6, 16)
        Me.ItemsList.Name = "ItemsList"
        Me.ItemsList.Size = New System.Drawing.Size(156, 121)
        Me.ItemsList.TabIndex = 0
        '
        'Generate
        '
        Me.Generate.Location = New System.Drawing.Point(6, 19)
        Me.Generate.Name = "Generate"
        Me.Generate.Size = New System.Drawing.Size(66, 24)
        Me.Generate.TabIndex = 2
        Me.Generate.Text = "Build"
        Me.Generate.UseVisualStyleBackColor = True
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(96, 19)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(66, 24)
        Me.Cancel.TabIndex = 3
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'ConfirmBox
        '
        Me.ConfirmBox.Controls.Add(Me.RegularExHelp)
        Me.ConfirmBox.Controls.Add(Me.Generate)
        Me.ConfirmBox.Controls.Add(Me.Cancel)
        Me.ConfirmBox.Location = New System.Drawing.Point(12, 247)
        Me.ConfirmBox.Name = "ConfirmBox"
        Me.ConfirmBox.Size = New System.Drawing.Size(170, 76)
        Me.ConfirmBox.TabIndex = 4
        Me.ConfirmBox.TabStop = False
        Me.ConfirmBox.Text = "Generate"
        '
        'RegularExHelp
        '
        Me.RegularExHelp.Location = New System.Drawing.Point(136, 46)
        Me.RegularExHelp.Name = "RegularExHelp"
        Me.RegularExHelp.Size = New System.Drawing.Size(26, 24)
        Me.RegularExHelp.TabIndex = 4
        Me.RegularExHelp.Text = "?"
        Me.RegularExHelp.UseVisualStyleBackColor = True
        '
        'frmRegExpBuilder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(194, 335)
        Me.Controls.Add(Me.ConfirmBox)
        Me.Controls.Add(Me.ItemsBox)
        Me.Controls.Add(Me.ActionBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRegExpBuilder"
        Me.Text = "Regular Expression Builder"
        Me.ActionBox.ResumeLayout(False)
        Me.ActionBox.PerformLayout()
        Me.ItemsBox.ResumeLayout(False)
        Me.ConfirmBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ActionBox As System.Windows.Forms.GroupBox
    Friend WithEvents Trade As System.Windows.Forms.CheckBox
    Friend WithEvents Buy As System.Windows.Forms.CheckBox
    Friend WithEvents Sell As System.Windows.Forms.CheckBox
    Friend WithEvents ItemsBox As System.Windows.Forms.GroupBox
    Friend WithEvents RemoveItems As System.Windows.Forms.Button
    Friend WithEvents AddItems As System.Windows.Forms.Button
    Friend WithEvents ItemsList As System.Windows.Forms.ListBox
    Friend WithEvents Generate As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents ConfirmBox As System.Windows.Forms.GroupBox
    Friend WithEvents RegularExHelp As System.Windows.Forms.Button
End Class
