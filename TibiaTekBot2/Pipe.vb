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

Imports System.IO.Pipes, Scripting

Public Class Pipe
    Implements IPipe

#Region " Events "
    Public Event OnReceive As OnReceiveDelegate
    Public Event OnSend As OnSendDelegate
    Public Event OnConnect As OnConnectDelegate
    Public Event OnConnected As OnConnectedDelegate
#End Region

#Region " Delegates "
    Public Delegate Sub OnConnectDelegate()
    Public Delegate Sub OnConnectedDelegate()
    Public Delegate Sub OnReceiveDelegate(ByVal Buffer() As Byte)
    Public Delegate Sub OnSendDelegate(ByVal Buffer() As Byte, ByRef Send As Boolean)
#End Region

#Region " Objects/Variables "
    Private _Pipe As NamedPipeServerStream
    Private _AsyncCallback As AsyncCallback
    Private _AsyncResult As IAsyncResult
    Private _InBuffer(0 To 1023) As Byte
#End Region

#Region " Properties "
    Public ReadOnly Property IsConnected() As Boolean Implements IPipe.IsConnected
        Get
            Try
                Return _Pipe.IsConnected
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return False
        End Get
    End Property
#End Region

#Region " Methods "
    Public Sub New(ByVal PipeName As String)
        Try
            _Pipe = New NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous)
            _Pipe.BeginWaitForConnection(New AsyncCallback(AddressOf BeginWaitForConnection), Nothing)
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub OnBeginRead(ByVal AR As IAsyncResult)
        Try
            SyncLock Me
                _Pipe.EndRead(AR)
                RaiseEvent OnReceive(_InBuffer)
                _Pipe.BeginRead(_InBuffer, 0, 1023, New AsyncCallback(AddressOf OnBeginRead), Nothing)
            End SyncLock
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Private Sub BeginWaitForConnection(ByVal AR As IAsyncResult)
        Try
            RaiseEvent OnConnect()
            _Pipe.WaitForConnection()
            If IsConnected Then
                RaiseEvent OnConnected()
                _Pipe.EndWaitForConnection(AR)
                _Pipe.BeginRead(_InBuffer, 0, 1023, New AsyncCallback(AddressOf OnBeginRead), Nothing)
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Sub Send(ByVal Packet As IPacket) Implements IPipe.Send
        Try
            Dim _buf() As Byte = Packet.GetBytes
            Dim Send As Boolean = True
            RaiseEvent OnSend(_buf, Send)
            If Send Then _Pipe.Write(_buf, 0, _buf.Length)
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
#End Region

End Class
