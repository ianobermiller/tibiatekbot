Public Interface IDatFile

#Region " Structures "

    Structure DatObject
        Public IsContainer As Boolean
        Dim ReadWriteInfo As Integer
        Dim IsFluidContainer As Boolean
        Dim IsStackable As Boolean
        Dim MultiType As Boolean
        Dim Useable As Boolean
        Dim IsNotMovable As Boolean
        Dim AlwaysOnTop As Boolean
        Dim IsGroundTile As Boolean
        Dim IsPickupable As Boolean
        Dim Blocking As Boolean
        Dim BlockPickupable As Boolean
        Dim IsWalkable As Boolean
        'Dim NoFloorChange As Boolean
        Dim IsDoor As Boolean
        Dim IsDoorWithLock As Boolean
        Dim Speed As Byte
        Dim CanDecay As Boolean
        Dim HasExtraByte As Boolean
        'Dim IsWater As Boolean
        Dim StackPriority As Integer
        'Dim HasFish As Boolean
        'Dim FloorChangeUp As Boolean
        'Dim FloorChangeDown As Boolean
        'Dim RequiresRightClick As Boolean
        'Dim RequiresRope As Boolean
        'Dim RequiresShovel As Boolean
        'Dim IsFood As Boolean
        Dim IsField As Boolean
        Dim IsDepot As Boolean
        Dim MoreAlwaysOnTop As Boolean
        Dim Usable2 As Boolean

        'Dim MultiCharge As Boolean
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
