Imports TibiaTekBot3.CoreModule, TibiaTekBot3.CoreModule.CoreClass

Module PacketUtils

#Region " Packet Reading "

    Public Function GetString(ByVal bytBuffer() As Byte, ByRef Start As Integer) As String
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

    Public Function GetByte(ByVal bytBuffer() As Byte, ByRef Start As Integer) As Byte
        'If Not Core.InGame Then Return 0
        Dim Result As Byte = bytBuffer(Start)
        Start += 1
        Return Result
    End Function

    Public Function GetWord(ByVal bytBuffer() As Byte, ByRef Start As Integer) As UInt16
        Dim Result As UInt16 = bytBuffer(Start) + (CUShort(bytBuffer(Start + 1)) * 256)
        Start += 2
        Return Result
    End Function

    Public Function GetDWord(ByVal bytBuffer() As Byte, ByRef Start As Integer) As UInt32
        Dim Result As UInt32 = bytBuffer(Start)
        Result += (CUInt(bytBuffer(Start + 1)) * 256)
        Result += (CUInt(bytBuffer(Start + 2)) * 256 * 256)
        Result += (CUInt(bytBuffer(Start + 3)) * 256 * 256 * 256)
        Start += 4
        Return Result
    End Function

    Public Function GetLocation(ByVal bytbuffer() As Byte, ByRef Start As Integer) As LocationDefinition
        Dim Loc As New LocationDefinition
        Loc.X = GetWord(bytbuffer, Start)
        Loc.Y = GetWord(bytbuffer, Start)
        Loc.Z = GetByte(bytbuffer, Start)
        Return Loc
    End Function


#End Region

#Region " Packet Writing "

    Public Sub AddByte(ByRef bytBuffer() As Byte, ByVal bytByte As Byte)
        Dim intTemp As Integer
        Dim bytTemp() As Byte
        intTemp = UBound(bytBuffer)
        ReDim Preserve bytBuffer(intTemp + 1)
        bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + 1)
        bytBuffer(0) = bytTemp(0)
        bytBuffer(1) = bytTemp(1)
        bytBuffer(intTemp + 1) = bytByte
    End Sub

    Public Sub AddWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt16)
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
    End Sub

    Public Sub AddDWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt32)
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
    End Sub

    Public Sub AddString(ByRef bytBuffer() As Byte, ByVal strString As String)
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
    End Sub

#End Region

#Region " Speak "

    Public Function Speak(ByVal Message As String, ByVal ChannelID As ChannelType) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H96)
        AddByte(bytBuffer, MessageType.Channel)
        AddWord(bytBuffer, ChannelID)
        AddString(bytBuffer, Message)
        Return bytBuffer
    End Function

    Public Function Speak(ByVal Destinatary As String, ByVal Message As String) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H96)
        AddByte(bytBuffer, MessageType.PM)
        AddString(bytBuffer, Destinatary)
        AddString(bytBuffer, Message)
        Return bytBuffer
    End Function

    Public Function Speak(ByVal Message As String, Optional ByVal Type As MessageType = MessageType.Normal) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H96)
        AddByte(bytBuffer, Type)
        AddString(bytBuffer, Message)
        Return bytBuffer
    End Function

#End Region

#Region " FYI Box "

    Public Function FYIBox(ByVal Message As String) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H15)
        AddString(bytBuffer, Message)
        Return bytBuffer
    End Function

#End Region

#Region " CreatureSpeak "

    Public Function CreatureSpeak(ByVal Name As String, ByVal SpeakType As MessageType, ByVal Level As Integer, ByVal Message As String, Optional ByVal X As UInt16 = 0, Optional ByVal Y As UInt16 = 0, Optional ByVal Z As Byte = 0, Optional ByVal Channel As ChannelType = ChannelType.Console) As Byte()

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
    End Function

#End Region

#Region " StopEverthing "

    Public Function StopEverything() As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HBE)
        Return bytBuffer
    End Function

#End Region

#Region " SystemMessage "

    Public Function SystemMessage(ByVal Type As SysMessageType, ByVal Message As String) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HB4)
        AddByte(bytBuffer, Type) '&h12
        AddString(bytBuffer, Message)
        Return bytBuffer
    End Function

#End Region

#Region " Change Outfit"
    Public Function ChangeOutfit(ByVal OutfitID As UShort, ByVal HeadColor As Byte, ByVal BodyColor As Byte, ByVal LegsColor As Byte, ByVal FeetColor As Byte, ByVal Addons As Byte) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HD3)
        AddWord(bytBuffer, OutfitID)
        AddByte(bytBuffer, HeadColor)
        AddByte(bytBuffer, BodyColor)
        AddByte(bytBuffer, LegsColor)
        AddByte(bytBuffer, FeetColor)
        AddByte(bytBuffer, Addons)
        Return bytBuffer
    End Function
#End Region

    Public Function UseObjectOnPlayerAsHotkey(ByVal ItemID As UShort, ByVal Destination As LocationDefinition, Optional ByVal ExtraByte As Byte = 0) As Byte()
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
    End Function

