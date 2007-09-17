Imports System.IO, System.Math

Public Module MiscUtils

    Public Function GetConfigurationDirectory() As String
        Try
            Return Application.StartupPath & "\Config"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function FPSXToB(ByVal X As Double) As Double
        Return Round(1100 / (X + 5), 1)
    End Function

    Public Function FPSBToX(ByVal B As Double) As Double
        Return Round((1110 / B) - 5, 1)
    End Function

    Public Function GetWaypointsDirectory() As String
        Try
            If Not IO.Directory.Exists(Application.StartupPath & "\Waypoints") Then
                IO.Directory.CreateDirectory(Application.StartupPath & "\Waypoints")
            End If
            Return Application.StartupPath & "\Waypoints"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return "\Data"
    End Function

    Public Function StrToShort(ByVal S As String) As Int16
        Dim Result As Int16 = 2
        If Not S.Length Then
            Select Case S.ToLower()
                Case "1", "on", "true", "yes", "sim", "enable", "activate", "si", "encender", "start"
                    Result = 1
                Case "0", "off", "false", "no", "nao", "disable", "deactivate", "apagar", "stop", "halt"
                    Result = 0
                Case Else
                    Result = -1
            End Select
        End If
        Return Result
    End Function

    Public Function BytesToStr(ByVal bytBuffer As Byte()) As String
        Dim I As Integer
        Dim Output As String = ""
        For I = 0 To bytBuffer.Length - 1
            Output &= Hex(bytBuffer(I)) & " "
        Next
        Return Output
    End Function

    Public Function GetInventorySlotAsLocation(ByVal Slot As InventorySlots) As LocationDefinition
        Dim Result As New LocationDefinition
        Result.X = &HFFFF
        Result.Y = CShort(Slot)
        Result.Z = 0
        Return Result
    End Function

End Module
