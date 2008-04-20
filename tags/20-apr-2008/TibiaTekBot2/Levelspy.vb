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

Module LevelspyModule

    Public LS As Levelspy

    Public Class Levelspy
        Private CurrentLevel As Integer = 0
        Private LevelSpyPointer As Integer = 0
        Private Enabled As Boolean = False
        'You MIGHT need to change these on update
        Private Nops() As Byte = {&H90, &H90, &H90, &H90, &H90, &H90}
        Private Defaults() As Byte = {&H89, &H86, &HD8, &H25, &H0, &H0}


        Public ReadOnly Property GetLevel() As Integer
            Get
                Try
                    If Kernel.CharacterLoc.Z > 7 Then
                        Return CurrentLevel - 2
                    Else
                        Return Kernel.CharacterLoc.Z - 7 + CurrentLevel
                    End If

                Catch ex As Exception
                    MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Get
        End Property

        Public Sub Reset()
            Try
                If LevelSpyPointer = 0 Then
                    Dim TempPointer As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrLevelSpy, TempPointer, 4)
                    TempPointer += 28
                    Kernel.Client.ReadMemory(TempPointer, LevelSpyPointer, 4)
                    LevelSpyPointer += &H25D8
                End If
                Kernel.Client.ReadMemory(LevelSpyPointer, CurrentLevel, 4)
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function Enable() As Boolean
            Try
                Reset()
                If InjectCode(Consts.LevelSpy1, Nops) _
                 AndAlso InjectCode(Consts.LevelSpy2, Nops) _
                 AndAlso InjectCode(Consts.LevelSpy3, Nops) Then
                    Enabled = True
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Function Disable() As Boolean
            Try
                CurrentLevel = Kernel.CharacterLoc.Z
                If InjectCode(Consts.LevelSpy1, Defaults) _
                 AndAlso InjectCode(Consts.LevelSpy2, Defaults) _
                 AndAlso InjectCode(Consts.LevelSpy3, Defaults) Then
                    Return True
                    Enabled = False
                Else
                    Return False
                End If
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Function GoUp() As Boolean
            Try
                If Not Enabled Then Return False
                If CurrentLevel + 1 > 7 Then Return False

                Kernel.Client.WriteMemory(LevelSpyPointer, CurrentLevel + 1, 4)
                CurrentLevel += 1
                Return True
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Function GoDown() As Boolean
            Try
                If Not Enabled Then Return False
                If CurrentLevel - 1 < 0 Then Return False

                Kernel.Client.WriteMemory(LevelSpyPointer, CurrentLevel - 1, 4)
                CurrentLevel -= 1
                Return True
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

    End Class

End Module
