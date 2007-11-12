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
        Dim DataArray() As String
        Try
            Dim Reader As IO.StreamReader
            Reader = IO.File.OpenText(Core.GetProfileDirectory() & "\config.txt")
            Data = Reader.ReadToEnd
            Reader.Close()
            DataArray = Data.Split(Ret)
            Configs.Lines = DataArray
        Catch
            If IO.File.Exists(Core.GetProfileDirectory() & "\config.txt") Then
                MsgBox("Couldn't load config.txt", MsgBoxStyle.Critical, "Error!")
                Me.Close()
            Else
            End If
        End Try
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        Try
            Dim Data As String = ""
            For i As Integer = 0 To Configs.Lines.Length - 1
                If i <= Configs.Lines.Length - 2 Then
                    Data += Configs.Lines(i) & Ret
                Else
                    Data += Configs.Lines(i)
                End If
            Next
            Dim Writer As StreamWriter = IO.File.CreateText(Core.GetProfileDirectory() & "\config.txt")
            Writer.Write(Data)
            Writer.Close()
            MsgBox("Your configuration has been saved.", MsgBoxStyle.OkOnly, "Configuration Saved")
            Me.Close()
        Catch Ex As Exception
            MsgBox("Couldn't save your configuration.", MsgBoxStyle.Critical, "Error!")
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class