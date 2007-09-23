Imports System.Runtime.InteropServices, System.io, System.text

Public Class frmMain
    'Dim SplashScreen As New frmMain
    'Public Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Integer, ByRef procid As Integer) As Integer
    'Public Declare Auto Function FindWindow Lib "user32" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    'Dim P As Process = Process.GetProcessById(Core.ProcessID)
    'P.Refresh()
    'SendMessage(P.MainWindowHandle, WM.Send, 0, 0)
    '  Dim bytBuffer() As Byte = TestPacket()
    'Core.SendPacketToClient(bytBuffer)
    'Dim AfxHwnd As IntPtr
    'AfxHwnd = Win32API.FindWindow("Afx:00400000:0", vbNullString)
    'MsgBox(AfxHwnd)
    'End Sub

    Protected Overrides Sub WndProc(ByRef M As Message)
        If M.HWnd = Me.Handle AndAlso M.Msg > WM.Begin AndAlso M.Msg < WM.End Then
            MyBase.WndProc(M)
            Core.WndProc(M)
        Else
            MyBase.WndProc(M)
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Core.TTBHandle = Me.Handle.ToInt32
        frmSplashScreen.ShowDialog()
        If Core.SelectClientForm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then End
        Core.AfterInjection() 'start timers... etc
        NotifyIcon1.Visible = True
        Dim I As Integer = 0
        'While Core.InjectionState <> InjectionState.Injected
        'If I = 3 Then End
        'If Core.InjectionState = InjectionState.Failed Then
        'Core.Tibia.Ping(10)
        'End If
        'System.Threading.Thread.Sleep(500)
        'I += 1
        'End While

        'Me.Visible = False
        'Core.Tibia.ShowMenu()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MessageBox.Show("Are you sure that you want to quit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Core.Uninject() 'after uninject, the bot closes itself
        End If
    End Sub

    Private Sub WebsiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        System.Diagnostics.Process.Start(BotWebsite)
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem.Click
        Core.Tibia.ShowMenu()
        Me.Hide()
    End Sub

    Private Sub TibiaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Core.TibiaWindowState <> Constants.WindowState.Hidden Then
            Core.TibiaWindowState = Constants.WindowState.Hidden
            Core.Tibia.Hide()
        Else
            Core.TibiaWindowState = Constants.WindowState.Active
            Core.Tibia.Show()
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        If MessageBox.Show("Are you sure that you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            If Core.TibiaWindowState = Constants.WindowState.Hidden Then
                Core.Tibia.Show()
            End If
            Core.Uninject()
        End If
    End Sub

    Private Sub TibiaTekBotWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
    End Sub

    Private Sub NotifyIcon1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotifyIcon1.Click
        Me.Show()
        Core.Tibia.HideMenu()
    End Sub

    Private Sub SendCommandcmd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendCommandcmd.Click
        CommandParser(CommandLinetxt.Text)
        CommandLinetxt.Clear()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub frmMain_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.Hide()
        Core.Tibia.ShowMenu()
    End Sub

    Private Sub ToolStripMenuItem154_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem154.Click
        System.Diagnostics.Process.Start(BotForum)
    End Sub

    Private Sub ToolStripMenuItem153_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem153.Click
        System.Diagnostics.Process.Start(BotWebsite)
    End Sub

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click
        CommandParser("light on")
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        CommandParser("light torch")
    End Sub

    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem18.Click
        CommandParser("light great torch")
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        CommandParser("light ultimate torch")
    End Sub

    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem20.Click
        CommandParser("light utevo lux")
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        CommandParser("light utevo gran lux")
    End Sub

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem22.Click
        CommandParser("light utevo vis lux")
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        CommandParser("light light wand")
    End Sub

    Private Sub ToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem24.Click
        CommandParser("light off")
    End Sub

    Private Sub ToolStripMenuItem80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem80.Click
        CommandParser("exp on")
    End Sub

    Private Sub ToolStripMenuItem84_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem84.Click
        CommandParser("exp off")
    End Sub

    Private Sub ToolStripMenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem35.Click
        Dim res As String = InputBox("Enter the delay in seconds to eat. Example: 30.", "Auto Eater Delay", "30")
        If Not String.IsNullOrEmpty(res) Then CommandParser("eat " & res)
    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem36.Click
        CommandParser("eat off")
    End Sub

    Private Sub ToolStripMenuItem41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem41.Click
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        If Not String.IsNullOrEmpty(mincap) Then CommandParser("fisher " & mincap)
    End Sub

    Private Sub ToolStripMenuItem43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem43.Click
        CommandParser("fisher off")
    End Sub

    Private Sub ToolStripMenuItem42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem42.Click
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        If Not String.IsNullOrEmpty(mincap) Then CommandParser("fisher " & mincap & " turbo")
    End Sub

    Private Sub ToolStripMenuItem139_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem139.Click
        CommandParser("wasd on")
    End Sub

    Private Sub ToolStripMenuItem140_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem140.Click
        CommandParser("wasd off")
    End Sub

    Private Sub ToolStripMenuItem45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem45.Click
        Dim advertisement As String = InputBox("Enter your advertisement. Example: sell 10 bp of uh ~ thais.", "Advertisement")
        If Not String.IsNullOrEmpty(advertisement) Then CommandParser("advertise " & advertisement)
    End Sub

    Private Sub ToolStripMenuItem38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem38.Click
        Dim mp As String = InputBox("Enter the minimum mana points to conjure the spell words. Example: 400.", "Minimum Mana Points")
        If String.IsNullOrEmpty(mp) Then Exit Sub
        Dim sp As String = InputBox("Enter the minimum soul points to conjure the spell words. Example: 3.", "Minimum Soul Points")
        If String.IsNullOrEmpty(sp) Then Exit Sub
        Dim sw As String = InputBox("Enter the spell words or the spell name. Example: great fireball.", "Spell Words/Spell Name")
        If String.IsNullOrEmpty(sw) Then Exit Sub
        CommandParser("runemaker " & mp & " " & sp & " """ & sw & """")
    End Sub

    Private Sub ToolStripMenuItem39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem39.Click
        CommandParser("runemaker off")
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem32.Click
        Dim number As String = InputBox("Enter the minimum mana points to cast the spell. Example: 100.", "Minimum Mana Points")
        If String.IsNullOrEmpty(number) Then Exit Sub
        Dim spell As String = InputBox("Enter the spell words. Example: eXuRa """"HeAl pLx.", "Spell Words")
        If String.IsNullOrEmpty(spell) Then Exit Sub
        CommandParser("spell " & number & " """ & spell)
    End Sub

    Private Sub ToolStripMenuItem33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem33.Click
        CommandParser("spell off")
    End Sub

    Private Sub ToolStripMenuItem92_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem92.Click
        CommandParser("namespy on")
    End Sub

    Private Sub ToolStripMenuItem93_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem93.Click
        CommandParser("namespy off")
    End Sub

    Private Sub ToolStripMenuItem60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem60.Click
        CommandParser("fpschanger on")
    End Sub

    Private Sub ToolStripMenuItem61_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem61.Click
        CommandParser("fpschanger off")
    End Sub

    Private Sub ToolStripMenuItem57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem57.Click
        CommandParser("statsuploader on")
    End Sub

    Private Sub ToolStripMenuItem58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem58.Click
        CommandParser("statsuploader off")
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.Click
        CommandParser("start")
    End Sub

    Private Sub PauseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseToolStripMenuItem.Click
        CommandParser("pause")
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        CommandParser("stop")
    End Sub

    Private Sub ToolStripMenuItem150_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem150.Click
        If Core.TibiaWindowState = Constants.WindowState.Hidden Then
            Core.Tibia.Show()
            Core.TibiaWindowState = Constants.WindowState.Active
        Else
            Core.Tibia.Hide()
            Core.TibiaWindowState = Constants.WindowState.Hidden
        End If
    End Sub

    Private Sub ToolStripMenuItem48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem48.Click
        Dim reg As String = InputBox("Enter the regular expression pattern to match. Example: bps? of uh.", "Regular Expression Pattern")
        If Not String.IsNullOrEmpty(reg) Then CommandParser("watch " & reg)
    End Sub

    Private Sub ToolStripMenuItem49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem49.Click
        CommandParser("watch off")
    End Sub


    Private Sub TestToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        Core.ScreenWrite("Eka Testi", ShowTextColors.Yellow)
    End Sub
End Class
