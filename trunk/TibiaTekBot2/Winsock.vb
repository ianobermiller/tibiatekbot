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
'    Boston, MA 02111-1307, USA.Imports System.Math

Imports System.Net, System.Net.Sockets, System.ComponentModel, System.Net.NetworkInformation

Public Module WinsockModule

    Public Class Winsock

#Region " Events "
        Public Event Connected(ByVal sender As Winsock)
        Public Event Disconnected(ByVal sender As Winsock)
        Public Event DataArrival(ByVal sender As Winsock, ByVal BytesTotal As Integer)
        Public Event ConnectionRequest(ByVal sender As Winsock, ByVal requestID As Socket)
        Public Event SendComplete(ByVal sender As Winsock)
        Public Event HandleError(ByVal sender As Winsock, ByVal Description As String, ByVal Method As String, ByVal myEx As String)
        Public Event StateChanged(ByVal sender As Winsock, ByVal state As WinsockStates)
#End Region

#Region " Variables "
        Private _RemoteIP As String = "127.0.0.1"
        Private _LocalPort As Integer = 8181
        Private _RemotePort As Integer = 8181
        Private _State As WinsockStates = WinsockStates.Closed
        Private _sBuffer As String
        Private _buffer() As Byte
        Private _bufferCol As Collection
        Private _byteBuffer(1024) As Byte
        Private _sockList As Socket
        Private _Client As Socket
        Private _networkChange As NetworkChange

#End Region

