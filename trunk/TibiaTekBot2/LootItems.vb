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

Public Module LootItemsModule

    Public Class LootItems
        Private Items As New Dictionary(Of UShort, LootItemDefinition)

        Public ReadOnly Property GetCount(Optional ByVal Truncate As Boolean = False) As Integer
            Get
                Try
                    If Items.Count > Integer.MaxValue AndAlso Truncate Then
                        Return Integer.MaxValue
                    Else
                        Return Items.Count
                    End If
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetBackpackIndex(ByVal ID As Integer, Optional ByVal OpenBps As Integer = -1) As Integer
            Get
                Try

                    Dim BPCount As Integer = 0
                    If OpenBps = -1 Then
                        Dim BP As New Container
                        BPCount = BP.GetBackpackCount
                    Else
                        BPCount = OpenBps
                    End If
                    For Each Item As LootItemDefinition In Items.Values
                        If Item.GetID = ID Then
                            If System.Math.Abs(BPCount - 1) < Item.GetContainerIndex Then 'We don't want to return negative value
                                Return System.Math.Abs(BPCount - 1)
                            Else
                                Return Item.GetContainerIndex
                            End If
                        End If
                    Next
                    Return -1
                Catch ex As Exception
                    MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Get
        End Property

        Public ReadOnly Property GetItemsIDs(Optional ByVal ContainerIndex As Integer = Integer.MaxValue, Optional ByVal Page As Integer = 0) As Scripting.IContainer.ContainerItemDefinition()
            Get
                Try
                    Dim R As New List(Of Scripting.IContainer.ContainerItemDefinition)
                    Dim I As Integer = 0
                    Dim ItemID As UShort = 0

                    For Each Item As LootItemDefinition In Items.Values
                        If ContainerIndex < Integer.MaxValue AndAlso Item.GetContainerIndex <> ContainerIndex Then Continue For
                        ItemID = Item.GetID
                        I += 1
                        Dim Count As Integer = 0
                        If Kernel.Client.Objects.HasExtraByte(ItemID) Then
                            Count = 100
                        Else
                            Count = 0
                        End If
                        If Page = 0 Then 'Do not care about inside backpacks (aka Pages)
                            R.Add(New Scripting.IContainer.ContainerItemDefinition(ItemID, Count))
                        ElseIf I >= (Page - 1) * 35 AndAlso I < Page * 35 Then 'Add Item to the backpack
                            R.Add(New Scripting.IContainer.ContainerItemDefinition(ItemID, Count))
                        ElseIf I = Page * 35 Then 'Time to add backpack
                            R.Add(New Scripting.IContainer.ContainerItemDefinition(Kernel.Client.Objects.ID("Brown Backpack"), 0))
                            Exit For
                        End If
                    Next
                    Return R.ToArray
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Structure LootItemDefinition
            Private ID As UShort
            Private ContainerIndex As Integer

            Sub New(ByVal ID As UShort, ByVal ContainerIndex As Integer) ', ByVal Capacity As Integer)
                Me.ID = ID
                Me.ContainerIndex = ContainerIndex
            End Sub

            Public ReadOnly Property GetID() As UShort
                Get
                    Return ID
                End Get
            End Property

            Public ReadOnly Property GetContainerIndex() As Integer
                Get
                    Return ContainerIndex
                End Get
            End Property

        End Structure

        Sub New()
            Load()
        End Sub

        Public Sub Load()
            Try
                Dim Document As New XmlDocument
                Document.Load(GetConfigurationDirectory() & "\LootItems.xml")
                Dim Name As String = ""
                Dim ID As UShort = 0
                Dim ContainerIndex As Integer = 0
                Dim TempStr As String = ""
                Items.Clear()
                For Each Element As XmlElement In Document.Item("LootItems")
                    Try
                        TempStr = Element.GetAttribute("ID")
                        If Not String.IsNullOrEmpty(TempStr) AndAlso TempStr.Chars(0) = "H" Then TempStr = "&" + TempStr
                        ID = CUShort(TempStr)
                        TempStr = Element.GetAttribute("ContainerIndex")
                        If Not String.IsNullOrEmpty(TempStr) AndAlso TempStr.Chars(0) = "H" Then TempStr = "&" + TempStr
                        ContainerIndex = CInt(TempStr)
                        If Items.ContainsKey(ID) Then Continue For
                        Items.Add(ID, New LootItemDefinition(ID, ContainerIndex))
                    Catch
                    End Try
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Protected Overrides Sub Finalize()
            'Save()
        End Sub

        Public Function Remove(ByVal ID As Integer) As Boolean
            Try
                If Items.ContainsKey(ID) Then
                    Items.Remove(ID)
                    Save()
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function


        Public Function Add(ByVal NewLootItem As LootItemDefinition) As Boolean
            Try
                If Not Items.ContainsKey(NewLootItem.GetID) Then
                    Items.Add(NewLootItem.GetID, NewLootItem)
                    Save()
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub Save()
            Try
                Dim Document As New XmlDocument
                Dim xmlLootItems As XmlElement = Document.CreateElement("LootItems")
                For Each LootItem As LootItemDefinition In Items.Values
                    Dim xmlItem As XmlElement = Document.CreateElement("Item")
                    Dim xmlID As XmlAttribute = Document.CreateAttribute("ID")
                    xmlID.InnerText = LootItem.GetID
                    Dim xmlContainerIndex As XmlAttribute = Document.CreateAttribute("ContainerIndex")
                    xmlContainerIndex.InnerText = LootItem.GetContainerIndex
                    xmlItem.Attributes.Append(xmlID)
                    xmlItem.Attributes.Append(xmlContainerIndex)
                    xmlLootItems.AppendChild(xmlItem)
                Next
                Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
                Document.AppendChild(Declaration)
                Dim xmlComment As XmlComment = Document.CreateComment(GNUGPLStatement)
                Document.AppendChild(xmlComment)
                Document.AppendChild(xmlLootItems)
                Document.Save(GetConfigurationDirectory() & "\LootItems.xml")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function Replace(ByVal ID As Integer, ByVal NewLootItems As LootItemDefinition) As Boolean
            Try
                If Items.ContainsKey(ID) Then
                    Items(ID) = NewLootItems
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetLootItem(ByVal ID As Integer) As LootItemDefinition
            Try
                Return Items(ID)
            Catch
            End Try
            Return Nothing
        End Function

        Public Function IsLootable(ByVal ID As Integer) As Boolean
            Try
                Return Items.ContainsKey(ID)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub ShowLootCategories()
            Try
                Dim StarBag As UShort = Kernel.Client.Objects.ID("Star Bag")
                Dim TreasureChest As UShort = Kernel.Client.Objects.ID("Treasure Chest With Gold")
                Dim BotanistContainer As UShort = Kernel.Client.Objects.ID("Botanist's Container")
                Dim GoldenBP As UShort = Kernel.Client.Objects.ID("Golden Backpack")
                Dim GreenBP As UShort = Kernel.Client.Objects.ID("Green Backpack")
                Dim RedBP As UShort = Kernel.Client.Objects.ID("Red Backpack")
                Dim BlueBP As UShort = Kernel.Client.Objects.ID("Blue Backpack")
                Dim PurpleBP As UShort = Kernel.Client.Objects.ID("Purple Backpack")
                Dim GrayBP As UShort = Kernel.Client.Objects.ID("Gray Backpack")
                Dim JungleBP As UShort = Kernel.Client.Objects.ID("Jungle Backpack")
                Dim BrownBP As UShort = Kernel.Client.Objects.ID("Brown Backpack")
                Dim StarBP As UShort = Kernel.Client.Objects.ID("Star Backpack")
                Dim PirateBP As UShort = Kernel.Client.Objects.ID("Pirate Backpack")
                Dim ItemsList() As Scripting.IContainer.ContainerItemDefinition = { _
                    New Scripting.IContainer.ContainerItemDefinition(TreasureChest), _
                    New Scripting.IContainer.ContainerItemDefinition(BotanistContainer), _
                    New Scripting.IContainer.ContainerItemDefinition(JungleBP), _
                    New Scripting.IContainer.ContainerItemDefinition(PirateBP), _
                    New Scripting.IContainer.ContainerItemDefinition(StarBP), _
                    New Scripting.IContainer.ContainerItemDefinition(GoldenBP), _
                    New Scripting.IContainer.ContainerItemDefinition(GreenBP), _
                    New Scripting.IContainer.ContainerItemDefinition(RedBP), _
                    New Scripting.IContainer.ContainerItemDefinition(BlueBP), _
                    New Scripting.IContainer.ContainerItemDefinition(PurpleBP), _
                    New Scripting.IContainer.ContainerItemDefinition(GrayBP), _
                    New Scripting.IContainer.ContainerItemDefinition(BrownBP)}
                Kernel.LooterCurrentCategory = 0
                Dim CP As New ClientPacketBuilder(Kernel.Proxy)
                CP.CreateContainer(StarBag, &HF, "Loot Categories", &H24, ItemsList, False)
                'Core.Proxy.SendPacketToClient(CreateContainer(StarBag, &HF, "Loot Categories", &H24, ItemsList, False))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

    End Class

End Module
