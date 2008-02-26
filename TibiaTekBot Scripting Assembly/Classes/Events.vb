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
#Region "MessageReceived"
        Public Class MessageReceivedEventArgs
            Inherits EventArgs
            Private _CharacterName As String
            Private _MessageType As ITibia.MessageType
            Private _CharacterLevel As Integer
            Private _CharacterLocation As ITibia.LocationDefinition
            Private _Message As String
            Private _DefaultMessageType As ITibia.DefaultMessageType
            Private _PrivateMessageType As ITibia.PrivateMessageType
            Private _ChannelMessageType As ITibia.ChannelMessageType
            Private _ChannelType As ITibia.Channel

            Public Sub New(ByVal MessageType As ITibia.MessageType, ByVal Name As String, ByVal Level As Integer, ByVal Location As ITibia.LocationDefinition, ByVal Message As String, _
                           Optional ByVal DefaultMessageType As ITibia.DefaultMessageType = ITibia.DefaultMessageType.Normal, _
                           Optional ByVal ChannelMessageType As ITibia.ChannelMessageType = ITibia.ChannelMessageType.Normal, _
                           Optional ByVal PrivateMessageType As ITibia.PrivateMessageType = ITibia.PrivateMessageType.Normal, _
                           Optional ByVal ChannelType As ITibia.Channel = ITibia.Channel.GameChat)

                _MessageType = MessageType
                _CharacterLevel = Level
                _CharacterName = Name
                _CharacterLocation = Location
                _Message = Message
                _PrivateMessageType = DefaultMessageType
                _ChannelMessageType = ChannelMessageType
                _PrivateMessageType = PrivateMessageType
                _ChannelType = ChannelType
            End Sub

            Public ReadOnly Property CharacterName() As String
                Get
                    Return _CharacterName
                End Get
            End Property
            Public ReadOnly Property MessageType() As ITibia.MessageType
                Get
                    Return _MessageType
                End Get
            End Property
            Public ReadOnly Property CharacterLevel() As Integer
                Get
                    Return _CharacterLevel
                End Get
            End Property
            Public ReadOnly Property CharacterLocation() As ITibia.LocationDefinition
                Get
                    Return _CharacterLocation
                End Get
            End Property
            Public ReadOnly Property Message() As String
                Get
                    Return _Message
                End Get
            End Property
            Public ReadOnly Property DefaultMessageType() As ITibia.DefaultMessageType
                Get
                    Return _DefaultMessageType
                End Get
            End Property
            Public ReadOnly Property PrivateMessageType() As ITibia.DefaultMessageType
                Get
                    Return _PrivateMessageType
                End Get
            End Property
            Public ReadOnly Property ChannelMessageType() As ITibia.DefaultMessageType
                Get
                    Return _ChannelMessageType
                End Get
            End Property
            Public ReadOnly Property ChannelType() As ITibia.DefaultMessageType
                Get
                    Return _ChannelType
                End Get
            End Property
        End Class
#End Region
    End Module
End Namespace
