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
Imports System.Windows.Forms

Public Class IrcPacketBuilder
    Implements IIrcPacketBuilder

    Private Packets As New Queue(Of Packet)
    Private _IrcClient As IIrcClient
    Private _AutoSend As Boolean = True

    Private _Channel As String

    Public Sub New(ByVal IrcClient As IIrcClient, ByVal Channel As String, ByVal Destinatary As String, Optional ByVal AutoSend As Boolean = True)
        _IrcClient = IrcClient
        _AutoSend = AutoSend
    End Sub

    Public Overloads Sub Send() Implements IPacketBuilder.Send
        Try
            Send("All")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Overloads Sub Send(ByVal Destinatary As IIrcPacketBuilder.Destinataries) Implements IIrcPacketBuilder.Send
        Try
            While Packets.Count > 0
                Dim P As Packet = Packets.Dequeue()
                If _IrcClient.IsConnected AndAlso _IrcClient.IsHiddenChannelOpened(_Channel) Then
                    _IrcClient.Speak("$" & Destinatary.ToString & "$" & P.ToString & "$", _Channel)
                End If
            End While
        Catch Ex As System.InvalidOperationException
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Overloads Sub Send(ByVal DestinataryExpression As String) Implements IIrcPacketBuilder.Send
        Try
            While Packets.Count > 0
                Dim P As Packet = Packets.Dequeue()
                If _IrcClient.IsConnected AndAlso _IrcClient.IsHiddenChannelOpened(_Channel) Then
                    _IrcClient.Speak("$" & DestinataryExpression & "$" & P.ToString & "$", _Channel)
                End If
            End While
        Catch Ex As System.InvalidOperationException
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
