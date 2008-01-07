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

Public Interface ISpells

#Region " Enumerations "
    Enum SpellKind As Integer
        Rune
        Food
        Ammunition
        Support
        Offensive
        Healing
        Incantation
    End Enum
#End Region

#Region " Structures "
    Structure SpellDefinition
        Dim Name As String
        Dim Words As String
        Dim ManaPoints As UShort
        Dim SoulPoints As Integer
        Dim Kind As SpellKind
    End Structure
#End Region

#Region " Methods "
    Function ConjuresMagicalRune(ByVal SpellName As String) As Boolean
    Function GetSpellWords(ByVal Name As String) As String
    Function GetSpellMana(ByVal Name As String) As UShort
#End Region

End Interface
