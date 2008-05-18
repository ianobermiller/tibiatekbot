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

Public Interface IPacket

#Region " Properties "
    ReadOnly Property GetBytes() As Byte()
    Property GetByte(ByVal Offset As UInteger) As Byte
#End Region

#Region " Methods "
    Sub AddString(ByVal str As String)
    Sub AddByte(ByVal Value As Byte)
    Sub AddWord(ByVal Value As UInt16)
    Sub AddDWord(ByVal Value As UInt32)
    Sub AddDouble(ByVal Value As Double)
    Sub AddLocation(ByVal Location As ITibia.LocationDefinition)
    Function ToString() As String
    Function ToBase64String() As String
#End Region

End Interface