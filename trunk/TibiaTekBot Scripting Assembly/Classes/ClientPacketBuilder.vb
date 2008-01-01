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

Imports System.Windows.Forms

Public Class ClientPacketBuilder
    Implements IPacketBuilder

    Private Packets As New Queue(Of Packet)
    Private _Proxy As IProxy
    Private _AutoSend As Boolean = True

    Public Sub New(ByVal Proxy As IProxy, Optional ByVal AutoSend As Boolean = True)
        _Proxy = Proxy
        _AutoSend = AutoSend
    End Sub

    Public Sub Speak(ByVal Name As String, ByVal DefaultMessageType As ITibia.DefaultMessageType, ByVal Level As Integer, ByVal Message As String, ByVal X As Integer, ByVal Y As Integer, ByVal Z As Byte)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAA)
            _Packet.AddDWord(0)
            _Packet.AddString(Name)
            _Packet.AddWord(Level)
            _Packet.AddByte(DefaultMessageType)
            _Packet.AddWord(X)
            _Packet.AddWord(Y)
            _Packet.AddByte(Z)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SpeakWithBroadcast(ByVal Nick As String, ByVal Message As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAA)
            _Packet.AddDWord(0)
            _Packet.AddString("Broadcast from " & Nick)
            _Packet.AddWord(0)
            _Packet.AddByte(9) ' Broacast message type
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Destinatary As String, ByVal Level As Integer, ByVal Message As String, Optional ByVal PrivateMessageType As ITibia.PrivateMessageType = ITibia.PrivateMessageType.Normal)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAA)
            _Packet.AddDWord(0)
            _Packet.AddString(Destinatary)
            _Packet.AddWord(Level)
            _Packet.AddByte(PrivateMessageType)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Speak(ByVal Name As String, ByVal Level As Integer, ByVal ChannelType As ITibia.ChannelMessageType, ByVal Message As String, Optional ByVal Channel As ITibia.Channel = ITibia.Channel.Console)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAA)
            _Packet.AddDWord(0)
            _Packet.AddString(Name)
            _Packet.AddWord(Level)
            _Packet.AddByte(ChannelType)
            _Packet.AddWord(Channel)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SystemMessage(ByVal Type As ITibia.SysMessageType, ByVal Message As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HB4)
            _Packet.AddByte(Type) '&h12
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AnimatedText(ByRef Color As ITibia.TextColors, ByRef Loc As ITibia.LocationDefinition, ByRef ShortText As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H84)
            _Packet.AddWord(Loc.X)
            _Packet.AddWord(Loc.Y)
            _Packet.AddByte(Loc.Z)
            _Packet.AddByte(Color)
            _Packet.AddString(ShortText)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FYIBox(ByVal Message As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H15)
            _Packet.AddString(Message)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CreateContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, ByVal Name As String, ByVal Size As Byte, ByVal Items() As IContainer.ContainerItemDefinition, Optional ByVal HasParent As Boolean = False)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H6E)
            _Packet.AddByte(ContainerIndex)
            _Packet.AddWord(ItemID)
            _Packet.AddString(Name)
            _Packet.AddByte(Size)
            _Packet.AddByte(HasParent)
            _Packet.AddByte(Items.Length)
            For Each Item As IContainer.ContainerItemDefinition In Items
                _Packet.AddWord(Item.ID)
                If _Proxy.Client.Dat.GetInfo(Item.ID).HasExtraByte OrElse _Proxy.Client.Items.IsRune(Item.ID) Then
                    _Packet.AddByte(Item.Count)
                End If
            Next
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub OpenPrivate(ByVal PlayerName As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAD)
            _Packet.AddString(PlayerName)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AnimationEffect(ByVal Loc As ITibia.LocationDefinition, ByVal Animation As ITibia.AnimationEffects)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H83)
            _Packet.AddWord(Loc.X)
            _Packet.AddWord(Loc.Y)
            _Packet.AddByte(Loc.Z)
            _Packet.AddByte(Animation)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub HouseSpellEdit(ByVal SpellID As Byte, ByVal Time As UInteger, ByVal Content As String)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H97)
            _Packet.AddByte(SpellID)
            _Packet.AddDWord(Time)
            _Packet.AddString(Content)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub OpenChannel(ByVal ChannelName As String, ByVal ChannelID As Integer)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&HAC)
            _Packet.AddWord(ChannelID)
            _Packet.AddString(ChannelName)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddObjectToContainer(ByVal ItemID As UShort, ByVal ContainerIndex As Byte, Optional ByVal Count As Byte = &HFF)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H70)
            _Packet.AddByte(ContainerIndex)
            _Packet.AddWord(ItemID)
            If _Proxy.Client.Dat.GetInfo(ItemID).HasExtraByte OrElse _Proxy.Client.Items.IsRune(ItemID) Then
                _Packet.AddByte(Count)
            End If
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub RemoveObjectFromContainer(ByVal Slot As Byte, ByVal ContainerIndex As Byte)
        Try
            Dim _Packet As New Packet
            _Packet.AddByte(&H72)
            _Packet.AddByte(ContainerIndex)
            _Packet.AddByte(Slot)
            Packets.Enqueue(_Packet)
            If _AutoSend Then Send()
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Send() Implements IPacketBuilder.Send
        Try
            While Packets.Count > 0
                Dim P As Packet = Packets.Dequeue()
                _Proxy.SendPacketToClient(P)
            End While
        Catch Ex As System.InvalidOperationException
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
