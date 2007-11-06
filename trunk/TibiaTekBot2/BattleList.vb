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

Public Module BattleListModule

    Public Class BattleList
        Private IndexPosition As Integer
        Private ID As Integer

        Public ReadOnly Property GetIndexPosition() As Integer
            Get
                Try
                    Return IndexPosition
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetFloor() As Integer
            Get
                Try
                    Dim Floor As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Floor, 1)
                    Return Floor
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetEntityID() As Integer
            Get
                Try
                    Dim ID As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    Return ID
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function WalkTo() As Boolean
            Try
                Dim Location As LocationDefinition = GetLocation
                If Location.Z = Core.CharacterLoc.Z Then
                    Core.WriteMemory(Consts.ptrGoToX, CInt(Location.X), 2)
                    Core.WriteMemory(Consts.ptrGoToY, CInt(Location.Y), 2)
                    Core.WriteMemory(Consts.ptrGoToZ, CInt(Location.Z), 1)
                    Core.WriteMemory(Consts.ptrGo, 1, 1)
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetName() As String
            Get
                Try
                    Dim Name As String = ""
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLNameOffset, Name)
                    If String.IsNullOrEmpty(Name) Then
                        Return "Unknown Entity"
                    Else
                        Return Name
                    End If
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetHPPercentage() As UShort
            Get
                Try
                    Dim HPPercentage As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHPPercentOffset, HPPercentage, 1)
                    Return CUShort(HPPercentage)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsOnScreen() As Boolean
            Get
                Try
                    Dim OnScreen As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, OnScreen, 1)
                    If OnScreen = 1 Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDirection() As Integer
            Get
                Try
                    Dim Dir As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLDirectionOffset, Dir, 1)
                    Return Dir
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDistance(Optional ByVal IncludeFloor As Boolean = False) As Double
            Get
                Try
                    If IsMyself Then Return 0
                    Dim Loc As LocationDefinition = GetLocation
                    Dim Dist As Double = 0.0
                    Dim X, Y, Z As Integer
                    X = Abs(CInt(Core.CharacterLoc.X) - CInt(Loc.X))
                    Y = Abs(CInt(Core.CharacterLoc.Y) - CInt(Loc.Y))
                    Z = 0
                    If IncludeFloor Then
                        Z = Abs(CInt(Core.CharacterLoc.Z) - CInt(Loc.Z))
                    End If
                    Dist = Sqrt(Pow(X, 2) + Pow(Y, 2) + Pow(Z, 2))
                    Return Dist
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDistanceFromLocation(ByVal Loc As LocationDefinition, Optional ByVal IncludeFloor As Boolean = False, Optional ByVal CurrentIndex As Boolean = False) As Double
            Get
                Try
                    Core.ReadMemory(Consts.ptrCoordX, Core.CharacterLoc.X, 4)
                    Core.ReadMemory(Consts.ptrCoordY, Core.CharacterLoc.Y, 4)
                    Core.ReadMemory(Consts.ptrCoordZ, Core.CharacterLoc.Z, 4)
                    'Log("GetDistanceFromLocation", String.Format("Loc = ({0},{1},{2}), CharLoc = ({3},{4},{5})", Loc.X, Loc.Y, Loc.Z, AppObjs.CharacterLoc.X, AppObjs.CharacterLoc.Y, AppObjs.CharacterLoc.Z))
                    Dim Dist As Double = 0.0
                    Dim X, Y, Z As Integer
                    If CurrentIndex = True Then
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                        X = Abs(CInt(X) - CInt(Loc.X))
                        Y = Abs(CInt(Y) - CInt(Loc.Y))
                        Z = 0
                        If IncludeFloor Then
                            Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
                            Z = Abs(CInt(Z) - CInt(Loc.Z))
                        End If
                    Else
                        X = Abs(CInt(Core.CharacterLoc.X) - CInt(Loc.X))
                        Y = Abs(CInt(Core.CharacterLoc.Y) - CInt(Loc.Y))
                        Z = 0
                        If IncludeFloor Then
                            Z = Abs(CInt(Core.CharacterLoc.Z) - CInt(Loc.Z))
                        End If
                    End If
                    Dist = Sqrt(Pow(X, 2) + Pow(Y, 2) + Pow(Z, 2))
                    Return Dist
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Property Speed() As Integer
            Get
                Try
                    Dim CurrentSpeed As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSpeedOffset, CurrentSpeed, 4)
                    Return CurrentSpeed
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewSpeed As Integer)
                Try
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSpeedOffset, NewSpeed, 4)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LightIntensity() As Integer
            Get
                Try
                    Dim CurrentLightI As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightIntensityOffset, CurrentLightI, 1)
                    Return CurrentLightI
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewLightI As Integer)
                Try
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightIntensityOffset, NewLightI, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LightColor() As Integer
            Get
                Try
                    Dim CurrentLightC As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightColorOffset, CurrentLightC, 1)
                    Return CurrentLightC
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewLightC As Integer)
                Try
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightColorOffset, NewLightC, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property IsWalking() As Boolean
            Get
                Try
                    Dim WalkingState As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, WalkingState, 1)
                    If WalkingState = 0 Then Return False Else Return True
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewValue As Boolean)
                Try
                    If NewValue = True Then
                        Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, 1, 1)
                    Else
                        Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, 0, 1)
                    End If
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public ReadOnly Property IsMyself() As Boolean
            Get
                Try
                    Dim ID As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = Core.CharacterID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetLocation() As LocationDefinition
            Get
                Try
                    Dim Loc As New LocationDefinition
                    Dim X, Y, Z As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
                    Loc.X = CUShort(X)
                    Loc.Y = CUShort(Y)
                    Loc.Z = CInt(Z)
                    Return Loc
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsFollowed() As Boolean
            Get
                Try
                    Dim FollowedID, ID As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = 0 Then Return False
                    Core.ReadMemory(Consts.ptrFollowedEntityID, FollowedID, 4)
                    If ID = FollowedID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsAttacked() As Boolean
            Get
                Try
                    Dim AttackedID, ID As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = 0 Then Return False
                    Core.ReadMemory(Consts.ptrAttackedEntityID, AttackedID, 4)
                    If ID = AttackedID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function Find(ByVal Name As String, Optional ByVal OnScreen As Boolean = False) As Boolean
            Try
                Dim IsOnScreen As Integer = 0
                Dim CurrentName As String = ""
                For IndexPosition = 0 To Consts.BLMax - 1
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLNameOffset, CurrentName)
                    If [String].Compare(CurrentName, Name, True) = 0 Then
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
                        If OnScreen AndAlso IsOnScreen = 0 Then Return False
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function 'Find By Name

        Public Function Find(ByVal ID As Integer, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean
            Try
                Dim EntityID As Integer
                For IndexPosition = 0 To Consts.BLMax - 1
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), EntityID, 4)
                    If ID = EntityID Then
                        If MustBeOnScreen AndAlso Not IsOnScreen Then Return False
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function 'Find By ID

        Public Function Find(ByVal Loc As LocationDefinition, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean
            Try
                Dim CurrentLoc As New LocationDefinition

                For IndexPosition = 0 To Consts.BLMax - 1
                    Dim X, Y, Z As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
                    CurrentLoc.X = CUShort(X)
                    CurrentLoc.Y = CUShort(Y)
                    CurrentLoc.Z = CInt(Z)

                    If Loc.X = CurrentLoc.X And Loc.Y = CurrentLoc.Y And Loc.Z = CurrentLoc.Z Then
                        If MustBeOnScreen AndAlso Not IsOnScreen Then Return False
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function JumpToEntity(ByVal Type As SpecialEntity) As Boolean
            Try
                Dim Found As Boolean = False
                Dim ID As Integer
                For IndexPosition = 0 To Consts.BLMax - 1
                    Select Case Type
                        Case SpecialEntity.Myself
                            Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                            If ID = Core.CharacterID Then
                                Return True
                            End If

                        Case SpecialEntity.Attacked
                            If IsAttacked Then Return True
                        Case SpecialEntity.Followed
                            If IsFollowed Then Return True
                    End Select
                Next
                IndexPosition = 0
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Property HeadColor() As Integer
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHeadCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHeadCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property BodyColor() As Integer
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLBodyCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLBodyCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LegsColor() As Integer
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLegsCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLegsCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property FeetColor() As Integer
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLFeetCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLFeetCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property OutfitID() As UShort
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOutfitOffset, Result, 2)
                    Return CUShort(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewOutfit As UShort)
                Try
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOutfitOffset, NewOutfit, 2)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property Addons() As Addons
            Get
                Try
                    Dim Result As Integer = 0
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLAddonsOffset, Result, 1)
                    Return CType(Result, Addons)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewAddons As Addons)
                Try
                    Dim Input As Integer = CInt(NewAddons)
                    Core.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLAddonsOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Shared Function IsPlayer(ByVal EntityID As Integer) As Boolean
            Return (EntityID < &H40000000)
        End Function

        Public Function IsPlayer() As Boolean
            Try
                Dim ID As Integer
                Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                Return (ID < &H40000000)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetPartyStatus() As PartyStatus
            Get
                Try
                    Dim PStatus As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLPartyOffset, PStatus, 1)
                    Dim PS As PartyStatus
                    Select Case PStatus
                        Case 0
                            PS = PartyStatus.None
                        Case 1
                            PS = PartyStatus.Unknown
                        Case 2
                            PS = PartyStatus.Invited
                        Case 3
                            PS = PartyStatus.Member
                        Case 4
                            PS = PartyStatus.Leader
                        Case Else
                            PS = PartyStatus.Unknown
                    End Select
                    Return PS
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetSkullMark() As SkullMark
            Get
                Try
                    Dim SMark As Integer
                    Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSkullOffset, SMark, 1)
                    Dim SM As SkullMark
                    Select Case SMark
                        Case 0
                            SM = SkullMark.None
                        Case 1
                            SM = SkullMark.Yellow
                        Case 2
                            SM = SkullMark.Green
                        Case 3
                            SM = SkullMark.White
                        Case 4
                            SM = SkullMark.Red
                        Case Else
                            SM = SkullMark.None
                    End Select
                    Return SM
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Sub New()
            IndexPosition = 0
        End Sub

        Public Sub New(ByVal Position As Integer)
            Try
                If (Position > CInt(Consts.BLMax) - 1) Then
                    Me.IndexPosition = 255
                Else
                    Me.IndexPosition = Position
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub New(ByVal SE As SpecialEntity)
            JumpToEntity(SE)
        End Sub

        Public Function Reset(Optional ByVal OnScreen As Boolean = False) As Boolean
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = 0 To Consts.BLMax - 1
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
                        If IsOnScreen = 1 Then Return True
                    Next
                Else
                    IndexPosition = 0
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Sub Attack()
            Try
                Dim ID As Integer = GetEntityID
                Core.WriteMemory(Consts.ptrAttackedEntityID, ID, 4)
                Core.Proxy.SendPacketToServer(AttackEntity(CUInt(ID)))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function NextEntity(Optional ByVal OnScreen As Boolean = False) As Boolean
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = IndexPosition + 1 To Consts.BLMax - 1
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
                        If IsOnScreen = 1 Then
                            Return True
                        End If
                    Next
                Else
                    Me.IndexPosition += 1
                    If Me.IndexPosition = Consts.BLMax Then
                        Me.IndexPosition = Consts.BLMax - 1
                        Return False
                    Else
                        Return True
                    End If
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function PrevEntity(Optional ByVal OnScreen As Boolean = False) As Boolean
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = IndexPosition - 1 To 0 Step -1
                        Core.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
                        If IsOnScreen = 1 Then Return True
                    Next
                Else
                    IndexPosition -= 1
                    If IndexPosition = -1 Then
                        IndexPosition = 0
                        Return False
                    End If
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function
        Public Shared Function CreaturesOnScreen() As Boolean
            Try
                Dim BL As New BattleList
                BL.Reset()
                Do
                    If Not BL.IsPlayer Then
                        If BL.IsOnScreen Then
                            Return True
                        End If
                    End If
                Loop While BL.NextEntity = True
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

    End Class

End Module
