Imports System.IO, System.Math

Public Module MiscUtils

    Public Function GetConfigurationDirectory() As String
        Try
            Return Application.StartupPath & "\Config"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function FPSXToB(ByVal X As Double) As Double
        Return Round(1100 / (X + 5), 1)
    End Function

    Public Function FPSBToX(ByVal B As Double) As Double
        Return Round((1110 / B) - 5, 1)
    End Function

    Public Function GetWaypointsDirectory() As String
        Try
            If Not IO.Directory.Exists(Application.StartupPath & "\Waypoints") Then
                IO.Directory.CreateDirectory(Application.StartupPath & "\Waypoints")
            End If
            Return Application.StartupPath & "\Waypoints"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function StrToShort(ByVal S As String) As Int16
        Dim Result As Int16 = 2
        If Not S.Length Then
            Select Case S.ToLower()
                Case "1", "on", "true", "yes", "sim", "enable", "activate", "si", "encender", "start"
                    Result = 1
                Case "0", "off", "false", "no", "nao", "disable", "deactivate", "apagar", "stop", "halt"
                    Result = 0
                Case Else
                    Result = -1
            End Select
        End If
        Return Result
    End Function

    Public Function BytesToStr(ByVal bytBuffer As Byte()) As String
        Dim I As Integer
        Dim Output As String = ""
        For I = 0 To bytBuffer.Length - 1
            Output &= Hex(bytBuffer(I)) & " "
        Next
        Return Output
    End Function

    Public Function GetInventorySlotAsLocation(ByVal Slot As InventorySlots) As LocationDefinition
        Dim Result As New LocationDefinition
        Result.X = &HFFFF
        Result.Y = CShort(Slot)
        Result.Z = 0
        Return Result
    End Function

    Public Sub AddFeature(ByVal Feature As Object)
        If Not frmMain.FtsOnBox.Items.Contains(Feature) Then
            frmMain.FtsOnBox.Items.Add(Feature)
            frmMain.FtsOnBox.SelectedItem = Feature
        End If
    End Sub

    Public Sub RemoveFeature(ByVal Feature As Object)
        If frmMain.FtsOnBox.Items.Contains(Feature) Then
            frmMain.FtsOnBox.Items.Remove(Feature)
        End If
    End Sub

    Public Sub ShowFeature(ByVal FeatureControlPanel As Control)
        frmMain.FeaturePanel.Controls.Clear()
        FeatureControlPanel.Location = New Point(12, 12)
        frmMain.FeaturePanel.Controls.Add(FeatureControlPanel)
    End Sub

    Public Sub InjectIncomingPacketInterception()
        Dim CodeCave As Integer = &H592040
        Dim TibiaCode As Integer = &H44FE68
        Dim InterceptionReplacement(0 To 7) As Byte
        InterceptionReplacement(0) = &HE8
        InterceptionReplacement(1) = &HD3
        InterceptionReplacement(2) = &H21
        InterceptionReplacement(3) = &H14
        InterceptionReplacement(4) = 0
        InterceptionReplacement(5) = &H90
        InterceptionReplacement(6) = &H90
        InterceptionReplacement(7) = &H90


        Dim InterceptionFunction(0 To &H22) As Byte
        InterceptionFunction(0) = &H50 ' push eax
        InterceptionFunction(1) = &HB8 ' mov eax, 0
        InterceptionFunction(2) = 0
        InterceptionFunction(3) = 0
        InterceptionFunction(4) = 0
        InterceptionFunction(5) = 0
        InterceptionFunction(6) = &H50 ' push eax
        InterceptionFunction(7) = &H50 ' push eax
        InterceptionFunction(8) = &HA1 ' mov eax, dword ds:[76DA28]
        InterceptionFunction(9) = &H28
        InterceptionFunction(&HA) = &HDA
        InterceptionFunction(&HB) = &H76
        InterceptionFunction(&HC) = 0
        InterceptionFunction(&HD) = &H50 ' push eax
        InterceptionFunction(&HE) = &HA1 ' mov eax, dword ds:[76DA24]
        InterceptionFunction(&HF) = &H24
        InterceptionFunction(&H10) = &HDA
        InterceptionFunction(&H11) = &H76
        InterceptionFunction(&H12) = 0
        InterceptionFunction(&H13) = &H50 ' push eax
        InterceptionFunction(&H14) = &HE8 ' call user32.SendMessageA
        InterceptionFunction(&H15) = &H2A
        InterceptionFunction(&H16) = &HD3
        InterceptionFunction(&H17) = &HDE
        InterceptionFunction(&H18) = &H7D
        InterceptionFunction(&H19) = &H58 ' pop eax
        InterceptionFunction(&H1A) = &H8D ' lea eax, dword ptr ds:[esi-A]
        InterceptionFunction(&H1B) = &H46 ' cmp eax, 0E7
        InterceptionFunction(&H1C) = &HF6
        InterceptionFunction(&H1D) = &H3D
        InterceptionFunction(&H1E) = &HE7
        InterceptionFunction(&H1F) = 0
        InterceptionFunction(&H20) = 0
        InterceptionFunction(&H21) = 0
        InterceptionFunction(&H22) = &HC3 ' ret

        Win32API.WriteProcessMemory(Core.Tibia.GetProcessHandle, CodeCave, InterceptionFunction, InterceptionFunction.Length, 0)
        Win32API.WriteProcessMemory(Core.Tibia.GetProcessHandle, TibiaCode, InterceptionReplacement, InterceptionReplacement.Length, 0)
    End Sub

    Public Sub InjectLastAttackedId()
        Dim CodeCave As Integer = &H5920B3
        'I'd like to tell about this function first. Because we can't surely find any address
        'where is the last attacked Id we need to create one. So I find the place where attacked
        'Id is writed to the memory, and made code cave where I write that value in another place of
        'the memory if it's not zero
        'Things to know: Adr where Tibia put's atkd id: 450DC3
        '                New LastAttackedEntityId: 76DA10
        '                CodeCave: 5920B3
        '                Continue Old Code: 450DC9
        'Offset 450DC3 . The place where Tibia puts attacked Id to the memory (adr: 60EA9C)

        Core.Tibia.Memory.Write(&H450DC3, &H1412EBE9, 5) ' JMP 592040
        Core.Tibia.Memory.Write(&H450DC8, &H90, 1) 'NOP
        'Offset 592040 . Our codecave
        Core.Tibia.Memory.Write(CodeCave, &HFE83, 3) : CodeCave += 3 'CMP ESI,0
        Core.Tibia.Memory.Write(CodeCave, &H674, 2) : CodeCave += 2 'JE 59204B
        Core.Tibia.Memory.Write(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [0076DA10],ESI
        Core.Tibia.Memory.Write(CodeCave, &H76DA10, 4) : CodeCave += 4 '---------"--------
        Core.Tibia.Memory.Write(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [60599C],ESI
        Core.Tibia.Memory.Write(CodeCave, &H60EA9C, 4) : CodeCave += 4 '------"---------
        Core.Tibia.Memory.Write(CodeCave, &HE9, 1) : CodeCave += 1 'JMP 450DC9
        Core.Tibia.Memory.Write(CodeCave, &HFFEBED00, 4) ' ---"----
    End Sub

    Public Function IsPrivateServer() As Boolean
        Dim ServerAddress As String = ""
        Core.Tibia.Memory.Read(Core.Consts.ptrServerAddressBegin, ServerAddress)
        If ServerAddress = "login01.tibia.com" Then
            Return False
        Else
            Return True
        End If
    End Function

End Module
