Imports System.Collections, Scripting

Public Class Objects
    Implements IObjects

    Private _Client As Tibia
    Private _MinimumItemID As Integer = 0
    Private _MaximumItemID As Integer = 0
    Private ptrObjectsStruct As Integer = 0
    Private ptrObjectsBegin As Integer = 0

    Public Sub New(ByRef Client As Tibia)
        Me._Client = Client
    End Sub

    Public Sub Initialize()
        GetPtrObjectsStruct()
        GetPtrObjectsBegin()
    End Sub

    Public Property AutoMapColor(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.AutoMapColor
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectAutoMapColorOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectAutoMapColorOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property Heighted(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.Heighted
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectHeightedOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectHeightedOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property LensHelp(ByVal ItemID As Integer) As IObjects.ObjectLensHelp Implements Scripting.IObjects.LensHelp
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLensHelpOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As IObjects.ObjectLensHelp)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLensHelpOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property LightColor(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.LightColor
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLightColorOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLightColorOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property LightRadius(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.LightRadius
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLightRadiusOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectLightRadiusOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property TextLimit(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.TextLimit
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectTextLimitOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectTextLimitOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property WalkSpeed(ByVal ItemID As Integer) As Integer Implements Scripting.IObjects.WalkSpeed
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectWalkSpeedOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectWalkSpeedOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property Flags(ByVal ItemID As Integer) As Scripting.IObjects.ObjectFlags Implements Scripting.IObjects.Flags
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return 0
                Dim Temp As Integer = 0
                _Client.ReadMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectFlagsOffset, Temp, 4)
                Return Temp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
        Set(ByVal value As Scripting.IObjects.ObjectFlags)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                _Client.WriteMemory(GetPtrObjectsBegin() + (ItemID - MinimumItemID) * Consts.ObjectDist + Consts.ObjectFlagsOffset, value, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Private Function GetPtrObjectsStruct() As Integer
        Try
            If ptrObjectsStruct = 0 Then
                _Client.ReadMemory(Consts.ptrObjects, ptrObjectsStruct, 4)
            End If
            Return ptrObjectsStruct
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    Private Function GetPtrObjectsBegin() As Integer
        Try
            If ptrObjectsBegin = 0 Then
                _Client.ReadMemory(GetPtrObjectsStruct() + 8, ptrObjectsBegin, 4)
            End If
            Return ptrObjectsBegin
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    Public ReadOnly Property MaximumItemID() As Integer Implements Scripting.IObjects.MaximumItemID
        Get
            Try
                If _MaximumItemID = 0 Then
                    _Client.ReadMemory(GetPtrObjectsStruct() + 4, _MaximumItemID, 4)
                    _MaximumItemID -= 1 'to make it inclusive
                End If
                Return _MaximumItemID
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property MinimumItemID() As Integer Implements Scripting.IObjects.MinimumItemID
        Get
            Try
                If _MinimumItemID = 0 Then _Client.ReadMemory(GetPtrObjectsStruct, _MinimumItemID, 4)
                Return _MinimumItemID
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
    End Property

    Public Function HasFlags(ByVal ItemID As Integer, ByVal Flags As IObjects.ObjectFlags) As Boolean Implements Scripting.IObjects.HasFlags
        Try
            If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return False
            Dim _Flags As IObjects.ObjectFlags = Me.Flags(ItemID)
            Return CBool((_Flags And Flags) = Flags)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function HasExtraByte(ByVal ItemID As Integer) As Boolean Implements Scripting.IObjects.HasExtraByte
        Try
            If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Return False
            Return HasFlags(ItemID, IObjects.ObjectFlags.IsStackable) _
                OrElse HasFlags(ItemID, IObjects.ObjectFlags.IsFluidContainer) _
                OrElse HasFlags(ItemID, IObjects.ObjectFlags.IsRune) _
                OrElse HasFlags(ItemID, IObjects.ObjectFlags.IsSplash)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Class
