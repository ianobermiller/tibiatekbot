Imports System, Scripting, Scripting.Events, System.Windows.Forms

Public Class Script
    Implements IScript
    Dim WithEvents Kernel As IKernel
    Dim WithEvents Client As ITibia

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Me.Client = Kernel.Client
    End Sub

    Public Sub ConditionsChanged(ByVal e As CharacterConditionsChangedEventArgs) Handles Client.CharacterConditionsChanged
        Kernel.ConsoleWrite("Character Conditions: " & e.Conditions.ToString())
    End Sub

    Public Sub Pause() Implements IScript.Pause
        MessageBox.Show("Not supported.")
    End Sub

    Public Sub [Resume]() Implements IScript.Resume
        MessageBox.Show("Not supported.")
    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub

End Class
