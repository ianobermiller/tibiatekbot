Public Class frmRegExpBuilder

    Private Sub AddItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItems.Click
        Dim Item As String = InputBox("Enter Item Name:" & vbCrLf & "Note: You can define optional words closing them with '<' and '>' characters." & vbCrLf & _
                                      " For Example: bp <of> hmm", "Enter Item")
        If Not ItemsList.Items.Contains(Item) AndAlso Not String.IsNullOrEmpty(Item) Then
            ItemsList.Items.Add(Item)
        End If
    End Sub

    Private Sub RemoveItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveItems.Click
        If ItemsList.SelectedIndex = -1 Then
            Beep()
            Exit Sub
        End If
        ItemsList.Items.RemoveAt(ItemsList.SelectedIndex)
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        If MessageBox.Show("Are you sure you want to close Regular Expression Builder?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then Me.Close()
    End Sub

    Private Sub Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Generate.Click
        Dim Output As String = "("

        'Actions:
        If Sell.Checked Then
            Output += "sell"
        End If
        If Buy.Checked Then
            If Sell.Checked Then Output += "|"
            Output += "buy"
        End If
        If Trade.Checked Then
            If Sell.Checked Or Buy.Checked Then Output += "|"
            Output += "trade"
        End If


        If Output = "(" Then
            MessageBox.Show("No actions were selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Output += ")("

        'Items
        Dim I As Integer = 0 'Counter
        'Parsing the items. This is not the nicest RegExp, but it works. If you can, you can write better :)
        For Each Item As String In ItemsList.Items
            I += 1
            Item = Item.Replace(" <", "(.+")
            Item = Item.Replace("<", "(.+")
            Item = Item.Replace(">", ")?")
            Item = Item.Replace(" ", ".+")
            If Item.Chars(0) = "(" Then
                Output += Item
            Else
                Output += ".+" & Item
            End If
            If Not I = ItemsList.Items.Count Then
                Output += "|"
            End If
        Next
        Output += ")"
        If ItemsList.Items.Count > 0 Then
            frmMain.TradeChannelWatcherExpression.Text = Output
            Me.Close()
        Else
            MessageBox.Show("Enter Items before building the epxression.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub RegularExHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegularExHelp.Click
        MessageBox.Show("How to Build a Good Regular Expression:" & vbCrLf & vbCrLf & _
                        "1. Select the action you're looking for." & vbCrLf & _
                        "Eg.: if you're looking for someone who sells, select Sell." & vbCrLf & _
                        "2. Add the an item name or any abbreviation (UH, BP, etc.) to the list, click on Add." & vbCrLf & _
                        "3. Click on Build.", "Regular Expresion Builder Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class