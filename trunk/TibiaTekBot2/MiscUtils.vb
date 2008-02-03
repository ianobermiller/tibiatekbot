'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.

Imports TibiaTekBot.KernelModule, System.IO, System.Math, Scripting, System.Text.RegularExpressions, System.Xml

Module MiscUtils

    Public Function GetConfigurationDirectory() As String
        Return My.Application.Info.DirectoryPath & "\Config"
    End Function

    Public Function TimeSpanToString(ByVal Time As TimeSpan) As String
        If Time.Days > 0 Then
            Return Time.Days & "d" & Time.Hours & "h" & Time.Minutes & "m" & Time.Seconds & "s"
        Else
            Return Time.Hours & "h" & Time.Minutes & "m" & Time.Seconds & "s"
        End If
    End Function

    Public Function MessageIsSpell(ByVal Message As String) As Boolean
        Dim Group1() As String = {"ad", "al", "ex", "ut"}
        Dim Group2() As String = {"amo", "ana", "ani", "eta", "evo", "ito", "iva", "ori", "ura"}
        For Each Gr1 As String In Group1
            For Each Gr2 As String In Group2
                If Regex.IsMatch(Message, "^" & Gr1 & "\s*" & Gr2, RegexOptions.IgnoreCase) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Public Function GetWaypointsDirectory() As String
        Return My.Application.Info.DirectoryPath & "\Waypoints"
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
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                End Select
            End If
            Return Result
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetInventorySlotAsLocation(ByVal Slot As ITibia.InventorySlots) As ITibia.LocationDefinition
        Try
            Return New ITibia.LocationDefinition(&HFFFF, Slot, 0)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Sub Log(ByVal Source As String, ByVal Text As String)
        If Kernel.LoggingEnabled OrElse Consts.DebugOnLog Then
            Try
                Dim TextFile As New StreamWriter(Kernel.ExecutablePath & "\Log.txt", True)
                TextFile.WriteLine("(" & Source & ") " & Date.Now.ToLongDateString & ": " & Text)
                TextFile.Close()
            Catch
            End Try
        End If
    End Sub

    Public Sub StopPlayer()
        Try
            Dim BL As New BattleList
            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
            BL.IsWalking = False
            'Core.ConsoleWrite("Stop Player: STOP")
            Dim ServerPacket As New ServerPacketBuilder(Kernel.Proxy)
            ServerPacket.StopEverything()
            'Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
            Kernel.Client.WriteMemory(Consts.ptrGoToX, 0, 4)
            Kernel.Client.WriteMemory(Consts.ptrGoToY, 0, 4)
            Kernel.Client.WriteMemory(Consts.ptrGoToZ, 0, 1)
            BL.IsWalking = True
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub InjectLastAttackedId()
        Try

            Dim CodeCave As Integer = &H596C3A
            'I'd like to tell about this function first. Because we can't surely find any address
            'where is the last attacked Id we need to create one. So I find the place where attacked
            'Id is writed to the memory, and made code cave where I write that value in another place of
            'the memory if it's not zero
            'Things to know: Adr where Tibia put's atkd id: 450DC3
            '                New LastAttackedEntityId: 76DA10
            '                CodeCave: 5920B3
            '                Continue Old Code: 450DC9
            'Offset 450DC3 . The place where Tibia puts attacked Id to the memory (adr: 60EA9C)
            Kernel.Client.WriteMemory(&H451803, &H145432E9, 5) ' JMP 596c3a
            Kernel.Client.WriteMemory(&H451808, &H90, 1) 'NOP
            'Offset 592040 . Our codecaves
            Kernel.Client.WriteMemory(CodeCave, &HFE83, 3) : CodeCave += 3 'CMP ESI,0
            Kernel.Client.WriteMemory(CodeCave, &H674, 2) : CodeCave += 2 'JE 59204B
            Kernel.Client.WriteMemory(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [0076DA10],ESI
            Kernel.Client.WriteMemory(CodeCave, &H76DA10, 4) : CodeCave += 4 '---------"--------
            Kernel.Client.WriteMemory(CodeCave, &H3589, 2) : CodeCave += 2 'MOV [613B3C],ESI
            Kernel.Client.WriteMemory(CodeCave, &H613B3C, 4) : CodeCave += 4 '------"---------
            Kernel.Client.WriteMemory(CodeCave, &HE9, 1) : CodeCave += 1 'JMP 450DC9
            Kernel.Client.WriteMemory(CodeCave, &HFFEBABB9, 4) ' ---"----
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UpdatePlayerPos()
        Try
            Kernel.Client.ReadMemory(Consts.ptrCoordX, Kernel.CharacterLoc.X, 4)
            Kernel.Client.ReadMemory(Consts.ptrCoordY, Kernel.CharacterLoc.Y, 4)
            Kernel.Client.ReadMemory(Consts.ptrCoordZ, Kernel.CharacterLoc.Z, 1)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Function SelectNearestWaypoint(ByVal Waypoints As List(Of Walker)) As Integer
        Try
            Dim X, Y, Z As New Integer
            Dim WaypointBuffer As New List(Of Walker)
            Dim NearestDist As Double = 999999.0
            Dim Dist As Double = 0.0
            Z = Kernel.CharacterLoc.Z

            'Let's find every waypoint which is at the same floor
            For Each Waypoint As Walker In Waypoints
                If Waypoint.Coordinates.Z = Z Then
                    WaypointBuffer.Add(Waypoint)
                End If
            Next
            'No waypoints found
            If WaypointBuffer.Count = 0 Then
                Return -1
            End If
            Dim NearestWaypointIndex As Integer = 0

            For I As Integer = 0 To Waypoints.Count - 1
                If Z <> Waypoints(I).Coordinates.Z Then
                    Continue For
                End If
                X = Abs(Waypoints(I).Coordinates.X - Kernel.CharacterLoc.X)
                Y = Abs(Waypoints(I).Coordinates.Y - Kernel.CharacterLoc.Y)
                Dist = Sqrt(Pow(X, 2) + Pow(Y, 2))

                If Dist < NearestDist Then
                    NearestDist = Dist
                    NearestWaypointIndex = I
                End If
            Next
            Return NearestWaypointIndex
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function InjectCode(ByVal Address As Integer, ByVal OpCodes() As Byte) As Boolean
        Try
            For I As Integer = 0 To OpCodes.Length - 1
                Kernel.Client.WriteMemory(Address + I, OpCodes(I), 1)
            Next
            Return True
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
End Module
