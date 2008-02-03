Imports System, Scripting, System.Threading

Public Class Script
    Implements IScript

    Dim WithEvents Kernel As IKernel

    Public Sub MyCommand(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Kernel.ConsoleWrite("Hello World!")
        Kernel.CommandParser.Invoke("animation 20")
    End Sub

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("mycommand", AddressOf MyCommand)
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        ' Paused
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        ' Resume
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
    End Sub

End Class
