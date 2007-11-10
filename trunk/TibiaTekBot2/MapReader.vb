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

Public Module MapReaderModule

    Public Structure ClientCoordinates
        Public X As Integer
        Public Y As Integer
        Public Z As Integer
    End Structure

    Public Structure TileObject
        Private ItemID As Integer
        Private Data As Integer
        Private ExtraData As Integer
        Private Location As LocationDefinition
        Private ClientCoords As ClientCoordinates
        Private StackPosition As Integer

        Public ReadOnly Property GetMapLocation() As LocationDefinition
            Get
                Return Location
            End Get
        End Property

        Public ReadOnly Property GetClientCoordinates() As ClientCoordinates
            Get
                Return ClientCoords
            End Get
        End Property

        Public ReadOnly Property GetObjectID() As Integer
            Get
                Return ItemID
            End Get
        End Property

        Public ReadOnly Property GetData() As Integer
            Get
                Return Data
            End Get
        End Property

        Public ReadOnly Property GetExtraData() As Integer
            Get
                Return ExtraData
            End Get
        End Property

        Public ReadOnly Property GetStackPosition() As Integer
            Get
                Return StackPosition
            End Get
        End Property

        Public Sub New(ByVal ItemID As Integer, ByVal Data As Integer, ByVal ExtraData As Integer, ByVal MapLocation As LocationDefinition, ByVal Coordinates As ClientCoordinates, ByVal StackPos As Integer)
            Me.ItemID = ItemID
            Me.Data = Data
            Me.ExtraData = ExtraData
            Me.Location = MapLocation
            Me.ClientCoords = Coordinates
            Me.StackPosition = StackPos
        End Sub
    End Structure

    Public Class MapReader

        Public MapBegins As Integer = 0
        'Private Map(7, 13, 17) As Integer
        Public MapX As Integer = 0
        Public MapY As Integer = 0
        Public MapZ As Integer = 0
        Private MapI As Integer = 0

        Private Busy As Boolean = False

        Public Shared Function WorldZToClientZ(ByVal WorldMapZ As Integer) As Integer
            Try
                If WorldMapZ >= 0 AndAlso WorldMapZ <= 7 Then
                    Return 7 - WorldMapZ
                ElseIf WorldMapZ >= 8 AndAlso WorldMapZ <= 15 Then
                    Return WorldMapZ - 8
                Else
                    Throw New IndexOutOfRangeException("World Map Z (Floor level) has has to be between 0 and 15 inclusive.")
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Shared Function ClientZToWorldZ(ByVal ClientZ As Integer) As Integer
            Try
                Dim CharZ As Integer = Core.CharacterLoc.Z
                If CharZ >= 0 AndAlso CharZ <= 7 Then 'above ground
                    Return 7 - ClientZ
                ElseIf CharZ >= 8 AndAlso CharZ <= 15 Then 'below ground
                    Return 8 + ClientZ
                End If
                Return 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetAddress(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Integer
            Try
                Dim SomeX As Integer = 0
                Dim SomeY As Integer = 0
                Dim SomeZ As Integer = 0
                Dim CellNumber As Integer = 0
                SomeX = MapX + X - 8
                If SomeX > 17 Then
                    SomeX -= 18
                ElseIf SomeX < 0 Then
                    SomeX += 18
                End If
                SomeY = MapY + Y - 6
                If SomeY > 13 Then
                    SomeY -= 14
                ElseIf SomeY < 0 Then
                    SomeY += 14
                End If
                If Core.CharacterLoc.Z >= 0 AndAlso Core.CharacterLoc.Z <= 7 Then
                    SomeZ = MapZ - WorldZToClientZ(Core.CharacterLoc.Z) + Z
                ElseIf Core.CharacterLoc.Z >= 8 AndAlso Core.CharacterLoc.Z <= 15 Then
                    SomeZ = MapZ + WorldZToClientZ(Core.CharacterLoc.Z) - Z
                End If
                If SomeZ < 0 Then
                    SomeZ += 8
                ElseIf SomeZ > 7 Then
                    SomeZ -= 8
                End If
                CellNumber = SomeX + (SomeY * 18) + (SomeZ * 14 * 18)
                Return MapBegins + (CellNumber * Consts.MapTileDist)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub RefreshMapBeginning()
            Try
                Core.ReadMemory(Consts.ptrMapPointer, MapBegins, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function FindObjectInTile(ByRef [Object] As TileObject, ByVal ItemID As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Boolean
            Try
                Dim Count As Integer = 0
                Dim ID As Integer = 0
                Dim Data As Integer = 0
                Dim ExtraData As Integer = 0
                Dim Address As Integer = GetAddress(X, Y, Z)
                Core.ReadMemory(Address, Count, 1)
                For I As Integer = 0 To Count - 1
                    Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ID, 4)
                    If ID = ItemID Then
                        Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectDataOffset, Data, 4)
                        Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectExtraDataOffset, ExtraData, 4)
                        Dim MapLocation As LocationDefinition
                        Dim ClientCoords As ClientCoordinates
                        MapLocation.X = Core.CharacterLoc.X + X - 8
                        MapLocation.Y = Core.CharacterLoc.Y + Y - 6
                        MapLocation.Z = ClientZToWorldZ(Z)
                        ClientCoords.X = X
                        ClientCoords.Y = Y
                        ClientCoords.Z = Z
                        [Object] = New TileObject(ItemID, Data, ExtraData, MapLocation, ClientCoords, I)
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetTileObjects(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As TileObject()
            Try
                Dim Address As Integer = GetAddress(X, Y, Z)
                Dim Count As Integer = 0
                Dim ID As Integer = 0
                Dim Data As Integer = 0
                Dim ExtraData As Integer = 0
                Dim MapLocation As LocationDefinition
                Dim ClientCoords As ClientCoordinates
                MapLocation.X = Core.CharacterLoc.X + X - 8
                MapLocation.Y = Core.CharacterLoc.Y + Y - 6
                MapLocation.Z = ClientZToWorldZ(Z)
                ClientCoords.X = X
                ClientCoords.Y = Y
                ClientCoords.Z = Z
                Core.ReadMemory(Address, Count, 1)
                Dim TileObjects(Count - 1) As TileObject
                For I As Integer = 0 To Count - 1
                    Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ID, 4)
                    Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectDataOffset, Data, 4)
                    Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectExtraDataOffset, ExtraData, 4)
                    TileObjects(I) = New TileObject(ID, Data, ExtraData, MapLocation, ClientCoords, I)
                Next
                Return TileObjects
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property IsBusy() As Boolean
            Get
                Return Busy
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub Refresh()
            Try
                Busy = True
                If MapBegins = 0 Then RefreshMapBeginning()
                SyncLock Me
                    FindYourSelf()
                    'WriteMap()
                End SyncLock
                Busy = False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub FindYourSelf()
            Try
                MapZ = 0
                MapY = 0
                MapZ = 0
                Dim StackSize As Integer = 0
                Dim ObjectID As Integer = 0
                Dim Data As Integer = 0
                For I As Integer = 0 To 2015
                    Core.ReadMemory(MapBegins + (Consts.MapTileDist * I), StackSize, 1)
                    If StackSize < 2 Then Continue For ' there's got to be a tile, and another object, at least
                    For E As Integer = 0 To StackSize - 1
                        Core.ReadMemory(MapBegins + (I * Consts.MapTileDist) + (E * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ObjectID, 2)
                        If ObjectID = &H63 Then
                            Core.ReadMemory(MapBegins + (I * Consts.MapTileDist) + (E * Consts.MapObjectDist) + Consts.MapObjectDataOffset, Data, 4)
                            If Data = Core.CharacterID Then
                                MapI = I
                                MapZ = Fix(I / (14 * 18))
                                MapY = Fix((I - MapZ * 14 * 18) / 18)
                                MapX = Fix((I - MapZ * 14 * 18 - MapY * 18))
                                Exit Sub
                            End If
                        End If
                    Next
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

    End Class

End Module