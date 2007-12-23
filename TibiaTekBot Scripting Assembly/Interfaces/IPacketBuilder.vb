Public Interface IPacketBuilder

#Region " Methods "
    Sub AddByte(ByVal Value As Byte)
    Sub AddWord(ByVal Value As UInt16)
    Sub AddDWord(ByVal Value As UInt32)
    Sub AddString(ByVal Value As String)
    Sub AddLocation(ByVal Location As ITibia.LocationDefinition)
#End Region

End Interface


