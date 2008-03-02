Imports Scripting, System.Text.RegularExpressions, System.Threading, System.Xml
Public Class frmAutoResponder

    Dim SelectedRowIndex As Integer
    Dim MatchFound As Boolean = False
    Dim WithEvents Client As ITibia = Kernel.Client
    Dim AR_Activated As Boolean = False

    Enum MsgTypes As Integer
        [Private] = 0
        [Default] = 1
        PrivateOrDefault = 2
    End Enum

    Enum ExpType As Integer
        RegExp = 0
        Normaltext = 1
    End Enum

    Private Sub AddAnswerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddAnswerToolStripMenuItem.Click
        On Error Resume Next
        Dim Answer As String
        Answer = InputBox("Type the new response", "TibiaTek Bot - Auto Responder")
        Dim Combo As New DataGridViewComboBoxCell
        Combo = DataGridView1.Rows(SelectedRowIndex).Cells(1)
        Combo.Items.Add(Answer)
        Combo.Value = Answer
        ActivateBtn.Focus()
        DataGridView1.Refresh()
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        e.Cancel = True
    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        SelectedRowIndex = e.RowIndex
    End Sub

    Private Sub DeleteAnswerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAnswerToolStripMenuItem.Click
        On Error Resume Next
        Dim Combo As New DataGridViewComboBoxCell
        Combo = DataGridView1.Rows(SelectedRowIndex).Cells(1)

        If Combo.Items.Count = 1 Then
            Combo.Items.Clear()
            Combo.Value = ""
            DataGridView1.Refresh()
            ActivateBtn.Focus()
            Exit Sub
        End If

        Combo.Items.Remove(Combo.Value)

        DataGridView1.Refresh()
        ActivateBtn.Focus()
    End Sub

    Private Sub ActivateBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivateBtn.Click
        If ActivateBtn.Text = "Activate" Then
            ActivateBtn.Text = "Desactivate"
            LoadBtn.Enabled = False
            SaveBtn.Enabled = False
            ClearBtn.Enabled = False
            LblChkDist.Enabled = False
            CntChkDist.Enabled = False
            ChkDistance.Enabled = False
            AR_Activated = True
        Else
            ActivateBtn.Text = "Activate"
            AR_Activated = False
            LoadBtn.Enabled = True
            SaveBtn.Enabled = True
            ClearBtn.Enabled = True
            LblChkDist.Enabled = True
            CntChkDist.Enabled = True
            ChkDistance.Enabled = True
        End If
    End Sub

    Private Sub DataGridView1_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowLeave
        On Error Resume Next
        Dim tmpStr As String = ""
        tmpStr = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        If String.IsNullOrEmpty(tmpStr) Then
            DataGridView1.Rows(e.RowIndex).Cells(2).Value = 0
        End If
        tmpStr = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        If String.IsNullOrEmpty(tmpStr) Then
            DataGridView1.Rows(e.RowIndex).Cells(3).Value = "Normal text"
        End If
        tmpStr = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        If String.IsNullOrEmpty(tmpStr) Then
            DataGridView1.Rows(e.RowIndex).Cells(4).Value = "Private"
        End If
    End Sub

    Private Sub DeleteRowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRowToolStripMenuItem.Click
        If DataGridView1.Rows.Count > 1 Then
            If (SelectedRowIndex + 1) = DataGridView1.Rows.Count Then Exit Sub
            DataGridView1.Rows.RemoveAt(SelectedRowIndex)
        Else
            DataGridView1.Rows.Clear()
        End If
    End Sub

    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        Try
            If Me.DataGridView1.Rows.Count = 1 Then
                MsgBox("There's no Auto Responders to save.", MsgBoxStyle.Question, "Cannot save the AutoResponder file")
                Exit Sub
            End If
            Dim SaveDlg As New SaveFileDialog
            Dim WalkerChar As New Walker
            With SaveDlg
                .InitialDirectory = GetWaypointsDirectory() & "\"
                .FileName = Kernel.Client.CharacterName & ".AutoResponder.xml"
                .DefaultExt = "xml"
                .Title = BotName & " - Save the AutoResponder file"
                .Filter = "Xml File|*.xml"
            End With
            If SaveDlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            Dim Document As New XmlDocument
            Dim xmlResponders As XmlElement = Document.CreateElement("AutoResponder")
            Dim xmlRespAttribute As Xml.XmlAttribute
            If Me.DataGridView1.Rows.Count = 1 Then Exit Sub
            xmlRespAttribute = Document.CreateAttribute("CheckDist")
            If Me.ChkDistance.Checked Then
                xmlRespAttribute.InnerText = Me.CntChkDist.Value
            Else
                xmlRespAttribute.InnerText = 0
            End If
            xmlResponders.Attributes.Append(xmlRespAttribute)

            For Each Row_ As DataGridViewRow In Me.DataGridView1.Rows
                Dim xmlResponder As XmlElement = Document.CreateElement("Responder")
                Dim tmpString As String = ""

                tmpString = Row_.Cells(0).Value

                If String.IsNullOrEmpty(tmpString) Then Continue For

                xmlRespAttribute = Document.CreateAttribute("Msg")
                xmlRespAttribute.InnerText = Row_.Cells(0).Value
                xmlResponder.Attributes.Append(xmlRespAttribute)

                xmlRespAttribute = Document.CreateAttribute("Wait")
                xmlRespAttribute.InnerText = Row_.Cells(2).Value
                xmlResponder.Attributes.Append(xmlRespAttribute)

                xmlRespAttribute = Document.CreateAttribute("ExpType")
                xmlRespAttribute.InnerText = Row_.Cells(3).Value
                xmlResponder.Attributes.Append(xmlRespAttribute)

                xmlRespAttribute = Document.CreateAttribute("MsgType")
                xmlRespAttribute.InnerText = Row_.Cells(4).Value
                xmlResponder.Attributes.Append(xmlRespAttribute)

                Dim combo As New DataGridViewComboBoxCell
                combo = Row_.Cells(1)

                If combo.Items.Count = 0 Then Continue For

                For Each Resp As String In combo.Items
                    Dim xmlSay As XmlElement = Document.CreateElement("Say")
                    xmlSay.InnerText = Resp
                    xmlResponder.AppendChild(xmlSay)
                Next
                xmlResponders.AppendChild(xmlResponder)
                Application.DoEvents()
            Next
            Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
            Document.AppendChild(Declaration)
            Document.AppendChild(xmlResponders)
            Document.Save(SaveDlg.FileName)

        Catch ex As Exception
            MessageBox.Show("Unable to Save the AutoResponder file", "TibiaTek Bot - AutoResponder", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBtn.Click
        Try
            Dim OpenDlg As New OpenFileDialog

            With OpenDlg
                .InitialDirectory = "\"
                .Title = "Tibiatek Bot - Load AutoResponder file"
                .DefaultExt = "xml"
                .Filter = ".xml Files|*.xml"
            End With
            If OpenDlg.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            Dim Document As New XmlDocument
            Document.Load(OpenDlg.FileName)
            Dim AR_ChckDist As Integer = 0
            AR_ChckDist = CInt(Document.Item("AutoResponder").GetAttribute("CheckDist"))
            If AR_ChckDist > 0 Then
                ChkDistance.CheckState = CheckState.Checked
                LblChkDist.Enabled = True
                CntChkDist.Enabled = True
                CntChkDist.Value = AR_ChckDist
            Else
                ChkDistance.CheckState = CheckState.Unchecked
                LblChkDist.Enabled = False
                CntChkDist.Enabled = False
                CntChkDist.Value = 0
            End If

            Dim AR_Msg As String = ""
            Dim AR_Wait As Integer
            Dim AR_ExpType As String
            Dim AR_MsgType As String
            Dim AR_Say As String = ""
            Dim AR_SayCnt As Integer = 0
            Dim Combo As DataGridViewComboBoxCell = Nothing


            For Each Element As XmlElement In Document.Item("AutoResponder")
                AR_Msg = IIf(Element.GetAttribute("Msg") <> "", Element.GetAttribute("Msg"), "")
                AR_Wait = IIf(Element.GetAttribute("Wait") <> "", CInt(Element.GetAttribute("Wait")), 0)
                AR_ExpType = IIf(Element.GetAttribute("ExpType") <> "", Element.GetAttribute("ExpType"), "")
                AR_MsgType = IIf(Element.GetAttribute("MsgType") <> "", Element.GetAttribute("MsgType"), "")

                Dim Row As New DataGridViewRow
                Row.CreateCells(DataGridView1)

                With Row.Cells
                    .Item(0).Value = AR_Msg
                    Combo = .Item(1)
                    Combo.Items.Clear()
                    For Each SubElement As XmlElement In Element.ChildNodes
                        AR_Say = SubElement.InnerText
                        Combo.Items.Add(AR_Say)
                    Next
                    Combo.Value = AR_Say
                    .Item(2).Value = AR_Wait
                    .Item(3).Value = AR_ExpType
                    .Item(4).Value = AR_MsgType
                End With
                DataGridView1.Rows.Add(Row)
                Row = Nothing
                Application.DoEvents()
            Next
        Catch ex As Exception
            MessageBox.Show("Unable to Load the AutoResponder file", "TibiaTek Bot - AutoResponder", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearBtn.Click
        Try
            If Me.DataGridView1.Rows.Count = 1 Then
                Exit Sub
            End If
            If MessageBox.Show("Are you sure you want to clear the Auto responder", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            DataGridView1.Rows.Clear()

        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub ChkDistance_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDistance.CheckedChanged
        If ChkDistance.CheckState = CheckState.Checked Then
            LblChkDist.Enabled = True
            CntChkDist.Enabled = True
        Else
            LblChkDist.Enabled = False
            CntChkDist.Enabled = False
            CntChkDist.Value = 0
        End If
    End Sub

    Private Sub Client_MessageReceived(ByVal e As Scripting.Events.Events.MessageReceivedEventArgs) Handles Client.MessageReceived
        If Not (AR_Activated Or Kernel.Client.IsConnected) Then Exit Sub
        If e.CharacterName = Client.CharacterName Then Exit Sub

        Dim i As Integer = 0
        Dim Match As Match
        Dim Text As String = ""
        Dim ComboResp As New DataGridViewComboBoxCell
        Dim ComboMsgType As New DataGridViewComboBoxCell
        Dim ComboExpType As New DataGridViewComboBoxCell
        Dim AR_MsgType As MsgTypes = 0
        Dim AR_ExpType As ExpType = 0
        Dim RndNum As Integer = 0
        Dim Matched As Boolean = False
        Dim Response As String = ""

        If DataGridView1.Rows.Count < 2 Then Exit Sub
        If e.MessageType = ITibia.MessageType.Channel Then Exit Sub
        If e.MessageType = ITibia.MessageType.Default Then
            If ChkDistance.Checked Then
                Dim BL As New BattleList
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                Dim Distance As Double = BL.GetDistanceFromLocation(e.CharacterLocation)
                If Distance Then
                    If Distance > CntChkDist.Value Then
                        Exit Sub
                    End If
                End If
            End If
        End If

        For i = 0 To DataGridView1.Rows.Count - 1
            Matched = False
            Text = DataGridView1.Rows(i).Cells(0).Value
            If Text = "" Or Text = Nothing Then Continue For

            ComboExpType = DataGridView1.Rows(i).Cells(3)
            Select Case CType(ComboExpType.Value, String)
                Case "RegExp"
                    AR_ExpType = ExpType.RegExp
                Case "NormalText"
                    AR_ExpType = ExpType.Normaltext
            End Select

            ComboMsgType = DataGridView1.Rows(i).Cells(4)

            Select Case CType(DataGridView1.Rows(i).Cells(4).Value, String)
                Case "Private"
                    AR_MsgType = MsgTypes.Private
                Case "Default"
                    AR_MsgType = MsgTypes.Default
                Case "Private or Default"
                    AR_MsgType = MsgTypes.PrivateOrDefault
            End Select

            If AR_MsgType = MsgTypes.Default And e.MessageType = ITibia.MessageType.Default Then
            ElseIf AR_MsgType = MsgTypes.Private And e.MessageType = ITibia.MessageType.PrivateMessage Then
            ElseIf AR_MsgType = MsgTypes.PrivateOrDefault And (e.MessageType = ITibia.MessageType.Default Or e.MessageType = ITibia.MessageType.PrivateMessage) Then
            Else
                Continue For
            End If

            If AR_ExpType = ExpType.RegExp Then
                Match = Regex.Match(e.Message, Text, RegexOptions.IgnoreCase Or RegexOptions.ExplicitCapture)
                Matched = Match.Success
            ElseIf AR_ExpType = ExpType.Normaltext Then
                If e.Message.ToLower.Contains(Text) Then
                    Matched = True
                End If
            End If

            If Matched Then
                ComboResp = DataGridView1.Rows(i).Cells(1)

                If ComboResp.Items.Count > 1 Then
                    Wait(Val(DataGridView1.Rows(i).Cells(2).Value))
                    RndNum = RandomNumber(ComboResp.Items.Count)
                    Response = ComboResp.Items(RndNum).ToString
                ElseIf ComboResp.Items.Count = 1 Then
                    Wait(Val(DataGridView1.Rows(i).Cells(2).Value))
                    Response = ComboResp.Items(0).ToString
                End If
                Dim sp As New ServerPacketBuilder(Kernel.Proxy)
                Dim cp As New ClientPacketBuilder(Kernel.Proxy)
                Dim strString As String = ""

                If AR_MsgType = MsgTypes.Default And e.MessageType = ITibia.MessageType.Default Then
                    sp.Speak(Response)
                    strString = "[AR Default]: " & e.CharacterName & "[" & e.CharacterLevel & "]: "
                    strString &= e.Message & Ret
                    strString &= "Responded with: " & Response
                    cp.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Tutor, strString)
                ElseIf AR_MsgType = MsgTypes.Private And e.MessageType = ITibia.MessageType.PrivateMessage Then
                    sp.Speak(e.CharacterName, Response)
                    strString = "[AR Private]: " & e.CharacterName & "[" & e.CharacterLevel & "]: "
                    strString &= e.Message & Ret
                    strString &= "Responded with: " & Response
                    cp.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Tutor, strString)
                ElseIf AR_MsgType = MsgTypes.PrivateOrDefault Then
                    Select Case e.MessageType
                        Case ITibia.MessageType.Default
                            sp.Speak(Response)
                            strString = "[AR Default]: " & e.CharacterName & "[" & e.CharacterLevel & "]: "
                            strString &= e.Message & Ret
                            strString &= "Responded with: " & Response
                            cp.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Tutor, strString)
                        Case ITibia.MessageType.PrivateMessage
                            sp.Speak(e.CharacterName, Response)
                            strString = "[AR Private]: " & e.CharacterName & "[" & e.CharacterLevel & "]: "
                            strString &= e.Message & Ret
                            strString &= "Responded with: " & Response
                            cp.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Tutor, strString)
                    End Select
                End If
            End If
            ComboExpType = Nothing
            ComboMsgType = Nothing
            ComboResp = Nothing
        Next
    End Sub

    Private Sub Wait(ByVal Seconds As Integer)
        Dim Time As Date
        Dim Elapsed As Double = 0
        If Seconds = 0 Then Exit Sub

        Time = Date.Now
        While True
            Elapsed = (Date.Now - Time).TotalSeconds
            If Elapsed > Seconds Then
                Exit While
            End If
            Application.DoEvents()
        End While
    End Sub

    Public Function RandomNumber(ByVal MaxNumber As Integer, _
            Optional ByVal MinNumber As Integer = 0) As Integer
        Randomize()

        Dim r As New Random(System.DateTime.Now.Millisecond)

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function

    Private Sub frmAutoResponder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

End Class
