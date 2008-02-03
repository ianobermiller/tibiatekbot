Imports System.CodeDom, System.CodeDom.Compiler, System.IO, Scripting, System.Reflection

Public Class frmScripts
    'Configure parameters
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Public Shared Function FindInterface(ByVal DLL As Reflection.Assembly, ByVal InterfaceName As String) As Object
        'Loop through types looking for one that implements the given interface
        For Each t As Type In DLL.GetTypes()
            If Not (t.GetInterface(InterfaceName, True) Is Nothing) Then
                Return DLL.CreateInstance(t.FullName)
            End If
        Next
        Return Nothing
    End Function

    Private Sub AddScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddScriptToolStripMenuItem.Click, AddToolStripMenuItem1.Click
        Try
            Dim CanClose As Boolean = False
            Do
                If OpenScriptDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    For Each SDD As ScriptDefinition In Kernel.Scripts
                        If SDD.Filename.Equals(OpenScriptDialog.FileName, StringComparison.CurrentCultureIgnoreCase) Then
                            If MessageBox.Show("That script is already running.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) <> Windows.Forms.DialogResult.Retry Then Exit Sub
                            CanClose = False
                            Continue Do
                        End If
                    Next

                    Dim FI As System.IO.FileInfo = New FileInfo(OpenScriptDialog.FileName)
                    Dim P As CodeDomProvider
                    Select Case FI.Extension.ToLower
                        Case ".cs"
                            P = New Microsoft.CSharp.CSharpCodeProvider
                        Case ".vb"
                            P = New Microsoft.VisualBasic.VBCodeProvider
                        Case Else
                            If MessageBox.Show("Invalid extension.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) <> Windows.Forms.DialogResult.Retry Then Exit Sub
                            Continue Do
                    End Select
                    '                    Dim C As ICodeCompiler = P.CreateCompiler()
                    Dim CP As New CompilerParameters()

                    CP.GenerateExecutable = False
                    CP.GenerateInMemory = True
                    CP.IncludeDebugInformation = False
                    'CP.CompilerOptions = "/optimize"
                    'CP.TreatWarningsAsErrors = False

                    CP.ReferencedAssemblies.Add(Application.StartupPath & "\\TibiaTekBot Scripting Assembly.dll")
                    CP.ReferencedAssemblies.Add("System.Windows.Forms.dll")
                    CP.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
                    CP.ReferencedAssemblies.Add("System.Xml.dll")
                    CP.ReferencedAssemblies.Add("System.Drawing.dll")
                    CP.ReferencedAssemblies.Add("System.dll")


                    Dim TextFile As System.IO.FileStream = System.IO.File.Open(OpenScriptDialog.FileName, FileMode.Open)
                    Dim Reader As New System.IO.StreamReader(TextFile)
                    Dim Source As String = Reader.ReadToEnd()

                    Dim Results As CompilerResults = P.CompileAssemblyFromSource(CP, Source)

                    If Results.Errors.Count > 0 Then
                        Dim Errors As String = ""
                        For Each err As CompilerError In Results.Errors
                            Errors &= OpenScriptDialog.FileName & ":" & err.Line & ". Message: " & err.ErrorText & vbCrLf
                        Next
                        MessageBox.Show(Errors & vbCrLf & "There were " & Results.Errors.Count.ToString() & " errors.", "Compiler Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Else
                        Dim SD As New ScriptDefinition
                        SD.Filename = OpenScriptDialog.FileName
                        SD.State = IScript.ScriptState.Running
                        SD.SafeFileName = OpenScriptDialog.SafeFileName
                        SD.CompilerResults = Results
                        SD.Script = DirectCast(FindInterface(Results.CompiledAssembly, "IScript"), Scripting.IScript)

                        If SD.Script Is Nothing Then
                            MessageBox.Show("Invalid script.", "Compiler Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        SD.Script.Initialize(Kernel)

                        Kernel.Scripts.Add(SD)
                        ScriptsView.Rows.Clear()
                        For Each SDD As ScriptDefinition In Kernel.Scripts
                            ScriptsView.Rows.Add(My.Resources.script_play, SDD.Filename)
                        Next
                    End If
                    Reader.Close()
                    TextFile.Close()
                End If
                CanClose = True
            Loop While Not CanClose
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ScriptsView_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles ScriptsView.UserDeletedRow
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            If Kernel.Scripts(I).Filename = e.Row.Cells(1).Value.ToString Then
                Kernel.Scripts(I).Script.Dispose()
                Kernel.Scripts.RemoveAt(I)
                Exit For
            End If
        Next
    End Sub

    'Private Sub EditToolStripMenuItem_DropDownOpened(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.DropDownOpened
    '    DeleteToolStripMenuItem.Enabled = ScriptsView.SelectedRows.Count > 0
    'End Sub

    'Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
    '    For I As Integer = 0 To Kernel.Scripts.Count - 1
    '        If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then
    '            Kernel.Scripts(I).Script.Dispose()
    '            Kernel.Scripts.RemoveAt(I)
    '            ScriptsView.Rows.Remove(ScriptsView.SelectedRows(0))
    '            Exit For
    '        End If
    '    Next
    'End Sub

    Private Sub frmScripts_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub AddToolStripMenuItem_DropDownOpened(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.DropDownOpened
        RemoveToolStripMenuItem1.Enabled = ScriptsView.SelectedRows.Count > 0
        EditToolStripMenuItem2.Enabled = ScriptsView.SelectedRows.Count > 0
    End Sub

    Private Sub StartToolStripMenuItem_DropDownOpened(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.DropDownOpened
        SelectedToolStripMenuItem.Enabled = ScriptsView.SelectedRows.Count > 0
        AllToolStripMenuItem.Enabled = ScriptsView.RowCount > 0
    End Sub

    Private Sub ResumeToolStripMenuItem1_DropDownOpened(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResumeToolStripMenuItem1.DropDownOpened
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then

                Exit For
            End If
        Next
        SelectedToolStripMenuItem1.Enabled = ScriptsView.SelectedRows.Count > 0
        AllToolStripMenuItem1.Enabled = ScriptsView.RowCount > 0
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub RemoveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem1.Click, RemoveToolStripMenuItem.Click
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then
                Kernel.Scripts(I).Script.Dispose()
                Kernel.Scripts.RemoveAt(I)
                ScriptsView.Rows.Remove(ScriptsView.SelectedRows(0))
                Exit For
            End If
        Next
    End Sub

    Private Sub SelectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedToolStripMenuItem.Click
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then
                Dim SD As ScriptDefinition
                SD = Kernel.Scripts(I)
                If SD.State = IScript.ScriptState.Paused Then Exit Sub
                SD.Script.Pause()
                SD.State = IScript.ScriptState.Paused
                Kernel.Scripts(I) = SD
                ScriptsView.SelectedRows(0).Cells(0).Value = My.Resources.script_pause
            End If
        Next
    End Sub

    Private Sub SelectedToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedToolStripMenuItem1.Click
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then
                Dim SD As ScriptDefinition
                SD = Kernel.Scripts(I)
                If SD.State = IScript.ScriptState.Running Then Exit For
                SD.Script.Resume()
                SD.State = IScript.ScriptState.Running
                Kernel.Scripts(I) = SD
                ScriptsView.SelectedRows(0).Cells(0).Value = My.Resources.script_play
            End If
        Next
    End Sub

    Private Sub EditToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem2.Click, EditToolStripMenuItem.Click
        Process.Start("notepad", ScriptsView.SelectedRows(0).Cells(1).Value.ToString)
    End Sub

    Private Sub AllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem.Click
        For I As Integer = 0 To Kernel.Scripts.Count - 1
            Dim SD As ScriptDefinition
            SD = Kernel.Scripts(I)
            If SD.State = IScript.ScriptState.Paused Then Continue For
            SD.Script.Pause()
            SD.State = IScript.ScriptState.Paused
            Kernel.Scripts(I) = SD
            ScriptsView.Rows(I).Cells(0).Value = My.Resources.script_pause
        Next
    End Sub

    Private Sub AllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem1.Click
        For I As Integer = 0 To Kernel.Scripts.Count - 1

            Dim SD As ScriptDefinition
            SD = Kernel.Scripts(I)
            If SD.State = IScript.ScriptState.Running Then Continue For
            SD.Script.Pause()
            SD.State = IScript.ScriptState.Running
            Kernel.Scripts(I) = SD
            ScriptsView.Rows(I).Cells(0).Value = My.Resources.script_play
        Next
    End Sub

    Private Sub ReloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadToolStripMenuItem.Click
        'Structure: First Compile, then remove selected from scripts (not from window), then add compiled back to scripts
        Try
            Dim CanClose As Boolean = False
            Do
                If String.IsNullOrEmpty(ScriptsView.SelectedRows(0).Cells(1).Value.ToString) Then
                    Beep()
                    Exit Sub
                End If
                'Compiling
                Dim FI As System.IO.FileInfo = New FileInfo(ScriptsView.SelectedRows(0).Cells(1).Value.ToString)
                Dim P As CodeDomProvider
                Select Case FI.Extension.ToLower
                    Case ".cs"
                        P = New Microsoft.CSharp.CSharpCodeProvider
                    Case ".vb"
                        P = New Microsoft.VisualBasic.VBCodeProvider
                    Case Else
                        If MessageBox.Show("Invalid extension.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) <> Windows.Forms.DialogResult.Retry Then Exit Sub
                        Continue Do
                End Select
                '                    Dim C As ICodeCompiler = P.CreateCompiler()
                Dim CP As New CompilerParameters()

                CP.GenerateExecutable = False
                CP.GenerateInMemory = True
                CP.IncludeDebugInformation = False
                'CP.CompilerOptions = "/optimize"
                'CP.TreatWarningsAsErrors = False

                CP.ReferencedAssemblies.Add(Application.StartupPath & "\\TibiaTekBot Scripting Assembly.dll")
                CP.ReferencedAssemblies.Add("System.Windows.Forms.dll")
                CP.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")
                CP.ReferencedAssemblies.Add("System.dll")


                Dim TextFile As System.IO.FileStream = System.IO.File.Open(OpenScriptDialog.FileName, FileMode.Open)
                Dim Reader As New System.IO.StreamReader(TextFile)
                Dim Source As String = Reader.ReadToEnd()

                Dim Results As CompilerResults = P.CompileAssemblyFromSource(CP, Source)

                If Results.Errors.Count > 0 Then
                    Dim Errors As String = ""
                    For Each err As CompilerError In Results.Errors
                        Errors &= OpenScriptDialog.FileName & ":" & err.Line & ". Message: " & err.ErrorText & vbCrLf
                    Next
                    MessageBox.Show(Errors & vbCrLf & "There were " & Results.Errors.Count.ToString() & " errors." & vbCrLf & "Using the old script file.", "Compiler Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Else
                    'First remove...
                    Dim TempSD As New ScriptDefinition
                    For I As Integer = 0 To Kernel.Scripts.Count - 1
                        If Kernel.Scripts(I).Filename = ScriptsView.SelectedRows(0).Cells(1).Value.ToString Then
                            TempSD = Kernel.Scripts(I)
                            Kernel.Scripts(I).Script.Dispose()
                            Kernel.Scripts.RemoveAt(I)
                            Exit For
                        End If
                    Next
                    '... then add again
                    Dim SD As New ScriptDefinition
                    SD.Filename = TempSD.Filename
                    SD.State = IScript.ScriptState.Running
                    SD.SafeFileName = TempSD.SafeFileName
                    SD.CompilerResults = Results
                    SD.Script = DirectCast(FindInterface(Results.CompiledAssembly, "IScript"), Scripting.IScript)
                    SD.Script.Initialize(Kernel)
                    Kernel.Scripts.Add(SD)
                    MessageBox.Show("Script Loading Completed.", "Ready!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Reader.Close()
                TextFile.Close()
                CanClose = True
            Loop While Not CanClose
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem.Click
        For Each SD As ScriptDefinition In Kernel.Scripts
            If SD.Filename.Equals(ScriptsView.SelectedRows(0).Cells(1).Value.ToString) Then
                Dim ass As System.Reflection.Assembly = SD.CompilerResults.CompiledAssembly
                Dim t() As Type = ass.GetExportedTypes
                Dim mi() As MethodInfo = t(0).GetMethods()
                For Each m As MethodInfo In mi
                    MsgBox(m.Name)
                Next
                Exit For
            End If
        Next
    End Sub


End Class