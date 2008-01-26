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
    <Flags()> Enum ObjectFlags As Integer
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
        Ladder = &H44C
        Sewer = &H44D
        Door = &H450
        LockedDoor = &H451
        RopeSpot = &H44E
        Switch = &H44F
        Stairs = &H452
        Mailbox = &H453
        Depot = &H454
        Trash = &H455
        Hole = &H456
        HasSpecialDescription = &H457
        [ReadOnly] = &H458
    End Enum
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

#End Region


#Region " Functions "

    Function HasExtraByte(ByVal ItemID As Integer) As Boolean
    Function HasFlags(ByVal ItemID As Integer, ByVal Flags As ObjectFlags) As Boolean

#End Region





 

    '<StructLayout(LayoutKind.Sequential)> _
    'Structure [Object]
    '    Private _Width As UInt32
    '    Private _Height As UInt32
    '    Private _Unknown As UInt32
    '    Private _Layers As UInt32
    '    Private _PatternX As UInt32
    '    Private _PatternY As UInt32
    '    Private _PatternDepth As UInt32
    '    Private _Phase As UInt32
    '    Private _Sprites As UInt32
    '    Private _Flags As ObjectFlags
    '    Private _WalkSpeed As UInt32
    '    Private _TextLimit As UInt32
    '    Private _LightRadius As UInt32
    '    Private _LightColor As UInt32
    '    Private _ShiftX As UInt32
    '    Private _ShiftY As UInt32
    '    Private _Heighted As UInt32
    '    Private _AutoMapColor As UInt32
    '    Private _LensHelp As ObjectLensHelp

    '    Public Sub New(ByVal Width As UInt32, ByVal Height As UInt32, ByVal Layers As UInt32, _
    '                  ByVal PatternX As UInt32, ByVal PatternY As UInt32, _
    '                  ByVal PatternDepth As UInt32, ByVal Phase As UInt32, ByVal Sprites As UInt32, _
    '                  ByVal Flags As UInt32, ByVal WalkSpeed As UInt32, ByVal TextLimit As UInt32, _
    '                  ByVal LightRadius As UInt32, ByVal LightColor As UInt32, _
    '                  ByVal ShiftX As UInt32, ByVal ShiftY As UInt32, _
    '                  ByVal Heighted As UInt32, ByVal AutoMapColor As UInt32, _
    '                  ByVal LensHelp As UInt32)
    '        _Width = Width
    '        _Height = Height
    '        _Layers = Layers
    '        _PatternX = PatternX
    '        _PatternY = PatternY
    '        _PatternDepth = PatternDepth
    '        _Phase = Phase
    '        _Sprites = Sprites
    '        _Flags = Flags
    '        _WalkSpeed = WalkSpeed
    '        _TextLimit = TextLimit
    '    End Sub

    '    ReadOnly Property Width() As UInt32
    '        Get
    '            Return _Width
    '        End Get
    '    End Property

    '    ReadOnly Property Height() As UInt32
    '        Get
    '            Return _Height
    '        End Get
    '    End Property

    '    ReadOnly Property Layers() As UInt32
    '        Get
    '            Return _Layers
    '        End Get
    '    End Property

    '    'ReadOnly Property 
    'End Structure

End Interface
