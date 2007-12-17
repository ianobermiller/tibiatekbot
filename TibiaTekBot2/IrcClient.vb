Imports System, System.Net, System.Net.Sockets, System.IO, System.Math, _
    System.Threading, System.Text.RegularExpressions, TibiaTekBot.Constants

Public Class IrcClient

    Public Structure UserInformation
        'Public ChannelOperator As Boolean
        'Public Voiced As Boolean
        Public UserLevel As Integer
    End Structure

    Public Structure ChannelInformation
        Public Name As String
        Public Topic As String
        Public TopicOwner As String
        Public Users As SortedList(Of String, UserInformation)
        Public ID As Integer
    End Structure

    Public Const One As Char = Chr(1)
    'Public Const Version As String = "TibiaTek IRC Client for TibiaTek Bot v2.1 (http://www.tibiatek.com/)"

    Public Event EventChannelJoin As ChannelJoin
    Public Event EventChannelSelfJoin As ChannelSelfJoin
    Public Event EventChannelKick As ChannelKick
	Public Event EventChannelSelfKick As ChannelSelfKick
    Public Event EventNickChange As NickChange
    Public Event EventChannelPart As ChannelPart
    Public Event EventChannelSelfPart As ChannelSelfPart
    Public Event EventQuit As DQuit
    Public Event EventChannelTopicChange As ChannelTopicChange
    Public Event EventRawMessage As RawMessage
    Public Event EventConnecting As Connecting
    Public Event EventDisconnected As Disconnected
    Public Event EventConnected As Connected
    Public Event EventChannelMessage As ChannelMessage
    Public Event EventPrivateMessage As PrivateMessage
    Public Event EventEndMOTD As EndMOTD
    Public Event EventChannelError As ChannelError
    Public Event EventChannelMode As ChannelMode
    Public Event EventChannelNamesList As ChannelNamesList
    Public Event EventChannelAction As ChannelAction
    Public Event EventChannelBroadcast As ChannelBroadcast
    Public Event EventNotice As Notice

    Public Delegate Sub ChannelJoin(ByVal Nick As String, ByVal Channel As String)
    Public Delegate Sub ChannelSelfJoin(ByVal Channel As String)
    Public Delegate Sub ChannelKick(ByVal NickKicker As String, ByVal NickKicked As String, ByVal KickMessage As String, ByVal Channel As String)
    Public Delegate Sub ChannelSelfKick(ByVal NickKicker As String, ByVal KickMessage As String, ByVal Channel As String)
    Public Delegate Sub ChannelMode(ByVal Nick As String, ByVal UserMode As String, ByVal Channel As String)
    Public Delegate Sub NickChange(ByVal UserOldNick As String, ByVal UserNewNick As String)
    Public Delegate Sub ChannelPart(ByVal Nick As String, ByVal Channel As String)
    Public Delegate Sub ChannelSelfPart(ByVal Channel As String)
    Public Delegate Sub DQuit(ByVal Nick As String, ByVal Message As String)
    Public Delegate Sub ChannelNamesList()
    Public Delegate Sub ChannelTopicChange(ByVal ChannelInfo As ChannelInformation)
    Public Delegate Sub ChannelMessage(ByVal Nick As String, ByVal Message As String, ByVal Channel As String)
    Public Delegate Sub PrivateMessage(ByVal Nick As String, ByVal Message As String)
    Public Delegate Sub ChannelError(ByVal Channel As String, ByVal Message As String)
    Public Delegate Sub RawMessage(ByVal RawMessage As String)
    Public Delegate Sub ChannelAction(ByVal Nick As String, ByVal Action As String, ByVal Channel As String)
    Public Delegate Sub ChannelBroadcast(ByVal Nick As String, ByVal Message As String, ByVal Channel As String)
    Public Delegate Sub Notice(ByVal Nick As String, ByVal Message As String)
    Public Delegate Sub Connecting()
    Public Delegate Sub Connected()
    Public Delegate Sub Disconnected()
    Public Delegate Sub EndMOTD()

    'Public ChannelInfo As ChannelInformation
    Public Channels As New SortedList(Of String, ChannelInformation)
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

    'Private RetryCount As Integer = 0
    Public DoMainLoopThread As Thread
    Public DoMainLoopThreadStart As ThreadStart

    'Private Timeout As Integer = 5000

    Dim CanReconnect As Boolean = True
    Dim WasConnected As Boolean = True
#Region " Constructors "
    Public Sub New()
        Try
            'ChannelInfo.Users = New SortedList(Of String, UserInformation)
            If Not DoMainLoopThread Is Nothing Then
                DoMainLoopThread.Abort()
            End If
            While Not DoMainLoopThread Is Nothing : End While
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub New(ByVal Server As String, ByVal Port As Integer)
        Me.New()
        Try
            Me.Server = Server
            Me.Port = Port
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
#End Region

