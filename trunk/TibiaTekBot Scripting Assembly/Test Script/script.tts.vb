Imports System, System.Windows.Forms, Scripting

Public Class Script
	Implements IScript
	
	Dim Kernel As IKernel
	Dim WithEvents Timer As ThreadTimer

	Public Sub Timer_Tick() Handles Timer.OnExecute
		MessageBox.Show("TICK")
	End Sub

	Public Sub New()
		Me.Timer = New ThreadTimer(3000)
    End Sub
	
	Public Sub Initialize(Kernel As IKernel) Implements IScript.Initialize
		Me.Kernel = Kernel
		Me.Timer.StartTimer(1000)
	End Sub
	
	Public Sub Dispose() Implements IDisposable.Dispose
		Timer.Dispose()
	End Sub

End Class
