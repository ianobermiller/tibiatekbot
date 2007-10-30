Imports System.xml

Public Class frmConstantsEditor

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim xmlDocument As New XmlDocument
            Dim xmlDeclaration As XmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "us-ascii", "")
            xmlDocument.AppendChild(xmlDeclaration)
            Dim xmlConstants As XmlElement = xmlDocument.CreateElement("Constants")
            For Each Row As DataGridViewRow In DataGrid.Rows
                If String.IsNullOrEmpty(Row.Cells(0).Value) Then Continue For
                Dim xmlConst As XmlElement = xmlDocument.CreateElement("Const")
                Dim xmlName As XmlAttribute = xmlDocument.CreateAttribute("Name")
                xmlName.Value = Row.Cells(0).Value
                Dim xmlValue As XmlAttribute = xmlDocument.CreateAttribute("Value")
                xmlValue.Value = Row.Cells(1).Value
                xmlConst.Attributes.Append(xmlName)
                xmlConst.Attributes.Append(xmlValue)
                xmlConstants.AppendChild(xmlConst)
            Next
            xmlDocument.AppendChild(xmlConstants)
            xmlDocument.Save(GetConfigurationDirectory() + "\Constants.xml")
            LoadConstants()
            Consts.LoadConstants()
            Core.ConsoleWrite("New Constants Loaded.")
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Hide()
    End Sub

    Private Sub LoadConstants()
        DataGrid.SuspendLayout()
        Try
            DataGrid.Rows.Clear()
            Dim Document As New XmlDocument
            Document.Load(GetConfigurationDirectory() + "\Constants.xml")
            Dim Value As String = ""
            Dim Name As String = ""
            For Each Node As XmlNode In Document.Item("Constants")
                Name = Node.Attributes("Name").Value
                Value = Node.Attributes("Value").Value
                DataGrid.Rows.Add(Name, Value)
            Next
        Catch Ex As Exception
            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DataGrid.ResumeLayout()
        End Try
    End Sub

    Private Sub frmConstantsEditor_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        LoadConstants()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Save_Click(Nothing, Nothing)
        Hide()
    End Sub


    Private Sub DataGrid_UserDeletingRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGrid.UserDeletingRow
        If MessageBox.Show("Are you sure you want to delete this row? TibiaTek Bot may stop working properly!", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmConstantsEditor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class