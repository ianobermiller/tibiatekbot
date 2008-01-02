Imports System, System.Windows.Forms, Scripting, Microsoft.VisualBasic

Public Class Script
    Implements IScript

    Dim WithEvents Kernel As IKernel
	Dim WithEvents Timer As ThreadTimer

    Public Sub Timer_OnExecute() Handles Timer.OnExecute
        Beep()
    End Sub

	Public Sub New()
		Me.Timer = New ThreadTimer(3000)
    End Sub
	
	Public Sub Initialize(Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
    End Sub

    Public Sub Kernel_OnConnected() Handles Kernel.OnConnected
        Me.Timer.StartTimer()
    End Sub

    Public Sub Kernel_OnDisconnected() Handles Kernel.OnDisconnected
        Me.Timer.StopTimer()
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        Me.Timer.StopTimer()
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        If Kernel.Client.IsConnected() Then
            Me.Timer.StartTimer()
            Kernel.ConsoleWrite("Hello World! The Script is Started again!")
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Timer.Dispose()
    End Sub

End Class
