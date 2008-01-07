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

Public Interface IServerPacketBuilder
    Inherits IPacketBuilder

    Sub PlayerLogout()
    Sub ChangeFightingMode(ByVal FightingMode As ITibia.FightingMode)
    Sub ChangeChasingMode(ByVal ChasingMode As ITibia.ChasingMode)
    Sub ChangeSecureMode(ByVal SecureMode As ITibia.SecureMode)
    Sub ChangeCombatModes(ByVal Fighting As ITibia.FightingMode, ByVal Chasing As ITibia.ChasingMode, ByVal Secure As ITibia.SecureMode)
    Sub ChangeOutfit(ByVal OutfitID As Integer, ByVal HeadColor As Integer, ByVal BodyColor As Integer, ByVal LegsColor As Integer, ByVal FeetColor As Integer, ByVal Addons As Integer)
    Sub Speak(ByVal Message As String, Optional ByVal DefaultMessageType As ITibia.DefaultMessageType = ITibia.DefaultMessageType.Normal)
    Sub Speak(ByVal Destinatary As String, ByVal Message As String, Optional ByVal PrivateMessageType As ITibia.PrivateMessageType = ITibia.PrivateMessageType.Normal)
    Sub Speak(ByVal Message As String, ByVal ChannelID As ITibia.Channel, ByVal ChannelType As ITibia.ChannelMessageType)
    Sub AttackEntity(ByVal EntityID As Int32)
    Sub CharacterTurn(ByVal Direction As IBattlelist.Directions)
    Sub UseObjectWithObjectOnGround(ByVal ItemID As UShort, ByVal Destination As ITibia.LocationDefinition, Optional ByVal TileId As UShort = 0)
    Sub UseObject(ByVal ItemID As Int16, ByVal InventorySlot As ITibia.InventorySlots, ByVal ContainerIndex As Byte)
    Sub UseObject(ByVal Item As IContainer.ContainerItemDefinition, Optional ByVal ContainerIndex As Byte = &HF)
    Sub UseHotkey(ByVal ItemID As Int32, ByVal CharacterID As Int32, Optional ByVal ExtraByte As Integer = 0)
    Sub StopEverything()
    Sub MoveObject(ByVal ItemID As Integer, ByVal Source As ITibia.LocationDefinition, ByVal Destination As ITibia.LocationDefinition, Optional ByVal Count As Byte = &HFF)
    Sub MoveObject(ByVal Item As IContainer.ContainerItemDefinition, ByVal Destination As ITibia.LocationDefinition, Optional ByVal Count As Byte = &HFF)
    Sub UseObject(ByVal ItemID As Integer, ByVal Source As ITibia.LocationDefinition, Optional ByVal ContainerIndex As Byte = 1)

End Interface


