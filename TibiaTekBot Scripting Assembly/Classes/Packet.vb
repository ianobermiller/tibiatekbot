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

Imports System.Windows.Forms, Scripting

Public Class Packet
    Implements IPacket

#Region " Variables "

    Private Buffer() As Byte = New Byte() {0, 0}

#End Region

#Region " Properties "
    Public ReadOnly Property GetBytes() As Byte() Implements Scripting.IPacket.GetBytes
        Get
            Return Buffer
        End Get
    End Property

    Public Shadows Function ToString() As String Implements IPacket.ToString
        Try
            Dim Result As String = String.Empty
            Dim Bs() As Byte = GetBytes()
            For Each B As Byte In Bs
                Result &= Hex(B).PadLeft(2, "0"c)
            Next
            Return Result
            'Return System.Convert.ToBase64String(GetBytes)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function

    Public Shadows Function ToBase64String() As String Implements IPacket.ToBase64String
        Try
            Return System.Convert.ToBase64String(GetBytes)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function

#End Region

#Region " Methods "

    Private Sub AddDouble(ByRef bytBuffer() As Byte, ByVal bytDouble As Double)
        Dim DoubleBuffer(0 To 7) As Byte
        DoubleBuffer = BitConverter.GetBytes(bytDouble)
        For B As Integer = 7 To 0 Step -1
            AddByte(bytBuffer, DoubleBuffer(B))
        Next
    End Sub

    Private Sub AddByte(ByRef bytBuffer() As Byte, ByVal bytByte As Byte)
        Try
            Dim intTemp As Integer
            Dim bytTemp() As Byte
            intTemp = UBound(bytBuffer)
            ReDim Preserve bytBuffer(intTemp + 1)
            bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + 1)
            bytBuffer(0) = bytTemp(0)
            bytBuffer(1) = bytTemp(1)
            bytBuffer(intTemp + 1) = bytByte
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt16)
        Try
            Dim intTemp As Integer
            Dim bytTemp() As Byte
            intTemp = UBound(bytBuffer)
            ReDim Preserve bytBuffer(intTemp + 2)
            bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + 2)
            bytBuffer(0) = bytTemp(0)
            bytBuffer(1) = bytTemp(1)
            bytTemp = BitConverter.GetBytes(intInteger)
            bytBuffer(intTemp + 1) = bytTemp(0)
            bytBuffer(intTemp + 2) = bytTemp(1)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddDWord(ByRef bytBuffer() As Byte, ByVal intInteger As UInt32)
        Try
            Dim intTemp As Integer
            Dim bytTemp() As Byte
            Dim I As Byte
            intTemp = UBound(bytBuffer)
            ReDim Preserve bytBuffer(intTemp + 4)
            bytTemp = BitConverter.GetBytes(BitConverter.ToInt32(bytBuffer, 0) + 4)
            bytBuffer(0) = bytTemp(0)
            bytBuffer(1) = bytTemp(1)
            bytTemp = BitConverter.GetBytes(intInteger)
            For I = 0 To 3
                bytBuffer(intTemp + I + 1) = bytTemp(I)
            Next
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddString(ByRef bytBuffer() As Byte, ByVal strString As String)
        Try
            If strString Is Nothing Then Exit Sub
            Dim intTemp As Integer
            Dim bytTemp() As Byte
            Dim chrTemp() As Char
            Dim intCounter As Integer
            AddWord(bytBuffer, strString.Length)
            intTemp = UBound(bytBuffer)
            ReDim Preserve bytBuffer(intTemp + strString.Length)
            bytTemp = BitConverter.GetBytes(BitConverter.ToInt16(bytBuffer, 0) + strString.Length)
            bytBuffer(0) = bytTemp(0)
            bytBuffer(1) = bytTemp(1)
            chrTemp = strString.ToCharArray
            For intCounter = 1 To strString.Length
                bytBuffer(intTemp + intCounter) = Asc(chrTemp(intCounter - 1))
            Next
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddString(ByVal str As String) Implements IPacket.AddString
        AddString(Buffer, str)
    End Sub

    Public Sub AddByte(ByVal Value As Byte) Implements IPacket.AddByte
        AddByte(Buffer, Value)
    End Sub

    Public Sub AddWord(ByVal Value As UInt16) Implements IPacket.AddWord
        AddWord(Buffer, Value)
    End Sub

    Public Sub AddDWord(ByVal Value As UInt32) Implements IPacket.AddDWord
        AddDWord(Buffer, Value)
    End Sub

    Public Sub AddLocation(ByVal Location As ITibia.LocationDefinition) Implements IPacket.AddLocation
        AddWord(Location.X)
        AddWord(Location.Y)
        AddByte(Location.Z)
    End Sub

    Public Sub AddDouble(ByVal Value As Double) Implements IPacket.AddDouble
        AddDouble(Buffer, Value)
    End Sub

    Public Property GetByte(ByVal Offset As UInteger) As Byte Implements IPacket.GetByte
        Get
            Try
                Return Buffer(Offset)
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return 0
        End Get
        Set(ByVal value As Byte)
            Try
                Buffer(Offset) = value
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Set
    End Property
#End Region


End Class
