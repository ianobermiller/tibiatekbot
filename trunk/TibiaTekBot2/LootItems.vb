Imports System.xml
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

        Public ReadOnly Property GetItemsIDs(Optional ByVal ContainerIndex As Integer = Integer.MaxValue) As ContainerItemDefinition()
            Get
                Try
                    Dim R As New List(Of ContainerItemDefinition)
                    Dim ItemID As UShort = 0
                    For Each Item As LootItemDefinition In Items.Values
                        If ContainerIndex < Integer.MaxValue AndAlso Item.GetContainerIndex <> ContainerIndex Then Continue For
                        ItemID = Item.GetID
                        Dim Count As Integer = 0
                        If DatInfo.GetInfo(ItemID).HasExtraByte Then
                            Count = 100
                        Else
                            Count = 0
                        End If
                        R.Add(New ContainerItemDefinition(ItemID, Count))
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
            Save()
        End Sub

        Public Function Remove(ByVal ID As Integer) As Boolean
            Try
                If Items.ContainsKey(ID) Then
                    Items.Remove(ID)
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
                Dim StarBag As UShort = Definitions.GetItemID("Star Bag")
                Dim TreasureChest As UShort = Definitions.GetItemID("Treasure Chest With Gold")
                Dim BotanistContainer As UShort = Definitions.GetItemID("Botanist's Container")
                Dim GoldenBP As UShort = Definitions.GetItemID("Golden Backpack")
                Dim GreenBP As UShort = Definitions.GetItemID("Green Backpack")
                Dim RedBP As UShort = Definitions.GetItemID("Red Backpack")
                Dim BlueBP As UShort = Definitions.GetItemID("Blue Backpack")
                Dim PurpleBP As UShort = Definitions.GetItemID("Purple Backpack")
                Dim GrayBP As UShort = Definitions.GetItemID("Gray Backpack")
                Dim JungleBP As UShort = Definitions.GetItemID("Jungle Backpack")
                Dim BrownBP As UShort = Definitions.GetItemID("Brown Backpack")
                Dim StarBP As UShort = Definitions.GetItemID("Star Backpack")
                Dim PirateBP As UShort = Definitions.GetItemID("Pirate Backpack")
                Dim ItemsList() As ContainerItemDefinition = { _
                    New ContainerItemDefinition(TreasureChest), _
                    New ContainerItemDefinition(BotanistContainer), _
                    New ContainerItemDefinition(JungleBP), _
                    New ContainerItemDefinition(PirateBP), _
                    New ContainerItemDefinition(StarBP), _
                    New ContainerItemDefinition(GoldenBP), _
                    New ContainerItemDefinition(GreenBP), _
                    New ContainerItemDefinition(RedBP), _
                    New ContainerItemDefinition(BlueBP), _
                    New ContainerItemDefinition(PurpleBP), _
                    New ContainerItemDefinition(GrayBP), _
                    New ContainerItemDefinition(BrownBP)}
                Core.LooterCurrentCategory = 0
                Core.Proxy.SendPacketToClient(CreateContainer(StarBag, &HF, "Loot Categories", &H24, ItemsList, False))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

    End Class

End Module
