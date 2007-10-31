Imports System.Windows.Forms, TibiaTekBot.CoreModule, System.Xml, System.Text.RegularExpressions

Public Class frmLoginSelectDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim MatchObj As Match = Regex.Match(loginServers.Text, "^([^:]+):(\d+)$")
            If MatchObj.Success Then
                Core.LoginServer = MatchObj.Groups(1).Value
                Core.LoginPort = CInt(MatchObj.Groups(2).Value)
                Dim xmlDocument As New System.Xml.XmlDocument()
                xmlDocument.Load(GetConfigurationDirectory() & "\Data.xml")

                Dim Add As Boolean = True
                Dim Node As XmlNode = Nothing
                For Each Node In xmlDocument.GetElementsByTagName("Address")
                    If String.Compare(Node.InnerText, loginServers.Text) = 0 Then
                        Add = False
                        Exit For
                    End If
                Next
                If Add Then
                    Dim xmlNewNode As XmlElement = xmlDocument.CreateElement("Address")
                    xmlNewNode.InnerText = loginServers.Text
                    xmlDocument.Item("Client").Item("Addresses").InsertBefore(xmlNewNode, xmlDocument.Item("Client").Item("Addresses").FirstChild)
                ElseIf Not Node Is Nothing Then
                    Dim xmlNewNode As XmlElement = xmlDocument.CreateElement("Address")
                    xmlNewNode.InnerText = loginServers.Text
                    xmlDocument.Item("Client").Item("Addresses").InsertBefore(Node.Clone(), xmlDocument.Item("Client").Item("Addresses").FirstChild)
                    xmlDocument.Item("Client").Item("Addresses").RemoveChild(Node)

                End If
                xmlDocument.Save(GetConfigurationDirectory() & "\Data.xml")
            Else
                Dim Result As DialogResult
                Result = MessageBox.Show("Entry is not of the form hostname:port.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                If Result = Windows.Forms.DialogResult.Cancel Then
                    End
                Else
                    Exit Sub
                End If
            End If
            Core.IsPrivateServer = privateServer.Checked
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            End
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmLoginSelectDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim xmlDocument As New System.Xml.XmlDocument()
            xmlDocument.Load(GetConfigurationDirectory() & "\Data.xml")
            For Each Node As XmlNode In xmlDocument.Item("Client").Item("Addresses")
                loginServers.Items.Add(Node.InnerText)
            Next
            If loginServers.Items.Count > 0 Then loginServers.SelectedIndex = 0
        Catch Ex As Exception
            MessageBox.Show("Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source, Ex.TargetSite.Name, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

End Class
