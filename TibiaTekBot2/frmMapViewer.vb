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
'    Boston, MA 02111-1307, USA.

Imports System.Drawing, System.Drawing.Imaging, TibiaTekBot.KernelModule, _
 TibiaTekBot.Constants, Scripting


Public Class frmMapViewer
    Dim Paused As Boolean = False

    Enum ImageTiles
        Void
        Walkable
        NotWalkable
        Player
        OtherPlayer
        Creature
        Teleport
        OpenDoor
        ClosedDoor
        DoorLocked
        Stairs
        Water
        WaterWithFish
        Ladder
        Down
        Mailbox
        Depot
        Swamp
    End Enum

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        If Kernel.Client.MapTiles.IsBusy Then Exit Sub
        'Me.SuspendLayout()
        Dim g As Graphics = Me.PictureBox1.CreateGraphics()
        Try
            g.SmoothingMode = Drawing2D.QualityMode.Low
            Dim Images(17, 13) As ImageTiles
            Dim StackCount As Integer = 0
            Dim ObjectID As Integer = 0
            Dim Data As Integer = 0
            Dim SomeX As Integer = 0
            Dim SomeY As Integer = 0
            Dim SomeZ As Integer = 0
            Dim Address As Integer = 0
            For Left As Integer = 0 To 17
                For Top As Integer = 0 To 13
                    Address = Kernel.Client.MapTiles.GetAddress(Left, Top, Floor.Value)
                    Kernel.Client.ReadMemory(Address, StackCount, 1)
                    If StackCount = 0 Then
                        Images(Left, Top) = ImageTiles.Void
                    Else
                        Images(Left, Top) = ImageTiles.NotWalkable
                        For I As Integer = 0 To StackCount - 1
                            Kernel.Client.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ObjectID, 2)
                            If ObjectID = &H63 Then
                                Kernel.Client.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectDataOffset, Data, 4)
                                If Data = Kernel.CharacterID Then
                                    Images(Left, Top) = ImageTiles.Player
                                ElseIf Data < &H40000000 Then 'player
                                    Images(Left, Top) = ImageTiles.OtherPlayer
                                Else 'monster/npc
                                    Images(Left, Top) = ImageTiles.Creature
                                End If
                                Exit For
                            Else
                                Dim ItemName As String = Kernel.Client.Items.GetItemName(ObjectID)
                                Select Case ItemName
                                    Case "Teleport"
                                        Images(Left, Top) = ImageTiles.Teleport
                                    Case "Open Door", "Open H. Door"
                                        Images(Left, Top) = ImageTiles.OpenDoor
                                    Case "Water"
                                        Images(Left, Top) = ImageTiles.Water
                                    Case "Water With Fish"
                                        Images(Left, Top) = ImageTiles.WaterWithFish
                                    Case "Closed Door", "Closed H. Door"
                                        Images(Left, Top) = ImageTiles.ClosedDoor
                                    Case "Locked Door"
                                        Images(Left, Top) = ImageTiles.DoorLocked
                                    Case "Ladder"
                                        Images(Left, Top) = ImageTiles.Ladder
                                    Case "Down"
                                        Images(Left, Top) = ImageTiles.Down
                                    Case "Stairs"
                                        Images(Left, Top) = ImageTiles.Stairs
                                    Case "Mailbox"
                                        Images(Left, Top) = ImageTiles.Mailbox
                                    Case "Depot"
                                        Images(Left, Top) = ImageTiles.Depot
                                    Case Else
                                        Try
                                            If ObjectID >= Kernel.Client.Dat.Length Then
                                                Images(Left, Top) = ImageTiles.Swamp
                                            ElseIf Kernel.Client.Dat.GetInfo(ObjectID).IsGroundTile Then
                                                Images(Left, Top) = ImageTiles.Walkable
                                            ElseIf Kernel.Client.Dat.GetInfo(ObjectID).Blocking Then
                                                Images(Left, Top) = ImageTiles.NotWalkable
                                            End If
                                        Catch
                                            Images(Left, Top) = ImageTiles.Void
                                        End Try
                                End Select
                            End If
                        Next
                    End If
                Next
            Next
            Dim Img As Image
            Me.PictureBox1.SuspendLayout()

            For Left As Integer = 0 To 17
                For Top As Integer = 0 To 13
                    Select Case Images(Left, Top)
                        Case ImageTiles.ClosedDoor
                            Img = My.Resources.doorclosed
                        Case ImageTiles.Creature
                            Img = My.Resources.creature
                        Case ImageTiles.Depot
                            Img = My.Resources.depot
                        Case ImageTiles.DoorLocked
                            Img = My.Resources.doorlocked
                        Case ImageTiles.Down
                            Img = My.Resources.stairsgodown
                        Case ImageTiles.Ladder
                            Img = My.Resources.ladder
                        Case ImageTiles.Mailbox
                            Img = My.Resources.mailbox
                        Case ImageTiles.NotWalkable
                            Img = My.Resources.notwalkable
                        Case ImageTiles.OpenDoor
                            Img = My.Resources.dooropen
                        Case ImageTiles.OtherPlayer
                            Img = My.Resources.otherplayer
                        Case ImageTiles.Player
                            Img = My.Resources.player
                        Case ImageTiles.Stairs
                            Img = My.Resources.stairsgoup
                        Case ImageTiles.Teleport
                            Img = My.Resources.teleport
                        Case ImageTiles.Void
                            Img = My.Resources.void
                        Case ImageTiles.Walkable
                            Img = My.Resources.walkable
                        Case ImageTiles.Water
                            Img = My.Resources.waternofish
                        Case ImageTiles.WaterWithFish
                            Img = My.Resources.waterfish
                        Case ImageTiles.Swamp
                            Img = My.Resources.swamp
                        Case Else
                            Img = My.Resources.void
                    End Select
                    g.DrawImage(Img, New Point((25 * Left) + 1, (25 * Top) + 1))
                Next
            Next
            'Me.ResumeLayout()
            Me.PictureBox1.ResumeLayout()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmMapViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer.Start()
    End Sub

    Private Sub frmMapViewer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Timer.Stop()
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        Try
            If Paused Then Exit Sub
            TileObjectsList.SuspendLayout()
            TileObjectsList.Items.Clear()
            Dim Left As Integer = Math.Floor(e.X / 25)
            Dim Top As Integer = Math.Floor(e.Y / 25)
            Dim Loc As ITibia.LocationDefinition
            Loc.X = Kernel.CharacterLoc.X + (Left - 8)
            Loc.Y = Kernel.CharacterLoc.Y + (Top - 6)
            Loc.Z = Kernel.CharacterLoc.Z
            ToolTip1.SetToolTip(PictureBox1, "(" & Loc.X & "," & Loc.Y & "," & Loc.Z & ")")
            If Left >= 18 OrElse Top >= 14 Then Exit Sub
            Dim StackCount As Integer = 0
            Dim ObjectID As Integer = 0
            Dim Data As Integer = 0
            Dim BL As New BattleList
            Dim Output As String = ""
            Dim ItemName As String = ""
            Dim TileObjects() As IMapTiles.TileObject = Kernel.Client.MapTiles.GetTileObjects(Left, Top, Floor.Value)
            StackCount = TileObjects.Length
            For Each TileObj As IMapTiles.TileObject In TileObjects
                ObjectID = TileObj.GetObjectID
                Data = TileObj.GetData
                If ObjectID = &H63 Then
                    BL.Find(Data)
                    Output = BL.GetName & " (H" & Hex(Data) & ")"
                Else
                    ItemName = Kernel.Client.Items.GetItemName(ObjectID)
                    Output = ItemName & " (H" & Hex(ObjectID) & ")"
                End If
                TileObjectsList.Items.Add(Output)
            Next
            TileObjectsList.ResumeLayout()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub PictureBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Try
            Dim Left As Integer = Math.Floor(e.X / 25)
            Dim Top As Integer = Math.Floor(e.Y / 25)
            Dim Loc As ITibia.LocationDefinition
            Loc.X = Kernel.CharacterLoc.X + (Left - 8)
            Loc.Y = Kernel.CharacterLoc.Y + (Top - 6)
            Loc.Z = Kernel.CharacterLoc.Z
            Kernel.Client.WriteMemory(Consts.ptrGoToX, CInt(Loc.X), 2)
            Kernel.Client.WriteMemory(Consts.ptrGoToY, CInt(Loc.Y), 2)
            Kernel.Client.WriteMemory(Consts.ptrGoToZ, CInt(Loc.Z), 1)
            Dim bl As New BattleList
            bl.JumpToEntity(IBattlelist.SpecialEntity.Myself)
            bl.IsWalking = True
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub ToolTip1_Popup(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PopupEventArgs) Handles ToolTip1.Popup
        Try
            Dim Left As Integer = Math.Floor((Windows.Forms.Control.MousePosition.X - PictureBox1.Left) / 25)
            Dim Top As Integer = Math.Floor((Windows.Forms.Control.MousePosition.Y - PictureBox1.Top) / 25)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub PictureBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then Paused = Not Paused
    End Sub

   
End Class