#Region " Properties "
    Public ReadOnly Property IsConnected() As Boolean
        Get
            Try
                Return (Not Client Is Nothing AndAlso Not Client.Client Is Nothing AndAlso Client.Client.Connected)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Get
    End Property
    Public Property Password() As String
        Get
            Return Me._Password
        End Get
        Set(ByVal value As String)
            Me._Password = value
        End Set
    End Property
    Public Property User() As String
        Get
            Return Me._User
        End Get
        Set(ByVal value As String)
            Me._User = value
        End Set
    End Property
    Public Property Invisible() As Boolean
        Get
            Return Me._IsInvisible
        End Get
        Set(ByVal value As Boolean)
            Me._IsInvisible = value
        End Set
    End Property
    Public Property RealName() As String
        Get
            Return Me._RealName
        End Get
        Set(ByVal value As String)
            Me._RealName = value
        End Set
    End Property
    Public Property Server() As String
        Get
            Return Me._Server
        End Get
        Set(ByVal value As String)
            Me._Server = value
        End Set
    End Property
    Public Property Port() As Integer
        Get
            Try
                If Port > System.Net.IPEndPoint.MinPort OrElse Port < System.Net.IPEndPoint.MaxPort Then
                    Return Me._Port
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Get
        Set(ByVal value As Integer)
            Me._Port = value
        End Set
    End Property
    Public Property Nick() As String
        Get
            Return Me._Nick
        End Get
        Set(ByVal value As String)
            Try
                Me._Nick = FormatNick(value)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Function Connect() As Boolean
        Try
            CanReconnect = True
            RaiseEvent EventConnecting()
            Me.Client = Nothing
            Me.Client = New TcpClient()
            Me.Client.ReceiveTimeout = 24 * 60 * 60 * 1000 ' 2 mins
            Me.Client.SendTimeout = 24 * 60 * 60 * 1000
            Me.Client.Connect(Me._Server, Me._Port)
            WasConnected = True
            Me.ConnectionStream = Me.Client.GetStream()
            Dim Encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1")
            Me.Reader = New StreamReader(Me.ConnectionStream, Encoding)
            Me.Writer = New StreamWriter(Me.ConnectionStream, Encoding)
            RaiseEvent EventConnected()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function
    Public Sub Disconnect()
		If WasConnected AndAlso Not Client Is Nothing AndAlso Client.Connected Then
			WasConnected = False
			If Not Client.Client Is Nothing Then
				Client.Client.Disconnect(True)
			End If
		End If
		RaiseEvent EventDisconnected()
        'DoMainLoopThread.Abort()
    End Sub
    Public Function GetUserLevel(ByVal Nickname As String, ByVal Channel As String) As Integer
        If Channels Is Nothing Then Return False
        If Channels.ContainsKey(Channel) Then
            For Each User As String In Channels(Channel).Users.Keys
                If String.Equals(Nickname, User, StringComparison.CurrentCultureIgnoreCase) Then
                    Return Channels(Channel).Users(User).UserLevel
                End If
            Next
        End If
    End Function

    Public Sub WriteLine(ByVal Command As String)
        Try
            If Writer Is Nothing OrElse Client Is Nothing OrElse Not Client.Connected Then Exit Sub
            Writer.WriteLine(Command)
            Writer.Flush()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub MainLoop()
        Try
            If Not WasConnected Then Exit Sub
            DoMainLoopThreadStart = New ThreadStart(AddressOf Me.DoMainLoop)
            DoMainLoopThread = New Thread(DoMainLoopThreadStart)
            DoMainLoopThread.Start()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Commands "
    Public Sub Quit(Optional ByVal Reason As String = "Good bye! [" & IRCClientVersion & "]")
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
    Public Sub ChangeNick(ByVal NewNick As String)
        WriteLine(String.Format("NICK {0}", NewNick))
    End Sub
    Public Sub Identify()
        If Not WasConnected Then Exit Sub
        WriteLine(String.Format("USER {0} {1} * :{2}", Me._User, IIf(Me._IsInvisible, "8", "0"), Me.RealName))
        ChangeNick(Me._Nick)
    End Sub
    Public Sub Part(ByVal Channel As String, Optional ByVal Reason As String = "Good Bye! [" & IRCClientVersion & "]")
        Try
            If Not WasConnected Then Exit Sub
            If Not String.IsNullOrEmpty(Channel) Then
                WriteLine(String.Format("PART {0}", Channel))
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub SendNotice(ByVal Destinatary As String, ByVal Message As String)
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("NOTICE {0} :{1}", Destinatary, Message))
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Message As String, ByVal Destinatary As String)
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("PRIVMSG {0} :{1}", Destinatary, Message))
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SpeakToServer(ByVal Message As String, ByVal Server As String)
        Try
            If Not WasConnected Then Exit Sub
            WriteLine(String.Format("{0} :{1}", Server, Message))
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Rejoin(ByVal Channel As String)
        Try
            If Channels.ContainsKey(Channel) Then
                Part(Channel)
                Thread.Sleep(2000)
                Join(Channel)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Join(ByVal Channel As String)
        Try
            If Not WasConnected Then Exit Sub
            If Not String.IsNullOrEmpty(Channel) AndAlso Not Channels.ContainsKey(Channel) Then
                WriteLine(String.Format("JOIN {0}", Channel))
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return String.Empty
    End Function
    Private Function IsSpecial(ByVal C As Char) As Boolean
        Try
            Return (Asc(C) >= &H5B AndAlso Asc(C) <= &H60) _
                 OrElse (Asc(C) >= &H7B AndAlso Asc(C) <= &H7D)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function
    Protected Overrides Sub Finalize()
        If Not Me.Client Is Nothing AndAlso Not Me.Client.Client Is Nothing Then
            If Me.Client.Connected Then
                Me.Client.Client.Close()
            End If
        End If
        If Not Me.DoMainLoopThread Is Nothing Then Me.DoMainLoopThread.Abort()
        MyBase.Finalize()
    End Sub
