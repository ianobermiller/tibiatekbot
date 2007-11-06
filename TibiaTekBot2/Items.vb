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
'    Boston, MA 02111-1307, USA.Imports System.Math

Imports System.Xml

Public Module ItemsModule

    Public Structure ItemDefinition
        Dim Name As String
        Dim ItemID As UShort
        Dim Kind As ItemKind

        Sub New(ByVal Name As String, ByVal Type As ItemKind, ByVal ItemID As Integer)
            Me.Name = Name
            Me.Kind = Type
            Me.ItemID = ItemID
        End Sub
    End Structure

    Public Class Items
        Public ItemsList As New List(Of ItemDefinition)

        Public Sub New()
            LoadItems()
        End Sub

        Public Function GetItemKind(ByVal ID As Integer) As ItemKind
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID Then Return Item.Kind
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetItemName(ByVal ID As Integer) As String
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID Then Return Item.Name
                Next
                Return "Unknown"
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetItemID(ByVal Name As String) As UShort
            Try
                For Each Item As ItemDefinition In ItemsList
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
            Dim Kind As ItemKind = ItemKind.Unknown
            Dim TempStr As String = ""
            Dim ID As UShort = 0
            Dim KindStrArray() As String
            Dim Document As New XmlDocument
            ItemsList.Clear()
            Try
                Document.Load(GetConfigurationDirectory() & "\Items.xml")
                For Each Node As XmlElement In Document.Item("Items")
                    Name = Node.GetAttribute("Name")
                    TempStr = Node.GetAttribute("ID")
                    If Not String.IsNullOrEmpty(TempStr) AndAlso TempStr.Chars(0) = "H" Then TempStr = "&" & TempStr
                    ID = CUShort(TempStr)
                    KindStrArray = Node.GetAttribute("Kind").Split(",")
                    Kind = ItemKind.Unknown
                    For Each KindStr As String In KindStrArray
                        If String.IsNullOrEmpty(KindStr) Then Continue For
                        Select Case KindStr
                            Case "Unknown"
                                Kind = Kind Or ItemKind.Unknown
                            Case "Equipment"
                                Kind = Kind Or ItemKind.Equipment
                            Case "Helmet"
                                Kind = Kind Or ItemKind.Helmet
                            Case "Armor"
                                Kind = Kind Or ItemKind.Armor
                            Case "Leg"
                                Kind = Kind Or ItemKind.Leg
                            Case "Footwear"
                                Kind = Kind Or ItemKind.Footwear
                            Case "Shield"
                                Kind = Kind Or ItemKind.Shield
                            Case "SingleHandedWeapon"
                                Kind = Kind Or ItemKind.SingleHandedWeapon
                            Case "DoubleHandedWeapon"
                                Kind = Kind Or ItemKind.DoubleHandedWeapon
                            Case "Ammunition"
                                Kind = Kind Or ItemKind.Ammunition
                            Case "Throwable"
                                Kind = Kind Or ItemKind.Throwable
                            Case "Tool"
                                Kind = Kind Or ItemKind.Tool
                            Case "Valuable"
                                Kind = Kind Or ItemKind.Valuable
                            Case "Ring"
                                Kind = Kind Or ItemKind.Ring
                            Case "Neck"
                                Kind = Kind Or ItemKind.Neck
                            Case "Container"
                                Kind = Kind Or ItemKind.Container
                            Case "Food"
                                Kind = Kind Or ItemKind.Food
                            Case "FluidContainer"
                                Kind = Kind Or ItemKind.FluidContainer
                            Case "LightSource"
                                Kind = Kind Or ItemKind.LightSource
                            Case "MagicField"
                                Kind = Kind Or ItemKind.MagicField
                            Case "Door"
                                Kind = Kind Or ItemKind.Door
                            Case "Special"
                                Kind = Kind Or ItemKind.Special
                            Case "RopeSpot"
                                Kind = Kind Or ItemKind.RopeSpot
                            Case "Teleport"
                                Kind = Kind Or ItemKind.Teleport
                            Case "UsableTeleport"
                                Kind = Kind Or ItemKind.UsableTeleport
                            Case "UsableTeleport2"
                                Kind = Kind Or ItemKind.UsableTeleport2
                            Case "BlockedTeleport"
                                Kind = Kind Or ItemKind.BlockedTeleport
                            Case "Blocking"
                                Kind = Kind Or ItemKind.Blocking
                            Case "FullBlocking"
                                Kind = Kind Or ItemKind.FullBlocking
                            Case "Rune"
                                Kind = Kind Or ItemKind.Rune
                            Case Else
                                Throw New Exception("Items.xml has errors. Invalid item kind for item " & Name & ": " & KindStr & ".")
                        End Select
                    Next
                    ItemsList.Add(New ItemDefinition(Name, Kind, ID))
                Next
            Catch Ex As Exception
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function IsRune(ByVal ID As UShort) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And ItemKind.Rune) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsThrowable(ByVal ID As UShort) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And ItemKind.Throwable) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsNeck(ByVal ID As UShort) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And ItemKind.Neck) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsRing(ByVal ID As UShort) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And ItemKind.Ring) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsFood(ByVal ID As Integer) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso CBool(Item.Kind And ItemKind.Food) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsAmmunition(ByVal ID As UShort) As Boolean
            Try
                For Each Item As ItemDefinition In ItemsList
                    If Item.ItemID = ID AndAlso (Item.Kind And ItemKind.Ammunition) Then Return True
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

    End Class

End Module
