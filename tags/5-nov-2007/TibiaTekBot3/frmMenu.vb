Public Class frmMenu

    Public MenuText As New Dictionary(Of Integer, String)
    Public MenuCaptions As New Dictionary(Of Integer, String)
    Dim Loaded As Boolean = False
    Dim LastTextMenuIndex As Integer = 0
    Dim Busy As Boolean = False

    Private Sub frmMenu_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Win32API.SetForegroundWindow(Core.Tibia.GetWindowHandle)
    End Sub

    Private Sub frmMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Win32API.SetForegroundWindow(Core.Tibia.GetWindowHandle)
    End Sub

    Private Sub TextLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Win32API.SetForegroundWindow(Core.Tibia.GetWindowHandle)
    End Sub

    Private Sub frmMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Main Menus
        MenuCaptions.Add(0, "Main Menu")
        MenuText.Add(0, "1. General Tools" & vbCrLf & _
            "2. AFK Tools" & vbCrLf & _
            "3. Healing Tools" & vbCrLf & _
            "4. Info Tools" & vbCrLf & _
            "5  Training Tools" & vbCrLf & _
            "6. Fun Tools" & vbCrLf & _
            "7. Miscellaneous" & vbCrLf & _
            "8. Hide" & vbCrLf & _
            "9. Bot State" & vbCrLf & _
            "0. Exit Menu.")

        MenuCaptions.Add(1, "General Tools")
        MenuText.Add(1, "1. Configuration Manager" & vbCrLf & _
            "2. Auto Looter" & vbCrLf & _
            "3. Auto Stacker" & vbCrLf & _
            "4. Light Effects" & vbCrLf & _
            "5  Ammunition Restacker" & vbCrLf & _
            "6. Commands List" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(2, "AFK Tools 1")
        MenuText.Add(2, "1. Alarms" & vbCrLf & _
            "2. Spell Caster" & vbCrLf & _
            "3. Auto Eater" & vbCrLf & _
            "4. Runemaker" & vbCrLf & _
            "5  Auto Fisher" & vbCrLf & _
            "6. Trade Channel Advertiser" & vbCrLf & _
            "7. Trade Channel Watcher" & vbCrLf & _
            "8. Cavebot" & vbCrLf & _
            "9. More" & vbCrLf & _
            "0. Back.")
        MenuCaptions.Add(3, "AFK Tools 2")
        MenuText.Add(3, "1. Stats Uploader" & vbCrLf & _
            "2. FPS Changer" & vbCrLf & _
            "3. Auto Log" & vbCrLf & _
            "4. Auto Connect" & vbCrLf & _
            "5  Auto Backpack Opener" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(4, "Healing Tools")
        MenuText.Add(4, "1. Auto Healer" & vbCrLf & _
            "2. Auto UHer" & vbCrLf & _
            "3. Auto Heal Friend" & vbCrLf & _
            "4. Auto Heal Party" & vbCrLf & _
            "5. Mana Fluid Drinker" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(5, "Info Tools 1")
        MenuText.Add(5, "1. Exprience Checker" & vbCrLf & _
            "2. Character Information Lookup" & vbCrLf & _
            "3. Guild Members Lookup" & vbCrLf & _
            "4. Floor Explorer" & vbCrLf & _
            "5. Name Spy" & vbCrLf & _
            "6. Open File/Websites" & vbCrLf & _
            "7. Send Location" & vbCrLf & _
            "8. Get Item IDs" & vbCrLf & _
            "9. More" & vbCrLf & _
            "0. Back.")
        MenuCaptions.Add(6, "Info Tools 2")
        MenuText.Add(6, "1. Training Information (Skills/Level/Etc)" & vbCrLf & _
            "2. Floor Spy" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(7, "Training Tools")
        MenuText.Add(7, "1. Auto Attacker" & vbCrLf & _
            "2. Auto Trainer" & vbCrLf & _
            "3. Auto Pickup" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(8, "Fun Tools")
        MenuText.Add(8, "1. Fake Title" & vbCrLf & _
            "2. Chameleon" & vbCrLf & _
            "3. Rainbow Outfit" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(9, "Miscelanneous")
        MenuText.Add(9, "1. WASD" & vbCrLf & _
            "2. Mage Bomb/Combo" & vbCrLf & _
            "3. Screenshooter" & vbCrLf & _
            "4. Constant Editor" & vbCrLf & _
            "5. Feedback" & vbCrLf & _
            "6. Reload Configuration Files" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(10, "Hide")
        MenuText.Add(10, "1. Tibia Client (Hide/Show)" & vbCrLf & _
            "2. TibiaTek Bot Window" & vbCrLf & _
            "0. Back.")

        'Sub Menus

        MenuCaptions.Add(11, "Configuration Manager")
        MenuText.Add(11, "1. Load" & vbCrLf & _
            "2. Edit" & vbCrLf & _
            "3. Clear" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(12, "Auto Looter")
        MenuText.Add(12, "1. On" & vbCrLf & _
            "2. Edit" & vbCrLf & _
            "3. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(13, "Auto Stacker")
        MenuText.Add(13, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(14, "Light Effects")
        MenuText.Add(14, "1. On (Full Light)" & vbCrLf & _
            "2. Torch" & vbCrLf & _
            "3. Great Torch" & vbCrLf & _
            "4. Ultimate Torch" & vbCrLf & _
            "5. Utevo Lux" & vbCrLf & _
            "6. Utevo Gran Lux" & vbCrLf & _
            "7. Utevo Vis Lux" & vbCrLf & _
            "8. Light Wand" & vbCrLf & _
            "9. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(15, "Ammunition Restacker")
        MenuText.Add(15, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(16, "Spell Caster")
        MenuText.Add(16, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(17, "Auto Eater")
        MenuText.Add(17, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(18, "Rune Maker")
        MenuText.Add(18, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(19, "Auto Fisher")
        MenuText.Add(19, "1. On" & vbCrLf & _
            "2. Turbo" & vbCrLf & _
            "3. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(20, "Trade Channel Advertiser")
        MenuText.Add(20, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(21, "Trade Channel Watcher")
        MenuText.Add(21, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(22, "Cavebot")
        MenuText.Add(22, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(23, "Stats Uploader")
        MenuText.Add(23, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(24, "FPS Changer")
        MenuText.Add(24, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(25, "Auto Log")
        MenuText.Add(25, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(26, "Auto Connect")
        MenuText.Add(26, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(27, "Auto Healer")
        MenuText.Add(27, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(28, "Auto UHer")
        MenuText.Add(28, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(29, "Auto Heal Friend")
        MenuText.Add(29, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(30, "Auto Heal Party")
        MenuText.Add(30, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(31, "Mana Fluid Drinker")
        MenuText.Add(31, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(32, "Exprience Checker")
        MenuText.Add(32, "1. On" & vbCrLf & _
            "2. Creatures" & vbCrLf & _
            "3. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(33, "Creatures")
        MenuText.Add(33, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(34, "Floor Explorer")
        MenuText.Add(34, "1. Above" & vbCrLf & _
            "2. Around" & vbCrLf & _
            "3. Below" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(35, "Name Spy")
        MenuText.Add(35, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(36, "Open File/Websites")
        MenuText.Add(36, "1. File" & vbCrLf & _
            "-----------------" & vbCrLf & _
            "2. Tibiawiki" & vbCrLf & _
            "3. Tibia.com Character Pages" & vbCrLf & _
            "4. Tibia.com Guild Pages" & vbCrLf & _
            "5. Erig.net Highscore Pages" & vbCrLf & _
            "6. Google" & vbCrLf & _
            "7. Mytibia.com" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(37, "Auto Attacker")
        MenuText.Add(37, "1. On" & vbCrLf & _
            "2. Automatic" & vbCrLf & _
            "3. Stance" & vbCrLf & _
            "4. Fighting" & vbCrLf & _
            "5. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(38, "Stance")
        MenuText.Add(38, "1. Stand" & vbCrLf & _
            "2. Follow" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(39, "Fighting")
        MenuText.Add(39, "1. Offensive" & vbCrLf & _
            "2. Balanced" & vbCrLf & _
            "3. Defensive" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(40, "Auto Trainer")
        MenuText.Add(40, "1. On" & vbCrLf & _
            "2. Add" & vbCrLf & _
            "3. Remove" & vbCrLf & _
            "4. Clear" & vbCrLf & _
            "5. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(41, "Auto Pickup")
        MenuText.Add(41, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(42, "Fake Title")
        MenuText.Add(42, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(43, "Chameleon")
        MenuText.Add(43, "1. Change Outfit" & vbCrLf & _
            "2. Copy Outfit" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(44, "Rainbow Outfit")
        MenuText.Add(44, "1. On" & vbCrLf & _
            "2. Fast" & vbCrLf & _
            "3. Slow" & vbCrLf & _
            "4. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(45, "WASD")
        MenuText.Add(45, "1. On" & vbCrLf & _
            "2. Off" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(46, "Reload Configuration Files")
        MenuText.Add(46, "1. Spells" & vbCrLf & _
            "2. Outfits" & vbCrLf & _
            "3. Items" & vbCrLf & _
            "4. Constants" & vbCrLf & _
            "5. Tibia.dat" & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(47, "Bot State")
        MenuText.Add(47, "1. Start" & vbCrLf & _
            "2. Pause" & vbCrLf & _
            "3. Stop" & vbCrLf & _
            "0. Back.")

        Core.TextMenuIndex = 0
        Loaded = True
        'Me.Invalidate()
        'CaptionLabel.Text = MenuCaptions(0)
        'TextLabel.Text = MenuText(0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not Loaded Then Exit Sub
        If Busy Then Exit Sub
        Busy = True
        'If Timer1.Interval < 200 Then Timer1.Interval = 200
        PictureBox1.SuspendLayout()
        Dim G As Graphics = Me.PictureBox1.CreateGraphics
        If Not LastTextMenuIndex = Core.TextMenuIndex Then
            LastTextMenuIndex = Core.TextMenuIndex
            'G.Clear(Color.Fuchsia)
            'G.DrawImage(My.Resources.menu, 0, 0)
            'Exit Sub
        End If
        Dim brush As New SolidBrush(Color.FromArgb(255, 254, 127, 127))
        G.DrawString(MenuCaptions(Core.TextMenuIndex), Me.Font, brush, 5, 5)
        G.DrawString(MenuText(Core.TextMenuIndex), Me.Font, Brushes.White, 5, 20)
        G.Flush()
        PictureBox1.ResumeLayout()
        Busy = False
    End Sub

    Private Sub frmMenu_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Timer1.Enabled = True
    End Sub
End Class