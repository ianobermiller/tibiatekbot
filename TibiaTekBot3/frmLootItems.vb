Public Class frmLootItems

    Private Sub frmLootItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each Item As ItemDefinition In Core.Definitions.ItemsList
            If Not Items.Items.Contains(Item.Name) Then
                If Item.Kind <> ItemKind.BlockedTeleport And _
                   Item.Kind <> ItemKind.Blocking And _
                   Item.Kind <> ItemKind.Door And _
                   Item.Kind <> ItemKind.FullBlocking And _
                   Item.Kind <> ItemKind.MagicField And _
                   Item.Kind <> ItemKind.RopeSpot And _
                   Item.Kind <> ItemKind.Rune And _
                   Item.Kind <> ItemKind.Special And _
                   Item.Kind <> ItemKind.Teleport And _
                   Item.Kind <> ItemKind.Unknown And _
                   Item.Kind <> ItemKind.UsableTeleport And _
                   Item.Kind <> ItemKind.UsableTeleport2 Then
                    If Not Core.LootItems.Items.ContainsKey(Item.ItemID) Then
                        Items.Items.Add(Item.Name)
                    End If
                End If
            End If
        Next
        Items.Sorted = True
        For Each Loot As LootItemsClass.LootItemDefinition In Core.LootItems.Items.Values
            Loots.Items.Add(Core.Definitions.GetItemName(Loot.GetID))
        Next
        Loots.Sorted = True
    End Sub

    Private Sub Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add.Click
        If Items.SelectedIndex <> -1 Then
            Dim LootItem As New LootItemsClass.LootItemDefinition
            LootItem.ID = Core.Definitions.GetItemID(Items.SelectedItem)
            Core.LootItems.Add(LootItem)
            Loots.Items.Add(Items.SelectedItem)
            Items.Items.Remove(Items.SelectedItem)
        End If
    End Sub

    Private Sub Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Remove.Click
        If Loots.SelectedIndex <> -1 Then
            Dim LootItem As New LootItemsClass.LootItemDefinition
            LootItem.ID = Core.Definitions.GetItemID(Loots.SelectedItem)
            Core.LootItems.Remove(LootItem.ID)
            Items.Items.Add(Loots.SelectedItem)
            Loots.Items.Remove(Loots.SelectedItem)
        End If
    End Sub

    Private Sub Savecmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savecmd.Click
        Core.LootItems.Save()
        Core.StatusMessage("Loot Items Saved.")
        Me.Close()
    End Sub

    Private Sub Cancelcmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancelcmd.Click
        If MsgBox("Continue without saving changes?", MsgBoxStyle.YesNo, "Don't Save Changes?") = MsgBoxResult.Yes Then
            Core.LootItems.Load()
            Me.Close()
        End If
    End Sub
End Class