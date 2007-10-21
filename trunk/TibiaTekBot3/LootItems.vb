Imports System.xml
Public Module LootItemsModule

    Public Class LootItems
        Public Items As New Dictionary(Of UShort, LootItemDefinition)

        Public ReadOnly Property GetCount(Optional ByVal Truncate As Boolean = False) As Integer
            Get
                If Items.Count > Integer.MaxValue AndAlso Truncate Then
                    Return Integer.MaxValue
                Else
                    Return Items.Count
                End If
            End Get
        End Property

        Public ReadOnly Property GetItemsIDs() As ContainerItemDefinition()
            Get
                Dim R As New List(Of ContainerItemDefinition)
                Dim ItemID As UShort = 0
                For Each Item As LootItemDefinition In Items.Values
                    ItemID = Item.GetID
                    Dim Count As Integer = 0
                    If Core.DatInfo.GetInfo(ItemID).HasExtraByte Then
                        Count = 100
                    Else
                        Count = 0
                    End If
                    R.Add(New ContainerItemDefinition(ItemID, Count))
                Next
                Return R.ToArray
            End Get
        End Property

        Public Structure LootItemDefinition
            Public ID As UShort

            Sub New(ByVal ID As UShort) ', ByVal Capacity As Integer)
                Me.ID = ID
            End Sub

            Public ReadOnly Property GetID() As UShort
                Get
                    Return ID
                End Get
            End Property

        End Structure

        Sub New()
            Load()
        End Sub

        Public Sub Load()
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
                    If Items.ContainsKey(ID) Then Continue For
                    Items.Add(ID, New LootItemDefinition(ID))
                Catch
                End Try
            Next
        End Sub

        Protected Overrides Sub Finalize()
            Save()
        End Sub

        Public Function Remove(ByVal ID As UShort) As Boolean
            If Items.ContainsKey(ID) Then
                Items.Remove(ID)
                Return True
            End If
            Return False
        End Function


        Public Function Add(ByVal NewLootItem As LootItemDefinition) As Boolean
            If Not Items.ContainsKey(NewLootItem.GetID) Then
                Items.Add(NewLootItem.GetID, NewLootItem)
                Return True
            End If
            Return False
        End Function

        Public Sub Save()
            Dim Document As New XmlDocument
            Dim xmlLootItems As XmlElement = Document.CreateElement("LootItems")
            For Each LootItem As LootItemDefinition In Items.Values
                Dim xmlItem As XmlElement = Document.CreateElement("Item")
                Dim xmlID As XmlAttribute = Document.CreateAttribute("ID")
                xmlID.InnerText = LootItem.GetID
                xmlItem.Attributes.Append(xmlID)
                xmlLootItems.AppendChild(xmlItem)
            Next
            Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
            Document.AppendChild(Declaration)
            Document.AppendChild(xmlLootItems)
            Document.Save(GetConfigurationDirectory() & "\LootItems.xml")
        End Sub

        Public Function Replace(ByVal ID As UShort, ByVal NewLootItems As LootItemDefinition) As Boolean
            If Items.ContainsKey(ID) Then
                Items(ID) = NewLootItems
                Return True
            End If
            Return False
        End Function

        Public Function GetLootItem(ByVal ID As UShort) As LootItemDefinition
            Try
                Return Items(ID)
            Catch
            End Try
            Return Nothing
        End Function

        Public Function IsLootable(ByVal ID As UShort) As Boolean
            Return Items.ContainsKey(ID)
        End Function

        Public Sub ShowLootCategories()
            Dim LootForm As New frmLootItems
            LootForm.Show()
        End Sub

    End Class

End Module
