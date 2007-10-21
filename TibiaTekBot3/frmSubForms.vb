Public Class frmSubForms
    'TODO: Improve HealFriend. Add Smart healer (give mana points when using uh and when sio)
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
        CommandParser("exp end")
        ExpCreaturesOnOffchk.Checked = False
        ExpOnOffchk.Checked = False
    End Sub

    Private Sub LightModecb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightModecb.SelectedIndexChanged
        If LightOnOffchk.Checked = True Then
            Select Case LightModecb.SelectedIndex
                Case 0
                    CommandParser("light on")
                Case 1
                    CommandParser("light torch")
                Case 2
                    CommandParser("light great torch")
                Case 3
                    CommandParser("light ultimate torch")
                Case 4
                    CommandParser("light utevo lux")
                Case 5
                    CommandParser("light utevo gran lux")
                Case 6
                    CommandParser("light utevo vis lux")
                Case 7
                    CommandParser("light light wand")
                Case Else
                    MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End Select
        End If
    End Sub

    Private Sub LightOnOffchk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightOnOffchk.CheckedChanged
        If LightOnOffchk.Checked = True Then
            Select Case LightModecb.SelectedIndex
                Case 0
                    CommandParser("light on")
                Case 1
                    CommandParser("light torch")
                Case 2
                    CommandParser("light great torch")
                Case 3
                    CommandParser("light ultimate torch")
                Case 4
                    CommandParser("light utevo lux")
                Case 5
                    CommandParser("light utevo gran lux")
                Case 6
                    CommandParser("light utevo vis lux")
                Case 7
                    CommandParser("light light wand")
                Case Else
                    MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End Select
        Else
            CommandParser("light pause")
        End If
    End Sub

    Private Sub LightStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LightStop.Click
        CommandParser("light off")
        LightModecb.Text = "Select Light Mode"
        LightOnOffchk.Checked = False
    End Sub

    Private Sub AdvertiserOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvertiserOnOff.Click
        If AdvertiserOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(AdvertiseMsgtxtbox.Text) Then
                CommandParser("advertiser """ & AdvertiseMsgtxtbox.Text & """")
                AdvertiserOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("advertiser pause")
            AdvertiserOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub AdvertiseStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvertiseStop.Click
        CommandParser("advertiser off")
        AdvertiseMsgtxtbox.Clear()
        AdvertiserOnOff.Text = "Activate"
    End Sub

    Private Sub WatcherOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WatcherOnOff.Click
        If WatcherOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(WatcherExptxtbox.Text) Then
                CommandParser("watcher " & WatcherExptxtbox.Text)
                WatcherOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("watcher pause")
            WatcherOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub WatcherStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WatcherStop.Click
        CommandParser("watcher off")
        WatcherExptxtbox.Text = ""
        WatcherOnOff.Text = "Activate"
    End Sub

    Private Sub WatcherOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WatcherOpen.Click
        Try
            Process.Start(Application.StartupPath & "\Offers.txt")
        Catch
            MsgBox("Couldn't Open Offers.txt")
        End Try


    End Sub

    Private Sub FPSOnOffcb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FPSOnOffcb.CheckedChanged
        If FPSOnOffcb.Checked = True Then
            CommandParser("fpschanger on")
        Else
            CommandParser("fpschanger pause")
        End If
    End Sub

    Private Sub FPSStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FPSStop.Click
        CommandParser("fpschanger off")
    End Sub

    Private Sub UploaderOnOffcb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploaderOnOffcb.CheckedChanged
        If UploaderOnOffcb.Checked = True Then
            CommandParser("statsuploader on")
        Else
            CommandParser("statsuploader pause")
        End If
    End Sub

    Private Sub UploaderStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploaderStop.Click
        CommandParser("statsuploader off")
    End Sub

    Private Sub ChangerOnOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangerOnOff.CheckedChanged
        If ChangerOnOff.Checked = True Then
            Select Case ChangerAmuletscb.SelectedIndex
                Case 0
                    CommandParser("changer on")
                Case 1
                    CommandParser("changer Stone Skin Amulet")
                Case 2
                    CommandParser("changer Bronze Amulet")
                Case 3
                    CommandParser("changer Elven Amulet")
                Case 4
                    CommandParser("changer Protection Amulet")
                Case 5
                    CommandParser("changer Silver Amulet")
                Case 6
                    CommandParser("chaner Strange Talisman")
                Case 7
                    CommandParser("Dragon Necklace")
                Case 8
                    CommandParser("Garlic Necklace")
                Case Else
                    MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End Select
        Else
            CommandParser("changer pause")
        End If
    End Sub

    Private Sub ChangerStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangerStop.Click
        CommandParser("changer off")
        ChangerOnOff.Checked = False
    End Sub

    Private Sub UHerOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UHerOnOff.Click
        If UHerOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(UHerHptxtbox.Text) Then
                CommandParser("uher " & UHerHptxtbox.Text)
                UHerOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("uher pause")
            UHerOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub UHerStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UHerStop.Click
        CommandParser("uher off")
        UHerOnOff.Text = "Activate"
    End Sub

    Private Sub HFOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HFOnOff.Click
        If HFOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(HFHptxtbox.Text) And Not String.IsNullOrEmpty(HFNametxtbox.Text) And HFTypecb.SelectedIndex <> -1 Then
                Select Case HFTypecb.SelectedIndex
                    Case 0
                        CommandParser("healfriend " & HFHptxtbox.Text & " ""uh"" " & """" & HFNametxtbox.Text & """")
                        HFOnOff.Text = "Pause"
                    Case 1
                        CommandParser("healfriend " & HFHptxtbox.Text & " ""sio"" " & """" & HFNametxtbox.Text & """")
                        HFOnOff.Text = "Pause"
                    Case 2
                        CommandParser("healfriend " & HFHptxtbox.Text & " ""both"" " & """" & HFNametxtbox.Text & """")
                        HFOnOff.Text = "Pause"
                    Case Else
                        MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
                End Select
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("healfriend pause")
            HFOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub HFStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HFStop.Click
        CommandParser("healfriend off")
        HFOnOff.Text = "Activate"
    End Sub

    Private Sub HFGetBL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HFGetBL.Click
        Dim SelectChar As New frmHFBattlelist
        SelectChar.Show()
    End Sub

    Private Sub HPOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HPOnOff.Click
        If HPOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(HPHptxtbox.Text) And HpTypescb.SelectedIndex <> -1 Then
                Select Case HFTypecb.SelectedIndex
                    Case 0
                        CommandParser("healparty " & HPHptxtbox.Text & " ""uh""")
                        HPOnOff.Text = "Pause"
                    Case 1
                        CommandParser("healparty " & HPHptxtbox.Text & " ""sio""")
                        HPOnOff.Text = "Pause"
                    Case 2
                        CommandParser("healparty " & HPHptxtbox.Text & " ""both""")
                        HPOnOff.Text = "Pause"
                    Case Else
                        MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
                End Select
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("healparty pause")
            HPOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub HPStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HPStop.Click
        CommandParser("healparty off")
        HPOnOff.Text = "Activate"
    End Sub

    Private Sub DrinkerOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DrinkerOnOff.Click
        If DrinkerOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(DrinkerManatxtbox.Text) Then
                CommandParser("drinker " & DrinkerManatxtbox.Text)
                DrinkerOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("drinker pause")
            DrinkerOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub DrinkerStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DrinkerStop.Click
        CommandParser("drinker off")
        DrinkerOnOff.Text = "Activate"
    End Sub

    Private Sub LooterOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LooterOnOff.Click
        If LooterOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(LooterCaptxtbox.Text) Then
                If CInt(LooterCaptxtbox.Text) = 0 Then
                    CommandParser("looter on")
                Else
                    CommandParser("looter " & LooterCaptxtbox.Text)
                End If
                LooterOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
            Else
                CommandParser("looter pause")
                LooterOnOff.Text = "Activate"
            End If
    End Sub

    Private Sub LooterEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LooterEdit.Click
        CommandParser("looter edit")
    End Sub

    Private Sub LooterStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LooterStop.Click
        CommandParser("looter off")
        LooterOnOff.Text = "Activate"
    End Sub

    Private Sub StackerOnOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StackerOnOff.CheckedChanged
        If StackerOnOff.Checked = True Then
            CommandParser("stacker on")
        Else
            CommandParser("stacker pause")
        End If
    End Sub

    Private Sub StackerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StackerOff.Click
        CommandParser("stacker off")
        StackerOnOff.Checked = False
    End Sub

    Private Sub AmmoOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmoOnOff.Click
        If AmmoOnOff.Text = "Activate" Then
            If Not String.IsNullOrEmpty(AmmoAmounttxtbox.Text) Then
                CommandParser("ammo " & AmmoAmounttxtbox.Text)
                AmmoOnOff.Text = "Pause"
            Else
                MsgBox("Invalid Parametres! Do you remembered to fill every section", MsgBoxStyle.OkOnly, "Invalid Parametres")
            End If
        Else
            CommandParser("ammo pause")
            AmmoOnOff.Text = "Activate"
        End If
    End Sub

    Private Sub AmmoStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AmmoStop.Click
        CommandParser("ammo off")
        AmmoOnOff.Text = "Activate"
    End Sub
End Class