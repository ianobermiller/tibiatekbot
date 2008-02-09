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

Imports System.Windows.Forms

Public Interface IContainer
#Region " Structures "
    Structure ContainerItemDefinition
        Dim ID As Integer
        Dim Count As Integer
        Dim ContainerIndex As Integer
        Dim Slot As Integer

        Public Sub New(ByVal ID As UShort, ByVal Count As Integer, ByVal ContainerIndex As Integer, ByVal Slot As Integer)
            Try
                Me.ID = ID
                Me.Count = Count
                Me.ContainerIndex = ContainerIndex
                Me.Slot = Slot
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub New(ByVal ID As UShort, ByVal Count As Integer)
            Try
                Me.ID = ID
                Me.Count = Count
                Me.ContainerIndex = 0
                Me.Slot = 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub New(ByVal ID As UShort)
            Try
                Me.ID = ID
                Me.Count = 0
                Me.ContainerIndex = 0
                Me.Slot = 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Property Location() As ITibia.LocationDefinition
            Get
                Try

                    Dim Loc As New ITibia.LocationDefinition
                    Loc.X = &HFFFF
                    Loc.Y = &H40 + ContainerIndex
                    Loc.Z = Slot
                    Return Loc
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Get
            Set(ByVal NewValue As ITibia.LocationDefinition)
                Try
                    If NewValue.Y >= &H40 AndAlso NewValue.Y <= &H4F Then
                        ContainerIndex = NewValue.Y - &H40
                    Else
                        ContainerIndex = NewValue.Y
                    End If
                    Slot = NewValue.Z
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Set
        End Property
    End Structure
#End Region

#Region " Properties "
    ReadOnly Property GetName() As String
    ReadOnly Property GetItemCount() As Integer
    ReadOnly Property GetContainerSize() As Integer
    ReadOnly Property GetContainerID() As Integer
    ReadOnly Property HasParent() As Boolean
    ReadOnly Property GetContainerIndex() As Integer
    ReadOnly Property Items(ByVal Index As Integer) As IContainer.ContainerItemDefinition
    ReadOnly Property GetInventorySlotId(ByVal Slot As ITibia.InventorySlots) As Integer
    ReadOnly Property GetInventorySlotCount(ByVal Slot As ITibia.InventorySlots) As Integer
    ReadOnly Property GetContainerCount() As Integer
#End Region

#Region " Methods "
    Function Reset() As Boolean
    Function JumpToContainer(ByVal NewContainerIndex As Integer) As Boolean
    Function NextContainer() As Boolean
    Function IsOpened() As Boolean
    Function PrevContainer() As Boolean
    Function FindItem(ByRef Item As IContainer.ContainerItemDefinition, ByVal ItemID As Integer, Optional ByVal ContainerIndexOffset As Integer = 0, Optional ByVal IndexOffset As Integer = 0, Optional ByVal ContainerIndexMax As Integer = 0, Optional ByVal MinCount As Integer = 0, Optional ByVal MaxCount As Integer = 100) As Boolean
    Function GetItemCountByItemID(ByVal ItemID As UShort) As Integer
#End Region
End Interface
