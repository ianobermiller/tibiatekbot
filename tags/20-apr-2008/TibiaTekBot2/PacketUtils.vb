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

Imports TibiaTekBot.KernelModule, Scripting

Module PacketUtils

#Region " Packet Reading "

    Public Function GetString(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As String
        Dim intCount As Integer
        Dim intTemp As UShort
        Dim strString As String = ""
        intTemp = GetWord(bytBuffer, Start)
        For intCount = Start To intTemp + Start - 1
            strString = strString & Chr(bytBuffer(intCount))
        Next
        Start = intTemp + Start
        Return strString
    End Function

    Public Function GetByte(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As Byte

        Dim Result As Byte = bytBuffer(Start)
        Start += 1
        Return Result
    End Function

    Public Function GetWord(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As UInt16
        Dim Result As UInt16 = bytBuffer(Start) + (CUShort(bytBuffer(Start + 1)) * 256)
        Start += 2
        Return Result
    End Function

    Public Function GetDWord(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As UInt32
        Dim Result As UInt32 = bytBuffer(Start)
        Result += (CUInt(bytBuffer(Start + 1)) * 256)
        Result += (CUInt(bytBuffer(Start + 2)) * 256 * 256)
        Result += (CUInt(bytBuffer(Start + 3)) * 256 * 256 * 256)
        Start += 4
        Return Result
    End Function

	Public Function GetLocation(ByVal bytbuffer() As Byte, ByRef Start As UInteger) As ITibia.LocationDefinition
        Dim Loc As New ITibia.LocationDefinition
        Loc.X = GetWord(bytbuffer, Start)
        Loc.Y = GetWord(bytbuffer, Start)
        Loc.Z = GetByte(bytbuffer, Start)
        Return Loc
    End Function


#End Region

#Region " Packet Writing "

	Public Sub AddByte(ByRef bytBuffer() As Byte, ByVal bytByte As Byte)
		Try
			Dim intTemp As Integer
			Dim bytTemp() As Byte
			intTemp = UBound(bytBuffer)
			ReDim Preserve bytBuffer(intTemp + 1)
			bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + 1)
			bytBuffer(0) = bytTemp(0)
			bytBuffer(1) = bytTemp(1)
			bytBuffer(intTemp + 1) = bytByte
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End
		End Try
	End Sub

	Public Sub AddWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt16)
		Try
			Dim intTemp As Integer
			Dim bytTemp() As Byte
			intTemp = UBound(bytBuffer)
			ReDim Preserve bytBuffer(intTemp + 2)
			bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + 2)
			bytBuffer(0) = bytTemp(0)
			bytBuffer(1) = bytTemp(1)
			bytTemp = BitConverter.GetBytes(intInteger)
			bytBuffer(intTemp + 1) = bytTemp(0)
			bytBuffer(intTemp + 2) = bytTemp(1)
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End
		End Try
	End Sub

	Public Sub AddDWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt32)
		Try
			Dim intTemp As Integer
			Dim bytTemp() As Byte
			Dim I As Byte
			intTemp = UBound(bytBuffer)
			ReDim Preserve bytBuffer(intTemp + 4)
			bytTemp = BitConverter.GetBytes(BitConverter.ToInt32(bytBuffer, 0) + 4)
			bytBuffer(0) = bytTemp(0)
			bytBuffer(1) = bytTemp(1)
			bytTemp = BitConverter.GetBytes(intInteger)
			For I = 0 To 3
				bytBuffer(intTemp + I + 1) = bytTemp(I)
			Next
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End
		End Try
	End Sub

	Public Sub AddString(ByRef bytBuffer() As Byte, ByVal strString As String)
		Try
			If strString Is Nothing Then Exit Sub
			Dim intTemp As Integer
			Dim bytTemp() As Byte
			Dim chrTemp() As Char
			Dim intCounter As Integer
			AddWord(bytBuffer, strString.Length)
			intTemp = UBound(bytBuffer)
			ReDim Preserve bytBuffer(intTemp + strString.Length)
			bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + strString.Length)
			bytBuffer(0) = bytTemp(0)
			bytBuffer(1) = bytTemp(1)
			chrTemp = strString.ToCharArray
			For intCounter = 1 To strString.Length
				bytBuffer(intTemp + intCounter) = Asc(chrTemp(intCounter - 1))
			Next
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End
		End Try
	End Sub