#End Region

    Public Sub DoMainLoop()
        Dim FirstTime As Boolean = True
        Do
            Try
                Do While IsConnected
                    If Not Core.InGame Then
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
                        'Core.ConsoleWrite(Message)
                        RaiseEvent EventRawMessage(Message)
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
                                    RaiseEvent EventEndMOTD()
                                Case "332" ' Topic
                                    Temp = Command(1).Split(New Char() {" "c}, 3)
                                    Dim Channel As String = Temp(1)

                                    If Channels.ContainsKey(Channel) Then
                                        Dim CI As ChannelInformation = Channels(Channel)
                                        CI.Topic = Temp(2).Substring(1)
                                        Channels(Channel) = CI
                                    End If
                                Case "333"
                                    Temp = Command(1).Split(New Char() {" "c}) 'nick, channel, topic
                                    Dim Channel As String = Temp(1)
                                    If Channels.ContainsKey(Channel) Then
                                        Dim CI As ChannelInformation = Channels(Channel)
                                        CI.TopicOwner = Temp(2)
                                        Channels(Channel) = CI
                                        RaiseEvent EventChannelTopicChange(CI)
                                    End If
                                Case "353" ' Names list
                                    Dim Match As Match = Regex.Match(Command(1), "[^#]+(#[^\s]+)\s:([^\n]+)")
                                    If Match.Success Then
                                        Dim Channel As String = Match.Groups(1).Value
                                        Dim Users() As String = Match.Groups(2).Value.Split(New Char() {" "c})
                                        For Each User As String In Users
                                            If String.IsNullOrEmpty(User) Then Continue For
                                            Dim UserInfo As New UserInformation
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
                                            If Channels(Channel).Users.ContainsKey(Nick) Then
                                                Channels(Channel).Users.Remove(Nick)
                                            End If
                                            Channels(Channel).Users.Add(Nick, UserInfo)
                                        Next
                                        RaiseEvent EventChannelNamesList()
                                    End If
                                Case "433", "432", "431", "436" 'nickname in use, erroneous nickname, no nickname, nick collision
                                    Core.IrcGenerateNick()
                                    ChangeNick(Me.Nick)
                                Case "471", "472", "473", "474", "475", "482"
                                    Temp = Command(1).Split(New Char() {" "c}, 3)
                                    RaiseEvent EventChannelError(Temp(1), Temp(2).Substring(1))
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
                                                    If Channels.ContainsKey(Destinatary) Then
                                                        If Msg.StartsWith(Chr(1) & "ACTION") Then
                                                            RaiseEvent EventChannelAction(From, Msg.Substring(8, Msg.Length - 9), Destinatary)
                                                        Else
                                                            RaiseEvent EventChannelMessage(From, Msg, Destinatary)
                                                        End If
                                                    ElseIf String.Equals(Destinatary, Me.Nick) Then
                                                        Select Case Msg
                                                            Case One & "VERSION" & One
                                                                SendNotice(From, One & "VERSION " & IRCClientVersion & One)
                                                            Case Else
                                                                If Msg.StartsWith(Chr(1) & "PING") Then
                                                                    SendNotice(From, Msg)
                                                                ElseIf Msg.StartsWith(Chr(1)) Then
                                                                    'dont do anything
                                                                Else
                                                                    RaiseEvent EventPrivateMessage(Destinatary, Msg)
                                                                End If
                                                        End Select
                                                    End If
                                                    End If
                                            Case "JOIN"
                                                Match2 = Regex.Match(Arguments, ":?(.+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    If String.Equals(From, Me.Nick) Then
                                                        Dim CI As New ChannelInformation
                                                        CI.Name = Channel
                                                        CI.ID = 0
                                                        CI.Users = New SortedList(Of String, UserInformation)
                                                        If Channels.ContainsKey(Channel) Then
                                                            Channels.Remove(Channel)
                                                        End If
                                                        Channels.Add(Channel, CI)
                                                        RaiseEvent EventChannelSelfJoin(Channel)
                                                    Else
                                                        If Not Channels(Channel).Users.ContainsKey(From) Then
                                                            Channels(Channel).Users.Add(From, New UserInformation())
                                                            RaiseEvent EventChannelJoin(From, Channel)
                                                        End If
                                                    End If
                                                End If
                                            Case "PART"
                                                If String.Equals(From, Me.Nick) AndAlso Channels.ContainsKey(Arguments) Then
                                                    Channels.Remove(Arguments)
                                                    RaiseEvent EventChannelSelfPart(Arguments)
                                                ElseIf Channels.ContainsKey(Arguments) Then
                                                    If Channels(Arguments).Users.ContainsKey(From) Then
                                                        Channels(Arguments).Users.Remove(From)
                                                        RaiseEvent EventChannelPart(From, Arguments)
                                                    End If
                                                End If
                                            Case "NICK"
                                                Dim NewNick As String = Arguments.Substring(1)
                                                If From = Me.Nick Then
                                                    Me.Nick = NewNick
                                                End If
                                                For Each Channel As String In Channels.Keys
                                                    For Each Nick As String In Channels(Channel).Users.Keys
                                                        If Nick.Equals(From) Then
                                                            Dim UI As UserInformation = Channels(Channel).Users(Nick)
                                                            Channels(Channel).Users.Remove(Nick)
                                                            Channels(Channel).Users.Add(NewNick, UI)
                                                            Exit For
                                                        End If
                                                    Next
                                                Next
                                                RaiseEvent EventNickChange(From, NewNick)
                                            Case "MODE"
                                                'this only matches modes to ppl
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s([^\s]+)\s([^\s]+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim Modes As String = Match2.Groups(2).Value
                                                    Dim Nick As String = Match2.Groups(3).Value
                                                    Match2 = Regex.Match(Modes, "([\+-][aqhov][aqhov]?)")
                                                    If Match2.Success Then
                                                        Dim UI As UserInformation = Channels(Channel).Users(Nick)
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
                                                        RaiseEvent EventChannelMode(Nick, Match2.Groups(1).Value, Channel)
                                                    End If
                                                End If
                                            Case "TOPIC"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:(.*)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim CI As ChannelInformation = Channels(Channel)
                                                    CI.Topic = Match2.Groups(2).Value
                                                    CI.TopicOwner = From
                                                    Channels(Channel) = CI
                                                    RaiseEvent EventChannelTopicChange(CI)
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
                                                                RaiseEvent EventChannelSelfKick(From, KickMessage, Channel)
                                                                Channels.Remove(Channel)
                                                            Else
                                                                RaiseEvent EventChannelKick(From, Nick, KickMessage, Channel)
                                                                Channels(Channel).Users.Remove(Nick)
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Case "QUIT"
                                                For Each Channel As String In Channels.Keys
                                                    If Channels(Channel).Users.ContainsKey(From) Then
                                                        Channels(Channel).Users.Remove(From)
                                                    End If
                                                Next
                                                RaiseEvent EventQuit(From, Arguments.Substring(1))
                                            Case "NOTICE"
                                                Match2 = Regex.Match(Arguments, "([^\s]+)\s:([^\n]+)")
                                                If Match2.Success Then
                                                    Dim Channel As String = Match2.Groups(1).Value
                                                    Dim Msg As String = Match2.Groups(2).Value
                                                    If Channels.ContainsKey(Channel) Then
                                                        If Channels(Channel).Users.ContainsKey(From) Then
                                                            If Channels(Channel).Users(From).UserLevel >= 3 Then
                                                                RaiseEvent EventChannelBroadcast(From, Msg, Channel)
                                                            End If
                                                        End If
                                                    Else
                                                        If Channel.Equals(Me.Nick, StringComparison.CurrentCultureIgnoreCase) Then
                                                            RaiseEvent EventNotice(From, Msg)
                                                        End If
                                                    End If
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
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Loop While IsConnected
        Disconnect()
    End Sub


End Class
