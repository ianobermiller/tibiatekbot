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

Imports System.ComponentModel, TibiaTekBot.Constants, TibiaTekBot.Winsock, _
    Scripting

Public Class PProxy2
    Implements IProxy

    Private WithEvents ClientProcess As Tibia
    Private GamePort As UInt16

    Public LastAction As Long

    Public LoginPort As UInt16
    'Private strCharName As String = ""

    Public WithEvents sckLListen As Winsock
    Public WithEvents sckGListen As Winsock

    Private WithEvents sckLC As Winsock
    Private WithEvents sckGC As Winsock
    Private WithEvents sckLS As Winsock
    Private WithEvents sckGS As Winsock
    Dim Crypt As New CPargermer.Tibia.Crypt.xTeaCrypt
    Private WithEvents phLC As PacketHandler
    Private WithEvents phLS As PacketHandler
    Private WithEvents phGC As PacketHandler
    Private WithEvents phGS As PacketHandler

    'Public ClientHandle As IntPtr = 0

    Private CharacterNames() As String
    Private CharacterWorlds() As String
    Private CharacterIPs() As UInteger
    Private CharacterPorts() As UShort
    Private CharacterIndex As Integer = 0

    Public Event PacketFromClient(ByRef bytArray() As Byte, ByRef Block As Boolean)
    Public Event PacketFromServer(ByRef bytArray() As Byte, ByRef Block As Boolean)
    Public Event ConnectionGained() Implements IProxy.ConnectionGained
    Public Event ConnectionLost() Implements IProxy.ConnectionLost

    Public Sub SendToServer(ByVal Packet As Packet) Implements IProxy.SendPacketToServer
        SendPacketToServer(Packet.GetBytes, False)
    End Sub

    Public Sub SendToClient(ByVal Packet As Packet) Implements IProxy.SendPacketToClient
        SendPacketToClient(Packet.GetBytes)
    End Sub

    Private Sub SendPacketToServer(ByVal bytBuffer() As Byte, Optional ByVal DirectTransfer As Boolean = True)
        Try
            If DirectTransfer = False Then
                SyncLock Me
                    While ((Date.Now.Ticks - LastAction) / TimeSpan.TicksPerMillisecond) < 120
                        System.Threading.Thread.Sleep(125 - ((Date.Now.Ticks - LastAction) / TimeSpan.TicksPerMillisecond))
                    End While
                    LastAction = Date.Now.Ticks
                End SyncLock
            Else
                LastAction = Date.Now.Ticks
            End If
            If sckGS.GetState = Winsock.WinsockStates.Connected Then
                If Fix(bytBuffer.Length / 8) <> (bytBuffer.Length / 8) Then
                    ReDim Preserve bytBuffer((Fix(bytBuffer.Length / 8) + 1) * 8)
                End If
                sckGS.Send(xTeaEncrypt(bytBuffer))
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub SendPacketToClient(ByVal bytBuffer() As Byte)
        Try
            If sckGC.GetState = Winsock.WinsockStates.Connected Then
                If Fix(bytBuffer.Length / 8) <> (bytBuffer.Length / 8) Then
                    ReDim Preserve bytBuffer((Fix(bytBuffer.Length / 8) + 1) * 8)
                End If
                sckGC.Send(xTeaEncrypt(bytBuffer))
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public ReadOnly Property Client() As ITibia Implements IProxy.Client
        Get
            Return ClientProcess
        End Get
    End Property

    Public Sub New(ByRef Client As Tibia)
        Try

            Dim strTemp As String = ""
            Dim Rnd As New Random(Date.Now.Millisecond * Date.Now.Minute)
            sckLListen = New Winsock(CLng(Rnd.Next(2000, 10000)))
            sckGListen = New Winsock(CLng(Rnd.Next(2000, 10000)))

            sckLC = New Winsock
            sckGC = New Winsock
            sckLS = New Winsock
            sckGS = New Winsock

            phLC = New PacketHandler
            phGC = New PacketHandler
            phLS = New PacketHandler
            phGS = New PacketHandler

            sckLListen.Listen()
            sckGListen.Listen()

            Me.ClientProcess = Client
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

