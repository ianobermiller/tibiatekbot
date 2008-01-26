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

Imports System.Math, System.Xml, Scripting

Public Module WalkerModule

    Public Class Walker
        Public Coordinates As ITibia.LocationDefinition
        Public Type As WaypointType
        Public Info As String
        Public IsReady As Boolean

        Public Enum WaypointType
            Walk = 0
            StairsOrHole = 1
            Rope = 2
            Ladder = 3
            Say = 4
            Wait = 5
            Sewer = 6
            Shovel = 7
        End Enum

        Public Enum Directions
            Left = 1
            Down = 2
            Right = 3
            Up = 4
        End Enum


        Public Function MoveChar() As Boolean
            Try
                Dim BL As New BattleList
                Dim TD As New TileData
                BL.Reset()
                TD.Reset()
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                TD.JumpToTile(TileData.SpecialTile.Myself)
                Dim StatusText As String = ""
                Dim StatusTimer As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrStatusMessage, StatusText)
                Kernel.Client.ReadMemory(Consts.ptrStatusMessageTimer, StatusTimer, 4)
                Kernel.Client.ReadMemory(Consts.ptrCoordX, Kernel.CharacterLoc.X, 4)
                Kernel.Client.ReadMemory(Consts.ptrCoordY, Kernel.CharacterLoc.Y, 4)
                Kernel.Client.ReadMemory(Consts.ptrCoordZ, Kernel.CharacterLoc.Z, 1)
                Dim SP As New ServerPacketBuilder(Kernel.Proxy)
                If StatusText = "There is no way." And StatusTimer <> 0 Then
                    'BL.JumpToEntity(SpecialEntity.Myself)
                    If BL.IsWalking = False Then
                        Kernel.Client.WriteMemory(Consts.ptrGoToX, BL.GetLocation.X, 2)
                        Kernel.Client.WriteMemory(Consts.ptrGoToY, BL.GetLocation.Y, 2)
                        Kernel.Client.WriteMemory(Consts.ptrGoToZ, BL.GetLocation.Z, 1)
                        BL.IsWalking = True
                        IsReady = False
                        Return False
                    End If
                End If

                Select Case Type
                    Case WaypointType.Walk
                        If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                            IsReady = True
                            Return True
                        Else
                            If BL.IsWalking = False Then
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, CInt(Coordinates.X), 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, CInt(Coordinates.Y), 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, CInt(Coordinates.Z), 1)
                                BL.IsWalking = True
                                IsReady = False
                                Return False
                            End If
                        End If
                    Case WaypointType.Ladder
                        If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                            IsReady = True
                            Return True
                        End If
                        If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                            'TD.JumpToTile(TileData.SpecialTile.Myself)
                            TD.Get_TileInfo()
                            If TD.Count = 0 Then
                                Kernel.ConsoleError("Theres no objects in the tile you're standing.")
                                IsReady = False
                                Return False
                            End If
                            For i As Integer = 0 To TD.Count - 1 'CHECK THIS
                                If Kernel.Client.Objects.LensHelp(TD.ObjectId(i)) = IObjects.ObjectLensHelp.Ladder Then
                                    SP.UseObject(TD.ObjectId(i), Coordinates)
                                    'Core.Proxy.SendPacketToServer(UseObject(TD.ObjectId(i), Coordinates))
                                    IsReady = False
                                    Return False
                                End If
                            Next
                            Kernel.ConsoleWrite("Couldn't find Ladders from the tile you are standing.")
                            IsReady = False
                            Return False
                        Else
                            If BL.IsWalking = False Then
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, CInt(Coordinates.X), 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, CInt(Coordinates.Y), 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, CInt(Coordinates.Z), 1)
                                BL.IsWalking = True
                                IsReady = False
                                Return False
                            End If
                        End If

                    Case WaypointType.Rope
                        If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                            'TD.JumpToTile(TileData.SpecialTile.Myself)
                            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                            Dim Container As New Container
                            'Dim Rope As ContainerItemDefinition
                            Dim RopeId As UShort = Kernel.Client.Items.GetItemID("Rope")
                            Dim ServerPacket As New ServerPacketBuilder(Kernel.Proxy)
                            ServerPacket.UseObjectWithObjectOnGround(RopeId, BL.GetLocation, TD.TileId)
                            ServerPacket.Send()
                            System.Threading.Thread.Sleep(2000)
                            If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                                IsReady = True
                                Return True
                            End If
                        ElseIf Kernel.CharacterLoc.Z <> Coordinates.Z Then
                            IsReady = True
                            Return True
                        ElseIf BL.IsWalking = False Then
                            Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                            Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                            Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                            BL.IsWalking = True
                            IsReady = False
                            Return False
                        End If
                    Case WaypointType.StairsOrHole
                        If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                            IsReady = True
                            Return True
                        Else
                            If BL.IsWalking = False Then
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                BL.IsWalking = True
                                IsReady = False
                                Return False
                            End If
                        End If
                    Case WaypointType.Say
                        If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                            Dim CM As New ChatMessageDefinition
                            System.Threading.Thread.Sleep(1000)
                            CM.MessageType = ITibia.MessageType.Default
                            CM.DefaultMessageType = ITibia.DefaultMessageType.Normal
                            CM.Prioritize = True
                            CM.Message = Info
                            Kernel.ChatMessageQueueList.Add(CM)
                            System.Threading.Thread.Sleep(1000)
                            'Core.Proxy.SendPacketToServer(Speak())
                            IsReady = True
                            Return True
                        Else
                            If BL.IsWalking = False Then
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                BL.IsWalking = True
                                IsReady = False
                                Return False
                            End If
                        End If
                    Case WaypointType.Wait
                        If Kernel.WalkerWaitUntil < Date.Now Then
                            IsReady = True
                            Kernel.WalkerFirstTime = True
                            Return True
                        Else
                            IsReady = False
                            Kernel.WalkerFirstTime = False
                            Return False
                        End If
                    Case WaypointType.Sewer
                        If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                            IsReady = True
                            Return True
                        End If
                        If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                            'Let's get Sewer's id and position
                            Dim TileObjects() As IMapTiles.TileObject
                            Dim TileObject As IMapTiles.TileObject

                            TileObjects = Kernel.Client.MapTiles.GetTileObjects(8, 6, Kernel.Client.MapTiles.WorldZToClientZ(Kernel.CharacterLoc.Z))
                            For Each TileObject In TileObjects
                                'With Kernel.Client.Dat.GetInfo(TileObject.GetObjectID)
                                If Kernel.Client.Objects.LensHelp(TileObject.GetObjectID) = IObjects.ObjectLensHelp.Sewer Then
                                    Dim SPB As New ServerPacketBuilder(Kernel.Proxy)
                                    SPB.UseObject(TileObject.GetObjectID, Coordinates)
                                End If
                                'End With
                            Next

                        Else
                            If BL.IsWalking = False Then
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                BL.IsWalking = True
                                IsReady = False
                                Return False
                            End If
                        End If
                    Case WaypointType.Shovel
                        If Not Info = "" Then 'Normal shovel waypoint
                            If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                                IsReady = True
                                Return True
                            End If
                            If Kernel.CharacterLoc.X = Coordinates.X AndAlso Kernel.CharacterLoc.Y = Coordinates.Y AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                                Dim HoleLoc As New ITibia.LocationDefinition
                                'Finding hole location
                                HoleLoc = Kernel.CharacterLoc
                                Select Case CType(Info, Directions)
                                    Case Directions.Left
                                        HoleLoc.X -= 1
                                    Case Directions.Right
                                        HoleLoc.X += 1
                                    Case Directions.Up
                                        HoleLoc.Y -= 1
                                    Case Directions.Down
                                        HoleLoc.Y += 1
                                End Select
                                'Finding Shovel
                                Dim Shovel As Scripting.IContainer.ContainerItemDefinition
                                If (New Container).FindItem(Shovel, Kernel.Client.Items.GetItemID("Shovel")) = False Then
                                    If (New Container).FindItem(Shovel, Kernel.Client.Items.GetItemID("Light Shovel")) = False Then
                                        Kernel.ConsoleError("Unable to find shovel. Stopping for 10 seconds.")
                                        System.Threading.Thread.Sleep(10000)
                                        IsReady = False
                                        Return False
                                    End If
                                End If
                                Dim ServerPacket As New ServerPacketBuilder(Kernel.Proxy)
                                ServerPacket.UseObjectWithObjectOnGround(Shovel.ID, HoleLoc)
                                ServerPacket.Send()
                                'Core.Proxy.SendPacketToServer(UseObjectWithObjectOnGround(Shovel.ID, HoleLoc))
                                System.Threading.Thread.Sleep(1000)
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, HoleLoc.X, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, HoleLoc.Y, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, HoleLoc.Z, 1)
                                BL.IsWalking = True
                            Else
                                If BL.IsWalking = False Then
                                    Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                    Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                    Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                    BL.IsWalking = True
                                    IsReady = False
                                    Return False
                                End If
                            End If
                        Else 'WPT Shovel waypoint
                            If Kernel.CharacterLoc.Z <> Coordinates.Z Then
                                IsReady = True
                                Return True
                            End If
                            If Abs(Kernel.CharacterLoc.X - Coordinates.X) < 2 AndAlso Abs(Kernel.CharacterLoc.Y - Coordinates.Y) < 2 AndAlso Kernel.CharacterLoc.Z = Coordinates.Z Then
                                'Finding Shovel
                                Dim Shovel As Scripting.IContainer.ContainerItemDefinition
                                If (New Container).FindItem(Shovel, Kernel.Client.Items.GetItemID("Shovel")) = False Then
                                    If (New Container).FindItem(Shovel, Kernel.Client.Items.GetItemID("Light Shovel")) = False Then
                                        Kernel.ConsoleError("Unable to find shovel. Stopping for 10 seconds.")
                                        System.Threading.Thread.Sleep(10000)
                                        IsReady = False
                                        Return False
                                    End If
                                End If
                                Dim ServerPacket As New ServerPacketBuilder(Kernel.Proxy)
                                ServerPacket.UseObjectWithObjectOnGround(Shovel.ID, Coordinates)
                                ServerPacket.Send()
                                System.Threading.Thread.Sleep(1000)
                                Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                BL.IsWalking = True
                                System.Threading.Thread.Sleep(2000)
                            Else
                                If BL.IsWalking = False Then
                                    Kernel.Client.WriteMemory(Consts.ptrGoToX, Coordinates.X, 2)
                                    Kernel.Client.WriteMemory(Consts.ptrGoToY, Coordinates.Y, 2)
                                    Kernel.Client.WriteMemory(Consts.ptrGoToZ, Coordinates.Z, 1)
                                    BL.IsWalking = True
                                    IsReady = False
                                    Return False
                                End If
                            End If
                        End If
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Shared Function CheckDistance() As Boolean
            Try
                Dim BL As New BattleList
                Dim PrevWp As New ITibia.LocationDefinition

                If Kernel.Walker_Waypoints.Count = 0 Then Return True
                PrevWp = Kernel.Walker_Waypoints(Kernel.Walker_Waypoints.Count - 1).Coordinates

                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                If BL.GetDistanceFromLocation(PrevWp) > Consts.WaypointMaxDistance Then
                    Kernel.ConsoleError("The waypoint is too far.")
                    Return False
                Else
                    Return True
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
    End Class

    Public Sub Save(ByVal Path As String)
        Try
            Dim Document As New XmlDocument
            Dim xmlWayPoints As XmlElement = Document.CreateElement("Waypoints")
            For Each WayPoint As Walker In Kernel.Walker_Waypoints
                Dim xmlWayPoint As XmlElement = Document.CreateElement("WayPoint")
                Dim xmlPosX As XmlAttribute = Document.CreateAttribute("PosX")
                xmlPosX.InnerText = WayPoint.Coordinates.X
                Dim xmlPosY As XmlAttribute = Document.CreateAttribute("PosY")
                xmlPosY.InnerText = WayPoint.Coordinates.Y
                Dim xmlPosZ As XmlAttribute = Document.CreateAttribute("PosZ")
                xmlPosZ.InnerText = WayPoint.Coordinates.Z
                Dim xmlType As XmlAttribute = Document.CreateAttribute("Type")
                xmlType.InnerText = WayPoint.Type
                Dim xmlInfo As XmlAttribute = Document.CreateAttribute("Info")
                xmlInfo.InnerText = WayPoint.Info

                xmlWayPoint.Attributes.Append(xmlPosX)
                xmlWayPoint.Attributes.Append(xmlPosY)
                xmlWayPoint.Attributes.Append(xmlPosZ)
                xmlWayPoint.Attributes.Append(xmlType)
                xmlWayPoint.Attributes.Append(xmlInfo)
                xmlWayPoints.AppendChild(xmlWayPoint)
            Next
            Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
            Document.AppendChild(Declaration)
            Document.AppendChild(xmlWayPoints)
            Document.Save(Path)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub Load(ByVal Path As String)
        Try
            Kernel.Walker_Waypoints.Clear()
            If Path.ToLower.EndsWith("xml") Then 'TTB Waypoints

                Dim Document As New XmlDocument
                Document.Load(Path)
                Dim TempStr As String = ""

                For Each Element As XmlElement In Document.Item("Waypoints")
                    Dim Walker_Char As New Walker
                    TempStr = Element.GetAttribute("PosX")
                    If Not String.IsNullOrEmpty(TempStr) Then Walker_Char.Coordinates.X = CInt(TempStr)
                    TempStr = Element.GetAttribute("PosY")
                    If Not String.IsNullOrEmpty(TempStr) Then Walker_Char.Coordinates.Y = CInt(TempStr)
                    TempStr = Element.GetAttribute("PosZ")
                    If Not String.IsNullOrEmpty(TempStr) Then Walker_Char.Coordinates.Z = CInt(TempStr)
                    TempStr = Element.GetAttribute("Type")
                    If Not String.IsNullOrEmpty(TempStr) Then Walker_Char.Type = CInt(TempStr)
                    TempStr = Element.GetAttribute("Info")
                    If Not String.IsNullOrEmpty(TempStr) Then Walker_Char.Info = TempStr
                    Kernel.Walker_Waypoints.Add(Walker_Char)
                Next
            ElseIf Path.ToLower.EndsWith("wpt") Then 'WPT Waypoints. Thanks to Caxtor in this part!
                Dim TempStrWPT(4) As String
                Dim wptReader As New System.IO.StreamReader(Path)

                Kernel.Walker_Waypoints.Clear()

                Do While wptReader.Peek <> -1
                    Dim Walker_Char As New Walker
                    TempStrWPT(0) = wptReader.ReadLine 'X Coord
                    TempStrWPT(1) = wptReader.ReadLine 'Y Coord
                    TempStrWPT(2) = wptReader.ReadLine 'Z Coord
                    TempStrWPT(3) = wptReader.ReadLine 'Type of Action

                    If Not String.IsNullOrEmpty(TempStrWPT(3)) Then
                        Walker_Char.Coordinates.X = CInt(TempStrWPT(0))
                        Walker_Char.Coordinates.Y = CInt(TempStrWPT(1))
                        Walker_Char.Coordinates.Z = CInt(TempStrWPT(2))
                        Walker_Char.Info = ""
                        Select Case CInt(TempStrWPT(3))
                            Case 1 'Rope OK
                                Walker_Char.Type = Walker.WaypointType.Rope
                            Case 2 'Ladder OK
                                Walker_Char.Type = Walker.WaypointType.Ladder
                            Case 3 'Upstaris OK
                                Walker_Char.Coordinates.Y = CInt(TempStrWPT(1)) + 1
                                Walker_Char.Coordinates.Z = CInt(TempStrWPT(2)) + 1
                                Walker_Char.Type = Walker.WaypointType.StairsOrHole
                            Case 4 'Downstairs OK
                                Walker_Char.Coordinates.Y = CInt(TempStrWPT(1)) - 1
                                Walker_Char.Coordinates.Z = CInt(TempStrWPT(2)) - 1
                                Walker_Char.Type = Walker.WaypointType.StairsOrHole
                            Case 5 'Hole OK
                                Walker_Char.Coordinates.Z = CInt(TempStrWPT(2)) - 1
                                Walker_Char.Type = Walker.WaypointType.StairsOrHole
                            Case 6 'Ground OK
                                Walker_Char.Type = Walker.WaypointType.Walk
                            Case 7 'Shovel Revise...
                                Walker_Char.Coordinates.Z -= 1
                                Walker_Char.Type = Walker.WaypointType.Shovel
                            Case 8 'Ramp OK
                                Walker_Char.Type = Walker.WaypointType.StairsOrHole
                            Case 9 'Delay OK
                                Walker_Char.Info = CInt(TempStrWPT(0))
                                Walker_Char.Type = Walker.WaypointType.Wait
                            Case Else
                        End Select
                    End If
                    Kernel.Walker_Waypoints.Add(Walker_Char)
                Loop
                wptReader.Close()
            Else
                MessageBox.Show("TibiaTek Bot doesn't support the waypoint file", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            UpdateList()
        Catch
            MessageBox.Show("Unable to load waypoints.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Kernel.Walker_Waypoints.Clear()
            UpdateList()
        End Try
    End Sub
    Public Sub UpdateList()
        Try
            Kernel.CavebotForm.Waypointslst.Items.Clear()
            If Kernel.Walker_Waypoints.Count = 0 Then Exit Sub
            Dim Character As Walker
            Dim WPType As String
            For Each Character In Kernel.Walker_Waypoints
                Select Case Character.Type
                    Case Walker.WaypointType.Ladder
                        WPType = "L"
                    Case Walker.WaypointType.Rope
                        WPType = "R"
                    Case Walker.WaypointType.StairsOrHole
                        WPType = "S/H"
                    Case Walker.WaypointType.Walk
                        WPType = "W"
                    Case Walker.WaypointType.Say
                        WPType = "S"
                    Case Walker.WaypointType.Wait
                        WPType = "WT"
                    Case Walker.WaypointType.Sewer
                        WPType = "SE"
                    Case Walker.WaypointType.Shovel
                        WPType = "SH"
                    Case Else
                        WPType = "NotFound"
                End Select

                If Character.Type = Walker.WaypointType.Wait Then
                    Kernel.CavebotForm.Waypointslst.Items.Add(WPType & ": Wait: " & Character.Info)

                Else

                    Kernel.CavebotForm.Waypointslst.Items.Add(WPType & ":" & Character.Coordinates.X _
            & ":" & Character.Coordinates.Y _
            & ":" & Character.Coordinates.Z & " " & Character.Info)
                End If
            Next
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

End Module
