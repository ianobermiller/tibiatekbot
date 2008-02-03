Imports System, Scripting, Microsoft.VisualBasic.Conversion, Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.Strings, System.Threading
Imports System.Windows.Forms
Public Class Script2
    Implements IScript

    Dim WithEvents Kernel As IKernel
    Dim WithEvents Timer As ThreadTimer
    Dim tryLogin_ As Boolean
    Public Sub New()
        Timer = New ThreadTimer(10000)
        Timer.StopTimer()
    End Sub

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        Timer.StopTimer()
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        Timer.StartTimer()
    End Sub

    Public Sub Timer_OnExecute() Handles Timer.OnExecute
        If Not Kernel.Client.IsConnected Then
            Try

                Login(2785150, "changoleon1", 2)

            Catch ex As Exception
                Kernel.ConsoleError(ex.Message)
            End Try
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Timer.Dispose()
    End Sub

    Sub Login(ByVal account As Integer, ByVal password As String, ByVal charindex As Integer)
        Dim tryLogin As Boolean = False
        Dim Client As ITibia = Kernel.Client
        Dim Retries As Integer = 100
        Dim ActiveDialog As String = Client.GetCurrentDialog
        Dim i As Integer
        Dim x As Long = 0
        Dim y As Long = 0

        ' Get "Enter game" cordz
        x = (12 * Screen.PrimaryScreen.Bounds.Width) / 100
        y = (65 * Screen.PrimaryScreen.Bounds.Height) / 100

        If Not Kernel.Client.IsConnected Then
            Dim COntinue_ As Boolean = False
            Do Until Kernel.Client.IsConnected
                Application.DoEvents()
                ActiveDialog = Client.GetCurrentDialog
                If ActiveDialog = "" And tryLogin = False Then
                    Client.Activate()
                    Thread.Sleep(500)
                    Clic(x, y) 'Clic Enter game button
                    Thread.Sleep(1000)

                    tryLogin = True
                End If
                If ActiveDialog = "Enter Game" Then
                    Send_Keys(account)
                    Thread.Sleep(500)
                    SendKeys(Keys.Tab)
                    Thread.Sleep(500)
                    Send_Keys(password)
                    Thread.Sleep(500)
                    SendKeys(Keys.Enter)

                End If
                If ActiveDialog.ToLower.Contains("select") Then
                    If Not tryLogin Then
                        SendKeys(Keys.Escape)
                        Thread.Sleep(100)
                    End If
                    'Go to the first Character in the list
                    For i = 1 To 50
                        SendKeys(Keys.Up)
                    Next
                    Thread.Sleep(500)
                    'Go to the Character index
                    If charindex > 1 Then
                        For i = 1 To charindex
                            SendKeys(Keys.Down)
                            Thread.Sleep(100)
                        Next
                    End If
                    Thread.Sleep(500)
                    SendKeys(Keys.Enter)
                End If

                If ActiveDialog.ToLower.Contains("message") Then
                    SendKeys(Keys.Enter)
                ElseIf ActiveDialog.ToLower.Contains("connection failed") Then
                    SendKeys(Keys.Enter)
                    tryLogin = False
                End If

                Thread.Sleep(200)
                Retries -= 1
                If Retries <= 0 Then
                    Exit Do
                End If
            Loop

        End If
    End Sub


#Region "Clic"


    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_LBUTTONUP As Integer = &H202
    Public Const MK_CONTROL = &H8
    Public Const MK_LBUTTON = &H1
    Public Const MK_RBUTTON = &H2
    Public Const MK_MBUTTON = &H10
    Public Const MK_SHIFT = &H4

    Function MakeDWord(ByVal LoWord As Long, ByVal HiWord As Long) As Long
        MakeDWord = (HiWord * &H10000) Or (LoWord And &HFFFF&)
    End Function

    Public Sub Clic(ByVal x As Integer, ByVal y As Integer)
        Dim targetXY As Long, ReturnValue As Integer
        targetXY = MakeDWord((x), (y))
        ReturnValue = Kernel.Client.SendMessage(WM_MOUSEMOVE, CLng(0), targetXY) 'Move mouse to spot
        ReturnValue = Kernel.Client.SendMessage(WM_LBUTTONDOWN, CLng(MK_LBUTTON), targetXY) 'Click down
        ReturnValue = Kernel.Client.SendMessage(WM_LBUTTONUP, CLng(MK_LBUTTON), targetXY) 'Click up
    End Sub

#End Region

