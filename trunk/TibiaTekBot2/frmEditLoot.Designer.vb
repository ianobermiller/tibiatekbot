<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditLoot
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditLoot))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LootCategoryItems = New System.Windows.Forms.DataGridView
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LootTo = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.LootCategories = New System.Windows.Forms.ComboBox
        Me.LooterEditButton = New System.Windows.Forms.Button
        Me.EditLootHelp = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LootCategoryItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(173, 213)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 32)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 24)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 24)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LootCategoryItems)
        Me.GroupBox1.Controls.Add(Me.LootTo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.LootCategories)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 202)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Loot Items to Backpacks"
        '
        'LootCategoryItems
        '
        Me.LootCategoryItems.AllowUserToAddRows = False
        Me.LootCategoryItems.AllowUserToDeleteRows = False
        Me.LootCategoryItems.AllowUserToResizeRows = False
        Me.LootCategoryItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LootCategoryItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.LootCategoryItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LootCategoryItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.LootCategoryItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemName, Me.ID})
        Me.LootCategoryItems.Location = New System.Drawing.Point(6, 46)
        Me.LootCategoryItems.Name = "LootCategoryItems"
        Me.LootCategoryItems.ReadOnly = True
        Me.LootCategoryItems.RowHeadersVisible = False
        Me.LootCategoryItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.LootCategoryItems.Size = New System.Drawing.Size(203, 150)
        Me.LootCategoryItems.TabIndex = 4
        '
        'ItemName
        '
        Me.ItemName.HeaderText = "ItemName"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        '
        'ID
        '
        Me.ID.HeaderText = "ItemID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'LootTo
        '
        Me.LootTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LootTo.FormattingEnabled = True
        Me.LootTo.Items.AddRange(New Object() {"Ground", "Backpack [1]", "Backpack [2]", "Backpack [3]", "Backpack [4]", "Backpack [5]", "Backpack [6]", "Backpack [7]", "Backpack [8]", "Backpack [9]", "Backpack [10]"})
        Me.LootTo.Location = New System.Drawing.Point(218, 62)
        Me.LootTo.Name = "LootTo"
        Me.LootTo.Size = New System.Drawing.Size(119, 21)
        Me.LootTo.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(215, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Loot Item to:"
        '
        'LootCategories
        '
        Me.LootCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LootCategories.FormattingEnabled = True
        Me.LootCategories.Items.AddRange(New Object() {"Loot Category #1", "Loot Category #2", "Loot Category #3", "Loot Category #4", "Loot Category #5", "Loot Category #6", "Loot Category #7", "Loot Category #8", "Loot Category #9", "Loot Category #10", "Loot Category #11", "Loot Category #12"})
        Me.LootCategories.Location = New System.Drawing.Point(6, 19)
        Me.LootCategories.Name = "LootCategories"
        Me.LootCategories.Size = New System.Drawing.Size(126, 21)
        Me.LootCategories.TabIndex = 0
        '
        'LooterEditButton
        '
        Me.LooterEditButton.Location = New System.Drawing.Point(10, 217)
        Me.LooterEditButton.Name = "LooterEditButton"
        Me.LooterEditButton.Size = New System.Drawing.Size(97, 24)
        Me.LooterEditButton.TabIndex = 2
        Me.LooterEditButton.Text = "Edit Loot Items"
        Me.LooterEditButton.UseVisualStyleBackColor = True
        '
        'EditLootHelp
        '
        Me.EditLootHelp.Location = New System.Drawing.Point(321, 217)
        Me.EditLootHelp.Name = "EditLootHelp"
        Me.EditLootHelp.Size = New System.Drawing.Size(26, 24)
        Me.EditLootHelp.TabIndex = 3
        Me.EditLootHelp.Text = "?"
        Me.EditLootHelp.UseVisualStyleBackColor = True
        '
        'frmEditLoot
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(362, 248)
        Me.Controls.Add(Me.EditLootHelp)
        Me.Controls.Add(Me.LooterEditButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditLoot"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Looter Items"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LootCategoryItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LootCategories As System.Windows.Forms.ComboBox
    Friend WithEvents LootTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LooterEditButton As System.Windows.Forms.Button
    Friend WithEvents LootCategoryItems As System.Windows.Forms.DataGridView
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EditLootHelp As System.Windows.Forms.Button

End Class
