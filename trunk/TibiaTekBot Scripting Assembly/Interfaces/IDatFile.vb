Public Interface IDatFile

#Region " Structures "

    Structure DatObject
        Dim IsContainer As Boolean
        Dim TopOrder As Byte
        Dim IsField As Boolean
        Dim IsStackable As Boolean
        Dim IsCorpse As Boolean
        Dim IsUsable As Boolean
        Dim IsRune As Boolean
        Dim IsWritable As Boolean
        Dim IsReadable As Boolean
        Dim IsFluid As Boolean
        Dim IsSplash As Boolean
        Dim Blocking As Boolean
        Dim IsImmovable As Boolean
        Dim BlocksMissile As Boolean
        Dim BlocksPath As Boolean
        Dim IsPickupable As Boolean
        Dim IsHangable As Boolean
        Dim IsHangableHorizontal As Boolean
        Dim IsHangableVertical As Boolean
        Dim IsRotatable As Boolean
        Dim IsLightSource As Boolean
        Dim FloorChange As Boolean
        'Dim Offset as boolean
        Dim IsHeighted As Boolean
        Dim IsLayer As Boolean
        Dim IsIdleAnimation As Boolean
        Dim HasMiniMapColor As Boolean
        Dim HasActions As Boolean
        Dim IsGround As Boolean
        Dim HasExtraByte As Boolean

        Dim Speed As Byte

        ' actions
        Dim IsLadder As Boolean
        Dim IsSewer As Boolean
        Dim IsDoor As Boolean
        Dim IsDoorWithLock As Boolean
        Dim IsRopeSpot As Boolean
        Dim IsSwitch As Boolean
        Dim IsStairs As Boolean
        Dim IsMailbox As Boolean
        Dim IsDepot As Boolean
        Dim IsTrash As Boolean
        Dim IsHole As Boolean
        Dim HasSpecialDescription As Boolean
        Dim IsReadOnly As Boolean
    End Structure

#End Region

#Region " Properties "

    ReadOnly Property Length() As Integer

#End Region

#Region " Methods "

    Function GetInfo(ByVal ItemID As Integer) As DatObject
    Sub Refresh()

#End Region

End Interface
