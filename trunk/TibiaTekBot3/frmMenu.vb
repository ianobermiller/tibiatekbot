Public Class frmMenu

    Public MenuText As New Dictionary(Of Integer, String)
    Public MenuCaptions As New Dictionary(Of Integer, String)
    Dim Loaded As Boolean = False
    Dim LastTextMenuIndex As Integer = 0

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
        MenuCaptions.Add(0, "Main Menu")
        MenuText.Add(0, "1. General Tools" & vbCrLf & _
            "2. AFK(Tools)" & vbCrLf & _
            "3. Fun(Tools)" & vbCrLf & _
            "4. Something else" & vbCrLf & _
            "5  Healing(Tools)" & vbCrLf & _
            "6. Misc(Tools)" & vbCrLf & _
            "7. Nice(Menu)" & vbCrLf & _
            "8. Options" & vbCrLf & _
            "9. Menu(Mode)" & vbCrLf & _
            "0. Exit Menu.")

        MenuCaptions.Add(1, "General Tools")
        MenuText.Add(1, "1. Tool 1." & vbCrLf & _
            "2. Tool 2." & vbCrLf & _
            "3. Tool 3." & vbCrLf & _
            "4. Tool 4." & vbCrLf & _
            "5  Tool 5." & vbCrLf & _
            "6. Tool 6." & vbCrLf & _
            "7. Tool 7." & vbCrLf & _
            "8. Tool 8." & vbCrLf & _
            "9. Tool 9." & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(2, "AFK Tools")
        MenuText.Add(2, "1. Tool 1." & vbCrLf & _
            "2. Tool 2." & vbCrLf & _
            "3. Tool 3." & vbCrLf & _
            "4. Tool 4." & vbCrLf & _
            "5  Tool 5." & vbCrLf & _
            "6. Tool 6." & vbCrLf & _
            "7. Tool 7." & vbCrLf & _
            "8. Tool 8." & vbCrLf & _
            "9. Tool 9." & vbCrLf & _
            "0. Back.")

        MenuCaptions.Add(3, "Healing Tools")
        MenuText.Add(3, "1. Tool 1." & vbCrLf & _
            "2. Tool 2." & vbCrLf & _
            "3. Tool 3." & vbCrLf & _
            "4. Tool 4." & vbCrLf & _
            "5  Tool 5." & vbCrLf & _
            "6. Tool 6." & vbCrLf & _
            "7. Tool 7." & vbCrLf & _
            "8. Tool 8." & vbCrLf & _
            "9. Tool 9." & vbCrLf & _
            "0. Back.")

        Core.TextMenuIndex = 0
        Loaded = True
        'Me.Invalidate()
        'CaptionLabel.Text = MenuCaptions(0)
        'TextLabel.Text = MenuText(0)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not Loaded Then Exit Sub
        If Timer1.Interval < 500 Then Timer1.Interval = 500
        PictureBox4.SuspendLayout()
        If Not LastTextMenuIndex = Core.TextMenuIndex Then
            LastTextMenuIndex = Core.TextMenuIndex
            PictureBox4.BackgroundImage = My.Resources.menu_bg
            Timer1.Interval = 100
            Exit Sub
        End If
        Dim G As Graphics = Me.PictureBox4.CreateGraphics
        Dim brush As New SolidBrush(Color.FromArgb(255, 254, 127, 127))
        G.DrawString(MenuCaptions(Core.TextMenuIndex), Me.Font, brush, 5, 5)
        G.DrawString(MenuText(Core.TextMenuIndex), Me.Font, Brushes.White, 5, 20)
        G.Flush()
        PictureBox4.ResumeLayout()
    End Sub

    Private Sub frmMenu_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Timer1.Enabled = True
    End Sub
End Class