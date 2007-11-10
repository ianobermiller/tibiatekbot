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

Imports TibiaTekBot.CoreModule

Module PacketUtils

#Region " Packet Reading "

    Public Function GetString(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As String
        Try
            Dim intCount As Integer
            Dim intTemp As UShort
            Dim strString As String = ""
            intTemp = GetWord(bytBuffer, Start)
            For intCount = Start To intTemp + Start - 1
                strString = strString & Chr(bytBuffer(intCount))
            Next
            Start = intTemp + Start
            Return strString
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetByte(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As Byte
        Try
            Dim Result As Byte = bytBuffer(Start)
            Start += 1
            Return Result
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetWord(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As UInt16
        Try
            Dim Result As UInt16 = bytBuffer(Start) + (CUShort(bytBuffer(Start + 1)) * 256)
            Start += 2
            Return Result
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetDWord(ByVal bytBuffer() As Byte, ByRef Start As UInteger) As UInt32
        Try
            Dim Result As UInt32 = bytBuffer(Start)
            Result += (CUInt(bytBuffer(Start + 1)) * 256)
            Result += (CUInt(bytBuffer(Start + 2)) * 256 * 256)
            Result += (CUInt(bytBuffer(Start + 3)) * 256 * 256 * 256)
            Start += 4
            Return Result
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function GetLocation(ByVal bytbuffer() As Byte, ByRef Start As UInteger) As CoreModule.LocationDefinition
        Try
            Dim Loc As New LocationDefinition
            Loc.X = GetWord(bytbuffer, Start)
            Loc.Y = GetWord(bytbuffer, Start)
            Loc.Z = GetByte(bytbuffer, Start)
            Return Loc
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
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

    Public Function Speak(ByVal Message As String, ByVal ChannelID As ChannelType) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H96)
            AddByte(bytBuffer, MessageType.Channel)
            AddWord(bytBuffer, ChannelID)
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function Speak(ByVal Destinatary As String, ByVal Message As String) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H96)
            AddByte(bytBuffer, MessageType.PM)
            AddString(bytBuffer, Destinatary)
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function Speak(ByVal Message As String, Optional ByVal Type As MessageType = MessageType.Normal) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H96)
            AddByte(bytBuffer, Type)
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " FYI Box "

    Public Function FYIBox(ByVal Message As String) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H15)
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " CreatureSpeak "

    Public Function CreatureSpeak(ByVal Name As String, ByVal SpeakType As MessageType, ByVal Level As Integer, ByVal Message As String, Optional ByVal X As UInt16 = 0, Optional ByVal Y As UInt16 = 0, Optional ByVal Z As Byte = 0, Optional ByVal Channel As ChannelType = ChannelType.Console) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &HAA)
            AddDWord(bytBuffer, 0)
            AddString(bytBuffer, Name)
            AddWord(bytBuffer, Level)
            AddByte(bytBuffer, SpeakType)
            Select Case SpeakType
                Case MessageType.Normal, MessageType.Whisper, MessageType.Yell, MessageType.MonsterSay, MessageType.MonsterYell
                    AddWord(bytBuffer, X)
                    AddWord(bytBuffer, Y)
                    AddByte(bytBuffer, Z)
                Case MessageType.Channel, MessageType.ChannelCounsellor, MessageType.ChannelGM, MessageType.ChannelTutor
                    AddWord(bytBuffer, Channel)
            End Select
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " OpenChannel "

    Public Sub OpenChannel(ByVal ChannelName As String, ByVal ChannelID As ChannelType)
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &HAC)
            AddByte(bytBuffer, MessageType.Channel)
            AddByte(bytBuffer, ChannelID)
            AddString(bytBuffer, ChannelName)
            Core.Proxy.SendPacketToClient(bytBuffer)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Sub

    Public Sub OpenChannel()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HAC)
        AddWord(bytBuffer, ConsoleChannelID)
        AddString(bytBuffer, ConsoleName)
        Core.Proxy.SendPacketToClient(bytBuffer)
    End Sub

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

    Public Function MoveObject(ByVal ItemID As Integer, ByVal Source As LocationDefinition, ByVal Destination As LocationDefinition, ByVal Count As Integer) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H78)
            AddWord(bytBuffer, Source.X)
            AddWord(bytBuffer, Source.Y)
            AddByte(bytBuffer, Source.Z)
            AddWord(bytBuffer, ItemID)
            AddByte(bytBuffer, Source.Z)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            AddByte(bytBuffer, CByte(Count))
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function MoveObject(ByVal Item As ContainerItemDefinition, ByVal Destination As LocationDefinition, ByVal Count As Integer) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H78)
            AddWord(bytBuffer, Item.Location.X)
            AddWord(bytBuffer, Item.Location.Y)
            AddByte(bytBuffer, Item.Location.Z)
            AddWord(bytBuffer, Item.ID)
            AddByte(bytBuffer, Item.Location.Z)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            AddByte(bytBuffer, CByte(Count))
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function MoveObject(ByVal Item As ContainerItemDefinition, ByVal Destination As LocationDefinition) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H78)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, &H40 + Item.ContainerIndex)
            AddByte(bytBuffer, Item.Slot)
            AddWord(bytBuffer, Item.ID)
            AddByte(bytBuffer, Item.Slot)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            Dim Count As Byte = Item.Count
            If Count = 0 Then Count = 1
            AddByte(bytBuffer, Count)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " UseObject "

    Public Function UseObject(ByVal Item As ContainerItemDefinition) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H82)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, &H40 + Item.ContainerIndex)
            AddByte(bytBuffer, Item.Slot)
            AddWord(bytBuffer, Item.ID)
            AddByte(bytBuffer, Item.Slot)
            AddByte(bytBuffer, Item.ContainerIndex)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function UseObjectOnGround(ByVal ItemID As Integer, ByVal Source As LocationDefinition, Optional ByVal OpenLoc As Byte = 1) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H82)
            AddWord(bytBuffer, Source.X)
            AddWord(bytBuffer, Source.Y)
            AddByte(bytBuffer, Source.Z)
            AddWord(bytBuffer, ItemID)
            Dim X As Integer = Source.X - Core.CharacterLoc.X + 8
            Dim Y As Integer = Source.Y - Core.CharacterLoc.Y + 6
            Dim Z As Integer = MapReader.WorldZToClientZ(Core.CharacterLoc.Z)
            Dim TileObj As TileObject
            If Core.Map.FindObjectInTile(TileObj, ItemID, X, Y, Z) Then
                AddByte(bytBuffer, TileObj.GetStackPosition)
            Else
                AddByte(bytBuffer, 1)
            End If
            AddByte(bytBuffer, OpenLoc)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function UseObjectWithObjectOnGround(ByVal ItemID As UShort, ByVal Destination As LocationDefinition, Optional ByVal TileId As UShort = 0) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H83)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, 0)
            AddByte(bytBuffer, 0)
            AddWord(bytBuffer, ItemID)
            AddByte(bytBuffer, &H0)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)

            Dim X As Integer = Destination.X - Core.CharacterLoc.X + 8
            Dim Y As Integer = Destination.Y - Core.CharacterLoc.Y + 6
            Dim Z As Integer = MapReader.WorldZToClientZ(Core.CharacterLoc.Z)

            Dim TileObjects() As TileObject = Core.Map.GetTileObjects(X, Y, Z)
            If TileObjects.Length = 1 Then 'just the ground tile
                AddWord(bytBuffer, TileObjects(0).GetObjectID)
                AddByte(bytBuffer, 0)
            ElseIf TileObjects.Length > 1 Then
                If TileId > 0 Then
                    Dim Found As Boolean = False
                    Dim StackPos As Byte = 0
                    For Each TileObj As TileObject In TileObjects
                        If TileObj.GetObjectID = TileId Then
                            Found = True
                            StackPos = TileObj.GetStackPosition
                            Exit For
                        End If
                    Next
                    If Found Then
                        AddWord(bytBuffer, TileId)
                        AddByte(bytBuffer, StackPos)
                    Else
                        AddWord(bytBuffer, TileId)
                        AddByte(bytBuffer, 0)
                    End If
                Else
                    Dim TObj As TileObject = New TileObject(0, 0, 0, New LocationDefinition(), New ClientCoordinates(), 0)
                    For Each TileObj As TileObject In TileObjects
                        If TileObj.GetObjectID = &H63 Then
                            TObj = TileObj
                            Exit For
                        End If
                    Next
                    If TObj.GetObjectID = 0 Then
                        TObj = TileObjects(TileObjects.Length - 1)
                    End If
                    AddWord(bytBuffer, TObj.GetObjectID)
                    AddByte(bytBuffer, TObj.GetStackPosition)
                End If
            End If
            '            Trace.WriteLine("UseObjectWithObjectOnGround>" & BytesToStr(bytBuffer))
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

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

