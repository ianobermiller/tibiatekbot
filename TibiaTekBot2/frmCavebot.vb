'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.Imports System.Math

Public Class frmCavebot

    Private Sub frmCavebot_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If Core.InGame Then
                Me.Text = "Cavebot/Walker for " & Core.Proxy.CharacterName
            Else
                Me.Text = "Cavebot/Walker"
            End If
            Waypointslst.SuspendLayout()
            Waypointslst.Items.Clear()
            WalkerModule.UpdateList()
            Waypointslst.ResumeLayout()
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                'Me.Enabled = False '<-- Freezes whole thing
                Me.AddWaypointcmd.Enabled = False
                Me.Typecmb.Enabled = False
                Me.Waittxt.Enabled = False
                Me.Waypointslst.Enabled = False
                Me.WPDeletecmd.Enabled = False
                Me.WPClearcmd.Enabled = False
                Me.Loadcmd.Enabled = False
            Else
                Me.AddWaypointcmd.Enabled = True
                Me.Typecmb.Enabled = True
                Me.Waittxt.Enabled = True
                Me.Waypointslst.Enabled = True
                Me.WPDeletecmd.Enabled = True
                Me.WPClearcmd.Enabled = True
                Me.Loadcmd.Enabled = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
    Private Sub frmCavebot_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmCavebot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            WalkerModule.UpdateList()
            LootMinimumCap.Value = Consts.CavebotLootMinCap
            LootFromCorpses.Checked = Consts.LootWithCavebot
            EatFromCorpses.Checked = Consts.LootEatFromCorpse
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AddWaypointcmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWaypointcmd.Click
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot/Walker is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Dim Character As New Walker
            Dim WpType As String = ""
            Dim Info As String = ""
            If Walker.CheckDistance = False Then Exit Sub
            Character.Coordinates = Core.CharacterLoc
            Select Case CType(Typecmb.SelectedIndex, Walker.WaypointType)
                Case Walker.WaypointType.Walk
                    Character.Type = Walker.WaypointType.Walk
                    WpType = "W"
                Case Walker.WaypointType.Rope
                    Character.Type = Walker.WaypointType.Rope
                    WpType = "R"
                Case Walker.WaypointType.StairsOrHole
                    Character.Type = Walker.WaypointType.StairsOrHole
                    WpType = "S/H"
                    If dUp.Checked = True Then
                        Character.Coordinates.Y -= 1
                    ElseIf dLeft.Checked = True Then
                        Character.Coordinates.X -= 1
                    ElseIf dDown.Checked = True Then
                        Character.Coordinates.Y += 1
                    ElseIf dRight.Checked = True Then
                        Character.Coordinates.X += 1
                    Else
                        Beep()
                        Exit Sub
                    End If
                Case Walker.WaypointType.Ladder
                    Character.Type = Walker.WaypointType.Ladder
                    WpType = "L"
                Case Walker.WaypointType.Say
                    Character.Type = Walker.WaypointType.Say
                    WpType = "S"
                    If String.IsNullOrEmpty(Infotxt.Text) Then
                        Beep()
                        Exit Sub
                    End If
                    Info = Infotxt.Text
                    Character.Info = Infotxt.Text
                Case Walker.WaypointType.Wait
                    Character.Type = Walker.WaypointType.Wait
                    WpType = "WT"
                    If String.IsNullOrEmpty(Waittxt.Text) Then
                        Beep()
                        Exit Sub
                    Else
                        Try
                            Integer.Parse(Waittxt.Text)
                        Catch ex As Exception
                            Beep()
                            Exit Sub
                        End Try
                    End If
                    Info = "Wait: " & Waittxt.Text
                    Character.Info = Waittxt.Text
                Case Walker.WaypointType.Sewer
                    Character.Type = Walker.WaypointType.Sewer
                    WpType = "SE"
                Case Walker.WaypointType.Shovel
                    Character.Type = Walker.WaypointType.Shovel
                    WpType = "SH"
                    If dUp.Checked = True Then
                        Info = Walker.Directions.Up
                    ElseIf dLeft.Checked = True Then
                        Info = Walker.Directions.Left
                    ElseIf dDown.Checked = True Then
                        Info = Walker.Directions.Down
                    ElseIf dRight.Checked = True Then
                        Info = Walker.Directions.Right
                    Else
                        Beep()
                        Exit Sub
                    End If
                    Character.Info = Info
                Case Else 'shouldnt happen but just in case o.o
                    Beep()
                    Exit Sub
            End Select


            Core.Walker_Waypoints.Add(Character)
            If Character.Type = Walker.WaypointType.Wait Then
                Waypointslst.Items.Add(WpType & " " & Info)
            Else
                Waypointslst.Items.Add(WpType & ":" & Character.Coordinates.X _
                & ":" & Character.Coordinates.Y _
                & ":" & Character.Coordinates.Z & " " & Character.Info)
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Typecmb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Typecmb.SelectedIndexChanged
        Try
            AddWaypointcmd.Enabled = True
            Select Case CType(Typecmb.SelectedIndex, Walker.WaypointType)
                Case Walker.WaypointType.StairsOrHole, Walker.WaypointType.Shovel
                    Direction.Visible = True
                    Infobox.Visible = False
                    Waitbox.Visible = False
                Case Walker.WaypointType.Say
                    Direction.Visible = False
                    Infobox.Visible = True
                    Waitbox.Visible = False
                Case Walker.WaypointType.Wait
                    Waitbox.Visible = True
                    Direction.Visible = False
                    Infobox.Visible = False
                Case Else
                    Direction.Visible = False
                    Infobox.Visible = False
                    Waitbox.Visible = False
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub WPDeletecmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WPDeletecmd.Click
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot/Walker is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If Waypointslst.SelectedIndex = -1 OrElse Waypointslst.SelectedIndex >= Core.Walker_Waypoints.Count Then
                Beep()
                Exit Sub
            End If
            Core.Walker_Waypoints.RemoveAt(Waypointslst.SelectedIndex)
            Waypointslst.Items.RemoveAt(Waypointslst.SelectedIndex)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub WPClearcmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WPClearcmd.Click
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot/Walker is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If Core.Walker_Waypoints.Count = 0 Then
                Beep()
                Exit Sub
            End If
            Core.Walker_Waypoints.Clear()
            Waypointslst.Items.Clear()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Savecmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savecmd.Click
        Try
            If Core.Walker_Waypoints.Count = 0 Then
                MsgBox("No waypoints to save.", MsgBoxStyle.Question, "Cannot save waypoints")
                Exit Sub
            End If
            Dim SaveDlg As New SaveFileDialog
            Dim WalkerChar As New Walker
            With SaveDlg
                .InitialDirectory = GetWaypointsDirectory() & "\"
                .FileName = Core.Proxy.CharacterName & ".Waypoints.xml"
                .DefaultExt = "xml"
                .Title = BotName & " - Save Cavebot Waypoints"
                .Filter = "Xml File|*.xml"
            End With
            If SaveDlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            WalkerModule.Save(SaveDlg.FileName)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Loadcmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Loadcmd.Click
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                MessageBox.Show("Cavebot/Walker is currently running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Dim OpenDlg As New OpenFileDialog
            Dim WalkerChar As New Walker
            With OpenDlg
                .InitialDirectory = GetWaypointsDirectory() & "\"
                .Title = BotName & " - Load Cavebot Waypoints"
                .DefaultExt = "xml"
                .Filter = "Xml Files|*.xml"
            End With
            If OpenDlg.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            WalkerModule.Load(OpenDlg.FileName)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub EnableMonsterList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableMonsterList.CheckedChanged
        Try
            If EnableMonsterList.Checked Then
                MonsterList.Enabled = True
                Core.AutoAttackerListEnabled = True
                AddMonster.Enabled = True
                RemoveMonster.Enabled = True
            Else
                MonsterList.Enabled = False
                Core.AutoAttackerListEnabled = False
                AddMonster.Enabled = False
                RemoveMonster.Enabled = False
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub AddMonster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddMonster.Click
        Try
            Dim Monster As String = InputBox("Enter the monster name exactly as it's shown on the screen. Example: Rotworm")
            MonsterList.Items.Add(Monster)
            Core.AutoAttackerList.Add(Monster)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub RemoveMonster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveMonster.Click
        Try
            If MonsterList.SelectedIndex = -1 Then
                Beep()
                Exit Sub
            End If
            Core.AutoAttackerList.RemoveAt(MonsterList.SelectedIndex)
            MonsterList.Items.RemoveAt(MonsterList.SelectedIndex)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
End Class