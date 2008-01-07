'    Copyright (C) 2007 TibiaTek Development Team
'
'    This file is part of TibiaTek Bot.
'
'    TibiaTek Bot is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General  License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    TibiaTek Bot is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General  License for more details.
'
'    You should have received a copy of the GNU General  License
'    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt
'    or write to the Free Software Foundation, 59 Temple Place - Suite 330,
'    Boston, MA 02111-1307, USA.

Public Interface IPipePacketBuilder
    Inherits IPacketBuilder

#Region " Methods "
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As UInteger)
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As UShort)
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As Byte)
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As String)
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As Integer)
    Sub SetConstant(ByVal ConstantName As String, ByVal Value As Double)
    Sub HookWndProc(ByVal Hook As Boolean)
#End Region

End Interface


