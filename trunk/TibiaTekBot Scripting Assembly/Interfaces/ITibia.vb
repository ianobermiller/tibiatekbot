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

Imports Scripting, System.Net, System.Windows.Forms

Public Interface ITibia

#Region " Structures "

    Structure CharacterListEntry
        Dim Index As Integer
        Dim CharacterName As String
        Dim WorldName As String
        Dim WorldIP As IPAddress
        Dim WorldPort As UInteger
    End Structure

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

    Enum GraphicsEngines As Integer
        DirectX5 = 0
        OpenGL
        DirectX9
    End Enum

    Enum EventKind As Integer
        None = 0
        CharacterAttacked
        CharacterConditionsChanged
        MessageReceived
    End Enum

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

    Enum TextColors
        Blue = 5
        Green = &H1E
        LightBlue = 35
        LightGreen = 30
        Crystal = 65
        Platinum = 89
        LightGrey = 129
        Red = 180
        Orange = 198
        Gold = 210
        White = 215
        None = 255
    End Enum

    Enum InventorySlots
        Head = 1
        Neck
        Backpack
        Armor
        LeftHand
        RightHand
        Legs
        Feet
        Finger
        Belt
    End Enum

    Enum LightColor
        Darkness = 0
        BrightSword = &H8F
        UtevoLux = &HD7
        Torch = &HCE
        LightWand = &HD1
    End Enum

    Enum LightIntensity
        None = 0
        VerySmall = 2
        Small = 4
        Medium = 6
        Large = 8
        VeryLarge = 10
        Huge = 12
    End Enum

    Enum SysMessageType As Byte
        StatusWarning = &H12
        EventAdvance 'white msg on status and console
        EventDefault 'white msg on status and console
        StatusDefault 'whte msg on status and console
        Information 'green msg
        StatusSmall 'white msg on the status
        StatusConsoleBlue 'Blue msg in the console
        StatusConsoleRed 'Red msg in the console
    End Enum

    Enum MessageType
        [Default]
        PrivateMessage
        Channel
    End Enum

    Enum ChannelMessageType As Byte
        Normal = 5
        GameMaster = &HA
        Tutor = &HC
        Anonymous = &HE
    End Enum

    Enum Channel As Integer
        GuildChat = 0
        GameChat = 4
        Trade = 5
        RLChat = 6
        Help = 7
        Console = 100
        IRCChannelBegin
        IRCChannelEnd = IRCChannelBegin + 40
        Personal = &HFFFF
    End Enum

    Enum DefaultMessageType As Byte
        Normal = 1
        Whisper = 2
        Yell = 3
        MonsterSay = &H10
        MonsterYell = &H11
    End Enum

    Enum PrivateMessageType As Byte
        Normal = 4
        GameMaster = &HB
    End Enum

    Enum FightingMode As Byte
        Offensive = 1
        Balanced = 2
        Defensive = 3
    End Enum

    Enum ChasingMode As Byte
        Standing
        Chasing
    End Enum

    Enum SecureMode As Byte
        Normal
        Secure
    End Enum

    Enum AnimationEffects As Byte
        BloodHit = 1
        WaterWaves = 2
        Smoke = 3
        Spark = 4
        Explosion1 = 5
        Explosion2 = 6
        Explosion3 = 7
        YellowCircle = 8 '<-- what's this?
        Poisoned = 9
        BerserkAttack = 10
        EnergyAttack = 11
        ElectricAttack = 12
        BlueAura = 13
        Haste = 14
        GreenAura = 15
        FireAttack = 16
        PoisonSpark = 17
        SuddenDeath = 18
        PhysicalAttack = 18
        MusicalNotesGreen = 19
        MusicalNotesRed = 20
        PoisonWave = 21
        MusicalNotesYellow = 22
        MusicalNotesPurple = 23
        MusicalNotesBlue = 24
        MusicalNotesWhite = 25
        Bubbles = 26
        RollingDice = 27
        PresentOpening = 28
        Fireworks = 29
        FireworksFail = 30
        FireworksBlue = 31
    End Enum

    <Flags()> Enum Conditions
        None
        Poison = 1
        Burnt = 2
        Electrified = 4
        Beer = 8
        MagicShield = 16
        Paralized = 32
        Haste = 64
        CombatSign = 128
        Drowning = 256
        Freezing = 512
        Dazzled = 1024
        Cursed = 2048
        All = Poison Or Burnt Or Electrified _
            Or Beer Or MagicShield Or Paralized _
            Or Haste Or CombatSign Or Drowning _
            Or Freezing Or Dazzled Or Cursed
    End Enum

    Enum Skills
        FistFighting = 0
        ClubFighting
        SwordFighting
        AxeFighting
        DistanceFighting
        Shielding
        Fishing
    End Enum
