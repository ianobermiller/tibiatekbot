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

            Dim I As Integer = 100
            'For I = 0 To 7921
            '    DatObjects(I).IsContainer = False
            '    DatObjects(I).ReadWriteInfo = 0
            '    DatObjects(I).IsFluidContainer = False
            '    DatObjects(I).IsStackable = False
            '    DatObjects(I).Splash = False
            '    DatObjects(I).Corpse = False
            '    DatObjects(I).IsNotMovable = False
            '    DatObjects(I).TopOrder2 = False
            '    DatObjects(I).IsGroundTile = False
            '    DatObjects(I).Blocking = False
            '    DatObjects(I).IsPickupable = False
            '    'DatTiles(I).isblockingProjectile = False
            '    DatObjects(I).IsWalkable = False
            '    'DatTiles(I).NoFloorChange = False
            '    DatObjects(I).BlockPickupable = True
            '    'DatTiles(I).IsDoor = False
            '    'DatTiles(I).IsDoorWithLock = False
            '    DatObjects(I).Speed = 0
            '    DatObjects(I).CanDecay = True
            '    DatObjects(I).HasExtraByte = False 'custom flag
            '    'DatTiles(I).FloorChangeUp = False 'custom flag
            '    'DatTiles(I).FloorChangeDown = False 'custom flag
            '    'DatTiles(I).requireRightClick = False 'custom flag
            '    'DatTiles(I).requireRope = False 'custom flag
            '    'DatTiles(I).requireShovel = False 'custom flag
            '    'DatTiles(I).IsWater = False ' custom flag
            '    'DatTiles(I).HasFish = False
            '    'DatTiles(I).IsFood = False
            '    DatObjects(I).IsField = False
            '    DatObjects(I).IsDepot = False
            '    DatObjects(I).TopOrder1 = False
            '    DatObjects(I).Usable2 = False
            '    'DatTiles(I).MultiCharge = False
            'Next
            DatObjects(97).Blocking = True
            DatObjects(98).Blocking = True
            DatObjects(99).Blocking = True
            Dim Action As Byte = 0
            Dim FS As New FileStream(Filename, FileMode.Open, FileAccess.Read)
            Dim Reader As New BinaryReader(FS)
            Try
                Reader.ReadUInt32() 'signature
                Reader.ReadUInt16() 'max client id
                Reader.ReadUInt16() 'monsters
                Reader.ReadUInt16() 'effects
                Reader.ReadUInt16() 'distance
                Reader.ReadByte() 'header
                Dim Opt As Byte = 0
                Do

                    Opt = Reader.ReadByte
                    While Opt <> &HFF
                        'If I = &HDB0 Then MsgBox("position:" & Reader.BaseStream.Position - 1)
                        With DatObjects(I)

                            Select Case Opt
                                Case &H0 ' has speed
                                    .Speed = Reader.ReadByte
                                Case &H1 ' top order 1
                                    .TopOrder = 1
                                Case &H2 ' top order 2
                                    .TopOrder = 2
                                Case &H3 ' top order 3
                                    .TopOrder = 3
                                Case &H4 ' container
                                    .IsContainer = True
                                Case &H5 ' stackable
                                    .IsStackable = True
                                Case &H6 ' corpse
                                    .IsCorpse = True
                                Case &H7 ' usable
                                    .IsUsable = True
                                Case &H8 'rune
                                    .IsRune = True
                                Case &H9 'writable
                                    .IsWritable = True
                                    Reader.ReadUInt16() 'text limit
                                Case &HA ' readable
                                    .IsReadable = True
                                    Reader.ReadUInt16()
                                Case &HB ' is fluid
                                    .IsFluid = True
                                Case &HC ' splash
                                    .IsSplash = True
                                Case &HD  ' blocking
                                    .Blocking = True
                                Case &HE ' not moveable
                                    .IsImmovable = True
                                Case &HF ' block missiles
                                    .BlocksMissile = True
                                Case &H10
                                    .BlocksPath = True
                                Case &H11
                                    .IsPickupable = True
                                Case &H12 'hangable
                                    .IsHangable = True
                                Case &H13 'horizontal hangable
                                    .IsHangableHorizontal = True
                                Case &H14 'vertical hangable
                                    .IsHangableVertical = True
                                Case &H15 'rotatable
                                    .IsRotatable = True
                                Case &H16 ' has light
                                    .IsLightSource = True
                                    Reader.ReadUInt16() 'radius
                                    Reader.ReadUInt16() '215 for items, 208 for non items
                                Case &H18 'floor change
                                    .FloorChange = True
                                Case &H19 'offset of sprite in screen
                                    Reader.ReadUInt16() 'x offset
                                    Reader.ReadUInt16() 'y offset
                                Case &H1A 'height
                                    .IsHeighted = True
                                    Reader.ReadUInt16()
                                Case &H1B 'layer (players appears on top of other sprites to item)
                                    .IsLayer = True
                                Case &H1C ' idle animation
                                    .IsIdleAnimation = True
                                Case &H1D ' for minimap drawing
                                    .HasMiniMapColor = True
                                    Reader.ReadUInt16() 'two bytes for color
                                Case &H1E ' actions
                                    .HasActions = True
                                    Action = Reader.ReadByte()
                                    Select Case Action
                                        Case &H4C 'ladders
                                            .IsLadder = True
                                        Case &H4D 'crate, trap door?
                                            .IsSewer = True
                                        Case &H4E 'rope spot?
                                            .IsRopeSpot = True
                                        Case &H4F 'switch
                                            .IsSwitch = True
                                        Case &H50 'doors
                                            .IsDoor = True
                                        Case &H51 'doors with locks
                                            .IsDoorWithLock = True
                                        Case &H52 'stairs
                                            .IsStairs = True
                                        Case &H53 'mailbox
                                            .IsMailbox = True
                                        Case &H54 'depot
                                            .IsDepot = True
                                        Case &H55 'trash
                                            .IsTrash = True
                                        Case &H56 'hole
                                            .IsHole = True
                                        Case &H57 'items with special description?
                                            .HasSpecialDescription = True
                                        Case &H58 'writtable?
                                            .IsReadOnly = True
                                        Case Else
                                            MsgBox(Hex(Action))
                                            'debugByte = optByte
                                            ' ignore
                                    End Select 'optbyte2
                                    Reader.ReadByte() 'always 4 o_o
                                Case &H1F
                                    .IsGround = True
                                    ' ground tiles that don't cause level change                                
                                Case Else
                                    'MsgBox(Opt)
                                    ' ignore
                            End Select 'optbyte
                            Opt = Reader.ReadByte
                        End With
                    End While

                    DatObjects(I).HasExtraByte = DatObjects(I).IsStackable OrElse DatObjects(I).IsRune OrElse DatObjects(I).IsSplash OrElse DatObjects(I).IsFluid
                    If I = &H389 Then DatObjects(&H389).IsRune = False
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
                    Dim Rare As Integer = Reader.ReadByte()
                    Reader.ReadBytes(Width * Height * BlendFrames * Xdiv * Ydiv * AnimCount * Rare * 2)
                    I += 1
                    If I > 7921 Then
                        MessageBox.Show("Tibia.dat file is too big.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        End
                    End If
                Loop Until False

            Catch Ex As EndOfStreamException
            Catch Ex As Exception
                MessageBox.Show("ItemID: " & I & "." & vbCrLf & "TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            Finally
                Reader.Close()
                FS.Close()
            End Try
        End Sub

    End Class

End Module