#End Region

#Region " FixChannelList "

	'Public Sub FixChannelList(ByRef bytBuffer() As Byte)
	'Dim bytBuffer2(1) As Byte
	'Dim I As Integer
	'Dim Position As Integer = 4
	'Dim ChannelID As UInt16
	'Dim ChannelName As String
	'AddByte(bytBuffer2, &HAB)
	'AddByte(bytBuffer2, bytBuffer(3))
	'For I = 0 To bytBuffer(3) - 1
	'ChannelID = GetWord(bytBuffer, Position)
	'ChannelName = GetString(bytBuffer, Position)
	'AddWord(bytBuffer2, ChannelID)
	'AddString(bytBuffer2, ChannelName)
	'Next
	'AddWord(bytBuffer2, ConsoleChannelID)
	'AddString(bytBuffer2, BotName)
	'bytBuffer = bytBuffer2
	'End Sub

#End Region

#Region " Speak "

    'Public Function Speak(ByVal Message As String, ByVal ChannelID As ITibia.Channel) As Byte()
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &H96)
    '        AddByte(bytBuffer, MessageType.Channel)
    '        AddWord(bytBuffer, ChannelID)
    '        AddString(bytBuffer, Message)
    '        Return bytBuffer
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Function

    'Public Function Speak(ByVal Destinatary As String, ByVal Message As String) As Byte()
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &H96)
    '        AddByte(bytBuffer, MessageType.PM)
    '        AddString(bytBuffer, Destinatary)
    '        AddString(bytBuffer, Message)
    '        Return bytBuffer
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Function

    'Public Function Speak(ByVal Message As String, Optional ByVal Type As MessageType = MessageType.Normal) As Byte()
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &H96)
    '        AddByte(bytBuffer, Type)
    '        AddString(bytBuffer, Message)
    '        Return bytBuffer
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Function

#End Region


#Region " OpenChannel "

    'Public Sub OpenChannel(ByVal ChannelName As String, ByVal ChannelID As ChannelType)
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &HAC)
    '        AddByte(bytBuffer, MessageType.Channel)
    '        AddByte(bytBuffer, ChannelID)
    '        AddString(bytBuffer, ChannelName)
    '        Core.Proxy.SendPacketToClient(bytBuffer)
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Sub

    'Public Function OpenPrivate(ByVal PlayerName As String) As Byte()
    '    Dim bytbuffer(1) As Byte
    '    AddByte(bytbuffer, &HAD)
    '    AddString(bytbuffer, PlayerName)
    '    Return bytbuffer
    'End Function

    'Public Sub OpenChannel()
    '    Dim bytBuffer(1) As Byte
    '    AddByte(bytBuffer, &HAC)
    '    AddWord(bytBuffer, ConsoleChannelID)
    '    AddString(bytBuffer, ConsoleName)
    '    Core.Proxy.SendPacketToClient(bytBuffer)
    'End Sub

    'Public Sub OpenIrcChannel(ByVal Channel As String, ByVal ChannelID As Short)
    '    Dim bytBuffer(1) As Byte
    '    AddByte(bytBuffer, &HAC)
    '    AddWord(bytBuffer, ChannelID)
    '    AddString(bytBuffer, Channel)
    '    Core.Proxy.SendPacketToClient(bytBuffer)
    'End Sub

#End Region

