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
