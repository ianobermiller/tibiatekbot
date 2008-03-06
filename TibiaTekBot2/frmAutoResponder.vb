Imports Scripting, System.Text.RegularExpressions, System.Threading, System.Xml
Public Class frmAutoResponder

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Dim WithEvents Client As ITibia
    Dim SelectedRowIndex As Integer
    Public AR_Activated As Boolean = False

    Structure MsgAnswerDefinition
        Dim LastAnswer As Date
        Dim CharName As String
        Dim Expression As String
    End Structure

    Enum MsgTypes As Integer
        [Private] = 0
        [Default] = 1
        PrivateOrDefault = 2
    End Enum

    Enum ExpType As Integer
        RegExp = 0
        Normaltext = 1
    End Enum

    Public MessagesAnswered As New List(Of MsgAnswerDefinition)
    Dim TestingMode As Boolean = False
    Public Sub AREnable()
        If DataGridView1.RowCount = 1 Then
            Kernel.ConsoleError("There's no Auto Responders. Please edit your AutoResponder. Go to TibiaTek Bot Main menu and select Auto Responder.")
            Kernel.ARisActive = False
            Exit Sub
        End If
        ActivateBtn.Text = "Desactivate"
        LoadBtn.Enabled = False
        SaveBtn.Enabled = False
        ClearBtn.Enabled = False
        LblChkDist.Enabled = False
        CntChkDist.Enabled = False
        ChkDistance.Enabled = False
        RmtimeTxt.Enabled = False
        Rmlbl.Enabled = False
        AR_Activated = True
        DataGridView1.Enabled = False
        SqrLbl.Enabled = False
    End Sub
    Public Sub ARDisable()

        ActivateBtn.Text = "Activate"
        AR_Activated = False
        LoadBtn.Enabled = True
        SaveBtn.Enabled = True
        ClearBtn.Enabled = True
        LblChkDist.Enabled = True
        CntChkDist.Enabled = True
        ChkDistance.Enabled = True
        RmtimeTxt.Enabled = True
        Rmlbl.Enabled = True
        DataGridView1.Enabled = True
        SqrLbl.Enabled = True
    End Sub
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

    Private Sub DataGridView1_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellMouseEnter

        If e.RowIndex >= 0 Then
            If DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.Red Then
                StatusLblHelp.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Tag
                Exit Sub
            End If
        End If
        Select Case e.ColumnIndex
            Case 0
                StatusLblHelp.Text = "Enter the expression te be evalaluated. Example: hello"
            Case 1
                StatusLblHelp.Text = "Add the possible responses to send when a messaged matched with the expression"
            Case 2
                StatusLblHelp.Text = "Type the seconds to wait for send the answer"
            Case 3
                StatusLblHelp.Text = "Select the expression type, RegExp (Regular Exporesions) Ex: hello|hi|hey there|hiho , or Normal Text Ex: hello"
            Case 4
                StatusLblHelp.Text = "Select the Message type, Private , Default, or Private or default"
        End Select
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
            If DataGridView1.RowCount = 1 Then
                StatusLblHelp.Text = "There's no Auto Responders"
                Wait(2)
                StatusLblHelp.Text = ""
                Exit Sub
            End If
            Kernel.ARisActive = True
            AR_Activated = True
            ActivateBtn.Text = "Desactivate"
            LoadBtn.Enabled = False
            SaveBtn.Enabled = False
            ClearBtn.Enabled = False
            LblChkDist.Enabled = False
            CntChkDist.Enabled = False
            ChkDistance.Enabled = False
            RmtimeTxt.Enabled = False
            Rmlbl.Enabled = False
            DataGridView1.Enabled = False
            SqrLbl.Enabled = False
        Else
            ActivateBtn.Text = "Activate"
            AR_Activated = False
            Kernel.ARisActive = False
            LoadBtn.Enabled = True
            SaveBtn.Enabled = True
            ClearBtn.Enabled = True
            LblChkDist.Enabled = True
            CntChkDist.Enabled = True
            ChkDistance.Enabled = True
            RmtimeTxt.Enabled = True
            Rmlbl.Enabled = True
            DataGridView1.Enabled = True
            SqrLbl.Enabled = True
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
                xmlRespAttribute.InnerText = Me.CntChkDist.Text
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
                CntChkDist.Text = AR_ChckDist
            Else
                ChkDistance.CheckState = CheckState.Unchecked
                LblChkDist.Enabled = False
                CntChkDist.Enabled = False
                CntChkDist.Text = 0
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
            SqrLbl.Enabled = True
        Else
            LblChkDist.Enabled = False
            CntChkDist.Enabled = False
            SqrLbl.Enabled = False
            CntChkDist.Text = 0
        End If
    End Sub

    Private Sub Client_MessageReceived(ByVal e As Scripting.Events.Events.MessageReceivedEventArgs) Handles Client.MessageReceived
        If Not (AR_Activated) Then Exit Sub
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
            Dim Distance As Double
            If ChkDistance.Checked Then
                If Not TestingMode Then
                    Dim BL As New BattleList
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    Distance = BL.GetDistanceFromLocation(e.CharacterLocation)

                    If Distance > CntChkDist.Text Then
                        Exit Sub
                    End If
                End If
            End If
        End If

        For Each Row As DataGridViewRow In DataGridView1.Rows
            Matched = False
            Text = Row.Cells(0).Value
            If Text = "" Or Text = Nothing Then Continue For

            ' ComboExpType = DataGridView1.Rows(i).Cells(3)
            Select Case Row.Cells(3).Value.ToString
                Case "RegExp"
                    AR_ExpType = ExpType.RegExp
                Case "NormalText"
                    AR_ExpType = ExpType.Normaltext
            End Select

            ComboMsgType = Row.Cells(4)

            Select Case CType(ComboMsgType.Value, String)
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
            Dim RowBackColor As Drawing.Color = Row.DefaultCellStyle.BackColor
            If AR_ExpType = ExpType.RegExp Then
                Try
                    Match = Regex.Match(e.Message, Text, RegexOptions.IgnoreCase Or RegexOptions.ExplicitCapture)
                    Matched = Match.Success
                    Row.DefaultCellStyle.BackColor = Row.DataGridView.DefaultCellStyle.BackColor
                    Row.Cells(0).Tag = ""
                Catch ex As Exception
                    Row.DefaultCellStyle.BackColor = Drawing.Color.Red
                    Matched = False
                    StatusLblHelp.Text = "Error: Regular Expresion not valid , [Row " & SelectedRowIndex & "] ( Error msg:" & ex.Message & " )"
                    Kernel.ConsoleError("Error: Regular Expresion not valid , [Row " & SelectedRowIndex & "] ( Error msg:" & ex.Message & " )")
                    Row.Cells(0).Tag = StatusLblHelp.Text
                    Exit Sub
                End Try

            ElseIf AR_ExpType = ExpType.Normaltext Then
                If e.Message.ToLower.Contains(Text) Then
                    Matched = True
                End If
            End If

            If Matched Then
                ComboResp = Row.Cells(1)
                StatusLblHelp.Text = ""
                Dim sp As New ServerPacketBuilder(Kernel.Proxy)
                Dim cp As New ClientPacketBuilder(Kernel.Proxy)
                Dim strString As String = ""
                Dim TimeForRepeatedMsg As Integer = CInt(RmtimeTxt.Text)
                Dim TimeElapsed As Integer
                For Each MsgAnswered_ As MsgAnswerDefinition In MessagesAnswered
                    If MsgAnswered_.CharName = e.CharacterName And MsgAnswered_.Expression = Text Then
                        TimeElapsed = Date.Now.Subtract(MsgAnswered_.LastAnswer).TotalSeconds
                        If TimeElapsed >= TimeForRepeatedMsg Then
                            MessagesAnswered.Remove(MsgAnswered_)
                            Exit For
                            'Ok Respond
                        Else
                            'Ignore msg
                            If TestingMode Then
                                StatusLblHelp.Text = "Repeated message, will be ignored, " & (TimeForRepeatedMsg - TimeElapsed) & " Seconds remaining"
                            End If
                            Exit Sub
                        End If
                    End If
                Next

                Dim MsgAnswered As New MsgAnswerDefinition
                MsgAnswered.CharName = e.CharacterName
                MsgAnswered.LastAnswer = Date.Now
                MsgAnswered.Expression = Text
                MessagesAnswered.Add(MsgAnswered)

                If ComboResp.Items.Count > 1 Then
                    Wait(Val(Row.Cells(2).Value))
                    RndNum = RandomNumber(ComboResp.Items.Count, 0)
                    Response = ComboResp.Items(RndNum).ToString
                ElseIf ComboResp.Items.Count = 1 Then
                    Wait(Val(DataGridView1.Rows(i).Cells(2).Value))
                    Response = ComboResp.Items(0).ToString
                End If

                If TestingMode Then
                    TestRitchtext.AppendText(vbLf & "Bot: " & Response)
                    TestRitchtext.Select(TestRitchtext.TextLength - Response.Length - 5, Response.Length + 5)
                    TestRitchtext.SelectionColor = Drawing.Color.Gold
                    TestRitchtext.Select()
                    TestRitchtext.Select(TestRitchtext.TextLength, 1)
                    Thread.Sleep(200)
                    TestTxt.Select()
                    Exit Sub
                End If

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

    Private Sub DataGridView1_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellMouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub LoadBtn_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoadBtn.MouseEnter
        StatusLblHelp.Text = "Load a AutoResponder FIle"
    End Sub

    Private Sub LoadBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoadBtn.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub SaveBtn_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveBtn.MouseEnter
        StatusLblHelp.Text = "Save your AutoResponder"
    End Sub

    Private Sub SaveBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveBtn.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub ClearBtn_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearBtn.MouseEnter
        StatusLblHelp.Text = "Clear all"
    End Sub

    Private Sub ClearBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearBtn.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub TestTxt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestTxt.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub TestTxt_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestTxt.KeyUp
        If e.KeyCode = Keys.Enter Then
            If AR_Activated = False Then
                StatusLblHelp.Text = "Activate the AutoResponder first"
                Wait(2)
                StatusLblHelp.Text = ""
                Exit Sub
            End If
            TestingMode = True
            Dim Str As String
            Str = TestTxt.Text
            TestRitchtext.AppendText(IIf(TestRitchtext.TextLength > 0, vbLf & "Player: " & Str, "Player: " & Str))
            TestRitchtext.Select(TestRitchtext.TextLength - Str.Length - 8, Str.Length + 8)
            TestRitchtext.SelectionColor = Drawing.Color.NavajoWhite
            TestRitchtext.Select()
            TestRitchtext.Select(TestRitchtext.TextLength, 1)
            TestRitchtext.SelectedText = ""
            Thread.Sleep(200)
            TestTxt.Text = ""
            TestTxt.Select()
            Dim Loc As New Scripting.ITibia.LocationDefinition
            Loc.X = 0
            Select Case Me.TestMsgTypeCmb.Text
                Case "Private"
                    Kernel.Client.RaiseEvent(ITibia.EventKind.MessageReceived, _
                                            New Events.MessageReceivedEventArgs(ITibia.MessageType.PrivateMessage, "Player", 27, Loc, Str))

                Case "Default"
                    Kernel.Client.RaiseEvent(ITibia.EventKind.MessageReceived, _
                        New Events.MessageReceivedEventArgs(ITibia.MessageType.Default, "Player", 27, Loc, Str))

            End Select
        End If
    End Sub

    Private Sub TestRitchtext_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestRitchtext.KeyDown
        e.SuppressKeyPress = True
    End Sub

    Private Sub TestRitchtext_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TestRitchtext.MouseDown
        TestTxt.Select()
    End Sub

    Private Sub TestBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestBtn.Click
        Dim Mysize As New System.Drawing.Size
        Mysize.Height = 440
        Mysize.Width = 690
        Me.Size = Mysize
        TestBtn.Enabled = False
        TestingModeGroup.Visible = True
    End Sub

    Private Sub TestHideBtn_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestHideBtn.Click
        Dim Mysize As New System.Drawing.Size
        Mysize.Height = 320
        Mysize.Width = 690
        Me.Size = Mysize
        TestBtn.Enabled = True
        TestingModeGroup.Visible = False
    End Sub

    Private Sub Rmlbl_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rmlbl.MouseEnter
        StatusLblHelp.Text = "Enter the seconds for wait to respond to a repeated message."
    End Sub

    Private Sub Rmlbl_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Rmlbl.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub RmtimeTxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles RmtimeTxt.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub RmtimeTxt_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RmtimeTxt.MouseEnter
        StatusLblHelp.Text = "Enter the seconds for wait to respond to a message repeated"
    End Sub

    Private Sub RmtimeTxt_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles RmtimeTxt.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub TestTxt_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestTxt.MouseEnter
        StatusLblHelp.Text = "Type a message and press Enter"
    End Sub

    Private Sub TestTxt_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestTxt.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub frmAutoResponder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Client = Kernel.Client
    End Sub

    Private Sub TimerChk_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerChk.Tick
        If Kernel.ARisActive Then
            If ActivateBtn.Text = "Activate" Then
                AREnable()
            End If
        Else
            If Not ActivateBtn.Text = "Activate" Then
                ARDisable()
            End If
        End If
    End Sub

    Private Sub frmAutoResponder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Mysize As New System.Drawing.Size
        Mysize.Height = 320
        Mysize.Width = 690
        Me.Size = Mysize
        TestingModeGroup.Visible = False
    End Sub

    Private Sub CntChkDist_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CntChkDist.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TestHideBtn_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestHideBtn.MouseEnter
        StatusLblHelp.Text = "Exit from Testing Mode"
    End Sub

    Private Sub TestHideBtn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestHideBtn.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub CntChkDist_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CntChkDist.MouseEnter
        StatusLblHelp.Text = "Enter the maximum distance of the message. (Messages from Default)"
    End Sub

    Private Sub CntChkDist_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles CntChkDist.MouseLeave
        StatusLblHelp.Text = ""
    End Sub

    Private Sub LblChkDist_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblChkDist.MouseEnter
        StatusLblHelp.Text = "Enter the maximum distance of the message. (Messages from Default)"
    End Sub

    Private Sub LblChkDist_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblChkDist.MouseLeave
        StatusLblHelp.Text = ""
    End Sub
End Class
