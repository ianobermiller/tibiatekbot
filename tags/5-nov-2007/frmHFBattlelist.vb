Public Class frmHFBattlelist

    Private Sub frmHFBattlelist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim BL As New BattleList
        BL.Reset() 'Not necessary
        GetCharlb.Items.Clear()

        Do
            If BL.IsPlayer AndAlso BL.IsOnScreen AndAlso Not BL.IsMyself Then
                GetCharlb.Items.Add(BL.GetName)
            End If
        Loop While BL.NextEntity
        If GetCharlb.Items.Count = 0 Then
            MsgBox("There's no players in your battlelist")
            Me.Close()
        End If
    End Sub

    Private Sub GetCharlb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetCharlb.SelectedIndexChanged
        frmSubForms.HFNametxtbox.Text = GetCharlb.SelectedItem.ToString
        Me.Close()
    End Sub
End Class