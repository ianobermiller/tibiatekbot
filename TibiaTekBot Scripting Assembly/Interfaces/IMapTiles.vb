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

Public Interface IMapTiles

#Region " Structures "

    Structure ClientCoordinates
        Public X As Integer
        Public Y As Integer
        Public Z As Integer
    End Structure

    Structure TileObject
        Private ItemID As Integer
        Private Data As Integer
        Private ExtraData As Integer
        Private Location As ITibia.LocationDefinition
        Private ClientCoords As IMapTiles.ClientCoordinates
        Private StackPosition As Integer

        Public ReadOnly Property GetMapLocation() As ITibia.LocationDefinition
            Get
                Return Location
            End Get
        End Property

        Public ReadOnly Property GetClientCoordinates() As IMapTiles.ClientCoordinates
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

        Public Sub New(ByVal ItemID As Integer, ByVal Data As Integer, ByVal ExtraData As Integer, ByVal MapLocation As ITibia.LocationDefinition, ByVal Coordinates As ClientCoordinates, ByVal StackPos As Integer)
            Me.ItemID = ItemID
            Me.Data = Data
            Me.ExtraData = ExtraData
            Me.Location = MapLocation
            Me.ClientCoords = Coordinates
            Me.StackPosition = StackPos
        End Sub
    End Structure

#End Region

#Region " Properties "
    ReadOnly Property IsBusy() As Boolean
#End Region

#Region " Methods "
    Function WorldZToClientZ(ByVal WorldMapZ As Integer) As Integer
    Function ClientZToWorldZ(ByVal ClientZ As Integer) As Integer
    Function GetAddress(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Integer
    Sub RefreshMapBeginning()
    Function FindObjectInTile(ByRef [Object] As TileObject, ByVal ItemID As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As Boolean
    Function GetTileObjects(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As TileObject()
    Sub Refresh()
#End Region
End Interface
