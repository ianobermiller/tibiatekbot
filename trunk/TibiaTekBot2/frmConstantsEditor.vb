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
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        Finally
            DataGrid.ResumeLayout()
        End Try
    End Sub

    Private Sub frmConstantsEditor_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        LoadConstants()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Save_Click(Nothing, Nothing)
            Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub


    Private Sub DataGrid_UserDeletingRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGrid.UserDeletingRow
        Try
            If MessageBox.Show("Are you sure you want to delete this row? TibiaTek Bot may stop working properly!", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Private Sub frmConstantsEditor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub
End Class