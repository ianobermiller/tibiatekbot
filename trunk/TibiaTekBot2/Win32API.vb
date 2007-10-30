Imports System.Runtime.InteropServices

Public Class Win32API

    Public Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Int32, ByVal lpBaseAddress As Int32, ByRef lpBuffer As Int32, ByVal nSize As Int32, ByVal lpNumberOfBytesWritten As Int32) As Long
    Public Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Int32, ByVal lpBaseAddress As Int32, ByVal lpBuffer() As Byte, ByVal nSize As Int16, ByVal lpNumberOfBytesWritten As Int32) As Long
    Public Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String) As Int32
    Public Declare Function GetWindowPlacement Lib "user32" (ByVal hWnd As IntPtr, ByRef windowPlacement As WindowPlacement) As Boolean
    Public Declare Function GetForegroundWindow Lib "user32" () As Integer
    Public Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Integer, ByRef procid As Integer) As Integer
    Public Declare Function ShowWindow Lib "user32" (ByVal handle As IntPtr, ByVal nCmd As Int32) As Boolean
    Public Declare Function FlashWindowEx Lib "user32" (ByRef fwi As FlashWInfo) As Int32
    Public Declare Function VirtualProtectEx Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As UIntPtr, ByVal flNewProtect As UInteger, ByRef lpflOldProtect As UInteger) As Boolean

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure FlashWInfo
        Dim cbSize As UInt32
        Dim hWnd As IntPtr
        Dim dwFlags As FlashWFlags
        Dim uCount As UInt32
        Dim dwTimeout As UInt32

        Sub New(ByVal Handle As IntPtr, ByVal Flags As FlashWFlags, ByVal Count As UInt32, ByVal Timeout As UInt32)
            hWnd = Handle
            cbSize = Convert.ToUInt32(Marshal.SizeOf(GetType(FlashWInfo)))
            dwFlags = Flags
            uCount = Count
            dwTimeout = Timeout
        End Sub
    End Structure

    Public Structure WindowPlacement
        Public Length As UInteger
        Public Flags As UInteger
        Public ShowCmd As ShowState
        Public MinPosition As Point
        Public MaxPosition As Point
        Public NormalPosition As Rectangle
    End Structure

    Public Enum ShowState As UInteger
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

    <Flags()> _
    Public Enum FlashWFlags As UInt32
        FLASHW_STOP = 0
        FLASHW_CAPTION = 1
        FLASHW_TRAY = 2
        FLASHW_TIMER = 4
        FLASHW_TIMERNOFG = 12
    End Enum
End Class
