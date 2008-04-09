Imports System.Drawing

Public Class frmILMap

    Private Sub frmILMap_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmILMap_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            Dim nop As Integer = Kernel.ILNumberofPlayers
            If nop > 0 Then
                ILPlayer1Pic.Visible = True
                If nop > 1 Then
                    ILPlayer2Pic.Visible = True
                    If nop > 2 Then
                        ILPlayer3Pic.Visible = True
                        If nop > 3 Then
                            ILPlayer4Pic.Visible = True
                            If nop > 4 Then
                                ILPlayer5Pic.Visible = True
                                If nop > 5 Then
                                    ILPlayer6Pic.Visible = True
                                    If nop > 6 Then
                                        ILPlayer7Pic.Visible = True
                                        If nop > 7 Then
                                            ILPlayer8Pic.Visible = True
                                            If nop > 8 Then
                                                ILPlayer9Pic.Visible = True
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            Kernel.ILProcessTimerObj.StartTimer()
            ILMapUpdate.Start()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ILMapUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILMapUpdate.Tick
        Try
            Dim nop As Integer = Kernel.ILNumberofPlayers
            Select Case nop
                Case Is = 1
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                Case Is = 2
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                Case Is = 3
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                Case Is = 4
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                Case Is = 5
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                    ILPlayer5Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer5LocX - 31790) * 0.35), ((Kernel.ILPlayer5LocY - 30856) * 0.35))
                Case Is = 6
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                    ILPlayer5Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer5LocX - 31790) * 0.35), ((Kernel.ILPlayer5LocY - 30856) * 0.35))
                    ILPlayer6Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer6LocX - 31790) * 0.35), ((Kernel.ILPlayer6LocY - 30856) * 0.35))
                Case Is = 7
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                    ILPlayer5Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer5LocX - 31790) * 0.35), ((Kernel.ILPlayer5LocY - 30856) * 0.35))
                    ILPlayer6Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer6LocX - 31790) * 0.35), ((Kernel.ILPlayer6LocY - 30856) * 0.35))
                    ILPlayer7Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer7LocX - 31790) * 0.35), ((Kernel.ILPlayer7LocY - 30856) * 0.35))
                Case Is = 8
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                    ILPlayer5Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer5LocX - 31790) * 0.35), ((Kernel.ILPlayer5LocY - 30856) * 0.35))
                    ILPlayer6Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer6LocX - 31790) * 0.35), ((Kernel.ILPlayer6LocY - 30856) * 0.35))
                    ILPlayer7Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer7LocX - 31790) * 0.35), ((Kernel.ILPlayer7LocY - 30856) * 0.35))
                    ILPlayer8Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer8LocX - 31790) * 0.35), ((Kernel.ILPlayer8LocY - 30856) * 0.35))
                Case Is = 9
                    ILPlayer1Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer1LocX - 31790) * 0.35), ((Kernel.ILPlayer1LocY - 30856) * 0.35))
                    ILPlayer2Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer2LocX - 31790) * 0.35), ((Kernel.ILPlayer2LocY - 30856) * 0.35))
                    ILPlayer3Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer3LocX - 31790) * 0.35), ((Kernel.ILPlayer3LocY - 30856) * 0.35))
                    ILPlayer4Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer4LocX - 31790) * 0.35), ((Kernel.ILPlayer4LocY - 30856) * 0.35))
                    ILPlayer5Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer5LocX - 31790) * 0.35), ((Kernel.ILPlayer5LocY - 30856) * 0.35))
                    ILPlayer6Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer6LocX - 31790) * 0.35), ((Kernel.ILPlayer6LocY - 30856) * 0.35))
                    ILPlayer7Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer7LocX - 31790) * 0.35), ((Kernel.ILPlayer7LocY - 30856) * 0.35))
                    ILPlayer8Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer8LocX - 31790) * 0.35), ((Kernel.ILPlayer8LocY - 30856) * 0.35))
                    ILPlayer9Pic.Location = New System.Drawing.Point(((Kernel.ILPlayer9LocX - 31790) * 0.35), ((Kernel.ILPlayer9LocY - 30856) * 0.35))
                    Exit Sub
            End Select
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmILMap_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseHover
        ILInfoLabel.Text = ""
        ILInfoLabel.Location = New System.Drawing.Point(0, 0)
    End Sub

    Private Sub ILPlayer1Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer1Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer1Name & " " & Kernel.ILPlayer1HP & "HP " & Kernel.ILPlayer1MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer1Pic.Location.X + 25), (ILPlayer1Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer2Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer2Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer2Name & " " & Kernel.ILPlayer2HP & "HP " & Kernel.ILPlayer2MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer2Pic.Location.X + 25), (ILPlayer2Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer3Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer3Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer3Name & " " & Kernel.ILPlayer3HP & "HP " & Kernel.ILPlayer3MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer3Pic.Location.X + 25), (ILPlayer3Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer4Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer4Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer4Name & " " & Kernel.ILPlayer4HP & "HP " & Kernel.ILPlayer4MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer4Pic.Location.X + 25), (ILPlayer4Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer5Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer5Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer5Name & " " & Kernel.ILPlayer5HP & "HP " & Kernel.ILPlayer5MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer5Pic.Location.X + 25), (ILPlayer5Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer6Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer6Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer6Name & " " & Kernel.ILPlayer6HP & "HP " & Kernel.ILPlayer6MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer6Pic.Location.X + 25), (ILPlayer6Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer7Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer7Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer7Name & " " & Kernel.ILPlayer7HP & "HP " & Kernel.ILPlayer7MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer7Pic.Location.X + 25), (ILPlayer7Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer8Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer8Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer8Name & " " & Kernel.ILPlayer8HP & "HP " & Kernel.ILPlayer8MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer8Pic.Location.X + 25), (ILPlayer8Pic.Location.Y - 20))
    End Sub

    Private Sub ILPlayer9Pic_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ILPlayer9Pic.MouseHover
        ILInfoLabel.Text = Kernel.ILPlayer9Name & " " & Kernel.ILPlayer9HP & "HP " & Kernel.ILPlayer9MP & "MP"
        ILInfoLabel.Location = New System.Drawing.Point((ILPlayer9Pic.Location.X + 25), (ILPlayer9Pic.Location.Y - 20))
    End Sub
End Class