#Region " AnimatedText "

    Public Function AnimatedText(ByRef Color As Byte, ByRef Loc As LocationDefinition, ByRef ShortText As String) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H84)
        AddWord(bytBuffer, Loc.X)
        AddWord(bytBuffer, Loc.Y)
        AddByte(bytBuffer, Loc.Z)
        AddByte(bytBuffer, Color)
        AddString(bytBuffer, ShortText)
        Return bytBuffer
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
        Dim bytBuffer(&H1) As Byte
        AddByte(bytBuffer, &H8C)
        AddWord(bytBuffer, Source.X)
        AddWord(bytBuffer, Source.Y)
        AddByte(bytBuffer, Source.Z)
        AddWord(bytBuffer, Item)
        AddByte(bytBuffer, &H1)
        Return bytBuffer
    End Function

#End Region

#Region " AttackEntity "

    Public Function AttackEntity(ByVal EntityID As UInt32) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HA1)
        AddDWord(bytBuffer, EntityID)
        Return bytBuffer
    End Function

#End Region

#Region " PlayerLogout "

    Public Function PlayerLogout() As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H14)
        Return bytBuffer
    End Function

#End Region

#Region " OpenChannel "

    Public Sub OpenChannel(ByVal ChannelName As String, ByVal ChannelID As ChannelType)
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HAC)
        AddByte(bytBuffer, MessageType.Channel)
        AddByte(bytBuffer, ChannelID)
        AddString(bytBuffer, ChannelName)
        Core.SendPacketToClient(bytBuffer)
    End Sub

    Public Sub OpenChannel()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &HAC)
        AddWord(bytBuffer, ConsoleChannelID)
        AddString(bytBuffer, ConsoleName)
        Core.SendPacketToClient(bytBuffer)
    End Sub

#End Region

#Region " UseObject "

    Public Function UseObject(ByVal Item As ContainerItemDefinition) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H82)
        AddWord(bytBuffer, &HFFFF)
        AddWord(bytBuffer, &H40 + Item.ContainerIndex)
        AddByte(bytBuffer, Item.Slot)
        AddWord(bytBuffer, Item.ID)
        AddByte(bytBuffer, Item.Slot)
        AddByte(bytBuffer, Item.ContainerIndex)
        Return bytBuffer
    End Function

    Public Function UseObjectOnGround(ByVal ItemID As UShort, ByVal Source As LocationDefinition, Optional ByVal OpenLoc As Byte = 1) As Byte()
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
    End Function

    Public Function UseObjectWithObjectOnGround(ByVal ItemID As UShort, ByVal Destination As LocationDefinition, Optional ByVal TileId As UShort = 0) As Byte()
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
        Trace.WriteLine("UseObjectWithObjectOnGround>" & BytesToStr(bytBuffer))
        Return bytBuffer
    End Function

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

    Public Function MoveObject(ByVal ItemID As UShort, ByVal Source As LocationDefinition, ByVal Destination As LocationDefinition, ByVal Count As Byte) As Byte()
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
        AddByte(bytBuffer, Count)
        Return bytBuffer
    End Function

    Public Function MoveObject(ByVal Item As ContainerItemDefinition, ByVal Destination As LocationDefinition, ByVal Count As Byte) As Byte()
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
        AddByte(bytBuffer, Count)
        Return bytBuffer
    End Function

    Public Function MoveObject(ByVal Item As ContainerItemDefinition, ByVal Destination As LocationDefinition) As Byte()
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
    End Function

#End Region

#Region " Use Fishingrod "
    Public Function UseFishingRodOnLocation(ByVal FishingRod As ContainerItemDefinition, ByVal Destination As LocationDefinition, ByVal Sprite As UInt32) As Byte()
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
    End Function
#End Region

#Region " Use Hotkey "
    Public Function UseHotkey(ByVal ItemId As UInt32, Optional ByVal ExtraByte As Byte = 0) As Byte()
        Dim bytBuffer(1) As Byte
        AddByte(bytBuffer, &H84)
        AddWord(bytBuffer, &HFFFF)
        AddWord(bytBuffer, &H0)
        AddByte(bytBuffer, &H0)
        AddWord(bytBuffer, ItemId)
        If Core.DatInfo.GetInfo(ItemId).HasExtraByte Then AddByte(bytBuffer, ExtraByte)
        AddDWord(bytBuffer, Core.CharacterID)
        Return bytBuffer
    End Function
#End Region

    Public Function TestPacket()
        Dim bytBuffer(&H1) As Byte
        AddByte(bytBuffer, &HA)
        AddByte(bytBuffer, &H0)
        AddByte(bytBuffer, &H4)
        AddByte(bytBuffer, &H70)
        AddByte(bytBuffer, &H0)
        AddByte(bytBuffer, &H36)
        AddByte(bytBuffer, &HB)
        AddByte(bytBuffer, &HFF)
        AddByte(bytBuffer, &HFF)
        Return bytBuffer
    End Function


End Module
