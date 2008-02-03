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

Public Class ServerPacketBuilder
    Implements IServerPacketBuilder
    Private Packets As New Queue(Of Packet)
    Private _AutoSend As Boolean = True
    Private _Proxy As IProxy

    Public Property AutoSend() As Boolean
        Get
            Return _AutoSend
        End Get
        Set(ByVal value As Boolean)
            _AutoSend = value
        End Set
    End Property

    Public Sub New(ByRef Proxy As IProxy, Optional ByVal AutoSend As Boolean = True)
        _Proxy = Proxy
        _AutoSend = AutoSend
    End Sub

    Public Sub PlayerLogout() Implements IServerPacketBuilder.PlayerLogout
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H14)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ChangeFightingMode(ByVal FightingMode As ITibia.FightingMode) Implements IServerPacketBuilder.ChangeFightingMode
        Try
            Dim _Packet As New Packet
            Dim SecureMode As ITibia.SecureMode = _Proxy.Client.CharacterSecureMode
            Dim ChasingMode As ITibia.ChasingMode = _Proxy.Client.CharacterChasingMode
            _Packet.AddByte(&HA0)
            _Packet.AddByte(FightingMode)
            _Packet.AddByte(ChasingMode)
            _Packet.AddByte(SecureMode)

            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ChangeChasingMode(ByVal ChasingMode As ITibia.ChasingMode) Implements IServerPacketBuilder.ChangeChasingMode
        Try
            Dim _Packet As New Packet
            Dim FightingMode As ITibia.FightingMode = _Proxy.Client.CharacterFightingMode
            Dim SecureMode As ITibia.SecureMode = _Proxy.Client.CharacterSecureMode
            _Packet.AddByte(&HA0)
            _Packet.AddByte(FightingMode)
            _Packet.AddByte(ChasingMode)
            _Packet.AddByte(SecureMode)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ChangeSecureMode(ByVal SecureMode As ITibia.SecureMode) Implements IServerPacketBuilder.ChangeSecureMode
        Try
            Dim _Packet As New Packet
            Dim FightingMode As ITibia.FightingMode = _Proxy.Client.CharacterFightingMode
            Dim ChasingMode As ITibia.ChasingMode = _Proxy.Client.CharacterChasingMode
            _Packet.AddByte(&HA0)
            _Packet.AddByte(FightingMode)
            _Packet.AddByte(ChasingMode)
            _Packet.AddByte(SecureMode)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ChangeCombatModes(ByVal Fighting As ITibia.FightingMode, ByVal Chasing As ITibia.ChasingMode, ByVal Secure As ITibia.SecureMode) Implements IServerPacketBuilder.ChangeCombatModes
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HA0)
            _Packet.AddByte(Fighting)
            _Packet.AddByte(Chasing)
            _Packet.AddByte(Secure)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ChangeOutfit(ByVal OutfitID As Integer, ByVal HeadColor As Integer, ByVal BodyColor As Integer, ByVal LegsColor As Integer, ByVal FeetColor As Integer, ByVal Addons As Integer) Implements IServerPacketBuilder.ChangeOutfit
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HD3)
            _Packet.AddWord(OutfitID)
            _Packet.AddByte(HeadColor)
            _Packet.AddByte(BodyColor)
            _Packet.AddByte(LegsColor)
            _Packet.AddByte(FeetColor)
            _Packet.AddByte(Addons)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Message As String, Optional ByVal DefaultMessageType As ITibia.DefaultMessageType = ITibia.DefaultMessageType.Normal) Implements IServerPacketBuilder.Speak
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H96)
            _Packet.AddByte(DefaultMessageType)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Destinatary As String, ByVal Message As String, Optional ByVal PrivateMessageType As ITibia.PrivateMessageType = ITibia.PrivateMessageType.Normal) Implements IServerPacketBuilder.Speak
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H96)
            _Packet.AddByte(PrivateMessageType)
            _Packet.AddString(Destinatary)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Message As String, ByVal ChannelID As ITibia.Channel, ByVal ChannelType As ITibia.ChannelMessageType) Implements IServerPacketBuilder.Speak
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H96)
            _Packet.AddByte(ChannelType)
            _Packet.AddWord(ChannelID)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AttackEntity(ByVal EntityID As Int32) Implements IServerPacketBuilder.AttackEntity
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HA1)
            _Packet.AddDWord(EntityID)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CharacterTurn(ByVal Direction As IBattlelist.Directions) Implements IServerPacketBuilder.CharacterTurn
        Try
            If Direction >= IBattlelist.Directions.Up AndAlso Direction <= IBattlelist.Directions.Left Then
                Dim _Packet As New Packet
                _Packet.AddByte(&H6F + Direction)
                Packets.Enqueue(_Packet)
                If _AutoSend Then Send()
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UseObjectWithObjectOnGround(ByVal ItemID As UShort, ByVal Destination As ITibia.LocationDefinition, Optional ByVal TileId As UShort = 0) Implements IServerPacketBuilder.UseObjectWithObjectOnGround
        Try
            Dim _Packet As New Packet
            Dim bytBuffer(1) As Byte
            _Packet.AddByte(&H83)
            _Packet.AddWord(&HFFFF)
            _Packet.AddWord(0)
            _Packet.AddByte(0)
            _Packet.AddWord(ItemID)
            _Packet.AddByte(&H0)
            _Packet.AddWord(Destination.X)
            _Packet.AddWord(Destination.Y)
            _Packet.AddByte(Destination.Z)

            Dim Loc As ITibia.LocationDefinition = _Proxy.Client.CharacterLocation

            Dim X As Integer = Destination.X - Loc.X + 8
            Dim Y As Integer = Destination.Y - Loc.Y + 6
            Dim Z As Integer = _Proxy.Client.MapTiles.WorldZToClientZ(Loc.Z)

            Dim TileObjects() As IMapTiles.TileObject = _Proxy.Client.MapTiles.GetTileObjects(X, Y, Z)
            If TileObjects.Length = 1 Then 'just the ground tile
                _Packet.AddWord(TileObjects(0).GetObjectID)
                _Packet.AddByte(0)
            ElseIf TileObjects.Length > 1 Then
                If TileId > 0 Then
                    Dim Found As Boolean = False
                    Dim StackPos As Byte = 0
                    For Each TileObj As IMapTiles.TileObject In TileObjects
                        If TileObj.GetObjectID = TileId Then
                            Found = True
                            StackPos = TileObj.GetStackPosition
                            Exit For
                        End If
                    Next
                    If Found Then
                        _Packet.AddWord(TileId)
                        _Packet.AddByte(StackPos)
                    Else
                        _Packet.AddWord(TileId)
                        _Packet.AddByte(0)
                    End If
                Else
                    Dim TObj As IMapTiles.TileObject = New IMapTiles.TileObject(0, 0, 0, New ITibia.LocationDefinition(), New IMapTiles.ClientCoordinates(), 0)
                    For Each TileObj As IMapTiles.TileObject In TileObjects
                        If TileObj.GetObjectID = &H63 Then
                            TObj = TileObj
                            Exit For
                        End If
                    Next
                    If TObj.GetObjectID = 0 Then
                        TObj = TileObjects(TileObjects.Length - 1)
                    End If
                    _Packet.AddWord(TObj.GetObjectID)
                    _Packet.AddByte(TObj.GetStackPosition)
                End If
            End If
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UseObject(ByVal ItemID As Int16, ByVal InventorySlot As ITibia.InventorySlots, ByVal ContainerIndex As Byte) Implements IServerPacketBuilder.UseObject
        Try
            Dim _Packet As New Packet
            'Dim bytBuffer(1) As Byte
            'Dim ID As Integer = 0
            'Core.Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (InventorySlot - 1)), ID, 2)
            _Packet.AddByte(&H82)
            _Packet.AddWord(&HFFFF)
            _Packet.AddWord(InventorySlot)
            _Packet.AddByte(0)
            _Packet.AddWord(ItemID)
            _Packet.AddByte(0)
            _Packet.AddByte(ContainerIndex)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UseObject(ByVal Item As IContainer.ContainerItemDefinition, Optional ByVal ContainerIndex As Byte = &HF) Implements IServerPacketBuilder.UseObject
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H82)
            _Packet.AddLocation(Item.Location)
            _Packet.AddWord(Item.ID)
            _Packet.AddByte(Item.Slot)
            _Packet.AddByte(ContainerIndex)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Public Sub OpenContainer(ByRef Item As IContainer.ContainerItemDefinition, ByRef ContainerIndex As Byte)
    '    Try
    '        Dim _Packet As New Packet
    '        _Packet.AddByte(&H82)
    '        _Packet.AddLocation(Item.Location)
    '        _Packet.AddWord(Item.ID)
    '        _Packet.AddByte(Item.Location.Z)
    '        _Packet.AddByte(ContainerIndex)
    '        Packets.Enqueue(_Packet)
    '        If _AutoSend Then Send()
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Public Sub UseHotkey(ByVal ItemID As Int32, ByVal CharacterID As Int32, Optional ByVal ExtraByte As Integer = 0) Implements IServerPacketBuilder.UseHotkey
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H84)
            _Packet.AddWord(&HFFFF)
            _Packet.AddWord(&H0)
            _Packet.AddByte(&H0)
            _Packet.AddWord(ItemID)
            _Packet.AddByte(ExtraByte)
            _Packet.AddDWord(CharacterID)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub StopEverything() Implements IServerPacketBuilder.StopEverything
        Try
            Dim _Packet As New Packet

            _Packet.AddByte(&HBE)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub MoveObject(ByVal ItemID As Integer, ByVal Source As ITibia.LocationDefinition, ByVal Destination As ITibia.LocationDefinition, Optional ByVal Count As Byte = &HFF) Implements IServerPacketBuilder.MoveObject
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H78)
            _Packet.AddLocation(Source)
            _Packet.AddWord(ItemID)
            _Packet.AddByte(Source.Z)
            _Packet.AddLocation(Destination)
            If Count = 0 Then
                Count = 1
            ElseIf Count > 100 Then
                Count = 100
            End If
            _Packet.AddByte(Count)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub MoveObject(ByVal Item As IContainer.ContainerItemDefinition, ByVal Destination As ITibia.LocationDefinition, Optional ByVal Count As Byte = &HFF) Implements IServerPacketBuilder.MoveObject
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H78)
            _Packet.AddLocation(Item.Location)
            _Packet.AddWord(Item.ID)
            _Packet.AddByte(Item.Location.Z)
            _Packet.AddLocation(Destination)
            Dim _Count As Byte = 0
            If Count <> &HFF Then
                _Count = Count
            Else
                _Count = Item.Count
            End If
            If _Count = 0 Then
                _Count = 1
            ElseIf _Count > 100 Then
                _Count = 100
            End If
            _Packet.AddByte(_Count)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Public Sub MoveObject(ByVal ItemID As Integer, ByVal Source As ITibia.LocationDefinition, ByVal Destination As ITibia.LocationDefinition, ByVal Count As Integer=100)
    '    Try
    '        Dim _Packet As New Packet
    '        _Packet.AddByte(&H78)
    '        _Packet.AddWord(Source.X)
    '        _Packet.AddWord(Source.Y)
    '        _Packet.AddByte(Source.Z)
    '        _Packet.AddWord(ItemID)
    '        _Packet.AddByte(Source.Z)
    '        _Packet.AddWord(Destination.X)
    '        _Packet.AddWord(Destination.Y)
    '        _Packet.AddByte(Destination.Z)
    '        If Count > 100 Then Count = 100
    '        _Packet.AddByte(CByte(Count))
    '        Packets.Enqueue(_Packet)
    '        If _AutoSend Then Send()
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Public Sub UseObject(ByVal ItemID As Integer, ByVal Source As ITibia.LocationDefinition, Optional ByVal ContainerIndex As Byte = 1) Implements IServerPacketBuilder.UseObject
        Try
            Dim _Packet As New Packet
            'Dim bytBuffer(&H1) As Byte
            _Packet.AddByte(&H82)
            _Packet.AddLocation(Source)
            _Packet.AddWord(ItemID)
            Dim Loc As ITibia.LocationDefinition = _Proxy.Client.CharacterLocation

            Dim X As Integer = Source.X - Loc.X + 8
            Dim Y As Integer = Source.Y - Loc.Y + 6
            Dim Z As Integer = _Proxy.Client.MapTiles.WorldZToClientZ(Loc.Z)
            Dim TileObj As IMapTiles.TileObject
            If _Proxy.Client.MapTiles.FindObjectInTile(TileObj, ItemID, X, Y, Z) Then
                _Packet.AddByte(TileObj.GetStackPosition)
            Else
                _Packet.AddByte(1)
            End If
            _Packet.AddByte(ContainerIndex)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UseFishingRodOnLocation(ByVal FishingRod As IContainer.ContainerItemDefinition, ByVal Destination As ITibia.LocationDefinition, ByVal Sprite As UInt32) Implements IServerPacketBuilder.UseFishingRodOnLocation
        Dim _Packet As New Packet()
        _Packet.AddByte(&H83)
        _Packet.AddLocation(FishingRod.Location)
        _Packet.AddWord(FishingRod.ID)
        _Packet.AddByte(FishingRod.Slot)
        _Packet.AddLocation(Destination)
        _Packet.AddWord(Sprite)
        _Packet.AddByte(&H0)
        Packets.Enqueue(_Packet)
        If _AutoSend Then Send()
    End Sub

    Public Sub Send() Implements IPacketBuilder.Send
        Try
            While Packets.Count > 0
                Dim P As Packet = Packets.Dequeue()
                Dim bytes() As Byte = P.GetBytes
                _Proxy.SendPacketToServer(P)
            End While
        Catch Ex As System.InvalidOperationException
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
