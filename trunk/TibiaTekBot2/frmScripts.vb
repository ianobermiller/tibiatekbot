Imports System.CodeDom, System.CodeDom.Compiler, System.IO, Scripting

Public Class frmScripts
    'Configure parameters
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    ' Private Sub AddToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToolStripMenuItem.Click

    'Dim a As CodeDomProvider = New Microsoft.VisualBasic.VBCodeProvider()
    'Dim Compiler As ICodeCompiler = a.CreateCompiler()
    'Dim Params As New CompilerParameters()
    'Dim Results As CompilerResults
    'With Params
    '    .GenerateExecutable = False
    '    .GenerateInMemory = True
    '    .IncludeDebugInformation = False
    '    .ReferencedAssemblies.Add("TibiaTekBot Scripting Assembly.dll")
    '    .ReferencedAssemblies.Add("System.Windows.Forms.dll")
    '    .ReferencedAssemblies.Add("System.dll")
    'End With
    'Dim TextFile As System.IO.FileStream = System.IO.File.Open("c:\defaultscript.txt", FileMode.Open)
    'Dim Reader As New System.IO.StreamReader(TextFile)
    'Dim Source As String = Reader.ReadToEnd()
    ''MsgBox(Source)
    'Results = Compiler.CompileAssemblyFromSource(Params, Source)

    'If Results.Errors.Count > 0 Then
    '    For Each err As CompilerError In Results.Errors
    '        MessageBox.Show("Line: " & err.Line & ". Message: " & err.ErrorText)
    '    Next
    '    MessageBox.Show("Compile failed with " & Results.Errors.Count.ToString() & " errors.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
    'Else
    '    Dim Script As IScript = DirectCast(FindInterface(Results.CompiledAssembly, "IScript"), Scripting.IScript)
    '    'Script.Initialize(Kernel)
    '    'Script.Method1()
    '    Threading.Thread.Sleep(5000)
    '    Script.Dispose()
    '    Script = Nothing
    'End If
    'Reader.Close()
    'TextFile.Close()

    ' End Sub

    Public Shared Function FindInterface(ByVal DLL As Reflection.Assembly, ByVal InterfaceName As String) As Object
        Dim t As Type

        'Loop through types looking for one that implements the given interface
        For Each t In DLL.GetTypes()
            If Not (t.GetInterface(InterfaceName, True) Is Nothing) Then
                Return DLL.CreateInstance(t.FullName)
            End If
        Next

        Return Nothing
    End Function

    Private Sub AddScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddScriptToolStripMenuItem.Click
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
                    Dim a As CodeDomProvider = New Microsoft.VisualBasic.VBCodeProvider()
                    Dim Compiler As ICodeCompiler = a.CreateCompiler()
                    Dim Params As New CompilerParameters()
                    Dim Results As CompilerResults
                    With Params
                        .GenerateExecutable = False
                        .GenerateInMemory = True
                        .IncludeDebugInformation = False
                        .ReferencedAssemblies.Add(Application.StartupPath & "\\TibiaTekBot Scripting Assembly.dll")
                        .ReferencedAssemblies.Add("System.Windows.Forms.dll")
                        .ReferencedAssemblies.Add("System.dll")
                    End With
                    Dim TextFile As System.IO.FileStream = System.IO.File.Open(OpenScriptDialog.FileName, FileMode.Open)
                    Dim Reader As New System.IO.StreamReader(TextFile)
                    Dim Source As String = Reader.ReadToEnd()
                    Results = Compiler.CompileAssemblyFromSource(Params, Source)

                    If Results.Errors.Count > 0 Then
                        Dim Errors As String = ""
                        For Each err As CompilerError In Results.Errors
                            Errors &= OpenScriptDialog.FileName & ":" & err.Line & ". Message: " & err.ErrorText & vbCrLf
                        Next
                        MessageBox.Show(Errors & vbCrLf & "There were " & Results.Errors.Count.ToString() & " errors.", "Compiler Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Else
                        Dim SD As New ScriptDefinition
                        SD.Filename = OpenScriptDialog.FileName
                        SD.SafeFileName = OpenScriptDialog.SafeFileName
                        SD.Script = DirectCast(FindInterface(Results.CompiledAssembly, "IScript"), Scripting.IScript)
                        SD.Script.Initialize(Kernel)
                        Kernel.Scripts.Add(SD)
                        ScriptsView.Rows.Clear()
                        For Each SDD As ScriptDefinition In Kernel.Scripts
                            ScriptsView.Rows.Add(SD.Filename)
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
            If Kernel.Scripts(I).Filename.Equals(e.Row.Cells(0).Value.ToString, StringComparison.CurrentCultureIgnoreCase) Then
                Kernel.Scripts(I).Script.Dispose()
                Kernel.Scripts.RemoveAt(I)
            End If
        Next
    End Sub
End Class