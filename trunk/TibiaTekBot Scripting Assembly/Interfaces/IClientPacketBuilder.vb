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

Public Interface IClientPacketBuilder
    Inherits IPacketBuilder

    Sub Speak(ByVal Name As String, ByVal DefaultMessageType As ITibia.DefaultMessageType, ByVal Level As Integer, ByVal Message As String, ByVal X As Integer, ByVal Y As Integer, ByVal Z As Byte)
    Sub SpeakWithBroadcast(ByVal Nick As String, ByVal Message As String)
    Sub Speak(ByVal Destinatary As String, ByVal Level As Integer, ByVal Message As String, Optional ByVal PrivateMessageType As ITibia.PrivateMessageType = ITibia.PrivateMessageType.Normal)
    Sub Speak(ByVal Name As String, ByVal Level As Integer, ByVal ChannelType As ITibia.ChannelMessageType, ByVal Message As String, Optional ByVal Channel As ITibia.Channel = ITibia.Channel.Console)
    Sub SystemMessage(ByVal Type As ITibia.SysMessageType, ByVal Message As String)
    Sub AnimatedText(ByRef Color As ITibia.TextColors, ByRef Loc As ITibia.LocationDefinition, ByRef ShortText As String)
    Sub FYIBox(ByVal Message As String)
    Sub CreateContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, ByVal Name As String, ByVal Size As Byte, ByVal Items() As IContainer.ContainerItemDefinition, Optional ByVal HasParent As Boolean = False)
    Sub OpenPrivate(ByVal PlayerName As String)
    Sub AnimationEffect(ByVal Loc As ITibia.LocationDefinition, ByVal Animation As ITibia.AnimationEffects)
    Sub HouseSpellEdit(ByVal SpellID As Byte, ByVal Time As UInteger, ByVal Content As String)
    Sub OpenChannel(ByVal ChannelName As String, ByVal ChannelID As Integer)
    Sub AddObjectToContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, Optional ByVal Count As Byte = &HFF)
    Sub RemoveObjectFromContainer(ByVal Slot As Byte, ByVal ContainerIndex As Byte)

End Interface


