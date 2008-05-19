Imports System.Drawing
Imports System.Globalization

Public Class frmGPSMap

    'Private State As Drawing.Drawing2D.GraphicsState

    Private Sub frmILMap_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Private Sub frmILMap_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            GPSMapUpdate.Start()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub ILMapUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GPSMapUpdate.Tick
        Try
            ListBox1.SuspendLayout()
            PictureBox1.Refresh()
            Static G As Graphics = PictureBox1.CreateGraphics()
            G.SmoothingMode = Drawing2D.SmoothingMode.None
            Static f As New Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Pixel)
            Dim s As SizeF
            SyncLock Kernel.GPSPlayerEntries

                For Each GPSEntry As GPSPlayerEntry In Kernel.GPSPlayerEntries.Values
                    If DateTime.Now.Subtract(GPSEntry.LastUpdate).TotalMinutes >= 1 Then
                        If ListBox1.Items.Contains(GPSEntry.Name) Then
                            ListBox1.Items.RemoveAt(ListBox1.Items.IndexOf(GPSEntry.Name))
                        End If
                    Else
                        If Not ListBox1.Items.Contains(GPSEntry.Name) Then
                            ListBox1.Items.Add(GPSEntry.Name)
                        End If
                    End If
                Next

                For Each GPSEntry As GPSPlayerEntry In Kernel.GPSPlayerEntries.Values
                    If DateTime.Now.Subtract(GPSEntry.LastUpdate).TotalSeconds > 60 Then Continue For
                    If GPSEntry.Loc.X > 0 AndAlso GPSEntry.Loc.Y > 0 Then
                        G.DrawImage(My.Resources.location_pointer_bw, New Point(GPSEntry.Loc.X - 31790, GPSEntry.Loc.Y - 30856))
                        s = G.MeasureString(GPSEntry.Name, f)
                        G.DrawString(GPSEntry.Name, f, Brushes.Red, Kernel.Client.CharacterLocation.X - 31790 - (s.Width / 2) + 8, Kernel.Client.CharacterLocation.Y - 30856 - 9)
                        G.DrawString(GPSEntry.Name, f, Brushes.Yellow, Kernel.Client.CharacterLocation.X - 31790 - (s.Width / 2) + 7, Kernel.Client.CharacterLocation.Y - 30856 - 10)
                    End If
                Next
            End SyncLock
            G.DrawImage(My.Resources.location_pointer_green, New Point(Kernel.Client.CharacterLocation.X - 31790, Kernel.Client.CharacterLocation.Y - 30856))
            s = G.MeasureString(Kernel.Client.CharacterName, f)
            G.DrawString(Kernel.Client.CharacterName, f, Brushes.Red, Kernel.Client.CharacterLocation.X - 31790 - (s.Width / 2) + 8, Kernel.Client.CharacterLocation.Y - 30856 - 9)
            G.DrawString(Kernel.Client.CharacterName, f, Brushes.Yellow, Kernel.Client.CharacterLocation.X - 31790 - (s.Width / 2) + 7, Kernel.Client.CharacterLocation.Y - 30856 - 10)
            G.Flush()
        Catch ex As Exception
            ShowError(ex)
        Finally
            ListBox1.ResumeLayout()

        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            SyncLock Kernel.GPSPlayerEntries
                If ListBox1.SelectedIndex >= 0 AndAlso Kernel.GPSPlayerEntries.ContainsKey(ListBox1.Items(ListBox1.SelectedIndex)) Then
                    Dim GPE As GPSPlayerEntry = Kernel.GPSPlayerEntries(ListBox1.Items(ListBox1.SelectedIndex))
                    Label1.Text = GPE.Name & vbCrLf
                    If GPE.Loc.X > 0 AndAlso GPE.Loc.Y > 0 Then
                        Label1.Text &= "X: " & GPE.Loc.X & vbCrLf & "Y:" & GPE.Loc.Y & vbCrLf & "Z:" & GPE.Loc.Z & vbCrLf
                    End If
                    If GPE.Level > 0 Then
                        Label1.Text &= "Level: " & GPE.Level & vbCrLf
                    End If
                    If GPE.HealthPoints > 0 Then
                        Label1.Text &= "HP: " & GPE.HealthPoints & vbCrLf
                    End If
                    If GPE.ManaPoints > 0 Then
                        Label1.Text &= "MP: " & GPE.ManaPoints & vbCrLf
                    End If
                    Dim p As New Point(GPE.Loc.X - 31790, GPE.Loc.Y - 30856)

                    SplitContainer1.Panel2.HorizontalScroll.Value = p.X
                    SplitContainer1.Panel2.VerticalScroll.Value = p.Y
                End If
            End SyncLock
        Catch
        End Try
    End Sub


End Class