#Region "SendKeysModule"
    ' Created By  : Kevin Wilson
    ' Traslation from VB 6.0 to VB.net By AngelBroz

    Public Structure KeyboardBytes
        Dim kbByte() As Byte
    End Structure


    Public kbKeys As KeyboardBytes
    Public Const KEY_SHIFT = &H10
    Public Const KEY_CAPITAL = &H14
    Public Const KEYEVENTF_KEYUP = &H2

    Public Declare Sub keybd_event Lib "USER32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long)
    Public Declare Function MapVirtualKey Lib "USER32" Alias "MapVirtualKeyA" (ByVal wCode As Long, ByVal wMapType As Long) As Long
    Public Declare Function GetAsyncKeyState Lib "USER32" (ByVal vKey As Long) As Integer
    Public Declare Function GetKeyboardState Lib "USER32" (ByVal kbArray As KeyboardBytes) As Long
    Public Declare Function GetKeyState Lib "USER32" (ByVal nVirtKey As Long) As Integer
    Public Declare Function VkKeyScan Lib "USER32" Alias "VkKeyScanA" (ByVal ccChar As Byte) As Integer
    Public Declare Function CharToOem Lib "USER32" Alias "CharToOemA" (ByVal lpszSrc As String, ByVal lpszDst As String) As Long
    Public Declare Function OemKeyScan Lib "USER32" (ByVal wOemChar As Long) As Long

    Public Function VirtualKey_Check(ByVal VirtualKey As Long) As Boolean
        On Error Resume Next

        If VirtualKey > 255 Then
            Exit Function
        ElseIf VirtualKey < 0 Then
            Exit Function
        End If

        VirtualKey_Check = CBool(GetAsyncKeyState(VirtualKey))

    End Function

    Public Function VirtualKey_Send(ByVal VirtualKey As Long, Optional ByVal KeyDownOnly As Boolean = False, Optional ByVal KeyUpOnly As Boolean = False)
        On Error Resume Next

        Dim bytVirtKey As Byte

        If VirtualKey > 255 Then
            VirtualKey_Send = 0
        ElseIf VirtualKey < 0 Then
            VirtualKey_Send = 0
        End If

        If KeyDownOnly = True Then
            keybd_event(VirtualKey, 0, 0, 0)
        ElseIf KeyUpOnly = True Then
            keybd_event(VirtualKey, 0, KEYEVENTF_KEYUP, 0)
        Else
            keybd_event(VirtualKey, 0, 0, 0)
            keybd_event(VirtualKey, 0, KEYEVENTF_KEYUP, 0)
        End If

    End Function

    Public Function WaitForKeyPress(ByVal VirtualKey As Long, Optional ByVal EscapeKey1 As Long = 999, Optional ByVal EscapeKey2 As Long = 999) As Boolean
        On Error Resume Next

        While GetAsyncKeyState(VirtualKey) = 0
            Application.DoEvents()
            If EscapeKey1 <> 999 Then
                If GetAsyncKeyState(EscapeKey1) <> 0 Then
                    GoTo TheEnd
                End If
            End If
            If EscapeKey2 <> 999 Then
                If GetAsyncKeyState(EscapeKey2) <> 0 Then
                    GoTo TheEnd
                End If
            End If
            Application.DoEvents()
        End While

TheEnd:

        WaitForKeyPress = True

    End Function

    Public Function Send_Keys(ByVal StringToEnter As String, Optional ByVal SendAccurately As Boolean = True) As Boolean
        Try
            Dim MyCounter As Integer
            Dim boolShift As Boolean
            Dim strChar As String
            Dim strOemChar As String
            Dim bytVKey As Byte
            Dim bytScan As Byte
            Dim bytVirtKey As Byte
            Dim intCAPLOCK As Integer

            For MyCounter = 1 To Len(StringToEnter)
                strChar = Mid(StringToEnter, MyCounter, 1)


                If strChar = "~" Or strChar = "!" Or strChar = "@" Or strChar = "#" _
                Or strChar = "$" Or strChar = "%" Or strChar = "^" Or strChar = "&" _
                Or strChar = "*" Or strChar = "(" Or strChar = ")" Or strChar = "_" _
                Or strChar = "+" Or strChar = "|" Or strChar = "<" Or strChar = ">" _
                Or strChar = "?" Or strChar = ":" Or strChar = Chr(34) _
                Or strChar = "{" Or strChar = "}" Then
                    boolShift = True
                    bytVirtKey = 42
                    keybd_event(KEY_SHIFT, bytVirtKey, 0, 0)
                ElseIf Asc(strChar) >= 65 And Asc(strChar) <= 90 Then
                    intCAPLOCK = GetKeyState(KEY_CAPITAL)
                    If intCAPLOCK = 0 Then
                        boolShift = True
                        bytVirtKey = 42
                        keybd_event(KEY_SHIFT, bytVirtKey, 0, 0)
                    End If
                ElseIf Asc(strChar) >= 97 And Asc(strChar) <= 122 Then
                    intCAPLOCK = GetKeyState(KEY_CAPITAL)
                    If intCAPLOCK = 1 Then
                        boolShift = True
                        bytVirtKey = 42
                        keybd_event(KEY_SHIFT, bytVirtKey, 0, 0)
                    End If
                End If

                bytVKey = VkKeyScan(Asc(strChar)) And &HFF
                strOemChar = "  "
                CharToOem(Left(strChar, 1), strOemChar)

                bytScan = OemKeyScan(Asc(strOemChar)) And &HFF

                keybd_event(bytVKey, bytScan, 0, 0)
                keybd_event(bytVKey, bytScan, KEYEVENTF_KEYUP, 0)
                If boolShift = True Then
                    boolShift = False
                    keybd_event(KEY_SHIFT, bytVirtKey, KEYEVENTF_KEYUP, 0)
                End If

                If SendAccurately = True Then
                    Application.DoEvents()
                End If
                Threading.Thread.Sleep(50)
            Next MyCounter

            Send_Keys = True

            Exit Function
        Catch ex As Exception
            Send_Keys = False
        End Try

    End Function

    Public Sub SendKeys(ByVal Key As Keys)
        keybd_event(Key, 0, 0, 0) ' Key DOwn
        keybd_event(Key, 0, 2, 0) ' Key up
    End Sub
#End Region

End Class