#Region " SystemMessage "

    Public Function SystemMessage(ByVal Type As SysMessageType, ByVal Message As String) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &HB4)
            AddByte(bytBuffer, Type) '&h12
            AddString(bytBuffer, Message)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " Change Combat Modes "

    Enum FightingMode
        Offensive = 1
        Balanced = 2
        Defensive = 3
    End Enum

    Enum ChasingMode
        Standing
        Chasing
    End Enum

    Enum SecureMode
        Normal
        Secure
    End Enum


    Public Function ChangeFightingMode(ByVal Mode As FightingMode) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            Dim ChasingMode As Integer = 0
            Dim SecureMode As Integer = 0
            Core.ReadMemory(Consts.ptrChasingMode, ChasingMode, 1)
            Core.ReadMemory(Consts.ptrSecureMode, SecureMode, 1)
            AddByte(bytBuffer, &HA0)
            AddByte(bytBuffer, CByte(Mode))
            AddByte(bytBuffer, ChasingMode)
            AddByte(bytBuffer, SecureMode)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function ChangeChasingMode(ByVal Mode As ChasingMode) As Byte()
        Dim bytBuffer(1) As Byte
        Dim FightingMode As Integer = 0
        Dim SecureMode As Integer = 0
        Core.ReadMemory(Consts.ptrFightingMode, FightingMode, 1)
        Core.ReadMemory(Consts.ptrSecureMode, SecureMode, 1)
        AddByte(bytBuffer, &HA0)
        AddByte(bytBuffer, FightingMode)
        AddByte(bytBuffer, CByte(Mode))
        AddByte(bytBuffer, SecureMode)
        Return bytBuffer
    End Function

    Public Function ChangeSecureMode(ByVal Mode As SecureMode) As Byte()
        Dim bytBuffer(1) As Byte
        Dim FightingMode As Integer = 0
        Dim ChasingMode As Integer = 0
        Core.ReadMemory(Consts.ptrFightingMode, FightingMode, 1)
        Core.ReadMemory(Consts.ptrChasingMode, ChasingMode, 1)
        AddByte(bytBuffer, &HA0)
        AddByte(bytBuffer, FightingMode)
        AddByte(bytBuffer, ChasingMode)
        AddByte(bytBuffer, CByte(Mode))
        Return bytBuffer
    End Function

    Public Function ChangeCombatModes(ByVal Fighting As FightingMode, ByVal Chasing As ChasingMode, ByVal Secure As SecureMode) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HA0)
        AddByte(bytBuffer, CByte(Fighting))
        AddByte(bytBuffer, CByte(Chasing))
        AddByte(bytBuffer, CByte(Secure))
        Return bytBuffer
    End Function

