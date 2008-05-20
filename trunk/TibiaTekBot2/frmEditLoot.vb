Imports System.Windows.Forms

Public Class frmEditLoot

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Kernel.LootItems.Save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Kernel.LootItems.Save()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LooterEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LooterEditButton.Click
        Kernel.LootItems.ShowLootCategories()
        Kernel.Client.BringToFront()
    End Sub

    Private Sub frmEditLoot_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Hide()
    End Sub

    Private Sub LootCategories_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LootCategories.SelectedIndexChanged
        LootCategoryItems.Rows.Clear()
        Dim Items() As Scripting.IContainer.ContainerItemDefinition
        Items = Kernel.LootItems.GetItemsIDs(LootCategories.SelectedIndex)
        For Each Item As Scripting.IContainer.ContainerItemDefinition In Items
            If String.IsNullOrEmpty(Kernel.Client.Objects.Name(Item.ID)) Then
                LootCategoryItems.Rows.Add("Unknown", Item.ID)
            Else
                LootCategoryItems.Rows.Add(Kernel.Client.Objects.Name(Item.ID), Item.ID)
            End If
        Next
    End Sub

    Private Sub frmEditLoot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LootCategories.SelectedIndex = 0
    End Sub

    Private Sub LootCategoryItems_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles LootCategoryItems.CellClick
        LootTo.SelectedIndex = Kernel.LootItems.GetLootItem(LootCategoryItems.SelectedRows(0).Cells(1).Value).GetLootBackpack
    End Sub

    Private Sub LootTo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LootTo.SelectedIndexChanged
        Kernel.LootItems.SetLootBackpackIndex(LootCategoryItems.SelectedRows(0).Cells(1).Value, LootTo.SelectedIndex)
    End Sub

    Private Sub LootCategoryItems_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LootCategoryItems.SelectionChanged
        If LootCategoryItems.SelectedRows.Count > 0 Then
            LootTo.SelectedIndex = Kernel.LootItems.GetLootItem(LootCategoryItems.SelectedRows(0).Cells(1).Value).GetLootBackpack
        End If
    End Sub

    Private Sub EditLootHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditLootHelp.Click
        MessageBox.Show("How to select to which backpack loot certain items:" & vbCrLf & _
                        "1. Select a Loot Category (Virtual Container)." & vbCrLf & _
                        "2. Select an Item from the Loot Category." & vbCrLf & _
                        "3. Select the Backpack Number you wish to loot that certain item to.", "Edit Loot Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
