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

Imports System.Xml, Scripting

Public Class Spells
    Implements ISpells

    Public SpellsList As New List(Of ISpells.SpellDefinition)

    Public Sub New()
        Try
            LoadSpells()
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Public Sub LoadSpells()
        Try
            SpellsList.Clear()
            Dim Document As New XmlDocument
            Document.Load(GetConfigurationDirectory() + "\Spells.xml")
            Dim SpellsElement As XmlElement = Document.Item("Spells")
            For Each SpellElement As XmlElement In SpellsElement
                Dim Spell As ISpells.SpellDefinition
                Spell.Name = SpellElement.GetAttribute("Name")
                Spell.Words = SpellElement.GetAttribute("Words")
                Spell.ManaPoints = SpellElement.GetAttribute("ManaPoints")
                Spell.SoulPoints = SpellElement.GetAttribute("SoulPoints")
                Spell.Kind = System.Enum.Parse(GetType(ISpells.SpellKind), SpellElement.GetAttribute("Kind"))
                SpellsList.Add(Spell)
            Next
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Sub

    Public Function ConjuresMagicalRune(ByVal SpellName As String) As Boolean Implements ISpells.ConjuresMagicalRune
        Try
            For Each Spell As ISpells.SpellDefinition In SpellsList
                If String.Equals(SpellName, Spell.Name, StringComparison.CurrentCultureIgnoreCase) Then
                    Return Spell.Kind = ISpells.SpellKind.Rune
                End If
            Next
            Return False
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Function

    Public Function GetSpellWords(ByVal Name As String) As String Implements ISpells.GetSpellWords
        Try
            For Each Spell As ISpells.SpellDefinition In SpellsList
                If String.Compare(Name, Spell.Name, True) = 0 Then
                    Return Spell.Words
                End If
            Next
            Return ""
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Function

    Public Function GetSpellMana(ByVal Name As String) As UShort Implements ISpells.GetSpellMana
        Try
            For Each Spell As ISpells.SpellDefinition In SpellsList
                If String.Equals(Name, Spell.Name, StringComparison.CurrentCultureIgnoreCase) OrElse _
                   String.Equals(Name, Spell.Words, StringComparison.CurrentCultureIgnoreCase) Then
                    Return Spell.ManaPoints
                End If

            Next
            Return 0
        Catch Ex As Exception
            ShowError(Ex)
            End
        End Try
    End Function

End Class
