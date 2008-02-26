Imports System, Scripting
Imports System.Windows.Forms

Public Class NotifyIcon
    Implements IScript

    Dim WithEvents Kernel As IKernel
    Dim Activated As Boolean
    Dim WithEvents Timer As New ThreadTimer(2000)

    Public Sub Initialize(ByVal Kernel As IKernel) Implements IScript.Initialize
        Me.Kernel = Kernel
        Kernel.CommandParser.Add("notyfyicontext", AddressOf SetNotifyIcontext)
        Kernel.ConsoleWrite("usage: &notyfyicontext Text here")
        Kernel.NotifyIcon.ShowBalloonTip(3, "TibiaTek Bot", "Script loaded", ToolTipIcon.Info)
        Kernel.CommandParser.Add("notyfyicon", AddressOf N_icon)
        Kernel.ConsoleWrite("usage: &notyfyicon <on|off>")
    End Sub
    Public Sub SetNotifyIcontext(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Kernel.NotifyIcon.Text = Arguments(2).ToString
    End Sub

    Public Sub N_icon(ByVal Arguments As System.Text.RegularExpressions.GroupCollection)
        Select Case Arguments(2).Value.ToString.ToLower
            Case "off", 0
                Kernel.NotifyIcon.Visible = False
            Case "on", 1
                Kernel.NotifyIcon.Visible = True
            Case Else
                Kernel.ConsoleError("its ""&notyfyicon on"" or ""&notyfyicon off"" -_-")
        End Select
    End Sub

    Public Sub PauseScript() Implements IScript.Pause
        ' Paused
        Kernel.NotifyIcon.ShowBalloonTip(1, "TibiaTek Bot", "Paused", ToolTipIcon.Info)
    End Sub

    Public Sub ResumeScript() Implements IScript.Resume
        ' Resume
        Kernel.NotifyIcon.ShowBalloonTip(1, "TibiaTek Bot", "Resume", ToolTipIcon.Info)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Dispose all created objects
        Kernel.CommandParser.Remove("notyfyicontext")
        Kernel.CommandParser.Remove("notyfyicon")
    End Sub


End Class