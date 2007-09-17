Imports TibiaTekBot3.CoreModule.CoreClass

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

        Public Shared Function WorldZToClientZ(ByVal Index As Integer) As Integer
            If Index >= 0 AndAlso Index <= 7 Then
                Return 7 - Index
            ElseIf Index >= 8 AndAlso Index <= 15 Then
                Return Index - 8
            Else
                Throw New IndexOutOfRangeException("World Map Z (Floor level) has has to be between 0 and 15 inclusive.")
            End If
        End Function

        Public Shared Function ClientZToWorldZ(ByVal ClientZ As Integer) As Integer
            Dim CharZ As Integer = Core.CharacterLoc.Z
            If CharZ >= 0 AndAlso CharZ <= 7 Then 'above ground
                Return 7 - ClientZ
            ElseIf CharZ >= 8 AndAlso CharZ <= 15 Then 'below ground
                Return 8 + ClientZ
            End If
            Return 0
        End Function

        Public Function GetAddress(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Integer
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
            Return MapBegins + (CellNumber * Core.Consts.MapTileDist)
        End Function

        Public Sub RefreshMapBeginning()
            Core.Tibia.Memory.Read(Core.Consts.ptrMapPointer, MapBegins, 4)
        End Sub

        Public Function FindObjectInTile(ByRef [Object] As TileObject, ByVal ItemID As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Boolean
            Dim Count As Integer = 0
            Dim ID As Integer = 0
            Dim Data As Integer = 0
            Dim ExtraData As Integer = 0
            Dim Address As Integer = GetAddress(X, Y, Z)
            Core.Tibia.Memory.Read(Address, Count, 1)
            For I As Integer = 0 To Count - 1
                Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectIdOffset, ID, 4)
                If ID = ItemID Then
                    Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectDataOffset, Data, 4)
                    Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectExtraDataOffset, ExtraData, 4)
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
        End Function

        Public Function GetTileObjects(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As TileObject()
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
            Core.Tibia.Memory.Read(Address, Count, 1)
            Dim TileObjects(Count - 1) As TileObject
            For I As Integer = 0 To Count - 1
                Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectIdOffset, ID, 4)
                Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectDataOffset, Data, 4)
                Core.Tibia.Memory.Read(Address + (I * Core.Consts.MapObjectDist) + Core.Consts.MapObjectExtraDataOffset, ExtraData, 4)
                TileObjects(I) = New TileObject(ID, Data, ExtraData, MapLocation, ClientCoords, I)
            Next
            Return TileObjects
        End Function

        Public ReadOnly Property IsBusy() As Boolean
            Get
                Return Busy
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub Refresh()
            Busy = True
            If MapBegins = 0 Then RefreshMapBeginning()
            SyncLock Me
                FindYourSelf()
                'WriteMap()
            End SyncLock
            Busy = False
        End Sub

        Private Sub FindYourSelf()
            MapZ = 0
            MapY = 0
            MapZ = 0
            Dim StackSize As Integer = 0
            Dim ObjectID As Integer = 0
            Dim Data As Integer = 0
            For I As Integer = 0 To 2015
                Core.Tibia.Memory.Read(MapBegins + (Core.Consts.MapTileDist * I), StackSize, 1)
                If StackSize < 2 Then Continue For ' there's got to be a tile, and another object, at least
                For E As Integer = 0 To StackSize - 1
                    Core.Tibia.Memory.Read(MapBegins + (I * Core.Consts.MapTileDist) + (E * Core.Consts.MapObjectDist) + Core.Consts.MapObjectIdOffset, ObjectID, 2)
                    If ObjectID = &H63 Then
                        Core.Tibia.Memory.Read(MapBegins + (I * Core.Consts.MapTileDist) + (E * Core.Consts.MapObjectDist) + Core.Consts.MapObjectDataOffset, Data, 4)
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
        End Sub

    End Class

End Module