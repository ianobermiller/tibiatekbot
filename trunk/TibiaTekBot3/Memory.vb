Imports TibiaTekBot3.Win32API

Public Module Memory

    Public Class MemoryClass

        Public Shared Sub Write(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByVal Value As Integer, ByVal Size As Integer)
            Try
                Dim bytArray() As Byte
                bytArray = BitConverter.GetBytes(Value)
                WriteProcessMemory(ProcessHandle, Address, bytArray, Size, 0)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Shared Sub Write(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByVal Value() As Byte)
            Try
                WriteProcessMemory(ProcessHandle, Address, Value, Value.Length, 0)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub



        Public Shared Sub Write(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByVal Value As String)
            Try
                Dim Length As Integer = Value.Length
                For I As Integer = 0 To Length - 1
                    Write(ProcessHandle, Address + I, Asc(Value.Chars(I)), 1)
                Next
                Write(ProcessHandle, Address + Length, 0, 1)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Shared Sub Write(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByVal Value As Double)
            Try
                Dim Buffer(0 To 7) As Byte
                Buffer = BitConverter.GetBytes(Value)
                For I As Integer = 0 To 7
                    Write(ProcessHandle, Address + I, CInt(Buffer(I)), 1)
                Next
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Shared Sub Read(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByRef Value As Integer, ByVal Size As Integer)
            Try
                Win32API.ReadProcessMemory(ProcessHandle, Address, Value, Size, 0)
            Catch
                MessageBox.Show("Unable to read from memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub



        Public Shared Sub Read(ByVal ProcessHandle As Integer, ByVal Address As Integer, ByRef Value As String)
            Dim intChar As Integer
            Dim strTemp As String = ""
            Dim I As Integer = 0
            Do
                Read(ProcessHandle, Address + I, intChar, 1)
                If intChar <> 0 Then strTemp = strTemp & Chr(intChar)
                I += 1
            Loop Until intChar = 0
            Value = strTemp
        End Sub

        Public Sub Write(ByVal Address As Integer, ByVal Value As Integer, ByVal Size As Integer)
            Try
                Dim bytArray() As Byte
                bytArray = BitConverter.GetBytes(Value)
                WriteProcessMemory(Core.Tibia.GetProcessHandle, Address, bytArray, Size, 0)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Write(ByVal Address As Integer, ByVal Value() As Byte)
            Try
                WriteProcessMemory(Core.Tibia.GetProcessHandle, Address, Value, Value.Length, 0)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Write(ByVal Address As Integer, ByVal Value As String)
            Try
                Dim Length As Integer = Value.Length
                For I As Integer = 0 To Length - 1
                    Write(Address + I, Asc(Value.Chars(I)), 1)
                Next
                Write(Address + Length, 0, 1)
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Write(ByVal Address As Integer, ByVal Value As Double)
            Try
                Dim Buffer(0 To 7) As Byte
                Buffer = BitConverter.GetBytes(Value)
                For I As Integer = 0 To 7
                    Write(Address + I, CInt(Buffer(I)), 1)
                Next
            Catch
                MessageBox.Show("Unable to write to memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Read(ByVal Address As Integer, ByRef Value As Byte)
            Try
                Dim IntValue As Integer = 0
                Win32API.ReadProcessMemory(Core.Tibia.GetProcessHandle, Address, IntValue, 1, 0)
                Value = CByte(IntValue)
            Catch
                MessageBox.Show("Unable to read from memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Read(ByVal Address As Integer, ByRef Value As Integer, ByVal Size As Integer)
            Try
                Win32API.ReadProcessMemory(Core.Tibia.GetProcessHandle, Address, Value, Size, 0)
            Catch
                MessageBox.Show("Unable to read from memory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub Read(ByVal Address As Integer, ByRef Value() As Byte, ByVal Length As Integer)
            Dim bytArray(Length - 1) As Byte
            Dim tempInteger As Integer
            For I As Integer = 0 To Length - 1
                Read(Address + I, tempInteger, 1)
                bytArray(I) = CInt(tempInteger)
            Next
            Value = bytArray
        End Sub

        Public Sub Read(ByVal Address As Integer, ByRef Value As String)
            Dim intChar As Integer
            Dim strTemp As String = ""
            Dim I As Integer = 0
            Do
                Read(Address + I, intChar, 1)
                If intChar <> 0 Then strTemp = strTemp & Chr(intChar)
                I += 1
            Loop Until intChar = 0
            Value = strTemp
        End Sub

    End Class

End Module
