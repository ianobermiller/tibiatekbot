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

Imports System.Math, Scripting

Public Module BattleListModule

    Public Class BattleList
        Implements IBattlelist

        Private IndexPosition As Integer
        Private ID As Integer

        Public ReadOnly Property GetIndexPosition() As Integer Implements IBattlelist.GetIndexPosition
            Get
                Try
                    Return IndexPosition
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetFloor() As Integer Implements IBattlelist.Floor
            Get
                Try
                    Dim Floor As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Floor, 1)
                    Return Floor
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetEntityID() As Integer Implements IBattlelist.EntityID
            Get
                Try
                    Dim ID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    Return ID
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function WalkTo() As Boolean Implements IBattlelist.WalkTo
            Try
                Dim Location As ITibia.LocationDefinition = GetLocation
                If Location.Z = Kernel.CharacterLoc.Z Then
                    Kernel.Client.WriteMemory(Consts.ptrGoToX, CInt(Location.X), 2)
                    Kernel.Client.WriteMemory(Consts.ptrGoToY, CInt(Location.Y), 2)
                    Kernel.Client.WriteMemory(Consts.ptrGoToZ, CInt(Location.Z), 1)
                    Kernel.Client.WriteMemory(Consts.ptrGo, 1, 1)
                    Return True
                End If
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetName() As String Implements IBattlelist.GetName
            Get
                Try
                    Dim Name As String = ""
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLNameOffset, Name)
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

        Public ReadOnly Property GetHPPercentage() As Integer Implements IBattlelist.GetHPPercentage
            Get
                Try
                    Dim HPPercentage As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHPPercentOffset, HPPercentage, 1)
                    Return HPPercentage
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsOnScreen() As Boolean Implements IBattlelist.IsOnScreen
            Get
                Try
                    Dim OnScreen As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, OnScreen, 1)
                    If OnScreen = 1 Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDirection() As IBattlelist.Directions Implements IBattlelist.Direction
            Get
                Try
                    Dim Dir As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLDirectionOffset, Dir, 1)
                    Return Dir
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDistance(Optional ByVal IncludeFloor As Boolean = False) As Double Implements IBattlelist.Distance
            Get
                Try
                    If IsMyself Then Return 0
                    Dim Loc As ITibia.LocationDefinition = GetLocation
                    Dim Dist As Double = 0.0
                    Dim X, Y, Z As Integer
                    X = Abs(CInt(Kernel.CharacterLoc.X) - CInt(Loc.X))
                    Y = Abs(CInt(Kernel.CharacterLoc.Y) - CInt(Loc.Y))
                    Z = 0
                    If IncludeFloor Then
                        Z = Abs(CInt(Kernel.CharacterLoc.Z) - CInt(Loc.Z))
                    End If
                    Dist = Sqrt(Pow(X, 2) + Pow(Y, 2) + Pow(Z, 2))
                    Return Dist
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetDistanceFromLocation(ByVal Loc As ITibia.LocationDefinition, Optional ByVal IncludeFloor As Boolean = False, Optional ByVal CurrentIndex As Boolean = False) As Double Implements IBattlelist.GetDistanceFromLocation
            Get
                Try
                    Kernel.Client.ReadMemory(Consts.ptrCoordX, Kernel.CharacterLoc.X, 4)
                    Kernel.Client.ReadMemory(Consts.ptrCoordY, Kernel.CharacterLoc.Y, 4)
                    Kernel.Client.ReadMemory(Consts.ptrCoordZ, Kernel.CharacterLoc.Z, 4)
                    'Log("GetDistanceFromLocation", String.Format("Loc = ({0},{1},{2}), CharLoc = ({3},{4},{5})", Loc.X, Loc.Y, Loc.Z, AppObjs.CharacterLoc.X, AppObjs.CharacterLoc.Y, AppObjs.CharacterLoc.Z))
                    Dim Dist As Double = 0.0
                    Dim X, Y, Z As Integer
                    If CurrentIndex = True Then
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                        X = Abs(CInt(X) - CInt(Loc.X))
                        Y = Abs(CInt(Y) - CInt(Loc.Y))
                        Z = 0
                        If IncludeFloor Then
                            Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
                            Z = Abs(CInt(Z) - CInt(Loc.Z))
                        End If
                    Else
                        X = Abs(CInt(Kernel.CharacterLoc.X) - CInt(Loc.X))
                        Y = Abs(CInt(Kernel.CharacterLoc.Y) - CInt(Loc.Y))
                        Z = 0
                        If IncludeFloor Then
                            Z = Abs(CInt(Kernel.CharacterLoc.Z) - CInt(Loc.Z))
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

        Public Property Speed() As Integer Implements IBattlelist.Speed
            Get
                Try
                    Dim CurrentSpeed As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSpeedOffset, CurrentSpeed, 4)
                    Return CurrentSpeed
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewSpeed As Integer)
                Try
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSpeedOffset, NewSpeed, 4)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LightIntensity() As Integer Implements IBattlelist.LightIntensity
            Get
                Try
                    Dim CurrentLightI As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightIntensityOffset, CurrentLightI, 1)
                    Return CurrentLightI
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewLightI As Integer)
                Try
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightIntensityOffset, NewLightI, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LightColor() As Integer Implements IBattlelist.LightColor
            Get
                Try
                    Dim CurrentLightC As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightColorOffset, CurrentLightC, 1)
                    Return CurrentLightC
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewLightC As Integer)
                Try
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLightColorOffset, NewLightC, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property IsWalking() As Boolean Implements IBattlelist.IsWalking
            Get
                Try
                    Dim WalkingState As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, WalkingState, 1)
                    If WalkingState = 0 Then Return False Else Return True
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewValue As Boolean)
                Try
                    If NewValue = True Then
                        Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, 1, 1)
                    Else
                        Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLWalkingOffset, 0, 1)
                    End If
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public ReadOnly Property IsMyself() As Boolean Implements IBattlelist.IsMyself
            Get
                Try
                    Dim ID As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = Kernel.CharacterID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetLocation() As ITibia.LocationDefinition Implements IBattlelist.Location
            Get
                Try
                    Dim Loc As New ITibia.LocationDefinition
                    Dim X, Y, Z As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
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

        Public ReadOnly Property IsFollowed() As Boolean Implements IBattlelist.IsFollowed
            Get
                Try
                    Dim FollowedID, ID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = 0 Then Return False
                    Kernel.Client.ReadMemory(Consts.ptrFollowedEntityID, FollowedID, 4)
                    If ID = FollowedID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property IsAttacked() As Boolean Implements IBattlelist.IsAttacked
            Get
                Try
                    Dim AttackedID, ID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                    If ID = 0 Then Return False
                    Kernel.Client.ReadMemory(Consts.ptrAttackedEntityID, AttackedID, 4)
                    If ID = AttackedID Then Return True Else Return False
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public Function Find(ByVal Name As String, Optional ByVal OnScreen As Boolean = False) As Boolean Implements IBattlelist.Find
            Try
                Dim IsOnScreen As Integer = 0
                Dim CurrentName As String = ""
                For IndexPosition = 0 To Consts.BLMax - 1
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLNameOffset, CurrentName)
                    If [String].Compare(CurrentName, Name, True) = 0 Then
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
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

        Public Function Find(ByVal ID As Integer, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean Implements IBattlelist.Find
            Try
                Dim EntityID As Integer
                For IndexPosition = 0 To Consts.BLMax - 1
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), EntityID, 4)
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

        Public Function Find(ByVal Loc As ITibia.LocationDefinition, Optional ByVal MustBeOnScreen As Boolean = False) As Boolean Implements IBattlelist.Find
            Try
                Dim CurrentLoc As New ITibia.LocationDefinition

                For IndexPosition = 0 To Consts.BLMax - 1
                    Dim X, Y, Z As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordXOffset, X, 2)
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordYOffset, Y, 2)
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLCoordZOffset, Z, 1)
                    CurrentLoc.X = X
                    CurrentLoc.Y = Y
                    CurrentLoc.Z = Z

                    If Loc.X = CurrentLoc.X And Loc.Y = CurrentLoc.Y And Loc.Z = CurrentLoc.Z Then
                        If MustBeOnScreen AndAlso Not IsOnScreen Then Continue For
                        Return True
                    End If
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function JumpToEntity(ByVal Type As IBattlelist.SpecialEntity) As Boolean Implements IBattlelist.JumpToEntity
            Try
                Dim Found As Boolean = False
                Dim ID As Integer
                For IndexPosition = 0 To Consts.BLMax - 1
                    Select Case Type
                        Case IBattlelist.SpecialEntity.Myself
                            Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                            If ID = Kernel.CharacterID Then
                                Return True
                            End If
                        Case IBattlelist.SpecialEntity.Attacked
                            If IsAttacked Then Return True
                        Case IBattlelist.SpecialEntity.Followed
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

        Public Property HeadColor() As Integer Implements IBattlelist.HeadColor
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHeadCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLHeadCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property BodyColor() As Integer Implements IBattlelist.BodyColor
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLBodyCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLBodyCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property LegsColor() As Integer Implements IBattlelist.LegsColor
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLegsCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLLegsCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property FeetColor() As Integer Implements IBattlelist.FeetColor
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLFeetCOffset, Result, 1)
                    Return CInt(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewColor As Integer)
                Try
                    Dim Input As Integer = CInt(NewColor)
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLFeetCOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property OutfitID() As Integer Implements IBattlelist.OutfitID
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOutfitOffset, Result, 2)
                    Return CUShort(Result)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewOutfit As Integer)
                Try
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOutfitOffset, NewOutfit, 2)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property

        Public Property OutfitAddons() As IBattlelist.OutfitAddons Implements IBattlelist.GetAddons
            Get
                Try
                    Dim Result As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLAddonsOffset, Result, 1)
                    Return CType(Result, IBattlelist.OutfitAddons)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
            Set(ByVal NewAddons As IBattlelist.OutfitAddons)
                Try
                    Dim Input As Integer = CInt(NewAddons)
                    Kernel.Client.WriteMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLAddonsOffset, Input, 1)
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Set
        End Property


        Public Function IsPlayer(ByVal EntityID As Integer) As Boolean Implements IBattlelist.IsPlayer
            Return (EntityID < &H40000000)
        End Function

        Public Function IsPlayer() As Boolean Implements IBattlelist.IsPlayer
            Try
                Dim ID As Integer
                Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist), ID, 4)
                Return (ID < &H40000000)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public ReadOnly Property GetPartyStatus() As IBattlelist.PartyStatus Implements IBattlelist.GetPartyStatus
            Get
                Try
                    Dim PStatus As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLPartyOffset, PStatus, 1)
                    Dim PS As IBattlelist.PartyStatus
                    Select Case PStatus
                        Case 0
                            PS = IBattlelist.PartyStatus.None
                        Case 1
                            PS = IBattlelist.PartyStatus.Unknown
                        Case 2
                            PS = IBattlelist.PartyStatus.Invited
                        Case 3
                            PS = IBattlelist.PartyStatus.Member
                        Case 4
                            PS = IBattlelist.PartyStatus.Leader
                        Case Else
                            PS = IBattlelist.PartyStatus.Unknown
                    End Select
                    Return PS
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End Get
        End Property

        Public ReadOnly Property GetSkullMark() As IBattlelist.SkullMark Implements IBattlelist.GetSkullMark
            Get
                Try
                    Dim SMark As Integer
                    Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLSkullOffset, SMark, 1)
                    Dim SM As IBattlelist.SkullMark
                    Select Case SMark
                        Case 0
                            SM = IBattlelist.SkullMark.None
                        Case 1
                            SM = IBattlelist.SkullMark.Yellow
                        Case 2
                            SM = IBattlelist.SkullMark.Green
                        Case 3
                            SM = IBattlelist.SkullMark.White
                        Case 4
                            SM = IBattlelist.SkullMark.Red
                        Case Else
                            SM = IBattlelist.SkullMark.None
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

        Public Sub New(ByVal SE As IBattlelist.SpecialEntity)
            JumpToEntity(SE)
        End Sub

        Public Function Reset(Optional ByVal OnScreen As Boolean = False) As Boolean Implements IBattlelist.Reset
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = 0 To Consts.BLMax - 1
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
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

        Public Sub Attack() Implements IBattlelist.Attack
            Try
                Dim ID As Integer = GetEntityID
                Kernel.Client.WriteMemory(Consts.ptrAttackedEntityID, ID, 4)
                Dim ServerPacket As New ServerPacketBuilder(Kernel.Proxy)
                ServerPacket.AttackEntity(ID)
                ServerPacket.Send()
                'Core.Proxy.SendPacketToServer(AttackEntity(CUInt(ID)))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function NextEntity(Optional ByVal OnScreen As Boolean = False) As Boolean Implements IBattlelist.NextEntity
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = IndexPosition + 1 To Consts.BLMax - 1
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
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

        Public Function PrevEntity(Optional ByVal OnScreen As Boolean = False) As Boolean Implements IBattlelist.PrevEntity
            Try
                Dim IsOnScreen As Integer = 0
                If OnScreen Then
                    For IndexPosition = IndexPosition - 1 To 0 Step -1
                        Kernel.Client.ReadMemory(Consts.ptrBattleListBegin + (IndexPosition * Consts.BLDist) + Consts.BLOnScreenOffset, IsOnScreen, 1)
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

        Public Function CreaturesOnScreen() As Boolean Implements IBattlelist.CreaturesOnScreen
            Try
                Dim BL As New BattleList
                BL.Reset()
                Do
                    If Not BL.IsPlayer Then
                        If BL.IsOnScreen Then
                            If BL.GetLocation.Z = Kernel.CharacterLoc.Z Then
                                Return True
                            End If
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