#Region " Constructors "
        Public Sub New()
            Me.New(8181)
        End Sub
        Public Sub New(ByVal Port As Long)
            Me.New("127.0.0.1", Port)
        End Sub
        Public Sub New(ByVal IP As String)
            Me.New(IP, 8181)
        End Sub
        Public Sub New(ByVal IP As String, ByVal Port As Long)
            Try
                RemoteIP = IP
                RemotePort = Port
                LocalPort = Port
                _bufferCol = New Collection
                AddHandler NetworkChange.NetworkAvailabilityChanged, AddressOf Me.NetWorkAvailabilityChangedCallback
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub
#End Region

        Private Sub NetWorkAvailabilityChangedCallback(ByVal sender As Object, ByVal e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
            Try
                If Not e.IsAvailable Then
                    ChangeState(WinsockStates.Closed)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#Region " Properties "
        Public Property LocalPort() As Integer
            Get
                Return _LocalPort
            End Get
            Set(ByVal Value As Integer)
                If GetState = WinsockStates.Closed Then
                    _LocalPort = Value
                Else
                    Throw New Exception("Must be idle to change the local port")
                End If
            End Set
        End Property
        Public Property RemotePort() As Integer
            Get
                Return _RemotePort
            End Get
            Set(ByVal Value As Integer)
                If GetState <> WinsockStates.Connected Then
                    _RemotePort = Value
                Else
                    Throw New Exception("Can't be connected to a server and change the remote port.")
                End If
            End Set
        End Property
        Public Property RemoteIP() As String
            Get
                Return _RemoteIP
            End Get
            Set(ByVal Value As String)
                If GetState = WinsockStates.Closed Then
                    _RemoteIP = Value
                Else
                    Throw New Exception("Must be closed to set the remote ip.")
                End If
            End Set
        End Property
        <Browsable(False)> Public ReadOnly Property GetState() As WinsockStates
            Get
                Return _State
            End Get
        End Property
#End Region

#Region " Methods "

        Public Sub Listen()
            Try
                Dim x As New System.Threading.Thread(AddressOf DoListen)
                x.Start()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Close()
            Try
                Select Case GetState
                    Case WinsockStates.Listening
                        ChangeState(WinsockStates.Closing)
                        _sockList.Close()
                    Case WinsockStates.Connected, WinsockStates.Connecting, WinsockStates.ConnectionPending, WinsockStates.HostResolved, WinsockStates.Open, WinsockStates.ResolvingHost
                        ChangeState(WinsockStates.Closing)
                        _Client.Close()
                End Select
                ChangeState(WinsockStates.Closed)
            Catch ex As Exception
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub

        Public Sub Accept(ByVal requestID As Socket)
            Try
                ChangeState(WinsockStates.ConnectionPending)
                _Client = requestID
                RaiseEvent Connected(Me)
                ChangeState(WinsockStates.Connected)
                _Client.BeginReceive(_byteBuffer, 0, 1024, SocketFlags.None, AddressOf DoStreamReceive, Nothing)
            Catch ex As Exception
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Public Sub Connect()
            If GetState = WinsockStates.Connected Or GetState = WinsockStates.Listening Then
                RaiseEvent HandleError(Me, "Already open, must be closed first", "Connect", "Nothing here")
                Exit Sub
            End If
            Try
                Dim remIP As String = ""
                ChangeState(WinsockStates.ResolvingHost)
                Try
                    If System.Text.RegularExpressions.Regex.IsMatch(_RemoteIP, "^\d+\.\d+\.\d+\.\d+$") Then
                        Dim x As System.Net.IPAddress
                        x = IPAddress.Parse(_RemoteIP)
                        remIP = x.ToString
                    Else
                        Dim ip As IPHostEntry = System.Net.Dns.GetHostEntry(_RemoteIP)
                        Dim t() As IPAddress = ip.AddressList
                        remIP = t(0).ToString
                    End If
                Catch ex As Exception
                    ChangeState(WinsockStates.Error)
                    RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
                End Try
                'Try
                'Dim x As System.Net.IPAddress
                'x = IPAddress.Parse(_RemoteIP)
                'remIP = x.ToString
                'Catch ex1 As Exception
                'not a valid IP address - resolve DNS
                'Try
                'Dim x As System.Net.Dns
                ''Dim x As System.Net.IPHostEntry
                'Dim ip As IPHostEntry = System.Net.Dns.GetHostEntry(_RemoteIP)
                '  Dim t() As IPAddress = ip.AddressList
                '   remIP = t(0).ToString
                'Catch ex2 As Exception
                'ChangeState(WinsockStates.Error)
                'RaiseEvent HandleError(Me, ex2.Message, ex2.TargetSite.Name, ex2.ToString)
                'End Try
                'End Try
                ChangeState(WinsockStates.HostResolved)
                _Client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Dim rEP As New IPEndPoint(IPAddress.Parse(remIP), RemotePort)
                '_Client.Connect(rEP)
                ChangeState(WinsockStates.Connecting)
                _Client.BeginConnect(rEP, New AsyncCallback(AddressOf OnConnected), Nothing)
            Catch ex As Exception
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Public Sub Connect(ByVal IP As String, ByVal Port As Integer)
            RemoteIP = IP
            RemotePort = Port
            Connect()
        End Sub

#End Region

#Region " Public Functions/Subs "
        Public Function LocalIP() As String
            Try
                Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
                Return CType(h.AddressList.GetValue(0), Net.IPAddress).ToString
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
        Public Function RemoteHostIP() As String
            Try
                Dim iEP As IPEndPoint = _Client.RemoteEndPoint
                Return iEP.Address.ToString
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
#End Region

#Region " Send Overloads "

        Public Sub Send(ByVal Data As String)
            Try
                Dim sendBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(Data)
                Me.Send(sendBytes)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Send(ByVal Data() As Byte)
            If GetState = WinsockStates.Connected Then
                Try
                    _Client.Send(Data)
                Catch ex As Exception
                    Me.Close()
                    ChangeState(WinsockStates.Error)
                    RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
                End Try
            End If
        End Sub

#End Region

#Region " GetData Overloads "

        Public Sub GetData(ByRef data As String)
            Try
                Dim byt() As Byte = {}
                GetData(byt)
                For i As Integer = 0 To UBound(byt)
                    If byt(i) = 10 Then
                        data &= vbLf
                    Else
                        data &= ChrW(byt(i))
                    End If
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub GetData(ByRef bytes() As Byte)
            Try
                If _bufferCol.Count = 0 Then Throw New IndexOutOfRangeException("Nothing in buffer.")
                Dim byt() As Byte = Me._bufferCol.Item(1)
                _bufferCol.Remove(1)
                ReDim bytes(UBound(byt))
                byt.CopyTo(bytes, 0)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Private Functions/Subs "
        Private Sub ChangeState(ByVal new_state As WinsockStates)
            Try
                _State = new_state
                RaiseEvent StateChanged(Me, _State)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub
        Private Sub OnConnected(ByVal asyn As IAsyncResult)
            Try
                _Client.EndConnect(asyn)
                Me.ClientFinalizeConnection()
            Catch ex As Exception
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Private Sub ClientFinalizeConnection()
            ChangeState(WinsockStates.Connected)
            _Client.BeginReceive(_byteBuffer, 0, 1024, SocketFlags.None, AddressOf DoRead, Nothing)
            RaiseEvent Connected(Me)
        End Sub
        Private Sub DoListen()
            Try
                'Dim tmpSocket As Socket
                _sockList = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Dim ipLocal As New IPEndPoint(IPAddress.Any, LocalPort)
                _sockList.Bind(ipLocal)
                _sockList.Listen(1)
                _LocalPort = CType(_sockList.LocalEndPoint, IPEndPoint).Port
                ChangeState(WinsockStates.Listening)
                _sockList.BeginAccept(New AsyncCallback(AddressOf OnClientConnect), Nothing)
            Catch ex As Exception
                Me.Close()
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Private Sub OnClientConnect(ByVal asyn As IAsyncResult)
            Try
                Dim tmpSock As Socket
                If GetState = WinsockStates.Listening Then
                    tmpSock = _sockList.EndAccept(asyn)
                    RaiseEvent ConnectionRequest(Me, tmpSock)
                    _sockList.BeginAccept(New AsyncCallback(AddressOf OnClientConnect), Nothing)
                End If
            Catch ex As Exception
                Me.Close()
                ChangeState(WinsockStates.Error)
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Private Sub DoStreamReceive(ByVal ar As IAsyncResult)
            Dim intCount As Integer = 0
            Try
                SyncLock _Client
                    If _Client.Connected Then
                        intCount = _Client.EndReceive(ar)
                    End If

                End SyncLock
                If intCount < 1 Then
                    Me.Close()
                    ReDim _byteBuffer(1024)
                    RaiseEvent Disconnected(Me)
                    Exit Sub
                End If
                AddToBuffer(_byteBuffer, intCount)
                'BuildString(_byteBuffer, 0, intCount)
                Array.Clear(_byteBuffer, 0, intCount)
                SyncLock _Client
                    _Client.BeginReceive(_byteBuffer, 0, 1024, SocketFlags.None, AddressOf DoStreamReceive, Nothing)
                End SyncLock
            Catch ex As Exception
                Me.Close()
                ReDim _byteBuffer(1024)
                RaiseEvent Disconnected(Me)
            End Try
        End Sub
        Private Sub DoRead(ByVal ar As IAsyncResult)
            Dim intCount As Integer = 0
            Try
                If _Client.Connected Then intCount = _Client.EndReceive(ar)
                If intCount < 1 Then
                    Me.Close()
                    ReDim _byteBuffer(1024)
                    RaiseEvent Disconnected(Me)
                    Exit Sub
                End If
                AddToBuffer(_byteBuffer, intCount)
                'BuildString(_byteBuffer, 0, intCount)
                Array.Clear(_byteBuffer, 0, intCount)
                _Client.BeginReceive(_byteBuffer, 0, 1024, SocketFlags.None, AddressOf DoRead, Nothing)
            Catch ex As Exception
                Me.Close()
                ReDim _byteBuffer(1024)
                RaiseEvent Disconnected(Me)
            End Try
        End Sub
        Private Sub BuildString(ByVal Bytes() As Byte, ByVal offset As Integer, ByVal count As Integer)
            Try
                Dim intIndex As Integer
                For intIndex = offset To offset + count - 1
                    If Bytes(intIndex) = 10 Then
                        _sBuffer &= vbLf
                    Else
                        _sBuffer &= ChrW(Bytes(intIndex))
                    End If
                Next
                If _sBuffer.IndexOf(vbCrLf) <> -1 Then
                    RaiseEvent DataArrival(Me, count)
                    Array.Clear(_byteBuffer, 0, _byteBuffer.Length)
                End If
            Catch ex As Exception
                RaiseEvent HandleError(Me, ex.Message, ex.TargetSite.Name, ex.ToString)
            End Try
        End Sub
        Private Sub AddToBuffer(ByVal bytes() As Byte, ByVal count As Integer)
            Try
                Dim curUB As Integer
                If Not _buffer Is Nothing Then curUB = UBound(_buffer) Else curUB = -1
                Dim newUB As Integer = curUB + count
                ReDim Preserve _buffer(newUB)
                Array.Copy(bytes, 0, _buffer, curUB + 1, count)
                If count < bytes.Length - 1 And _buffer.Length > 0 Then
                    _bufferCol.Add(_buffer)
                    RaiseEvent DataArrival(Me, _buffer.Length)
                    _buffer = Nothing
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub
#End Region
        Enum WinsockStates
            Closed = 0
            Open = 1
            Listening = 2
            ConnectionPending = 3
            ResolvingHost = 4
            HostResolved = 5
            Connecting = 6
            Connected = 7
            Closing = 8
            [Error] = 9
            'Listening = 1
            'Connected = 2
        End Enum
    End Class

End Module