#Region " MoveObject "

	'Public Function MoveObject(ByVal Item As Int16, ByVal Source As locGround, ByVal Destination As locGround, Optional ByVal Quantity As Byte = 1) As Byte()
	'    Dim bytBuffer(&H1) As Byte
	'AddByte(bytBuffer, &H78)
	'AddWord(bytBuffer, Source.X)
	'AddWord(bytBuffer, Source.Y)
	'AddByte(bytBuffer, Source.Z)
	'AddWord(bytBuffer, Item)
	'addByte(bytBuffer, &H1)
	'AddWord(bytBuffer, Destination.X)
	'AddWord(bytBuffer, Destination.Y)
	'AddByte(bytBuffer, Destination.Z)
	'AddByte(bytBuffer, Quantity)
	'Return bytBuffer
	'End Function

	'Public Function MoveObject(ByVal Item As Int16, ByVal Source As locGround, ByVal Destination As locInventory, Optional ByVal Quantity As Byte = 1) As Byte()
	''    Dim bytBuffer(&H1) As Byte
	'       AddByte(bytBuffer, &H78)
	'AddWord(bytBuffer, Source.X)
	'AddWord(bytBuffer, Source.Y)
	'        AddByte(bytBuffer, Source.Z)
	'AddWord(bytBuffer, Item)
	'AddByte(bytBuffer, &H1)
	'AddWord(bytBuffer, &HFFFF)
	'AddWord(bytBuffer, Destination.Location)
	'AddByte(bytBuffer, Destination.Slot)
	'AddByte(bytBuffer, Quantity)
	'Return bytBuffer
	'End Function

	'Public Function MoveObject(ByVal Item As Int16, ByVal Source As locInventory, ByVal Destination As locGround, Optional ByVal Quantity As Byte = 1) As Byte()
	'    Dim bytBuffer(&H1) As Byte
	'AddByte(bytBuffer, &H78)
	'AddWord(bytBuffer, &HFFFF)
	'AddWord(bytBuffer, Source.Location)
	'AddByte(bytBuffer, Source.Slot)
	'AddWord(bytBuffer, Item)
	'AddByte(bytBuffer, Source.Slot)
	'AddWord(bytBuffer, Destination.X)
	'AddWord(bytBuffer, Destination.Y)
	'AddByte(bytBuffer, Destination.Z)
	'AddByte(bytBuffer, Quantity)
	'Return bytBuffer
	'End Function

	'Public Function MoveObject(ByVal Item As Int16, ByVal Source As locInventory, ByVal Destination As locInventory, Optional ByVal Quantity As Byte = 1) As Byte()
	'    Dim bytBuffer(&H1) As Byte
	'AddByte(bytBuffer, &H78)
	'AddWord(bytBuffer, &HFFFF)
	'AddWord(bytBuffer, Source.Location)
	'AddByte(bytBuffer, Source.Slot)
	'AddWord(bytBuffer, Item)
	'AddByte(bytBuffer, Source.Slot)
	'AddWord(bytBuffer, &HFFFF)
	'AddWord(bytBuffer, Destination.Location)
	'AddByte(bytBuffer, Destination.Slot)
	'AddByte(bytBuffer, Quantity)
	'Return bytBuffer
	'End Function



    'Public Function MoveObject(ByVal Item As ContainerItemDefinition, ByVal Destination As ITibia.LocationDefinition) As Byte()
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &H78)
    '        AddWord(bytBuffer, &HFFFF)
    '        AddWord(bytBuffer, &H40 + Item.ContainerIndex)
    '        AddByte(bytBuffer, Item.Slot)
    '        AddWord(bytBuffer, Item.ID)
    '        AddByte(bytBuffer, Item.Slot)
    '        AddWord(bytBuffer, Destination.X)
    '        AddWord(bytBuffer, Destination.Y)
    '        AddByte(bytBuffer, Destination.Z)
    '        Dim Count As Byte = Item.Count
    '        If Count = 0 Then Count = 1
    '        AddByte(bytBuffer, Count)
    '        Return bytBuffer
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Function

#End Region


#Region " StopEverthing "

	Public Function StopEverything() As Byte()
		Try
			Dim bytBuffer(1) As Byte
			AddByte(bytBuffer, &HBE)
			Return bytBuffer
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			End
		End Try
	End Function

