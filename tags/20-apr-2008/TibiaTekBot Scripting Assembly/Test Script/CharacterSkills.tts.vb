Imports System, Scripting

Public Class CharacterSkills
    Implements IScript

    Dim WithEvents Kernel As IKernel

    Public Sub MyCommand(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Kernel.ConsoleWrite("Character Skills:")
        Kernel.ConsoleWrite("Level: " & Kernel.Client.CharacterLevel)
        Kernel.ConsoleWrite("MLevel: " & Kernel.Client.CharacterMagicLevel)
        Kernel.ConsoleWrite("Club Fighting: " & Kernel.Client.CharacterSkill(ITibia.Skills.ClubFighting))
        Kernel.ConsoleWrite("Exp Amount: " & Kernel.Client.CharacterExperience)
    End Sub

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("skills", AddressOf MyCommand)
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
