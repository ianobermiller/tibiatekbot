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

Public Class ServerPacketBuilder
    Implements IPacketBuilder
    Private _Packet As New Packet

    Public Sub AddByte(ByVal Value As Byte) Implements IPacketBuilder.AddByte
        _Packet.AddByte(Value)
    End Sub

    Public Sub AddWord(ByVal Value As UInt16) Implements IPacketBuilder.AddWord
        _Packet.AddWord(Value)
    End Sub

    Public Sub AddDWord(ByVal Value As UInt32) Implements IPacketBuilder.AddDWord
        _Packet.AddDWord(Value)
    End Sub

    Public Sub AddString(ByVal Value As String) Implements IPacketBuilder.AddString
        _Packet.AddString(Value)
    End Sub

    Public Sub AddLocation(ByVal Location As ITibia.LocationDefinition) Implements IPacketBuilder.AddLocation
        _Packet.AddLocation(Location)
    End Sub


End Class
