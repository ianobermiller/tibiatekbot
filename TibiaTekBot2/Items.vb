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

Public Module ItemsModule

    Public Class Items
        Implements IItems

        Public _ItemsList As New List(Of IItems.ItemDefinition)

        Public Sub New()
            LoadItems()
        End Sub

        Public ReadOnly Property ItemsList() As List(Of IItems.ItemDefinition) Implements IItems.ItemsList
            Get
                Return _ItemsList
            End Get
        End Property

        Public Function GetItemKind(ByVal ID As Integer) As IItems.ItemKind Implements IItems.GetItemKind
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID Then Return Item.Kind
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub Refresh() Implements IItems.Refresh
            LoadItems()
        End Sub

        Public Function GetItemName(ByVal ID As Integer) As String Implements IItems.GetItemName
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID Then Return Item.Name
                Next
                Return "Unknown"
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetItemID(ByVal Name As String) As Integer Implements IItems.GetItemID
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If String.Compare(Item.Name, Name, True) = 0 Then
                        Return Item.ItemID
                    End If
                Next
                Return 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub LoadItems()
            Dim Name As String = ""
            Dim Kind As IItems.ItemKind = IItems.ItemKind.Unknown
            Dim TempStr As String = ""
            Dim ID As UShort = 0
            Dim KindStrArray() As String
            Dim Document As New XmlDocument
            _ItemsList.Clear()
            Try
                Document.Load(GetConfigurationDirectory() & "\Items.xml")
                For Each Node As XmlElement In Document.Item("Items")
                    Name = Node.GetAttribute("Name")
                    TempStr = Node.GetAttribute("ID")
                    If Not String.IsNullOrEmpty(TempStr) AndAlso TempStr.Chars(0) = "H" Then TempStr = "&" & TempStr
                    ID = CUShort(TempStr)
                    KindStrArray = Node.GetAttribute("Kind").Split(",")
                    Kind = IItems.ItemKind.Unknown
                    For Each KindStr As String In KindStrArray
                        If String.IsNullOrEmpty(KindStr) Then Continue For
                        Select Case KindStr
                            Case "Unknown"
                                Kind = Kind Or IItems.ItemKind.Unknown
                            Case "Equipment"
                                Kind = Kind Or IItems.ItemKind.Equipment
                            Case "Helmet"
                                Kind = Kind Or IItems.ItemKind.Helmet
                            Case "Armor"
                                Kind = Kind Or IItems.ItemKind.Armor
                            Case "Leg"
                                Kind = Kind Or IItems.ItemKind.Leg
                            Case "Footwear"
                                Kind = Kind Or IItems.ItemKind.Footwear
                            Case "Shield"
                                Kind = Kind Or IItems.ItemKind.Shield
                            Case "SingleHandedWeapon"
                                Kind = Kind Or IItems.ItemKind.SingleHandedWeapon
                            Case "DoubleHandedWeapon"
                                Kind = Kind Or IItems.ItemKind.DoubleHandedWeapon
                            Case "Ammunition"
                                Kind = Kind Or IItems.ItemKind.Ammunition
                            Case "RangedWeapon"
                                Kind = Kind Or IItems.ItemKind.RangedWeapon
                            Case "Tool"
                                Kind = Kind Or IItems.ItemKind.Tool
                            Case "Valuable"
                                Kind = Kind Or IItems.ItemKind.Valuable
                            Case "Ring"
                                Kind = Kind Or IItems.ItemKind.Ring
                            Case "Neck"
                                Kind = Kind Or IItems.ItemKind.Neck
                            Case "Food"
                                Kind = Kind Or IItems.ItemKind.Food
                            Case Else
                                Throw New Exception("Items.xml has errors. Invalid item kind for item " & Name & ": " & KindStr & ".")
                        End Select
                    Next
                    _ItemsList.Add(New IItems.ItemDefinition(Name, Kind, ID))
                Next
            Catch Ex As Exception
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        'Public Function IsRune(ByVal ID As UShort) As Boolean Implements IItems.IsRune
        '    Try
        '        For Each Item As IItems.ItemDefinition In _ItemsList
        '            If Item.ItemID = ID AndAlso (Item.Kind And IItems.ItemKind.Rune) Then Return True
        '        Next
        '        Return False
        '    Catch Ex As Exception
        '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End
        '    End Try
        'End Function

        Public Function IsRangedWeapon(ByVal ID As UShort) As Boolean Implements IItems.IsRangedWeapon
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And IItems.ItemKind.RangedWeapon) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsNeck(ByVal ID As UShort) As Boolean Implements IItems.IsNeck
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And IItems.ItemKind.Neck) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsRing(ByVal ID As UShort) As Boolean Implements IItems.IsRing
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And IItems.ItemKind.Ring) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsFood(ByVal ID As Integer) As Boolean Implements IItems.IsFood
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID AndAlso CBool(Item.Kind And IItems.ItemKind.Food) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsAmmunition(ByVal ID As UShort) As Boolean Implements IItems.IsAmmunition
            Try
                For Each Item As IItems.ItemDefinition In _ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And IItems.ItemKind.Ammunition) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

    End Class

End Module