#End Region

#Region " Change Outfit"
    Public Function ChangeOutfit(ByVal OutfitID As Integer, ByVal HeadColor As Integer, ByVal BodyColor As Integer, ByVal LegsColor As Integer, ByVal FeetColor As Integer, ByVal Addons As Integer) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &HD3)
            AddWord(bytBuffer, OutfitID)
            AddByte(bytBuffer, HeadColor)
            AddByte(bytBuffer, BodyColor)
            AddByte(bytBuffer, LegsColor)
            AddByte(bytBuffer, FeetColor)
            AddByte(bytBuffer, Addons)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " HouseSpellEdit "

    Public Function HouseSpellEdit(ByVal SpellID As Byte, ByVal Time As UInteger, ByVal Content As String) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H97)
        AddByte(bytBuffer, SpellID)
        AddDWord(bytBuffer, Time)
        AddString(bytBuffer, Content)
        Return bytBuffer
    End Function

#End Region

    Public Function UseFishingRodOnLocation(ByVal FishingRod As ContainerItemDefinition, ByVal Destination As LocationDefinition, ByVal Sprite As Int32) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H83)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, &H40 + FishingRod.ContainerIndex)
            AddByte(bytBuffer, FishingRod.Slot)
            AddWord(bytBuffer, FishingRod.ID)
            AddByte(bytBuffer, FishingRod.Slot)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            AddWord(bytBuffer, Sprite)
            AddByte(bytBuffer, &H0)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function UseObjectOnPlayer(ByVal Item As ContainerItemDefinition, ByVal Destination As LocationDefinition) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H83)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, &H40 + Item.ContainerIndex)
            AddByte(bytBuffer, Item.Slot)
            AddWord(bytBuffer, Item.ID)
            AddByte(bytBuffer, Item.Slot)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            AddByte(bytBuffer, &H63) '
            AddByte(bytBuffer, &H0) '
            AddByte(bytBuffer, &H1) '
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function UseObjectOnPlayerAsHotkey(ByVal ItemID As UShort, ByVal Destination As LocationDefinition, Optional ByVal ExtraByte As Byte = 0) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H83)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, 0)
            AddByte(bytBuffer, 0)
            AddWord(bytBuffer, ItemID)
            AddByte(bytBuffer, 0)
            AddWord(bytBuffer, Destination.X)
            AddWord(bytBuffer, Destination.Y)
            AddByte(bytBuffer, Destination.Z)
            AddByte(bytBuffer, &H63) '
            AddByte(bytBuffer, &H0) '
            AddByte(bytBuffer, &H1) '
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function UseObjectOnPlayerAsHotkey(ByVal ItemId As UShort, ByVal PlayerID As UInt32) As Byte()
        Try
            Dim bytBuffer(&H1) As Byte
            AddByte(bytBuffer, &H84)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, 0)
            AddByte(bytBuffer, 0)
            AddWord(bytBuffer, ItemId)
            AddByte(bytBuffer, 0)
            AddDWord(bytBuffer, PlayerID)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#Region " AddObjectToContainer "

    Public Function AddObjectToContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, Optional ByVal Count As Byte = 0) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H70)
            AddByte(bytBuffer, ContainerIndex)
            AddWord(bytBuffer, ItemID)
            If DatInfo.GetInfo(ItemID).HasExtraByte OrElse Definitions.IsRune(ItemID) Then
                AddByte(bytBuffer, Count)
            End If
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " RemoveObjectFromContainer "

    Public Function RemoveObjectFromContainer(ByVal Slot As Byte, ByVal ContainerIndex As Byte) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H72)
            AddByte(bytBuffer, ContainerIndex)
            AddByte(bytBuffer, Slot)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " CreateContainer "

    Public Function CreateContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, ByVal Name As String, ByVal Size As Byte, ByVal Items() As ContainerItemDefinition, Optional ByVal HasParent As Boolean = False) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H6E)
            AddByte(bytBuffer, ContainerIndex)
            AddWord(bytBuffer, ItemID)
            AddString(bytBuffer, Name)
            AddByte(bytBuffer, Size)
            AddByte(bytBuffer, HasParent)
            AddByte(bytBuffer, Items.Length)
            For Each Item As ContainerItemDefinition In Items
                AddWord(bytBuffer, Item.ID)
                If DatInfo.GetInfo(Item.ID).HasExtraByte OrElse Definitions.IsRune(Item.ID) Then
                    AddByte(bytBuffer, Item.Count)
                End If
            Next
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " AnimatedText "

    Public Function AnimatedText(ByRef Color As Byte, ByRef Loc As LocationDefinition, ByRef ShortText As String) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H84)
            AddWord(bytBuffer, Loc.X)
            AddWord(bytBuffer, Loc.Y)
            AddByte(bytBuffer, Loc.Z)
            AddByte(bytBuffer, Color)
            AddString(bytBuffer, ShortText)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " OpenContainer "

    Public Function OpenContainer(ByRef Loc As LocationDefinition, ByRef ItemID As UShort, ByRef ContainerIndex As Byte) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H82)
            AddWord(bytBuffer, Loc.X)
            AddWord(bytBuffer, Loc.Y)
            AddByte(bytBuffer, Loc.Z)
            AddWord(bytBuffer, ItemID)
            AddByte(bytBuffer, 1)
            AddByte(bytBuffer, ContainerIndex)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

    Public Function OpenContainer(ByRef Item As ContainerItemDefinition, ByRef ContainerIndex As Byte) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H82)
            AddWord(bytBuffer, Item.Location.X)
            AddWord(bytBuffer, Item.Location.Y)
            AddByte(bytBuffer, Item.Location.Z)
            AddWord(bytBuffer, Item.ID)
            AddByte(bytBuffer, Item.Location.Z)
            AddByte(bytBuffer, ContainerIndex)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

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

