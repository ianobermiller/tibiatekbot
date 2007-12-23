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

Imports Scripting, System.Windows.Forms


Public Interface ITibia

#Region " Structures "

	Structure LocationDefinition
		Dim X As Integer
		Dim Y As Integer
		Dim Z As Integer

		Public Sub New(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer)
			Me.X = X
			Me.Y = Y
			Me.Z = Z
		End Sub
	End Structure

#End Region

#Region " Enumerations "

	Enum WindowStates As Integer
		Active
		Minimized
		Inactive
		Hidden
	End Enum

	Enum ConnectionStates As Integer
		Disconnected = 0
		InitLogging = 2
		Logging = 3
		LoggedOn = 4
		InitConnecting = 5
		Connecting = 6
		Connected = 8
	End Enum

#End Region

#Region " Events "

	Event Starting()
	Event Started()
	Event Exited()
	Event Connected()
	Event Disconnected()

#End Region

#Region " Properties "

	ReadOnly Property Directory() As String
	ReadOnly Property Filename() As String
	ReadOnly Property GetProcessID() As Integer
	ReadOnly Property GetProcessHandle() As Integer
	ReadOnly Property GetWindowHandle() As Integer
	ReadOnly Property GetWindowState() As WindowStates
	Property Title() As String
	ReadOnly Property GetCurrentDialog() As String
	ReadOnly Property ConnectionState() As ConnectionStates
	ReadOnly Property IsConnected() As Boolean

#End Region

#Region " Methods "

	Sub ConsoleWrite(ByVal Message As String)
	Sub ConsoleError(ByVal Message As String)
	Sub ConsoleRead(ByVal Message As String)
	Sub Restore()
	Sub Minimize()
	Sub Hide()
	Sub Show()
	Sub Activate()
	Sub FlashWindow(Optional ByVal [Stop] As Boolean = False)
	Sub Close()
	Function SendMessage(ByVal MessageID As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	Function MessageBox(ByVal Message As String, Optional ByVal Caption As String = "", Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.None, Optional ByVal DefaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.DefaultDesktopOnly) As DialogResult
	Sub SetFramesPerSecond(ByVal FPS As Double)


#End Region


End Interface