#Region " Disconnected "
    Private Sub sckGC_Disconnected(ByVal sender As Winsock) Handles sckGC.Disconnected
        Try
            sckGC.Close()
            sckGS.Close()
            RaiseEvent ConnectionLost()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub sckGS_Disconnected(ByVal sender As Winsock) Handles sckGS.Disconnected
        Try
            sckGS.Close()
            sckGC.Close()
            RaiseEvent ConnectionLost()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub sckLC_Disconnected(ByVal sender As Winsock) Handles sckLC.Disconnected
        Try
            sckLC.Close()
            sckLS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub sckLS_Disconnected(ByVal sender As Winsock) Handles sckLS.Disconnected
        Try
            sckLS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region

#Region " Connection Request "
    Private Sub sckGListen_ConnectionRequest(ByVal sender As Winsock, ByVal requestID As System.Net.Sockets.Socket) Handles sckGListen.ConnectionRequest
        Try
            sckGC.Close()
            sckGC.Accept(requestID)
            sckGS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub sckLListen_ConnectionRequest(ByVal sender As Winsock, ByVal requestID As System.Net.Sockets.Socket) Handles sckLListen.ConnectionRequest
        Try
            sckLC.Close()
            sckLC.Accept(requestID)
            sckLS.Close()
            sckLS.Connect(Kernel.LoginServer, LoginPort)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region

#Region " LoginClient "
    Private Sub sckLC_HandleError(ByVal sender As Winsock, ByVal Description As String, ByVal Method As String, ByVal myEx As String) Handles sckLC.HandleError
        Try
            sckLC.Close()
            sckLS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
#If DEBUG Then
        'MessageBox.Show(Description & " + " & Method & " + " & myEx, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
#End If
    End Sub

    Private Sub sckLC_DataArrival(ByVal sender As Winsock, ByVal BytesTotal As Integer) Handles sckLC.DataArrival
        Try
            Dim bytArray() As Byte = {}
            sckLC.GetData(bytArray)
            phLC.GetData(bytArray)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub phLC_SendData(ByRef bytArray() As Byte) Handles phLC.SendData
        Dim Send As Boolean = True

        If Send = False Then
            Exit Sub
        End If
        Do Until sckLS.GetState = Winsock.WinsockStates.Connected
            System.Threading.Thread.Sleep(10)
        Loop
        Try
            sckLS.Send(bytArray)
        Catch ex As Exception
            sckLC.Close()
            sckLS.Close()
        End Try
    End Sub
#End Region

#Region " LoginServer "
    Private Sub sckLS_HandleError(ByVal sender As Winsock, ByVal Description As String, ByVal Method As String, ByVal myEx As String) Handles sckLS.HandleError
        Try
            sckLC.Close()
            sckLS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
#If DEBUG Then
        'MessageBox.Show(Description & " + " & Method & " + " & myEx, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
#End If
    End Sub

    Private Sub sckLS_DataArrival(ByVal sender As Winsock, ByVal BytesTotal As Integer) Handles sckLS.DataArrival
        Try
            Dim bytArray() As Byte = {}
            sckLS.GetData(bytArray)
            phLS.GetData(bytArray)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub phLS_SendData(ByRef bytArray() As Byte) Handles phLS.SendData
        Dim Send As Boolean = True

        bytArray = xTeaDecrypt(bytArray)

        Select Case bytArray(2)
            Case &H14
                pckCharList(bytArray)
        End Select

        If Send = False Then
            Exit Sub
        End If

        bytArray = xTeaEncrypt(bytArray)

        Do Until sckLC.GetState = Winsock.WinsockStates.Connected
            System.Threading.Thread.Sleep(10)
        Loop
        Try
            sckLC.Send(bytArray)
        Catch ex As Exception
            sckLC.Close()
            sckLS.Close()
        End Try
    End Sub
#End Region

#Region " GameServer "
    Private Sub sckGS_HandleError(ByVal sender As Winsock, ByVal Description As String, ByVal Method As String, ByVal myEx As String) Handles sckGS.HandleError
        Try
            sckGC.Close()
            sckGS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
#If DEBUG Then
        'MessageBox.Show(Description & " + " & Method & " + " & myEx, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
