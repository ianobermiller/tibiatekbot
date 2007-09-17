Imports TibiaTekBot3.Win32API

Public Module TibiaModule

    Public Class TibiaClass
        Private ClientProcess As Process
        Private ClientVersion As String

        Public Memory As New MemoryClass

        Public Sub New()
        End Sub

        Public Sub SetClientProcessByID(ByVal ProcessID As Integer)
            ClientProcess = System.Diagnostics.Process.GetProcessById(ProcessID)
            ClientProcess.Refresh()
        End Sub

        Public Function GetProcessHandle() As Integer
            Return ClientProcess.Handle.ToInt32
        End Function

        Public Function SendMessage(ByVal MessageID As WM, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
            Return Win32API.SendMessage(ClientProcess.MainWindowHandle, MessageID, wParam, lParam)
        End Function

        Public Function Process() As Process
            Return ClientProcess
        End Function

        Public Property Title() As String
            Get
                Return ClientProcess.MainWindowTitle
            End Get
            Set(ByVal Title As String)
                If Core.WASDActive Then
                    If Core.WASDSayModeActive Then
                        Title = BotShortName & " [WASD Say Mode] - " & Title
                    Else
                        Title = BotShortName & " [WASD Mode] - " & Title
                    End If
                Else
                    Title = BotName & " - " & Title
                End If
                SetWindowText(ClientProcess.MainWindowHandle.ToInt32, Title)
            End Set
        End Property

        Public Function Ping(Optional ByVal Timeout As Integer = 2000) As Boolean
            Dim Result As Integer = 0
            SendMessageTimeout(ClientProcess.MainWindowHandle, WM.Ping, 0, 0, SMTO.AbortIfHung Or SMTO.NoTimeoutIfNotHung, Timeout, Result)
            Return Result = 1
        End Function

        Public Sub Close()
            ClientProcess.Kill()
        End Sub

        Public Function GetProcessID() As Integer
            Return ClientProcess.Id
        End Function

        Public Function GetWindowHandle() As Integer
            Return ClientProcess.MainWindowHandle.ToInt32
        End Function

        Public Sub Restore()
            Win32API.ShowWindow(ClientProcess.MainWindowHandle, Win32API.ShowState.SW_RESTORE)
        End Sub

        Public Sub Minimize()
            Win32API.ShowWindow(ClientProcess.MainWindowHandle, Win32API.ShowState.SW_MINIMIZE)
        End Sub

        Public Sub Hide()
            Win32API.ShowWindow(ClientProcess.MainWindowHandle, Win32API.ShowState.SW_HIDE)
        End Sub

        Public Sub Show()
            Win32API.ShowWindow(ClientProcess.MainWindowHandle, Win32API.ShowState.SW_SHOW)
        End Sub

        Public Sub ShowMenu()
            SendMessage(WM.Menu, 1, 0)
        End Sub

        Public Sub HideMenu()
            SendMessage(WM.Menu, 0, 0)
        End Sub

        Public Property Version() As String
            Get
                Return ClientVersion
            End Get
            Set(ByVal Version As String)
                Me.ClientVersion = Version
            End Set
        End Property

        Public Sub SetWasd(ByVal Active As Boolean, Optional ByVal SayModeActive As Boolean = False)
            Dim ActiveInt As Integer = 0
            Dim SMInt As Integer = 0
            If Active Then
                ActiveInt = 1
            Else
                ActiveInt = 0
            End If
            If SayModeActive Then
                SMInt = 1
            Else
                SMInt = 0
            End If
            SendMessage(WM.WASD, ActiveInt, SMInt)
            Core.WASDActive = Active
            Core.WASDSayModeActive = SayModeActive
        End Sub

        Public Sub FlashTaskbar()
            Dim FWI As New FlashWInfo(ClientProcess.MainWindowHandle, FlashWFlags.FLASHW_TIMERNOFG Or FlashWFlags.FLASHW_TRAY Or FlashWFlags.FLASHW_CAPTION, 0, 0)
            FlashWindowEx(FWI)
        End Sub


    End Class

End Module
