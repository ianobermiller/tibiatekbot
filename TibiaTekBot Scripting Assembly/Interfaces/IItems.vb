﻿'    Copyright (C) 2007 TibiaTek Development Team
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

Public Interface IItems
#Region " Enumerations "

    <Flags()> Enum ItemKind
        Unknown = &H0 'void? xd, any item that doesnt have any category
        Equipment = &H1
        Helmet = &H2
        Armor = &H4
        Leg = &H8
        Footwear = &H10
        Shield = &H20
        SingleHandedWeapon = &H40 'for rods and wands too
        DoubleHandedWeapon = &H80 'for bows/xbows too
        Ammunition = &H100 'arros & bolts
        RangedWeapon = &H200 'spears, snowballs, throwing stars, etc
        Tool = &H400 'rope, shovel, light shovel, pick, etc
        Valuable = &H800 'gems, creature products, books, etc
        Ring = &H1000
        Neck = &H2000 'amulets and necklaces, scarf
        Food = &H4000
    End Enum

#End Region

#Region " Structures "


    Structure ItemDefinition
        Dim Name As String
        Dim ItemID As UShort
        Dim Kind As ItemKind

        Sub New(ByVal Name As String, ByVal Type As ItemKind, ByVal ItemID As Integer)
            Me.Name = Name
            Me.Kind = Type
            Me.ItemID = ItemID
        End Sub

    End Structure

#End Region

#Region " Properties "
    ReadOnly Property ItemsList As List(of ItemDefinition) 
#End Region

#Region " Methods "
    'Function IsRune(ByVal ID As UShort) As Boolean
    Function IsRangedWeapon(ByVal ID As UShort) As Boolean
    Function IsNeck(ByVal ID As UShort) As Boolean
    Function IsRing(ByVal ID As UShort) As Boolean
    Function IsFood(ByVal ID As Integer) As Boolean
    Function IsAmmunition(ByVal ID As UShort) As Boolean
    Function GetItemKind(ByVal ID As Integer) As IItems.ItemKind
    Function GetItemName(ByVal ID As Integer) As String
    Function GetItemID(ByVal Name As String) As Integer
    Sub Refresh()
#End Region
End Interface
