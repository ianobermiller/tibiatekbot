Imports System, Scripting, Microsoft.VisualBasic.Conversion
Imports Microsoft.VisualBasic.Strings, System.Threading
Imports System.Windows.Forms, System.Text.RegularExpressions

Public Class Script232
    Implements IScript

    Dim Account As String = "12345"   ' Enter your account number here
    Dim Password As String = "BlaBla"  ' Enter your password here
    Dim CharacterIndex As Integer = 1   ' Enter your Character Index here (1 = First Character in your Character List)

    Dim WithEvents Kernel As IKernel
    Dim WithEvents Timer As ThreadTimer
    Dim LoginRunning As Boolean


    Public Sub New()
        Timer = New ThreadTimer(30000)
        Timer.StopTimer()
    End Sub

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("relogin", AddressOf MyCommand)
        Kernel.ConsoleWrite("relogin script loaded, usage: &relogin <on|off>")
        Kernel.ConsoleWrite("Note: Edit the script to set your data, Account, password, Character, Save it and reload it.")
    End Sub
    Public Sub MyCommand(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case Value.ToLower
            Case "off"
                Timer.StopTimer()
                Kernel.ConsoleWrite("Script stopped")
            Case "on"
                Timer.StartTimer()
                Kernel.ConsoleWrite("Script started")
            Case Else
                Kernel.ConsoleWrite("-.- its  ""&relogin on"" or ""&relogin off"" -.-")
        End Select
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
                Login(Account, Password, CharacterIndex)
            Catch ex As Exception
                Kernel.ConsoleError(ex.Message)
            End Try
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Timer.Dispose()
        Kernel.CommandParser.Remove("relogin")
    End Sub

    Sub Login(ByVal account As Integer, ByVal password As String, ByVal charindex As Integer)
        If LoginRunning Then Exit Sub
        LoginRunning = True
        Dim tryLogin As Boolean = False
        Dim Client As ITibia = Kernel.Client
        Dim Retries As Integer = 100
        Dim ActiveDialog As String
        Dim i As Integer, x As Integer, y As Integer
        Dim Hidden As Boolean = False
        Dim Minimized As Boolean = False

        If Not Kernel.Client.IsConnected Then

            Do Until Kernel.Client.IsConnected
                ActiveDialog = Client.DialogCaption
                If ActiveDialog = "" And tryLogin = False Then
                    Dim TibiaRect As New RECT

                    GetWindowRect(Client.WindowHandle, TibiaRect) ' Get the tibia Rect
                    x = TibiaRect.Left + 124    ' Coordz of Enter Game Button
                    y = TibiaRect.Bottom - 224  '

                    If Client.WindowState = ITibia.WindowStates.Hidden Then
                        Hidden = True
                    End If

                    If Client.WindowState = ITibia.WindowStates.Minimized Then
                        Minimized = True
                    End If

                    Client.Show()
                    ShowWindow(Client.WindowHandle, ShowState.SW_SHOWMAXIMIZED)
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

                If ActiveDialog.ToLower.Contains("sorry") Then
                    SendKeys(Keys.Escape)
                    LoginRunning = False
                    Exit Sub
                End If

                Thread.Sleep(200)
                Retries -= 1
                If Retries <= 0 Then
                    Exit Do
                End If
            Loop

        End If
        If Hidden Then Client.Hide()
        If Minimized Then Client.Minimize()
        LoginRunning = False
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

    Private Const MOUSEEVENTF_LEFTDOWN = &H2
    Private Const MOUSEEVENTF_LEFTUP = &H4

    Private Structure RECT        Public Left As Integer        Public Top As Integer        Public Right As Integer        Public Bottom As Integer
    End Structure
    Private Enum ShowState As UInteger
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_SHOWMAXIMIZED = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
        SW_SHOWDEFAULT = 10
    End Enum
    Private Declare Function ShowWindow Lib "user32" (ByVal handle As IntPtr, ByVal nCmd As Int32) As Boolean
    Private Declare Function GetWindowRect Lib "user32" (ByVal hWnd As Integer, ByRef lpRect As RECT) As Integer    Declare Function SetCursorPos Lib "user32" Alias "SetCursorPos" (ByVal x As Integer, ByVal y As Integer) As Integer
    Private Declare Sub mouse_event Lib "user32" _
                        (ByVal dwFlags As Long, _
                         ByVal dx As Long, _
                         ByVal dy As Long, _
                         ByVal cButtons As Long, _
                        ByVal dwExtraInfo As Long)


    Function MakeDWord(ByVal LoWord As Integer, ByVal HiWord As Integer) As Integer
        MakeDWord = (HiWord * &H10000) Or (LoWord And &HFFFF&)
    End Function

    Public Sub Clic(ByVal x As Integer, ByVal y As Integer)
        SetCursorPos(x, y) ' Move the mouse to x,y
        Call mouse_event(MOUSEEVENTF_LEFTDOWN, 0&, 0&, 0&, 0&) 'Simulate Clic Down
        Call mouse_event(MOUSEEVENTF_LEFTUP, 0&, 0&, 0&, 0&)   'Simulate Clic Up
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