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

Imports System.IO, Scripting

Public Module DatReaderModule

    Public Class DatFile
        Implements IDatFile
        Private _Filename As String = ""
        Private DatObjects(7921) As IDatFile.DatObject

        Public ReadOnly Property Length() As Integer Implements IDatFile.Length
            Get
                Return DatObjects.Length
            End Get
        End Property

        Public Function GetInfo(ByVal ItemID As Integer) As IDatFile.DatObject Implements IDatFile.GetInfo
            If ItemID >= DatObjects.Length Then Return Nothing
            Return DatObjects(ItemID)
        End Function

        Public Sub New(ByVal Filename As String)
            ReadDatFile(Filename)
        End Sub

        Public Sub Refresh() Implements IDatFile.Refresh
            ReadDatFile(_Filename)
        End Sub

        Public Sub ReadDatFile(ByVal Filename As String)
            _Filename = Filename
            Dim I As Integer
            Dim B1 As Byte
            For I = 0 To 7921
                DatObjects(I).IsContainer = False
                DatObjects(I).ReadWriteInfo = 0
                DatObjects(I).IsFluidContainer = False
                DatObjects(I).IsStackable = False
                DatObjects(I).MultiType = False
                DatObjects(I).Useable = False
                DatObjects(I).IsNotMovable = False
                DatObjects(I).AlwaysOnTop = False
                DatObjects(I).IsGroundTile = False
                DatObjects(I).Blocking = False
                DatObjects(I).IsPickupable = False
                'DatTiles(I).isblockingProjectile = False
                DatObjects(I).IsWalkable = False
                'DatTiles(I).NoFloorChange = False
                DatObjects(I).BlockPickupable = True
                'DatTiles(I).IsDoor = False
                'DatTiles(I).IsDoorWithLock = False
                DatObjects(I).Speed = 0
                DatObjects(I).CanDecay = True
                DatObjects(I).HasExtraByte = False 'custom flag
                'DatTiles(I).FloorChangeUp = False 'custom flag
                'DatTiles(I).FloorChangeDown = False 'custom flag
                'DatTiles(I).requireRightClick = False 'custom flag
                'DatTiles(I).requireRope = False 'custom flag
                'DatTiles(I).requireShovel = False 'custom flag
                'DatTiles(I).IsWater = False ' custom flag
                DatObjects(I).StackPriority = 1 ' custom flag, higher number, higher priority
                'DatTiles(I).HasFish = False
                'DatTiles(I).IsFood = False
                DatObjects(I).IsField = False
                DatObjects(I).IsDepot = False
                DatObjects(I).MoreAlwaysOnTop = False
                DatObjects(I).Usable2 = False
                'DatTiles(I).MultiCharge = False
            Next
            DatObjects(0).StackPriority = 0
            DatObjects(97).StackPriority = 2
            DatObjects(98).StackPriority = 2
            DatObjects(99).StackPriority = 2
            DatObjects(97).Blocking = True
            DatObjects(98).Blocking = True
            DatObjects(99).Blocking = True

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
                                DatObjects(I).IsGroundTile = True
                                DatObjects(I).Speed = Reader.ReadByte
                                If DatObjects(I).Speed = 0 Then
                                    DatObjects(I).Blocking = True
                                End If
                                Reader.ReadByte() 'ignore one byte
                            Case &H1 'always on top
                                DatObjects(I).MoreAlwaysOnTop = True
                            Case &H2
                                DatObjects(I).AlwaysOnTop = True
                            Case &H3
                                'can walk through, open doors
                                DatObjects(I).AlwaysOnTop = True
                                DatObjects(I).IsWalkable = True
                            Case &H4
                                ' is container
                                DatObjects(I).IsContainer = True
                            Case &H5
                                ' is stackable
                                DatObjects(I).IsStackable = True
                            Case &H6
                                ' is useable
                                DatObjects(I).Useable = True
                            Case &H7
                                ' writtable objects
                                DatObjects(I).Usable2 = True
                            Case &H8
                                'DatTiles(I).multicharge=true
                            Case &H9
                                DatObjects(I).ReadWriteInfo = 3 ' can writen + can be read
                                Reader.ReadByte() 'max chars, 0 is unlimited
                                Reader.ReadByte() 'max new lines
                            Case &HA
                                DatObjects(I).ReadWriteInfo = 1 ' can writen, cant be edited
                                Reader.ReadByte() 'max chars, 0 is unlimited
                                Reader.ReadByte() 'max new lines
                            Case &HB
                                ' is fluid container
                                DatObjects(I).IsFluidContainer = True
                            Case &HC
                                ' multitype
                                DatObjects(I).MultiType = True
                            Case &HD
                                ' is blocking
                                DatObjects(I).Blocking = True
                            Case &HE
                                ' not moveable
                                DatObjects(I).IsNotMovable = True
                            Case &HF
                                ' block missiles
                                'DatTiles(I).isblockingProjectile = True
                            Case &H10
                                ' Slight obstacle (include fields and certain boxes)
                                ' I prefer to don't consider a generic obstable and
                                ' do special cases for fields and ignore the boxes
                            Case &H11
                                DatObjects(I).IsPickupable = True
                            Case &H16
                                ' makes light -- skip bytes
                                Reader.ReadUInt16() 'radius
                                Reader.ReadUInt16() '215 for items, 208 for non items
                            Case &H12
                                ' can see what is under (ladder holes, stairs holes etc)
                            Case &H13
                                'unknown
                            Case &H14
                                'unknown
                            Case &H15 'can be rotated?
                                'unknown
                                'MsgBox(15 & " " & I)
                            Case &H1F
                                ' ground tiles that don't cause level change
                                'DatTiles(I).noFloorChange = True
                            Case &H1A
                                ' mostly blocking items, but also items that can pile up in level (boxes, chairs etc)
                                DatObjects(I).BlockPickupable = False
                                Reader.ReadByte() 'always 8
                                Reader.ReadByte() 'always 0
                            Case &H15
                                'unknown
                            Case &H18
                                'unknown
                            Case &H1D
                                ' for minimap drawing
                                Reader.ReadUInt16() 'two bytes for color
                            Case &H1B
                                ' corpses that don't decay
                                DatObjects(I).CanDecay = False
                            Case &H19
                                'unknown
                                Reader.ReadUInt16() '?
                                Reader.ReadUInt16() '?
                            Case &H1C
                                'MsgBox(I & "->" & &H1C)
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
                                        DatObjects(I).IsDoor = True
                                    Case &H51
                                        'doors with locks
                                        DatObjects(I).IsDoorWithLock = True
                                    Case &H52
                                        'stairs
                                    Case &H53
                                        'mailbox
                                    Case &H54
                                        'depot
                                        DatObjects(I).IsDepot = True
                                    Case &H55
                                        'trash
                                    Case &H56
                                        'hole
                                    Case &H57
                                        'items with special description?
                                    Case &H58
                                        'writtable?
                                        DatObjects(I).ReadWriteInfo = 1 ' read only
                                    Case Else
                                        'debugByte = optByte
                                        ' ignore
                                End Select 'optbyte2
                                Reader.ReadByte() 'always 4 o_o
                            Case Else
                                MsgBox(Opt)
                                ' ignore
                        End Select 'optbyte
                        Opt = Reader.ReadByte
                    End While

                    If DatObjects(I).IsStackable OrElse DatObjects(I).MultiType = True OrElse DatObjects(I).IsFluidContainer Then
                        DatObjects(I).HasExtraByte = True
                    End If
                    If DatObjects(I).AlwaysOnTop = True Then
                        DatObjects(I).StackPriority = 3 ' max priority
                    End If
                    If DatObjects(I).MoreAlwaysOnTop = True Then
                        DatObjects(I).AlwaysOnTop = True
                        DatObjects(I).StackPriority = 4
                    End If
                    If (I >= &H4608 AndAlso I <= &H4F08) OrElse (I >= &H5308 AndAlso I <= &H5A08) Then
                        DatObjects(I).IsField = True
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
                    If I > 7921 Then
                        MessageBox.Show("Tibia.dat file is too big.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        End
                    End If
                Loop Until False

            Catch Ex As EndOfStreamException
                'ignored
                'MsgBox(I)
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
