Namespace Events
    Public Module Events
#Region " CharacterAttackedEventArgs "

        Public Class CharacterAttackedEventArgs
            Inherits EventArgs
            Private _EntityID As Integer = 0

            Public Sub New(ByVal EntityID As Integer)
                _EntityID = EntityID
            End Sub

            Public ReadOnly Property EntityID() As Integer
                Get
                    Return _EntityID
                End Get
            End Property
        End Class

        Public Class CharacterConditionsChangedEventArgs
            Inherits EventArgs
            Private _Conditions As ITibia.Conditions = ITibia.Conditions.None

            Public Sub New(ByVal Conditions As ITibia.Conditions)
                _Conditions = Conditions
            End Sub

            Public ReadOnly Property Conditions() As ITibia.Conditions
                Get
                    Return _Conditions
                End Get
            End Property
        End Class
#End Region
    End Module
End Namespace
