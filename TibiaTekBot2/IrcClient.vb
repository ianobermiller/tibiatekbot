Imports System, System.Net, System.Net.Sockets, System.IO, System.Math, _
    System.Threading, System.Text.RegularExpressions, TibiaTekBot.Constants, Scripting

Public Class IrcClient
    Implements IIrcClient

    Public Event ChannelMessageHidden(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Implements Scripting.IIrcClient.ChannelMessageHidden
    Public Event ChannelSelfKickHidden(ByVal NickKicker As String, ByVal KickMessage As String, ByVal Channel As String) Implements Scripting.IIrcClient.ChannelSelfKickHidden
    Public Event ChannelJoin(ByVal Nick As String, ByVal Channel As String) Implements IIrcClient.ChannelJoin
    Public Event ChannelSelfJoinHidden(ByVal Channel As String) Implements IIrcClient.ChannelSelfJoinHidden
    Public Event ChannelSelfJoin(ByVal Channel As String) Implements IIrcClient.ChannelSelfJoin
    Public Event ChannelKick(ByVal NickKicker As String, ByVal NickKicked As String, ByVal KickMessage As String, ByVal Channel As String) Implements IIrcClient.ChannelKick
    Public Event ChannelSelfKick(ByVal NickKicker As String, ByVal KickMessage As String, ByVal Channel As String) Implements IIrcClient.ChannelSelfKick
    Public Event NickChange(ByVal UserOldNick As String, ByVal UserNewNick As String) Implements IIrcClient.NickChange
    Public Event ChannelPart(ByVal Nick As String, ByVal Channel As String) Implements IIrcClient.ChannelPart
    Public Event ChannelSelfPart(ByVal Channel As String) Implements IIrcClient.ChannelSelfPart
    Public Event ChannelSelfPartHidden(ByVal Channel As String) Implements IIrcClient.ChannelSelfPartHidden
    Public Event QuitIrc(ByVal Nick As String, ByVal Message As String) Implements IIrcClient.QuitIrc
    Public Event RawMessage(ByVal RawMessage As String) Implements IIrcClient.RawMessage
    Public Event Connecting() Implements IIrcClient.Connecting
    Public Event Disconnected() Implements IIrcClient.Disconnected
    Public Event ChannelAction(ByVal Nick As String, ByVal Action As String, ByVal Channel As String) Implements IIrcClient.ChannelAction
    Public Event ChannelBroadcast(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Implements IIrcClient.ChannelBroadcast
    Public Event ChannelError(ByVal Channel As String, ByVal Message As String) Implements IIrcClient.ChannelError
    Public Event ChannelMessage(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Implements IIrcClient.ChannelMessage
    Public Event ChannelMode(ByVal Nick As String, ByVal UserMode As String, ByVal Channel As String) Implements IIrcClient.ChannelMode
    Public Event ChannelNamesList() Implements IIrcClient.ChannelNamesList
    Public Event Connected() Implements IIrcClient.Connected
    Public Event EndMOTD() Implements IIrcClient.EndMOTD
    Public Event Invite(ByVal Nick As String, ByVal Channel As String) Implements IIrcClient.Invite
    Public Event Notice(ByVal Nick As String, ByVal Message As String) Implements IIrcClient.Notice
    Public Event PrivateMessage(ByVal Nick As String, ByVal Message As String) Implements IIrcClient.PrivateMessage
    Public Event ChannelTopicChange(ByVal ChannelInfo As Scripting.IIrcClient.ChannelInformation) Implements Scripting.IIrcClient.ChannelTopicChange
    Public Event PacketReceived(ByVal Nick As String, ByVal Channel As String, ByVal Packet As String) Implements Scripting.IIrcClient.PacketReceived

    Public Const One As Char = Chr(1)
    'Public Const Version As String = "TibiaTek IRC Client for TibiaTek Bot v2.1 (http://www.tibiatek.com/)"

    Public Channels As New SortedList(Of String, IIrcClient.ChannelInformation)
    Public HiddenChannels As New SortedList(Of String, IIrcClient.ChannelInformation)
    Public HiddenChannelNames As New List(Of String)

    Const NickMaxLen As Integer = 15

    Private _Server As String
    Private _Port As String
    Private Client As TcpClient
    Public ConnectionStream As NetworkStream
    Private Reader As StreamReader
    Private Writer As StreamWriter

    Private _Nick As String
    Private _Password As String
    Private _User As String
    Private _RealName As String
    Private _IsInvisible As Boolean

    Public MainThread As Thread

    Dim CanReconnect As Boolean = True
    Dim WasConnected As Boolean = True
#Region " Constructors "
    Public Sub New()
        Try
            'ChannelInfo.Users = New SortedList(Of String, UserInformation)
            If Not MainThread Is Nothing Then
                MainThread.Abort()
            End If
            While Not MainThread Is Nothing : End While
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Public Sub New(ByVal Server As String, ByVal Port As Integer)
        Me.New()
        Try
            Me.Server = Server
            Me.Port = Port
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub
#End Region

#Region " Properties "

    Public ReadOnly Property IsConnected() As Boolean Implements IIrcClient.IsConnected
        Get
            Try
                Return (Not Client Is Nothing AndAlso Not Client.Client Is Nothing AndAlso Client.Client.Connected)
            Catch Ex As Exception
                ShowError(Ex)
                End
            End Try
        End Get
    End Property
    Public Property Password() As String Implements IIrcClient.Password
        Get
            Try
                Return Me._Password
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return String.Empty
        End Get
        Set(ByVal value As String)
            Me._Password = value
        End Set
    End Property
    Public Property User() As String Implements IIrcClient.User
        Get
            Try
                Return Me._User
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return String.Empty
        End Get
        Set(ByVal value As String)
            Me._User = value
        End Set
    End Property
    Public Property Invisible() As Boolean Implements IIrcClient.Invisible
        Get
            Try
                Return Me._IsInvisible
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return String.Empty
        End Get
        Set(ByVal value As Boolean)
            Me._IsInvisible = value
        End Set
    End Property
    Public Property RealName() As String Implements IIrcClient.RealName
        Get
            Try
                Return Me._RealName
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return String.Empty
        End Get
        Set(ByVal value As String)
            Me._RealName = value
        End Set
    End Property
    Public Property Server() As String Implements IIrcClient.Server
        Get
            Return Me._Server
        End Get
        Set(ByVal value As String)
            Me._Server = value
        End Set
    End Property
    Public Property Port() As Integer Implements IIrcClient.Port
        Get
            Try
                If Port > System.Net.IPEndPoint.MinPort OrElse Port < System.Net.IPEndPoint.MaxPort Then
                    Return Me._Port
                End If
            Catch Ex As Exception
                ShowError(Ex)
                End
            End Try
        End Get
        Set(ByVal value As Integer)
            Me._Port = value
        End Set
    End Property
    Public Property Nick() As String Implements IIrcClient.Nick
        Get
            Try
                Return Me._Nick
            Catch Ex As Exception
                ShowError(Ex)
            End Try
            Return String.Empty
        End Get
        Set(ByVal value As String)
            Try
                Me._Nick = FormatNick(value)
            Catch Ex As Exception
                ShowError(Ex)
                End
            End Try
        End Set
    End Property
#End Region

#Region " Methods "
    Public Function Connect(ByVal Server As String, ByVal Port As Integer) As Boolean
        Try
            Me.Server = Server
            Me.Port = Port
            Me.Connect()
            WasConnected = True
            Return True
        Catch Ex As SocketException
            Return False
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Public Function Connect() As Boolean Implements IIrcClient.Connect
        Try
            If Not MainThread Is Nothing AndAlso MainThread.IsAlive Then MainThread.Abort()
            CanReconnect = True
            RaiseEvent Connecting()
            Me.Client = Nothing
            Me.Client = New TcpClient()
            Me.Client.ReceiveTimeout = 24 * 60 * 60 * 1000
            Me.Client.SendTimeout = 24 * 60 * 60 * 1000
            Me.Client.Connect(Me._Server, Me._Port)
            WasConnected = True
            Me.ConnectionStream = Me.Client.GetStream()
            Dim Encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1")
            Me.Reader = New StreamReader(Me.ConnectionStream, Encoding)
            Me.Writer = New StreamWriter(Me.ConnectionStream, Encoding)
            RaiseEvent Connected()
            Return True
        Catch Ex As Exception
            ShowError(Ex)
            Return False
        End Try
    End Function

    Public Sub Disconnect() Implements IIrcClient.Disconnect
        Try
            If WasConnected AndAlso Not Client Is Nothing AndAlso Client.Connected Then
                WasConnected = False
                If Not Client.Client Is Nothing Then
                    Client.Client.Disconnect(True)
                End If
            End If
            RaiseEvent Disconnected()
            'DoMainLoopThread.Abort()
        Catch Ex As Exception
        End Try
    End Sub
    Public Function GetUserLevel(ByVal Nickname As String, ByVal Channel As String) As Integer Implements IIrcClient.GetUserLevel
        Try
            If Channels Is Nothing Then Return False
            If Channels.ContainsKey(Channel) Then
                For Each User As String In Channels(Channel).Users.Keys
                    If String.Equals(Nickname, User, StringComparison.CurrentCultureIgnoreCase) Then
                        Return Channels(Channel).Users(User).UserLevel
                    End If
                Next
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Function

    Public Sub WriteLine(ByVal Command As String)
        Try
            If Writer Is Nothing OrElse Client Is Nothing OrElse Not Client.Connected Then Exit Sub
            Writer.WriteLine(Command)
            Writer.Flush()
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
    Public Sub DoMainThread() Implements IIrcClient.DoMainThread
        Try
            If Not WasConnected Then Exit Sub
            MainThread = New Thread(New ThreadStart(AddressOf Me.DoMainLoop))
            MainThread.Start()
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Function IsChannelOpened(ByVal Channel As String) As Boolean Implements Scripting.IIrcClient.IsChannelOpened
        Try
            For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In Channels
                If String.Equals(Channel, ChannelKVP.Key, StringComparison.CurrentCultureIgnoreCase) AndAlso ChannelKVP.Value.ID > 0 Then
                    Return True
                End If
            Next
            Return False
        Catch Ex As Exception
            ShowError(Ex)
            Return False
        End Try
    End Function

    Public Function IsHiddenChannelOpened(ByVal Channel As String) As Boolean Implements Scripting.IIrcClient.IsHiddenChannelOpened
        Try
            For Each _Channel As String In HiddenChannels.Keys
                If Channel.Equals(_Channel, StringComparison.CurrentCultureIgnoreCase) Then
                    Return True
                End If
            Next
            Return False
        Catch Ex As Exception
            ShowError(Ex)
            Return False
        End Try
    End Function
#End Region

#Region " Commands "
    Public Sub Quit(Optional ByVal Reason As String = "Good Bye!") Implements Scripting.IIrcClient.Quit
        Try
            If Not WasConnected Then Exit Sub
            CanReconnect = False
            If String.IsNullOrEmpty(Reason) Then
                WriteLine("QUIT")
            Else
                WriteLine("QUIT :" & Reason)
            End If
            If Not Client Is Nothing AndAlso Client.Connected Then
                Disconnect()
            End If
        Catch
        End Try
    End Sub
    Public Sub ChangeNick(ByVal NewNick As String) Implements Scripting.IIrcClient.ChangeNick
        Try
            If NewNick Is Nothing Or NewNick = String.Empty Then Exit Sub
            Dim InvalidWords() As String = New String() {"admin", "gm", "cm"}
            For Each InvalidWord As String In InvalidWords
                If NewNick.ToLower.Contains(InvalidWord) Then
                    Exit Sub
                End If
            Next
            WriteLine(String.Format("NICK {0}", NewNick))
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
    Public Sub Identify()
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("USER {0} {1} * :{2}", Me._User, IIf(Me._IsInvisible, "8", "0"), Me.RealName))
            ChangeNick(Me._Nick)
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
    Public Sub Part(ByVal Channel As String, Optional ByVal Reason As String = "Good Bye!") Implements Scripting.IIrcClient.Part
        Try
            If Not WasConnected Then Exit Sub
            If Not String.IsNullOrEmpty(Channel) Then
                WriteLine(String.Format("PART {0}", Channel))
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
    Public Sub SendNotice(ByVal Destinatary As String, ByVal Message As String) Implements Scripting.IIrcClient.SendNotice
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("NOTICE {0} :{1}", Destinatary, Message))
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Sub Speak(ByVal Message As String, ByVal Destinatary As String) Implements Scripting.IIrcClient.Speak
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("PRIVMSG {0} :{1}", Destinatary, Message))
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Sub Rejoin(ByVal Channel As String) Implements Scripting.IIrcClient.Rejoin
        Try
            If Channels.ContainsKey(Channel) Then
                Part(Channel)
                Thread.Sleep(2000)
                Join(Channel)
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Sub Join(ByVal Channel As String, Optional ByVal Password As String = "") Implements Scripting.IIrcClient.Join
        Try
            If Not WasConnected Then Exit Sub
            If Not String.IsNullOrEmpty(Channel) AndAlso Not Channels.ContainsKey(Channel) Then
                WriteLine(String.Format("JOIN {0}", Channel))
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

    Public Sub JoinHidden(ByVal Channel As String, Optional ByVal Password As String = "") Implements Scripting.IIrcClient.JoinHidden
        Try
            If Not WasConnected Then Exit Sub
            If Not String.IsNullOrEmpty(Channel) AndAlso Not HiddenChannels.ContainsKey(Channel) Then
                HiddenChannelNames.Add(Channel.ToLower)
                WriteLine(String.Format("JOIN {0} {1}", Channel, Password))
            End If
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub

