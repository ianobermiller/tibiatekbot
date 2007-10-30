Public Module VipListModule

    Public Class VipList
        Private Position As Integer

        Public Sub New()
            Position = 0
        End Sub

        Public ReadOnly Property GetID() As Integer
            Get
                Dim ID As Integer = 0
                Core.ReadMemory(Consts.ptrVipListBegin + (Position * Consts.VipDist), ID, 4)
                Return CUInt(ID)
            End Get
        End Property

        Public ReadOnly Property GetName() As String
            Get
                Dim Name As String = ""
                Dim Address As Integer = Consts.ptrVipListBegin + (Position * Consts.VipDist) + Consts.VipNameOffset
                Core.ReadMemory(Address, Name)
                Return Name
            End Get
        End Property

        Public ReadOnly Property IsOnline() As Boolean
            Get
                Dim Online As Integer = 0
                Core.ReadMemory(Consts.ptrVipListBegin + (Position * Consts.VipDist) + Consts.VipStatusOffset, Online, 1)
                Return (Online > 0)
            End Get
        End Property

        Public Function Find(ByVal Name As String, Optional ByVal MustBeOnline As Boolean = False, Optional ByVal Offset As Integer = 0) As Boolean
            For I As Integer = Offset To Consts.VipMax - 1
                If GetID = 0 Then Continue For
                If String.Compare(Name, GetName, True) = 0 Then
                    If MustBeOnline Then
                        Return IsOnline
                    Else
                        Return True
                    End If
                End If
            Next
            Return False
        End Function

        Public Function Reset(Optional ByVal MustBeOnline As Boolean = False) As Boolean
            If Not MustBeOnline Then
                Position = 0
                Return True
            Else
                For Position = 0 To Consts.VipMax - 1
                    If IsOnline Then Return True
                Next
            End If
            Return False
        End Function

        Public Function NextPlayer(Optional ByVal MustBeOnline As Boolean = False) As Boolean
            If Not MustBeOnline Then
                Position += 1
                If Position = Consts.VipMax Then
                    Position -= 1
                    Return False
                Else
                    Return True
                End If

            Else
                For Position = Position + 1 To Consts.VipMax - 1
                    If IsOnline Then Return True
                Next
            End If
            Return False
        End Function

    End Class

End Module
