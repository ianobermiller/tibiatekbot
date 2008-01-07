Imports System, Scripting

Public Class Script
    Implements IScript

    Dim WithEvents Kernel As IKernel
    Dim WithEvents Timer As ThreadTimer

    Public Sub New()
        Timer = New ThreadTimer(5000)
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
        If Kernel.Client.IsConnected Then
            Kernel.ConsoleWrite("Tick!")
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Timer.Dispose()
    End Sub

End Class
