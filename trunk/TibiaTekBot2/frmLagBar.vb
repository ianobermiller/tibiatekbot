Public Class frmLagBar
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Private Sub frmLagBar_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Hide()
    End Sub

    Dim Socket As New Winsock
    Dim Time As Date
    Dim Elapsed As Double = 0
    'Dim MaxElapsed As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        SyncLock Me
            Try
                If Kernel.Client.IsConnected Then
                    If Socket.GetState <> Winsock.WinsockStates.Closed Then
                        Socket.Close()
                        While Socket.GetState <> Winsock.WinsockStates.Closed
                            Application.DoEvents()
                        End While
                    End If
                    Time = Date.Now
                    Socket.Connect(Kernel.Client.CharacterListCurrentEntry.WorldIP.ToString, Kernel.Client.CharacterListCurrentEntry.WorldPort)
                    While Socket.GetState <> Winsock.WinsockStates.Connected
                        Elapsed = (Date.Now - Time).TotalMilliseconds
                        If Elapsed > 5000 Then
                            Label2.Text = (Date.Now - Time).TotalMilliseconds & " ms"
                            Label2.ForeColor = Drawing.Color.DarkRed
                            PictureBox1.Size = New System.Drawing.Size(88, 9)
                            Exit Sub
                        End If
                        Application.DoEvents()
                    End While
                    Label2.Text = (Date.Now - Time).TotalMilliseconds & " ms"
                    If Elapsed <= 200 Then
                        Label2.ForeColor = Drawing.Color.Lime
                    ElseIf Elapsed <= 300 Then
                        Label2.ForeColor = Drawing.Color.Yellow
                    ElseIf Elapsed <= 400 Then
                        Label2.ForeColor = Drawing.Color.Orange
                    ElseIf Elapsed <= 500 Then
                        Label2.ForeColor = Drawing.Color.DarkOrange
                    Else
                        Label2.ForeColor = Drawing.Color.Red
                    End If
                    'ProgressBar1.Maximum = CInt(Fix(Math.Max(ProgressBar1.Maximum, Elapsed)))
                    If Elapsed > 500 Then
                        PictureBox1.Size = New System.Drawing.Size(88, 9)
                    Else
                        PictureBox1.Size = New System.Drawing.Size(Math.Min(88, Fix(Elapsed * 88 / 500)), 9)
                    End If

                    Socket.Close()
                    While Socket.GetState <> Winsock.WinsockStates.Closed
                        Application.DoEvents()
                    End While
                Else
                    Label2.Text = "N/A"
                    Label2.ForeColor = Drawing.Color.White
                    PictureBox1.Size = New System.Drawing.Size(0, 9)
                End If

            Catch ex As Exception
            End Try
        End SyncLock
    End Sub

    Private Sub frmLagBar_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, Label2.MouseDown
        If Not My.Computer.Mouse.ButtonsSwapped And e.Button = MouseButtons.Left OrElse _
           My.Computer.Mouse.ButtonsSwapped And e.Button = MouseButtons.Right Then
            CType(sender, System.Windows.Forms.Control).Capture = False
            SendMessage(Handle.ToInt32, &HA1, 2, 0)
        End If
    End Sub

End Class