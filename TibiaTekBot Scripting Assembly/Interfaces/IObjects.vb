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
Imports System.Runtime.InteropServices

Public Interface IObjects

#Region " Enumerations "
    <Flags()> Enum ObjectFlags As UInteger
        None = &H0
        WalkSpeed = &H1
        TopOrder1 = WalkSpeed << 1
        TopOrder2 = TopOrder1 << 1
        TopOrder3 = TopOrder2 << 1
        IsContainer = TopOrder3 << 1
        IsStackable = IsContainer << 1
        IsCorpse = IsStackable << 1
        IsUsable = IsCorpse << 1
        IsRune = IsUsable << 1
        IsWritable = IsRune << 1
        IsReadable = IsWritable << 1
        IsFluidContainer = IsReadable << 1
        IsSplash = IsFluidContainer << 1
        Blocking = IsSplash << 1
        IsImmovable = Blocking << 1
        BlocksMissiles = IsImmovable << 1
        BlocksPath = BlocksMissiles << 1
        IsPickupable = BlocksPath << 1
        IsHangable = IsPickupable << 1
        IsHangableHorizontally = IsHangable << 1
        IsHangableVertically = IsHangableHorizontally << 1
        IsRotatable = IsHangableVertically << 1
        IsLightSource = IsRotatable << 1
        CausesFloorChange = IsLightSource << 1
        HasShift = CausesFloorChange << 1
        HasHeight = HasShift << 1
        IsLayer = HasHeight << 1
        IsIdleAnimation = IsLayer << 1
        HasAutoMapColor = IsIdleAnimation << 1
        HasLensHelp = HasAutoMapColor << 1
        IsGround = HasLensHelp << 1
    End Enum

    Enum ObjectLensHelp As Integer
        BookOrScroll = &H456
        Sign = &H457
        StonePile = &H456
        Trash = &H455
        Depot = &H454
        Mailbox = &H453
        Stairs = &H452
        SpecialDoor = &H451
        Door = &H450
        Lever = &H44F
        DungeonFloor = &H44E
        SewerGrates = &H44D
        Ladder = &H44C
        Container = &H44B



    End Enum

    <Flags()> Enum ObjectKind
        None = &H0
        Equipment = &H1
        Helmet = Equipment << 1
        Armor = Helmet << 1
        Leg = Armor << 1
        Footwear = Leg << 1
        Shield = Footwear << 1
        SingleHandedWeapon = Shield << 1 'for rods and wands too
        DoubleHandedWeapon = SingleHandedWeapon << 1 'for bows/xbows too
        Ammunition = DoubleHandedWeapon << 1 'arros & bolts
        RangedWeapon = Ammunition << 1 'spears, snowballs, throwing stars, etc, bows/xbows included
        Tool = RangedWeapon << 1 'rope, shovel, light shovel, pick, etc
        Valuable = Tool << 1 'gems, creature products, books, creature products, quest items, etc
        Ring = Valuable << 1
        Neck = Ring << 1 'amulets and necklaces, scarf
        Food = Neck << 1
    End Enum
#End Region

#Region " Structures "
    Structure ObjectDefinition
        Dim ItemID As UInt32
        Dim Name As String
        Dim Kind As ObjectKind

        Sub New(ByVal ItemID As UInt32, ByVal Name As String, ByVal Kind As ObjectKind)
            Me.ItemID = ItemID
            Me.Name = Name
            Me.Kind = Kind
        End Sub
    End Structure
#End Region

#Region " Properties "

    Property Flags(ByVal ItemID As Integer) As ObjectFlags
    Property WalkSpeed(ByVal ItemID As Integer) As Integer
    Property TextLimit(ByVal ItemID As Integer) As Integer
    Property LightRadius(ByVal ItemID As Integer) As Integer
    Property LightColor(ByVal ItemID As Integer) As Integer
    Property Heighted(ByVal ItemID As Integer) As Integer
    Property AutoMapColor(ByVal ItemID As Integer) As Integer
    Property LensHelp(ByVal ItemID As Integer) As ObjectLensHelp
    ReadOnly Property MinimumItemID() As Integer
    ReadOnly Property MaximumItemID() As Integer
    Property Name(ByVal ItemID As Integer) As String
    Property Kind(ByVal ItemID As Integer) As IObjects.ObjectKind
    ReadOnly Property ID(ByVal Name As String) As Integer

#End Region

#Region " Functions "

    Function HasExtraByte(ByVal ItemID As Integer) As Boolean
    Function HasFlags(ByVal ItemID As Integer, ByVal Flags As ObjectFlags) As Boolean
    Function IsKind(ByVal ItemID As Integer, ByVal Kind As IObjects.ObjectKind) As Boolean
#End Region


End Interface
