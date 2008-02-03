Imports System.Collections, Scripting, System.Xml

Public Class Objects
    Implements IObjects

    Private _Client As Tibia
    Private _MinimumItemID As Integer = 0
    Private _MaximumItemID As Integer = 0
    Private ptrObjectsStruct As Integer = 0
    Private ptrObjectsBegin As Integer = 0
    Private _Objects As New SortedList(Of Integer, IObjects.ObjectDefinition)

    Public Sub New(ByRef Client As Tibia)
        Me._Client = Client
        LoadObjects()
    End Sub

    Public Sub Initialize()
        GetPtrObjectsStruct()
        GetPtrObjectsBegin()
    End Sub

    Public ReadOnly Property Objects() As IObjects.ObjectDefinition()
        Get
            Try
                Dim L(0 To _Objects.Count - 1) As IObjects.ObjectDefinition
                Dim I As Integer = 0
                For Each OD As IObjects.ObjectDefinition In _Objects.Values
                    L(I) = OD
                    I += 1
                Next
                Return L
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return New IObjects.ObjectDefinition() {}
            End Try
        End Get
    End Property

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
                Dim Temp As UInteger = 0
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

    Public Property Name(ByVal ItemID As Integer) As String Implements Scripting.IObjects.Name
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) _
                    OrElse Not _Objects.ContainsKey(ItemID) Then Return String.Empty
                Return _Objects(ItemID).Name
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return String.Empty
            End Try
        End Get
        Set(ByVal value As String)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) Then Exit Property
                If _Objects.ContainsKey(ItemID) Then
                    Dim OD As IObjects.ObjectDefinition = _Objects(ItemID)
                    OD.Name = value
                    _Objects(ItemID) = OD
                Else
                    Dim OD As New IObjects.ObjectDefinition
                    OD.ItemID = ItemID
                    OD.Name = value
                    OD.Kind = IObjects.ObjectKind.None
                    _Objects.Add(ItemID, OD)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public Property Kind(ByVal ItemID As Integer) As IObjects.ObjectKind Implements IObjects.Kind
        Get
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) _
                    OrElse Not _Objects.ContainsKey(ItemID) Then Return IObjects.ObjectKind.None
                Return _Objects(ItemID).Kind
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return IObjects.ObjectKind.None
            End Try
        End Get
        Set(ByVal value As IObjects.ObjectKind)
            Try
                If Not (ItemID >= MinimumItemID AndAlso ItemID <= MaximumItemID) _
                    OrElse Not _Objects.ContainsKey(ItemID) Then Exit Property
                Dim OD As IObjects.ObjectDefinition = _Objects(ItemID)
                OD.Kind = value
                _Objects(ItemID) = OD
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property

    Public ReadOnly Property ID(ByVal Name As String) As Integer Implements Scripting.IObjects.ID
        Get
            Try
                For Each OD As IObjects.ObjectDefinition In _Objects.Values
                    If String.Equals(OD.Name, Name, StringComparison.CurrentCultureIgnoreCase) Then
                        Return OD.ItemID
                    End If
                Next
                Return 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return 0
            End Try
        End Get
    End Property

    Public Sub LoadObjects()
        Dim Name As String = ""
        Dim Kind As IObjects.ObjectKind
        Dim TempStr As String = ""
        Dim ID As UInteger = 0
        Dim Document As New XmlDocument
        Try
            _Objects.Clear()
            Document.Load(GetConfigurationDirectory() & "\Objects.xml")
            For Each Node As XmlElement In Document.Item("Objects")
                Name = Node.GetAttribute("Name")
                TempStr = Node.GetAttribute("ID")
                If Not String.IsNullOrEmpty(TempStr) AndAlso TempStr.Chars(0) = "H" Then TempStr = "&" & TempStr
                ID = CUInt(TempStr)
                Kind = CType(System.Enum.Parse(Kind.GetType, Node.GetAttribute("Kind"), True), IObjects.ObjectKind)
                If Not _Objects.ContainsKey(ID) Then
                    _Objects.Add(ID, New IObjects.ObjectDefinition(ID, Name, Kind))
                Else
                    MessageBox.Show("Object " & Name & " with ID " & ID & " (H" & Hex(ID) & ") already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Next
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Refresh()
        LoadObjects()
    End Sub

    Public Sub SaveObjects()
        Dim Document As New XmlDocument
        Try
            Dim Objects As XmlElement = Document.CreateElement("Objects")
            For Each OD As IObjects.ObjectDefinition In _Objects.Values
                Dim [Object] As XmlElement = Document.CreateElement("Object")
                Dim ID As XmlAttribute = Document.CreateAttribute("ID")
                Dim Name As XmlAttribute = Document.CreateAttribute("Name")
                Dim Kind As XmlAttribute = Document.CreateAttribute("Kind")
                ID.InnerText = "H" & Hex(OD.ItemID)
                Name.InnerText = OD.Name
                Kind.InnerText = OD.Kind.ToString
                [Object].Attributes.Append(ID)
                [Object].Attributes.Append(Name)
                [Object].Attributes.Append(Kind)
                Objects.AppendChild([Object])
            Next
            Dim Declaration As XmlDeclaration = Document.CreateXmlDeclaration("1.0", "", "")
            Document.AppendChild(Declaration)
            Dim xmlComment As XmlComment = Document.CreateComment(GNUGPLStatement)
            Document.AppendChild(xmlComment)
            Document.AppendChild(Objects)
            Document.Save(GetConfigurationDirectory() & "\Objects.xml")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function IsKind(ByVal ItemID As Integer, ByVal Kind As Scripting.IObjects.ObjectKind) As Boolean Implements Scripting.IObjects.IsKind
        Try
            Return ((Me.Kind(ItemID) And Kind) = Kind)
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
End Class
