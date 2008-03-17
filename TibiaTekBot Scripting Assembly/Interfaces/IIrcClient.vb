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

Public Interface IIrcClient

#Region " Structures "
    Structure UserInformation
        Public UserLevel As Integer
    End Structure

    Structure ChannelInformation
        Public Name As String
        Public Topic As String
        Public TopicOwner As String
        Public Users As SortedList(Of String, IIrcClient.UserInformation)
        Public ID As Integer
    End Structure
#End Region

#Region " Events "
    Event ChannelJoin(ByVal Nick As String, ByVal Channel As String)
    Event ChannelSelfJoin(ByVal Channel As String)
    Event ChannelKick(ByVal NickKicker As String, ByVal NickKicked As String, ByVal KickMessage As String, ByVal Channel As String)
    Event ChannelSelfKick(ByVal NickKicker As String, ByVal KickMessage As String, ByVal Channel As String)
    Event NickChange(ByVal UserOldNick As String, ByVal UserNewNick As String)
    Event ChannelPart(ByVal Nick As String, ByVal Channel As String)
    Event ChannelSelfPart(ByVal Channel As String)
    Event QuitIrc(ByVal Nick As String, ByVal Message As String)
    Event ChannelTopicChange(ByVal ChannelInfo As IIrcClient.ChannelInformation)
    Event RawMessage(ByVal RawMessage As String)
    Event Connecting()
    Event Disconnected()
    Event Connected()
    Event ChannelMessage(ByVal Nick As String, ByVal Message As String, ByVal Channel As String)
    Event PrivateMessage(ByVal Nick As String, ByVal Message As String)
    Event EndMOTD()
    Event ChannelError(ByVal Channel As String, ByVal Message As String)
    Event ChannelMode(ByVal Nick As String, ByVal UserMode As String, ByVal Channel As String)
    Event ChannelNamesList()
    Event ChannelAction(ByVal Nick As String, ByVal Action As String, ByVal Channel As String)
    Event ChannelBroadcast(ByVal Nick As String, ByVal Message As String, ByVal Channel As String)
    Event Notice(ByVal Nick As String, ByVal Message As String)
    Event Invite(ByVal Nick As String, ByVal Channel As String)
    Event PacketReceived(ByVal Nick As String, ByVal Channel As String, ByVal Packet As String)
#End Region

#Region " Properties "
    ReadOnly Property IsConnected() As Boolean
    Property Password() As String
    Property User() As String
    Property Invisible() As Boolean
    Property RealName() As String
    Property Server() As String
    Property Port() As Integer
    Property Nick() As String
#End Region

#Region " Methods "
    Function GetUserLevel(ByVal Nickname As String, ByVal Channel As String) As Integer
    Sub Quit(Optional ByVal Reason As String = "Good Bye!")
    Sub ChangeNick(ByVal NewNick As String)
    Sub Part(ByVal Channel As String, Optional ByVal Reason As String = "Good Bye!")
    Sub SendNotice(ByVal Destinatary As String, ByVal Message As String)
    Sub Speak(ByVal Message As String, ByVal Destinatary As String)
    Sub Rejoin(ByVal Channel As String)
    Sub Join(ByVal Channel As String)
    Function Connect() As Boolean
    Sub DoMainThread()
    Sub Disconnect()
    Function IsChannelOpened(ByVal Channel As String) As Boolean
    Function IsHiddenChannelOpened(ByVal Channel As String) As Boolean
#End Region

End Interface
