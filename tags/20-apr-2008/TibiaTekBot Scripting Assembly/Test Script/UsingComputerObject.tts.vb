Imports System, Microsoft.VisualBasic, System.Windows.Forms, Scripting

Public Class Script
    Implements IScript

    Dim Kernel As IKernel

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("computer", AddressOf UseComputerObject)
    End Sub

    Public Sub UseComputerObject(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Kernel.ConsoleWrite("Clock:" & Kernel.Computer.Clock.LocalTime.ToLongDateString)
        Kernel.ConsoleWrite("OS: " & Kernel.Computer.Info.OSFullName & ". Version: " & Kernel.Computer.Info.OSVersion)
        Kernel.ConsoleWrite("CapsLock: " & Kernel.Computer.Keyboard.CapsLock & ". NumLock: " & Kernel.Computer.Keyboard.NumLock)
        Kernel.ConsoleWrite("Computer Name: " & Kernel.Computer.Name)
        Kernel.ConsoleWrite("Current Directory: " & Kernel.Computer.FileSystem.CurrentDirectory)
        Kernel.ConsoleWrite("Memory: " & Kernel.Computer.Info.AvailablePhysicalMemory & "/" & Kernel.Computer.Info.TotalPhysicalMemory)
    End Sub

    Public Sub Pause() Implements IScript.Pause
        MessageBox.Show("This script does not support pausing.")
    End Sub

    Public Sub [Resume]() Implements IScript.Resume
        MessageBox.Show("This script does not support resuming.")
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

End Class
