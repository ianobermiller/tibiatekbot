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

Imports System.Threading, TibiaTekBot.frmMain, System.Text.RegularExpressions, System.Math, _
  System, System.Net, System.Net.Sockets, System.Text, System.Globalization, _
        System.IO, System.Xml, Microsoft.VisualBasic.Devices, TibiaTekBot.Constants, _
        System.Drawing.Imaging, TibiaTekBot.PProxy2, TibiaTekBot.ThreadTimer, _
        System.ComponentModel, System.Runtime.InteropServices, TibiaTekBot.IrcClient

#Region " To Do "

' B. Add Alarms
'   1. Player On Screen  (done)
'   2. Moved from spot
'   3. Player Killer On Screen (DONE)
'   4. Out of Food (done)
'   5. Out of Capacity (done)
'   6. Out of Soul Points (done)
'   7. Out of mana points (done)
'   8. Player On VIP
'   9. Private/Public (done)
' C. Add Map Reader 'working on it! xD (done)

' 1. Make the Command Parser (done)
' 2. Add Trade Channel Advertiser (done)
' 3. Add Aimbot (skip)
' 4. Add Ammo Maker (skip)
' 5. Add Ammo Restacker (done)
' 6. Add Auto Attacker (done)
' 7. Add Char Information (done)
' 8. Add Configuration Manager (done)
' 9. Add Auto Drinker (done)
' 10. Add Auto Eater (done)
' 11. Add Edit Command ???
' 12. Add Experience Checker (done)
' 13. Add Fake Title (done)
' 14. Add Auto Fisher (done)
' 15. Add Get Item ID (done)
' 16. Add Guild Members Online (done)
' 17. Add Auto Healer (done)
' 18. Add Auto Heal Friend (done)
' 19. Add Auto Heal Party (done)
' 20. Add Help (done)
' 21. Add Light Effects (done)
' 22. Add Auto Logout
' 23. Add Look (done)
' 24. Add Auto Looter (done)
' 25. Add Monsters/exp
' 26. Add Open (done)
' 27. Add Outfit Copier (done)
' 28. Add Spear Pickup (done)
' 29. Add Spell Caster (done)
' 30. Add Auto UHer (done)
' 31. Add Web Page Commands (done)
' 32. Add Shoveling?
' 33. Add Version (done)
' 34. Add Container Modifier (skip)
' 35. Add Trade Channel Watcher (done)
' 
' 36. Add IsFull check
' 37. Add sewer in cavebot type

#End Region

Public Module CoreModule

#Region " Structures "

    Public Structure ChatMessageDefinition
        Dim Prioritize As Boolean
        Dim MessageType As MessageType
        Dim Channel As ChannelType
        Dim Destinatary As String
        Dim Message As String
    End Structure

    Public Structure LocationDefinition
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
    Public Core As New CoreClass
    Public Outfits As Outfits
    Public Spells As Spells
    Public LootItems As LootItems
    Public Creatures As Creatures

    Public Class CoreClass
#Region " Objects "

        Public WithEvents Proxy As PProxy2
        'Dim MyLua As New LuaInterface.Lua
        Public WithEvents BGWOpenCommand As BackgroundWorker
        Public WithEvents BGWCharCommand As BackgroundWorker
        Public WithEvents BGWGuildMembersCommand As BackgroundWorker
        Public WithEvents BGWUpdateChecker As BackgroundWorker
        Public WithEvents LightTimerObj As ThreadTimer
        Public WithEvents GreetingTimerObj As ThreadTimer
        Public WithEvents ExpCheckerTimerObj As ThreadTimer
        Public WithEvents StatsTimerObj As ThreadTimer
        Public WithEvents SpellTimerObj As ThreadTimer
        Public WithEvents UHTimerObj As ThreadTimer
        Public WithEvents AdvertiseTimerObj As ThreadTimer
        Public WithEvents EaterTimerObj As ThreadTimer
        Public WithEvents HealFriendTimerObj As ThreadTimer
        Public WithEvents ChatMessageQueueTimerObj As ThreadTimer
        Public WithEvents MapReaderTimerObj As ThreadTimer
        Public WithEvents FisherTimerObj As ThreadTimer
        Public WithEvents RunemakerTimerObj As ThreadTimer
        Public WithEvents MagicShieldTimerObj As ThreadTimer
        Public WithEvents AutoTrainerTimerObj As ThreadTimer
        Public WithEvents AmmoRestackerTimerObj As ThreadTimer
        Public WithEvents PickUpTimerObj As ThreadTimer
        Public WithEvents HealTimerObj As ThreadTimer
        Public WithEvents HealPartyTimerObj As ThreadTimer
        Public WithEvents StatsUploaderTimerObj As ThreadTimer
        Public WithEvents LooterTimerObj As ThreadTimer
        Public WithEvents StackerTimerObj As ThreadTimer
        Public WithEvents BGWMapViewer As BackgroundWorker
        Public HotkeySettings As HotkeySettings
        Public WithEvents AlarmsForm As New frmAlarms
        Public WithEvents MapViewerForm As New frmMapViewer
        Public WithEvents CavebotForm As New frmCavebot
        Public Map As MapReader
        Public WithEvents ConstantsEditorForm As New frmConstantsEditor
        Public WithEvents BGWSendLocation As BackgroundWorker
        Public WithEvents ShowInvisibleCreaturesTimerObj As ThreadTimer
        Public WithEvents AutoPublishLocationTimerObj As ThreadTimer
        Public WithEvents FPSChangerTimerObj As ThreadTimer
        Public WithEvents RainbowOutfitTimerObj As ThreadTimer
        Public WithEvents TibiaClientStateTimerObj As ThreadTimer
        Public WithEvents AutoDrinkerTimerObj As ThreadTimer
        Public WithEvents CaveBotTimerObj As ThreadTimer
        Public WithEvents AutoAttackerTimerObj As ThreadTimer
        Public WithEvents BGWLooter As BackgroundWorker
        Public WithEvents WindowTimerObj As ThreadTimer
        Public WithEvents WalkerTimerObj As ThreadTimer
        Public WithEvents CharacterStatisticsForm As New frmCharacterStatistics
        Public WithEvents AutoAddTimerObj As ThreadTimer
        Public WithEvents AmuletChangerTimerObj As ThreadTimer
        Public WithEvents RingChangerTimerObj As ThreadTimer
        Public WithEvents IRCClient As IrcClient
        Public WithEvents AntiLogoutObj As ThreadTimer
        Public WithEvents TTMessagesTimerObj As ThreadTimer
#End Region

#Region " Variables "

        Public CharacterID As Integer = 0
        Public Experience As Integer = 0
        Public Level As Integer = 0
        Public NextLevelPercentage As Integer = 0
        Public NextLevelExp As Integer = 0
        Public CurrentLevelExp As Integer = 0
        Public LastExperience As Integer = 0
        Public HitPoints As Integer = 0
        Public ManaPoints As Integer = 0
        Public SoulPoints As Integer = 0
        Public Capacity As Integer = 0
        Public CharacterLoc As LocationDefinition
        Public CharacterLastLocation As LocationDefinition
        Public LightC As LightColor = LightColor.Darkness
        Public LightI As LightIntensity = LightIntensity.None
        Public ExpCheckerActivated As Boolean = False
        Public AutoAttackerActivated As Boolean = False
        Public AutoAttackerIgnoredID As Integer = 0
        Public SpellMsg As String = ""
        Public SpellManaRequired As Integer = 0
        Public UHHPRequired As Integer = 0
        Public UHId As Integer = 0
        Public AdvertiseMsg As String = ""
        Public AutoEaterSmart As Integer = 0
        Public SendLocationDestinatary As String = ""
        Public TibiaDirectory As String = ""
        Public TibiaFilename As String = ""

        Public OpenCommand As String = ""
        Public CharCommand As String = ""
        Public GuildMembersCommand As String = ""
        Public GuildMembersOnlineOnly As Boolean = False
        Public HealFriendCharacterName As String = ""
        Public HealFriendHealthPercentage As UShort = 0
        Public HealFriendHealType As HealTypes = HealTypes.None
        Public AlarmsActivated As Boolean = False

        Public ChatMessageQueueList As New List(Of ChatMessageDefinition)
        Public ChatMessageLastSent As Date = Date.MinValue

        Public HotkeyWindowWasOpened As Boolean = False


        'Public MapReaderIsBusy As Boolean = False

        Public FisherSpeed As UShort = 0
        Public FisherMinimumCapacity As Integer = 0
        Public FisherTurbo As Boolean = False

        Public RunemakerSpell As SpellDefinition = Nothing
        Public RunemakerManaPoints As Integer = 0
        Public RunemakerSoulPoints As Integer = 0

        'Public AttackTrainerEntityID As Integer = 0
        Public AutoTrainerEntities As New List(Of Integer)
        Public AutoTrainerMinHPPercent As Integer = 0
        Public AutoTrainerMaxHPPercent As Integer = 0
        Public AttackTrainerEntityIsHealing As Boolean = False
        Public AutoAttackerListEnabled As Boolean = False
        Public AutoAttackerList As New List(Of String)

        Public AmmoRestackerItemID As UShort = 0
        Public AmmoRestackerMinimumItemCount As Integer = 0
        Public AmmoRestackerOutOfAmmo As Boolean = False

        Public PickUpItemID As UShort = 0

        Public HealMinimumHP As UShort = 0
        Public HealSpell As SpellDefinition
        Public HealComment As String = ""

        Public HealPartyMinimumHPPercentage As Integer = 0
        Public HealPartyHealType As HealTypes = HealTypes.None

        Public UploaderUrl As String = ""
        Public UploaderFilename As String = ""
        Public UploaderPath As String = ""
        Public UploaderUserId As String = ""
        Public UploaderPassword As String = ""
        Public UploaderSaveToDiskOnly As Boolean = False

        Public LoggingEnabled As Boolean = False

        Public StatsUploaderScreenLastUpdate As Date = Date.MinValue

        Public LooterMinimumCapacity As UShort = 0
        Public LooterCurrentCategory As Integer = 0
        Public BagOpened As Boolean = False

        Public TradeWatcherActive As Boolean = False
        Public TradeWatcherRegex As String = ""

        Public MagicShieldActivated As Boolean = False
        'Public MagicShieldTimerIsDone As Boolean = True
        'Public MagicShieldIsBusy As Boolean = False

        Public FeedbackLastSend As Date = Date.MinValue

        Private IsGreetingSent As Boolean = False
        Private GreetingSentTry As Integer = 0

        Public ExecutablePath As String = ""
        Public LoginServer As String = ""
        Public LoginPort As Integer = 7171
        Public IsPrivateServer As Boolean = False

        Public LastUpdateCheck As Date = Date.MinValue

        Public RainbowOutfitHead As Integer = 0
        Public RainbowOutfitBody As Integer = 0
        Public RainbowOutfitLegs As Integer = 0
        Public RainbowOutfitFeet As Integer = 0

        Public TibiaWindowState As WindowState = WindowState.Active
        Public TibiaClientIsVisible As Boolean = True

        Public FrameRateBegin As Integer = 0
        Public FrameRateActive As Integer = 0
        Public FrameRateInactive As Integer = 0
        Public FrameRateMinimized As Integer = 0
        Public FrameRateHidden As Integer = 0

        Public DrinkerManaRequired As Integer = 0

        Public Walker_Waypoints As New List(Of Walker)
        Public WaypointIndex As Integer = 0
        Public WalkerWaitUntil As DateTime
        Public WalkerFirstTime As Boolean = True
        Public IsOpeningReady As Boolean = True
        Public CBState As CavebotState = CavebotState.Walking
        Public CBContainerCount As Integer = 0
        Public CBCreatureDied As Boolean = False
        Public LearningMode As Boolean = False
        Public IgnoreCreature As List(Of Integer)

        Public LooterItemID As Integer = 0
        Public LooterLoc As LocationDefinition
        Public ReplacedContainer As Boolean = False
        Public WaitTime As DateTime


        Public FakingTitle As Boolean = False

        Public ShowCreaturesUntilNextLevel As Boolean = False

        Public WalkerLoop As Boolean = False

        Public CharacterStatisticsTime As New DateTime

        Public AutoAddTime As New Date
        Public LastFloor As Integer = 0

        Public ComboBotEnabled As Boolean = False
        Public ComboBotLeader As String = ""

        Public AmuletID As Integer = 0

        Public RingID As Integer = 0

        Public LastActivity As Date = Date.Now

        Public TTMessages As Integer = 0

        Public NameSpyActivated As Boolean = False
#End Region

#Region " Memory Reading/Writing "
        Public Sub WriteMemory(ByVal Address As Integer, ByVal Value As Integer, ByVal Size As Integer)
            Try
                Dim bytArray() As Byte
                bytArray = BitConverter.GetBytes(Value)
                Win32API.WriteProcessMemory(Proxy.Client.Handle.ToInt32, Address, bytArray, Size, 0)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub WriteMemory(ByVal Address As Integer, ByVal Value() As Byte)
            Try
                Win32API.WriteProcessMemory(Proxy.Client.Handle.ToInt32, Address, Value, Value.Length, 0)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub WriteMemory(ByVal Address As Integer, ByVal Value() As Byte, ByVal Offset As Integer, ByVal Length As Integer)
            Try
                Dim Count1 As Integer
                For Count1 = 0 To Length - 1
                    WriteMemory(Address + Count1, Value(Count1 + Offset), 1)
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub WriteMemory(ByVal Address As Integer, ByVal Value As String)
            Try
                Dim Length As Integer = Value.Length
                For I As Integer = 0 To Length - 1
                    WriteMemory(Address + I, Asc(Value.Chars(I)), 1)
                Next
                WriteMemory(Address + Length, 0, 1)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub WriteMemory(ByVal Address As Integer, ByVal Value As Double)
            Try
                Dim Buffer(0 To 7) As Byte
                Buffer = BitConverter.GetBytes(Value)
                For I As Integer = 0 To 7
                    WriteMemory(Address + I, CInt(Buffer(I)), 1)
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value As Double)
            Try
                Dim Buffer(7) As Byte
                Dim Temp As Integer
                For I As Integer = 0 To 7
                    ReadMemory(Address + I, Temp, 1)
                    Buffer(I) = CByte(Temp)
                Next
                Value = BitConverter.ToDouble(Buffer, 0)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value As Integer, ByVal Size As Integer)
            Try
                Win32API.ReadProcessMemory(Proxy.Client.Handle.ToInt32, Address, Value, Size, 0)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value As UInteger, ByVal Size As Integer)
            Try
                Dim mValue As Integer = 0
                Win32API.ReadProcessMemory(Proxy.Client.Handle.ToInt32, Address, mValue, Size, 0)
                Value = CUInt(mValue)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value() As Byte, ByVal Length As Integer)
            Try
                Dim bytArray() As Byte
                Dim Count1 As Integer
                Dim tempInteger As Integer
                ReDim bytArray(Length - 1)
                For Count1 = 0 To Length - 1
                    ReadMemory(Address + Count1, tempInteger, 1)
                    bytArray(Count1) = CByte(tempInteger)
                Next
                Value = bytArray
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value As String)
            Try
                Dim intChar As Integer
                Dim Count1 As Integer
                Dim strTemp As String
                strTemp = ""
                Count1 = 0
                Do
                    ReadMemory(Address + Count1, intChar, 1)
                    If intChar <> 0 Then strTemp = strTemp & Chr(intChar)
                    Count1 += 1
                Loop Until intChar = 0
                Value = strTemp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ReadMemory(ByVal Address As Integer, ByRef Value As String, ByVal Length As Integer)
            Try
                Dim intChar As Integer
                Dim Count1 As Integer
                Dim strTemp As String
                strTemp = ""
                For Count1 = 0 To Length - 1
                    ReadMemory(Address + Count1, intChar, 1)
                    strTemp = strTemp & Chr(intChar)
                Next
                Value = strTemp
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub ChangeClientTitle(ByVal strString As String)
            Try
                Win32API.SetWindowText(Proxy.Client.MainWindowHandle.ToInt32, strString)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub
#End Region

#Region " Initialization"

        Public Sub New()
            Try
                Consts = New Constants()
                Definitions = New Items
                Outfits = New Outfits()
                Spells = New Spells()
                LootItems = New LootItems()
                Creatures = New Creatures()
                HotkeySettings = New HotkeySettings()
                BGWOpenCommand = New BackgroundWorker()
                BGWCharCommand = New BackgroundWorker()
                BGWGuildMembersCommand = New BackgroundWorker()
                BGWUpdateChecker = New BackgroundWorker()
                BGWMapViewer = New BackgroundWorker()
                BGWSendLocation = New BackgroundWorker()
                StatsTimerObj = New ThreadTimer(300)
                LightTimerObj = New ThreadTimer(500)
                ExpCheckerTimerObj = New ThreadTimer(1000)
                GreetingTimerObj = New ThreadTimer(10000)
                SpellTimerObj = New ThreadTimer(1000)
                UHTimerObj = New ThreadTimer(1000)
                AdvertiseTimerObj = New ThreadTimer(125000)
                EaterTimerObj = New ThreadTimer(0)
                HealFriendTimerObj = New ThreadTimer(300)
                ChatMessageQueueTimerObj = New ThreadTimer(2500)
                MapReaderTimerObj = New ThreadTimer(100)
                FisherTimerObj = New ThreadTimer(1000)
                RunemakerTimerObj = New ThreadTimer(1000)
                MagicShieldTimerObj = New ThreadTimer(1000)
                AutoTrainerTimerObj = New ThreadTimer(500)
                AmmoRestackerTimerObj = New ThreadTimer(1000)
                PickUpTimerObj = New ThreadTimer()
                HealTimerObj = New ThreadTimer(300)
                HealPartyTimerObj = New ThreadTimer(300)
                StatsUploaderTimerObj = New ThreadTimer()
                LooterTimerObj = New ThreadTimer(500)
                StackerTimerObj = New ThreadTimer()
                ShowInvisibleCreaturesTimerObj = New ThreadTimer(500)
                AutoPublishLocationTimerObj = New ThreadTimer(Consts.AutoPublishLocationInterval)
                RainbowOutfitTimerObj = New ThreadTimer(50)
                Map = New MapReader()
                FPSChangerTimerObj = New ThreadTimer(1000)
                TibiaClientStateTimerObj = New ThreadTimer(1000)
                AutoDrinkerTimerObj = New ThreadTimer(300)
                CaveBotTimerObj = New ThreadTimer(100)
                AutoAttackerTimerObj = New ThreadTimer(500)
                BGWLooter = New BackgroundWorker()
                WindowTimerObj = New ThreadTimer(100)
                WalkerTimerObj = New ThreadTimer(100)
                AutoAddTimerObj = New ThreadTimer(100)
                AmuletChangerTimerObj = New ThreadTimer(300)
                RingChangerTimerObj = New ThreadTimer(300)
                IRCClient = New IrcClient(IRCServer, IRCPort)
                AntiLogoutObj = New ThreadTimer(Consts.AntiLogoutInterval)
                TTMessagesTimerObj = New ThreadTimer(Consts.TTMessagesInterval)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Background Workers "

#Region " Looter BG Worker "

        Private Sub BGWLooter_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWLooter.DoWork
            Try
                System.Threading.Thread.Sleep(300)
                If LooterItemID = 0 Then Exit Sub
                Dim N As Byte = 0
                'N = Container.ContainerCount
                'If N > &HF Then N = &HF
                N = &HE
                Dim buffer() As Byte = UseObjectOnGround(LooterItemID, LooterLoc, N)
                If (buffer(11) - 1) = Container.ContainerCount Then
                    ReplacedContainer = True
                Else
                    ReplacedContainer = False
                End If
                'ConsoleWrite(BytesToStr(buffer))
                Proxy.SendPacketToServer(buffer)
                LooterItemID = 0
                If CBState = CavebotState.OpeningBody Then
                    WaitTime = Date.Now.AddSeconds(5)
                    IsOpeningReady = True
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        'Test with normal function
        Private Sub LootMonster()
            Try
                System.Threading.Thread.Sleep(300)
                If LooterItemID = 0 Then Exit Sub
                Dim N As Byte = 0
                'N = Container.ContainerCount
                'If N > &HF Then N = &HF
                N = &HE
                Dim buffer() As Byte = UseObjectOnGround(LooterItemID, LooterLoc, N)
                If (buffer(11) - 1) = Container.ContainerCount Then
                    ReplacedContainer = True
                Else
                    ReplacedContainer = False
                End If
                'ConsoleWrite(BytesToStr(buffer))
                Proxy.SendPacketToServer(buffer)
                LooterItemID = 0
                If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                    WaitTime = Date.Now.AddSeconds(5)
                    IsOpeningReady = True
                    CBCreatureDied = False
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Send Location BG Worker "

        Private Sub BGWSendLocation_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWSendLocation.DoWork
            Try
                Dim R As New Random(System.DateTime.Now.Millisecond)
                Dim Key As Integer = R.Next(1000, 9999)
                Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("charname=" & System.Web.HttpUtility.UrlEncode(Proxy.CharacterName) & "&x=" & CharacterLoc.X & "&y=" & CharacterLoc.Y & "&z=" & CharacterLoc.Z & "&key=" & Key)
                Dim Client As New WebClient
                Client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim URI As New System.Uri("http://www.tibiatek.com/updatemaploc.php")
                Client.UploadData(URI, "POST", Content)
                Dim ChatMessage As ChatMessageDefinition
                ChatMessage.Destinatary = SendLocationDestinatary
                ChatMessage.MessageType = MessageType.PM
                ChatMessage.Message = "http://www.tibiatek.com/map.php?charname=" & System.Web.HttpUtility.UrlEncode(Proxy.CharacterName) & "&key=" & Key & "#pointer"
                ChatMessageQueueList.Add(ChatMessage)
                ConsoleWrite("Your location will be sent to " & SendLocationDestinatary & " , you can check it yourself by typing: &open """ & ChatMessage.Message & ".")
            Catch Ex As Exception
                ConsoleWrite("Error: The operation was not successful. Message: " & Ex.Message)
            End Try
        End Sub

