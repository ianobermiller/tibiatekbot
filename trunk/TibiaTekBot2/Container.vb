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


Public Module ContainerModule

    Public Structure ContainerItemDefinition
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
                End
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
                End
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
                End
            End Try
        End Sub

        Property Location() As LocationDefinition
            Get
                Try

                    Dim Loc As New LocationDefinition
                    Loc.X = &HFFFF
                    Loc.Y = &H40 + ContainerIndex
                    Loc.Z = Slot
                    Return Loc
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewValue As LocationDefinition)
                Try
                    If NewValue.Y >= &H40 AndAlso NewValue.Y <= &H4F Then
                        ContainerIndex = NewValue.Y - &H40
                    Else
                        ContainerIndex = NewValue.Y
                    End If
                    Slot = NewValue.Z
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

    End Structure

    Public Class Container
        Private ContainerIndex As Integer = 0
        Private ContainerItemCount As Integer = 0
        Private ContainerIsOpened As Boolean = False

        Public Sub New()
            ContainerIndex = 0
        End Sub

        Public Function Reset() As Boolean
            Try
                ContainerIndex = 0
                Dim IsOpened As Integer = 0
                Core.ReadMemory(Consts.ptrFirstContainer, IsOpened, 1)
                If CBool(IsOpened) Then
                    ContainerItemCount = Me.GetItemCount()
                    Me.ContainerIsOpened = True
                End If
                Return Me.ContainerIsOpened
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function JumpToContainer(ByVal NewContainerIndex As Integer) As Boolean
            Try
                If NewContainerIndex > Consts.MaxContainers Then NewContainerIndex = Consts.MaxContainers
                If Container.IsOpened(NewContainerIndex) Then
                    Me.ContainerIndex = NewContainerIndex
                    ContainerItemCount = GetItemCount()
                    ContainerIsOpened = True
                    Return True
                Else
                    Return False
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Shared Function FindItem(ByRef Item As ContainerItemDefinition, ByVal ItemID As Integer, Optional ByVal ContainerIndexOffset As Integer = 0, Optional ByVal IndexOffset As Integer = 0, Optional ByVal ContainerIndexMax As Integer = 0, Optional ByVal MinCount As Integer = 0, Optional ByVal MaxCount As Integer = 100) As Boolean
            Try
                Dim mIsOpened As Integer = 0
                Dim mContainerItemCount As Integer = 0
                Dim mItemID As Integer = 0
                Dim mItemCount As Integer = 0
                Dim FirstLoop As Boolean = True
                If ContainerIndexMax = 0 Then ContainerIndexMax = ContainerIndexOffset
                If ContainerIndexMax >= Consts.MaxContainers Then ContainerIndexMax = Consts.MaxContainers - 1
                If ContainerIndexOffset > (Consts.MaxContainers - 1) Then ContainerIndexOffset = Consts.MaxContainers - 1
                For I As Integer = ContainerIndexOffset To ContainerIndexMax
                    Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist), mIsOpened, 1)
                    Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist) + Consts.ContainerItemCountOffset, mContainerItemCount, 1)
                    If CBool(mIsOpened) Then
                        Dim ItemIndexStart As Integer
                        If FirstLoop Then
                            ItemIndexStart = IndexOffset
                            FirstLoop = False
                        Else
                            ItemIndexStart = 0
                        End If
                        If ItemIndexStart >= mContainerItemCount Then Continue For
                        For E As Integer = ItemIndexStart To mContainerItemCount - 1
                            Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist) + (Consts.ItemDist * E) + Consts.ContainerFirstItemOffset, mItemID, 2)
                            Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist) + (Consts.ItemDist * E) + Consts.ContainerFirstItemOffset + Consts.ItemCountOffset, mItemCount, 1)
                            If ItemID = mItemID AndAlso mItemCount >= MinCount AndAlso mItemCount <= MaxCount Then 'found!
                                Item.ID = ItemID
                                Item.Count = mItemCount
                                Item.ContainerIndex = I
                                Item.Slot = E
                                Return True
                            End If
                        Next
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function NextContainer() As Boolean
            Try
                Dim mIsOpened As Integer = 0
                For I As Integer = Me.ContainerIndex + 1 To Consts.MaxContainers - 1
                    Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist), mIsOpened, 1)
                    If CBool(mIsOpened) Then
                        ContainerIndex = I
                        ContainerItemCount = GetItemCount()
                        ContainerIsOpened = True
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
        Public Shared Function ContainerCount() As Integer
            Try
                Dim ContCount As Integer = 0
                Dim Cont As New Container
                Cont.Reset()
                Do
                    If Cont.IsOpened() Then
                        ContCount += 1
                    End If
                Loop While Cont.NextContainer()
                Return ContCount
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Shared Function IsOpened(ByVal Index As Integer) As Boolean
            Try
                Dim mIsOpened As Integer = 0
                Core.ReadMemory(Consts.ptrFirstContainer + (Index * Consts.ContainerDist), mIsOpened, 1)
                Return CBool(mIsOpened)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IsOpened() As Boolean
            Try
                Dim mIsOpened As Integer = 0
                Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist), mIsOpened, 1)
                Return CBool(mIsOpened)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetName() As String
            Get
                Try
                    Dim Name As String = ""
                    Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerNameOffset, Name)
                    Return Name
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function PrevContainer() As Boolean
            Try
                Dim mIsOpened As Integer = 0
                For I As Integer = ContainerIndex To 0 Step -1
                    Core.ReadMemory(Consts.ptrFirstContainer + (I * Consts.ContainerDist), mIsOpened, 1)
                    If CBool(mIsOpened) Then
                        ContainerIndex = I
                        Me.ContainerItemCount = GetItemCount()
                        Me.ContainerIsOpened = True
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Shared Function GetItemCountByItemID(ByVal ItemID As UShort) As Integer
            Try
                Dim Item As ContainerItemDefinition
                Dim Count As Integer = 0
                Dim ContainerItemCount As Integer
                Dim MyC As New Container
                MyC.Reset()
                Do
                    If MyC.IsOpened() Then
                        ContainerItemCount = MyC.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyC.Items(I)
                            If Item.ID = ItemID Then
                                If Item.Count = 0 Then
                                    Count += 1
                                Else
                                    Count += Item.Count
                                End If
                            End If
                        Next
                    End If
                Loop While MyC.NextContainer()
                Return Count
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetItemCount() As Integer
            Get
                Try
                    Dim ItemCount As Integer = 0
                    Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerItemCountOffset, ItemCount, 1)
                    Return CInt(ItemCount)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetContainerSize() As Integer
            Get
                Try
                    Dim Size As Integer = 0
                    Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerSizeOffset, Size, 1)
                    Return CInt(Size)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetContainerID() As Integer
            Get
                Try
                    Dim ID As Integer = 0
                    Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerIDOffset, ID, 4)
                    Return CInt(ID)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Shared Function ContainerHasParent(ByVal CIndex As Byte) As Boolean
            Try
                Dim HasP As Integer = 0
                Core.ReadMemory(Consts.ptrFirstContainer + (CIndex * Consts.ContainerDist) + Consts.ContainerHasParentOffset, HasP, 1)
                Return (HasP = 1)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property HasParent() As Boolean
            Get
                Try
                    Dim HasP As Integer = 0
                    Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerHasParentOffset, HasP, 1)
                    Return (HasP = 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetContainerIndex() As Integer
            Get
                Return ContainerIndex
            End Get
        End Property

        Public ReadOnly Property Items(ByVal Index As Integer) As ContainerItemDefinition
            Get
                Try
                    Dim Item As ContainerItemDefinition
                    Dim ItemID As Integer
                    Dim ItemCount As Integer
                    If Index < Me.ContainerItemCount Then
                        Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerFirstItemOffset + (Index * Consts.ItemDist), ItemID, 4)
                        Core.ReadMemory(Consts.ptrFirstContainer + (ContainerIndex * Consts.ContainerDist) + Consts.ContainerFirstItemOffset + (Index * Consts.ItemDist) + Consts.ItemCountOffset, ItemCount, 1)
                        Item.ID = CUShort(ItemID)
                        Item.Count = CInt(ItemCount)
                        Item.ContainerIndex = CInt(ContainerIndex)
                        Item.Slot = CInt(Index)
                    Else
                        Item.ID = 0
                        Item.Slot = 0
                        Item.ContainerIndex = 0
                        Item.Count = 0
                    End If
                    Return Item
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

    End Class

End Module