#End Region


    '#Region " AddObjectToContainer "

    '    Public Function AddObjectToContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, Optional ByVal Count As Byte = 0) As Byte()
    '        Try
    '            Dim bytBuffer(1) As Byte
    '            AddByte(bytBuffer, &H70)
    '            AddByte(bytBuffer, ContainerIndex)
    '            AddWord(bytBuffer, ItemID)
    '            If Dat.GetInfo(ItemID).HasExtraByte OrElse Client.Items.IsRune(ItemID) Then
    '                AddByte(bytBuffer, Count)
    '            End If
    '            Return bytBuffer
    '        Catch Ex As Exception
    '            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End
    '        End Try
    '    End Function

    '#End Region

    '#Region " RemoveObjectFromContainer "

    '    Public Function RemoveObjectFromContainer(ByVal Slot As Byte, ByVal ContainerIndex As Byte) As Byte()
    '        Try
    '            Dim bytBuffer(1) As Byte
    '            AddByte(bytBuffer, &H72)
    '            AddByte(bytBuffer, ContainerIndex)
    '            AddByte(bytBuffer, Slot)
    '            Return bytBuffer
    '        Catch Ex As Exception
    '            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End
    '        End Try
    '    End Function

    '#End Region

#Region " CreateContainer "

 

#End Region

#Region " OpenContainer "

    'Public Function OpenContainer(ByRef Loc As ITibia.LocationDefinition, ByRef ItemID As UShort, ByRef ContainerIndex As Byte) As Byte()
    '    Try
    '        Dim bytBuffer(1) As Byte
    '        AddByte(bytBuffer, &H82)
    '        AddWord(bytBuffer, Loc.X)
    '        AddWord(bytBuffer, Loc.Y)
    '        AddByte(bytBuffer, Loc.Z)
    '        AddWord(bytBuffer, ItemID)
    '        AddByte(bytBuffer, 1)
    '        AddByte(bytBuffer, ContainerIndex)
    '        Return bytBuffer
    '    Catch Ex As Exception
    '        MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End
    '    End Try
    'End Function

#End Region

#Region " CloseContainer "

    Public Function CloseContainer(ByRef ContainerIndex As Byte) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H6F)
            AddByte(bytBuffer, ContainerIndex)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

    '#Region " AnimateOnLocation "

    '    Public Function MagicEffect(ByVal Loc As ITibia.LocationDefinition, ByVal Animation As AnimationEffects) As Byte()
    '        Try
    '            Dim bytBuffer(1) As Byte
    '            AddByte(bytBuffer, &H83)
    '            AddWord(bytBuffer, Loc.X)
    '            AddWord(bytBuffer, Loc.Y)
    '            AddByte(bytBuffer, Loc.Z)
    '            AddByte(bytBuffer, CByte(Animation))
    '            Return bytBuffer
    '        Catch Ex As Exception
    '            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End
    '        End Try
    '    End Function

    '#End Region

#Region " LookAtObject "

    'Public Function LookAtObject(ByVal Item As Int16, ByVal Source As locInventory) As Byte()
    '    Dim bytBuffer(&H1) As Byte
    'AddByte(bytBuffer, &H8C)
    'AddWord(bytBuffer, &HFFFF)
    'AddWord(bytBuffer, Source.Location)
    'AddByte(bytBuffer, Source.Slot)
    'AddWord(bytBuffer, Item)
    'AddByte(bytBuffer, Source.Slot)
    'Return bytBuffer
    'End Function

    Public Function LookAtObject(ByVal Item As Int16, ByVal Source As ITibia.LocationDefinition) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H8C)
            AddWord(bytBuffer, Source.X)
            AddWord(bytBuffer, Source.Y)
            AddByte(bytBuffer, Source.Z)
            AddWord(bytBuffer, Item)
            AddByte(bytBuffer, &H1)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region



End Module
