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

Public Interface IKernel

#Region " Properties "
    ReadOnly Property Client() As ITibia
    ReadOnly Property Proxy() As IProxy
    ReadOnly Property Spells() As ISpells
    ReadOnly Property CommandParser() As ICommandParser
    ReadOnly Property IrcClient() As IIrcClient
#End Region

#Region " Methods "
    Function NewBattlelist() As IBattlelist
    Function NewBattlelist(ByVal SE As IBattlelist.SpecialEntity) As IBattlelist
    Function NewBattlelist(ByVal Position As Integer) As IBattlelist

    Function NewContainer() As IContainer

    Sub ConsoleRead(ByVal strString As String)
    Sub ConsoleWrite(ByVal strString As String)
    Sub ConsoleError(ByVal strString As String)
#End Region

End Interface
