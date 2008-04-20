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

Imports System.xml
Public Module CreaturesModule

    Public Structure Creature
        Dim Name As String
        Dim Experience As Integer
    End Structure

    Public Class Creatures
        Public Creatures As New Dictionary(Of String, Creature)

        Public Sub New()
            LoadCreatures()
        End Sub

        Public Function LoadCreatures() As Boolean
            Try
                Creatures.Clear()
                Dim Document As New XmlDocument
                Document.Load(GetConfigurationDirectory() & "\Creatures.xml")
                For Each Element As XmlElement In Document.Item("Creatures")
                    Dim NewC As Creature
                    NewC.Name = Element.GetAttribute("Name")
                    NewC.Experience = CInt(Element.GetAttribute("Experience"))
                    Creatures.Add(NewC.Name, NewC)
                Next
                Return True
            Catch
                Return False
            End Try
        End Function
    End Class

End Module
