Imports System.IO

Public Module DatReaderModule

    Public DatInfo As DatReader

    Public Structure DatTile
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

    Public Class DatReader
        Private DatTiles(7900) As DatTile

        Public ReadOnly Property Length() As Integer
            Get
                Return DatTiles.Length
            End Get
        End Property

        Public Function GetInfo(ByVal ItemID As UShort) As DatTile
            If ItemID >= DatTiles.Length Then Return Nothing
            Return DatTiles(ItemID)
        End Function

        Public Sub New(ByVal Filename As String)
            ReadDatFile(Filename)
        End Sub

        Public Sub ReadDatFile(ByVal Filename As String)

            Dim I As Integer
            Dim B1 As Byte
            For I = 0 To 7900
                DatTiles(I).IsContainer = False
                DatTiles(I).ReadWriteInfo = 0
                DatTiles(I).IsFluidContainer = False
                DatTiles(I).IsStackable = False
                DatTiles(I).MultiType = False
                DatTiles(I).Useable = False
                DatTiles(I).IsNotMovable = False
                DatTiles(I).AlwaysOnTop = False
                DatTiles(I).IsGroundTile = False
                DatTiles(I).Blocking = False
                DatTiles(I).IsPickupable = False
                'DatTiles(I).isblockingProjectile = False
                DatTiles(I).IsWalkable = False
                'DatTiles(I).NoFloorChange = False
                DatTiles(I).BlockPickupable = True
                'DatTiles(I).IsDoor = False
                'DatTiles(I).IsDoorWithLock = False
                DatTiles(I).Speed = 0
                DatTiles(I).CanDecay = True
                DatTiles(I).HasExtraByte = False 'custom flag
                'DatTiles(I).FloorChangeUp = False 'custom flag
                'DatTiles(I).FloorChangeDown = False 'custom flag
                'DatTiles(I).requireRightClick = False 'custom flag
                'DatTiles(I).requireRope = False 'custom flag
                'DatTiles(I).requireShovel = False 'custom flag
                'DatTiles(I).IsWater = False ' custom flag
                DatTiles(I).StackPriority = 1 ' custom flag, higher number, higher priority
                'DatTiles(I).HasFish = False
                'DatTiles(I).IsFood = False
                DatTiles(I).IsField = False
                DatTiles(I).IsDepot = False
                DatTiles(I).MoreAlwaysOnTop = False
                DatTiles(I).Usable2 = False
                'DatTiles(I).MultiCharge = False
            Next
            DatTiles(0).StackPriority = 0
            DatTiles(97).StackPriority = 2
            DatTiles(98).StackPriority = 2
            DatTiles(99).StackPriority = 2
            DatTiles(97).Blocking = True
            DatTiles(98).Blocking = True
            DatTiles(99).Blocking = True

            I = 100
            Dim FS As New FileStream(Filename, FileMode.Open, FileAccess.Read)
            Dim Reader As New BinaryReader(FS)
            Try
                Reader.ReadBytes(8)
                B1 = Reader.ReadByte
                'If B1 <> &H1F Then Throw New Exception("Error loading Tibia.dat file. Invalid version.")
                'If B1 <> &H23 Then Throw New Exception("Error loading Tibia.dat file. Invalid version.")
                Reader.ReadBytes(3)
                Dim Opt As Byte = 0
                Do
                    Opt = Reader.ReadByte
                    While Opt <> &HFF
                        Select Case Opt
                            Case &H0 'ground tile
                                DatTiles(I).IsGroundTile = True
                                DatTiles(I).Speed = Reader.ReadByte
                                If DatTiles(I).Speed = 0 Then
                                    DatTiles(I).Blocking = True
                                End If
                                Reader.ReadByte() 'ignore one byte
                            Case &H1 'always on top
                                DatTiles(I).MoreAlwaysOnTop = True
                            Case &H2
                                DatTiles(I).AlwaysOnTop = True
                            Case &H3
                                'can walk through, open doors
                                DatTiles(I).AlwaysOnTop = True
                                DatTiles(I).IsWalkable = True
                            Case &H4
                                ' is container
                                DatTiles(I).IsContainer = True
                            Case &H5
                                ' is stackable
                                DatTiles(I).IsStackable = True
                            Case &H6
                                ' is useable
                                DatTiles(I).Useable = True
                            Case &H7
                                ' writtable objects
                                DatTiles(I).Usable2 = True
                            Case &H8
                                'DatTiles(I).multicharge=true
                            Case &H9
                                DatTiles(I).ReadWriteInfo = 3 ' can writen + can be read
                                Reader.ReadByte() 'max chars, 0 is unlimited
                                Reader.ReadByte() 'max new lines
                            Case &HA
                                DatTiles(I).ReadWriteInfo = 1 ' can writen, cant be edited
                                Reader.ReadByte() 'max chars, 0 is unlimited
                                Reader.ReadByte() 'max new lines
                            Case &HB
                                ' is fluid container
                                DatTiles(I).IsFluidContainer = True
                            Case &HC
                                ' multitype
                                DatTiles(I).MultiType = True
                            Case &HD
                                ' is blocking
                                DatTiles(I).Blocking = True
                            Case &HE
                                ' not moveable
                                DatTiles(I).IsNotMovable = True
                            Case &HF
                                ' block missiles
                                'DatTiles(I).isblockingProjectile = True
                            Case &H10
                                ' Slight obstacle (include fields and certain boxes)
                                ' I prefer to don't consider a generic obstable and
                                ' do special cases for fields and ignore the boxes
                            Case &H11
                                DatTiles(I).IsPickupable = True
                            Case &H16
                                ' makes light -- skip bytes
                                Reader.ReadUInt16() 'radius
                                Reader.ReadUInt16() '215 for items, 208 for non items
                            Case &H12
                                ' can see what is under (ladder holes, stairs holes etc)
                            Case &H1F
                                ' ground tiles that don't cause level change
                                'DatTiles(I).noFloorChange = True
                            Case &H1A
                                ' mostly blocking items, but also items that can pile up in level (boxes, chairs etc)
                                DatTiles(I).BlockPickupable = False
                                Reader.ReadByte() 'always 8
                                Reader.ReadByte() 'always 0
                            Case &H15
                                'unknown
                            Case &H1D
                                ' for minimap drawing
                                Reader.ReadUInt16() 'two bytes for color
                            Case &H1B
                                ' corpses that don't decay
                                DatTiles(I).CanDecay = False
                            Case &H19
                                'unknown
                                Reader.ReadUInt16() '?
                                Reader.ReadUInt16() '?
                            Case &H1E
                                ' line spot ...
                                'Get fn, , optbyte2 '86 -> openable holes, 77-> can be used to go down, 76 can be used to go up, 82 -> stairs up, 79 switch,
                                Select Case Reader.ReadByte()
                                    Case &H4C
                                        'ladders
                                    Case &H4D
                                        'crate, trap door?
                                    Case &H4E
                                        'rope spot?
                                    Case &H4F
                                        'switch
                                    Case &H50
                                        'doors
                                        DatTiles(I).IsDoor = True
                                    Case &H51
                                        'doors with locks
                                        DatTiles(I).IsDoorWithLock = True
                                    Case &H52
                                        'stairs
                                    Case &H53
                                        'mailbox
                                    Case &H54
                                        'depot
                                        DatTiles(I).IsDepot = True
                                    Case &H55
                                        'trash
                                    Case &H56
                                        'hole
                                    Case &H57
                                        'items with special description?
                                    Case &H58
                                        'writtable?
                                        DatTiles(I).ReadWriteInfo = 1 ' read only
                                    Case Else
                                        'debugByte = optByte
                                        ' ignore
                                End Select 'optbyte2

                                Reader.ReadByte() 'always 4 o_o
                            Case Else
                                ' ignore
                        End Select 'optbyte
                        Opt = Reader.ReadByte
                    End While

                    If DatTiles(I).IsStackable OrElse DatTiles(I).MultiType = True OrElse DatTiles(I).IsFluidContainer Then
                        DatTiles(I).HasExtraByte = True
                    End If
                    If DatTiles(I).AlwaysOnTop = True Then
                        DatTiles(I).StackPriority = 3 ' max priority
                    End If
                    If DatTiles(I).MoreAlwaysOnTop = True Then
                        DatTiles(I).AlwaysOnTop = True
                        DatTiles(I).StackPriority = 4
                    End If
                    If (I >= &H4608 AndAlso I <= &H4F08) OrElse (I >= &H5308 AndAlso I <= &H5A08) Then
                        DatTiles(I).IsField = True
                    End If
                    ' to skip graph \/
                    Dim Width As Integer = Reader.ReadByte()
                    Dim Height As Integer = Reader.ReadByte()
                    If Width > 1 OrElse Height > 1 Then
                        Reader.ReadByte() 'skip 1 byte
                    End If
                    Dim BlendFrames As Integer = Reader.ReadByte()
                    Dim Xdiv As Integer = Reader.ReadByte()
                    Dim Ydiv As Integer = Reader.ReadByte()
                    Dim AnimCount As Integer = Reader.ReadByte()
                    Dim Rare As Integer = Reader.ReadByte
                    'skip the required bytes
                    Reader.ReadBytes(Width * Height * BlendFrames * Xdiv * Ydiv * AnimCount * Rare * 2)
                    I += 1
                    If I > 7900 Then
                        MessageBox.Show("Tibia.dat file is too big.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        End
                    End If
                Loop Until False
            Catch Ex As EndOfStreamException
                'ignored
            Catch Ex As Exception
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            Finally
                Reader.Close()
                FS.Close()
            End Try
        End Sub

    End Class

End Module
