Public Class frmKeyboard
    'the only way i could think of, i miss my pointers
    'the pure essence of inefficient programming
    Structure commandStruct
        Dim identifier As Integer
        Dim param1 As Integer
        Dim param2 As Integer
        Dim param3 As Integer
        Dim param4 As Integer
        Dim paramS As String
    End Structure

    Dim curChar As Integer
    Dim stateShift As Integer
    Dim stateCtrl As Integer
    Dim stateAlt As Integer
    Dim keyString As String
    Dim commandArray(359) As commandStruct
    Dim offset As Integer
    Private Sub SetSayCommand(ByVal sayType As Integer, ByRef sayString As String)
        offset = (stateShift Or stateCtrl << 1 Or stateAlt << 2) * 45 + curChar
        commandArray(offset).identifier = 1
        commandArray(offset).param1 = sayType '1 = say, 2 = whisper, 3 = yell
        commandArray(offset).paramS = sayString

        'do the send, not sure how to do it yet
    End Sub


    Private Sub HKey1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey1.CheckedChanged
        If HKey1.Checked Then
            curChar = 0
        End If
        SetKeyString()
    End Sub
    Private Sub HKey2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey2.CheckedChanged
        If HKey2.Checked Then
            curChar = 1
        End If
        SetKeyString()
    End Sub
    Private Sub HKey3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey3.CheckedChanged
        If HKey3.Checked Then
            curChar = 2
        End If
        SetKeyString()
    End Sub
    Private Sub HKey4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey4.CheckedChanged
        If HKey4.Checked Then
            curChar = 3
        End If
        SetKeyString()
    End Sub
    Private Sub HKey5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey5.CheckedChanged
        If HKey5.Checked Then
            curChar = 4
        End If
        SetKeyString()
    End Sub
    Private Sub HKey6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey6.CheckedChanged
        If HKey6.Checked Then
            curChar = 5
        End If
        SetKeyString()
    End Sub
    Private Sub HKey7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey7.CheckedChanged
        If HKey7.Checked Then
            curChar = 6
        End If
        SetKeyString()
    End Sub
    Private Sub HKey8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey8.CheckedChanged
        If HKey8.Checked Then
            curChar = 7
        End If
        SetKeyString()
    End Sub
    Private Sub HKey9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey9.CheckedChanged
        If HKey9.Checked Then
            curChar = 8
        End If
        SetKeyString()
    End Sub
    Private Sub HKey0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKey0.CheckedChanged
        If HKey0.Checked Then
            curChar = 9
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyMinus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyMinus.CheckedChanged
        If HKeyMinus.Checked Then
            curChar = 10
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyEqual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyEqual.CheckedChanged
        If HKeyEqual.Checked Then
            curChar = 11
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyQ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyQ.CheckedChanged
        If HKeyQ.Checked Then
            curChar = 12
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyW_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyW.CheckedChanged
        If HKeyW.Checked Then
            curChar = 13
        End If
        SetKeyString()
    End Sub
    Private Sub HkeyE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HkeyE.CheckedChanged
        If HkeyE.Checked Then
            curChar = 14
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyR.CheckedChanged
        If HKeyR.Checked Then
            curChar = 15
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyT.CheckedChanged
        If HKeyT.Checked Then
            curChar = 16
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyY_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyY.CheckedChanged
        If HKeyY.Checked Then
            curChar = 17
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyU_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyU.CheckedChanged
        If HKeyU.Checked Then
            curChar = 18
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyI.CheckedChanged
        If HKeyI.Checked Then
            curChar = 19
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyO.CheckedChanged
        If HKeyO.Checked Then
            curChar = 20
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyP.CheckedChanged
        If HKeyP.Checked Then
            curChar = 21
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyLBrac_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyLBrac.CheckedChanged
        If HKeyLBrac.Checked Then
            curChar = 22
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyRBrac_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyRBrac.CheckedChanged
        If HKeyRBrac.Checked Then
            curChar = 23
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyA.CheckedChanged
        If HKeyA.Checked Then
            curChar = 24
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyS.CheckedChanged
        If HKeyS.Checked Then
            curChar = 25
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyD.CheckedChanged
        If HKeyD.Checked Then
            curChar = 26
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyF.CheckedChanged
        If HKeyF.Checked Then
            curChar = 27
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyG_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyG.CheckedChanged
        If HKeyG.Checked Then
            curChar = 28
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyH.CheckedChanged
        If HKeyH.Checked Then
            curChar = 29
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyJ.CheckedChanged
        If HKeyJ.Checked Then
            curChar = 30
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyK.CheckedChanged
        If HKeyK.Checked Then
            curChar = 31
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyL.CheckedChanged
        If HKeyL.Checked Then
            curChar = 32
        End If
        SetKeyString()
    End Sub
    Private Sub HKeySColon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeySColon.CheckedChanged
        If HKeySColon.Checked Then
            curChar = 33
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyApost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyApost.CheckedChanged
        If HKeyApost.Checked Then
            curChar = 34
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyZ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyZ.CheckedChanged
        If HKeyZ.Checked Then
            curChar = 35
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyX_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyX.CheckedChanged
        If HKeyX.Checked Then
            curChar = 36
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyC.CheckedChanged
        If HKeyC.Checked Then
            curChar = 37
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyV.CheckedChanged
        If HKeyV.Checked Then
            curChar = 38
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyB.CheckedChanged
        If HKeyB.Checked Then
            curChar = 39
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyN.CheckedChanged
        If HKeyN.Checked Then
            curChar = 40
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyM.CheckedChanged
        If HKeyM.Checked Then
            curChar = 41
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyComma_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyComma.CheckedChanged
        If HKeyComma.Checked Then
            curChar = 42
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyFStop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyFStop.CheckedChanged
        If HKeyFStop.Checked Then
            curChar = 43
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyFSlsh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyFSlsh.CheckedChanged
        If HKeyFSlsh.Checked Then
            curChar = 44
        End If
        SetKeyString()
    End Sub
    Private Sub HKeyAlt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyAlt.CheckedChanged
        stateAlt = stateAlt Xor 1
        SetKeyString()
    End Sub
    Private Sub HKeyCtrl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyCtrl.CheckedChanged
        stateCtrl = stateCtrl Xor 1
        SetKeyString()
    End Sub
    Private Sub HKeyShift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyShift.CheckedChanged
        stateShift = stateShift Xor 1
        SetKeyString()
    End Sub
    Private Sub SetKeyString()
        keyString = ""
        If stateShift = 1 Then
            keyString &= "Shift +"
        End If
        If stateCtrl = 1 Then
            keyString &= "Ctrl + "
        End If
        If stateAlt = 1 Then
            keyString &= "Alt + "
        End If
        Select Case curChar
            Case 0
                keyString &= "1"
            Case 1
                keyString &= "2"
            Case 2
                keyString &= "3"
            Case 3
                keyString &= "4"
            Case 4
                keyString &= "5"
            Case 5
                keyString &= "6"
            Case 6
                keyString &= "7"
            Case 7
                keyString &= "8"
            Case 8
                keyString &= "9"
            Case 9
                keyString &= "0"
            Case 10
                keyString &= "-"
            Case 11
                keyString &= "="
            Case 12
                keyString &= "Q"
            Case 13
                keyString &= "W"
            Case 14
                keyString &= "E"
            Case 15
                keyString &= "R"
            Case 16
                keyString &= "T"
            Case 17
                keyString &= "Y"
            Case 18
                keyString &= "U"
            Case 19
                keyString &= "I"
            Case 20
                keyString &= "O"
            Case 21
                keyString &= "P"
            Case 22
                keyString &= "["
            Case 23
                keyString &= "]"
            Case 24
                keyString &= "A"
            Case 25
                keyString &= "S"
            Case 26
                keyString &= "D"
            Case 27
                keyString &= "F"
            Case 28
                keyString &= "G"
            Case 29
                keyString &= "H"
            Case 30
                keyString &= "J"
            Case 31
                keyString &= "K"
            Case 32
                keyString &= "L"
            Case 33
                keyString &= ";"
            Case 34
                keyString &= "'"
            Case 35
                keyString &= "Z"
            Case 36
                keyString &= "X"
            Case 37
                keyString &= "C"
            Case 38
                keyString &= "V"
            Case 39
                keyString &= "B"
            Case 40
                keyString &= "N"
            Case 41
                keyString &= "M"
            Case 42
                keyString &= ","
            Case 43
                keyString &= "."
            Case 44
                keyString &= "/"
        End Select
        PressedLabel.Text = keyString
    End Sub

    Private Sub IdentifierBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IdentifierBox.SelectedIndexChanged
        offset = (stateShift Or stateCtrl << 1 Or stateAlt << 2) * 45 + curChar
        Dim index As Integer
        index = IdentifierBox.SelectedIndex
        Select Case index
            Case 1
                commandArray(offset).identifier = 1
        End Select
    End Sub

    Private Sub frmKeyboard_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

End Class