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

    Private Sub SendCommandcmd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'CommandParser(CommandLinetxt.Text)
        'CommandLinetxt.Clear()
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

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light on")
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light torch")
    End Sub

    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light great torch")
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light ultimate torch")
    End Sub

    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light utevo lux")
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light utevo gran lux")
    End Sub

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light utevo vis lux")
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light light wand")
    End Sub

    Private Sub ToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("light off")
    End Sub

    Private Sub ToolStripMenuItem80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("exp on")
    End Sub

    Private Sub ToolStripMenuItem84_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("exp off")
    End Sub

    Private Sub ToolStripMenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim res As String = InputBox("Enter the delay in seconds to eat. Example: 30.", "Auto Eater Delay", "30")
        If Not String.IsNullOrEmpty(res) Then CommandParser("eat " & res)
    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("eat off")
    End Sub

    Private Sub ToolStripMenuItem41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        If Not String.IsNullOrEmpty(mincap) Then CommandParser("fisher " & mincap)
    End Sub

    Private Sub ToolStripMenuItem43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("fisher off")
    End Sub

    Private Sub ToolStripMenuItem42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mincap As String = InputBox("Enter the minimum capacity to fish. Example: 6.", "Minimum Capacity")
        If Not String.IsNullOrEmpty(mincap) Then CommandParser("fisher " & mincap & " turbo")
    End Sub

    Private Sub ToolStripMenuItem139_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("wasd on")
    End Sub

    Private Sub ToolStripMenuItem140_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("wasd off")
    End Sub

    Private Sub ToolStripMenuItem45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim advertisement As String = InputBox("Enter your advertisement. Example: sell 10 bp of uh ~ thais.", "Advertisement")
        If Not String.IsNullOrEmpty(advertisement) Then CommandParser("advertise " & advertisement)
    End Sub

    Private Sub ToolStripMenuItem38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mp As String = InputBox("Enter the minimum mana points to conjure the spell words. Example: 400.", "Minimum Mana Points")
        If String.IsNullOrEmpty(mp) Then Exit Sub
        Dim sp As String = InputBox("Enter the minimum soul points to conjure the spell words. Example: 3.", "Minimum Soul Points")
        If String.IsNullOrEmpty(sp) Then Exit Sub
        Dim sw As String = InputBox("Enter the spell words or the spell name. Example: great fireball.", "Spell Words/Spell Name")
        If String.IsNullOrEmpty(sw) Then Exit Sub
        CommandParser("runemaker " & mp & " " & sp & " """ & sw & """")
    End Sub

    Private Sub ToolStripMenuItem39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("runemaker off")
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim number As String = InputBox("Enter the minimum mana points to cast the spell. Example: 100.", "Minimum Mana Points")
        If String.IsNullOrEmpty(number) Then Exit Sub
        Dim spell As String = InputBox("Enter the spell words. Example: eXuRa """"HeAl pLx.", "Spell Words")
        If String.IsNullOrEmpty(spell) Then Exit Sub
        CommandParser("spell " & number & " """ & spell)
    End Sub

    Private Sub ToolStripMenuItem33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("spell off")
    End Sub

    Private Sub ToolStripMenuItem92_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("namespy on")
    End Sub

    Private Sub ToolStripMenuItem93_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("namespy off")
    End Sub

    Private Sub ToolStripMenuItem60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("fpschanger on")
    End Sub

    Private Sub ToolStripMenuItem61_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("fpschanger off")
    End Sub

    Private Sub ToolStripMenuItem57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("statsuploader on")
    End Sub

    Private Sub ToolStripMenuItem58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub ToolStripMenuItem48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim reg As String = InputBox("Enter the regular expression pattern to match. Example: bps? of uh.", "Regular Expression Pattern")
        If Not String.IsNullOrEmpty(reg) Then CommandParser("watch " & reg)
    End Sub

    Private Sub ToolStripMenuItem49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommandParser("watch off")
    End Sub

    Private Sub FtsOnBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FtsOnBox.SelectedIndexChanged
        Select Case FtsOnBox.SelectedItem
            Case "Spellcaster"
                ShowFeature(frmSubForms.Spellcaster)
            Case "Auto Eater"
                ShowFeature(frmSubForms.AutoEater)
            Case "Runemaker"
                ShowFeature(frmSubForms.Runemaker)
            Case "Auto Fisher"
                ShowFeature(frmSubForms.AutoFisher)
            Case "Namespy"
                ShowFeature(frmSubForms.Namespy)
            Case "Auto Healer"
                ShowFeature(frmSubForms.Autohealer)
            Case "WASD"
                ShowFeature(frmSubForms.WASD)
            Case "Exp Checker"
                ShowFeature(frmSubForms.ExpChecker)
            Case "Light Effects"
                ShowFeature(frmSubForms.LightEffects)
            Case "Advertiser"
                ShowFeature(frmSubForms.Advertiser)
            Case "Watcher"
                ShowFeature(frmSubForms.Watcher)
            Case "FPS Changer"
                ShowFeature(frmSubForms.FPSChanger)
            Case "Stats Uploader"
                ShowFeature(frmSubForms.StatsUploader)
            Case "Amulet Changer"
                ShowFeature(frmSubForms.Changer)
            Case "Auto UHer"
                ShowFeature(frmSubForms.AutoUHer)
            Case "Heal Friend"
                ShowFeature(frmSubForms.HealFriend)
            Case "Heal Party"
                ShowFeature(frmSubForms.HealParty)
            Case "Auto Drinker"
                ShowFeature(frmSubForms.AutoDrinker)
            Case "Auto Looter"
                ShowFeature(frmSubForms.AutoLooter)
            Case "Auto Stacker"
                ShowFeature(frmSubForms.AutoStacker)
            Case "Ammo Restacker"
                ShowFeature(frmSubForms.AmmoRestacker)
            Case Else
                FeaturePanel.Controls.Clear()
        End Select
    End Sub

    Private Sub ToolStripMenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem31.Click
        AddFeature("Spellcaster")
    End Sub

    Private Sub ToolStripMenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem34.Click
        AddFeature("Auto Eater")
    End Sub

    Private Sub ToolStripMenuItem37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem37.Click
        AddFeature("Runemaker")
    End Sub

    Private Sub ToolStripMenuItem40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem40.Click
        AddFeature("Auto Fisher")
    End Sub

    Private Sub ToolStripMenuItem91_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem91.Click
        AddFeature("Namespy")
    End Sub

    Private Sub ToolStripMenuItem63_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem63.Click
        AddFeature("Auto Healer")
    End Sub

    Private Sub ToolStripMenuItem138_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem138.Click
        AddFeature("WASD")
    End Sub

    Private Sub ToolStripMenuItem79_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem79.Click
        AddFeature("Exp Checker")
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        AddFeature("Light Effects")
    End Sub

    Private Sub ToolStripMenuItem44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem44.Click
        AddFeature("Advertiser")
    End Sub

    Private Sub ToolStripMenuItem47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem47.Click
        AddFeature("Watcher")
    End Sub

    Private Sub ToolStripMenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem28.Click
        AddFeature("FPS Changer")
    End Sub

    Private Sub ToolStripMenuItem56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem56.Click
        AddFeature("Stats Uploader")
    End Sub

    Private Sub TestToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem1.Click
        InjectIncomingPacketInterception()
    End Sub

    Private Sub AMuletNecklaceChangerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AMuletNecklaceChangerToolStripMenuItem.Click
        AddFeature("Amulet Changer")
    End Sub

    Private Sub ToolStripMenuItem66_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem66.Click
        AddFeature("Auto UHer")
    End Sub

    Private Sub ToolStripMenuItem69_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem69.Click
        AddFeature("Heal Friend")
    End Sub

    Private Sub ToolStripMenuItem72_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem72.Click
        AddFeature("Heal Party")
    End Sub

    Private Sub ToolStripMenuItem75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem75.Click
        AddFeature("Auto Drinker")
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        AddFeature("Auto Looter")
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        AddFeature("Auto Stacker")
    End Sub

    Private Sub ToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem25.Click
        AddFeature("Ammo Restacker")
    End Sub
End Class
