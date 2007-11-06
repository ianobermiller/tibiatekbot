Imports System, System.Windows, System.Windows.Forms, System.IO, TibiaTek_Multi_Client_Patcher.My

Public Class MainForm

    Dim Filename As String = ""
    Dim OutputFilename As String = ""
    Dim Directory As String = ""

    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        Try
            PatchButton.Enabled = False
            If Not OpenDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then Exit Sub
            Dim FVI As FileVersionInfo = FileVersionInfo.GetVersionInfo(OpenDialog.FileName)
            If Not FVI.ProductName.Equals(MySettings.Default.ProductName) Then
                MessageBox.Show(MySettings.Default.ErrorMsg1, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
            If Not FVI.ProductVersion.Equals(MySettings.Default.ProductVersion) Then
                MessageBox.Show(MySettings.Default.ErrorMsg2 & MySettings.Default.ProductVersion, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
            Filename = OpenDialog.FileName
            For I As Integer = Filename.Length - 1 To 0 Step -1
                If Filename.Chars(I) = "\" Then
                    Directory = Strings.Left(Filename, I)
                    Exit For
                End If
            Next

            Dim FSR As New FileStream(Filename, FileMode.Open, FileAccess.Read)
            FSR.Seek(MySettings.Default.PatchOffset, SeekOrigin.Begin)
            If FSR.ReadByte = MySettings.Default.PatchReplacement Then
                MessageBox.Show(MySettings.Default.ErrorMsg3, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
                FSR.Close()
                Exit Sub
            End If
            FSR.Close()
            TibiaExecutableTextBox.Text = Filename
            PatchButton.Enabled = True
        Catch Ex As Exception
            MessageBox.Show("Error: " & Ex.Message & vbCrLf & "Stack Trace: " & Ex.StackTrace, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
            End
        End Try
    End Sub

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        End
    End Sub

    Private Sub PatchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatchButton.Click
        
        Try
            If MessageBox.Show("Would you like to make a backup of your Tibia Executable?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Forms.DialogResult.Yes Then
                File.Copy(Filename, Directory & "\" & MySettings.Default.BackupExecutable, True)
                MessageBox.Show("Backup of the Tibia Executable saved to """ & Directory & "\" & MySettings.Default.BackupExecutable & """.", "Backup Complete", MessageBoxButtons.OK)
            End If

            If SaveDialog.ShowDialog() <> Forms.DialogResult.OK Then
                Exit Sub
            End If
            OutputFilename = SaveDialog.FileName
            PatchButton.Enabled = False
            Dim FI As New FileInfo(Filename)
            ProgressBar1.Value = 0
            ProgressBar1.Visible = True
            BGW.RunWorkerAsync()
        Catch Ex As Exception
            MessageBox.Show("Error: " & Ex.Message & vbCrLf & "Stack Trace: " & Ex.StackTrace, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
            End
        End Try
    End Sub


    Private Sub BGW_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGW.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BGW_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW.RunWorkerCompleted
        ProgressBar1.Visible = False
    End Sub

    Private Sub BGW_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork
        Try
            Dim FSR As New FileStream(Filename, FileMode.Open, FileAccess.Read)
            Dim FI As New FileInfo(Filename)
            Dim FSW As New FileStream(Directory & "\Tibia.exe.tmp", FileMode.OpenOrCreate, FileAccess.Write)
            Dim Reader As New BinaryReader(FSR)
            Dim Writer As New BinaryWriter(FSW)

            ' Write executable
            Dim CurrentByte As Byte = 0
            Try
                Do
                    CurrentByte = Reader.ReadByte()
                    If FSW.Position = MySettings.Default.PatchOffset Then
                        CurrentByte = MySettings.Default.PatchReplacement
                    End If
                    Writer.Write(CurrentByte)
                    If (FSW.Position / FI.Length * 100) Mod 10 = 0 Then
                        BGW.ReportProgress(FSW.Position / FI.Length * 100)
                    End If
                Loop While True
            Catch
            End Try
            Writer.Close()
            Reader.Close()
            FSR.Close()
            FSW.Close()
            If File.Exists(SaveDialog.FileName) Then
                File.Delete(SaveDialog.FileName)
            End If
            File.Move(Directory & "\Tibia.exe.tmp", SaveDialog.FileName)
            MessageBox.Show("Successfully patched.", "Complete", MessageBoxButtons.OK)
            PatchButton.Enabled = False
        Catch Ex As Exception
            MessageBox.Show("Error: " & Ex.Message & vbCrLf & "Stack Trace: " & Ex.StackTrace, MySettings.Default.ErrorCaption, MessageBoxButtons.OK)
            End
        End Try
    End Sub
End Class