#Region " AnimateOnLocation "

    Public Function MagicEffect(ByVal Loc As LocationDefinition, ByVal Animation As MagicEffects) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H83)
            AddWord(bytBuffer, Loc.X)
            AddWord(bytBuffer, Loc.Y)
            AddByte(bytBuffer, Loc.Z)
            AddByte(bytBuffer, CByte(Animation))
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

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

    Public Function LookAtObject(ByVal Item As Int16, ByVal Source As LocationDefinition) As Byte()
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

#Region " AttackEntity "

    Public Function AttackEntity(ByVal EntityID As Int32) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &HA1)
            AddDWord(bytBuffer, EntityID)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " PlayerLogout "

    Public Function PlayerLogout() As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H14)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function

#End Region

#Region " Use Hotkey "
    Public Function UseHotkey(ByVal ItemId As Int32, Optional ByVal ExtraByte As Integer = 0) As Byte()
        Try
            Dim bytBuffer(1) As Byte
            AddByte(bytBuffer, &H84)
            AddWord(bytBuffer, &HFFFF)
            AddWord(bytBuffer, &H0)
            AddByte(bytBuffer, &H0)
            AddWord(bytBuffer, ItemId)
            If DatInfo.GetInfo(ItemId).HasExtraByte Then AddByte(bytBuffer, ExtraByte)
            AddDWord(bytBuffer, Core.CharacterID)
            Return bytBuffer
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
    End Function
#End Region

End Module