#End Region

#Region " Private/Protected Methods "
    Private Function FormatNick(ByVal Nick As String) As String
        Try
            If String.IsNullOrEmpty(Nick) Then
                Dim R As New Random(System.DateTime.Now.Millisecond)
                Return "TempUser" & R.Next(0, 999)
            End If
            ' Keep nick fixed at 15 chars
            If Nick.Length > 15 Then
                Nick = Nick.Substring(0, 15)
            End If
            ' Fix nick if does not start with letter or digit
            Dim ResultArray As Char() = Nick.ToCharArray
            If Not (Char.IsLetter(ResultArray(0)) OrElse IsSpecial(ResultArray(0))) Then
                ResultArray(0) = Chr(&H5F)
            End If
            Dim Length As Integer = ResultArray.Length
            Dim ResultByteArray(0 To Length - 1) As Byte

            For I As Integer = 0 To Length - 1
                If I > 0 AndAlso Not (Char.IsLetterOrDigit(ResultArray(I)) _
                    OrElse IsSpecial(ResultArray(I)) _
                    OrElse ResultArray(I) = Chr(&H2D)) Then
                    ResultByteArray(I) = &H5F
                Else
                    ResultByteArray(I) = Asc(ResultArray(I))
                End If
            Next
            Return System.Text.ASCIIEncoding.ASCII.GetString(ResultByteArray)
        Catch Ex As Exception
            ShowError(Ex)
        End Try
        Return String.Empty
    End Function
    Private Function IsSpecial(ByVal C As Char) As Boolean
        Try
            Return (Asc(C) >= &H5B AndAlso Asc(C) <= &H60) _
                 OrElse (Asc(C) >= &H7B AndAlso Asc(C) <= &H7D)
        Catch Ex As Exception
            ShowError(Ex)
        End Try
        Return False
    End Function
    Protected Overrides Sub Finalize()
        Try
            If Not Me.Client Is Nothing AndAlso Not Me.Client.Client Is Nothing Then
                If Me.Client.Connected Then
                    Me.Client.Client.Close()
                End If
            End If
            If Not Me.MainThread Is Nothing Then Me.MainThread.Abort()
            MyBase.Finalize()
        Catch Ex As Exception
            ShowError(Ex)
        End Try
    End Sub
