'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General  License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General  License for more details.
'
'    You should have received a copy of the GNU General  License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.

Imports Scripting

Public Interface IBattlelist

#Region " Properties "

    ReadOnly Property GetIndexPosition() As Integer
    ReadOnly Property Floor() As Integer
    ReadOnly Property EntityID() As Integer
    ReadOnly Property GetName() As String
    ReadOnly Property GetHPPercentage() As Integer
    ReadOnly Property IsOnScreen() As Boolean
    ReadOnly Property Direction() As Directions
    ReadOnly Property Distance(Optional ByVal IncludeFloor As Boolean = False) As Double
	ReadOnly Property GetDistanceFromLocation(ByVal Loc As ITibia.LocationDefinition, Optional ByVal IncludeFloor As Boolean = False, Optional ByVal CurrentIndex As Boolean = False) As Double
    ReadOnly Property IsMyself() As Boolean
    ReadOnly Property Location() As ITibia.LocationDefinition
    ReadOnly Property IsFollowed() As Boolean
    ReadOnly Property IsAttacked() As Boolean
    ReadOnly Property GetPartyStatus() As PartyStatus
    ReadOnly Property GetSkullMark() As SkullMark

    Property Speed() As Integer
    Property LightIntensity() As Integer
    Property LightColor() As Integer
    Property IsWalking() As Boolean
    Property HeadColor() As Integer
    Property BodyColor() As Integer
    Property LegsColor() As Integer
    Property FeetColor() As Integer
    Property OutfitID() As Integer
    Property GetAddons() As OutfitAddons

#End Region

#Region " Methods "

    Function WalkTo() As Boolean
    Function Find(ByVal Name As String, Optional ByVal OnScreen As Boolean = False) As Boolean
    Function Find(ByVal ID As Integer, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean
	Function Find(ByVal Location As ITibia.LocationDefinition, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean
    Function JumpToEntity(ByVal Type As IBattlelist.SpecialEntity) As Boolean
    Function IsPlayer() As Boolean
    Function IsPlayer(ByVal EntityID As Integer) As Boolean
    Function Reset(Optional ByVal OnScreen As Boolean = False) As Boolean
    Sub Attack()
    Function NextEntity(Optional ByVal OnScreen As Boolean = False) As Boolean
    Function PrevEntity(Optional ByVal OnScreen As Boolean = False) As Boolean
    Function CreaturesOnScreen() As Boolean

#End Region

#Region " Enumerations "

    <Flags()> Enum OutfitAddons
        None = 0
        First = 1
        Second = 2
        Both = 3
    End Enum

    Enum PartyStatus
        None
        Unknown
        Invited
        Member
        Leader
    End Enum

    Enum SkullMark
        None
        Yellow
        Green
        White
        Red
    End Enum

    Enum SpecialEntity
        Myself
        Attacked
        Followed
    End Enum

    Enum Directions As Int32
        Up
        Right
        Down
        Left
        UpRight
        DownRight
        DownLeft
        UpLeft
    End Enum

#End Region

End Interface