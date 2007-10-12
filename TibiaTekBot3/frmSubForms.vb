Public Class frmSubForms
    'TODO: I'm sure there's better way to do this than the way I'm usin (form..)
    Private Sub SpellOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellOnOff.Click
        If SpellOnOff.Text = "Activate" Then
            If SpellNametxtbox.Text <> vbNullString And SpellManatxtbox.Text <> vbNullString Then
                CommandParser("spellcaster " & SpellManatxtbox.Text & " """ & SpellNametxtbox.Text)
                SpellOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("spellcaster pause")
            SpellOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub SpellStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpellStop.Click
        CommandParser("spellcaster off")
        SpellOnOff.Text = "Activate"
    End Sub

    Private Sub EaterOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EaterOnOff.Click
        If EaterOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(EaterDelaytxtbox.Text) Then
                Select Case EaterSmartchk.Checked
                    Case True
                        If Not String.IsNullOrEmpty(EaterSmarttxtbox.Text) Then
                            CommandParser("eater smart " & EaterSmarttxtbox.Text)
                        Else
                            MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
                        End If
                    Case False
                        CommandParser("eater " & EaterDelaytxtbox.Text)
                End Select
                EaterOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("eater pause")
            EaterOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub EaterSmartchkbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EaterSmartchk.CheckedChanged
        If EaterSmartchk.Checked = False Then
            EaterSmartlbl.Enabled = False
            EaterSmarttxtbox.Enabled = False
        Else
            EaterSmartlbl.Enabled = True
            EaterSmarttxtbox.Enabled = True
        End If
    End Sub

    Private Sub EaterStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EaterStop.Click
        CommandParser("eater stop")
        EaterOnOff.Text = "Activate"
    End Sub

    Private Sub MakerOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakerOnOff.Click
        If MakerOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(MakerManatxtbox.Text) And _
               Not String.IsNullOrEmpty(MakerNametxtbox.Text) And _
               Not String.IsNullOrEmpty(MakerSoultxtbox.Text) Then
                CommandParser("runemaker " & MakerManatxtbox.Text & " " & MakerSoultxtbox.Text & " """ & MakerNametxtbox.Text)
                MakerOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("runemaker pause")
            MakerOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub MakerStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakerStop.Click
        CommandParser("runemaker stop")
        MakerOnOff.Text = "Activate"
    End Sub

    Private Sub FisherOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FisherOnOff.Click
        If FisherOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(FisherCaptxtbox.Text) Then
                Select Case FisherModecb.SelectedItem
                    Case "Normal"
                        CommandParser("fisher " & FisherCaptxtbox.Text)
                    Case "Turbo"
                        CommandParser("fisher " & FisherCaptxtbox.Text & " turbo")
                End Select
                FisherOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("fisher pause")
            FisherOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub FisherStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FisherStop.Click
        CommandParser("fisher stop")
        FisherOnOff.Text = "Activate"
    End Sub

    Private Sub NamespyStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NamespyStop.Click
        CommandParser("namespy off")
        NamespyOnOffchk.Checked = False
    End Sub

    Private Sub NamespyOnOffchk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NamespyOnOffchk.CheckedChanged
        If NamespyOnOffchk.Checked = True Then
            CommandParser("namespy on")
        Else
            CommandParser("namespy hide")
        End If
    End Sub

    Private Sub HealerOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealerOnOff.Click
        If HealerOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(HealerHptxtbox.Text) And Not String.IsNullOrEmpty(HealerNametxtbox.Text) Then
                CommandParser("heal " & HealerHptxtbox.Text & " """ & HealerNametxtbox.Text)
                HealerOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("heal pause")
            HealerOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub HealerStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HealerStop.Click
        CommandParser("heal off")
        HealerOnOff.Text = "Activate"
    End Sub

    Private Sub WASDOnOffchk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WASDOnOffchk.CheckedChanged
        If WASDOnOffchk.Checked = True Then
            CommandParser("wasd on")
        Else
            CommandParser("wasd pause")
        End If
    End Sub

    Private Sub WASDStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WASDStop.Click
        CommandParser("wasd off")
        WASDOnOffchk.Checked = False
    End Sub

    Private Sub ExpOnOffchk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpOnOffchk.CheckedChanged
        If ExpOnOffchk.Checked = True Then
            CommandParser("exp on")
        Else
            CommandParser("exp off")
        End If
    End Sub

    Private Sub ExpCreaturesOnOffchk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpCreaturesOnOffchk.CheckedChanged
        If ExpCreaturesOnOffchk.Checked = True Then
            CommandParser("exp creatures on")
        Else
            CommandParser("exp creatures off")
        End If
    End Sub

    Private Sub ExpStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpStop.Click
        CommandParser("exp stop")
        ExpCreaturesOnOffchk.Checked = False
        ExpOnOffchk.Checked = False
    End Sub
End Class