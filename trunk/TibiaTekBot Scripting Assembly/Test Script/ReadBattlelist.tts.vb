'Author: OsQu
'Description: Simple script that adds new command called &showcreatures and it just reads and displays the creatures on screen like
Imports System, Scripting

Public Class Script
    Implements IScript

    Dim WithEvents Kernel As IKernel

    Public Sub ShowCreatures(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Dim BL As IBattlelist = Kernel.NewBattlelist
        BL.Reset()
        Do
            If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                Kernel.ConsoleWrite(BL.GetName & "(" & BL.GetHPPercentage & "%)")
            End If
        Loop While BL.NextEntity()
    End Sub

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("showcreatures", AddressOf ShowCreatures)
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        ' Paused
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        ' Resume
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Kernel.CommandParser.Remove("showcreatures")
    End Sub

End Class