#End If
    End Sub

    Private Sub sckGS_DataArrival(ByVal sender As Winsock, ByVal BytesTotal As Integer) Handles sckGS.DataArrival
        Try
            Dim bytArray() As Byte = {}
            sckGS.GetData(bytArray)
            phGS.GetData(bytArray)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub phGS_SendData(ByRef bytArray() As Byte) Handles phGS.SendData
        Dim Send As Boolean = True

        bytArray = xTeaDecrypt(bytArray)

        RaiseEvent PacketFromServer(bytArray, Send)
        If Not Send Then Exit Sub

        bytArray = xTeaEncrypt(bytArray)

        Do Until sckGC.GetState = Winsock.WinsockStates.Connected
            System.Threading.Thread.Sleep(10)
        Loop
        Try
            sckGC.Send(bytArray)
        Catch ex As Exception
            sckGC.Close()
            sckGS.Close()
        End Try
    End Sub
#End Region

#Region " GameClient "
    Private Sub sckGC_HandleError(ByVal sender As Winsock, ByVal Description As String, ByVal Method As String, ByVal myEx As String) Handles sckGC.HandleError
        Try
            sckGC.Close()
            sckGS.Close()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
#If DEBUG Then
        'MessageBox.Show(Description & " + " & Method & " + " & myEx, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
#End If
    End Sub

    Private Sub sckGC_DataArrival(ByVal sender As Winsock, ByVal BytesTotal As Integer) Handles sckGC.DataArrival
        Try
            Dim bytArray() As Byte = {}
            sckGC.GetData(bytArray)
            phGC.GetData(bytArray)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub phGC_SendData(ByRef bytArray() As Byte) Handles phGC.SendData
        Try
            Dim Send As Boolean = True
            Dim intTemp As Integer = 0

            If Fix(((bytArray.Length - 2) / 8)) <> ((bytArray.Length - 2) / 8) Then
                Kernel.Client.ReadMemory(Consts.ptrCharacterSelectionIndex, intTemp, 1)
                CharacterIndex = intTemp
                sckGS.Close()
                'Dim IP As New Net.IPAddress(CharacterIPs(intTemp))
                sckGS.Connect((New Net.IPAddress(CharacterIPs(intTemp))).ToString, CharacterPorts(intTemp))
                'strCharName = CharacterNames(intTemp)
                RaiseEvent ConnectionGained()
            Else
                bytArray = xTeaDecrypt(bytArray)
                Select Case bytArray(2)
                    Case &HA
                        Kernel.Client.ReadMemory(Consts.ptrCharacterSelectionIndex, intTemp, 1)
                        CharacterIndex = intTemp
                        sckGS.Close()
                        sckGS.Connect((New Net.IPAddress(CharacterIPs(intTemp))).ToString, CharacterPorts(intTemp))
                        'strCharName = CharacterNames(intTemp)
                    Case Else
                End Select

                RaiseEvent PacketFromClient(bytArray, Send)

                If Not Send Then Exit Sub

                bytArray = xTeaEncrypt(bytArray)

            End If
            Do Until sckGS.GetState = Winsock.WinsockStates.Connected
                System.Threading.Thread.Sleep(10)
            Loop
            Try
                sckGS.Send(bytArray)
            Catch ex As Exception
                sckGS.Close()
                sckGC.Close()
            End Try
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region

