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
