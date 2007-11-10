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

Imports System.Threading

Public Module ThreadTimerModule

    Public Class ThreadTimer

        Private TimerDelegate As TimerCallback
        Private TimerObj As Timer
        Private TimerInterval As Long
        Private CurrentState As ThreadTimerState
        Public Event OnExecute()

        Public ReadOnly Property State() As ThreadTimerState
            Get
                Return CurrentState
            End Get
        End Property

        Public Property Interval() As Long
            Get
                Return TimerInterval
            End Get
            Set(ByVal Value As Long)
                TimerInterval = Value
                If CurrentState = ThreadTimerState.Running Then
                    TimerObj.Change(Value, Value)
                Else
                    TimerObj.Change(Timeout.Infinite, Value)
                End If
            End Set
        End Property

        Public Sub New(Optional ByVal Interval As Long = 0, Optional ByVal DelayBeforeStart As Integer = Timeout.Infinite)
            If DelayBeforeStart > Timeout.Infinite Then
                CurrentState = ThreadTimerState.Running
            Else
                CurrentState = ThreadTimerState.Stopped
            End If
            TimerInterval = Interval
            TimerDelegate = AddressOf TimerTick
            TimerObj = New Timer(TimerDelegate, Nothing, DelayBeforeStart, TimerInterval)
        End Sub

        Private Sub TimerTick(ByVal state As Object)
            SyncLock Me
                If CurrentState = ThreadTimerState.Stopped Then Exit Sub
                RaiseEvent OnExecute()
            End SyncLock
        End Sub

        Public Sub StartTimer(Optional ByVal DelayBeforeStart As Long = 0)
            TimerObj.Change(DelayBeforeStart, TimerInterval)
            CurrentState = ThreadTimerState.Running
        End Sub

        Public Sub StopTimer()
            TimerObj.Change(Timeout.Infinite, 0)
            CurrentState = ThreadTimerState.Stopped
        End Sub

        Protected Overrides Sub Finalize()
            TimerObj.Dispose()
        End Sub

    End Class

End Module
