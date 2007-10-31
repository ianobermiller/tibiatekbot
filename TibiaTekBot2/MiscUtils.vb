Imports TibiaTekBot.CoreModule, System.IO, System.Math

Module MiscUtils

    Public Function GetConfigurationDirectory() As String
        Try
            Dim ExecutablePath As String = ""
            For I As Integer = Application.ExecutablePath.Length - 1 To 0 Step -1
                If Application.ExecutablePath.Chars(I) = "\" Then
                    ExecutablePath = Strings.Left(Application.ExecutablePath, I)
                    Exit For
                End If
            Next
            If Not IO.Directory.Exists(ExecutablePath & "\Config") Then
                IO.Directory.CreateDirectory(ExecutablePath & "\Config")
            End If
            Return ExecutablePath & "\Config"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function FPSXToB(ByVal X As Double) As Double
        Try
            Return Round(1100 / (X + 5), 1)
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function FPSBToX(ByVal B As Double) As Double
        Try
            Return Round((1110 / B) - 5, 1)
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetWaypointsDirectory() As String
        Try
            Dim ExecutablePath As String = ""
            For I As Integer = Application.ExecutablePath.Length - 1 To 0 Step -1
                If Application.ExecutablePath.Chars(I) = "\" Then
                    ExecutablePath = Strings.Left(Application.ExecutablePath, I)
                    Exit For
                End If
            Next
            If Not IO.Directory.Exists(ExecutablePath & "\Waypoints") Then
                IO.Directory.CreateDirectory(ExecutablePath & "\Waypoints")
            End If
            Return ExecutablePath & "\Waypoints"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function StrToShort(ByVal S As String) As Int16
        Try
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
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function StrictStrToBool(ByVal S As String) As Boolean
        Try
            Dim Result As Boolean = False
            If Not String.IsNullOrEmpty(S) Then
                Select Case S.ToLower()
                    Case "1", "on", "true", "yes", "sim", "enable", "activate", "si"
                        Result = True
                        '    Case "0", "off", "false", "no", "nao", "disable", "deactivate"
                        'Result = False
                End Select
            End If
            Return Result
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function BytesToStr(ByVal bytBuffer As Byte(), Optional ByVal Mark As UShort = 0) As String
        Try
            Dim I As Integer
            Dim Output As String = ""
            Dim Length As Integer = CInt(bytBuffer(0)) + CInt(bytBuffer(1)) * CInt(Byte.MaxValue) + 2
            For I = 0 To Length - 1
                If I >= bytBuffer.Length Then Exit For
                If Mark > 0 And Mark = I Then
                    Output &= "|"
                Else
                    Output &= " "
                End If
                Output &= Hex(bytBuffer(I)).PadLeft(2, "0"c)
            Next
            Return Output
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetInventorySlotAsLocation(ByVal Slot As InventorySlots) As LocationDefinition
        Try
            Dim Result As New LocationDefinition
            Result.X = &HFFFF
            Result.Y = CShort(Slot)
            Result.Z = 0
            Return Result
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Sub Log(ByVal Source As String, ByVal Text As String)

        If Core.LoggingEnabled OrElse Consts.DebugOnLog Then
            Try
                Dim TextFile As New StreamWriter(Core.ExecutablePath & "\Log.txt", True)
                TextFile.WriteLine("(" & Source & ") " & Date.Now.ToLongDateString & ": " & Text)
                TextFile.Close()
            Catch
            End Try
        End If
    End Sub

    Public Sub StopPlayer()
        Try
            Dim BL As New BattleList
            BL.JumpToEntity(SpecialEntity.Myself)
            BL.IsWalking = False
            Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
            Core.WriteMemory(Consts.ptrGoToX, 0, 4)
            Core.WriteMemory(Consts.ptrGoToY, 0, 4)
            Core.WriteMemory(Consts.ptrGoToZ, 0, 1)
            BL.IsWalking = True
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub InjectLastAttackedId()
        Try
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
            Core.WriteMemory(&H450DC3, &H1412EBE9, 5) ' JMP 592040
            Core.WriteMemory(&H450DC8, &H90, 1) 'NOP
            'Offset 592040 . Our codecave
            Core.WriteMemory(CodeCave, &HFE83, 3) : CodeCave += 3 'CMP ESI,0
            Core.WriteMemory(CodeCave, &H674, 2) : CodeCave += 2 'JE 59204B
            Core.WriteMemory(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [0076DA10],ESI
            Core.WriteMemory(CodeCave, &H76DA10, 4) : CodeCave += 4 '---------"--------
            Core.WriteMemory(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [60599C],ESI
            Core.WriteMemory(CodeCave, &H60EA9C, 4) : CodeCave += 4 '------"---------
            Core.WriteMemory(CodeCave, &HE9, 1) : CodeCave += 1 'JMP 450DC9
            Core.WriteMemory(CodeCave, &HFFEBED00, 4) ' ---"----
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub UpdatePlayerPos()
        Try
            Core.ReadMemory(Consts.ptrCoordX, Core.CharacterLoc.X, 4)
            Core.ReadMemory(Consts.ptrCoordY, Core.CharacterLoc.Y, 4)
            Core.ReadMemory(Consts.ptrCoordZ, Core.CharacterLoc.Z, 1)
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

End Module