#End Region

#Region " Events "

    Event Starting()
    Event Started()
    Event Closed()
    Event Connected()
    Event Disconnected()
    Event CharacterConditionsChanged As CharacterConditionsChangedEventHandler
    Event CharacterAttacked As CharacterAttackedEventHandler
    Event MessageReceived As MessageReceivedEventHandler
#End Region

#Region " Delegates "
    Delegate Sub CharacterAttackedEventHandler(ByVal e As Events.CharacterAttackedEventArgs)
    Delegate Sub CharacterConditionsChangedEventHandler(ByVal e As Events.CharacterConditionsChangedEventArgs)
    Delegate Sub MessageReceivedEventHandler(ByVal e As Events.MessageReceivedEventArgs)
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
    ReadOnly Property CharacterLocation() As LocationDefinition
    ReadOnly Property MapTiles() As IMapTiles
    ReadOnly Property CharacterName() As String
    ReadOnly Property CharacterWorld() As String
    ReadOnly Property CharacterChasingMode() As ChasingMode
    ReadOnly Property CharacterSecureMode() As SecureMode
    ReadOnly Property CharacterFightingMode() As FightingMode
    ReadOnly Property Objects() As IObjects
    WriteOnly Property TopMost() As Boolean
    ReadOnly Property CharacterHasCondition(ByVal Condition As Scripting.ITibia.Conditions) As Boolean
    ReadOnly Property CharacterHitPoints() As Integer
    ReadOnly Property CharacterManaPoints() As Integer
    ReadOnly Property CharacterExperience() As Integer
    ReadOnly Property CharacterCapacity() As Integer
    ReadOnly Property CharacterSoulPoints() As Integer
    ReadOnly Property CharacterStamina() As Integer
    ReadOnly Property CharacterSkill(ByVal Skill As ITibia.Skills) As Integer
    ReadOnly Property CharacterSkillPercent(ByVal Skill As ITibia.Skills) As Integer
    ReadOnly Property CharacterMagicLevel() As Integer
    ReadOnly Property CharacterLevel() As Integer
    ReadOnly Property CharacterListCurrentEntry() As ITibia.CharacterListEntry
    ReadOnly Property CharacterList() As ITibia.CharacterListEntry()
    ReadOnly Property CharacterListSelectedIndex() As Integer
    ReadOnly Property ScreenWidth() As Integer
    ReadOnly Property ScreenHeight() As Integer
    ReadOnly Property ScreenTop() As Integer
    ReadOnly Property ScreenLeft() As Integer
    ReadOnly Property ScreenRight() As Integer
    ReadOnly Property ScreenBottom() As Integer
    ReadOnly Property GraphicsEngine() As GraphicsEngines

#End Region

#Region " Methods "

    Sub ConsoleWrite(ByVal Message As String)
    Sub ConsoleError(ByVal Message As String)
    Sub ConsoleRead(ByVal Message As String)
    Sub Restore()
    Sub Minimize()
    Sub Maximize()
    Sub Hide()
    Sub Show()
    Sub Activate()
    Sub FlashWindow(Optional ByVal [Stop] As Boolean = False)
    Sub Close()
    Function SendMessage(ByVal MessageID As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Function MessageBox(ByVal Message As String, Optional ByVal Caption As String = "", Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.None, Optional ByVal DefaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.DefaultDesktopOnly) As DialogResult
    Sub SetFramesPerSecond(ByVal FPS As Double)
    Sub CharacterMove(ByVal Location As ITibia.LocationDefinition)
    Function BringToFront() As Boolean

    'Sub WriteMemory(ByVal Address As Integer, ByVal Value As Integer, ByVal Size As Integer)
    'Sub WriteMemory(ByVal Address As Integer, ByVal Value() As Byte)
    'Sub WriteMemory(ByVal Address As Integer, ByVal Value() As Byte, ByVal Offset As Integer, ByVal Length As Integer)
    'Sub WriteMemory(ByVal Address As Integer, ByVal Value As String)
    'Sub WriteMemory(ByVal Address As Integer, ByVal Value As Double)

    'Sub ReadMemory(ByVal Address As Integer, ByRef Value As Double)
    'Sub ReadMemory(ByVal Address As Integer, ByRef Value As Integer, ByVal Size As Integer)
    'Sub ReadMemory(ByVal Address As Integer, ByRef Value As UInteger, ByVal Size As Integer)
    'Sub ReadMemory(ByVal Address As Integer, ByRef Value() As Byte, ByVal Length As Integer)
    'Sub ReadMemory(ByVal Address As Integer, ByRef Value As String)
    'Sub ReadMemory(ByVal Address As Integer, ByRef Value As String, ByVal Length As Integer)


#End Region


End Interface