Imports System.xml

Public Module VersionsModule

    Public Structure VersionInfo
        Dim Name As String
        Dim ConstantsFile As String
        Dim InGame As Integer
        Dim CharacterListBegin As Integer
        Dim CharacterListDist As Integer
        Dim CharacterSelectionIndex As Integer
        Dim CodeCave As Integer
        Dim DllFile As String
    End Structure

    Public Class VersionsClass

        Private Versions As New Dictionary(Of String, VersionInfo)

        Public ReadOnly Property Items(ByVal Name As String) As VersionInfo
            Get
                Return Versions(Name)
            End Get
        End Property

        Public ReadOnly Property Contains(ByVal Name As String)
            Get
                Return Versions.ContainsKey(Name)
            End Get
        End Property

        Public Sub New()
            Load()
        End Sub

        Private Sub Load()
            Try
                Dim Document As New XmlDocument()
                Document.Load(GetConfigurationDirectory() & "\Versions.xml")
                Dim VersionsElement As XmlElement = Document.Item("Versions")
                For Each VersionElement As XmlElement In VersionsElement
                    Dim V As New VersionInfo
                    V.Name = VersionElement.GetAttribute("Name")
                    V.ConstantsFile = VersionElement.GetAttribute("ConstantsFile")
                    V.InGame = CInt(VersionElement.GetAttribute("InGame"))
                    V.CharacterListBegin = CInt(VersionElement.GetAttribute("CharacterListBegin"))
                    V.CharacterListDist = CInt(VersionElement.GetAttribute("CharacterListDist"))
                    V.CharacterSelectionIndex = CInt(VersionElement.GetAttribute("CharacterSelectionIndex"))
                    V.CodeCave = CInt(VersionElement.GetAttribute("CodeCave"))
                    V.DllFile = VersionElement.GetAttribute("DllFile")
                    Versions.Add(V.Name, V)
                Next
            Catch
                MessageBox.Show("Unable to load Versions.xml. Re-installing the application my solve this problem.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub
    End Class

End Module