#End Region

    Public Sub DoMainLoop()
        Dim FirstTime As Boolean = True
        Do
            Try
                Do While IsConnected
                    If Not Kernel.Client.IsConnected Then
                        Disconnect()
                    End If
                    Try
                        Dim Message As String = ""
                        Dim SplitMessages() As String
                        Try
                            Message = Reader.ReadLine()
                        Catch Ex As System.Text.DecoderFallbackException
                        End Try
                        If Message Is Nothing OrElse String.IsNullOrEmpty(Message) Then Exit Do
                        'Kernel.ConsoleWrite(Message)
                        RaiseEvent RawMessage(Message)
                        SplitMessages = Message.Split(New Char() {" "c}, 2)
                        Dim Temp() As String
                        If FirstTime Then
                            FirstTime = False
                            Me._Server = SplitMessages(0).Substring(1)
                            'Core.ConsoleWrite("""" & Me._Server & """")
                        End If
                        If SplitMessages(0).Contains(Me._Server) Then
                            Dim Command() As String = SplitMessages(1).Split(New Char() {" "c}, 2)
                            Select Case Command(0)
                                Case "376" ' END OF MOTD
                                    RaiseEvent EndMOTD()
                                Case "332" ' Topic
                                    Temp = Command(1).Split(New Char() {" "c}, 3)
                                    Dim Channel As String = Temp(1)
                                    If Channels.ContainsKey(Channel) Then

                                        Dim CI As IIrcClient.ChannelInformation = Channels(Channel)
                                        CI.Topic = Temp(2).Substring(1)
                                        Channels(Channel) = CI
                                    End If
                                Case "333"
                                    Temp = Command(1).Split(New Char() {" "c}) 'nick, channel, topic
                                    Dim Channel As String = Temp(1)
                                    If Channels.ContainsKey(Channel) Then
                                        Dim CI As IIrcClient.ChannelInformation = Channels(Channel)
                                        CI.TopicOwner = Temp(2)
                                        Channels(Channel) = CI
                                        RaiseEvent ChannelTopicChange(CI)
                                    End If
                                Case "353" ' Names list
                                    Dim Match As Match = Regex.Match(Command(1), "[^#]+(#[^\s]+)\s:([^\n]+)")
                                    If Match.Success Then
                                        Dim Channel As String = Match.Groups(1).Value
                                        Dim Users() As String = Match.Groups(2).Value.Split(New Char() {" "c})
                                        For Each User As String In Users
                                            If String.IsNullOrEmpty(User) Then Continue For
                                            Dim UserInfo As New IIrcClient.UserInformation
                                            Dim Nick As String = User
                                            Select Case User(0) 'none, v, h, @, ~
                                                Case "~"c
                                                    UserInfo.UserLevel = 4
                                                    Nick = Nick.Remove(0, 1)
                                                Case "&"c
                                                    UserInfo.UserLevel = 4
                                                    Nick = Nick.Remove(0, 1)
                                                Case "@"c
                                                    UserInfo.UserLevel = 3
                                                    Nick = Nick.Remove(0, 1)
                                                Case "%"c
                                                    UserInfo.UserLevel = 2
                                                    Nick = Nick.Remove(0, 1)
                                                Case "+"c
                                                    UserInfo.UserLevel = 1
                                                    Nick = Nick.Remove(0, 1)
                                            End Select
                                            If Channels.ContainsKey(Channel) Then
                                                If Channels(Channel).Users.ContainsKey(Nick) Then
                                                    Channels(Channel).Users.Remove(Nick)
                                                End If
                                                Channels(Channel).Users.Add(Nick, UserInfo)
                                            End If
                                        Next
                                        RaiseEvent ChannelNamesList()
                                    End If
                                Case "433", "432", "431", "436" 'nickname in use, erroneous nickname, no nickname, nick collision
                                    Kernel.IrcGenerateNick()
                                    ChangeNick(Me.Nick)
                                Case "471", "472", "473", "474", "475", "482"
                                    Temp = Command(1).Split(New Char() {" "c}, 3)
                                    RaiseEvent ChannelError(Temp(1), Temp(2).Substring(1))
                            End Select
                        Else
                            Select Case SplitMessages(0)
                                Case "PING"
                                    WriteLine(String.Format("PONG {0}", SplitMessages(1)))
                                    'MsgBox(String.Format("PONG {0}", SplitMessages(1)))
                                Case "ERROR"
                                    Disconnect()
                                Case Else
                                    Dim Match As Match = Regex.Match(Message, ":([^!]+)![^@]+@[^\s]+\s([^\s]+)\s([^\n]+)")
                                    Dim Match2 As Match
                                    If Match.Success Then
                                        Dim From As String = Match.Groups(1).Value
                                        Dim Command As String = Match.Groups(2).Value
                                        Dim Arguments As String = Match.Groups(3).Value.TrimEnd(Chr(&HA), Chr(&HD))
                                        Select Case Command
                                            Case "PRIVMSG"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:(.+)")
                                                If Match2.Success Then
                                                    Dim Destinatary As String = Match2.Groups(1).Value
                                                    Dim Msg As String = Match2.Groups(2).Value
                                                    If HiddenChannels.ContainsKey(Destinatary) Then
                                                        RaiseEvent PacketReceived(From, Destinatary, Msg)
                                                    ElseIf Channels.ContainsKey(Destinatary) Then
                                                        If Msg.StartsWith(Chr(1) & "ACTION") Then
                                                            RaiseEvent ChannelAction(From, Msg.Substring(8, Msg.Length - 9), Destinatary)
                                                        Else
                                                            RaiseEvent ChannelMessage(From, Msg, Destinatary)
                                                        End If
                                                    ElseIf String.Equals(Destinatary, Me.Nick) Then
                                                        Select Case Msg
                                                            Case One & "VERSION" & One
                                                                SendNotice(From, One & "VERSION " & IRCClientVersion & " for " & BotName & " v" & BotVersion & One)
                                                            Case Else
                                                                If Msg.StartsWith(Chr(1) & "PING") Then
                                                                    SendNotice(From, Msg)
                                                                ElseIf Msg.StartsWith(Chr(1)) Then
                                                                    'dont do anything
                                                                Else
                                                                    RaiseEvent PrivateMessage(From, Msg)
                                                                End If
                                                        End Select
                                                    End If
                                                End If
                                            Case "JOIN"
                                                Match2 = Regex.Match(Arguments, ":?(.+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    If HiddenChannelNames.Contains(Channel.ToLower) Then
                                                        If String.Equals(From, Me.Nick) Then
                                                            Dim CI As New IIrcClient.ChannelInformation
                                                            CI.Name = Channel
                                                            CI.ID = 0
                                                            CI.Users = New SortedList(Of String, IIrcClient.UserInformation)
                                                            If HiddenChannels.ContainsKey(Channel) Then
                                                                HiddenChannels.Remove(Channel)
                                                            End If
                                                            HiddenChannels.Add(Channel, CI)
                                                            RaiseEvent ChannelSelfJoinHidden(Channel)
                                                        End If
                                                    Else
                                                        If String.Equals(From, Me.Nick) Then
                                                            Dim CI As New IIrcClient.ChannelInformation
                                                            CI.Name = Channel
                                                            CI.ID = 0
                                                            CI.Users = New SortedList(Of String, IIrcClient.UserInformation)
                                                            If Channels.ContainsKey(Channel) Then
                                                                Channels.Remove(Channel)
                                                            End If
                                                            Channels.Add(Channel, CI)
                                                            RaiseEvent ChannelSelfJoin(Channel)
                                                        Else
                                                            If Channels.ContainsKey(Channel) Then
                                                                If Not Channels(Channel).Users.ContainsKey(From) Then
                                                                    Channels(Channel).Users.Add(From, New IIrcClient.UserInformation())
                                                                    RaiseEvent ChannelJoin(From, Channel)
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Case "PART"
                                                If Channels.ContainsKey(Arguments) Then
                                                    If String.Equals(From, Me.Nick) Then
                                                        Channels.Remove(Arguments)
                                                        RaiseEvent ChannelSelfPart(Arguments)
                                                    Else
                                                        If Channels(Arguments).Users.ContainsKey(From) Then
                                                            Channels(Arguments).Users.Remove(From)
                                                            RaiseEvent ChannelPart(From, Arguments)
                                                        End If
                                                    End If
                                                ElseIf HiddenChannels.ContainsKey(Arguments) AndAlso From.Equals(Me.Nick) Then
                                                    HiddenChannels.Remove(Arguments)
                                                    HiddenChannelNames.Remove(Arguments.ToLower)
                                                    RaiseEvent ChannelSelfPartHidden(Arguments)
                                                End If
                                            Case "NICK"
                                                Dim NewNick As String = Arguments.Substring(1)
                                                If From = Me.Nick Then
                                                    Me.Nick = NewNick
                                                End If
                                                For Each Channel As String In Channels.Keys
                                                    For Each Nick As String In Channels(Channel).Users.Keys
                                                        If Nick.Equals(From) Then
                                                            Dim UI As IIrcClient.UserInformation = Channels(Channel).Users(Nick)
                                                            Channels(Channel).Users.Remove(Nick)
                                                            Channels(Channel).Users.Add(NewNick, UI)
                                                            Exit For
                                                        End If
                                                    Next
                                                Next
                                                RaiseEvent NickChange(From, NewNick)
                                            Case "MODE"
                                                'this only matches modes to ppl
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s([^\s]+)\s([^\s]+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim Modes As String = Match2.Groups(2).Value
                                                    Dim Nick As String = Match2.Groups(3).Value
                                                    Match2 = Regex.Match(Modes, "([\+-][aqhov][aqhov]?)")
                                                    If Match2.Success Then
                                                        Dim UI As IIrcClient.UserInformation = Channels(Channel).Users(Nick)
                                                        Select Case Match2.Groups(1).Value
                                                            Case "+q", "+qo", "+a"
                                                                UI.UserLevel = 4
                                                            Case "+o"
                                                                UI.UserLevel = 3
                                                            Case "+h"
                                                                UI.UserLevel = 2
                                                            Case "+v"
                                                                UI.UserLevel = 1
                                                            Case "-o", "-qo", "-q", "-h", "-v", "-a"
                                                                UI.UserLevel = 0
                                                        End Select
                                                        Channels(Channel).Users(Nick) = UI
                                                        RaiseEvent ChannelMode(Nick, Match2.Groups(1).Value, Channel)
                                                    End If
                                                End If
                                            Case "TOPIC"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:(.*)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim CI As IIrcClient.ChannelInformation = Channels(Channel)
                                                    CI.Topic = Match2.Groups(2).Value
                                                    CI.TopicOwner = From
                                                    Channels(Channel) = CI
                                                    RaiseEvent ChannelTopicChange(CI)
                                                End If
                                            Case "KICK"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s([^\s]+)\s:([^\n]*)")
                                                If Match2.Success Then
                                                    Dim Nick As String = Match2.Groups(2).Value
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim KickMessage As String = Match2.Groups(3).Value
                                                    'MsgBox(From & " kicked " & Nick & ". reason: " & KickMessage)
                                                    If Channels.ContainsKey(Channel) Then
                                                        If Channels(Channel).Users.ContainsKey(Nick) Then
                                                            If Nick = Me.Nick Then
                                                                RaiseEvent ChannelSelfKick(From, KickMessage, Channel)
                                                                Channels.Remove(Channel)
                                                            Else
                                                                RaiseEvent ChannelKick(From, Nick, KickMessage, Channel)
                                                                Channels(Channel).Users.Remove(Nick)
                                                            End If
                                                        End If
                                                    ElseIf HiddenChannels.ContainsKey(Channel) Then
                                                        If Nick = Me.Nick Then
                                                            RaiseEvent ChannelSelfKickHidden(From, KickMessage, Channel)
                                                            HiddenChannels.Remove(Channel)
                                                            HiddenChannelNames.Remove(Channel)
                                                        End If
                                                    End If
                                                End If
                                            Case "QUIT"
                                                For Each Channel As String In Channels.Keys
                                                    If Channels(Channel).Users.ContainsKey(From) Then
                                                        Channels(Channel).Users.Remove(From)
                                                    End If
                                                Next
                                                RaiseEvent QuitIrc(From, Arguments.Substring(1))
                                            Case "NOTICE"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:([^\n]+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim Msg As String = Match2.Groups(2).Value
                                                    If Channels.ContainsKey(Channel) Then
                                                        If Channels(Channel).Users.ContainsKey(From) Then
                                                            If Channels(Channel).Users(From).UserLevel >= 4 Then
                                                                RaiseEvent ChannelBroadcast(From, Msg, Channel)
                                                            End If
                                                        End If
                                                    Else
                                                        If Channel.Equals(Me.Nick, StringComparison.CurrentCultureIgnoreCase) Then
                                                            RaiseEvent Notice(From, Msg)
                                                        End If
                                                    End If
                                                End If
                                            Case "INVITE"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:([^\n]+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(2).Value
                                                    Dim TTBChannel As String = "#tibiatekbot"
                                                    Dim Nick As String = Match2.Groups(1).Value
                                                    Dim Found As Boolean = False
                                                    For Each Chan As String In Channels.Keys
                                                        If Chan.ToLower.Equals(TTBChannel) Then
                                                            TTBChannel = Chan
                                                            Found = True
                                                        End If
                                                    Next
                                                    If Found Then
                                                        If Channels(TTBChannel).Users.ContainsKey(From) Then
                                                            If Channels(TTBChannel).Users(From).UserLevel >= 4 Then
                                                                Join(Channel)
                                                            End If
                                                        End If
                                                    End If
                                                    RaiseEvent Invite(From, Channel)
                                                End If
                                        End Select
                                    End If
                            End Select
                        End If
                    Catch Ex As SocketException
                        'MsgBox("socket exception")
                    Catch Ex As IOException
                        '    MsgBox("io exception")
                    End Try
                Loop
            Catch Ex As ThreadAbortException
                'MsgBox("threadabortexception")
            Catch Ex As Exception
                ShowError(Ex)
            End Try
        Loop While IsConnected
        Disconnect()
    End Sub





End Class
