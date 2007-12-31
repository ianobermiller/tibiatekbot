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
Imports System.Windows, System.IO

Public Class frmConfigEdit

    Private Sub frmConfigEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Data As String = ""
        'Dim DataArray() As String
        Try
            Dim Reader As IO.StreamReader
            Reader = IO.File.OpenText(Kernel.GetProfileDirectory() & "\config.txt")
            Data = Reader.ReadToEnd
            Reader.Close()
            'DataArray = Data.Split(Ret)
            'Configs.Lines = DataArray
            Configs.Text = Data.Replace(vbLf, vbCrLf)
        Catch
            If IO.File.Exists(Kernel.GetProfileDirectory() & "\config.txt") Then
                MessageBox.Show("Unable to load your configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        End Try
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        Try
            Dim Writer As StreamWriter = IO.File.CreateText(Kernel.GetProfileDirectory() & "\config.txt")
            Writer.Write(Configs.Text.Replace(vbCrLf, vbLf))
            Writer.Close()
            Me.DialogResult = Forms.DialogResult.OK
            Me.Close()
        Catch Ex As Exception
            MessageBox.Show("Unable to save your configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = Forms.DialogResult.Abort
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.DialogResult = Forms.DialogResult.Cancel
        Me.Close()
    End Sub

 
End Class