Imports Scripting

Public Class frmKeyboard
    Private VirtualKeysList As New Dictionary(Of String, IKernel.VirtualKey)

    Private SelectedVK As IKernel.VirtualKey = IKernel.VirtualKey.None
    Dim SelectedModifier As IKernel.KeyboardModifier = IKernel.KeyboardModifier.None

    Private VK As String = ""

    Private Sub frmKeyboard_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub frmKeyboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VirtualKeysList.Add("Enter", IKernel.VirtualKey.Enter)
            VirtualKeysList.Add("Escape", IKernel.VirtualKey.Escape)
            VirtualKeysList.Add("Space", IKernel.VirtualKey.Space)
            VirtualKeysList.Add("Page Up", IKernel.VirtualKey.PageUp)
            VirtualKeysList.Add("Page Down", IKernel.VirtualKey.PageDown)
            VirtualKeysList.Add("End", IKernel.VirtualKey.End)
            VirtualKeysList.Add("Home", IKernel.VirtualKey.Home)
            VirtualKeysList.Add("Left", IKernel.VirtualKey.Left)
            VirtualKeysList.Add("Up", IKernel.VirtualKey.Up)
            VirtualKeysList.Add("Right", IKernel.VirtualKey.Right)
            VirtualKeysList.Add("Down", IKernel.VirtualKey.Down)
            VirtualKeysList.Add("Insert", IKernel.VirtualKey.Insert)
            VirtualKeysList.Add("Delete", IKernel.VirtualKey.Delete)
            VirtualKeysList.Add("0", IKernel.VirtualKey.Number0)
            VirtualKeysList.Add("1", IKernel.VirtualKey.Number1)
            VirtualKeysList.Add("2", IKernel.VirtualKey.Number2)
            VirtualKeysList.Add("3", IKernel.VirtualKey.Number3)
            VirtualKeysList.Add("4", IKernel.VirtualKey.Number4)
            VirtualKeysList.Add("5", IKernel.VirtualKey.Number5)
            VirtualKeysList.Add("6", IKernel.VirtualKey.Number6)
            VirtualKeysList.Add("7", IKernel.VirtualKey.Number7)
            VirtualKeysList.Add("8", IKernel.VirtualKey.Number8)
            VirtualKeysList.Add("9", IKernel.VirtualKey.Number9)
            VirtualKeysList.Add("A", IKernel.VirtualKey.A)
            VirtualKeysList.Add("B", IKernel.VirtualKey.B)
            VirtualKeysList.Add("C", IKernel.VirtualKey.C)
            VirtualKeysList.Add("D", IKernel.VirtualKey.D)
            VirtualKeysList.Add("E", IKernel.VirtualKey.E)
            VirtualKeysList.Add("F", IKernel.VirtualKey.F)
            VirtualKeysList.Add("G", IKernel.VirtualKey.G)
            VirtualKeysList.Add("H", IKernel.VirtualKey.H)
            VirtualKeysList.Add("I", IKernel.VirtualKey.I)
            VirtualKeysList.Add("J", IKernel.VirtualKey.J)
            VirtualKeysList.Add("K", IKernel.VirtualKey.K)
            VirtualKeysList.Add("L", IKernel.VirtualKey.L)
            VirtualKeysList.Add("M", IKernel.VirtualKey.M)
            VirtualKeysList.Add("N", IKernel.VirtualKey.N)
            VirtualKeysList.Add("O", IKernel.VirtualKey.O)
            VirtualKeysList.Add("P", IKernel.VirtualKey.P)
            VirtualKeysList.Add("Q", IKernel.VirtualKey.Q)
            VirtualKeysList.Add("R", IKernel.VirtualKey.R)
            VirtualKeysList.Add("S", IKernel.VirtualKey.S)
            VirtualKeysList.Add("T", IKernel.VirtualKey.T)
            VirtualKeysList.Add("U", IKernel.VirtualKey.U)
            VirtualKeysList.Add("V", IKernel.VirtualKey.V)
            VirtualKeysList.Add("W", IKernel.VirtualKey.W)
            VirtualKeysList.Add("X", IKernel.VirtualKey.X)
            VirtualKeysList.Add("Y", IKernel.VirtualKey.Y)
            VirtualKeysList.Add("Z", IKernel.VirtualKey.Z)
            VirtualKeysList.Add("NumPad 0", IKernel.VirtualKey.NumPad0)
            VirtualKeysList.Add("NumPad 1", IKernel.VirtualKey.NumPad1)
            VirtualKeysList.Add("NumPad 2", IKernel.VirtualKey.NumPad2)
            VirtualKeysList.Add("NumPad 3", IKernel.VirtualKey.NumPad3)
            VirtualKeysList.Add("NumPad 4", IKernel.VirtualKey.NumPad4)
            VirtualKeysList.Add("NumPad 5", IKernel.VirtualKey.NumPad5)
            VirtualKeysList.Add("NumPad 6", IKernel.VirtualKey.NumPad6)
            VirtualKeysList.Add("NumPad 7", IKernel.VirtualKey.NumPad7)
            VirtualKeysList.Add("NumPad 8", IKernel.VirtualKey.NumPad8)
            VirtualKeysList.Add("NumPad 9", IKernel.VirtualKey.NumPad9)
            VirtualKeysList.Add("F1", IKernel.VirtualKey.F1)
            VirtualKeysList.Add("F2", IKernel.VirtualKey.F2)
            VirtualKeysList.Add("F3", IKernel.VirtualKey.F3)
            VirtualKeysList.Add("F4", IKernel.VirtualKey.F4)
            VirtualKeysList.Add("F5", IKernel.VirtualKey.F5)
            VirtualKeysList.Add("F6", IKernel.VirtualKey.F6)
            VirtualKeysList.Add("F7", IKernel.VirtualKey.F7)
            VirtualKeysList.Add("F8", IKernel.VirtualKey.F8)
            VirtualKeysList.Add("F9", IKernel.VirtualKey.F9)
            VirtualKeysList.Add("F10", IKernel.VirtualKey.F10)
            VirtualKeysList.Add("F11", IKernel.VirtualKey.F11)
            VirtualKeysList.Add("F12", IKernel.VirtualKey.F12)
            VirtualKeysList.Add("F13", IKernel.VirtualKey.F13)
            VirtualKeysList.Add("F14", IKernel.VirtualKey.F14)
            VirtualKeysList.Add("F15", IKernel.VirtualKey.F15)
            VirtualKeysList.Add("F16", IKernel.VirtualKey.F16)
            VirtualKeysList.Add("F17", IKernel.VirtualKey.F17)
            VirtualKeysList.Add("F18", IKernel.VirtualKey.F18)
            VirtualKeysList.Add("F19", IKernel.VirtualKey.F19)
            VirtualKeysList.Add("F20", IKernel.VirtualKey.F20)
            VirtualKeysList.Add("F21", IKernel.VirtualKey.F21)
            VirtualKeysList.Add("F22", IKernel.VirtualKey.F22)
            VirtualKeysList.Add("F23", IKernel.VirtualKey.F23)
            VirtualKeysList.Add("F24", IKernel.VirtualKey.F24)
            VirtualKeysList.Add(";", IKernel.VirtualKey.Semicolon)
            VirtualKeysList.Add(":", IKernel.VirtualKey.Semicolon)
            VirtualKeysList.Add("=", IKernel.VirtualKey.Plus)
            VirtualKeysList.Add(",", IKernel.VirtualKey.Comma)
            VirtualKeysList.Add("-", IKernel.VirtualKey.Minus)
            VirtualKeysList.Add(".", IKernel.VirtualKey.Dot)
            VirtualKeysList.Add("/", IKernel.VirtualKey.ForwardSlash)
            VirtualKeysList.Add("?", IKernel.VirtualKey.ForwardSlash)
            VirtualKeysList.Add("~", IKernel.VirtualKey.Tick)
            VirtualKeysList.Add("`", IKernel.VirtualKey.Tick)
            VirtualKeysList.Add("[", IKernel.VirtualKey.LeftSquareBracket)
            VirtualKeysList.Add("{", IKernel.VirtualKey.LeftSquareBracket)
            VirtualKeysList.Add("\", IKernel.VirtualKey.BackwardSlash)
            VirtualKeysList.Add("|", IKernel.VirtualKey.BackwardSlash)
            VirtualKeysList.Add("]", IKernel.VirtualKey.RightSquareBracket)
            VirtualKeysList.Add("}", IKernel.VirtualKey.RightSquareBracket)
            VirtualKeysList.Add("'", IKernel.VirtualKey.SingleQuote)
            VirtualKeysList.Add("""", IKernel.VirtualKey.SingleQuote)
            VirtualKeysList.Add("NumPad /", IKernel.VirtualKey.NumPadDivide)
            VirtualKeysList.Add("NumPad *", IKernel.VirtualKey.NumPadMultiply)
            VirtualKeysList.Add("NumPad +", IKernel.VirtualKey.NumPadAdd)
            VirtualKeysList.Add("NumPad -", IKernel.VirtualKey.NumPadSubtract)
            VirtualKeysList.Add("NumPad .", IKernel.VirtualKey.NumPadDecimal)
            For Each VK As String In VirtualKeysList.Keys
                PressKeyKeyList.Items.Add(VK)
            Next
            PressKeyKeyList.SelectedIndex = 0
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub HKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyF1.CheckedChanged, HKeyF8.CheckedChanged, HKeyF7.CheckedChanged, HKeyF6.CheckedChanged, HKeyF5.CheckedChanged, HKeyF4.CheckedChanged, HKeyF3.CheckedChanged, HKeyF2.CheckedChanged, HKeyF9.CheckedChanged, HKeyF10.CheckedChanged, HKeyZ.CheckedChanged, HKeyY.CheckedChanged, HKeyX.CheckedChanged, HKeyW.CheckedChanged, HKeyV.CheckedChanged, HKeyU.CheckedChanged, HKeyTick.CheckedChanged, HKeyT.CheckedChanged, HKeySColon.CheckedChanged, HKeyS.CheckedChanged, HKeyRBrac.CheckedChanged, HKeyR.CheckedChanged, HKeyQ.CheckedChanged, HKeyPgUp.CheckedChanged, HKeyPgDn.CheckedChanged, HKeyP.CheckedChanged, HKeyO.CheckedChanged, HKeyNPSubtract.CheckedChanged, HKeyNPMultiply.CheckedChanged, HKeyNPDivide.CheckedChanged, HKeyNPDecimal.CheckedChanged, HKeyNPAdd.CheckedChanged, HKeyNP9.CheckedChanged, HKeyNP8.CheckedChanged, HKeyNP7.CheckedChanged, HKeyNP6.CheckedChanged, HKeyNP5.CheckedChanged, HKeyNP4.CheckedChanged, HKeyNP3.CheckedChanged, HKeyNP2.CheckedChanged, HKeyNP1.CheckedChanged, HKeyNP0.CheckedChanged, HKeyN.CheckedChanged, HKeyMinus.CheckedChanged, HKeyM.CheckedChanged, HKeyLBrac.CheckedChanged, HKeyL.CheckedChanged, HKeyK.CheckedChanged, HKeyJ.CheckedChanged, HKeyInsert.CheckedChanged, HKeyI.CheckedChanged, HKeyHome.CheckedChanged, HKeyH.CheckedChanged, HKeyG.CheckedChanged, HKeyFStop.CheckedChanged, HKeyFSlsh.CheckedChanged, HKeyF12.CheckedChanged, HKeyF11.CheckedChanged, HKeyF.CheckedChanged, HKeyEqual.CheckedChanged, HKeyEnd.CheckedChanged, HkeyE.CheckedChanged, HKeyDelete.CheckedChanged, HKeyD.CheckedChanged, HKeyComma.CheckedChanged, HKeyC.CheckedChanged, HKeyBSlsh.CheckedChanged, HKeyB.CheckedChanged, HKeyApost.CheckedChanged, HKeyA.CheckedChanged, HKey9.CheckedChanged, HKey8.CheckedChanged, HKey7.CheckedChanged, HKey6.CheckedChanged, HKey5.CheckedChanged, HKey4.CheckedChanged, HKey3.CheckedChanged, HKey2.CheckedChanged, HKey1.CheckedChanged, HKey0.CheckedChanged
        Try
            VK = CType(sender, RadioButton).Tag.ToString
            SelectedVK = System.Enum.Parse(GetType(IKernel.VirtualKey), VK)
            RefreshCombination()
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub RefreshCombination()
        Try
            If Not String.IsNullOrEmpty(VK) Then
                Dim Output As String = VK
                SelectedModifier = IKernel.KeyboardModifier.None
                If HKeyShift.Checked Then
                    Output = "Shift + " & Output
                    SelectedModifier = SelectedModifier Or IKernel.KeyboardModifier.Shift
                End If
                If HKeyAlt.Checked Then
                    Output = "Alt + " & Output
                    SelectedModifier = SelectedModifier Or IKernel.KeyboardModifier.Alt
                End If
                If HKeyCtrl.Checked Then
                    Output = "Ctrl + " & Output
                    SelectedModifier = SelectedModifier Or IKernel.KeyboardModifier.Ctrl
                End If
                Name = SelectedVK.ToString & ";" & SelectedModifier.ToString
                SelectedCombinationLabel.Text = Output
                If Kernel.KeyboardEntries.ContainsKey(Name) Then
                    Dim Entry As IKernel.KeyboardEntry = Kernel.KeyboardEntries(Name)
                    Select Case Entry.Action
                        Case IKernel.KeyboardEntryAction.PressKey
                            WithSelectedPressKey.Checked = True
                            Dim Names() As String = Name.Split(";"c)
                            PressKeyKeyList.Select(PressKeyKeyList.Items.IndexOf(Names(0)), 1)
                            PressKeyShift.Checked = Entry.NewModifier And IKernel.KeyboardModifier.Shift
                            PressKeyAlt.Checked = Entry.NewModifier And IKernel.KeyboardModifier.Alt
                            PressKeyCtrl.Checked = Entry.NewModifier And IKernel.KeyboardModifier.Ctrl
                        Case Else
                            WithSelectedNothing.Checked = True
                    End Select
                Else
                    WithSelectedNothing.Checked = True
                End If
            Else
                Beep()
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub KeyboardActivateButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyboardActivateButton.CheckedChanged
        Try
            Dim PPB As New PipePacketBuilder(Kernel.Client.Pipe)
            If KeyboardActivateButton.Checked Then
                PPB.Keyboard(True)
            Else
                PPB.Keyboard(False)
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub HKeyState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HKeyCtrl.CheckedChanged, HKeyShift.CheckedChanged, HKeyAlt.CheckedChanged
        Try
            If Not String.IsNullOrEmpty(VK) Then
                RefreshCombination()
            Else
                Beep()
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add.Click
        Try
            Dim NewEntry As IKernel.KeyboardEntry
            NewEntry.Name = SelectedVK.ToString & ";" & SelectedModifier.ToString
            Dim Output As String = String.Empty
            If WithSelectedPressKey.Checked Then
                NewEntry.Action = IKernel.KeyboardEntryAction.PressKey
                NewEntry.OldVirtualKey = SelectedVK
                NewEntry.OldModifier = SelectedModifier
                NewEntry.NewVirtualKey = System.Enum.Parse(GetType(IKernel.VirtualKey), PressKeyKeyList.SelectedItem.ToString)
                NewEntry.NewModifier = IKernel.KeyboardModifier.None
                If PressKeyAlt.Checked Then
                    NewEntry.NewModifier = NewEntry.NewModifier Or IKernel.KeyboardModifier.Alt
                End If
                If PressKeyShift.Checked Then
                    NewEntry.NewModifier = NewEntry.NewModifier Or IKernel.KeyboardModifier.Shift
                End If
                If PressKeyCtrl.Checked Then
                    NewEntry.NewModifier = NewEntry.NewModifier Or IKernel.KeyboardModifier.Ctrl
                End If
                If Kernel.KeyboardEntries.ContainsKey(NewEntry.Name) Then
                    Kernel.KeyboardEntries(NewEntry.Name) = NewEntry
                Else
                    Kernel.KeyboardEntries.Add(NewEntry.Name, NewEntry)
                End If
                KeyboardEntriesGrid.Rows.Clear()
                For Each Entr As KeyValuePair(Of String, IKernel.KeyboardEntry) In Kernel.KeyboardEntries
                    KeyboardEntriesGrid.Rows.Add(Entr.Value.OldVirtualKey.ToString, _
                        (Entr.Value.OldModifier And IKernel.KeyboardModifier.Ctrl) = IKernel.KeyboardModifier.Ctrl, _
                        (Entr.Value.OldModifier And IKernel.KeyboardModifier.Alt) = IKernel.KeyboardModifier.Alt, _
                        (Entr.Value.OldModifier And IKernel.KeyboardModifier.Shift) = IKernel.KeyboardModifier.Shift)
                    'ListBox1.Items.Add(Output)
                Next
            Else

            End If
            'ListBox1.Items.Clear()s

        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub



End Class