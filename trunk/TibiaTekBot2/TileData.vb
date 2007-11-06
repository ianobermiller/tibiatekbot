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
'    Boston, MA 02111-1307, USA.Imports System.Math

Imports System.Math

Public Module TileDataModule

    Public Class TileData
        Public TileNum As Integer = -1
        Public Count As Integer
        Public TileId As Integer
        Public ObjectId(0 To 9) As Integer
        Public ObjectInfo(0 To 9) As Integer
        Public Pos As LocationDefinition

        Public Sub Get_TileInfo()
            Try
                If TileNum = -1 Then Exit Sub
                Dim MapTile_Address As Integer
                Dim Map_Begin As Integer

                Core.ReadMemory(Consts.ptrMapPointer, Map_Begin, 4)
                MapTile_Address = Map_Begin + (TileNum * Consts.MapTileDist) + 4

                'Getting info
                Core.ReadMemory(MapTile_Address - 4, Count, 2)
                If Count > 9 Then Exit Sub
                Core.ReadMemory(MapTile_Address, TileId, 2)
                Pos.X = Tile_Coords(Coords.X)
                Pos.Y = Tile_Coords(Coords.Y)
                Pos.Z = Tile_Coords(Coords.Z)

                For N As Integer = 0 To Count - 1
                    Core.ReadMemory(MapTile_Address + (Consts.MapObjectDist * N) + (Consts.MapObjectIdOffset - 4), ObjectId(N), 4)
                    Core.ReadMemory(MapTile_Address + (Consts.MapObjectDist * N) + (Consts.MapObjectDataOffset - 4), ObjectInfo(N), 4)
                Next N
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Function Tile_Coords(ByVal xyz As Coords) As Integer
            Try
                Dim Z, Y, X As Integer
                Select Case xyz
                    Case Coords.Z
                        Z = Fix(TileNum / (14 * 18))
                        Return Z
                    Case Coords.Y
                        Z = Fix(TileNum / (14 * 18))
                        Y = Fix((TileNum - Z * 14 * 18) / 18)
                        Return Y
                    Case Coords.X
                        Z = Fix(TileNum / (14 * 18))
                        Y = Fix((TileNum - Z * 14 * 18) / 18)
                        X = Fix((TileNum - Z * 14 * 18 - Y * 18))
                        Return X
                    Case Else
                        Exit Function
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub Reset()
            TileNum = 0
        End Sub

        Public Sub New()
            TileNum = 0
        End Sub

        Public Function JumpToTile(ByVal Tile As SpecialTile) As Boolean
            Try
                TileNum = 0
                Select Case Tile
                    Case SpecialTile.Myself
                        For i As Integer = 0 To 2015
                            Get_TileInfo()
                            If Count > 1 Then
                                For E As Integer = 0 To Count - 1
                                    If ObjectId(E) = &H63 Then
                                        If ObjectInfo(E) = Core.CharacterID Then
                                            TileNum = i ' WE HAVE IT
                                            Return True
                                        End If
                                    End If
                                Next
                            End If
                            TileNum += 1
                        Next i
                End Select
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function FindTile(ByVal Location As LocationDefinition, Optional ByVal MapCoords As Boolean = True) As Boolean
            Try
                Dim mapX, mapY, mapZ As Integer

                If MapCoords = False Then
                    mapX = ConvertGlobalToMap(Coords.X, Location.X)
                    mapY = ConvertGlobalToMap(Coords.Y, Location.Y)
                    mapZ = ConvertGlobalToMap(Coords.Z, Location.Z)
                Else
                    mapX = Location.X
                    mapY = Location.Y
                    mapZ = Location.Z
                End If
                TileNum = 0
                For i As Integer = 0 To 2015
                    Get_TileInfo()
                    If Pos.X = mapX And Pos.Y = mapY And Pos.Z = mapZ Then
                        TileNum = i
                        Return True
                    End If
                    TileNum += 1
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function ConvertGlobalToMap(ByVal Axis As Coords, ByVal Coordinant As Integer) As Integer
            Try
                Dim OwnGPos, OwnPos As LocationDefinition
                Dim Space As Integer
                Dim TD As New TileData
                Dim K, S As Integer
                OwnGPos = Core.CharacterLoc
                TD.JumpToTile(SpecialTile.Myself)
                TD.Get_TileInfo()
                OwnPos = TD.Pos
                Select Case Axis
                    Case Coords.X
                        Space = OwnGPos.X - Coordinant
                        If Space < 0 Then
                            If OwnPos.X - Space > 17 Then
                                K = 17 - OwnPos.X
                                Return Abs(Space) - K - 1
                            Else
                                Return OwnPos.X + Abs(Space)
                            End If
                        End If
                        If Space > 0 Then
                            If OwnPos.X - Space < 0 Then
                                K = OwnPos.X
                                S = Abs(Space) - K
                                Return 17 - S + 1
                            Else
                                Return OwnPos.X - Abs(Space)
                            End If
                        End If
                        If Space = 0 Then
                            Return OwnPos.X
                        End If
                    Case Coords.Y
                        Space = OwnGPos.Y - Coordinant
                        If Space < 0 Then
                            If OwnPos.Y - Space > 13 Then
                                K = 13 - OwnPos.Y
                                Return Abs(Space) - K - 1
                            Else
                                Return OwnPos.Y + Abs(Space)
                            End If
                        End If
                        If Space > 0 Then
                            If OwnPos.Y - Space < 0 Then
                                K = OwnPos.Y
                                S = Abs(Space) - K
                                Return 13 - S + 1
                            Else
                                Return OwnPos.Y - Abs(Space)
                            End If
                        End If
                        If Space = 0 Then
                            Return OwnPos.Y
                        End If
                    Case Coords.Z
                        Space = OwnGPos.Z - Coordinant
                        If Space < 0 Then
                            If OwnPos.Z - Space > 7 Then
                                K = 7 - OwnPos.Z
                                Return Abs(Space) - K - 1
                            Else
                                Return OwnPos.Z + Abs(Space)
                            End If
                        End If
                        If Space > 0 Then
                            If OwnPos.Z - Space < 0 Then
                                K = OwnPos.Z
                                S = Abs(Space) - K
                                Return 7 - S + 1
                            Else
                                Return OwnPos.Z - Abs(Space)
                            End If
                        End If
                        If Space = 0 Then
                            Return OwnPos.Z
                        End If
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Enum Coords
            X
            Y
            Z
        End Enum

        Public Enum SpecialTile
            Myself
        End Enum
    End Class
End Module