#End Region

#Region " Map Viewer BG Worker "

        Private Sub BGWMapViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWMapViewer.DoWork
            Try
                MapViewerForm.ShowDialog()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Char Command BG Worker "

        Private Sub BGWCharCommand_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWCharCommand.DoWork
            Try
                Dim URL As String = "http://www.tibia.com/community/?subtopic=character&name=" & CharCommand
                Dim Client As WebClient
                Dim Data As Stream
                Dim Reader As StreamReader
                Dim Line As String
                Dim MatchObj As Match = Nothing
                Dim CharacterName As String = ""
                Dim Vocation As String = ""
                Dim Sex As String = ""
                Dim Level As String = ""
                Dim World As String = ""
                Dim Residence As String = ""
                Dim House As String = ""
                Dim GuildRank As String = ""
                Dim GuildName As String = ""
                Dim LastLogin As String = ""
                Dim Comment As String = ""
                Dim OnComment As Boolean = False
                Dim OnDeath As Boolean = False
                Dim Death As String = ""
                Dim DeathCount As Integer = 0
                Dim NoMoreDeath As Boolean = False
                Dim Output As String = ""
                Try
                    Client = New WebClient()
                    Data = Client.OpenRead(URL)
                    Reader = New StreamReader(Data)
                    Line = Reader.ReadLine()
                    While (Not Line Is Nothing)
                        MatchObj = Regex.Match(Line, "Name:</TD><TD>([^<]+)</TD>.*Sex:</TD><TD>([^<]+)</TD>.*Profession:</TD><TD>([^<]+)</TD>" & _
                            ".*Level:</TD><TD>([^<]+)</TD>.*World:</TD><TD>([^<]+)</TD>.*Residence:</TD><TD>([^<]+)</TD>" & _
                            ".*Last login:</TD><TD>([^<]+)</TD>")
                        If MatchObj.Success Then
                            CharacterName = MatchObj.Groups(1).ToString
                            Sex = MatchObj.Groups(2).ToString
                            Select Case MatchObj.Groups(3).ToString
                                Case "Knight"
                                    Vocation = "K"
                                Case "Elite Knight"
                                    Vocation = "EK"
                                Case "Druid"
                                    Vocation = "D"
                                Case "Elder Druid"
                                    Vocation = "ED"
                                Case "Sorcerer"
                                    Vocation = "S"
                                Case "Master Sorcerer"
                                    Vocation = "MS"
                                Case "Paladin"
                                    Vocation = "P"
                                Case "Royal Paladin"
                                    Vocation = "RP"
                                Case Else
                                    Vocation = "NV"
                            End Select
                            Level = MatchObj.Groups(4).ToString
                            World = MatchObj.Groups(5).ToString
                            Residence = MatchObj.Groups(6).ToString
                            LastLogin = MatchObj.Groups(7).ToString.Replace("&#160;", " ")
                            Output = CharacterName & " L" & Level & " " & Vocation & Ret & _
                                Residence & ", " & World

                            MatchObj = Regex.Match(Line, "membership:</TD><TD>(.*)</TD>")
                            If MatchObj.Success Then
                                GuildRank = MatchObj.Groups(1).ToString
                                GuildRank = GuildRank.Substring(0, GuildRank.IndexOf(" of the <A"))
                                GuildName = MatchObj.Groups(1).ToString
                                GuildName = GuildName.Substring(GuildName.IndexOf(""">") + 2, GuildName.IndexOf("</A") - GuildName.IndexOf(""">") - 2)
                                GuildName = GuildName.Replace("&#160;", " ")
                                Output &= Ret & GuildRank & " of the " & GuildName
                            End If
                            MatchObj = Regex.Match(Line, "House:</TD><TD>([^<]+)</TD>")
                            If MatchObj.Success Then
                                House = MatchObj.Groups(1).ToString
                                House = House.Substring(0, House.IndexOf(" is paid until"))
                                Output &= Ret & House
                            End If
                            Output &= Ret & LastLogin
                        End If
                        MatchObj = Regex.Match(Line, "Account&#160;Status:</TD><TD>([^<]+)</TD>")
                        If MatchObj.Success Then
                            Output &= Ret & MatchObj.Groups(1).ToString
                        End If
                        MatchObj = Regex.Match(Line, "Comment:</TD><TD>(.*)")
                        If MatchObj.Success Then
                            OnComment = True
                            Comment = MatchObj.Groups(1).ToString
                        ElseIf OnComment Then
                            MatchObj = Regex.Match(Line, "([^<]*)</TD></TR>")
                            If MatchObj.Success Then
                                If MatchObj.Groups(1).ToString.Length > 0 Then
                                    Comment &= Ret & MatchObj.Groups(1).ToString
                                End If
                                OnComment = False
                            Else
                                Comment &= Ret & Line
                            End If
                        End If

                        '<TR BGCOLOR=#F1E0C6><TD WIDTH=25%>
                        '<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%>([^<]+)</TD><TD>((?:Killed|Died)\sat\sLevel\s\d+\sby)\s(?:(?:an|a)\s.*?\sof\s<A HREF=""http://www\.tibia\.com/community/\?subtopic=characters&name=[^""]+"">([^<]+)</A>|<A HREF=""http://www\.tibia\.com/community/\?subtopic=characters&name=[^""]+"">([^<]+)</A>|(?:a|an)\s([^<]+))</TD></TR>
                        MatchObj = Regex.Match(Line, "<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%>([^<]+)</TD><TD>((?:Killed|Died)\sat\sLevel\s\d+\sby)\s(?:an|a)\s.*?\sof\s<A HREF=""http://www\.tibia\.com/community/\?subtopic=characters&name=[^""]+"">([^<]+)</A></TD></TR>")
                        If MatchObj.Success Then
                            If Not OnDeath Then OnDeath = True
                            If DeathCount < 5 Then
                                Death &= Ret & MatchObj.Groups(1).Value.Replace("&#160;", " ") & ", " & MatchObj.Groups(2).Value & ": " & MatchObj.Groups(3).Value.Replace("&#160;", " ")
                            End If
                            DeathCount += 1
                        Else
                            MatchObj = Regex.Match(Line, "<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%>([^<]+)</TD><TD>((?:Killed|Died)\sat\sLevel\s\d+\sby)\s<A HREF=""http://www\.tibia\.com/community/\?subtopic=characters&name=[^""]+"">([^<]+)</A></TD></TR>")
                            If MatchObj.Success Then
                                If Not OnDeath Then OnDeath = True
                                If DeathCount < 5 Then
                                    Death &= Ret & MatchObj.Groups(1).Value.Replace("&#160;", " ") & ", " & MatchObj.Groups(2).Value & ": " & MatchObj.Groups(3).Value.Replace("&#160;", " ")
                                End If
                                DeathCount += 1
                            Else
                                MatchObj = Regex.Match(Line, "<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%>([^<]+)</TD><TD>((?:Killed|Died)\sat\sLevel\s\d+\sby)\s(?:a|an)\s([^<]+)</TD></TR>")
                                If MatchObj.Success Then
                                    If Not OnDeath Then OnDeath = True
                                    If DeathCount < 5 Then
                                        Death &= Ret & MatchObj.Groups(1).Value.Replace("&#160;", " ") & ", " & MatchObj.Groups(2).Value & ": " & MatchObj.Groups(3).Value.Replace("&#160;", " ")
                                    End If
                                    DeathCount += 1
                                End If
                            End If
                        End If
                        MatchObj = Regex.Match(Line, "<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%></TD><TD>and by (?:<A HREF=""http://www\.tibia\.com/community/\?subtopic=characters&name=[^""]+"">([^<]+)</A>|([^""]+))</TD></TR>")
                        If MatchObj.Success Then
                            If DeathCount <= 5 And Not NoMoreDeath Then
                                Death &= " and " & MatchObj.Groups(1).Value.Replace("&#160;", " ")
                                If DeathCount = 5 Then NoMoreDeath = True
                            End If
                        End If
                        '<TR BGCOLOR=#(?:F1E0C6|D4C0A1)><TD WIDTH=25%></TD><TD>and by <A HREF="http://www\.tibia\.com/community/\?subtopic=characters&name=[^"]+">([^<]+)</A></TD></TR>
                        Line = Reader.ReadLine()
                    End While
                    If String.IsNullOrEmpty(Output) Then
                        Core.ConsoleWrite("The character does not exist.")
                    Else : Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, Output))
                        'Core.ConsoleWrite(Output)
                    End If
                    If Comment.Length > 0 Then
                        Dim RegExp As New Regex("&[^;]+;")
                        Comment = RegExp.Replace(Comment, "")
                        Core.ConsoleWrite("Comment: " & Comment.Replace("<br />", ""))
                    Else
                        Core.ConsoleWrite("Comment not available.")
                    End If
                    If OnDeath Then
                        Core.ConsoleWrite("Last 5 Deaths:" & Death)
                    End If
                    Reader.Close()
                    Data.Close()
                Catch Exception As WebException
                    Core.ConsoleWrite("Error while fetching URL with message """ & Exception.Message & """.")
                End Try
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Open Command BG Worker "

        Private Sub BGWOpenCommand_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWOpenCommand.DoWork
            Try
                System.Diagnostics.Process.Start(OpenCommand)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Guild Members BG Worker "

        Private Sub BGWGuildMembersCommand_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWGuildMembersCommand.DoWork
            Try
                Dim URL As String = "http://www.tibia.com/community/?subtopic=guilds&page=view&GuildName=" & GuildMembersCommand
                Dim Client, Client2 As WebClient
                Dim Data, Data2 As Stream
                Dim Reader, Reader2 As StreamReader
                Dim Line As String
                Dim MatchObj As Match = Nothing
                Dim World As String = ""
                Dim Found As Boolean = False
                Dim Output As String = ""
                Dim Players As New List(Of String)
                Dim RegExp As Regex = New Regex(""">([^<]+)</A></TD>")
                Dim MCollection As MatchCollection = Nothing
                Dim CurrentPlayer As String
                Dim Index As Integer = 0
                Dim I As Integer = 0
                Try
                    Client = New WebClient()
                    Data = Client.OpenRead(URL)
                    Reader = New StreamReader(Data)
                    Line = Reader.ReadLine()
                    While (Not Line Is Nothing)
                        MatchObj = Regex.Match(Line, "founded\son\s([^\s]+)\son\s")
                        If MatchObj.Success Then
                            World = MatchObj.Groups(1).ToString
                        End If
                        MatchObj = Regex.Match(Line, """>([^<]+)</A>(\s\([^\)]+\))?</TD>")
                        If MatchObj.Success Then
                            Players.Add(MatchObj.Groups(1).ToString)
                        End If
                        Line = Reader.ReadLine()
                    End While
                    If World.Length = 0 Then
                        Core.ConsoleWrite("This guild does not exist, make sure you typed it correctly (case-sensitive).")
                    Else
                        Client2 = New WebClient
                        Data2 = Client2.OpenRead("http://www.tibia.com/community/?subtopic=whoisonline&world=" & World & "&order=level")
                        Reader2 = New StreamReader(Data2)
                        Line = Reader2.ReadLine()
                        While (Not Line Is Nothing)
                            MCollection = RegExp.Matches(Line)
                            If MCollection.Count > 0 Then
                                Core.ConsoleWrite("~~~~~~~Online Players~~~~~~~")
                                For I = 0 To MCollection.Count - 1
                                    CurrentPlayer = MCollection(I).Groups(1).ToString
                                    Index = Players.IndexOf(CurrentPlayer)
                                    If Index > -1 Then
                                        Core.ConsoleWrite(CurrentPlayer.Replace("&#160;", " "))
                                        Players.RemoveAt(Index)
                                    End If
                                Next
                            End If
                            Line = Reader2.ReadLine
                        End While
                        If Not GuildMembersOnlineOnly AndAlso Players.Count > 0 Then
                            Core.ConsoleWrite("~~~~~~~Offline Players~~~~~~~")
                            Players.Sort()
                            For I = 0 To Players.Count - 1
                                Core.ConsoleWrite(Players(I).Replace("&#160;", " "))
                            Next
                        End If
                        Reader2.Close()
                        Data2.Close()
                    End If
                    Reader.Close()
                    Data.Close()
                Catch Exception As WebException
                    Core.ConsoleWrite("Error while fetching URL with message """ & Exception.Message & """.")
                End Try
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Update Checker BG Worker "

        Private Sub BGWUpdateChecker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWUpdateChecker.DoWork
            Try
                Dim Reader As New System.Xml.XmlTextReader(Consts.LatestVersionUrl)
                Reader.WhitespaceHandling = WhitespaceHandling.None
                Dim Version As String = ""
                Dim Website As String = ""
                Dim DownloadSize As Integer = 0
                Dim DownloadUrl As String = ""
                While Reader.Read()
                    If Not Reader.NodeType = XmlNodeType.Element Then Continue While
                    Select Case Reader.Name
                        Case "Version"
                            Reader.Read()
                            Version = Reader.Value
                        Case "Website"
                            Reader.Read()
                            Website = Reader.Value
                        Case "DownloadUrl"
                            DownloadSize = CInt(Reader.GetAttribute("Size"))
                            Reader.Read()
                            DownloadUrl = Reader.Value
                    End Select
                End While
                Reader.Close()
                If String.Compare(Version, BotVersion) <> 0 Then
                    Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusWarning, _
                        "There is a new version of TibiaTek Bot available for download. " & _
                        "Get it at: " & Website & "."))
                End If
            Catch
            End Try

        End Sub

#End Region

#End Region

#Region " Thread Timers "

        Public Sub StopEverything()
            Try
                LightC = LightColor.Darkness
                LightI = LightIntensity.None
                LightTimerObj.StopTimer()
                Experience = 0
                Level = 0
                StatsTimerObj.StopTimer()
                NextLevelPercentage = 0
                ExpCheckerTimerObj.StopTimer()
                LastExperience = 0
                AutoAttackerIgnoredID = 0
                AutoAttackerActivated = False
                SpellMsg = ""
                SpellManaRequired = 0
                SpellTimerObj.StopTimer()
                UHHPRequired = 0
                UHTimerObj.StopTimer()
                AdvertiseMsg = ""
                AdvertiseTimerObj.StopTimer()
                AutoEaterSmart = 0
                EaterTimerObj.StopTimer()
                HealFriendCharacterName = ""
                HealFriendHealthPercentage = 0
                HealFriendHealType = HealTypes.None
                HealFriendTimerObj.StopTimer()
                ChatMessageQueueTimerObj.StopTimer()
                ChatMessageQueueList.Clear()
                MapReaderTimerObj.StopTimer()
                FisherTimerObj.StopTimer()
                FisherMinimumCapacity = 0
                FisherSpeed = 0
                RunemakerTimerObj.StopTimer()
                RunemakerSpell = Nothing
                RunemakerManaPoints = 0
                RunemakerSoulPoints = 0
                AutoTrainerTimerObj.StopTimer()
                AutoTrainerEntities.Clear()
                AutoTrainerMinHPPercent = 0
                AutoTrainerMaxHPPercent = 0
                MagicShieldTimerObj.StopTimer()
                AmmoRestackerItemID = 0
                AmmoRestackerMinimumItemCount = 0
                AmmoRestackerOutOfAmmo = False
                AmmoRestackerTimerObj.StopTimer()
                PickUpTimerObj.StopTimer()
                PickUpItemID = 0
                LoggingEnabled = False
                HealTimerObj.StopTimer()
                HealMinimumHP = 0
                HealComment = ""
                StatsUploaderTimerObj.StopTimer()
                LooterMinimumCapacity = 0
                LooterTimerObj.StopTimer()
                If AlarmsActivated Then
                    AlarmsForm.BattlelistAlarmTimer.Stop()
                    AlarmsForm.ItemsAlarmTimer.Stop()
                    AlarmsForm.StatusAlarmTimer.Stop()
                End If
                StackerTimerObj.StopTimer()
                AutoPublishLocationTimerObj.StopTimer()
                ShowInvisibleCreaturesTimerObj.StopTimer()
                RainbowOutfitTimerObj.StopTimer()
                RainbowOutfitHead = 0
                RainbowOutfitBody = 0
                RainbowOutfitLegs = 0
                RainbowOutfitFeet = 0
                DrinkerManaRequired = 0
                AutoDrinkerTimerObj.StopTimer()
                CaveBotTimerObj.StopTimer()
                WaypointIndex = 0
                WalkerFirstTime = True
                CBContainerCount = 0
                AutoAttackerTimerObj.StopTimer()
                WalkerTimerObj.StopTimer()
                AutoAddTimerObj.StopTimer()
                LearningMode = False
                AmuletChangerTimerObj.StopTimer()
                AmuletID = 0
                RingChangerTimerObj.StopTimer()
                RingID = 0
                AntiLogoutObj.StopTimer()
                If Not IRCClient Is Nothing And Consts.IRCConnectOnStartUp Then
                    IRCClient.Quit()
                    IRCClient.Channels.Clear()
                End If
                TTMessagesTimerObj.StopTimer()
                FPSChangerTimerObj.StopTimer()
                Thread.Sleep(500)
                WriteMemory(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenActive))
                Log("Event", "All timers are now stopped.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#Region " Window Timer "

        Private Sub WindowTimerObj_Execute() Handles WindowTimerObj.OnExecute
            Try
                Dim WindowBegin As Integer = 0
                Dim WindowCaption As String = ""
                Dim Title As String
                ReadMemory(Consts.ptrWindowBegin, WindowBegin, 4)
                If WindowBegin = 0 Then 'no window opened
                    If Not InGame() Then
                        If Not (Core.Proxy Is Nothing OrElse Core.Proxy.Client Is Nothing) Then
                            Title = BotName & " - " & Hex(Core.Proxy.Client.Handle.ToInt32) & " - Not Logged In"
                        Else
                            Title = BotName & " - Not Logged In"
                        End If
                        If Not Proxy.Client.MainWindowTitle.Equals(Title) Then
                            ChangeClientTitle(Title)
                        End If
                    Else
                        If HotkeyWindowWasOpened Then
                            HotkeySettings.LoadFromMemory()
                            HotkeySettings.Save()
                            ConsoleWrite("Your hotkeys have been saved.")
                            HotkeyWindowWasOpened = False
                        End If
                    End If
                Else
                    ReadMemory(WindowBegin + Consts.WindowCaptionOffset, WindowCaption)
                    If Not InGame() Then
                        If Not (Core.Proxy Is Nothing OrElse Core.Proxy.Client Is Nothing) Then
                            Title = BotName & " - " & Hex(Core.Proxy.Client.Handle.ToInt32) & " - " & WindowCaption
                        Else
                            Title = BotName & " - " & WindowCaption
                        End If
                        If Not Proxy.Client.MainWindowTitle.Equals(Title) Then
                            ChangeClientTitle(Title)
                        End If
                    Else
                        If Not ExpCheckerActivated AndAlso Not FakingTitle Then
                            Title = BotName & " - " & Proxy.CharacterName
                            If Not Proxy.Client.MainWindowTitle.Equals(Title) Then
                                ChangeClientTitle(Title)
                            End If
                        End If
                        If Not HotkeyWindowWasOpened AndAlso WindowCaption.Equals("Hotkey Options") Then
                            HotkeyWindowWasOpened = True
                        End If
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Tibia Client Status Timer "

        Private Sub TibiaClientStateTimerObj_Execute() Handles TibiaClientStateTimerObj.OnExecute
            Try
                If Not TibiaClientIsVisible Then
                    TibiaWindowState = WindowState.Hidden
                    Return
                End If
                Dim WP As New Win32API.WindowPlacement
                Dim CurrentProcess As Process
                Dim PID As Integer = 1
                Dim hWnd As Integer = 0
                WP.Length = Convert.ToByte(Marshal.SizeOf(GetType(Win32API.WindowPlacement)))
                If Win32API.GetWindowPlacement(Proxy.Client.MainWindowHandle, WP) = False Then
                    Return
                End If
                Select Case WP.ShowCmd
                    Case Win32API.ShowState.SW_SHOWNORMAL, Win32API.ShowState.SW_SHOWMAXIMIZED
                        hWnd = Win32API.GetForegroundWindow()
                        Win32API.GetWindowThreadProcessId(hWnd, PID)
                        If PID = 1 Then
                            TibiaWindowState = WindowState.Inactive
                            Return
                        End If
                        CurrentProcess = Process.GetProcessById(PID)
                        CurrentProcess.Refresh()
                        If CurrentProcess.Id = Proxy.Client.Id Then
                            TibiaWindowState = WindowState.Active
                        Else
                            TibiaWindowState = WindowState.Inactive
                        End If
                    Case Win32API.ShowState.SW_SHOWMINIMIZED
                        TibiaWindowState = WindowState.Minimized
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " FPS Changer Timer "

        Private Sub FPSChangerTimerObj_Execute() Handles FPSChangerTimerObj.OnExecute
            Try
                Select Case TibiaWindowState
                    Case WindowState.Active
                        WriteMemory(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(FrameRateActive))
                        System.Threading.Thread.Sleep(1000)
                    Case WindowState.Inactive
                        WriteMemory(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(FrameRateInactive))
                        System.Threading.Thread.Sleep(1000)
                    Case WindowState.Minimized
                        WriteMemory(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(FrameRateMinimized))
                        System.Threading.Thread.Sleep(1000)
                    Case WindowState.Hidden
                        WriteMemory(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(FrameRateHidden))
                        System.Threading.Thread.Sleep(1000)
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


#End Region

#Region " Show Invisible Creatures Timer "

        Private Sub ShowInvisibleCreaturesTimerObj_Execute() Handles ShowInvisibleCreaturesTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim BL As New BattleList
                BL.Reset(True)
                Do
                    If BL.IsOnScreen AndAlso BL.OutfitID = 0 AndAlso Not BL.IsMyself AndAlso Not BL.IsPlayer Then
                        Dim Outfit As New OutfitDefinition
                        If Outfits.GetOutfitByName("Male Druid", Outfit) Then
                            BL.OutfitID = Outfit.ID
                            BL.HeadColor = 0
                            BL.BodyColor = 0
                            BL.LegsColor = 0
                            BL.FeetColor = 0
                        End If
                    End If
                Loop While BL.NextEntity(True)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Publish Location Timer "

        Private Sub AutoPublishLocationTimerObj_Execute() Handles AutoPublishLocationTimerObj.OnExecute
            SyncLock AutoPublishLocationTimerObj
                Try
                    If Not InGame() Then Exit Sub
                    If String.IsNullOrEmpty(Proxy.CharacterName) Or CharacterLoc.X = 0 Or CharacterLoc.Y = 0 Then Exit Sub
                    Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("charname=" & System.Web.HttpUtility.UrlEncode(Proxy.CharacterName) & "&x=" & CharacterLoc.X & "&y=" & CharacterLoc.Y & "&z=" & CharacterLoc.Z)
                    Dim Client As New WebClient
                    Client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                    Client.UploadDataAsync(New System.Uri(BotWebsite & "/updatemaploc.php"), "POST", Content)
                Catch
                End Try
            End SyncLock
        End Sub

#End Region

#Region " Auto Stacker Timer "

        Private Sub StackerTimerObj_Execute() Handles StackerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim MyContainer As New Container
                Dim SecondContainer As New Container
                Dim ContainerIndex As Integer
                Dim Item As ContainerItemDefinition
                Dim Item2 As ContainerItemDefinition
                Dim ContainerItemCount As Integer
                MyContainer.Reset()
                Do
                    If MyContainer.IsOpened Then
                        'do not stack if it's fake bp
                        If MyContainer.GetContainerIndex = &HF AndAlso MyContainer.GetContainerSize = &H24 Then Continue Do
                        ContainerItemCount = MyContainer.GetItemCount
                        ContainerIndex = MyContainer.GetContainerIndex
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyContainer.Items(I)
                            If DatInfo.GetInfo(Item.ID).IsStackable AndAlso Item.Count < 100 Then
                                If Container.FindItem(Item2, Item.ID, ContainerIndex, I + 1, ContainerIndex, 1, 99) Then
                                    Proxy.SendPacketToServer(MoveObject(Item, Item2.Location))
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                Loop While MyContainer.NextContainer
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Looter Timer "

        Private Sub LooterTimerObj_Execute() Handles LooterTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If Not Consts.UnlimitedCapacity Then
                    Core.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                    If Capacity <= LooterMinimumCapacity Then Exit Sub
                End If
                Dim Container As New Container
                Dim Container2 As New Container
                Dim Item As ContainerItemDefinition
                Dim Item2 As ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim ContainerItemCount2 As Integer
                Dim Found As Boolean = False
                Dim BrownBagID As UShort = Definitions.GetItemID("Brown Bag")
                Container.Reset()
                Do
                    If Container.IsOpened Then
                        If Not Container.GetName.StartsWith("Dead") AndAlso Not Container.GetName.StartsWith("Slain") AndAlso Not Container.GetName.StartsWith("Bag") AndAlso Not Container.GetName.StartsWith("Remains") Then Continue Do
                        If Container.GetName.StartsWith("Bag") AndAlso Container.GetContainerIndex < &HF Then Continue Do
                        ContainerItemCount = Container.GetItemCount
                        For I As Integer = ContainerItemCount - 1 To 0 Step -1
                            Item = Container.Items(I)
                            If CaveBotTimerObj.State = ThreadTimerState.Running Then
                                If CavebotForm.EatFromCorpses.Checked AndAlso Definitions.IsFood(Item.ID) Then
                                    Proxy.SendPacketToServer(UseObject(Item))
                                End If
                                If CavebotForm.LootFromCorpses.Checked Then
                                    If Not Consts.UnlimitedCapacity Then
                                        If Capacity < CInt(CavebotForm.LootMinimumCap.Value) Then
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    Exit Sub
                                End If
                            ElseIf CaveBotTimerObj.State = ThreadTimerState.Running Then
                                If Consts.LootEatFromCorpse AndAlso Definitions.IsFood(Item.ID) Then
                                    Proxy.SendPacketToServer(UseObject(Item))
                                End If
                            End If
                            Item = Container.Items(I)
                            If Item.ID = 0 Then Continue For
                            If Item.ID = BrownBagID AndAlso Not BagOpened AndAlso Consts.LootInBag Then 'got bag!
                                Proxy.SendPacketToServer(OpenContainer(Item, &HF))
                                BagOpened = True
                                System.Threading.Thread.Sleep(Consts.LootInBagDelay)
                                Exit Sub
                            End If
                            If LootItems.IsLootable(Item.ID) Then
                                Found = False
                                If DatInfo.GetInfo(Item.ID).IsStackable Then
                                    Container2.Reset()
                                    Do
                                        'if its a corpse, or a brown bag that has a parent container
                                        'this is NOT always true, but for the sake of simplicity...
                                        If Container2.GetName.StartsWith("Dead") _
                                            OrElse Container2.GetName.StartsWith("Slain") _
                                            OrElse Container.GetName.StartsWith("Remains") _
                                            OrElse (Container2.GetName.StartsWith("Bag") _
                                            AndAlso Container2.HasParent _
                                            AndAlso Container2.GetContainerID = BrownBagID) Then Continue Do
                                        If Container2.IsOpened Then
                                            ContainerItemCount2 = Container2.GetItemCount
                                            For E As Integer = 0 To ContainerItemCount2 - 1
                                                Item2 = Container2.Items(E)
                                                If Item2.Count = &H64 Then Continue For 'already fully stacked, next please..
                                                If Item2.ID = Item.ID Then
                                                    Found = True
                                                    Proxy.SendPacketToServer(MoveObject(Item, Item2.Location, Min(100 - Item2.Count, Item.Count)))
                                                    System.Threading.Thread.Sleep(Consts.LootDelay / 2)
                                                    If (100 - Item2.Count) < Item.Count Then
                                                        If Container2.GetItemCount = Container2.GetContainerSize Then
                                                            Proxy.SendPacketToServer(MoveObject(Item, GetInventorySlotAsLocation(InventorySlots.Backpack), Item.Count - (100 - Item2.Count)))
                                                        Else
                                                            Dim Loc As LocationDefinition
                                                            Loc = Item2.Location
                                                            Loc.Z = 0
                                                            Proxy.SendPacketToServer(MoveObject(Item, Loc, Item.Count - (100 - Item2.Count)))
                                                        End If
                                                        'Proxy.SendPacketToServer(MoveObject(Item, Item2.Location, Item.Count - (100 - Item2.Count)))
                                                    End If
                                                    Exit Do
                                                End If
                                            Next
                                        End If
                                    Loop While Container2.NextContainer
                                End If
                                If Not Found Then Proxy.SendPacketToServer(MoveObject(Item, GetInventorySlotAsLocation(InventorySlots.Backpack)))
                            End If
                        Next
                    End If
                Loop While Container.NextContainer()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Stats Uploader Timer "

        Private Sub StatsUploaderTimerObj_Execute() Handles StatsUploaderTimerObj.OnExecute
            Try
                ' If StatsUploaderIsBusy Then Exit Sub
                Try
                    If Not InGame() Then Exit Sub
                    'StatsUploaderIsBusy = True
                    Dim Client As New System.Net.WebClient
                    Dim BL As New BattleList
                    BL.JumpToEntity(SpecialEntity.Myself)
                    Dim xmlFile As New System.Xml.XmlDocument()

                    Dim xmlStats As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Stats", "")

                    Dim xmlLastUpdate As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "LastUpdate", "")
                    xmlLastUpdate.InnerText = Date.Now.ToLongDateString & " " & Date.Now.ToLongTimeString

                    Dim xmlName As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Name", "")
                    xmlName.InnerText = BL.GetName

                    Dim xmlLevel As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Level", "")
                    xmlLevel.InnerText = CStr(Level)

                    Dim xmlExperience As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Experience", "")
                    xmlExperience.InnerText = CStr(Experience)

                    Dim xmlCurrentLevelExperience As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "CurrentLevelExperience", "")
                    xmlCurrentLevelExperience.InnerText = CStr(CurrentLevelExp)

                    Dim xmlNextLevelExperience As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "NextLevelExperience", "")
                    xmlNextLevelExperience.InnerText = CStr(NextLevelExp)

                    Dim xmlExpForNextLevel As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "ExpForNextLevel", "")
                    xmlExpForNextLevel.InnerText = CStr(NextLevelExp - Experience)

                    Dim xmlHitPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "HitPoints", "")
                    xmlHitPoints.InnerText = CStr(HitPoints)

                    Dim xmlManaPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "ManaPoints", "")
                    xmlManaPoints.InnerText = CStr(ManaPoints)

                    Dim xmlSoulPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "SoulPoints", "")
                    xmlSoulPoints.InnerText = CStr(SoulPoints)

                    Dim xmlCapacity As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Capacity", "")
                    Dim Capacity As Integer = 0
                    Core.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                    xmlCapacity.InnerText = CStr(Capacity)

                    Dim xmlStamina As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Stamina", "")
                    Dim Stamina As Integer = 0
                    Core.ReadMemory(Consts.ptrStamina, Stamina, 4)
                    Dim StaminaTime As TimeSpan = TimeSpan.FromSeconds(Stamina)
                    xmlStamina.InnerText = StaminaTime.ToString

                    Dim xmlSkills As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Skills", "")
                    Dim Skill As Integer = 0
                    Dim SkillPercent As Integer = 0

                    Dim xmlFistFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "FistFighting", "")
                    Dim xmlFistFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.FistFighting * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.FistFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlFistFighting.InnerText = CStr(Skill)
                    xmlFistFightingP.InnerText = CStr(SkillPercent)
                    xmlFistFighting.Attributes.Append(xmlFistFightingP)
                    xmlSkills.AppendChild(xmlFistFighting)

                    Dim xmlClubFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "ClubFighting", "")
                    Dim xmlClubFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.ClubFighting * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.ClubFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlClubFighting.InnerText = CStr(Skill)
                    xmlClubFightingP.InnerText = CStr(SkillPercent)
                    xmlClubFighting.Attributes.Append(xmlClubFightingP)
                    xmlSkills.AppendChild(xmlClubFighting)

                    Dim xmlSwordFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "SwordFighting", "")
                    Dim xmlSwordFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.SwordFighting * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.SwordFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlSwordFighting.InnerText = CStr(Skill)
                    xmlSwordFightingP.InnerText = CStr(SkillPercent)
                    xmlSwordFighting.Attributes.Append(xmlSwordFightingP)
                    xmlSkills.AppendChild(xmlSwordFighting)

                    Dim xmlAxeFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "AxeFighting", "")
                    Dim xmlAxeFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.AxeFighting * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.AxeFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlAxeFighting.InnerText = CStr(Skill)
                    xmlAxeFightingP.InnerText = CStr(SkillPercent)
                    xmlAxeFighting.Attributes.Append(xmlAxeFightingP)
                    xmlSkills.AppendChild(xmlAxeFighting)

                    Dim xmlDistanceFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "DistanceFighting", "")
                    Dim xmlDistanceFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.DistanceFighting * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.DistanceFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlDistanceFighting.InnerText = CStr(Skill)
                    xmlDistanceFightingP.InnerText = CStr(SkillPercent)
                    xmlDistanceFighting.Attributes.Append(xmlDistanceFightingP)
                    xmlSkills.AppendChild(xmlDistanceFighting)

                    Dim xmlShielding As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Shielding", "")
                    Dim xmlShieldingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.Shielding * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.Shielding * Consts.SkillsDist), SkillPercent, 1)
                    xmlShielding.InnerText = CStr(Skill)
                    xmlShieldingP.InnerText = CStr(SkillPercent)
                    xmlShielding.Attributes.Append(xmlShieldingP)
                    xmlSkills.AppendChild(xmlShielding)

                    Dim xmlFishing As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Fishing", "")
                    Dim xmlFishingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Core.ReadMemory(Consts.ptrSkillsBegin + (Skills.Fishing * Consts.SkillsDist), Skill, 1)
                    Core.ReadMemory(Consts.ptrSkillsPercentBegin + (Skills.Fishing * Consts.SkillsDist), SkillPercent, 1)
                    xmlFishing.InnerText = CStr(Skill)
                    xmlFishingP.InnerText = CStr(SkillPercent)
                    xmlFishing.Attributes.Append(xmlFishingP)
                    xmlSkills.AppendChild(xmlFishing)

                    Dim xmlBattlelist As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Battlelist", "")
                    BL.Reset(True)
                    Do
                        If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                            Dim xmlEntity As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Entity", "")
                            Dim Loc As LocationDefinition = BL.GetLocation
                            Dim X As XmlAttribute = xmlFile.CreateAttribute("X")
                            X.Value = Loc.X.ToString
                            Dim Y As XmlAttribute = xmlFile.CreateAttribute("Y")
                            Y.Value = Loc.Y.ToString
                            Dim Z As XmlAttribute = xmlFile.CreateAttribute("Z")
                            Z.Value = Loc.Z.ToString
                            Dim HP As XmlAttribute = xmlFile.CreateAttribute("HP")
                            HP.Value = BL.GetHPPercentage.ToString
                            xmlEntity.InnerText = BL.GetName
                            xmlEntity.Attributes.Append(HP)
                            xmlEntity.Attributes.Append(X)
                            xmlEntity.Attributes.Append(Y)
                            xmlEntity.Attributes.Append(Z)
                            xmlBattlelist.AppendChild(xmlEntity)
                        End If
                    Loop While BL.NextEntity(True)
                    Dim xmlVipList As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "VipList", "")
                    Dim Vip As New VipList
                    Vip.Reset(False)
                    Do
                        If Vip.IsOnline Then
                            Dim xmlPlayer As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Player", "")
                            xmlPlayer.InnerText = Vip.GetName
                            xmlVipList.AppendChild(xmlPlayer)
                        End If
                    Loop While Vip.NextPlayer(False)

                    Dim xmlContainers As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Containers", "")
                    Dim Container As New Container
                    Dim ContainerItemCount As Integer
                    Dim Item As ContainerItemDefinition
                    Container.Reset()
                    Do
                        If Container.IsOpened Then
                            ContainerItemCount = Container.GetItemCount
                            Dim xmlContainer As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Container", "")
                            Dim xmlContainerName As XmlAttribute = xmlFile.CreateAttribute("Name")
                            xmlContainerName.InnerText = Container.GetName
                            xmlContainer.Attributes.Append(xmlContainerName)
                            Dim xmlContainerSize As XmlAttribute = xmlFile.CreateAttribute("Size")
                            xmlContainerSize.InnerText = Container.GetContainerSize.ToString
                            Dim xmlContainerItems As XmlAttribute = xmlFile.CreateAttribute("Items")
                            xmlContainerItems.InnerText = ContainerItemCount.ToString
                            xmlContainer.Attributes.Append(xmlContainerName)
                            xmlContainer.Attributes.Append(xmlContainerItems)
                            xmlContainer.Attributes.Append(xmlContainerSize)
                            For I As Integer = 0 To ContainerItemCount - 1
                                Item = Container.Items(I)
                                Dim xmlItem As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Item", "")
                                Dim xmlItemName As XmlAttribute = xmlFile.CreateAttribute("Name")
                                xmlItemName.InnerText = Definitions.GetItemName(Item.ID)
                                Dim xmlItemID As XmlAttribute = xmlFile.CreateAttribute("ID")
                                xmlItemID.InnerText = Item.ID.ToString
                                Dim xmlItemCount As XmlAttribute = xmlFile.CreateAttribute("Count")
                                xmlItemCount.InnerText = Item.Count.ToString
                                Dim xmlItemSlot As XmlAttribute = xmlFile.CreateAttribute("Slot")
                                xmlItemSlot.InnerText = Item.Slot.ToString
                                xmlItem.Attributes.Append(xmlItemName)
                                xmlItem.Attributes.Append(xmlItemID)
                                xmlItem.Attributes.Append(xmlItemSlot)
                                xmlItem.Attributes.Append(xmlItemCount)
                                xmlContainer.AppendChild(xmlItem)
                            Next
                            xmlContainers.AppendChild(xmlContainer)
                        End If
                    Loop While Container.NextContainer

                    xmlStats.AppendChild(xmlName)
                    xmlStats.AppendChild(xmlLevel)
                    xmlStats.AppendChild(xmlExperience)
                    xmlStats.AppendChild(xmlCurrentLevelExperience)
                    xmlStats.AppendChild(xmlNextLevelExperience)
                    xmlStats.AppendChild(xmlExpForNextLevel)
                    xmlStats.AppendChild(xmlHitPoints)
                    xmlStats.AppendChild(xmlManaPoints)
                    xmlStats.AppendChild(xmlSoulPoints)
                    xmlStats.AppendChild(xmlCapacity)
                    xmlStats.AppendChild(xmlStamina)
                    xmlStats.AppendChild(xmlSkills)
                    xmlStats.AppendChild(xmlBattlelist)
                    xmlStats.AppendChild(xmlVipList)
                    xmlStats.AppendChild(xmlContainers)
                    xmlStats.AppendChild(xmlLastUpdate)

                    xmlFile.AppendChild(xmlStats)

                    If UploaderSaveToDiskOnly Then
                        xmlFile.Save(UploaderPath & UploaderFilename)
                    Else
                        xmlFile.Save("temp.xml")
                        'Dim CS As New CaptureScreen.CaptureScreen
                        'CS.CaptureScreenToFile("screenshot.jpg", ImageFormat.Jpeg)
                        If IO.File.Exists("temp.xml") Then  'AndAlso IO.File.Exists("screenshot.jpg") 
                            Client.UploadFile("ftp://" & UploaderUserId & ":" & UploaderPassword & "@" & UploaderUrl & UploaderPath & UploaderFilename, "Temp.xml")
                            'Client.UploadFile("ftp://" & Consts.StatsUploaderUserID & ":" & Consts.StatsUploaderPassword & "@" & Consts.StatsUploaderUrl & Consts.StatsUploaderPath & "screenshot.jpg", "screenshot.jpg")
                            IO.File.Delete("temp.xml")
                            'IO.File.Delete("screenshot.jpg")
                        End If
                    End If
                Catch
                Finally
                    'StatsUploaderIsBusy = False
                End Try
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Heal Party Timer "

        Private Sub HealPartyTimerObj_Execute() Handles HealPartyTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim BL As New BattleList
                If HealPartyMinimumHPPercentage = 0 OrElse HealPartyHealType = HealTypes.None Then
                    HealPartyMinimumHPPercentage = 0
                    HealPartyHealType = HealTypes.None
                    Exit Sub
                End If

                If HealPartyTimerObj.Interval > Consts.HealersCheckInterval Then HealPartyTimerObj.Interval = Consts.HealersCheckInterval

                BL.Reset(True)
                Do
                    If Not BL.IsOnScreen OrElse BL.IsMyself OrElse BL.GetFloor <> CharacterLoc.Z Then Continue Do
                    Dim PLPartyStatus As PartyStatus = BL.GetPartyStatus
                    If (PLPartyStatus = PartyStatus.Member OrElse PLPartyStatus = PartyStatus.Leader) _
                        AndAlso BL.GetHPPercentage <= HealPartyMinimumHPPercentage Then
                        Select Case HealPartyHealType
                            Case HealTypes.ExuraSio
                                If ManaPoints < Spells.GetSpellMana("Heal Friend") Then Exit Sub
                                SioPlayer(BL.GetName)
                            Case HealTypes.UltimateHealingRune
                                UHOnLocation(BL.GetLocation)
                            Case HealTypes.Both
                                If ManaPoints >= Spells.GetSpellMana("Heal Friend") Then
                                    SioPlayer(BL.GetName)
                                Else
                                    UHOnLocation(BL.GetLocation)
                                End If
                        End Select
                        HealPartyTimerObj.Interval = Consts.HealersAfterHealDelay
                        Exit Sub
                    End If
                Loop While BL.NextEntity(True)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Healer Timer "

        Public Sub HealTimerObj_Execute() Handles HealTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If HealTimerObj.Interval > Consts.HealersCheckInterval Then HealTimerObj.Interval = Consts.HealersCheckInterval
                If HealMinimumHP = 0 OrElse HitPoints > HealMinimumHP Then Exit Sub
                Dim Output As String = HealSpell.Words
                If ManaPoints < Spells.GetSpellMana(HealSpell.Name) Then Exit Sub
                If String.Compare(HealSpell.Name, "heal friend", True) = 0 Then
                    Dim BL As New BattleList
                    BL.JumpToEntity(SpecialEntity.Myself)
                    Output &= " """ & BL.GetName & """"
                End If
                If Not String.IsNullOrEmpty(HealComment) Then Output &= " """"" & HealComment
                HealTimerObj.Interval = Consts.HealersAfterHealDelay
                Proxy.SendPacketToServer(Speak(Output, MessageType.Normal))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " PickUp Timer "

        Public Sub PickUpTimerObj_Execute() Handles PickUpTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If PickUpItemID = 0 Then Exit Sub
                'If PickUpTimerObj.Interval > 500 Then PickUpTimerObj.Interval = 500
                Dim RightHandItemID As Integer = 0
                Dim RightHandItemCount As Integer = 0
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandItemCount, 1)
                If RightHandItemID > 0 AndAlso RightHandItemID <> PickUpItemID Then
                    If RightHandItemCount = 0 Then RightHandItemCount = 1
                    Proxy.SendPacketToServer(MoveObject(RightHandItemID, GetInventorySlotAsLocation(InventorySlots.RightHand), GetInventorySlotAsLocation(InventorySlots.Backpack), CInt(RightHandItemCount)))
                    PickUpTimerObj.Interval = 2000
                    Exit Sub
                End If
                Dim StackSize As Integer = 0
                Dim ItemID As Integer = 0
                Dim ItemCount As Integer = 0
                Dim Capacity As Integer = 0
                Dim Z As Integer = 0
                Dim Source As New LocationDefinition
                'Core.ReadMemory(Consts.ptrCapacity, Capacity, 2)

                Dim MaxItemsToPickUp As Integer = CInt(Fix(Capacity / 20))
                If Consts.UnlimitedCapacity Then
                    MaxItemsToPickUp = 100
                End If
                Dim Address As Integer = 0
                If MaxItemsToPickUp > 0 Then
                    For X As Short = 7 To 9
                        For Y As Short = 5 To 7
                            Address = Map.GetAddress(X, Y, MapReader.WorldZToClientZ(CharacterLoc.Z))
                            Core.ReadMemory(Address, StackSize, 1)
                            If StackSize > 1 Then 'look for spear plx
                                For I As Integer = 0 To StackSize - 1
                                    Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ItemID, 2)
                                    If ItemID = PickUpItemID Then
                                        Core.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectDataOffset, ItemCount, 1)
                                        If ItemCount = 0 Then ItemCount = 1
                                        Source = CharacterLoc
                                        Source.X += X - 8
                                        Source.Y += Y - 6
                                        Proxy.SendPacketToServer(MoveObject(PickUpItemID, Source, GetInventorySlotAsLocation(InventorySlots.RightHand), Min(ItemCount, MaxItemsToPickUp)))
                                        Exit Sub
                                    End If
                                Next
                            End If
                        Next
                    Next
                End If
                'System.Threading.Thread.Sleep(300)
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandItemCount, 1)
                Core.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                MaxItemsToPickUp = CInt(Fix(Capacity / 20))
                If RightHandItemID = 0 And RightHandItemCount = 0 Then
                    'Dim MyContainer As New Container
                    'Dim ContainerItemCount As Integer
                    Dim Item As ContainerItemDefinition
                    Dim Found As Boolean = False

                    If Container.FindItem(Item, PickUpItemID, 0, 0) Then
                        If MaxItemsToPickUp > 0 Then
                            Proxy.SendPacketToServer(MoveObject(PickUpItemID, Item.Location, GetInventorySlotAsLocation(InventorySlots.RightHand), Min(MaxItemsToPickUp, Item.Count)))
                            'PickUpTimerObj.Interval = 200
                        Else
                            ConsoleError("Can't pick up more items because capacity is too low.")
                            'PickUpTimerObj.Interval = 2000
                        End If
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Ammo Restacker Timer "

        Public Sub AmmoRestackerTimerObj_Execute() Handles AmmoRestackerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim Container As New Container
                Dim ContainerItemCount As Integer
                Dim Item As ContainerItemDefinition
                Dim Found As Boolean = False
                Dim AmmoItemID As Integer = 0
                Dim AmmoItemCount As Integer = 0
                Dim TotalAmmo As Integer = 0
                If AmmoRestackerItemID = 0 OrElse AmmoRestackerMinimumItemCount = 0 Then Exit Sub
                'find out of we really need more ammo
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), AmmoItemID, 2)
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, AmmoItemCount, 1)
                If AmmoItemID = AmmoRestackerItemID AndAlso AmmoItemCount > AmmoRestackerMinimumItemCount Then
                    Exit Sub
                End If
                Container.Reset()
                Do
                    If Container.IsOpened Then
                        ContainerItemCount = Container.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            If Container.Items(I).ID = AmmoRestackerItemID Then
                                If Not Found Then
                                    Item = Container.Items(I)
                                    Found = True
                                End If
                                TotalAmmo += Container.Items(I).Count
                            End If
                        Next
                    End If
                Loop While Container.NextContainer()
                If Found Then
                    If AmmoRestackerTimerObj.Interval = 2000 Then AmmoRestackerTimerObj.Interval = 1000
                    If AmmoRestackerOutOfAmmo Then AmmoRestackerOutOfAmmo = False
                    Proxy.SendPacketToServer(MoveObject(AmmoRestackerItemID, Item.Location, GetInventorySlotAsLocation(InventorySlots.Belt), 100 - AmmoItemCount))
                    Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, (TotalAmmo + AmmoItemCount) & " ammunition left."))
                Else
                    If Not AmmoRestackerOutOfAmmo Then
                        AmmoRestackerTimerObj.Interval = 2000
                        Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Warning: You ran out of ammunition."))
                        AmmoRestackerOutOfAmmo = True
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Trainer Timer "

        Public Sub AutoTrainerTimerObj_Execute() Handles AutoTrainerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If AutoTrainerMinHPPercent = 0 OrElse AutoTrainerMaxHPPercent = 0 Then
                    AutoTrainerEntities.Clear()
                    AutoTrainerMinHPPercent = 0
                    AutoTrainerMaxHPPercent = 0
                    Exit Sub
                End If
                Dim BL As New BattleList
                Dim AttackedEntityID As Integer = 0
                Core.ReadMemory(Consts.ptrAttackedEntityID, AttackedEntityID, 4)
                If AttackedEntityID > 0 Then
                    If AutoTrainerEntities.Contains(AttackedEntityID) Then
                        If BL.Find(AttackedEntityID, True) Then
                            If BL.GetHPPercentage > AutoTrainerMinHPPercent Then
                                Exit Sub
                            Else
                                Proxy.SendPacketToServer(PacketUtils.StopEverything)
                            End If
                        End If
                    Else
                        Exit Sub 'do not train if you are attacking something else
                    End If
                End If
                For Each EntityID As Integer In AutoTrainerEntities
                    If BL.Find(EntityID, True) AndAlso BL.GetHPPercentage >= AutoTrainerMaxHPPercent Then
                        BL.Attack()
                    End If
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Runemaker Timer "

        Public Sub RunemakerTimerObj_Execute() Handles RunemakerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim FirstRightHandSlot As Integer = 0
                Dim FirstRightHandSlotCount As Integer = 0
                Dim BeltSlot As Integer = 0
                Dim RightHandSlot As Integer = 0
                Dim RightHandSlotCount As Integer = 0
                Dim Retries As Integer = 0
                Dim MyContainer As New Container
                Dim BlankRune As ContainerItemDefinition
                Dim BlankRuneID As UShort = Definitions.GetItemID("Blank")
                Dim Found As Boolean = False
                Dim Count As Integer = 0
                If RunemakerManaPoints = 0 OrElse RunemakerSoulPoints < 0 Then Exit Sub

                'continue only if there enough mana
                If ManaPoints < RunemakerManaPoints OrElse ManaPoints < RunemakerSpell.ManaPoints Then
                    Exit Sub
                End If
                'exit if not enough soulpoints
                If RunemakerSoulPoints > 0 AndAlso (SoulPoints < RunemakerSoulPoints OrElse SoulPoints < RunemakerSpell.SoulPoints) Then
                    RunemakerManaPoints = 0
                    RunemakerSoulPoints = 0
                    RunemakerTimerObj.StopTimer()
                    ConsoleError("You ran out of Soul Points, therefore the Runemaker is now Disabled.")
                    Exit Sub
                End If
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
                'check that there are no items occupying the belt slot

                If BeltSlot > 0 Then
                    Retries = 0
                    Do
                        Retries += 1
                        If Retries > 20 Then
                            RunemakerManaPoints = 0
                            RunemakerSoulPoints = 0
                            RunemakerTimerObj.StopTimer()
                            ConsoleError("Runemaker is stuck. Can't move an item from belt to the backpack. Runemaker is now disabled.")
                            Exit Sub
                        End If
                        Proxy.SendPacketToServer(MoveObject(BeltSlot, GetInventorySlotAsLocation(InventorySlots.Belt), GetInventorySlotAsLocation(InventorySlots.Backpack), 100))
                        System.Threading.Thread.Sleep(1000)
                        Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
                    Loop While BeltSlot <> 0
                End If

                'find blank rune
                '                Container.FindItem(BlankRune, BlankRuneID, 0, 0, Consts.MaxContainers - 1)

                MyContainer.Reset()
                Dim ContainerItemCount As Integer
                Found = False
                Do
                    If MyContainer.IsOpened Then
                        ContainerItemCount = MyContainer.GetItemCount
                        For I As Integer = 0 To ContainerItemCount - 1
                            If MyContainer.Items(I).ID = BlankRuneID Then
                                BlankRune = MyContainer.Items(I)
                                Found = True
                                Exit Do
                            End If
                        Next
                    End If
                Loop While MyContainer.NextContainer
                If Not Found Then
                    RunemakerManaPoints = 0
                    RunemakerSoulPoints = 0
                    RunemakerTimerObj.StopTimer()
                    ConsoleError("Blank Rune not found. Runemaker is now disabled.")
                    Exit Sub
                End If

                'move any object in right hand to arrow slot
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandSlotCount, 1)
                If RightHandSlot > 0 Then
                    FirstRightHandSlot = RightHandSlot
                    FirstRightHandSlotCount = RightHandSlotCount
                    Count = RightHandSlotCount
                    If Count = 0 Then Count = 1
                    Retries = 0
                    Do
                        Retries += 1
                        If (Retries > 20) Then ' 10 seconds stuck
                            RunemakerManaPoints = 0
                            RunemakerSoulPoints = 0
                            RunemakerTimerObj.StopTimer()
                            ConsoleError("Runemaker is stuck. Can't move item from right hand to belt/arrow slot. Runemaker is now disabled.")
                            Exit Sub
                        End If
                        Proxy.SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(InventorySlots.RightHand), GetInventorySlotAsLocation(InventorySlots.Belt), Count))
                        System.Threading.Thread.Sleep(1000)
                        Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
                    Loop Until FirstRightHandSlot = BeltSlot
                End If

                'move blank rune to right hand
                Retries = 0
                Do
                    Retries += 1
                    If Retries > 20 Then
                        RunemakerManaPoints = 0
                        RunemakerSoulPoints = 0
                        RunemakerTimerObj.StopTimer()
                        ConsoleError("Runemaker is stuck. Can't move blank rune from container to right hand. Runemaker is now disabled.")
                        Exit Sub
                    End If
                    Proxy.SendPacketToServer(MoveObject(BlankRune.ID, BlankRune.Location, GetInventorySlotAsLocation(InventorySlots.RightHand), 1))
                    System.Threading.Thread.Sleep(1000)
                    Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Loop Until RightHandSlot = BlankRune.ID

                'cast conjure
                Retries = 0
                Do
                    Retries += 1
                    If Retries > 20 Then
                        RunemakerManaPoints = 0
                        RunemakerSoulPoints = 0
                        RunemakerTimerObj.StopTimer()
                        ConsoleError("Runemaker is stuck. Unable to conjure spell words to convert the blank rune. Runemaker is now disabled.")
                        Exit Sub
                    End If
                    Proxy.SendPacketToServer(Speak(RunemakerSpell.Words))
                    System.Threading.Thread.Sleep(1000)
                    Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Loop Until RightHandSlot <> BlankRuneID

                'move magical rune to backpack
                Retries = 0
                Do
                    Retries += 1
                    If Retries > 20 Then
                        RunemakerManaPoints = 0
                        RunemakerSoulPoints = 0
                        RunemakerTimerObj.StopTimer()
                        ConsoleError("Runemaker is stuck. Can't move " & RunemakerSpell.Name & " Rune to it's container. Runemaker is now disabled.")
                        Exit Sub
                    End If
                    Proxy.SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(InventorySlots.RightHand), BlankRune.Location, 1))
                    System.Threading.Thread.Sleep(1000)
                    Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Loop Until RightHandSlot = 0

                'move any object that was in arrow slot to right hand
                If FirstRightHandSlot > 0 Then
                    Count = FirstRightHandSlotCount
                    If Count = 0 Then Count = 1
                    Retries = 0
                    Do
                        Retries += 1
                        If Retries > 20 Then
                            RunemakerManaPoints = 0
                            RunemakerSoulPoints = 0
                            RunemakerTimerObj.StopTimer()
                            ConsoleError("Runemaker is stuck. Can't move object in arrow slot to your right hand. Runemaker is now disabled.")
                            Exit Sub
                        End If
                        Proxy.SendPacketToServer(MoveObject(FirstRightHandSlot, GetInventorySlotAsLocation(InventorySlots.Belt), GetInventorySlotAsLocation(InventorySlots.RightHand), Count))
                        System.Threading.Thread.Sleep(1000)
                        Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                    Loop Until RightHandSlot = FirstRightHandSlot
                End If
                System.Threading.Thread.Sleep(5000)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Fisher Timer "

        Public Sub FisherTimerObj_OnExecute() Handles FisherTimerObj.OnExecute
            Try
                If FisherTimerObj.State = ThreadTimerState.Stopped Then Exit Sub
                Dim Intervals() As UShort = {1000, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000}
                If Not InGame() Then Exit Sub
                If FisherTimerObj.Interval = 10000 Then FisherTimerObj.Interval = 1000
                If Map.IsBusy Then Exit Sub
                Dim FishingRodItemData As ContainerItemDefinition
                Dim TileID As Integer
                Dim FishingRodID As UShort = Definitions.GetItemID("Fishing Rod")
                Dim WormID As UShort = Definitions.GetItemID("Worm")
                Dim WormItemData As ContainerItemDefinition
                Dim Tiles As New List(Of TileObject)
                Dim Tile As TileObject
                If Not Container.FindItem(WormItemData, WormID, 0, 0, Consts.MaxContainers - 1) Then
                    ConsoleError("Auto Fisher couldn't find any worms, it is now Disabled.")
                    FisherTimerObj.StopTimer()
                    Exit Sub
                End If
                If Not Container.FindItem(FishingRodItemData, FishingRodID, 0, 0, Consts.MaxContainers - 1) Then
                    ConsoleError("Auto Fisher couldn't find any fishing rods, pausing for 10 seconds.")
                    FisherTimerObj.Interval = 10000
                    Exit Sub
                End If
                If Not Consts.UnlimitedCapacity Then
                    If Capacity < FisherMinimumCapacity Then
                        Exit Sub
                    End If
                End If
                Dim TileObjects() As TileObject
                For XXX As Integer = 1 To 15
                    For YYY As Integer = 1 To 11
                        TileObjects = Map.GetTileObjects(XXX, YYY, MapReader.WorldZToClientZ(CharacterLoc.Z))
                        If TileObjects.Length = 1 Then
                            TileID = TileObjects(0).GetObjectID
                            If TileID >= &H11F5 And TileID <= &H11FA Then
                                Tiles.Add(TileObjects(0))
                            End If
                        End If
                    Next
                Next
                If Tiles.Count > 0 Then
                    Dim RandomNumber As New Random(Date.Now.Second)
                    Tile = Tiles(RandomNumber.Next(Tiles.Count)) 'randomize plx
                    Proxy.SendPacketToServer(UseFishingRodOnLocation(FishingRodItemData, Tile.GetMapLocation, Tile.GetObjectID))
                    If FisherSpeed > 0 AndAlso Not FisherTimerObj.Interval = FisherSpeed Then
                        FisherTimerObj.Interval = FisherSpeed
                    Else
                        FisherTimerObj.Interval = Intervals(RandomNumber.Next(Intervals.Length))
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Map Reader Timer "

        Public Sub MapReaderTimerObj_OnExecute() Handles MapReaderTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim X, Y, Z As Integer
                Core.ReadMemory(Consts.ptrCoordX, X, 2)
                CharacterLoc.X = X
                Core.ReadMemory(Consts.ptrCoordY, Y, 2)
                CharacterLoc.Y = Y
                Core.ReadMemory(Consts.ptrCoordZ, Z, 1)
                CharacterLoc.Z = Z
                If CharacterLastLocation.X <> CharacterLoc.X OrElse _
                    CharacterLastLocation.Y <> CharacterLoc.Y OrElse _
                    CharacterLastLocation.Z <> CharacterLoc.Z Then
                    Map.Refresh()
                    CharacterLastLocation = CharacterLoc
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Chat Message Queue Timer "

        Public Sub ChatMessageQueueTimerObj_Execute() Handles ChatMessageQueueTimerObj.OnExecute
            Try
                If Not InGame() Then
                    ChatMessageQueueList.Clear()
                    Exit Sub
                End If
                If ChatMessageQueueList.Count > 0 Then
                    Dim ChatMessage As New ChatMessageDefinition
                    Dim bytBuffer(1) As Byte
                    Dim Found As Boolean = False
                    For Each ChatMessage In ChatMessageQueueList
                        If ChatMessage.Prioritize Then
                            ChatMessageQueueList.Remove(ChatMessage)
                            Found = True
                            Exit For
                        End If
                    Next
                    If Not Found Then
                        ChatMessage = ChatMessageQueueList.Item(0)
                        ChatMessageQueueList.RemoveAt(0)
                    End If
                    ChatMessageLastSent = Date.Now
                    Select Case ChatMessage.MessageType 'default
                        Case MessageType.Normal 'normal
                            bytBuffer = Speak(ChatMessage.Message, MessageType.Normal)
                            Log("You", "Said in default: " & ChatMessage.Message)
                        Case MessageType.Whisper 'whisper
                            bytBuffer = Speak(ChatMessage.Message, MessageType.Whisper)
                            Log("You", "Whispered in default: " & ChatMessage.Message)
                        Case MessageType.Yell 'yell
                            bytBuffer = Speak(ChatMessage.Message, MessageType.Yell)
                            Log("You", "Yelled in default: " & ChatMessage.Message)
                        Case MessageType.PM 'pm
                            bytBuffer = Speak(ChatMessage.Destinatary, ChatMessage.Message)
                            Log("You", "Said to """ & ChatMessage.Destinatary & """: " & ChatMessage.Message)
                        Case MessageType.Channel 'channels
                            bytBuffer = Speak(ChatMessage.Message, ChatMessage.Channel)
                            Dim Channel As String
                            Select Case ChatMessage.Channel
                                Case ChannelType.Console
                                    Channel = "Console"
                                Case ChannelType.GameChat
                                    Channel = "Game Chat"
                                Case ChannelType.GuildChat
                                    Channel = "Guild Chat"
                                Case ChannelType.Help
                                    Channel = "Help"
                                Case ChannelType.Personal
                                    Channel = "Personal"
                                Case ChannelType.RLChat
                                    Channel = "RL Chat"
                                Case ChannelType.Trade
                                    Channel = "Trade"
                                Case Else
                                    Channel = "Unknown"
                            End Select
                            Log("You", "Said in """ & Channel & """: " & ChatMessage.Message)
                    End Select
                    Proxy.SendPacketToServer(bytBuffer)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Eater Timer "

        Public Sub EaterTimerObj_Execute() Handles EaterTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If AutoEaterSmart > 0 AndAlso HitPoints > AutoEaterSmart Then Exit Sub
                Dim Ate As Boolean = False
                If Consts.EatFromFloorFirst Then
                    Ate = EatFromFloor(Consts.EatFromFloorMaxDistance)
                End If
                If Not Ate Then
                    Ate = EatFromContainers()
                End If
                If Not Ate AndAlso Not Consts.EatFromFloorFirst Then
                    EatFromFloor(Consts.EatFromFloorMaxDistance)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function EatFromFloor(ByVal MaxDistance As Double) As Boolean
            Try
                Dim TileObj As TileObject
                Dim TileObjects() As TileObject
                For Left As Integer = 1 To 17
                    For Top As Integer = 1 To 13
                        Dim Dist As Double = Math.Sqrt(Math.Pow(Left - 8, 2) + Math.Pow(Top - 6, 2))
                        If Dist <= MaxDistance Then
                            TileObjects = Map.GetTileObjects(Left, Top, MapReader.WorldZToClientZ(CharacterLoc.Z))
                            For Each TileObj In TileObjects
                                If Definitions.IsFood(TileObj.GetObjectID) Then
                                    Proxy.SendPacketToServer(UseObjectOnGround(TileObj.GetObjectID, TileObj.GetMapLocation))
                                    Return True
                                End If
                            Next
                        End If
                    Next
                Next
                Return False
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Function EatFromContainers() As Boolean
            Try
                Dim Container As New Container
                Dim Item As ContainerItemDefinition
                Dim ContainerItemCount As Integer = 0
                Dim I As Integer
                Dim Found As Boolean = False
                Container.Reset()
                Do
                    If Container.IsOpened Then
                        ContainerItemCount = Container.GetItemCount
                        For I = 0 To ContainerItemCount - 1
                            Item = Container.Items(I)
                            If Definitions.IsFood(Item.ID) Then
                                Found = True
                                Exit Do
                            End If
                        Next
                    End If
                Loop While Container.NextContainer()
                If Found Then
                    Core.Proxy.SendPacketToServer(UseObject(Item))
                End If
                Return Found
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

#End Region

#Region " Stats Timer "

        Public Sub StatsTimerObj_Execute() Handles StatsTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim X, Y, Z As Integer
                Core.ReadMemory(Consts.ptrLevel, Level, 4)
                Core.ReadMemory(Consts.ptrExperience, Experience, 4)
                Core.ReadMemory(Consts.ptrHitPoints, HitPoints, 4)
                Core.ReadMemory(Consts.ptrManaPoints, ManaPoints, 4)
                Core.ReadMemory(Consts.ptrSoulPoints, SoulPoints, 4)
                Core.ReadMemory(Consts.ptrCoordX, X, 2)
                CharacterLoc.X = X
                Core.ReadMemory(Consts.ptrCoordY, Y, 2)
                CharacterLoc.Y = Y
                Core.ReadMemory(Consts.ptrCoordZ, Z, 1)
                CharacterLoc.Z = Z
                Core.ReadMemory(Consts.ptrCharacterID, CharacterID, 4)
                Core.ReadMemory(Consts.ptrCapacity, Capacity, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Light Effect Timer "

        Public Sub LightTimerObj_Execute() Handles LightTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                SetLight(LightI, LightC)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub SetLight(ByVal LightIntensity As LightIntensity, ByVal LightColor As LightColor)
            Try
                Dim BL As New BattleList
                BL.JumpToEntity(SpecialEntity.Myself)
                If (BL.LightIntensity <> LightIntensity) OrElse (BL.LightColor <> LightColor) Then
                    BL.LightIntensity = LightIntensity
                    BL.LightColor = LightColor
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Experience Checker Timer "

        Public Sub ExpCheckerTimerObj_Execute() Handles ExpCheckerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim NextLevelExpL As Long = 0
                Dim CurrentLevelExpL As Long = 0
                Dim ExperienceL As Long = Experience
                Dim LastExperienceL As Long = LastExperience
                Dim NextLevelPercentageL As Long = NextLevelPercentage
                If ExperienceL < 0 Then Exit Sub
                If ExpCheckerActivated Then
                    If LastExperienceL > 0 AndAlso ExperienceL = LastExperience Then
                        Exit Sub
                    End If
                End If
                NextLevelExpL = CLng(Floor(((16 + (2 / 3)) * Pow(Level + 1, 3)) - (100 * Pow(Level + 1, 2)) + (((283 + (1 / 3)) * (Level + 1)) - 200)))
                CurrentLevelExpL = CLng(Floor(((16 + (2 / 3)) * Pow(Level, 3)) - (100 * Pow(Level, 2)) + (((283 + (1 / 3)) * (Level)) - 200)))
                If (Level = 0) Or (Experience = 0) Then Exit Sub
                NextLevelPercentageL = CLng(Floor((ExperienceL - CurrentLevelExpL) * 100 / (NextLevelExpL - CurrentLevelExpL)))
                If ExpCheckerActivated Then
                    If Not Proxy.Client.MainWindowTitle.Equals(BotName & " - " & Core.Proxy.CharacterName.ToString & " - Exp. For Level " & (Level + 1) & ": " & (NextLevelExp - Experience) & " (" & NextLevelPercentage & "% completed)") Then
                        ChangeClientTitle(BotName & " - " & Core.Proxy.CharacterName.ToString & " - Exp. For Level " & (Level + 1) & ": " & (NextLevelExpL - ExperienceL) & " (" & NextLevelPercentageL & "% completed)")
                    End If
                    LastExperienceL = ExperienceL
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Spell Caster Timer "

        Private Sub SpellTimerObj_Execute() Handles SpellTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If SpellTimerObj.Interval = Consts.SpellCasterDelay Then SpellTimerObj.Interval = Consts.SpellCasterInterval
                If SpellManaRequired = 0 OrElse String.IsNullOrEmpty(SpellMsg) Then Exit Sub
                If ManaPoints = 0 Then
                    Exit Sub
                ElseIf ManaPoints >= SpellManaRequired Then
                    SpellTimerObj.Interval = Consts.SpellCasterDelay
                    Proxy.SendPacketToServer(Speak(SpellMsg))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto UH Timer "

        Private Sub UHTimerObj_Execute() Handles UHTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If UHTimerObj.Interval > Consts.HealersCheckInterval Then UHTimerObj.Interval = Consts.HealersCheckInterval
                If UHHPRequired = 0 Then
                    UHTimerObj.StopTimer()
                    Exit Sub
                End If
                If HitPoints > UHHPRequired Then Exit Sub
                UHTimerObj.Interval = Consts.HealersAfterHealDelay
                Proxy.SendPacketToServer(UseObjectOnPlayerAsHotkey(UHID, CharacterLoc))
                'Proxy.SendPacketToClient(CreatureSpeak(Proxy.CharacterName, MessageType.MonsterSay, 0, "Uh!", CharacterLoc.X, CharacterLoc.Y, CharacterLoc.Z))
                Log("Auto UHer", "Used UH on yourself.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Heal Friend Timer "

        Private Sub HealFriendTimerObj_Execute() Handles HealFriendTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim BL As New BattleList
                If HealFriendCharacterName.Length = 0 OrElse HealFriendHealthPercentage = 0 OrElse HealFriendHealType = HealTypes.None Then
                    HealFriendCharacterName = ""
                    HealFriendHealthPercentage = 0
                    HealFriendHealType = HealTypes.None
                    Exit Sub
                End If
                If HealFriendTimerObj.Interval > Consts.HealersCheckInterval Then HealFriendTimerObj.Interval = Consts.HealersCheckInterval
                If BL.Find(HealFriendCharacterName, True) AndAlso BL.GetFloor = CharacterLoc.Z AndAlso BL.GetHPPercentage <= HealFriendHealthPercentage Then
                    Select Case HealFriendHealType
                        Case HealTypes.ExuraSio
                            If ManaPoints < Spells.GetSpellMana("Heal Friend") Then Exit Sub
                            SioPlayer(HealFriendCharacterName)
                        Case HealTypes.UltimateHealingRune
                            UHOnLocation(BL.GetLocation)
                        Case HealTypes.Both
                            If ManaPoints >= Spells.GetSpellMana("Heal Friend") Then
                                SioPlayer(HealFriendCharacterName)
                            Else
                                UHOnLocation(BL.GetLocation)
                            End If
                    End Select
                    HealFriendTimerObj.Interval = Consts.HealersAfterHealDelay
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub SioPlayer(ByVal Name As String)
            Try
                Proxy.SendPacketToServer(Speak(Spells.GetSpellWords("Heal Friend") & " """ & Name & """"))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub UHOnLocation(ByVal Loc As LocationDefinition)
            Try
                Dim UHRuneID As UShort = Definitions.GetItemID("Ultimate Healing")
                Proxy.SendPacketToServer(UseObjectOnPlayerAsHotkey(UHRuneID, Loc))
                'Proxy.SendPacketToClient(CreatureSpeak(Proxy.CharacterName, MessageType.MonsterSay, 0, "UHed player!", CharacterLoc.X, CharacterLoc.Y, CharacterLoc.Z))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Advertiser Timer "

        Public Sub AdvertiserTimerObj_OnExecute() Handles AdvertiseTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If AdvertiseMsg.Length = 0 Then Exit Sub
                Dim ChatMessage As New ChatMessageDefinition
                ChatMessage.MessageType = MessageType.Channel
                ChatMessage.Channel = ChannelType.Trade
                ChatMessage.Message = AdvertiseMsg
                Core.ChatMessageQueueList.Add(ChatMessage)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Rainbow Outfit Timer "

        Public Sub rainbowTimerObj_Execute() Handles RainbowOutfitTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim BL As New BattleList
                If RainbowOutfitHead = 131 Then RainbowOutfitHead = 0 Else RainbowOutfitHead = RainbowOutfitHead + 1
                If RainbowOutfitBody = 131 Then RainbowOutfitBody = 0 Else RainbowOutfitBody = RainbowOutfitBody + 1
                If RainbowOutfitLegs = 131 Then RainbowOutfitLegs = 0 Else RainbowOutfitLegs = RainbowOutfitLegs + 1
                If RainbowOutfitFeet = 131 Then RainbowOutfitFeet = 0 Else RainbowOutfitFeet = RainbowOutfitFeet + 1
                BL.JumpToEntity(SpecialEntity.Myself)
                Proxy.SendPacketToServer(ChangeOutfit(BL.OutfitID, RainbowOutfitHead, RainbowOutfitBody, RainbowOutfitLegs, RainbowOutfitFeet, BL.Addons))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Auto Drinker Timer "

        Public Sub AutoDrinkerTimerObj_Execute() Handles AutoDrinkerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                If AutoDrinkerTimerObj.Interval > Consts.HealersCheckInterval Then AutoDrinkerTimerObj.Interval = Consts.HealersCheckInterval
                If DrinkerManaRequired = 0 Then Exit Sub
                If ManaPoints = 0 Then
                    Exit Sub
                ElseIf ManaPoints <= DrinkerManaRequired Then
                    Dim ItemID As Integer = 0
                    If Core.IsPrivateServer Then
                        Proxy.SendPacketToServer(UseHotkey(Definitions.GetItemID("Vial"), CInt(Fluids.ManaOpenTibia)))
                    Else
                        Proxy.SendPacketToServer(UseHotkey(Definitions.GetItemID("Vial"), CInt(Fluids.Mana)))
                    End If
                    AutoDrinkerTimerObj.Interval = Consts.HealersAfterHealDelay
                End If

            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


#End Region

#Region " CaveBot Timer "
        Public Function IsLooterReady() As Boolean
            Try
                Dim Item As ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim Container As New Container
                Dim StatusMessage As String = ""
                Dim StatusTmr As Integer = 0
                Container.Reset()
                Core.ReadMemory(Consts.ptrStatusMessage, StatusMessage)
                Core.ReadMemory(Consts.ptrStatusMessageTimer, StatusTmr, 4)
                Core.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                If Capacity < CInt(CavebotForm.LootMinimumCap.Value) Then Return True
                If Capacity <= LooterMinimumCapacity Then Return True
                If StatusMessage.Equals("This object is too heavy.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                If StatusMessage.Equals("You cannot put more objects in this container.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                Do
                    If Not Container.GetName.StartsWith("Dead") And Not Container.GetName.StartsWith("Slain") And Not Container.GetName.StartsWith("Remains") Then Continue Do
                    If Container.IsOpened Then
                        ContainerItemCount = Container.GetItemCount
                        For I As Integer = ContainerItemCount - 1 To 0 Step -1
                            Item = Container.Items(I)
                            If LootItems.IsLootable(Item.ID) Then
                                Return False
                            End If
                        Next
                    End If

                Loop While Container.NextContainer()
                Return True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Function IsEaterReady() As Boolean
            Try
                Dim Item As ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim Container As New Container
                Dim StatusMessage As String = ""
                Dim StatusTmr As Integer = 0
                Container.Reset()
                Core.ReadMemory(Consts.ptrStatusMessage, StatusMessage)
                Core.ReadMemory(Consts.ptrStatusMessageTimer, StatusTmr, 4)
                If StatusMessage.Equals("You are full.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                If StatusMessage.Equals("This object is too heavy.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                Do
                    If Not Container.GetName.StartsWith("Dead") AndAlso Not Container.GetName.StartsWith("Slain") AndAlso Not Container.GetName.StartsWith("Remains") Then Continue Do
                    If Container.IsOpened Then
                        ContainerItemCount = Container.GetItemCount
                        For I As Integer = ContainerItemCount - 1 To 0 Step -1
                            Item = Container.Items(I)
                            If Definitions.IsFood(Item.ID) Then
                                Return False
                            End If
                        Next
                    End If
                Loop While Container.NextContainer
                Return True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Sub CaveBotTimerObj_Execute() Handles CaveBotTimerObj.OnExecute
            Try
                If CaveBotTimerObj.State = ThreadTimerState.Stopped Then Exit Sub
                Dim BL As New BattleList
                Dim MyselfBL As New BattleList
                MyselfBL.JumpToEntity(SpecialEntity.Myself)
                Dim PlayerZ As Integer
                Dim CurrentContCount As Integer = 0
                Dim StatusText As String = ""
                Dim StatusTimer As Integer = 0
                Core.ReadMemory(Consts.ptrCoordZ, PlayerZ, 4)
                Core.ReadMemory(Consts.ptrStatusMessage, StatusText)
                Core.ReadMemory(Consts.ptrStatusMessageTimer, StatusTimer, 4)
                If Not InGame() Then Exit Sub
                Select Case CBState
                    Case CavebotState.Walking
                        CBCreatureDied = False
                        If BL.JumpToEntity(SpecialEntity.Attacked) AndAlso (BL.IsOnScreen Or BattleList.CreaturesOnScreen) Then
                            CBState = CavebotState.Attacking
                            Core.WriteMemory(Consts.ptrGoToX, 0, 4)
                            Core.WriteMemory(Consts.ptrGoToY, 0, 4)
                            Core.WriteMemory(Consts.ptrGoToZ, 0, 1)
                            BL.IsWalking = True
                            Exit Sub
                        End If
                        If BL.JumpToEntity(SpecialEntity.Attacked) Then
                            Exit Sub
                        ElseIf Walker_Waypoints(WaypointIndex).MoveChar() Then
                            WaypointIndex += 1
                        End If
                        If WaypointIndex = Walker_Waypoints.Count Then
                            WaypointIndex = 0
                        End If

                        If Walker_Waypoints(WaypointIndex).Type = Walker.WaypointType.Wait Then
                            If WalkerFirstTime = True Then
                                WalkerWaitUntil = Date.Now.AddSeconds(CDbl(Walker_Waypoints(WaypointIndex).Info))
                            End If
                        End If
                    Case CavebotState.Attacking
                        If BL.JumpToEntity(SpecialEntity.Attacked) Then
                            If BL.GetHPPercentage = 0 Then
                                Core.CBState = CavebotState.OpeningBody
                                Exit Select
                            End If
                        End If
                        If BattleList.CreaturesOnScreen = False Then
                            'Core.ConsoleWrite("Attacking -> Walking")
                            Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
                            CBState = CavebotState.Walking
                        End If
                        Dim Chase As Integer
                        Core.ReadMemory(Consts.ptrChasingMode, Chase, 1)
                        If Chase <> 1 Then
                            Core.WriteMemory(Consts.ptrChasingMode, 1, 1)
                            Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                        End If
                        If BL.GetHPPercentage < 30 And BL.IsWalking = True And MyselfBL.IsWalking = False And BL.GetDistance > 4 Then
                            'Core.ConsoleWrite("PRESS STOP YOU IDIOT. CHAR IS PROBABLY STANDING")
                            Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
                            System.Threading.Thread.Sleep(1000)
                            CBState = CavebotState.Walking
                        End If
                        'Changing CBstate.OpeningBody in the proxy section.
                    Case CavebotState.OpeningBody
                        CBCreatureDied = False
                        If IsOpeningReady = True Then
                            CurrentContCount = Container.ContainerCount
                            If CurrentContCount > CBContainerCount Then
                                CBState = CavebotState.Looting ' : Core.ConsoleWrite("Looting state ->")
                            Else
                                If Date.Now > WaitTime Then

                                    'Core.ConsoleWrite("Running out of time, Walking ->")
                                    CBState = CavebotState.Walking
                                End If
                            End If
                            If ReplacedContainer = True Then CBState = CavebotState.Walking ' : Core.ConsoleWrite("Too much bp's open, walk!")
                        End If
                    Case CavebotState.Looting
                        Dim KeepWalking As Boolean = True
                        If CavebotForm.EatFromCorpses.Checked Then
                            KeepWalking = IsEaterReady()
                        End If
                        If KeepWalking AndAlso CavebotForm.LootFromCorpses.Checked Then
                            KeepWalking = IsLooterReady()
                        End If
                        If KeepWalking Then CBState = CavebotState.Walking
                End Select

            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Auto Attack Timer "
        Public Sub AutoAttackerTimerObj_Execute() Handles AutoAttackerTimerObj.OnExecute
            Try
                Dim BL As New BattleList 'Variables
                Dim AttackerMonsters As New SortedList
                Dim AttackBL As New BattleList
                'MyBL.JumpToEntity(SpecialEntity.Myself)
                If CaveBotTimerObj.State = ThreadTimerState.Running Then
                    If CBState <> CavebotState.Walking OrElse Walker_Waypoints(WaypointIndex).Type = Walker.WaypointType.Wait Then Exit Sub
                End If
                AttackerMonsters.Clear()
                If Consts.SmartAttacker Then 'If Using Smart Attack (whole time attack the nearest creature)
                    BL.Reset()
                    Do
                        If BL.IsMyself Then Continue Do
                        If Not BL.IsPlayer AndAlso BL.IsOnScreen AndAlso BL.GetLocation.Z = Core.CharacterLoc.Z Then
                            If BL.GetDistance < Consts.CavebotAttackerRadius Then
                                If CheckRadius(BL.GetEntityID) = True Then
                                    If AutoAttackerListEnabled Then
                                        If Not Core.AutoAttackerList.Contains(BL.GetName) Then
                                            Exit Sub
                                        End If
                                    End If
                                    If Not AttackerMonsters.ContainsKey(BL.GetDistance) Then 'If list doesn't contain Distance add it
                                        AttackerMonsters.Add(BL.GetDistance, BL.GetEntityID) 'Add distance
                                    End If
                                End If
                            End If
                        End If
                    Loop While BL.NextEntity(True) = True
                    'Attacking part
                    If AttackerMonsters.Count = 0 Then Exit Sub
                    If AttackBL.JumpToEntity(SpecialEntity.Attacked) Then
                        If BL.GetEntityID.Equals(AttackerMonsters.GetByIndex(0)) Then Exit Sub
                    End If
                    If CaveBotTimerObj.State = ThreadTimerState.Running Then
                        CBState = CavebotState.None
                        'Core.ConsoleWrite("AttackerTimer: STOP!")
                        Proxy.SendPacketToServer(PacketUtils.StopEverything)
                        Core.WriteMemory(Consts.ptrGoToX, 0, 4)
                        Core.WriteMemory(Consts.ptrGoToY, 0, 4)
                        Core.WriteMemory(Consts.ptrGoToZ, 0, 1)
                        System.Threading.Thread.Sleep(1000)
                    End If
                    WriteMemory(Consts.ptrAttackedEntityID, AttackerMonsters.GetByIndex(0), 4)
                    Proxy.SendPacketToServer(AttackEntity(AttackerMonsters.GetByIndex(0)))
                    If CaveBotTimerObj.State = ThreadTimerState.Running Then CBState = CavebotState.Attacking
                    System.Threading.Thread.Sleep(2000)
                    Exit Sub
                Else
                    If BL.JumpToEntity(SpecialEntity.Attacked) Then Exit Sub
                    'We are not attacking, so..
                    BL.Reset()
                    'Looping trough battlelist
                    Do
                        If BL.IsMyself Then Continue Do
                        If Not BL.IsPlayer AndAlso BL.IsOnScreen AndAlso BL.GetLocation.Z = Core.CharacterLoc.Z Then
                            If BL.GetDistance < Consts.CavebotAttackerRadius Then
                                If CheckRadius(BL.GetEntityID) = True Then
                                    If AutoAttackerListEnabled Then
                                        If Not Core.AutoAttackerList.Contains(BL.GetName) Then
                                            Exit Sub
                                        End If
                                    End If
                                    If CaveBotTimerObj.State = ThreadTimerState.Running Then
                                        CBState = CavebotState.None
                                        'Core.ConsoleWrite("AttackerTimer: STOP!")
                                        Proxy.SendPacketToServer(PacketUtils.StopEverything)
                                        Core.WriteMemory(Consts.ptrGoToX, 0, 4)
                                        Core.WriteMemory(Consts.ptrGoToY, 0, 4)
                                        Core.WriteMemory(Consts.ptrGoToZ, 0, 1)
                                        System.Threading.Thread.Sleep(1000)
                                    End If
                                    WriteMemory(Consts.ptrAttackedEntityID, BL.GetEntityID, 4)
                                    Proxy.SendPacketToServer(AttackEntity(BL.GetEntityID))
                                    If CaveBotTimerObj.State = ThreadTimerState.Running Then CBState = CavebotState.Attacking
                                    System.Threading.Thread.Sleep(2000)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Loop While BL.NextEntity(True) = True
                End If

            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function CheckRadius(ByVal EntityId As Integer) As Boolean
            Try
                Dim PlayerBL As New BattleList 'Player
                Dim MyBL As New BattleList 'Own
                Dim CreatureBL As New BattleList 'Selected Creature

                MyBL.JumpToEntity(SpecialEntity.Myself)
                If CreatureBL.Find(EntityId, True) = False Then Return True
                'Looping trough battlelist
                PlayerBL.Reset(True)
                Do
                    If PlayerBL.IsMyself Then Continue Do
                    If PlayerBL.IsPlayer AndAlso PlayerBL.GetLocation.Z = MyBL.GetLocation.Z Then
                        If PlayerBL.IsOnScreen Then
                            If PlayerBL.GetDistanceFromLocation(CreatureBL.GetLocation, False, True) <= MyBL.GetDistanceFromLocation(CreatureBL.GetLocation, False, True) + Consts.CavebotAttackerShrinkRadius Then
                                Return False
                            End If
                        End If
                    End If
                Loop While PlayerBL.NextEntity(True)
                Return True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function
#End Region

#Region " Greeting Timer "

        Private Sub GreetingTimer_Execute() Handles GreetingTimerObj.OnExecute
            GreetingTimerObj.StopTimer()
            Try
                ConsoleWrite("Welcome " & Proxy.CharacterName & "!" & Ret & _
                "Don't forget to visit us at: www.tibiatek.com." & Ret & _
                "Please report any bug you may found!" & Ret & _
                "For a list of available commands type: &help.")
                Try
                    Dim Reader As IO.StreamReader
                    ConsoleWrite("Loading your hotkeys, please wait...")
                    If HotkeySettings.Load() Then
                        ConsoleWrite("Hotkeys loaded.")
                    Else
                        ConsoleError("Unable to load your hotkeys.")
                    End If
                    CharacterStatisticsForm.FirstTime = True
                    ConsoleWrite("Loading your configuration, please wait...")
                    MagicShieldActivated = False
                    CharacterStatisticsTime = Now
                    If Consts.AutoPublishLocation Then AutoPublishLocationTimerObj.StartTimer()
                    If Consts.ShowInvisibleCreatures Then ShowInvisibleCreaturesTimerObj.StartTimer()
                    If Consts.IRCConnectOnStartUp Then
                        ConnectToIrc()
                    End If
                    If Not IO.File.Exists(GetProfileDirectory() & "\config.txt") Then
                        ConsoleError("Unable to load your configuration.")
                        Exit Sub
                    End If
                    Reader = IO.File.OpenText(GetProfileDirectory() & "\config.txt")
                    Dim Data As String = Reader.ReadToEnd
                    Dim MCollection As MatchCollection
                    Dim GroupMatch As Match
                    MCollection = [Regex].Matches(Data, "&([^\n;]+)")
                    For Each GroupMatch In MCollection
                        ConsoleRead("&" & GroupMatch.Groups(1).Value)
                        CommandParser(GroupMatch.Groups(1).Value)
                    Next
                    Reader.Close()
                    ConsoleWrite("Configuration loaded.")
                Catch ex As System.IO.IOException
                    ConsoleError("Unable to load your configuration.")
                End Try
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Walker Timer "
        Public Sub WalkerTimerObj_Execute() Handles WalkerTimerObj.OnExecute
            Try
                If CaveBotTimerObj.State = ThreadTimerState.Running Then
                    Core.ConsoleError("You can't use Walker same time as Cavebot")
                    WalkerTimerObj.StopTimer()
                    Exit Sub
                End If
                If Walker_Waypoints(WaypointIndex).MoveChar() Then
                    WaypointIndex += 1
                End If

                If WaypointIndex = Walker_Waypoints.Count Then
                    If WalkerLoop Then
                        WaypointIndex = 0
                    Else
                        Core.ConsoleWrite("Arrived to Destination.")
                        WalkerTimerObj.StopTimer()
                        Exit Sub
                    End If
                End If

                If Walker_Waypoints(WaypointIndex).Type = Walker.WaypointType.Wait Then
                    If WalkerFirstTime = True Then
                        WalkerWaitUntil = Date.Now.AddSeconds(CDbl(Walker_Waypoints(WaypointIndex).Info))
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " AutoAdd Timer "
        Private Function AddWaypointLocation(ByVal Loc As LocationDefinition, ByVal CameDirection As Directions, ByVal HeadingDirection As Directions) As LocationDefinition
            Try
                Dim TD As New TileData
                Dim ReturnLocation As New LocationDefinition
                Dim TempLocation As New LocationDefinition
                ReturnLocation = Loc
                TempLocation = Loc

                'Selecting Came Direction
                Select Case CameDirection
                    Case 0 'From Down to Up
                        'User can only use stairs, ramps, etc.. to go up, so there's only one option
                        Select Case HeadingDirection
                            Case Directions.Down
                                ReturnLocation.Y += 1
                            Case Directions.Left
                                ReturnLocation.X -= 1
                            Case Directions.Right
                                ReturnLocation.X += 1
                            Case Directions.Up
                                ReturnLocation.Y -= 1
                        End Select
                        ReturnLocation.Z += 1
                        Return ReturnLocation
                        Exit Function
                    Case 1 'From Up to Down
                        TD.FindTile(ReturnLocation, False)
                        TD.Get_TileInfo()
                        'Option 1
                        For i As Integer = 0 To TD.Count
                            If Definitions.GetItemKind(TD.ObjectId(i)) = ItemKind.UsableTeleport Or Definitions.GetItemKind(TD.ObjectId(i)) = ItemKind.UsableTeleport2 Then
                                ReturnLocation.Z -= 1
                                Return ReturnLocation
                                Exit Function
                            End If
                        Next
                        'Option 2
                        TempLocation.Z -= 1
                        TD.FindTile(TempLocation, False)
                        TD.Get_TileInfo()
                        For i As Integer = 0 To TD.Count
                            If Definitions.GetItemKind(TD.ObjectId(i)) = ItemKind.Teleport Then
                                ReturnLocation.Z -= 1
                                Return ReturnLocation
                                Exit Function
                            End If
                        Next
                        'Option 3
                        TempLocation.Z += 1 'Restoring temploc
                        TD.FindTile(TempLocation, False)
                        TD.Get_TileInfo()
                        If TD.TileId = 386 Then 'ROPE SPOT
                            ReturnLocation.Z -= 1
                            Return ReturnLocation
                            Exit Function
                        End If
                        'Option 4
                        Select Case HeadingDirection
                            Case Directions.Down
                                TempLocation.Y += 1
                            Case Directions.Left
                                TempLocation.X -= 1
                            Case Directions.Right
                                TempLocation.X += 1
                            Case Directions.Up
                                TempLocation.Y -= 1
                        End Select
                        TD.FindTile(TempLocation, False)
                        TD.Get_TileInfo()
                        Core.ConsoleWrite(TD.Count.ToString)
                        Core.ConsoleWrite(TD.ObjectId(1).ToString)
                        For i As Integer = 0 To TD.Count
                            If Definitions.GetItemKind(TD.ObjectId(i)) = ItemKind.Teleport Then
                                ReturnLocation = TempLocation
                                ReturnLocation.Z -= 1
                                Return ReturnLocation
                                Exit Function
                            End If
                        Next
                        'Option 5
                        TempLocation.Z -= 1
                        TD.FindTile(TempLocation, False)
                        TD.Get_TileInfo()
                        For i As Integer = 0 To TD.Count
                            If Definitions.GetItemKind(TD.ObjectId(i)) = ItemKind.Teleport Then
                                ReturnLocation = TempLocation
                                Return ReturnLocation
                                Exit Function
                            End If
                        Next
                End Select
                'Can't find
                ReturnLocation.X = 0
                ReturnLocation.Y = 0
                ReturnLocation.Z = 0
                Return ReturnLocation
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Sub AutoAddTimerObj_Execute() Handles AutoAddTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim BL As New BattleList
                Dim TD As New TileData
                Dim CameLoc As New LocationDefinition
                Dim WaypointLoc As New LocationDefinition
                Dim CameDir As New Byte '0 = From Down to Up, 1 = From Up to Down
                'Add normal Walking time every 10s
                If Walker_Waypoints.Count <> 0 Then 'List is not empty
                    BL.JumpToEntity(SpecialEntity.Myself)
                    If BL.GetLocation.Z <> LastFloor Then
                        If BL.GetLocation.Z < LastFloor Then CameDir = 0
                        If BL.GetLocation.Z > LastFloor Then CameDir = 1
                        LastFloor = BL.GetLocation.Z
                        CameLoc = BL.GetLocation
                        If Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Ladder And _
                        Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Rope And _
                        Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Sewer And _
                        Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Shovel Then
                            WaypointLoc = AddWaypointLocation(CameLoc, CameDir, BL.GetDirection)
                            If WaypointLoc.X <> 0 Then 'There were no error
                                Dim WalkerChar As New Walker
                                WalkerChar.Coordinates = WaypointLoc
                                WalkerChar.Type = Walker.WaypointType.StairsOrHole
                                WalkerChar.Info = ""
                                Walker_Waypoints.Add(WalkerChar)
                                AutoAddTime = Now.AddSeconds(5) 'Don't add next right away
                                Core.ConsoleWrite("Stairs/Hole waypoint added.")
                            Else
                                Core.ConsoleWrite("Error, can't add waypoint")
                            End If
                        End If
                    End If
                End If
                If AutoAddTime < Date.Now Then
                    Dim WalkerChar As New Walker
                    BL.JumpToEntity(SpecialEntity.Myself)
                    UpdatePlayerPos()
                    If Walker_Waypoints.Count = 0 Then
                        WalkerChar.Coordinates = BL.GetLocation
                        WalkerChar.Type = Walker.WaypointType.Walk
                        WalkerChar.Info = ""
                        Walker_Waypoints.Add(WalkerChar)
                        Core.ConsoleWrite("Walking waypoint added.")
                        AutoAddTime = Now.AddSeconds(10)
                        Exit Sub
                    End If
                    If Walker_Waypoints(Walker_Waypoints.Count - 1).Coordinates.X <> Core.CharacterLoc.X Or _
                       Walker_Waypoints(Walker_Waypoints.Count - 1).Coordinates.Y <> Core.CharacterLoc.Y Then
                        WalkerChar.Coordinates = BL.GetLocation
                        WalkerChar.Type = Walker.WaypointType.Walk
                        WalkerChar.Info = ""
                        Walker_Waypoints.Add(WalkerChar)
                        AutoAddTime = Now.AddSeconds(10)
                        Core.ConsoleWrite("Walking waypoint added.")
                        Exit Sub
                    End If
                    AutoAddTime = Now.AddSeconds(10)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Amulet/Necklace Changer "
        Private Sub AmuletChangerTimerObj_Execute() Handles AmuletChangerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim Cont As New Container
                Dim Amulet As New ContainerItemDefinition
                Dim NeckSlot As Integer = 0
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Neck - 1) * Consts.ItemDist), NeckSlot, 2)
                If NeckSlot = 0 Then 'No amulet, let's change there something :)
                    If Not Container.FindItem(Amulet, AmuletID, 0, 0, Consts.MaxContainers - 1) Then
                        Exit Sub
                    End If
                    Core.Proxy.SendPacketToServer(PacketUtils.MoveObject(Amulet, GetInventorySlotAsLocation(InventorySlots.Neck), 1))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Ring Changer "
        Private Sub RingChangerTimerObj_Execute() Handles RingChangerTimerObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim Cont As New Container
                Dim Ring As New ContainerItemDefinition
                Dim FingerSlot As Integer = 0
                Core.ReadMemory(Consts.ptrInventoryBegin + ((InventorySlots.Finger - 1) * Consts.ItemDist), FingerSlot, 2)
                If FingerSlot = 0 Then 'No amulet, let's change there something :)
                    If Not Container.FindItem(Ring, RingID, 0, 0, Consts.MaxContainers - 1) Then
                        Exit Sub
                    End If
                    Core.Proxy.SendPacketToServer(PacketUtils.MoveObject(Ring, GetInventorySlotAsLocation(InventorySlots.Finger), 1))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Anti-Logout Timer "

        Public Sub AntiLogoutObj_Execute() Handles AntiLogoutObj.OnExecute
            Try
                If Not InGame() Then Exit Sub
                Dim IdleTime As TimeSpan = Date.Now.Subtract(LastActivity)
                If IdleTime.TotalMinutes <= 14 Then Exit Sub
                Dim BL As New BattleList
                Dim MyLastDirection As Integer
                Dim RandNum As New Random(Date.Now.Millisecond)
                BL.JumpToEntity(SpecialEntity.Myself)
                MyLastDirection = BL.GetDirection
                Select Case BL.GetDirection
                    Case Directions.Up
                        Proxy.SendPacketToServer(CharacterTurn(Directions.Down))
                    Case Directions.Down
                        Proxy.SendPacketToServer(CharacterTurn(Directions.Up))
                    Case Directions.Right
                        Proxy.SendPacketToServer(CharacterTurn(Directions.Left))
                    Case Directions.Left
                        Proxy.SendPacketToServer(CharacterTurn(Directions.Right))
                End Select
                System.Threading.Thread.Sleep(RandNum.Next(2000, 5001))
                Proxy.SendPacketToServer(CharacterTurn(MyLastDirection))
                LastActivity = Date.Now
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " TibiaTek Messages Timer "

        Private Sub TTMessagesTimerObj_OnExecute() Handles TTMessagesTimerObj.OnExecute
            Try
                Dim Client As New WebClient
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Dim Response As String = Client.UploadString(BotWebsite & "/messages.php", "POST", "name=" & Web.HttpUtility.UrlEncode(Proxy.CharacterName) & "&world=" & Web.HttpUtility.UrlEncode(Proxy.CharacterWorld))
                If Not String.IsNullOrEmpty(Response) Then
                    If System.Int32.TryParse(Response, TTMessages) AndAlso TTMessages > 0 Then
                        Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusWarning, "You have received a message from the TibiaTek Development team. Type &viewmsg to read it."))
                    End If
                End If
            Catch
            End Try
        End Sub

#End Region

#End Region

#Region " IRC Client "

#Region " Connection "

        Public Sub ConnectToIrc()
            Try
                If Not InGame() Then Exit Sub
                'IrcGenerateNick()
                If Not String.IsNullOrEmpty(Consts.IRCNickname) Then
                    IRCClient.Nick = Consts.IRCNickname
                End If
                IRCClient.RealName = Proxy.CharacterWorld & " Level " & Level
                IRCClient.User = Environment.MachineName
                IRCClient.Invisible = True
                If Not IRCClient.Connect Then
                    Exit Sub
                End If
                IRCClient.Identify()
                If Not String.IsNullOrEmpty(Consts.IRCPassword) Then
                    IRCClient.Password = Consts.IRCPassword
                    IRCClient.Speak(String.Format("AUTH {0} {1}", IRCClient.Nick, IRCClient.Password), "Q@CServe.quakenet.org")
                End If
                IRCClient.MainLoop()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub IrcGenerateNick()
            Dim Nicks() As String = {"TTBOwner", "TTBFan", "TTBKicker", "TTBKiller", "TTBPwner", "TTBRokr", _
                "TTBKrzr", "TTBRazr", "TTBLord", "TTBUser", "TTBPKer", "TTBLurer", "TTBGamer", "TTBLoco", _
                "TTBNeedy", "TTBBeast", "TTBCrzy", "TTBHunter", "TTBRot", "TTBSuper", "TTBLntc", "TTBTurbo", _
                "TTBKilo", "TTBAlpha", "TTBBeta", "TTBOmega", "TTBPhi", "TTBPsych", "TTBMstr"}
            Dim R As New Random(System.DateTime.Now.Millisecond)
            IRCClient.Nick = Nicks(R.Next(Nicks.Length)) & R.Next(100, 1000).ToString
        End Sub

#End Region

#Region " Methods "

        Public Sub IrcChannelSpeakOperator(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelGM, 3, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub IrcChannelSpeakVoiced(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelTutor, 2, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Sub IrcChannelSpeakNormal(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.Channel, 1, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Public Function IrcChannelIDToName(ByVal ChannelID As Integer) As String
            Try
                For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, ChannelInformation) In IRCClient.Channels
                    If ChannelKVP.Value.ID = ChannelID Then
                        Return ChannelKVP.Key
                    End If
                Next
                Return ""
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function IrcChannelIsOpened(ByVal ChannelID As Integer) As Boolean
            For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, ChannelInformation) In IRCClient.Channels
                If ChannelID = ChannelKVP.Value.ID Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function IrcChannelIsOpened(ByRef ChannelName As String) As Boolean
            For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, ChannelInformation) In IRCClient.Channels
                If String.Equals(ChannelName, ChannelKVP.Key, StringComparison.CurrentCultureIgnoreCase) AndAlso ChannelKVP.Value.ID > 0 Then
                    ChannelName = ChannelKVP.Key
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function IrcChannelNameToID(ByVal ChannelName As String) As Integer
            Try
                For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, ChannelInformation) In IRCClient.Channels
                    If String.Equals(ChannelName, ChannelKVP.Key, StringComparison.CurrentCultureIgnoreCase) Then
                        Return ChannelKVP.Value.ID
                    End If
                Next
                Return 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

#End Region

#Region " IRC Events "
        'Public Event EventPrivateMessage As PrivateMessage
        'Public Event EventChannelError As ChannelError
        'Public Event EventChannelMode As ChannelMode
        'Public Event EventChannelNamesList As ChannelNamesList

        Private Sub IrcClient_ChannelJoin(ByVal Nick As String, ByVal Channel As String) Handles IRCClient.EventChannelJoin
            'ConsoleWrite(Nick & " joined " & Channel & ".")
        End Sub

        Private Sub IrcClient_ChannelKick(ByVal NickKicker As String, ByVal NickKicked As String, ByVal Reason As String, ByVal Channel As String) Handles IRCClient.EventChannelKick
            'ConsoleWrite(NickKicker & " kicked " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".")
            If IrcChannelIsOpened(Channel) Then
                IrcChannelSpeakOperator(Channel, NickKicker & " kicked " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".", IrcChannelNameToID(Channel))
            End If
        End Sub

        Private Sub IrcClient_TopicChange(ByVal ChannelInfo As ChannelInformation) Handles IRCClient.EventChannelTopicChange
            Try
                Thread.Sleep(1000)
                If IrcChannelIsOpened(ChannelInfo.Name) Then
                    IrcChannelSpeakOperator(ChannelInfo.TopicOwner, ChannelInfo.Topic, IrcChannelNameToID(ChannelInfo.Name))
                End If
            Catch Ex As Exception
                'MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'End
            End Try
        End Sub

        Private Sub IrcClient_ChannelSelfPart(ByVal Channel As String) Handles IRCClient.EventChannelSelfPart
            ConsoleWrite("You have left " & Channel & ".")
        End Sub

        Private Sub IrcClient_NickChange(ByVal OldNick As String, ByVal NewNick As String) Handles IRCClient.EventNickChange
            'If IrcChannelIsOpened(Channel) Then
            '    IrcChannelSpeakOperator(Channel, OldNick & " is now known as " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".", IrcChannelNameToID(Channel))
            'End If
        End Sub

        Private Sub IrcClient_Quit(ByVal Nick As String, ByVal Message As String) Handles IRCClient.EventQuit
            'ConsoleWrite(Nick & " quits. Reason: " & Message & ".")
        End Sub

        Private Sub IrcClient_ChannelPart(ByVal Nick As String, ByVal Channel As String) Handles IRCClient.EventChannelPart
            'ConsoleWrite(Nick & " parts " & Channel & ".")

        End Sub

        Private Sub IrcClient_ChannelSelfJoin(ByVal Channel As String) Handles IRCClient.EventChannelSelfJoin
            Try
                ConsoleWrite("Joined channel " & Channel & ".")
                Dim UsedIDs As New List(Of Integer)
                Dim ChannelID As Integer = 1
                If IrcChannelIsOpened(Channel) Then
                    ConsoleWrite("You have already joined that channel.")
                End If
                For Each ChannelInfo As ChannelInformation In IRCClient.Channels.Values
                    If ChannelInfo.ID > 0 Then
                        UsedIDs.Add(ChannelInfo.ID)
                    End If
                Next
                Dim R As New Random(System.DateTime.Now.Millisecond)
                Dim CI As ChannelInformation
                Do
                    ChannelID = R.Next(ChannelType.IRCChannel, ChannelType.IRCChannel + 40) '0..39
                    If Not UsedIDs.Contains(ChannelID) Then
                        For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, ChannelInformation) In IRCClient.Channels
                            If ChannelKVP.Key = Channel Then
                                CI = ChannelKVP.Value
                                CI.ID = ChannelID
                                IRCClient.Channels(Channel) = CI
                                Exit For
                            End If
                        Next
                        'IRCChannelIDs.Add(ChannelID, Channel)
                        OpenIrcChannel(Channel, ChannelID)
                        Exit Do
                    End If
                Loop While True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub IrcClient_Connecting() Handles IRCClient.EventConnecting
            Try
                ConsoleWrite("Connecting to IRC. Please Wait...")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub IrcClient_Connected() Handles IRCClient.EventConnected
            Try
                ConsoleWrite("Successfully connected to IRC. Opening channels, please wait...")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub IrcClient_Disconnected() Handles IRCClient.EventDisconnected
            Try
                ConsoleWrite("Disconnected from IRC.")
                IRCClient.Channels.Clear()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub IrcClient_EndMOTD() Handles IRCClient.EventEndMOTD
            Try
                If Consts.IRCJoinAfterConnected Then
                    IRCClient.Join(IRCChannel)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub IrcClient_ChannelMessage(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Handles IRCClient.EventChannelMessage
            Try
                If IrcChannelIsOpened(Channel) Then
                    If IRCClient.IsOperator(Nick, Channel) Then
                        IrcChannelSpeakOperator(Nick, Message, IrcChannelNameToID(Channel))
                    ElseIf IRCClient.IsVoiced(Nick, Channel) Then
                        IrcChannelSpeakVoiced(Nick, Message, IrcChannelNameToID(Channel))
                    Else
                        IrcChannelSpeakNormal(Nick, Message, IrcChannelNameToID(Channel))
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#End Region

#Region " Proxy Events "

        Public Function InGame() As Boolean
            Try
                If Core.Proxy Is Nothing Then Return False
                If Core.Proxy.Client Is Nothing Then Return False
                Dim IsInGame As Integer = 0
                ReadMemory(Consts.ptrInGame, IsInGame, 4)
                Return CBool(IsInGame = 8)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Function

        Public Function GetProfileDirectory() As String
            Try
                'If Not InGame() Then Throw New Exception("You must be logged in.")
                Dim Path As String = ExecutablePath & "\Profiles\" & Proxy.CharacterWorld & "\" & Proxy.CharacterName
                If Not IO.Directory.Exists(Path) Then
                    IO.Directory.CreateDirectory(Path)
                End If
                Return Path
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
            Return ""
        End Function

        Private Sub Proxy_ConnectionGained() Handles Proxy.ConnectionGained
            Try
                'ChangeClientTitle(BotName & " - " & Proxy.CharacterName)
                ExpCheckerActivated = False
                ExpCheckerTimerObj.StartTimer()
                If StatsTimerObj.State = ThreadTimerState.Stopped Then
                    StatsTimerObj.StartTimer()
                    ChatMessageQueueList.Clear()
                    ChatMessageQueueTimerObj.StartTimer()
                End If
                System.GC.Collect()
                Log("Event", "Connected to game server.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub Proxy_ConnectionLost() Handles Proxy.ConnectionLost
            Try
                'ChangeClientTitle(BotName & " - Not Logged In")
                IsGreetingSent = False
                GreetingSentTry = 0
                StopEverything()
                System.GC.Collect()
                Log("Event", "Disconnected from game server.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub Proxy_ClientHasClosed() Handles Proxy.ClientHasClosed
            Try
                Log("Event", "The Tibia Client has been closed.")
                End
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub Proxy_PacketFromClient(ByRef bytBuffer() As Byte, ByRef Send As Boolean) Handles Proxy.PacketFromClient
            Try
                Dim RegExp As Regex = New Regex("&([^;]+);?")
                Dim MCollection As MatchCollection
                Dim GroupMatch As Match
                Dim Pos As Integer = 2
                Dim Message As String
                Dim BL As New BattleList
                BL.JumpToEntity(SpecialEntity.Myself)
                If Consts.DebugOnLog Then Log("FromClient", BytesToStr(bytBuffer))
                'ConsoleWrite(BytesToStr(bytBuffer))
                Dim ID As UShort = GetByte(bytBuffer, Pos)
                Select Case ID
                    Case &H1E 'ping
                    Case &H64 'Clicked Map or Ground, so Player Moving
                        LastActivity = Date.Now
                        Exit Sub
                    Case &H65, &H66, &H67, &H68, &H6A, &H6B, &H6C, &H6D 'Player Moving
                        LastActivity = Date.Now
                        Exit Sub
                    Case &H88 'go to parent
                        LastActivity = Date.Now
                        Dim ContainerIndex As Integer = GetByte(bytBuffer, Pos)
                        Dim MyContainer As New Container
                        If ContainerIndex = &HF Then
                            MyContainer.JumpToContainer(&HF)
                            Dim ContainerSize As Integer = MyContainer.GetContainerSize
                            If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then
                                LootItems.ShowLootCategories()
                                Send = False
                            End If
                        End If
                    Case &H78 'move object
                        LastActivity = Date.Now
                        Dim Source As LocationDefinition = GetLocation(bytBuffer, Pos)
                        Dim ItemID As UShort = GetWord(bytBuffer, Pos)
                        Dim Slot As Integer = GetByte(bytBuffer, Pos)
                        Dim Destination As LocationDefinition = GetLocation(bytBuffer, Pos)
                        Dim Count As Integer = GetByte(bytBuffer, Pos)
                        Dim MyContainer As New Container
                        If Source.X = &HFFFF AndAlso Source.Y = &H4F Then 'containers only
                            MyContainer.JumpToContainer(&HF) 'go to that container
                            Dim ContainerSize As Integer = MyContainer.GetContainerSize
                            If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then 'is fake
                                Send = False
                                If LooterCurrentCategory = 0 Then Exit Sub
                                'thrown to map, or thrown to inventory, or thrown to another bp
                                If (Destination.X < &HFFFF) _
                                    OrElse (Destination.X = &HFFFF AndAlso Destination.Y < &H40) _
                                    OrElse (Destination.X = Source.X AndAlso Destination.Y <> Source.Y) Then
                                    LootItems.Remove(ItemID)
                                    Proxy.SendPacketToClient(RemoveObjectFromContainer(Slot, Source.Y - &H40))
                                    ConsoleWrite(Definitions.GetItemName(ItemID) & " (H" & Hex(ItemID) & ") removed from " & MyContainer.GetName & ".")
                                Else
                                    Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, "Sorry, not possible."))
                                End If
                            End If
                        ElseIf (Source.X = &HFFFF AndAlso Source.Y < &H40) _
                            OrElse Source.X < &HFFFF _
                            OrElse (Source.X = &HFFFF AndAlso Source.Y <> Destination.Y) Then
                            If Destination.X = &HFFFF AndAlso Destination.Y = &H4F Then
                                MyContainer.JumpToContainer(&HF) 'go to that container
                                Dim ContainerSize As Integer = MyContainer.GetContainerSize
                                If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then 'is fake
                                    Send = False
                                    If LooterCurrentCategory = 0 Or ItemID <= 100 Then Exit Sub
                                    If LootItems.Add(New LootItems.LootItemDefinition(ItemID, LooterCurrentCategory - 1)) Then
                                        If DatInfo.GetInfo(ItemID).IsStackable Then
                                            Count = 100
                                        ElseIf DatInfo.GetInfo(ItemID).IsFluidContainer Then
                                            '   keep count
                                        ElseIf DatInfo.GetInfo(ItemID).MultiType Then
                                            Count = 1
                                        Else
                                            Count = 0
                                        End If
                                        ConsoleWrite(Definitions.GetItemName(ItemID) & " (H" & Hex(ItemID) & ") addded to " & MyContainer.GetName & ".")
                                        Proxy.SendPacketToClient(AddObjectToContainer(ItemID, &HF, Count))
                                    Else
                                        ConsoleError("This item already exists.")
                                    End If
                                End If
                            End If
                        End If
                    Case &H82 'use item
                        LastActivity = Date.Now
                        Dim Location As LocationDefinition = GetLocation(bytBuffer, Pos)
                        Dim ItemID As Integer = GetWord(bytBuffer, Pos)
                        Dim Slot As Integer = GetByte(bytBuffer, Pos)
                        Dim ContainerIndex As Integer = GetByte(bytBuffer, Pos)
                        'Core.ConsoleWrite(BytesToStr(bytBuffer))
                        If DatInfo.GetInfo(ItemID).IsContainer Then BagOpened = False
                        If Location.Y = &H4F Then
                            Dim MyContainer As New Container
                            MyContainer.JumpToContainer(&HF)
                            Dim ContainerSize As Integer = MyContainer.GetContainerSize
                            If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then
                                If String.Compare(MyContainer.GetName, "Loot Categories") = 0 Then 'using a category :O
                                    LooterCurrentCategory = Slot + 1
                                    Proxy.SendPacketToClient(CreateContainer(ItemID, &HF, "Loot Category #" & (Slot + 1), &H24, LootItems.GetItemsIDs(Slot), True))
                                Else
                                    Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Item Information: " & Definitions.GetItemName(ItemID) & " (H" & Hex(ItemID) & ")."))
                                End If
                                Send = False
                                Exit Sub
                            End If
                        End If
                        If Consts.HotkeysCanEquipItems AndAlso (Location.X = &HFFFF AndAlso Location.Y = 0 AndAlso Location.Z = 0) Then 'hotkey
                            If Definitions.IsRing(ItemID) Then
                                Dim ItemDef As ContainerItemDefinition
                                If Container.FindItem(ItemDef, ItemID, 0, 0, Consts.MaxContainers - 1) Then
                                    Proxy.SendPacketToServer(MoveObject(ItemDef, GetInventorySlotAsLocation(InventorySlots.Finger)))
                                Else
                                    ConsoleError("Could not find " & Definitions.GetItemName(ItemID) & ", make sure it is on an open container.")
                                End If
                                Send = False
                            End If
                            If Definitions.IsNeck(ItemID) Then
                                Dim ItemDef As ContainerItemDefinition
                                If Container.FindItem(ItemDef, ItemID, 0, 0, Consts.MaxContainers - 1) Then
                                    Proxy.SendPacketToServer(MoveObject(ItemDef, GetInventorySlotAsLocation(InventorySlots.Neck)))
                                    AmuletID = ItemID
                                Else
                                    ConsoleError("Could not find " & Definitions.GetItemName(ItemID) & ", make sure it is on an open container.")
                                End If
                                Send = False
                            End If
                            If Definitions.IsAmmunition(ItemID) Then
                                Dim Ammodef As ContainerItemDefinition
                                Dim Cont As New Container
                                If Container.FindItem(Ammodef, ItemID, 0, 0, Consts.MaxContainers - 1) Then
                                    AmmoRestackerItemID = ItemID
                                    Proxy.SendPacketToServer(MoveObject(Ammodef, GetInventorySlotAsLocation(InventorySlots.Belt), Cont.GetItemCount))
                                Else
                                    ConsoleError("Could not find " & Definitions.GetItemName(ItemID) & ", make sure it is on an open container.")
                                End If
                                Send = False
                            End If
                        ElseIf Consts.EquipItemsOnUse Then
                            If Definitions.IsNeck(ItemID) Then
                                Proxy.SendPacketToServer(MoveObject(ItemID, Location, GetInventorySlotAsLocation(InventorySlots.Neck), 1))
                                Send = False
                            End If
                            If Definitions.IsRing(ItemID) Then
                                Proxy.SendPacketToServer(MoveObject(ItemID, Location, GetInventorySlotAsLocation(InventorySlots.Finger), 1))
                                Send = False
                            End If
                            If Definitions.IsAmmunition(ItemID) Then
                                Proxy.SendPacketToServer(MoveObject(ItemID, Location, GetInventorySlotAsLocation(InventorySlots.Belt), 100))
                                Send = False
                            End If
                        End If
                        If LearningMode Then
                            If Definitions.GetItemKind(ItemID) = ItemKind.UsableTeleport Then
                                Dim WalkerChar As New Walker
                                WalkerChar.Coordinates = Location
                                WalkerChar.Type = Walker.WaypointType.Ladder
                                WalkerChar.Info = ""
                                Walker_Waypoints.Add(WalkerChar)
                                Core.ConsoleWrite("Ladder waypoint added.")
                            End If
                        End If
                    Case &H83 'Use Item With
                        LastActivity = Date.Now
                        Pos += 2
                        Dim Cont As New ContainerItemDefinition
                        Cont.ContainerIndex = GetByte(bytBuffer, Pos)
                        Cont.Slot = GetWord(bytBuffer, Pos)
                        Cont.ID = GetWord(bytBuffer, Pos)
                        Pos += 1
                        Dim Location As LocationDefinition = GetLocation(bytBuffer, Pos)
                        Dim TileId As Integer = GetWord(bytBuffer, Pos)
                        If LearningMode Then
                            Dim WalkerChar As New Walker
                            Select Case Definitions.GetItemName(Cont.ID)
                                Case "Rope"
                                    WalkerChar.Type = Walker.WaypointType.Rope
                                    WalkerChar.Info = ""
                                    Core.ConsoleWrite("Rope waypoint added.")
                                Case "Shovel", "Light Shovel"
                                    WalkerChar.Type = Walker.WaypointType.Shovel
                                    WalkerChar.Info = BL.GetDirection 'HAVE TO BE TESTED!
                                    Core.ConsoleWrite("Shovel waypoint added.")
                                Case Else
                                    Exit Sub
                            End Select
                            WalkerChar.Coordinates = Location
                            Walker_Waypoints.Add(WalkerChar)
                        End If
                    Case &H84 'Use hotkey
                        LastActivity = Date.Now
                        Pos += 13
                    Case &H8A
                        Dim SpellID As Integer = GetByte(bytBuffer, Pos)
                        GetDWord(bytBuffer, Pos)
                        Select Case SpellID
                            Case &HFF
                                Try
                                    Dim Data As String = GetString(bytBuffer, Pos)
                                    Dim Writer As StreamWriter = IO.File.CreateText(GetProfileDirectory() & "\config.txt")
                                    Writer.Write(Data)
                                    Writer.Close()
                                Catch
                                End Try
                                ConsoleWrite("Your configuration has been saved.")
                                Send = False
                            Case &HFE
                                Dim Data As String = GetString(bytBuffer, Pos)
                                Send = False
                                If Data.Trim(Chr(&HA)).Length = 0 Then
                                    ConsoleError("Unable to send empty feedback message.")
                                    Exit Sub
                                End If

                                Data &= vbLf & "-" & Proxy.CharacterName & " (" & Proxy.CharacterWorld & ")"
                                If Data.Length > 0 Then
                                    Try
                                        Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("feedback=" & System.Web.HttpUtility.UrlEncode(Data))
                                        Dim Client As New WebClient
                                        Client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                                        Dim URI As New System.Uri("http://www.tibiatek.com/feedback.php")
                                        Client.UploadDataAsync(URI, "POST", Content)
                                        ConsoleWrite("Thank you for your feedback, it is greatly appreciated.")
                                    Catch
                                        ConsoleError("Sorry, the feedback was not sent properly.")
                                    End Try
                                End If

                        End Select
                    Case &H96 'message
                        LastActivity = Date.Now
                        Dim MessageType As MessageType = GetByte(bytBuffer, Pos)
                        If MessageType = MessageType.Channel AndAlso bytBuffer(4) = ChannelType.Console Then
                            Message = GetString(bytBuffer, 6)
                            If Message.StartsWith("&") Then
                                'ConsoleRead(Message)
                                ConsoleRead(Message)
                                MCollection = RegExp.Matches(Message)
                                For Each GroupMatch In MCollection
                                    CommandParser(GroupMatch.Groups(1).ToString)
                                Next
                            End If
                            Send = False

                        ElseIf MessageType = MessageType.Channel AndAlso (bytBuffer(4) >= ChannelType.IRCChannel AndAlso bytBuffer(4) < ChannelType.IRCChannel + 40) Then
                            Send = False
                            Dim ChannelID As Int16 = bytBuffer(4)
                            Message = GetString(bytBuffer, 6)
                            Dim Channel As String = IrcChannelIDToName(ChannelID)
                            If IRCClient.Channels.ContainsKey(Channel) Then
                                If Message.StartsWith("&") Then
                                    IrcChannelSpeakNormal(Channel, "You cannot send TibiaTek Bot commands on an IRC Channel", ChannelID)
                                ElseIf Message.StartsWith("/") Then
                                    Dim Match As Match = Regex.Match(Message.TrimEnd(" "c), "/(join|nick|users)(?:\s(.+))?", RegexOptions.IgnoreCase)
                                    If Match.Success Then
                                        Select Case Match.Groups(1).Value.ToLower
                                            Case "join"
                                                IRCClient.Join(Match.Groups(2).Value)
                                            Case "nick"
                                                IRCClient.Nick = Match.Groups(2).Value
                                                IRCClient.ChangeNick(IRCClient.Nick)
                                            Case "users"
                                                If Core.IRCClient.Channels.ContainsKey(Channel) Then
                                                    Dim TempNick As String = ""
                                                    For Each Nick As String In Core.IRCClient.Channels(Channel).Users.Keys
                                                        TempNick = IIf(Core.IRCClient.IsOperator(Nick, Channel), "@", IIf(Core.IRCClient.IsVoiced(Nick, Channel), "+", String.Empty))
                                                        TempNick &= Nick
                                                        Core.IrcChannelSpeakNormal(Channel, TempNick, Core.IrcChannelNameToID(Channel))
                                                    Next
                                                End If
                                        End Select

                                    End If
                                Else
                                    If IRCClient.IsOperator(IRCClient.Nick, Channel) Then
                                        IrcChannelSpeakOperator(IRCClient.Nick, Message, ChannelID)
                                    ElseIf IRCClient.IsVoiced(IRCClient.Nick, Channel) Then
                                        IrcChannelSpeakVoiced(IRCClient.Nick, Message, ChannelID)
                                    Else
                                        IrcChannelSpeakNormal(IRCClient.Nick, Message, ChannelID)
                                    End If
                                    IRCClient.Speak(Message, Channel)
                                End If
                            Else
                                ConsoleError("Unable to send message to the IRC Channel.")
                            End If
                        Else
                            Dim ChatMessage As New ChatMessageDefinition
                            ChatMessage.MessageType = MessageType
                            ChatMessage.Prioritize = True
                            Dim bytNewBuffer(1) As Byte
                            AddByte(bytNewBuffer, &H96)
                            Select Case MessageType
                                Case MessageType.PM
                                    ChatMessage.Destinatary = GetString(bytBuffer, Pos)
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            CommandParser(GroupMatch.Groups(1).Value)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    End If
                                    bytNewBuffer = Speak(ChatMessage.Destinatary, ChatMessage.Message)
                                Case MessageType.Channel
                                    ChatMessage.Channel = GetWord(bytBuffer, Pos)
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            CommandParser(GroupMatch.Groups(1).ToString)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    End If
                                    bytNewBuffer = Speak(ChatMessage.Message, ChatMessage.Channel)
                                Case Else
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            CommandParser(GroupMatch.Groups(1).ToString)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    End If
                                    bytNewBuffer = Speak(ChatMessage.Message, MessageType)
                            End Select
                            Dim TimeElapsed As TimeSpan = Date.Now.Subtract(ChatMessageLastSent)
                            If ChatMessageLastSent = Date.MinValue OrElse TimeElapsed.TotalSeconds >= 3 Then
                                Proxy.SendPacketToServer(bytNewBuffer)
                            Else
                                ChatMessageQueueList.Add(ChatMessage)
                            End If
                            Send = False
                        End If
                    Case &H98 ' Requesting console through Channel List
                        LastActivity = Date.Now
                        If bytBuffer(3) = ConsoleChannelID Then
                            Send = False
                            OpenChannel()
                        End If
                    Case &H99 ' Closing channel
                        LastActivity = Date.Now
                        If bytBuffer(3) > ChannelType.IRCChannel AndAlso bytBuffer(3) <= ChannelType.IRCChannel + 40 Then
                            Dim ChannelID As Int16 = bytBuffer(3)
                            Send = False
                            If IrcChannelIsOpened(ChannelID) Then
                                IRCClient.Part(IrcChannelIDToName(ChannelID))
                                IRCClient.Channels.Remove(IrcChannelIDToName(ChannelID))
                            End If
                        End If
                    Case &H9A ' Requesting console given a string
                        LastActivity = Date.Now
                        Dim ChannelName As String = GetString(bytBuffer, 3)
                        If String.Compare(ChannelName, "console", True) = 0 Or String.Compare(ChannelName, ConsoleName, True) = 0 Then
                            Send = False
                            OpenChannel()
                        ElseIf ChannelName.StartsWith("#") AndAlso ChannelName.Length > 1 Then
                            Send = False
                            IRCClient.Join(ChannelName)
                            ConsoleWrite("Opening IRC Channel " & ChannelName & ".")
                        End If
                    Case &H64, &H65, &H66, &H67, &H68, &H6A, &H6B, &H6C, &H6D, &H6F, &H70, &H71, &H72
                        LastActivity = Date.Now
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub Proxy_PacketFromServer(ByRef bytBuffer() As Byte, ByRef Block As Boolean) Handles Proxy.PacketFromServer
            Try
                If Not IsGreetingSent Then
                    GreetingSentTry += 1
                    If GreetingSentTry >= 2 Then
                        IsGreetingSent = True
                        OpenChannel()
                        GreetingTimerObj.StartTimer(1500)
                        Map.RefreshMapBeginning()
                        MapReaderTimerObj.StartTimer()
                        If Consts.TTMessagesEnabled Then
                            TTMessagesTimerObj.StartTimer(2000)
                        End If
                        If Consts.AutoOpenBackpack Then
                            Dim ItemID As Integer = 0
                            Core.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (InventorySlots.Backpack - 1)), ItemID, 2)
                            If DatInfo.GetInfo(ItemID).IsContainer Then
                                Proxy.SendPacketToServer(UseObject(InventorySlots.Backpack, 0))
                            End If
                        End If
                    End If
                Else
                    If LastUpdateCheck <> Date.MinValue Then
                        Dim ElapsedTime As TimeSpan = Date.Now.Subtract(LastUpdateCheck)
                        If ElapsedTime.TotalHours >= 3 Then
                            LastUpdateCheck = Date.Now
                            BGWUpdateChecker.RunWorkerAsync()
                        End If
                    Else
                        LastUpdateCheck = Date.Now
                        BGWUpdateChecker.RunWorkerAsync()
                    End If
                    If Consts.DebugOnLog Then Log("FromServer", BytesToStr(bytBuffer))
                End If
                Dim Pos As Integer = 0
                Dim Loc As LocationDefinition
                Dim ID As Integer = 0
                Dim PacketLength As UShort = GetWord(bytBuffer, Pos) + 2
                Dim PacketID As Integer = 0
                Dim Word As UShort = 0
                Dim OneByte As Byte = 0
                'Trace.WriteLine("FromServer: " & BytesToStr(bytBuffer))
                While Pos < PacketLength
                    PacketID = GetByte(bytBuffer, Pos)
                    Select Case PacketID
                        Case &H15 'fyi box
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word
                        Case &H28 'death message ='(
                        Case &HB4 'sys message ^_^u
                            Pos += 1
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word
                        Case &H6A 'add object to map
                            Loc = GetLocation(bytBuffer, Pos)
                            ID = GetWord(bytBuffer, Pos)
                            If ID = &H62 Then 'known creature, skip this
                                Pos += 6
                                Word = GetWord(bytBuffer, Pos)
                                If Word = 0 Then 'invisible!
                                    Pos += 2
                                Else
                                    Pos += 5 'skip outfit stuff
                                End If
                                Pos += 6
                            ElseIf ID = &H61 Then 'unknown creatre, skip that shit
                                Pos += 8 'word + word
                                Word = GetWord(bytBuffer, Pos) 'skip creature name
                                Pos += Word + 2 'Integer + Integer
                                Word = GetWord(bytBuffer, Pos) 'outfit? or invis?
                                Pos += 2
                                If Word > 0 Then Pos += 3 'skip outfit stuff
                                Pos += 6
                            ElseIf ID = &H63 Then
                                Pos += 5
                            ElseIf DatInfo.GetInfo(ID).IsFluidContainer Then
                                Pos += 1
                            ElseIf DatInfo.GetInfo(ID).IsContainer Then
                                If LooterTimerObj.State = ThreadTimerState.Running Then
                                    If Not ((Definitions.GetItemKind(ID) And ItemKind.Container) = ItemKind.Container) Then 'if its known container, skip
                                        Dim BL As New BattleList
                                        BL.JumpToEntity(SpecialEntity.Myself)
                                        If BL.GetDistanceFromLocation(Loc) <= Consts.LootMaxDistance Then
                                            If CaveBotTimerObj.State = ThreadTimerState.Running Then
                                                If Not CavebotForm.LootFromCorpses.Checked AndAlso Not CavebotForm.EatFromCorpses.Checked Then
                                                    Continue While
                                                End If
                                                'WriteMemory(Consts.ptrGoToX, 0, 1)
                                                'WriteMemory(Consts.ptrGoToY, 0, 1)
                                                'WriteMemory(Consts.ptrGoToZ, 0, 1)
                                                BL.IsWalking = False
                                                CBContainerCount = Container.ContainerCount
                                                IsOpeningReady = False
                                                WaitTime = Date.Now.AddSeconds(5)
                                                CBCreatureDied = True
                                                CBState = CavebotState.OpeningBody
                                                'Core.ConsoleWrite("Looter Part of Proxy: STOP")
                                                StopPlayer()
                                            End If
                                            BagOpened = False
                                            LooterItemID = ID
                                            LooterLoc = Loc
                                            'If Not BGWLooter.IsBusy Then BGWLooter.RunWorkerAsync()
                                            LootMonster()
                                        End If
                                    End If
                                End If
                            ElseIf DatInfo.GetInfo(ID).HasExtraByte Then
                                Pos += 1
                            End If
                        Case &H6B 'update tile object
                            Pos += 6 'position, stackpos
                            ID = GetWord(bytBuffer, Pos)
                            If ID = &H62 Then 'known creature, skip this
                                Pos += 6
                                Word = GetWord(bytBuffer, Pos)
                                If Word = 0 Then 'invisible!
                                    Pos += 2
                                Else
                                    Pos += 5 'skip outfit stuff
                                End If
                                Pos += 6
                            ElseIf ID = &H61 Then 'unknown creatre, skip that shit
                                Pos += 8 'word + word
                                Word = GetWord(bytBuffer, Pos) 'skip creature name
                                Pos += Word + 2 'Integer + Integer
                                Word = GetWord(bytBuffer, Pos) 'outfit? or invis?
                                Pos += 2
                                If Word > 0 Then Pos += 3 'skip outfit stuff
                                Pos += 6
                            ElseIf ID = &H63 Then
                                Pos += 5
                            ElseIf DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                Pos += 1
                            End If
                        Case &H6C 'remove item from map
                            'Trace.WriteLine("FromServer: " & BytesToStr(bytBuffer))
                            Pos += 6 'loc + Integer
                        Case &H6D 'move creature
                            Pos += 11 'loc + Integer + loc
                        Case &H6E 'get container
                            Pos += 3 'container index, itemid
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word + 2 'name,size,hasparent
                            OneByte = GetByte(bytBuffer, Pos)
                            For I As Integer = 1 To OneByte
                                ID = GetWord(bytBuffer, Pos)
                                If DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                    Pos += 1
                                End If
                            Next
                        Case &H6F 'close container
                            Pos += 1 'containerindex
                        Case &H70 'add item to container
                            Pos += 1
                            ID = GetWord(bytBuffer, Pos)
                            If DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                Pos += 1
                            End If
                        Case &H71 'update container item
                            Pos += 2
                            ID = GetWord(bytBuffer, Pos)
                            If DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                Pos += 1
                            End If
                        Case &H72 'remove container item
                            Pos += 2
                        Case &H78
                            Pos += 1 'slot
                            ID = GetWord(bytBuffer, Pos)
                            If DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                Pos += 1
                            End If
                        Case &H79 'remove inventory item
                            Pos += 1
                        Case &H7D, &H7E 'trade item request
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word  'name
                            OneByte = GetByte(bytBuffer, Pos)
                            For I As Integer = 1 To OneByte
                                ID = GetWord(bytBuffer, Pos)
                                If DatInfo.GetInfo(ID).HasExtraByte Or Definitions.IsRune(ID) Then
                                    Pos += 1
                                End If
                            Next
                        Case &H7F 'close trade
                        Case &H82 'world light
                            Pos += 2 'intensity,color
                        Case &H83 'magic effect
                            Pos += 6
                        Case &H84 'animated text
                            Pos += 6
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word
                        Case &H85 'projectile?
                            'Core.ConsoleWrite(BytesToStr(bytBuffer))
                            Dim FromBL As New BattleList
                            Dim ToBl As New BattleList
                            Dim Type As Integer = 0

                            Dim FromFound As Boolean = FromBL.Find(GetLocation(bytBuffer, Pos), True)
                            Dim ToFound As Boolean = ToBl.Find(GetLocation(bytBuffer, Pos), True)
                            Type = GetByte(bytBuffer, Pos)
                            If Not (FromFound And ToFound) Then Continue While
                            'ConsoleWrite("Projectile type: " & Type.ToString & " (" & FromBL.GetName & "->" & ToBl.GetName & ") ")
                            If ComboBotEnabled Then
                                If Type = 11 Then 'SD rune
                                    If ComboBotLeader.ToLower = FromBL.GetName.ToLower Then
                                        Proxy.SendPacketToServer(PacketUtils.UseObjectOnPlayerAsHotkey(Definitions.GetItemID("Sudden Death"), ToBl.GetEntityID))
                                    End If
                                End If
                            End If
                        Case &H86 'direct hit, black square
                            Dim AttackedID As Integer = 0
                            Dim EntityID As Integer = 0
                            EntityID = GetDWord(bytBuffer, Pos)
                            If (AutoAttackerActivated) Then
                                If BattleList.IsPlayer(EntityID) OrElse EntityID = AutoAttackerIgnoredID Then Exit Sub
                                ReadMemory(Consts.ptrAttackedEntityID, AttackedID, 4)
                                If AttackedID = 0 Then
                                    WriteMemory(Consts.ptrFollowedEntityID, AutoAttackerIgnoredID, 4)
                                    WriteMemory(Consts.ptrAttackedEntityID, EntityID, 4)
                                    Proxy.SendPacketToServer(AttackEntity(EntityID))
                                End If
                            End If
                            Pos += 1
                        Case &H8C 'creature health
                            ID = GetDWord(bytBuffer, Pos)
                            OneByte = GetByte(bytBuffer, Pos)
                            If OneByte > 0 Then Continue While
                            CBCreatureDied = True
                            If ShowCreaturesUntilNextLevel Then
                                Dim LastAttackedID As Integer = 0
                                ReadMemory(Consts.ptrLastAttackedEntityID, LastAttackedID, 4)
                                If ID = LastAttackedID Then
                                    Dim BL As New BattleList
                                    Dim Name As String = 0
                                    If Not BL.Find(ID) Then Continue While
                                    Name = BL.GetName()
                                    If Creatures.Creatures.ContainsKey(Name) Then
                                        Dim N As Integer = (NextLevelExp - Experience) / Creatures.Creatures(Name).Experience
                                        Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, "You need to kill " & N & " " & Name & " to level up."))
                                    End If
                                End If
                            End If
                        Case &H8D 'creature light
                            Pos += 6 'id, light intensity, light color
                        Case &H8E 'add creature, or invisible creature
                            Pos += 4
                            Word = GetWord(bytBuffer, Pos)
                            If Word <> 0 Then
                                Pos += 5
                            Else
                                Pos += 2
                            End If
                        Case &H90 'creature got skull change
                            Pos += 5 'id, skull
                        Case &HA0 'stats
                            Pos += 22 'constant
                        Case &HA1 'player skills
                            Pos += 14
                            'skill level+ skill percent
                            'fist,club,sword,axe,dist,shield,fish
                        Case &HA2 'icons
                            Dim Condition As Conditions = CType(GetWord(bytBuffer, Pos), Conditions)
                            If Not MagicShieldActivated AndAlso CBool((Condition And Conditions.MagicShield) = Conditions.MagicShield) Then 'got magic shield plx
                                MagicShieldActivated = True
                            ElseIf MagicShieldActivated AndAlso Not CBool((Condition And Conditions.MagicShield)) Then
                                MagicShieldActivated = False
                                Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Your Magic Shield is now over."))
                            End If
                        Case &HAA 'received message
                            GetDWord(bytBuffer, Pos)
                            Dim Name As String = ""
                            Dim Level As Integer = 0
                            Dim Message As String = ""
                            Name = GetString(bytBuffer, Pos)
                            Level = GetWord(bytBuffer, Pos)
                            Dim MessageType As MessageType = GetByte(bytBuffer, Pos)
                            Select Case MessageType
                                'cant add monstersay or monsteryell here... or we will have the message alarm alerting when there is no reason to do so
                                Case MessageType.Normal, MessageType.Whisper, MessageType.Yell ', ConstantsModule.MessageType.MonsterSay, ConstantsModule.MessageType.MonsterYell 
                                    Loc = GetLocation(bytBuffer, Pos)
                                    Message = GetString(bytBuffer, Pos)
                                    MessageAlarm(MessageType, Name, Level, Loc, Message)
                                Case ConstantsModule.MessageType.MonsterSay, ConstantsModule.MessageType.MonsterYell
                                    Loc = GetLocation(bytBuffer, Pos)
                                    Message = GetString(bytBuffer, Pos)
                                Case MessageType.Channel, MessageType.ChannelGM, MessageType.ChannelTutor, MessageType.ChannelCounsellor
                                    Dim Channel As String = ""
                                    Dim ChanType As ChannelType = CType(GetWord(bytBuffer, Pos), ChannelType)
                                    Select Case ChanType
                                        Case ChannelType.Console
                                            Channel = "Console"
                                        Case ChannelType.GameChat
                                            Channel = "Game Chat"
                                        Case ChannelType.GuildChat
                                            Channel = "Guild Chat"
                                        Case ChannelType.Help
                                            Channel = "Help"
                                        Case ChannelType.Personal
                                            Channel = "Personal"
                                        Case ChannelType.RLChat
                                            Channel = "RL Chat"
                                        Case ChannelType.Trade
                                            Channel = "Trade"
                                        Case Else
                                            Channel = "Unknown"
                                    End Select

                                    Message = GetString(bytBuffer, Pos)
                                    If TradeWatcherActive AndAlso ChanType = ChannelType.Trade AndAlso Not Name.Equals(Proxy.CharacterName) Then
                                        If Regex.IsMatch(Message, TradeWatcherRegex, RegexOptions.IgnoreCase) Then
                                            Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Offer: " & Name & "[" & Level & "]: " & Message))
                                        End If
                                    End If
                                    Log("Message", Name & "[" & Level & "] said in " & Channel & ": " & Message)
                                Case MessageType.PM, MessageType.PMGM 'private message
                                    Message = GetString(bytBuffer, Pos)
                                    MessageAlarm(MessageType, Name, Level, Loc, Message)
                            End Select
                        Case &HAB 'channel dialog
                            OneByte = GetByte(bytBuffer, Pos)
                            For I As Byte = 1 To OneByte
                                Pos += 2 'channel id
                                Word = GetWord(bytBuffer, Pos)
                                Pos += Word 'channel name
                            Next
                        Case &HAC 'channel
                            Pos += 2 'channel id o.o
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word 'channel name
                        Case &HAD 'open private
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word
                        Case &HB3 'close private
                            Pos += 2
                        Case &HD2 'get new vip?
                            Pos += 4 'id
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word + 1 'name, isonline
                        Case &HD3 'vip login
                            Pos += 4
                        Case &HD4 'viplogout
                            Pos += 4
                        Case Else
#If TRACE Then
                            'Trace.WriteLine("FromServer: " & Hex(PacketID) & " @ Pos " & (Pos - 1) & vbCrLf & "->" & BytesToStr(bytBuffer, Pos - 1))
#End If
                            Exit Sub
                    End Select

                End While
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub MessageAlarm(ByVal MessageType As MessageType, ByVal Name As String, ByVal Level As Integer, ByVal Loc As LocationDefinition, ByVal Message As String)
            Try
                Dim Alert As Boolean = True
                Dim Player As String
                Dim Output As String = ""
                If String.Compare(Name, Proxy.CharacterName) = 0 Then Exit Sub
                If TibiaWindowState <> WindowState.Active AndAlso Consts.FlashTaskbarWhenMessaged Then
                    Dim FWI As New Win32API.FlashWInfo(Proxy.Client.MainWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                    Win32API.FlashWindowEx(FWI)
                End If
                Select Case MessageType
                    Case MessageType.Normal, MessageType.Whisper, MessageType.Yell
                        'ConsoleWrite("(" & Loc.X & "," & Loc.Y & "," & Loc.Z & ") y (" & CharacterLoc.X & "," & CharacterLoc.Y & "," & CharacterLoc.Z & ")")
                        Output = Name & "[" & Level & "] said in public: " & Message
                        Log("Message", Output)
                        If Not AlarmsForm.MessagePublic.Checked Then Exit Sub
                    Case MessageType.PM
                        Output = Name & "[" & Level & "] said privately: " & Message
                        Log("Message", Output)
                        If Not AlarmsForm.MessagePrivate.Checked Then Exit Sub
                    Case Else
                        Exit Sub
                End Select

                For Each Player In AlarmsForm.MessageIgnoredPlayers.Items
                    If Regex.IsMatch(Name, "^" & Player & "$", RegexOptions.IgnoreCase) Then
                        Alert = False
                        Exit For
                    End If
                Next
                If Not Alert OrElse Not AlarmsActivated Then Exit Sub
                If TibiaWindowState <> WindowState.Active AndAlso Consts.FlashTaskbarWhenAlarmFires Then
                    Dim FWI As New Win32API.FlashWInfo(Proxy.Client.MainWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                    Win32API.FlashWindowEx(FWI)
                End If
                If Consts.MusicalNotesOnAlarm Then Proxy.SendPacketToClient(MagicEffect(CharacterLoc, MagicEffects.MusicalNotesWhite))
                Dim ChatMessage As New ChatMessageDefinition
                If AlarmsForm.MessagePlaySound.Checked Then
                    Dim Sound As New Audio
                    Try
                        Select Case MessageType
                            Case MessageType.Normal, MessageType.Whisper, MessageType.Yell
                                If AlarmsForm.MessagePublic.Checked Then Sound.Play(ExecutablePath & "\Alarms\Public Message.wav", AudioPlayMode.Background)
                            Case MessageType.PM
                                If AlarmsForm.MessagePrivate.Checked Then Sound.Play(ExecutablePath & "\Alarms\Private Message.wav", AudioPlayMode.Background)
                        End Select
                    Catch
                    End Try
                End If
                If AlarmsForm.MessageLogOut.Checked Then
                    Core.Proxy.SendPacketToServer(PacketUtils.PlayerLogout)
                    Log("Message Alarm", "Logging out.")
                End If
                If AlarmsForm.MessageForwardMessage.Checked AndAlso AlarmsForm.MessageForwardMessageInput.Text.Length > 0 Then
                    ChatMessage.Message = Output
                    ChatMessage.MessageType = MessageType.PM
                    ChatMessage.Destinatary = AlarmsForm.MessageForwardMessageInput.Text
                    ChatMessage.Prioritize = True
                    ChatMessageQueueList.Add(ChatMessage)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Console R/W "

        Public Sub ConsoleRead(ByVal strString As String)
            Try
                Log("ConsoleRead", strString)
                Proxy.SendPacketToClient(CreatureSpeak(Core.Proxy.CharacterName, MessageType.ChannelTutor, Level, strString, 0, 0, 0, ChannelType.Console))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub ConsoleWrite(ByVal strString As String)
            Try
                Log("ConsoleWrite", strString)
                Proxy.SendPacketToClient(CreatureSpeak(ConsoleName, MessageType.Channel, ConsoleLevel, strString, 0, 0, 0, ChannelType.Console))
                Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, strString))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub ConsoleError(ByVal strString As String)
            Try
                Log("ConsoleError", strString)
                Proxy.SendPacketToClient(CreatureSpeak(ConsoleName, MessageType.ChannelGM, ConsoleLevel, strString, 0, 0, 0, ChannelType.Console))
                Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, strString))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

    End Class

End Module
