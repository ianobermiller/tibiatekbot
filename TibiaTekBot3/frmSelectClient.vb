Imports System.Diagnostics, System.xml
Public Class frmSelectClient

    Public Declare Ansi Function InjectLibrary Lib "TibiaTekBot3Loader.dll" (ByVal ProcessId As Integer, ByRef arr As Byte) As Boolean

    Public ClientProcesses As New List(Of Integer)
    Public ClientProcessesVersions As New List(Of String)

    Private Sub RadioButtons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExistingTibiaRadioButton.CheckedChanged, NewTibiaRadioButton.CheckedChanged
        ExistingTibiaClientGroupBox.Enabled = ExistingTibiaRadioButton.Checked
        If NewTibiaRadioButton.Checked Then
            OKButton.Enabled = True
        Else
            OKButton.Enabled = ExistingClients.SelectedIndex >= 0
        End If
    End Sub

    Private Sub ExistingClients_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExistingClients.DropDown
        Try
            Dim Processes() As Process = System.Diagnostics.Process.GetProcesses()
            ExistingClients.Items.Clear()
            ClientProcesses.Clear()
            Dim CurrentV As New Versioninfo
            Dim InGame As Integer
            Dim CharacterSelectionIndex As Integer = 0
            Dim CharacterListBegin As Integer = 0
            'Dim Address As Integer = 0
            Dim Skip As Boolean
            For Each P As Process In Processes
                If Not P.ProcessName.ToLower.Contains("tibia") Then Continue For
                If P.ProcessName.ToLower.Contains("bot") Then Continue For
                Dim FVI As FileVersionInfo = P.MainModule.FileVersionInfo 'FileVersionInfo.GetVersionInfo(P.MainModule.FileName)
                Dim ptrInGame As Integer = 0
                Dim Found As Boolean = False
                If Core.Versions.Contains(FVI.FileVersion) Then
                    CurrentV = Core.Versions.Items(FVI.FileVersion)
                Else
                    ClientProcesses.Add(P.Id)
                    ClientProcessesVersions.Add(CurrentV.Name)
                    ExistingClients.Items.Add("Tibia " & FVI.FileVersion & " (Not Supported)")
                    Continue For
                End If
                Skip = False
                For Each PModule As ProcessModule In P.Modules
                    If PModule.ModuleName.ToLower.Equals("tibiatekbot3dll.dll") Then
                        Skip = True
                        Exit For
                    End If
                Next
                If Skip Then Continue For
                MemoryClass.Read(P.Handle.ToInt32, CurrentV.InGame, InGame, 1)
                If InGame = 8 Then
                    MemoryClass.Read(P.Handle.ToInt32, CurrentV.CharacterSelectionIndex, CharacterSelectionIndex, 1)
                    MemoryClass.Read(P.Handle.ToInt32, CurrentV.CharacterListBegin, CharacterListBegin, 4)
                    Dim CharacterName As String = ""
                    MemoryClass.Read(P.Handle.ToInt32, CharacterListBegin + (CharacterSelectionIndex * CurrentV.CharacterListDist), CharacterName)
                    ExistingClients.Items.Add("Tibia " & FVI.FileVersion & " (Logged In As " & CharacterName & ")")
                Else
                    ExistingClients.Items.Add("Tibia " & FVI.FileVersion & " (Not Logged In)")
                End If
                P.Refresh()
                ClientProcesses.Add(P.Id)
                ClientProcessesVersions.Add(CurrentV.Name)
            Next
        Catch Ex As Exception

        End Try
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click

        If ExistingTibiaRadioButton.Checked Then
            If ExistingClients.SelectedIndex = -1 Then
                MessageBox.Show("You must select a running Tibia client to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim Pid As Integer = ClientProcesses(ExistingClients.SelectedIndex)
            Dim TibiaV As String = ClientProcessesVersions(ExistingClients.SelectedIndex)
            Dim DllPath As String = Application.StartupPath & "\" & Core.Versions.Items(TibiaV).DllFile
            Dim bytDllPath() As Byte = System.Text.Encoding.ASCII.GetBytes(DllPath)

            Core.Tibia.SetClientProcessByID(Pid)

            ' Populate CodeCave in the Tibia Client
            Core.Tibia.Memory.Write(Core.Versions.Items(TibiaV).CodeCave + CodeCave.TibiaHandleOffset, Core.Tibia.GetWindowHandle, 4)
            Core.Tibia.Memory.Write(Core.Versions.Items(TibiaV).CodeCave + CodeCave.TTBHandleOffset, Core.TTBHandle, 4)

            If InjectLibrary(Pid, bytDllPath(0)) Then
                Core.Tibia.Version = TibiaV
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Unable to inject """ & DllPath & """ into the Tibia client.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End
            End If
        End If
    End Sub

    Private Sub ExistingClients_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExistingClients.SelectedIndexChanged
        If ExistingTibiaRadioButton.Checked Then
            OKButton.Enabled = (ExistingClients.SelectedIndex >= 0) _
                                AndAlso (ExistingClients.Items.Count > 0) _
                                AndAlso (ClientProcessesVersions(ExistingClients.SelectedIndex) = "8.00")
        Else
            OKButton.Enabled = True
        End If
    End Sub

End Class