#Region " xTea Stuff "
    Private Function GetKey() As Byte()
        Try
            Dim bytBuffer() As Byte = {}

            Kernel.Client.ReadMemory(Consts.ptrEncryptionKey, bytBuffer, 16)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Private Function xTeaDecrypt(ByVal bytBuffer() As Byte) As Byte()
        Try
            Dim bytTemp() As Byte
            Dim bytTemp2() As Byte
            Dim Key() As Byte = GetKey()
            Dim intCount As Integer
            Dim intCount2 As Integer
            ReDim bytTemp2(UBound(bytBuffer) - 2)
            For intCount = 0 To ((UBound(bytBuffer) - 1) / 8) - 1
                bytTemp = Crypt.XTEADecrypt(bytBuffer, 2 + (8 * intCount), 8, Key)
                For intCount2 = 0 To 7
                    bytTemp2((8 * intCount) + intCount2) = bytTemp(intCount2)
                Next
            Next
            Return bytTemp2
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Private Function xTeaEncrypt(ByVal bytBuffer() As Byte) As Byte()
        Try
            Dim bytTemp() As Byte
            Dim bytTemp2(1) As Byte
            Dim Key() As Byte = GetKey()
            Dim intCount As Integer
            For intCount = 0 To ((UBound(bytBuffer) + 1) / 8) - 1
                bytTemp = Crypt.XTEAEncrypt(bytBuffer, (8 * intCount), 8, Key)
                AddByteArray(bytTemp2, bytTemp)
            Next
            Return bytTemp2
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

    Sub pckCharList(ByRef bytBuffer() As Byte)
        Try
            Dim Pos As Integer = 3
            Dim Motd As String = GetString(bytBuffer, Pos)
            Pos += 1
            Dim TotalChars As Short = GetByte(bytBuffer, Pos)
            ReDim CharacterNames(TotalChars - 1)
            ReDim CharacterWorlds(TotalChars - 1)
            ReDim CharacterIPs(TotalChars - 1)
            ReDim CharacterPorts(TotalChars - 1)
            'Dim pck As String = BytesToStr(bytBuffer)
            For I As Integer = 0 To TotalChars - 1
                CharacterNames(I) = GetString(bytBuffer, Pos)
                CharacterWorlds(I) = GetString(bytBuffer, Pos)
                CharacterIPs(I) = GetDWord(bytBuffer, Pos)
                CharacterPorts(I) = GetWord(bytBuffer, Pos)
            Next
            Dim PremDays As UShort = GetWord(bytBuffer, Pos)
            ReDim bytBuffer(1)
            'Dim newBytBuffer(1) As Byte
            AddByte(bytBuffer, &H14)
            If Consts.ModifyMOTD Then
                Dim Tmp As String = (New Random((Now.Millisecond))).Next(1, 300)
                AddString(bytBuffer, Tmp & BotMOTD)
            Else
                AddString(bytBuffer, Motd)
            End If
            AddByte(bytBuffer, &H64)
            AddByte(bytBuffer, CByte(TotalChars))
            For I As Integer = 0 To TotalChars - 1
                AddString(bytBuffer, CharacterNames(I))
                AddString(bytBuffer, CharacterWorlds(I))
                AddByte(bytBuffer, 127)
                AddByte(bytBuffer, 0)
                AddByte(bytBuffer, 0)
                AddByte(bytBuffer, 1)
                AddWord(bytBuffer, sckGListen.LocalPort)
            Next
            AddWord(bytBuffer, PremDays)
            If Fix(bytBuffer.Length / 8) <> (bytBuffer.Length / 8) Then
                ReDim Preserve bytBuffer((Fix(bytBuffer.Length / 8) + 1) * 8)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AddByteArray(ByRef bytBuffer() As Byte, ByVal bytAdd() As Byte)
        Try
            Dim intTemp As Integer
            Dim bytTemp() As Byte
            Dim intCounter As Integer
            intTemp = UBound(bytBuffer)
            ReDim Preserve bytBuffer(intTemp + bytAdd.Length)
            bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + bytAdd.Length)
            bytBuffer(0) = bytTemp(0)
            bytBuffer(1) = bytTemp(1)
            For intCounter = 1 To bytAdd.Length
                bytBuffer(intTemp + intCounter) = bytAdd(intCounter - 1)
            Next
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Class PacketHandler

        Private bytBuffer() As Byte = {}
        Private PacketPosition As Integer = 0
        Private PacketSize As Integer

        Public Event SendData(ByRef bytArray() As Byte)

        Public Sub GetData(ByRef bytData() As Byte)
            Try
                Dim Count1 As Integer
                Dim PSize As Integer = bytData.Length
                Dim PPosition As Integer = 0

                Do
                    If PacketPosition = 0 Then
                        PacketSize = BtoW(bytData(PPosition), bytData(PPosition + 1)) + 2
                        ReDim bytBuffer(PacketSize - 1)
                        bytBuffer(0) = bytData(PPosition)
                        bytBuffer(1) = bytData(PPosition + 1)
                        PacketPosition = 2
                        PPosition = PPosition + 2
                    End If
                    If PacketSize - PacketPosition > PSize - PPosition Then
                        For Count1 = 0 To PSize - (PPosition + 1)
                            bytBuffer(PacketPosition + Count1) = bytData(PPosition + Count1)
                        Next
                        PacketPosition += Count1
                        PPosition += Count1
                    Else
                        For Count1 = 0 To PacketSize - (PacketPosition + 1)
                            bytBuffer(PacketPosition + Count1) = bytData(PPosition + Count1)
                        Next
                        PacketPosition = 0
                        PPosition += Count1
                        RaiseEvent SendData(bytBuffer)
                    End If
                Loop Until PPosition = PSize
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Function BtoW(ByVal byt1 As Byte, ByVal byt2 As Byte) As Integer
            Try
                Return (byt1 + (byt2 * 256))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
    End Class

End Class