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
Imports Microsoft.VisualBasic, Microsoft.VisualBasic.Devices, Microsoft.VisualBasic.ApplicationServices
Imports System.Windows.Forms

Public Interface IKernel

#Region " Enumerations "
    <Flags()> Enum KeyboardState
        None = 0
        Alt = 1
        Shift = 2
        Ctrl = 4
    End Enum

    Enum VirtualKey As Integer
        None
        Enter = &HD
        Escape = &H1B
        Space = &H20
        PageUp
        PageDown
        [End]
        Home
        Left
        Up
        Right
        Down
        Insert = &H2D
        Delete
        Number0 = &H30
        Number1
        Number2
        Number3
        Number4
        Number5
        Number6
        Number7
        Number8
        Number9
        A = &H41
        B
        C
        D
        E
        F
        G
        H
        I
        J
        K
        L
        M
        N
        O
        P
        Q
        R
        S
        T
        U
        V
        W
        X
        Y
        Z
        NumPad0 = &H60
        NumPad1
        NumPad2
        NumPad3
        NumPad4
        NumPad5
        NumPad6
        NumPad7
        NumPad8
        NumPad9
        NumPadMultiply
        NumPadAdd
        NumPadSubtract = &H6D
        NumPadDecimal
        NumPadDivide
        F1
        F2
        F3
        F4
        F5
        F6
        F7
        F8
        F9
        F10
        F11
        F12
        F13
        F14
        F15
        F16
        F17
        F18
        F19
        F20
        F21
        F22
        F23
        F24
        Semicolon = &HBA
        Plus
        Comma
        Minus
        Dot
        ForwardSlash
        Tick
        Tilde = Tick
        LeftSquareBracket = &HDB
        BackwardSlash
        RightSquareBracket
        SingleQuote
        Reserved = &HFF
    End Enum
#End Region

#Region " Structures "
    Structure KeyboardVKEntry
        Dim Name As String
        Dim VirtualKeyOriginalCode As VirtualKey
        Dim VirtualKeyNewCode As VirtualKey
        Dim State As KeyboardState
    End Structure

#End Region

#Region " Properties "
    ReadOnly Property Client() As ITibia
    ReadOnly Property Proxy() As IProxy
    ReadOnly Property Spells() As ISpells
    ReadOnly Property CommandParser() As ICommandParser
    ReadOnly Property IrcClient() As IIrcClient
    ReadOnly Property Computer() As Computer
    ReadOnly Property [NotifyIcon]() As NotifyIcon
#End Region

#Region " Methods "
    Function NewBattlelist() As IBattlelist
    Function NewBattlelist(ByVal SE As IBattlelist.SpecialEntity) As IBattlelist
    Function NewBattlelist(ByVal Position As Integer) As IBattlelist
    Function NewContainer() As IContainer

    Sub ConsoleRead(ByVal strString As String)
    Sub ConsoleWrite(ByVal strString As String)
    Sub ConsoleError(ByVal strString As String)
#End Region

End Interface
