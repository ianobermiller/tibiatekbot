'This is an example of how to use the event MessageReceived

Imports System, Scripting
Imports System.Threading, System.Text.RegularExpressions
Imports Microsoft.VisualBasic.VBMath

Public Class OnMessageReceived
    Implements IScript
    Dim WithEvents Kernel As IKernel
    Dim WithEvents Client As ITibia
    Dim Stoped As Boolean = False

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Client = Kernel.Client
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        ' Paused
        Stoped = True
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        ' Resume
        Stoped = False
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Stoped = True
    End Sub

    Private Sub Client_MessageReceived(ByVal Args As Scripting.Events.Events.MessageReceivedEventArgs) Handles Client.MessageReceived
        If Stoped Then Exit Sub
        Dim Channelname As String = ""

        Select Case Args.MessageType
            Case ITibia.MessageType.Channel
                Select Case Args.ChannelType
                    Case ITibia.Channel.GameChat
                        Channelname = "GameChat"
                    Case ITibia.Channel.Trade
                        Channelname = "Trade"
                        '
                        '
                        '...
                End Select
                Kernel.ConsoleRead("New message in " & Channelname & ": " & Args.CharacterName & ": " & Args.Message)
            Case ITibia.MessageType.Default
                '
            Case ITibia.MessageType.PrivateMessage
                'AutoResponder?
                Kernel.ConsoleRead("Msg from " & Args.CharacterName & "[" & Args.CharacterLevel & "]" & _
                                    " Msg: " & Args.Message)
                Dim MsgsToSend() As String = {""}

                Dim MatchObj As Match = Regex.Match(Args.Message, "hi|hiho|hello|hey there", RegexOptions.IgnoreCase)

                Dim strString As String = ""
                Dim MsgNumber As Integer = 0

                If MatchObj.Success Then
                    'Respond with a random message
                    MsgsToSend(0) = "hello i'm a little bussy"
                    MsgsToSend(1) = "hello :) , sec please"
                    MsgsToSend(2) = "hey there, brb"
                    MsgNumber = RandomNumber(3)

                    'wait 2 seconds
                    Thread.Sleep(2000)

                    Dim SP As New ServerPacketBuilder(Kernel.Proxy)
                    ' Send the message to the player
                    SP.Speak(Args.CharacterName, MsgsToSend(MsgNumber))
                    SP.Send()

                    Kernel.ConsoleWrite(Args.Message & " --> " & MsgsToSend(MsgNumber))
                End If
        End Select

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
End Class