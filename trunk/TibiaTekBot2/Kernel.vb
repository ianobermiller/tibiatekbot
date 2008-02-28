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
  System.Net, System.Net.Sockets, System.Text, System.Globalization, _
  System.IO, System.Xml, Microsoft.VisualBasic.Devices, TibiaTekBot.Constants, _
  System.Drawing.Imaging, TibiaTekBot.PProxy2, System.Runtime.InteropServices, _
  TibiaTekBot.IrcClient, System.Windows.Forms, Scripting, System.ComponentModel, _
  System.CodeDom.Compiler

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

Public Module KernelModule

#Region " Structures "
    Public Structure WeaponFavoritDefinition
        Dim WeaponID As Integer
        Dim Monster As String
        Dim Hand As Short
    End Structure

    Public Structure MagicWallDefinition
        Dim Enabled As Boolean
        Dim LastMagicWallDate As Date
        Dim Stage As UShort
        Dim Position As ITibia.LocationDefinition
    End Structure

    Public Structure ChatMessageDefinition
        Dim Prioritize As Boolean
        Dim MessageType As ITibia.MessageType
        Dim PrivateMessageType As ITibia.PrivateMessageType
        Dim ChannelMessageType As ITibia.ChannelMessageType
        Dim DefaultMessageType As ITibia.DefaultMessageType
        Dim Channel As ITibia.Channel
        Dim Destinatary As String
        Dim Message As String
    End Structure

    Public Structure ScriptDefinition
        Dim Script As IScript
        Dim Filename As String
        Dim SafeFileName As String
        Dim State As IScript.ScriptState
        Dim CompilerResults As CompilerResults
    End Structure

#End Region
    Public Kernel As New KernelClass

    Public Class KernelClass
        Implements IKernel

#Region " Objects "
        Public WithEvents Client As Tibia
        Public WithEvents Proxy As PProxy2
        Public Spells As Spells
        Public Outfits As Outfits
        Public LootItems As LootItems
        Public Creatures As Creatures
        'Dim MyLua As New LuaInterface.Lua
        Public WithEvents BGWOpenCommand As BackgroundWorker
        Public WithEvents BGWCharCommand As BackgroundWorker
        Public WithEvents BGWGuildMembersCommand As BackgroundWorker
        Public WithEvents BGWUpdateChecker As BackgroundWorker
        Public WithEvents BGWConnectIrc As BackgroundWorker
        Public WithEvents LightTimerObj As ThreadTimer
        Public WithEvents GreetingTimerObj As ThreadTimer
        Public WithEvents ExpCheckerTimerObj As ThreadTimer
        Public WithEvents StatsTimerObj As ThreadTimer
        Public WithEvents SpellTimerObj As ThreadTimer
        Public WithEvents UHTimerObj As ThreadTimer
        Public WithEvents PotionTimerObj As ThreadTimer
        Public WithEvents ManaPotionTimerObj As ThreadTimer
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
        Public WithEvents ScriptsForm As New frmScripts
        Public WithEvents KeyboardForm As New frmKeyboard
        Public WithEvents LagBarForm As New frmLagBar
        Public WithEvents EditLootForm As New frmEditLoot

        'Public Map As MapTiles
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
        Public WithEvents MagicWallTimerObj As ThreadTimer
        Public WithEvents DancerTimerObj As ThreadTimer
        Public WithEvents AmmoMakerTimerObj As ThreadTimer
        Public Scripts As List(Of ScriptDefinition)
        Public CommandParser As CommandParser
        Public WithEvents RenameBackpackObj As ThreadTimer
        Public _NotifyIcon As NotifyIcon
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

        Public CharacterLoc As ITibia.LocationDefinition
        Public CharacterLastLocation As ITibia.LocationDefinition
        Public LightC As ITibia.LightColor = ITibia.LightColor.Darkness
        Public LightI As ITibia.LightIntensity = ITibia.LightIntensity.None
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
        'Public TibiaDirectory As String = ""
        'Public TibiaFilename As String = ""

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

        Public FisherSpeed As Integer = 0
        Public FisherMinimumCapacity As Integer = 0
        Public FisherTurbo As Boolean = False

        Public RunemakerSpell As ISpells.SpellDefinition = Nothing
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
        Public HealSpell As ISpells.SpellDefinition
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
        Public IsOpenTibiaServer As Boolean = False

        Public LastUpdateCheck As Date = Date.MinValue

        Public RainbowOutfitHead As Integer = 0
        Public RainbowOutfitBody As Integer = 0
        Public RainbowOutfitLegs As Integer = 0
        Public RainbowOutfitFeet As Integer = 0

        Public TibiaWindowState As ITibia.WindowStates = ITibia.WindowStates.Active
        Public TibiaClientIsVisible As Boolean = True

        Public FrameRateBegin As Integer = 0
        Public FrameRateActive As Integer = 0
        Public FrameRateInactive As Integer = 0
        Public FrameRateMinimized As Integer = 0
        Public FrameRateHidden As Integer = 0

        Public DrinkerManaRequired As Integer = 0

        Public Walker_Waypoints As New List(Of Walker)
        Public WaitAttacker As Date = Nothing
        Public WaypointIndex As Integer = 0
        Public WalkerWaitUntil As DateTime
        Public WalkerFirstTime As Boolean = True
        Public IsOpeningReady As Boolean = True
        Public CBState As CavebotState = CavebotState.Walking
        Public CBContainerCount As Integer = 0
        Public CBCreatureDied As Boolean = False
        Public LearningMode As Boolean = False
        Public IgnoreCreature As List(Of Integer)
        Public LooterNextExecution As Long = 0
        Public LootHasChanged As Integer = 2

        Public LooterItemID As Integer = 0
        Public LooterLoc As ITibia.LocationDefinition
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
        Public Combobotleaders As New Collection

        Public AmuletID As Integer = 0

        Public RingID As Integer = 0

        Public LastActivity As Date = Date.Now

        Public TTMessages As Integer = 0

        Public NameSpyActivated As Boolean = False

        Public MagicWalls As List(Of MagicWallDefinition)

        Public ManaPotionID As Integer = 0

        Public PotionHPRequired As Integer = 0
        Public PotionID As Integer = 0

        Public AmmoMakerMinCap As Integer = 0
        Public AmmoMakerMinMana As Integer = 0
        Public AmmoMakerSpell As ISpells.SpellDefinition

        Public TTBState As BotState = BotState.Running

        Public FavoredWeapon As List(Of WeaponFavoritDefinition)
        Public FavoredWeaponEnabled As Boolean = False
        Public FavoredWeaponShield As Integer = 0
#End Region

#Region " Initialization"

        Public Sub New()
            Try
                Consts = New Constants()
                LS = New Levelspy()
                Outfits = New Outfits()
                Spells = New Spells()
                LootItems = New LootItems()
                Creatures = New Creatures()
                Scripts = New List(Of ScriptDefinition)
                CommandParser = New CommandParser()
                HotkeySettings = New HotkeySettings()
                BGWOpenCommand = New BackgroundWorker()
                BGWCharCommand = New BackgroundWorker()
                BGWGuildMembersCommand = New BackgroundWorker()
                BGWConnectIrc = New BackgroundWorker
                BGWUpdateChecker = New BackgroundWorker()
                BGWMapViewer = New BackgroundWorker()
                BGWSendLocation = New BackgroundWorker()
                StatsTimerObj = New ThreadTimer(300)
                LightTimerObj = New ThreadTimer(500)
                ExpCheckerTimerObj = New ThreadTimer(1000)
                GreetingTimerObj = New ThreadTimer(10000)
                SpellTimerObj = New ThreadTimer(1000)
                UHTimerObj = New ThreadTimer(1000)
                PotionTimerObj = New ThreadTimer(1000)
                ManaPotionTimerObj = New ThreadTimer(1000)
                AdvertiseTimerObj = New ThreadTimer(121000)
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
                LooterTimerObj = New ThreadTimer(100)
                StackerTimerObj = New ThreadTimer()
                ShowInvisibleCreaturesTimerObj = New ThreadTimer(500)
                AutoPublishLocationTimerObj = New ThreadTimer(Consts.AutoPublishLocationInterval)
                RainbowOutfitTimerObj = New ThreadTimer(50)
                FPSChangerTimerObj = New ThreadTimer(1000)
                TibiaClientStateTimerObj = New ThreadTimer(500)
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
                MagicWallTimerObj = New ThreadTimer(300)
                MagicWalls = New List(Of MagicWallDefinition)
                DancerTimerObj = New ThreadTimer()
                AmmoMakerTimerObj = New ThreadTimer(1000)
                RenameBackpackObj = New ThreadTimer(100)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#End Region

#Region " Background Workers "

#Region " Looter BG Worker "

        'Test with normal function
        Private Sub LootMonster()
            Try
                System.Threading.Thread.Sleep(300)
                If LooterItemID = 0 Then Exit Sub
                Dim N As Byte = 0
                'N = Container.ContainerCount
                'If N > &HF Then N = &HF
                N = &HE
                'Dim buffer() As Byte
                Dim ServerPacket As New ServerPacketBuilder(Proxy)
                ServerPacket.UseObject(LooterItemID, LooterLoc, N)
                'buffer = UseObject(LooterItemID, LooterLoc, N)
                Static Cont As New Container
                If N - 1 = Cont.ContainerCount Then
                    ReplacedContainer = True
                Else
                    ReplacedContainer = False
                End If
                'ConsoleWrite(BytesToStr(buffer))
                'Proxy.SendPacketToServer(buffer)
                LooterItemID = 0
                If Kernel.CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    WaitTime = Date.Now.AddSeconds(5)
                    IsOpeningReady = True
                    CBCreatureDied = False
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Send Location BG Worker "

        Private Sub BGWSendLocation_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGWSendLocation.DoWork
            Try
                Dim R As New Random(System.DateTime.Now.Millisecond)
                Dim Key As Integer = R.Next(1000, 9999)
                Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("charname=" & System.Web.HttpUtility.UrlEncode(Client.CharacterName) & "&x=" & CharacterLoc.X & "&y=" & CharacterLoc.Y & "&z=" & CharacterLoc.Z & "&key=" & Key)
                Dim _Client As New WebClient
                _Client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                Dim URI As New System.Uri("http://www.tibiatek.com/updatemaploc.php")
                _Client.UploadData(URI, "POST", Content)
                Dim ChatMessage As ChatMessageDefinition
                ChatMessage.Destinatary = SendLocationDestinatary
                ChatMessage.MessageType = ITibia.MessageType.PrivateMessage
                ChatMessage.PrivateMessageType = ITibia.PrivateMessageType.Normal
                ChatMessage.Message = "http://www.tibiatek.com/Client.MapTiles.php?charname=" & System.Web.HttpUtility.UrlEncode(Client.CharacterName) & "&key=" & Key & "#pointer"
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
                Dim WClient As WebClient
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
                    WClient = New WebClient()
                    Data = WClient.OpenRead(URL)
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
                        Kernel.ConsoleWrite("The character does not exist.")
                    Else
                        Dim ClientPacket As New ClientPacketBuilder(Proxy)
                        ClientPacket.SystemMessage(SysMessageType.Information, Output)
                        'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, Output))
                        'Core.ConsoleWrite(Output)
                    End If
                    If Comment.Length > 0 Then
                        Dim RegExp As New Regex("&[^;]+;")
                        Comment = RegExp.Replace(Comment, "")
                        Kernel.ConsoleWrite("Comment: " & Comment.Replace("<br />", ""))
                    Else
                        Kernel.ConsoleWrite("Comment not available.")
                    End If
                    If OnDeath Then
                        Kernel.ConsoleWrite("Last 5 Deaths:" & Death)
                    End If
                    Reader.Close()
                    Data.Close()
                Catch Exception As WebException
                    Kernel.ConsoleWrite("Error while fetching URL with message """ & Exception.Message & """.")
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
                Dim WClient, WClient2 As WebClient
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
                    WClient = New WebClient()
                    Data = WClient.OpenRead(URL)
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
                        Kernel.ConsoleWrite("This guild does not exist, make sure you typed it correctly (case-sensitive).")
                    Else
                        WClient2 = New WebClient
                        Data2 = WClient2.OpenRead("http://www.tibia.com/community/?subtopic=whoisonline&world=" & World & "&order=level")
                        Reader2 = New StreamReader(Data2)
                        Line = Reader2.ReadLine()
                        While (Not Line Is Nothing)
                            MCollection = RegExp.Matches(Line)
                            If MCollection.Count > 0 Then
                                Kernel.ConsoleWrite("~~~~~~~Online Players~~~~~~~")
                                For I = 0 To MCollection.Count - 1
                                    CurrentPlayer = MCollection(I).Groups(1).ToString
                                    Index = Players.IndexOf(CurrentPlayer)
                                    If Index > -1 Then
                                        Kernel.ConsoleWrite(CurrentPlayer.Replace("&#160;", " "))
                                        Players.RemoveAt(Index)
                                    End If
                                Next
                            End If
                            Line = Reader2.ReadLine
                        End While
                        If Not GuildMembersOnlineOnly AndAlso Players.Count > 0 Then
                            Kernel.ConsoleWrite("~~~~~~~Offline Players~~~~~~~")
                            Players.Sort()
                            For I = 0 To Players.Count - 1
                                Kernel.ConsoleWrite(Players(I).Replace("&#160;", " "))
                            Next
                        End If
                        Reader2.Close()
                        Data2.Close()
                    End If
                    Reader.Close()
                    Data.Close()
                Catch Exception As WebException
                    Kernel.ConsoleWrite("Error while fetching URL with message """ & Exception.Message & """.")
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
                    Dim ClientPacket As New ClientPacketBuilder(Proxy)
                    ClientPacket.SystemMessage(SysMessageType.StatusWarning, _
                     "There is a new version of TibiaTek Bot available for download. " & _
                     "Get it at: " & Website & ".")
                    'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusWarning, _
                    ' "There is a new version of TibiaTek Bot available for download. " & _
                    ' "Get it at: " & Website & "."))
                End If
            Catch
            End Try

        End Sub

#End Region

#Region " Connect Irc BG Worker "
        Public Sub ConnectIrcd_() Handles BGWConnectIrc.DoWork
            Try
                ConnectToIrc()
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#End Region

#Region " Thread Timers "
        Public Sub StopEverything()
            Try
                If BGWConnectIrc.IsBusy Then BGWConnectIrc.CancelAsync()
                LightC = ITibia.LightColor.Darkness
                LightI = ITibia.LightIntensity.None
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
                PotionTimerObj.StopTimer()
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
                MagicWalls.Clear()
                Client.SetFramesPerSecond(Consts.FPSWhenActive)
                MagicWalls.Clear()
                Log("Event", "All timers are now stopped.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#Region " Window Timer "

        Private Sub WindowTimerObj_Execute() Handles WindowTimerObj.OnExecute
            Try
                If Client Is Nothing Then Exit Sub
                Dim WindowBegin As Integer = 0
                Dim WindowCaption As String = ""
                Dim BotNameStart As String = BotName
                Dim Title As String

                If TTBState = BotState.Paused Then BotNameStart += " [Paused]"
                Client.ReadMemory(Consts.ptrWindowBegin, WindowBegin, 4)
                If WindowBegin = 0 Then 'no window opened
                    If Not Client.IsConnected() Then
                        If Not (Kernel.Proxy Is Nothing OrElse Kernel.Client Is Nothing) Then
                            Title = BotNameStart & " - " & Hex(Kernel.Client.GetProcessHandle) & " - Not Logged In"
                        Else
                            Title = BotNameStart & " - Not Logged In"
                        End If
                        If Not Client.Title.Equals(Title) Then
                            Client.Title = Title
                        End If
                    Else
                        If HotkeyWindowWasOpened Then
                            HotkeySettings.LoadFromMemory()
                            HotkeySettings.Save()
                            ConsoleWrite("Your hotkeys have been saved.")
                            HotkeyWindowWasOpened = False
                        End If
                        If Not Client.Title.Equals(BotNameStart) AndAlso Not ExpCheckerActivated AndAlso Not FakingTitle Then
                            Client.Title = BotNameStart & " - " & Client.CharacterName
                        End If
                    End If
                Else
                    Client.ReadMemory(WindowBegin + Consts.WindowCaptionOffset, WindowCaption)
                    If Not Client.IsConnected() Then
                        If Not (Kernel.Proxy Is Nothing OrElse Kernel.Client Is Nothing) Then
                            Title = BotNameStart & " - " & Hex(Kernel.Client.GetProcessHandle) & " - " & WindowCaption
                        Else
                            Title = BotNameStart & " - " & WindowCaption
                        End If
                        If Not Client.Title.Equals(Title) Then
                            Client.Title = Title
                        End If
                    Else
                        If Not ExpCheckerActivated AndAlso Not FakingTitle Then
                            Title = BotNameStart & " - " & Client.CharacterName
                            If Not Client.Title.Equals(Title) Then
                                Client.Title = Title
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
                TibiaWindowState = Client.GetWindowState()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " FPS Changer Timer "

        Private Sub FPSChangerTimerObj_Execute() Handles FPSChangerTimerObj.OnExecute
            Try
                Select Case TibiaWindowState
                    Case ITibia.WindowStates.Active
                        Client.SetFramesPerSecond(FrameRateActive)
                        System.Threading.Thread.Sleep(1000)
                    Case ITibia.WindowStates.Inactive
                        Client.SetFramesPerSecond(FrameRateInactive)
                        System.Threading.Thread.Sleep(1000)
                    Case ITibia.WindowStates.Minimized
                        Client.SetFramesPerSecond(FrameRateMinimized)
                        System.Threading.Thread.Sleep(1000)
                    Case ITibia.WindowStates.Hidden
                        Client.SetFramesPerSecond(FrameRateHidden)
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
                If Not Client.IsConnected Then Exit Sub
                Dim BL As New BattleList
                BL.Reset(True)
                Do
                    If BL.IsOnScreen AndAlso BL.OutfitID = 0 AndAlso Not BL.IsMyself AndAlso Not BL.IsPlayer Then
                        Dim Outfit As New IOutfits.OutfitDefinition
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
            Try
                If Not Client.IsConnected Then Exit Sub
                If String.IsNullOrEmpty(Client.CharacterName) Or CharacterLoc.X = 0 Or CharacterLoc.Y = 0 Then Exit Sub
                Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("charname=" & System.Web.HttpUtility.UrlEncode(Client.CharacterName) & "&x=" & CharacterLoc.X & "&y=" & CharacterLoc.Y & "&z=" & CharacterLoc.Z)
                Dim WClient As New WebClient
                WClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                WClient.UploadDataAsync(New System.Uri("http://www.tibiatekbot.com/updatemaploc.php"), "POST", Content)
            Catch
            End Try
        End Sub

#End Region

#Region " Auto Stacker Timer "

        Private Sub StackerTimerObj_Execute() Handles StackerTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim MyContainer As New Container
                Dim SecondContainer As New Container
                Dim ContainerIndex As Integer
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Dim Item2 As Scripting.IContainer.ContainerItemDefinition
                Dim ContainerItemCount As Integer
                MyContainer.Reset()
                Do
                    If MyContainer.IsOpened Then
                        'do not stack if it's fake bp
                        If MyContainer.GetContainerIndex = &HF AndAlso MyContainer.GetContainerSize = &H24 Then Continue Do
                        If LooterTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                            If MyContainer.GetName.StartsWith("Dead") Or MyContainer.GetName.StartsWith("Slain") Or MyContainer.GetName.StartsWith("Remains") Or MyContainer.GetName.StartsWith("Bag") Then Continue Do
                        End If
                        ContainerItemCount = MyContainer.GetItemCount
                        ContainerIndex = MyContainer.GetContainerIndex
                        For I As Integer = 0 To ContainerItemCount - 1
                            Item = MyContainer.Items(I)
                            If Client.Objects.HasFlags(Item.ID, IObjects.ObjectFlags.IsStackable) AndAlso Item.Count < 100 Then
                                If MyContainer.FindItem(Item2, Item.ID, ContainerIndex, I + 1, ContainerIndex, 1, 99) Then
                                    Dim ServerPacket As New ServerPacketBuilder(Proxy)
                                    ServerPacket.MoveObject(Item, Item2.Location)
                                    'Proxy.SendPacketToServer(MoveObject(Item, Item2.Location))
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
            Dim WaitMillis As Integer = 10 'When any packet is sent to server WaitMillis is set to 300
            Try
                If LooterNextExecution > Date.Now.Ticks OrElse LootHasChanged = 0 Then
                    Exit Sub
                End If
                Static ServerPacket As New ServerPacketBuilder(Proxy)
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                LootHasChanged -= 1 'The looter has two tries to loot successfully
                Dim ActualItem As Integer
                Dim ActualIndex As Integer
                Dim ActualIndex2 As Integer
                Dim LootItem As New LootItems.LootItemDefinition
                Dim Container As New Container
                Dim NumberOfBps As Integer = Container.GetBackpackCount
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Container.Reset()
                Dim Containers() As InternalContainer = {New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer, New InternalContainer}
                Do
                    If Container.IsOpened() Then
                        ActualIndex = Container.GetContainerIndex()
                        Containers(ActualIndex).SetSize(Container.GetContainerSize())
                        Containers(ActualIndex).Index = ActualIndex
                        Containers(ActualIndex).ItemCount = Container.GetItemCount()
                        Containers(ActualIndex).ID = Container.GetContainerID()
                        Containers(ActualIndex).Name = Container.GetName()
                        Containers(ActualIndex).Parent = Container.HasParent()
                        For ActualItem = Container.GetItemCount() - 1 To 0 Step -1
                            Containers(ActualIndex).SetItem(ActualItem, Container.Items(ActualItem))
                        Next ActualItem
                    End If
                Loop While Container.NextContainer()
                Dim Item2 As Scripting.IContainer.ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim ContainerItemCount2 As Integer
                Dim Found As Boolean = False
                Dim BrownBagID As UShort = Client.Objects.ID("Brown Bag")
                Client.ReadMemory(Consts.ptrCapacity, Capacity, 2)

                'If all loot conditions are fulfilled, start looting
                If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Stopped AndAlso _
                (Consts.UnlimitedCapacity OrElse Capacity > LooterMinimumCapacity) OrElse _
                (CavebotForm.LootFromCorpses.Checked AndAlso _
                (Consts.UnlimitedCapacity OrElse Capacity > CInt(CavebotForm.LootMinimumCap.Value))) Then
                    For ActualIndex = 0 To 15
                        If Containers(ActualIndex).ID = 0 Then Continue For
                        If Not Containers(ActualIndex).GetName.StartsWith("Dead") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Slain") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Bag") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Remains") Then Continue For
                        If Containers(ActualIndex).GetName.StartsWith("Bag") AndAlso Containers(ActualIndex).GetContainerIndex() < &HF Then Continue For
                        ContainerItemCount = Containers(ActualIndex).GetItemCount()
                        For I As Integer = ContainerItemCount - 1 To 0 Step -1
                            Item = Containers(ActualIndex).Items(I)
                            If Item.ID = 0 Then Continue For
                            If Item.ID = BrownBagID AndAlso Not BagOpened AndAlso Consts.LootInBag Then 'got bag!
                                'Dim ServerPacket As New ServerPacketBuilder(Proxy)
                                ServerPacket.UseObject(Item, &HF)
                                'Proxy.SendPacketToServer(OpenContainer(Item, &HF), False)
                                BagOpened = True
                            End If
                            If LootItems.IsLootable(Item.ID) Then
                                LootItem = LootItems.GetLootItem(Item.ID)
                                If LootItem.GetLootBackpack = 0 Then 'LOOT TO GROUND
                                    ServerPacket.MoveObject(Item, CharacterLoc)
                                    Continue For
                                End If
                                Dim RemainingCount As Integer
                                RemainingCount = Max(Item.Count, 1)
                                If Client.Objects.HasFlags(Item.ID, IObjects.ObjectFlags.IsStackable) Then
                                    For ActualIndex2 = 0 To 15
                                        If Containers(ActualIndex2).ID = 0 Then Continue For
                                        'if its a corpse, or a brown bag that has a parent container
                                        'this is NOT always true, but for the sake of simplicity...
                                        If Containers(ActualIndex2).GetName.StartsWith("Dead") _
                                            OrElse Containers(ActualIndex2).GetName.StartsWith("Slain") _
                                            OrElse Containers(ActualIndex2).GetName.StartsWith("Remains") _
                                            OrElse (Containers(ActualIndex2).GetName.StartsWith("Bag") _
                                            AndAlso Containers(ActualIndex2).HasParent _
                                            AndAlso Containers(ActualIndex2).GetContainerID = BrownBagID) Then Continue For
                                        If Not Containers(ActualIndex2).GetContainerIndex + 1 = LootItems.GetLootingBackpack(Item.ID, NumberOfBps) Then Continue For
                                        ContainerItemCount2 = Containers(ActualIndex2).GetItemCount()
                                        For E As Integer = 0 To ContainerItemCount2 - 1
                                            Item2 = Containers(ActualIndex2).Items(E)
                                            If Item2.ID = 0 Then Continue For
                                            If Item2.Count = 100 Then Continue For 'already fully stacked, next please..
                                            If Item2.ID = Item.ID Then
                                                'Dim ServerPacket As New ServerPacketBuilder(Proxy)
                                                If Item2.Count + RemainingCount <= 100 Then
                                                    ServerPacket.MoveObject(Item, Item2.Location, RemainingCount)
                                                    'Proxy.SendPacketToServer(MoveObject(Item, Item2.Location, RemainingCount), False)
                                                    WaitMillis = 1000
                                                    Containers(ActualIndex).RemoveItem(Item.Location.Z)
                                                    Containers(ActualIndex2).SetItemCount(Item2.Location.Z, Item2.Count + RemainingCount)
                                                    RemainingCount = 0
                                                ElseIf Containers(ActualIndex2).GetItemCount < Containers(ActualIndex2).GetContainerSize Then
                                                    ServerPacket.MoveObject(Item, Item2.Location, RemainingCount)
                                                    'Proxy.SendPacketToServer(MoveObject(Item, Item2.Location, RemainingCount), False)
                                                    WaitMillis = 1000
                                                    Containers(ActualIndex).RemoveItem(Item.Location.Z)
                                                    Containers(ActualIndex2).SetItemCount(Item2.Location.Z, 100)
                                                    Containers(ActualIndex2).AddItem(Item.ID, (Item2.Count + RemainingCount) - 100)
                                                    RemainingCount = 0
                                                Else
                                                    ServerPacket.MoveObject(Item, Item2.Location, 100 - Item2.Count)
                                                    'Proxy.SendPacketToServer(MoveObject(Item, Item2.Location, 100 - Item2.Count), False)
                                                    RemainingCount = RemainingCount - (100 - Item2.Count)
                                                    Containers(ActualIndex2).SetItemCount(Item2.Location.Z, 100)
                                                End If
                                            End If
                                            If RemainingCount = 0 Then
                                                Exit For
                                            End If
                                        Next
                                        If RemainingCount = 0 Then
                                            Exit For
                                        End If
                                    Next ActualIndex2
                                End If
                                If RemainingCount > 0 Then
                                    For ActualIndex2 = 0 To 15
                                        If Containers(ActualIndex2).ID = 0 Then Continue For
                                        If Containers(ActualIndex2).GetName.StartsWith("Dead") _
                                            OrElse Containers(ActualIndex2).GetName.StartsWith("Slain") _
                                            OrElse Containers(ActualIndex2).GetName.StartsWith("Remains") _
                                            OrElse (Containers(ActualIndex2).GetName.StartsWith("Bag") _
                                            AndAlso Containers(ActualIndex2).HasParent _
                                            AndAlso Containers(ActualIndex2).GetContainerID = BrownBagID) Then Continue For
                                        If Not Containers(ActualIndex2).GetContainerIndex + 1 = LootItems.GetLootingBackpack(Item.ID, NumberOfBps) Then Continue For
                                        If Containers(ActualIndex2).GetItemCount < Containers(ActualIndex2).GetContainerSize Then
                                            Dim Loc As ITibia.LocationDefinition
                                            Loc.X = &HFFFF
                                            Loc.Y = &H40 + Containers(ActualIndex2).GetContainerIndex()
                                            'Loc.Y = &H40 + LootItems.GetLootingBackpack(Item.ID, NumberOfBps) - 1
                                            Loc.Z = Containers(ActualIndex2).GetContainerSize - 1
                                            ServerPacket.MoveObject(Item, Loc, RemainingCount)
                                            'Proxy.SendPacketToServer(MoveObject(Item, Loc, RemainingCount), False)
                                            WaitMillis = 1000
                                            Containers(ActualIndex).RemoveItem(Item.Location.Z)
                                            Containers(ActualIndex2).AddItem(Item.ID, RemainingCount)
                                            RemainingCount = 0
                                        End If
                                    Next ActualIndex2
                                End If
                                If RemainingCount > 0 Then
                                    ServerPacket.MoveObject(Item, GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), RemainingCount)
                                    'Proxy.SendPacketToServer(MoveObject(Item, GetInventorySlotAsLocation(InventorySlots.Backpack), RemainingCount), False)
                                    WaitMillis = 1000
                                    Containers(ActualIndex).RemoveItem(Item.Location.Z)
                                End If
                            End If
                        Next
                    Next ActualIndex
                End If
                'Eating comes afterward, so the loot item positions don't get invalid
                If (CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running AndAlso _
                CavebotForm.EatFromCorpses.Checked) OrElse _
                (CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Stopped AndAlso Consts.LootEatFromCorpse) Then
                    For ActualIndex = 0 To 15
                        If Containers(ActualIndex).ID = 0 Then Continue For
                        If Not Containers(ActualIndex).GetName.StartsWith("Dead") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Slain") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Bag") _
                        AndAlso Not Containers(ActualIndex).GetName.StartsWith("Remains") Then Continue For
                        If Containers(ActualIndex).GetName.StartsWith("Bag") AndAlso Containers(ActualIndex).GetContainerIndex() < &HF Then Continue For
                        ContainerItemCount = Containers(ActualIndex).GetItemCount()
                        For I As Integer = ContainerItemCount - 1 To 0 Step -1
                            Item = Containers(ActualIndex).Items(I)
                            If Item.ID = 0 Then Continue For
                            If Client.Objects.Kind(Item.ID) = IObjects.ObjectKind.Food Then
                                ServerPacket.UseObject(Item)
                                'Proxy.SendPacketToServer(UseObject(Item), False)
                                WaitMillis = 1000
                            End If
                        Next I
                    Next ActualIndex
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            If LooterNextExecution = 0 Then
                LooterNextExecution = 1
            Else
                LooterNextExecution = Date.Now.Ticks + TimeSpan.TicksPerMillisecond * WaitMillis
            End If
        End Sub

#End Region

#Region " Stats Uploader Timer "

        Private Sub StatsUploaderTimerObj_Execute() Handles StatsUploaderTimerObj.OnExecute
            Try
                ' If StatsUploaderIsBusy Then Exit Sub
                Try
                    If Not Client.IsConnected Then Exit Sub
                    'StatsUploaderIsBusy = True
                    Dim WClient As New System.Net.WebClient
                    Dim BL As New BattleList
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
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

                    Dim xmlMagicLevel As XmlElement = xmlFile.CreateElement("MagicLevel")
                    Dim MagicLevel As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrMagicLevel, MagicLevel, 4)
                    xmlMagicLevel.InnerText = MagicLevel.ToString

                    Dim xmlMagicLevelP As XmlAttribute = xmlFile.CreateAttribute("MagicLevel")
                    Dim MagicLevelP As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrMagicLevelPercent, MagicLevelP, 1)
                    xmlMagicLevelP.InnerText = MagicLevelP.ToString
                    xmlMagicLevel.Attributes.Append(xmlMagicLevelP)

                    Dim xmlHitPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "HitPoints", "")
                    xmlHitPoints.InnerText = CStr(HitPoints)

                    Dim xmlManaPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "ManaPoints", "")
                    xmlManaPoints.InnerText = CStr(ManaPoints)

                    Dim xmlSoulPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "SoulPoints", "")
                    xmlSoulPoints.InnerText = CStr(SoulPoints)

                    Dim xmlCapacity As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Capacity", "")
                    Dim Capacity As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                    xmlCapacity.InnerText = CStr(Capacity)

                    Dim xmlStamina As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Stamina", "")
                    Dim Stamina As Integer = 0
                    Kernel.Client.ReadMemory(Consts.ptrStamina, Stamina, 4)
                    Dim StaminaTime As TimeSpan = TimeSpan.FromSeconds(Stamina)
                    xmlStamina.InnerText = StaminaTime.ToString

                    Dim xmlSkills As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Skills", "")
                    Dim Skill As Integer = 0
                    Dim SkillPercent As Integer = 0

                    Dim xmlFistFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "FistFighting", "")
                    Dim xmlFistFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.FistFighting * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.FistFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlFistFighting.InnerText = CStr(Skill)
                    xmlFistFightingP.InnerText = CStr(SkillPercent)
                    xmlFistFighting.Attributes.Append(xmlFistFightingP)
                    xmlSkills.AppendChild(xmlFistFighting)

                    Dim xmlClubFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "ClubFighting", "")
                    Dim xmlClubFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.ClubFighting * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.ClubFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlClubFighting.InnerText = CStr(Skill)
                    xmlClubFightingP.InnerText = CStr(SkillPercent)
                    xmlClubFighting.Attributes.Append(xmlClubFightingP)
                    xmlSkills.AppendChild(xmlClubFighting)

                    Dim xmlSwordFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "SwordFighting", "")
                    Dim xmlSwordFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.SwordFighting * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.SwordFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlSwordFighting.InnerText = CStr(Skill)
                    xmlSwordFightingP.InnerText = CStr(SkillPercent)
                    xmlSwordFighting.Attributes.Append(xmlSwordFightingP)
                    xmlSkills.AppendChild(xmlSwordFighting)

                    Dim xmlAxeFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "AxeFighting", "")
                    Dim xmlAxeFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.AxeFighting * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.AxeFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlAxeFighting.InnerText = CStr(Skill)
                    xmlAxeFightingP.InnerText = CStr(SkillPercent)
                    xmlAxeFighting.Attributes.Append(xmlAxeFightingP)
                    xmlSkills.AppendChild(xmlAxeFighting)

                    Dim xmlDistanceFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "DistanceFighting", "")
                    Dim xmlDistanceFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.DistanceFighting * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.DistanceFighting * Consts.SkillsDist), SkillPercent, 1)
                    xmlDistanceFighting.InnerText = CStr(Skill)
                    xmlDistanceFightingP.InnerText = CStr(SkillPercent)
                    xmlDistanceFighting.Attributes.Append(xmlDistanceFightingP)
                    xmlSkills.AppendChild(xmlDistanceFighting)

                    Dim xmlShielding As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Shielding", "")
                    Dim xmlShieldingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.Shielding * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.Shielding * Consts.SkillsDist), SkillPercent, 1)
                    xmlShielding.InnerText = CStr(Skill)
                    xmlShieldingP.InnerText = CStr(SkillPercent)
                    xmlShielding.Attributes.Append(xmlShieldingP)
                    xmlSkills.AppendChild(xmlShielding)

                    Dim xmlFishing As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Fishing", "")
                    Dim xmlFishingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
                    Kernel.Client.ReadMemory(Consts.ptrSkillsBegin + (ITibia.Skills.Fishing * Consts.SkillsDist), Skill, 1)
                    Kernel.Client.ReadMemory(Consts.ptrSkillsPercentBegin + (ITibia.Skills.Fishing * Consts.SkillsDist), SkillPercent, 1)
                    xmlFishing.InnerText = CStr(Skill)
                    xmlFishingP.InnerText = CStr(SkillPercent)
                    xmlFishing.Attributes.Append(xmlFishingP)
                    xmlSkills.AppendChild(xmlFishing)

                    Dim xmlBattlelist As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Battlelist", "")
                    BL.Reset(True)
                    Do
                        If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                            Dim xmlEntity As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Entity", "")
                            Dim Loc As ITibia.LocationDefinition = BL.GetLocation
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
                    Dim Item As Scripting.IContainer.ContainerItemDefinition
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
                                xmlItemName.InnerText = Client.Objects.Name(Item.ID)
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
                    xmlStats.AppendChild(xmlMagicLevel)
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
                            WClient.UploadFile("ftp://" & UploaderUserId & ":" & UploaderPassword & "@" & UploaderUrl & UploaderPath & UploaderFilename, "Temp.xml")
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
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
                    Dim PLPartyStatus As IBattlelist.PartyStatus = BL.GetPartyStatus
                    If (PLPartyStatus = IBattlelist.PartyStatus.Member OrElse PLPartyStatus = IBattlelist.PartyStatus.Leader) _
                     AndAlso BL.GetHPPercentage <= HealPartyMinimumHPPercentage Then
                        Select Case HealPartyHealType
                            Case HealTypes.ExuraSio
                                If ManaPoints < Spells.GetSpellMana("Heal Friend") Then Exit Sub
                                SioPlayer(BL.GetName)
                            Case HealTypes.UltimateHealingRune
                                UHByCharacterID(BL.GetEntityID)
                            Case HealTypes.Both
                                If ManaPoints >= Spells.GetSpellMana("Heal Friend") Then
                                    SioPlayer(BL.GetName)
                                Else
                                    UHByCharacterID(BL.GetEntityID)
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If HealTimerObj.Interval > Consts.HealersCheckInterval Then HealTimerObj.Interval = Consts.HealersCheckInterval
                If HealMinimumHP = 0 OrElse HitPoints > HealMinimumHP Then Exit Sub
                Dim Output As String = HealSpell.Words
                If ManaPoints < Spells.GetSpellMana(HealSpell.Name) Then Exit Sub
                If String.Compare(HealSpell.Name, "heal friend", True) = 0 Then
                    Dim BL As New BattleList
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    Output &= " """ & BL.GetName & """"
                End If
                If Not String.IsNullOrEmpty(HealComment) Then Output &= " """"" & HealComment
                HealTimerObj.Interval = Consts.HealersAfterHealDelay
                Dim ServerPacket As New ServerPacketBuilder(Proxy)
                ServerPacket.Speak(Output, ITibia.DefaultMessageType.Normal)
                System.Threading.Thread.Sleep(Consts.HealersAfterHealDelay)
                'Proxy.SendPacketToServer(Speak(Output, MessageType.Normal))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " PickUp Timer "

        Public Sub PickUpTimerObj_Execute() Handles PickUpTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If PickUpItemID = 0 Then Exit Sub
                'If PickUpTimerObj.Interval > 500 Then PickUpTimerObj.Interval = 500
                Dim RightHandItemID As Integer = 0
                Dim RightHandItemCount As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandItemCount, 1)
                Dim ServerPacket As New ServerPacketBuilder(Proxy)
                If RightHandItemID > 0 AndAlso RightHandItemID <> PickUpItemID Then
                    If RightHandItemCount = 0 Then RightHandItemCount = 1

                    ServerPacket.MoveObject(RightHandItemID, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), CInt(RightHandItemCount))
                    'Proxy.SendPacketToServer(MoveObject(RightHandItemID, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), CInt(RightHandItemCount)))
                    PickUpTimerObj.Interval = 2000
                    Exit Sub
                End If
                Dim StackSize As Integer = 0
                Dim ItemID As Integer = 0
                Dim ItemCount As Integer = 0
                Dim Capacity As Integer = 0
                Dim Z As Integer = 0
                Dim Source As New ITibia.LocationDefinition
                'Core.Client.ReadMemory(Consts.ptrCapacity, Capacity, 2)

                Dim MaxItemsToPickUp As Integer = CInt(Fix(Capacity / 20))
                If Consts.UnlimitedCapacity Then
                    MaxItemsToPickUp = 100
                End If
                Dim Address As Integer = 0
                If MaxItemsToPickUp > 0 Then
                    For X As Short = 7 To 9
                        For Y As Short = 5 To 7
                            Address = Client.MapTiles.GetAddress(X, Y, Client.MapTiles.WorldZToClientZ(CharacterLoc.Z))
                            Kernel.Client.ReadMemory(Address, StackSize, 1)
                            If StackSize > 1 Then 'look for spear plx
                                For I As Integer = 0 To StackSize - 1
                                    Kernel.Client.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectIdOffset, ItemID, 2)
                                    If ItemID = PickUpItemID Then
                                        Kernel.Client.ReadMemory(Address + (I * Consts.MapObjectDist) + Consts.MapObjectDataOffset, ItemCount, 1)
                                        If ItemCount = 0 Then ItemCount = 1
                                        Source = CharacterLoc
                                        Source.X += X - 8
                                        Source.Y += Y - 6
                                        ServerPacket.MoveObject(PickUpItemID, Source, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), Min(ItemCount, MaxItemsToPickUp))
                                        'Proxy.SendPacketToServer(MoveObject(PickUpItemID, Source, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), Min(ItemCount, MaxItemsToPickUp)))
                                        Exit Sub
                                    End If
                                Next
                            End If
                        Next
                    Next
                End If
                'System.Threading.Thread.Sleep(300)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandItemCount, 1)
                Kernel.Client.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                MaxItemsToPickUp = CInt(Fix(Capacity / 20))
                If RightHandItemID = 0 And RightHandItemCount = 0 Then
                    'Dim MyContainer As New Container
                    'Dim ContainerItemCount As Integer
                    Dim Item As Scripting.IContainer.ContainerItemDefinition
                    Dim Found As Boolean = False

                    If (New Container).FindItem(Item, PickUpItemID, 0, 0) Then
                        If MaxItemsToPickUp > 0 Then
                            ServerPacket.MoveObject(PickUpItemID, Item.Location, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), Min(MaxItemsToPickUp, Item.Count))
                            'Proxy.SendPacketToServer(MoveObject(PickUpItemID, Item.Location, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), Min(MaxItemsToPickUp, Item.Count)))
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim Container As New Container
                Dim ContainerItemCount As Integer
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Dim Found As Boolean = False
                Dim AmmoItemID As Integer = 0
                Dim AmmoItemCount As Integer = 0
                Dim TotalAmmo As Integer = 0
                If AmmoRestackerItemID = 0 OrElse AmmoRestackerMinimumItemCount = 0 Then Exit Sub
                'find out of we really need more ammo
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), AmmoItemID, 2)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, AmmoItemCount, 1)
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
                Dim ServerPacket As New ServerPacketBuilder(Proxy)
                Dim ClientPacket As New ClientPacketBuilder(Proxy)
                If Found Then
                    If AmmoRestackerTimerObj.Interval = 2000 Then AmmoRestackerTimerObj.Interval = 1000
                    If AmmoRestackerOutOfAmmo Then AmmoRestackerOutOfAmmo = False
                    ServerPacket.MoveObject(AmmoRestackerItemID, Item.Location, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), 100 - AmmoItemCount)
                    ClientPacket.SystemMessage(SysMessageType.Information, (TotalAmmo + AmmoItemCount) & " ammunition left.")
                    'Proxy.SendPacketToServer(MoveObject(AmmoRestackerItemID, Item.Location, GetInventorySlotAsLocation(InventorySlots.Belt), 100 - AmmoItemCount))
                    'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, (TotalAmmo + AmmoItemCount) & " ammunition left."))
                Else
                    If Not AmmoRestackerOutOfAmmo Then
                        AmmoRestackerTimerObj.Interval = 2000
                        ClientPacket.SystemMessage(SysMessageType.Information, "Warning: You ran out of ammunition.")
                        'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Warning: You ran out of ammunition."))
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If AutoTrainerMinHPPercent = 0 OrElse AutoTrainerMaxHPPercent = 0 Then
                    AutoTrainerEntities.Clear()
                    AutoTrainerMinHPPercent = 0
                    AutoTrainerMaxHPPercent = 0
                    Exit Sub
                End If
                Dim BL As New BattleList
                Dim AttackedEntityID As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrAttackedEntityID, AttackedEntityID, 4)
                If AttackedEntityID > 0 Then
                    If AutoTrainerEntities.Contains(AttackedEntityID) Then
                        If BL.Find(AttackedEntityID, True) Then
                            If BL.GetHPPercentage > AutoTrainerMinHPPercent Then
                                Exit Sub
                            Else
                                Dim ServerPacket As New ServerPacketBuilder(Proxy)
                                ServerPacket.StopEverything()
                                'Proxy.SendPacketToServer(PacketUtils.StopEverything)
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim FirstRightHandSlot As Integer = 0
                Dim FirstRightHandSlotCount As Integer = 0
                Dim BeltSlot As Integer = 0
                Dim RightHandSlot As Integer = 0
                Dim RightHandSlotCount As Integer = 0
                Dim Retries As Integer = 0
                Dim MyContainer As New Container
                Dim BlankRune As Scripting.IContainer.ContainerItemDefinition
                Dim BlankRuneID As UShort = Client.Objects.ID("Blank")
                Dim Found As Boolean = False
                Dim Count As Integer = 0
                If RunemakerManaPoints = 0 Then Exit Sub

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
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
                'check that there are no items occupying the belt slot
                Dim SP As New ServerPacketBuilder(Proxy)
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
                        SP.MoveObject(BeltSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), 100)
                        'Proxy.SendPacketToServer(MoveObject(BeltSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), 100))
                        System.Threading.Thread.Sleep(1000)
                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
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
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandSlotCount, 1)
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
                        SP.MoveObject(RightHandSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), Count)
                        'Proxy.SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), Count))
                        System.Threading.Thread.Sleep(1000)
                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
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
                    SP.MoveObject(BlankRune.ID, BlankRune.Location, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), 1)
                    'Proxy.SendPacketToServer(MoveObject(BlankRune.ID, BlankRune.Location, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), 1))
                    System.Threading.Thread.Sleep(1000)
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                    SP.Speak(RunemakerSpell.Words)
                    'Proxy.SendPacketToServer(Speak(RunemakerSpell.Words))
                    System.Threading.Thread.Sleep(1000)
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                    SP.MoveObject(RightHandSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), BlankRune.Location, 1)
                    'Proxy.SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), BlankRune.Location, 1))
                    System.Threading.Thread.Sleep(1000)
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                        SP.MoveObject(FirstRightHandSlot, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), GetInventorySlotAsLocation(ITibia.InventorySlots.RightHand), Count)
                        'Proxy.SendPacketToServer(MoveObject(FirstRightHandSlot, GetInventorySlotAsLocation(InventorySlots.Belt), GetInventorySlotAsLocation(InventorySlots.RightHand), Count))
                        System.Threading.Thread.Sleep(1000)
                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                If FisherTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then Exit Sub
                Dim Intervals() As UShort = {1000, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000}
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If FisherTimerObj.Interval = 10000 Then FisherTimerObj.Interval = 1000
                If Client.MapTiles.IsBusy Then Exit Sub
                Dim FishingRodItemData As Scripting.IContainer.ContainerItemDefinition
                Dim TileID As Integer
                Dim FishingRodID As UShort = Client.Objects.ID("Fishing Rod")
                Dim WormID As UShort = Client.Objects.ID("Worm")
                Dim WormItemData As Scripting.IContainer.ContainerItemDefinition
                Dim Tiles As New List(Of IMapTiles.TileObject)
                Dim Tile As IMapTiles.TileObject
                If Not (New Container).FindItem(WormItemData, WormID, 0, 0, Consts.MaxContainers - 1) Then
                    ConsoleError("Auto Fisher couldn't find any worms, it is now Disabled.")
                    FisherTimerObj.StopTimer()
                    Exit Sub
                End If
                If Not (New Container).FindItem(FishingRodItemData, FishingRodID, 0, 0, Consts.MaxContainers - 1) Then
                    ConsoleError("Auto Fisher couldn't find any fishing rods, pausing for 10 seconds.")
                    FisherTimerObj.Interval = 10000
                    Exit Sub
                End If
                If Not Consts.UnlimitedCapacity Then
                    If Capacity < FisherMinimumCapacity Then
                        Exit Sub
                    End If
                End If
                Dim TileObjects() As IMapTiles.TileObject
                For XXX As Integer = 1 To 15
                    For YYY As Integer = 1 To 11
                        TileObjects = Client.MapTiles.GetTileObjects(XXX, YYY, Client.MapTiles.WorldZToClientZ(CharacterLoc.Z))
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
                    Dim ServerPacket As New ServerPacketBuilder(Proxy)
                    ServerPacket.UseFishingRodOnLocation(FishingRodItemData, Tile.GetMapLocation, Tile.GetObjectID)
                    ServerPacket.Send()
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
                If Not Client.IsConnected Then Exit Sub
                Dim X, Y, Z As Integer
                Kernel.Client.ReadMemory(Consts.ptrCoordX, X, 2)
                CharacterLoc.X = X
                Kernel.Client.ReadMemory(Consts.ptrCoordY, Y, 2)
                CharacterLoc.Y = Y
                Kernel.Client.ReadMemory(Consts.ptrCoordZ, Z, 1)
                CharacterLoc.Z = Z
                If CharacterLastLocation.X <> CharacterLoc.X OrElse _
                 CharacterLastLocation.Y <> CharacterLoc.Y OrElse _
                 CharacterLastLocation.Z <> CharacterLoc.Z Then
                    Client.MapTiles.Refresh()
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
                If Not Client.IsConnected Then
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
                    Dim SP As New ServerPacketBuilder(Proxy)
                    Select Case ChatMessage.MessageType 'default
                        Case ITibia.MessageType.Default 'normal
                            SP.Speak(ChatMessage.Message, ChatMessage.DefaultMessageType)
                        Case ITibia.MessageType.PrivateMessage 'pm
                            SP.Speak(ChatMessage.Destinatary, ChatMessage.Message, ChatMessage.PrivateMessageType)
                        Case ITibia.MessageType.Channel 'channels
                            SP.Speak(ChatMessage.Message, ChatMessage.Channel, ChatMessage.ChannelMessageType)
                    End Select
                    'AppObjs.ConsoleRead(CStr(ChatMessage.SentByUser))
                    'Proxy.SendPacketToServer(bytBuffer)
                    'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, ChatMessage.Message))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Eater Timer "

        Public Sub EaterTimerObj_Execute() Handles EaterTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
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
                Dim TileObj As IMapTiles.TileObject
                Dim TileObjects() As IMapTiles.TileObject
                For Left As Integer = 1 To 17
                    For Top As Integer = 1 To 13
                        Dim Dist As Double = Math.Sqrt(Math.Pow(Left - 8, 2) + Math.Pow(Top - 6, 2))
                        If Dist <= MaxDistance Then
                            TileObjects = Client.MapTiles.GetTileObjects(Left, Top, Client.MapTiles.WorldZToClientZ(CharacterLoc.Z))
                            For Each TileObj In TileObjects
                                If Client.Objects.IsKind(TileObj.GetObjectID, IObjects.ObjectKind.Food) Then
                                    Dim SP As New ServerPacketBuilder(Proxy)
                                    SP.UseObject(TileObj.GetObjectID, TileObj.GetMapLocation)
                                    'Proxy.SendPacketToServer(UseObject(TileObj.GetObjectID, TileObj.GetMapLocation))
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
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Dim ContainerItemCount As Integer = 0
                Dim I As Integer
                Dim Found As Boolean = False
                Container.Reset()
                Do
                    If Container.IsOpened Then
                        ContainerItemCount = Container.GetItemCount
                        For I = 0 To ContainerItemCount - 1
                            Item = Container.Items(I)
                            If Client.Objects.IsKind(Item.ID, IObjects.ObjectKind.Food) Then
                                Found = True
                                Exit Do
                            End If
                        Next
                    End If
                Loop While Container.NextContainer()
                If Found Then
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.UseObject(Item)
                    'Core.Proxy.SendPacketToServer(UseObject(Item))
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
                If Not Client.IsConnected Then Exit Sub
                Dim X, Y, Z As Integer
                Kernel.Client.ReadMemory(Consts.ptrLevel, Level, 4)
                Kernel.Client.ReadMemory(Consts.ptrExperience, Experience, 4)
                Kernel.Client.ReadMemory(Consts.ptrHitPoints, HitPoints, 4)
                Kernel.Client.ReadMemory(Consts.ptrManaPoints, ManaPoints, 4)
                Kernel.Client.ReadMemory(Consts.ptrSoulPoints, SoulPoints, 4)
                Kernel.Client.ReadMemory(Consts.ptrCoordX, X, 2)
                CharacterLoc.X = X
                Kernel.Client.ReadMemory(Consts.ptrCoordY, Y, 2)
                CharacterLoc.Y = Y
                Kernel.Client.ReadMemory(Consts.ptrCoordZ, Z, 1)
                CharacterLoc.Z = Z
                Kernel.Client.ReadMemory(Consts.ptrCharacterID, CharacterID, 4)
                Kernel.Client.ReadMemory(Consts.ptrCapacity, Capacity, 4)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Light Effect Timer "

        Public Sub LightTimerObj_Execute() Handles LightTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                SetLight(LightI, LightC)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub SetLight(ByVal LightIntensity As ITibia.LightIntensity, ByVal LightColor As ITibia.LightColor)
            Try
                Dim BL As New BattleList
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
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
                If Not Client.IsConnected Then Exit Sub
                Dim BotNameStart As String = BotName
                If TTBState = BotState.Paused Then BotNameStart += " [Paused]"
                Dim NextLevelExpL As Double = 0
                Dim CurrentLevelExpL As Double = 0
                Dim ExperienceL As Double = Experience
                Dim LastExperienceL As Double = LastExperience
                Dim NextLevelPercentageL As Double = NextLevelPercentage
                If ExperienceL < 0 Then Exit Sub
                If ExpCheckerActivated Then
                    If LastExperienceL > 0 AndAlso ExperienceL = LastExperience Then
                        Exit Sub
                    End If
                End If
                NextLevelExpL = CDbl(Floor(((16 + (2 / 3)) * Pow(Level + 1, 3)) - (100 * Pow(Level + 1, 2)) + (((283 + (1 / 3)) * (Level + 1)) - 200)))
                NextLevelExp = CInt(NextLevelExpL)
                CurrentLevelExpL = CDbl(Floor(((16 + (2 / 3)) * Pow(Level, 3)) - (100 * Pow(Level, 2)) + (((283 + (1 / 3)) * (Level)) - 200)))
                CurrentLevelExp = CDbl(CurrentLevelExpL)
                If (Level = 0) Or (Experience = 0) Then Exit Sub
                NextLevelPercentageL = CDbl(Floor((ExperienceL - CurrentLevelExpL) * 100 / (NextLevelExpL - CurrentLevelExpL)))
                NextLevelPercentage = CInt(NextLevelPercentageL)
                If ExpCheckerActivated Then
                    If Not Client.Title.Equals(BotNameStart & " - " & Kernel.Client.CharacterName.ToString & " - Exp. For Level " & (Level + 1) & ": " & (NextLevelExp - Experience) & " (" & NextLevelPercentage & "% completed)") Then
                        Client.Title = BotNameStart & " - " & Kernel.Client.CharacterName.ToString & " - Exp. For Level " & (Level + 1) & ": " & (NextLevelExpL - ExperienceL) & " (" & NextLevelPercentageL & "% completed)"
                    End If
                    LastExperienceL = ExperienceL
                    LastExperience = Experience
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Spell Caster Timer "

        Private Sub SpellTimerObj_Execute() Handles SpellTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If SpellTimerObj.Interval = Consts.SpellCasterDelay Then SpellTimerObj.Interval = Consts.SpellCasterInterval
                If SpellManaRequired = 0 OrElse String.IsNullOrEmpty(SpellMsg) Then Exit Sub
                If ManaPoints = 0 Then
                    Exit Sub
                ElseIf ManaPoints >= SpellManaRequired Then
                    SpellTimerObj.Interval = Consts.SpellCasterDelay
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.Speak(SpellMsg)
                    Thread.Sleep(Consts.SpellCasterDelay)
                    'Proxy.SendPacketToServer(Speak(SpellMsg))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto UH Timer "

        Private Sub UHTimerObj_Execute() Handles UHTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If UHTimerObj.Interval > Consts.HealersCheckInterval Then UHTimerObj.Interval = Consts.HealersCheckInterval
                If UHHPRequired = 0 Then
                    UHTimerObj.StopTimer()
                    Exit Sub
                End If

                If HitPoints > UHHPRequired Then Exit Sub
                UHTimerObj.Interval = Consts.HealersAfterHealDelay
                'Proxy.SendPacketToServer(UseObjectOnPlayerAsHotkey(UHId, CharacterLoc))
                Dim SP As New ServerPacketBuilder(Proxy)
                SP.UseHotkey(UHId, CharacterID)
                'Proxy.SendPacketToServer(UseHotkey(UHId, CharacterID))
                'Proxy.SendPacketToServer(UseHotkey(UHId))
                Log("Auto UHer", "Used UH on yourself.")
                Thread.Sleep(Consts.HealersAfterHealDelay)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


#End Region

#Region " Heal Friend Timer "

        Private Sub HealFriendTimerObj_Execute() Handles HealFriendTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
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
                            UHByCharacterID(BL.GetEntityID)
                        Case HealTypes.Both
                            If ManaPoints >= Spells.GetSpellMana("Heal Friend") Then
                                SioPlayer(HealFriendCharacterName)
                            Else
                                UHByCharacterID(BL.GetEntityID)
                            End If
                    End Select
                    HealFriendTimerObj.Interval = Consts.HealersAfterHealDelay
                    Thread.Sleep(Consts.HealersAfterHealDelay)
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub SioPlayer(ByVal Name As String)
            Try
                Dim SP As New ServerPacketBuilder(Proxy)
                SP.Speak(Spells.GetSpellWords("Heal Friend") & " """ & Name & """")
                'Proxy.SendPacketToServer(Speak(Spells.GetSpellWords("Heal Friend") & " """ & Name & """"))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub UHByCharacterID(ByVal CharacterID As Int32)
            Try
                Dim UHRuneID As UShort = Client.Objects.ID("Ultimate Healing")
                Dim SP As New ServerPacketBuilder(Proxy)
                SP.UseHotkey(UHRuneID, CharacterID)
                'Proxy.SendPacketToServer(UseHotkey(UHRuneID, CharacterID))
                'Proxy.SendPacketToClient(CreatureSpeak(Client.CharacterName, MessageType.MonsterSay, 0, "UHed player!", CharacterLoc.X, CharacterLoc.Y, CharacterLoc.Z))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Advertiser Timer "

        Public Sub AdvertiserTimerObj_OnExecute() Handles AdvertiseTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If AdvertiseMsg.Length = 0 Then Exit Sub
                Dim ChatMessage As New ChatMessageDefinition
                ChatMessage.MessageType = ITibia.MessageType.Channel
                ChatMessage.Channel = ITibia.Channel.Trade
                ChatMessage.ChannelMessageType = ITibia.ChannelMessageType.Normal
                ChatMessage.Message = AdvertiseMsg
                Kernel.ChatMessageQueueList.Add(ChatMessage)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Rainbow Outfit Timer "

        Public Sub rainbowTimerObj_Execute() Handles RainbowOutfitTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim BL As New BattleList
                If RainbowOutfitHead = 131 Then RainbowOutfitHead = 0 Else RainbowOutfitHead = RainbowOutfitHead + 1
                If RainbowOutfitBody = 131 Then RainbowOutfitBody = 0 Else RainbowOutfitBody = RainbowOutfitBody + 1
                If RainbowOutfitLegs = 131 Then RainbowOutfitLegs = 0 Else RainbowOutfitLegs = RainbowOutfitLegs + 1
                If RainbowOutfitFeet = 131 Then RainbowOutfitFeet = 0 Else RainbowOutfitFeet = RainbowOutfitFeet + 1
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                Dim SP As New ServerPacketBuilder(Proxy)
                SP.ChangeOutfit(BL.OutfitID, RainbowOutfitHead, RainbowOutfitBody, RainbowOutfitLegs, RainbowOutfitFeet, BL.OutfitAddons)
                'Proxy.SendPacketToServer(ChangeOutfit(BL.OutfitID, RainbowOutfitHead, RainbowOutfitBody, RainbowOutfitLegs, RainbowOutfitFeet, BL.OutfitAddons))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Auto Drinker Timer "

        Public Sub AutoDrinkerTimerObj_Execute() Handles AutoDrinkerTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If AutoDrinkerTimerObj.Interval > Consts.HealersCheckInterval Then AutoDrinkerTimerObj.Interval = Consts.HealersCheckInterval
                If DrinkerManaRequired = 0 Then Exit Sub
                If ManaPoints = 0 Then
                    Exit Sub
                ElseIf ManaPoints <= DrinkerManaRequired Then
                    Dim ItemID As Integer = 0
                    Dim SP As New ServerPacketBuilder(Proxy)
                    If Kernel.IsOpenTibiaServer Then
                        SP.UseHotkey(Client.Objects.ID("Vial"), CInt(Fluids.ManaOpenTibia))
                        'Proxy.SendPacketToServer(UseHotkey(Client.Objects.ID("Vial"), CInt(Fluids.ManaOpenTibia)))
                    Else
                        SP.UseHotkey(Client.Objects.ID("Vial"), CInt(Fluids.Mana))
                        'Proxy.SendPacketToServer(UseHotkey(Client.Objects.ID("Vial"), CInt(Fluids.Mana)))
                    End If
                    AutoDrinkerTimerObj.Interval = Consts.HealersAfterHealDelay
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Mana Potion Timer "

        Private Sub ManaPotionTimerObj_Execute() Handles ManaPotionTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If ManaPotionTimerObj.Interval > Consts.HealersCheckInterval Then ManaPotionTimerObj.Interval = Consts.HealersCheckInterval
                If ManaPoints = 0 Then
                    ManaPotionTimerObj.StopTimer()
                    Exit Sub
                End If
                If DrinkerManaRequired = 0 Then Exit Sub
                If ManaPoints < DrinkerManaRequired Then
                    ManaPotionTimerObj.Interval = Consts.HealersAfterHealDelay
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.UseHotkey(ManaPotionID, CharacterID)
                    'Proxy.SendPacketToServer(UseHotkey(ManaPotionID, CharacterID))
                End If
                ManaPotionTimerObj.Interval = Consts.HealersAfterHealDelay
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " CaveBot Timer "
        Public Function IsLooterReady() As Boolean
            Try
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim Container As New Container
                Dim StatusMessage As String = ""
                Dim StatusTmr As Integer = 0
                Container.Reset()
                Kernel.Client.ReadMemory(Consts.ptrStatusMessage, StatusMessage)
                Kernel.Client.ReadMemory(Consts.ptrStatusMessageTimer, StatusTmr, 4)
                Kernel.Client.ReadMemory(Consts.ptrCapacity, Capacity, 2)
                If Capacity < CInt(CavebotForm.LootMinimumCap.Value) Then Return True
                If Capacity <= LooterMinimumCapacity Then Return True
                If StatusMessage.Equals("This object is too heavy.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                If StatusMessage.Equals("You cannot put more objects in this container.") AndAlso StatusTmr <> 0 Then
                    Return True
                End If
                Do
                    If Not Container.GetName.StartsWith("Dead") And Not Container.GetName.StartsWith("Slain") And Not Container.GetName.StartsWith("Remains") And Not Container.GetName.StartsWith("Bag") Then Continue Do
                    If Not Container.HasParent Then Continue Do
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
                Dim Item As Scripting.IContainer.ContainerItemDefinition
                Dim ContainerItemCount As Integer
                Dim Container As New Container
                Dim StatusMessage As String = ""
                Dim StatusTmr As Integer = 0
                Container.Reset()
                Kernel.Client.ReadMemory(Consts.ptrStatusMessage, StatusMessage)
                Kernel.Client.ReadMemory(Consts.ptrStatusMessageTimer, StatusTmr, 4)
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
                            If Client.Objects.IsKind(Item.ID, IObjects.ObjectKind.Food) Then
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
                Dim SP As New ServerPacketBuilder(Proxy)
                If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim BL As New BattleList
                Dim MyselfBL As New BattleList
                MyselfBL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                Dim PlayerZ As Integer
                Dim CurrentContCount As Integer = 0
                Dim StatusText As String = ""
                Dim StatusTimer As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrCoordZ, PlayerZ, 4)
                Kernel.Client.ReadMemory(Consts.ptrStatusMessage, StatusText)
                Kernel.Client.ReadMemory(Consts.ptrStatusMessageTimer, StatusTimer, 4)
                If Not Client.IsConnected Then Exit Sub
                Select Case CBState
                    Case CavebotState.Walking
                        CBCreatureDied = False
                        If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) AndAlso (BL.IsOnScreen Or BL.CreaturesOnScreen) Then
                            CBState = CavebotState.Attacking
                            Kernel.Client.WriteMemory(Consts.ptrGoToX, 0, 4)
                            Kernel.Client.WriteMemory(Consts.ptrGoToY, 0, 4)
                            Kernel.Client.WriteMemory(Consts.ptrGoToZ, 0, 1)
                            BL.IsWalking = True
                            Exit Sub
                        End If
                        If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
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
                        If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                            If BL.GetHPPercentage = 0 Then
                                Kernel.CBState = CavebotState.OpeningBody
                                Exit Select
                            End If
                        Else
                            If WaitAttacker = Nothing Then
                                WaitAttacker = Date.Now.AddSeconds(5)
                                'Kernel.ConsoleWrite("Added 10 seconds to timer")
                            ElseIf Date.Now > WaitAttacker Then
                                Kernel.CBState = CavebotState.Walking
                                WaitAttacker = Nothing
                                'Kernel.ConsoleWrite("Out of time, Attacker -> Walker")
                            End If
                        End If
                        If Not BL.CreaturesOnScreen Then
                            'Core.ConsoleWrite("Attacking -> Walking")
                            SP.StopEverything()
                            'Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
                            CBState = CavebotState.Walking
                        End If
                        Dim Chase As Integer
                        Kernel.Client.ReadMemory(Consts.ptrChasingMode, Chase, 1)
                        If Chase <> 1 Then
                            Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                            SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                            'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                        End If
                        If BL.GetHPPercentage < 30 And BL.IsWalking = True And MyselfBL.IsWalking = False And BL.GetDistance > 4 Then
                            'Core.ConsoleWrite("PRESS STOP YOU IDIOT. CHAR IS PROBABLY STANDING")
                            SP.StopEverything()
                            'Core.Proxy.SendPacketToServer(PacketUtils.StopEverything)
                            System.Threading.Thread.Sleep(1000)
                            CBState = CavebotState.Walking
                        End If
                        'Changing CBstate.OpeningBody in the proxy section.
                    Case CavebotState.OpeningBody
                        CBCreatureDied = False
                        If IsOpeningReady = True Then
                            Static Cont As New Container
                            CurrentContCount = Cont.ContainerCount
                            If CurrentContCount > CBContainerCount Then
                                WaitAttacker = Nothing
                                CBState = CavebotState.Looting ' : Core.ConsoleWrite("Looting state ->")
                            Else
                                If Date.Now > WaitTime Then

                                    'Core.ConsoleWrite("Running out of time, Walking ->")
                                    WaitAttacker = Nothing
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
                If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    If CBState <> CavebotState.Walking OrElse Walker_Waypoints(WaypointIndex).Type = Walker.WaypointType.Wait Then Exit Sub
                End If
                If TTBState = BotState.Paused Then Exit Sub
                AttackerMonsters.Clear()
                Dim sp As New ServerPacketBuilder(Proxy)
                If Consts.SmartAttacker Then 'If Using Smart Attack (whole time attack the nearest creature)
                    BL.Reset()
                    Do
                        If BL.IsMyself Then Continue Do
                        If Not BL.IsPlayer AndAlso BL.IsOnScreen AndAlso BL.GetLocation.Z = Kernel.CharacterLoc.Z Then
                            If BL.GetDistance < Consts.CavebotAttackerRadius Then
                                If CheckRadius(BL.GetEntityID) = True Then
                                    If AutoAttackerListEnabled Then
                                        If Not AutoAttackerList.Contains(BL.GetName.ToLower) Then Exit Sub
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
                    If AttackBL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                        If BL.GetEntityID.Equals(AttackerMonsters.GetByIndex(0)) Then Exit Sub
                    End If
                    If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                        CBState = CavebotState.None
                        'Core.ConsoleWrite("AttackerTimer: STOP!")
                        sp.StopEverything()
                        'Proxy.SendPacketToServer(PacketUtils.StopEverything)
                        Client.WriteMemory(Consts.ptrGoToX, 0, 4)
                        Client.WriteMemory(Consts.ptrGoToY, 0, 4)
                        Client.WriteMemory(Consts.ptrGoToZ, 0, 1)
                        System.Threading.Thread.Sleep(1000)
                    End If
                    If FavoredWeaponEnabled Then
                        Dim CID As Integer 'CID = Creature ID
                        Dim CN As String 'CN = Creature Nam
                        Dim cHand As Integer
                        Dim Retries As Short
                        Dim CT As New Container
                        Dim FoundWep As Boolean = False
                        Dim FavWepCT As Scripting.IContainer.ContainerItemDefinition
                        Dim SlotFromTo As ITibia.InventorySlots
                        Dim SlotShield As ITibia.InventorySlots
                        CT.Reset()
                        CID = AttackerMonsters.GetByIndex(0)
                        If BL.Find(CID, False) Then
                            CN = BL.GetName.ToLower
                            For Each FW As WeaponFavoritDefinition In FavoredWeapon
                                If FW.Monster = CN Then
                                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.LeftHand - 1) * Consts.ItemDist), cHand, 2)
                                    If cHand = FW.WeaponID Then GoTo ContinueAttack
                                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), cHand, 2)
                                    If cHand = FW.WeaponID Then GoTo ContinueAttack
                                    Do
                                        For i As Integer = 0 To CT.GetItemCount - 1
                                            If CT.Items(i).ID = FW.WeaponID Then
                                                FoundWep = True
                                                Exit Do
                                            End If
                                        Next
                                    Loop While CT.NextContainer
                                    If Not FoundWep Then
                                        ConsoleError("Can't find the favored weapon for: " & FW.Monster)
                                        GoTo ContinueAttack
                                    End If
                                    SlotFromTo = ITibia.InventorySlots.LeftHand
BackFWHand:
                                    Select Case FW.Hand
                                        Case 1 'left
                                            SlotFromTo = ITibia.InventorySlots.LeftHand
                                            SlotShield = ITibia.InventorySlots.RightHand
                                        Case 2 'right
                                            SlotFromTo = ITibia.InventorySlots.RightHand
                                            SlotShield = ITibia.InventorySlots.LeftHand
                                        Case 3 'twohanded
                                            If SlotFromTo = ITibia.InventorySlots.LeftHand Then
                                                SlotFromTo = ITibia.InventorySlots.RightHand
                                            Else
                                                SlotFromTo = ITibia.InventorySlots.LeftHand
                                            End If
                                            SlotShield = ITibia.InventorySlots.Armor 'WOW
                                    End Select
                                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotFromTo - 1) * Consts.ItemDist), cHand, 2)
                                    If cHand > 0 Then
                                        Retries = 0
                                        Do
                                            Retries += 1
                                            If Retries > 20 Then
                                                ConsoleError("Can't move hands for Fav Weapon.") 'Remove actually
                                                GoTo ContinueAttack
                                                Exit Sub
                                            End If
                                            sp.MoveObject(cHand, GetInventorySlotAsLocation(SlotFromTo), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), 100)
                                            System.Threading.Thread.Sleep(1000)
                                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotFromTo - 1) * Consts.ItemDist), cHand, 2)
                                        Loop While cHand <> 0
                                    End If
                                    If SlotShield <> ITibia.InventorySlots.Armor Then
                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotShield - 1) * Consts.ItemDist), cHand, 2)
                                        If cHand <> FavoredWeaponShield Then
                                            Retries = 0
                                            Do
                                                Retries += 1
                                                If Retries > 20 Then
                                                    ConsoleError("Can't move hands for Fav Weapon.") 'Remove actually
                                                    GoTo ContinueAttack
                                                    Exit Sub
                                                End If
                                                sp.MoveObject(cHand, GetInventorySlotAsLocation(SlotShield), GetInventorySlotAsLocation(ITibia.InventorySlots.Backpack), 100)
                                                System.Threading.Thread.Sleep(1000)
                                                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotShield - 1) * Consts.ItemDist), cHand, 2)
                                            Loop While cHand <> 0
                                        End If
                                    End If
                                    If FW.Hand = 3 Then
                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((Scripting.ITibia.InventorySlots.LeftHand - 1) * Consts.ItemDist), cHand, 2)
                                        If cHand > 0 Then
                                            SlotFromTo = ITibia.InventorySlots.RightHand
                                            GoTo BackFWHand
                                        End If
                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((Scripting.ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), cHand, 2)
                                        If cHand > 0 Then
                                            SlotFromTo = ITibia.InventorySlots.LeftHand
                                            GoTo BackFWHand
                                        End If
                                    End If
                                    CT.Reset()
                                    Do
                                        For i As Integer = 0 To CT.GetItemCount - 1
                                            If CT.Items(i).ID = FW.WeaponID Then
                                                FoundWep = True
                                                FavWepCT = CT.Items(i)
                                                Exit Do
                                            End If
                                        Next
                                    Loop While CT.NextContainer
                                    Retries = 0
                                    Do
                                        Retries += 1
                                        If Retries > 20 Then
                                            ConsoleError("Can't move fav weapon to inventory.")
                                            GoTo ContinueAttack
                                        End If
                                        sp.MoveObject(FavWepCT, GetInventorySlotAsLocation(SlotFromTo))
                                        System.Threading.Thread.Sleep(1000)
                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotFromTo - 1) * Consts.ItemDist), cHand, 2)
                                    Loop While cHand = 0
                                    If FavoredWeaponShield = 0 Then GoTo ContinueAttack
                                    Select Case FW.Hand
                                        Case 1 'left
                                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((Scripting.ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), cHand, 2)
                                            If cHand <> FavoredWeaponShield Then
                                                SlotFromTo = ITibia.InventorySlots.RightHand
                                                If cHand = 0 Then
                                                    FoundWep = False
                                                    CT.Reset()
                                                    Do
                                                        For i As Integer = 0 To CT.GetItemCount - 1
                                                            If CT.Items(i).ID = FavoredWeaponShield Then
                                                                FoundWep = True
                                                                FavWepCT = CT.Items(i)
                                                                Exit Do
                                                            End If
                                                        Next
                                                    Loop While CT.NextContainer
                                                    If Not FoundWep Then
                                                        ConsoleError("Can't find you shield!")
                                                    End If
                                                    Do
                                                        Retries += 1
                                                        If Retries > 20 Then
                                                            ConsoleError("Can't move shield to inventory.")
                                                            GoTo ContinueAttack
                                                            Exit Sub
                                                        End If
                                                        sp.MoveObject(FavWepCT, GetInventorySlotAsLocation(SlotFromTo))
                                                        System.Threading.Thread.Sleep(1000)
                                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotFromTo - 1) * Consts.ItemDist), cHand, 2)
                                                    Loop While cHand = 0
                                                End If
                                            End If
                                        Case 2 'right
                                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((Scripting.ITibia.InventorySlots.LeftHand - 1) * Consts.ItemDist), cHand, 2)
                                            If cHand <> FavoredWeaponShield Then
                                                SlotFromTo = ITibia.InventorySlots.LeftHand
                                                If cHand = 0 Then
                                                    FoundWep = False
                                                    CT.Reset()
                                                    Do
                                                        For i As Integer = 0 To CT.GetItemCount - 1
                                                            If CT.Items(i).ID = FavoredWeaponShield Then
                                                                FoundWep = True
                                                                FavWepCT = CT.Items(i)
                                                                Exit Do
                                                            End If
                                                        Next
                                                    Loop While CT.NextContainer
                                                    If Not FoundWep Then
                                                        ConsoleError("Can't find you shield!")
                                                    End If
                                                    Do
                                                        Retries += 1
                                                        If Retries > 20 Then
                                                            ConsoleError("Can't move shield to inventory.")
                                                            GoTo ContinueAttack
                                                            Exit Sub
                                                        End If
                                                        sp.MoveObject(FavWepCT, GetInventorySlotAsLocation(SlotFromTo))
                                                        System.Threading.Thread.Sleep(1000)
                                                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotFromTo - 1) * Consts.ItemDist), cHand, 2)
                                                    Loop While cHand = 0
                                                End If
                                            End If
                                    End Select
                                End If
                            Next
                        End If
                    End If
ContinueAttack:
                    Client.WriteMemory(Consts.ptrAttackedEntityID, AttackerMonsters.GetByIndex(0), 4)
                    sp.AttackEntity(AttackerMonsters.GetByIndex(0))
                    'Proxy.SendPacketToServer(AttackEntity(AttackerMonsters.GetByIndex(0)))
                    If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then CBState = CavebotState.Attacking
                    System.Threading.Thread.Sleep(2000)
                    Exit Sub
                Else
                    If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then Exit Sub
                    'We are not attacking, so..
                    BL.Reset()
                    'Looping trough battlelist
                    Do
                        If BL.IsMyself Then Continue Do
                        If Not BL.IsPlayer AndAlso BL.IsOnScreen AndAlso BL.GetLocation.Z = Kernel.CharacterLoc.Z Then
                            If BL.GetDistance < Consts.CavebotAttackerRadius Then
                                If CheckRadius(BL.GetEntityID) = True Then
                                    If AutoAttackerListEnabled Then
                                        If Not AutoAttackerList.Contains(BL.GetName.ToLower) Then Exit Sub
                                    End If
                                    If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                                        CBState = CavebotState.None
                                        'Core.ConsoleWrite("AttackerTimer: STOP!")
                                        sp.StopEverything()
                                        'Proxy.SendPacketToServer(PacketUtils.StopEverything)
                                        Client.WriteMemory(Consts.ptrGoToX, 0, 4)
                                        Client.WriteMemory(Consts.ptrGoToY, 0, 4)
                                        Client.WriteMemory(Consts.ptrGoToZ, 0, 1)
                                        System.Threading.Thread.Sleep(1000)
                                    End If
                                    Client.WriteMemory(Consts.ptrAttackedEntityID, BL.GetEntityID, 4)
                                    sp.AttackEntity(BL.GetEntityID)
                                    'Proxy.SendPacketToServer(AttackEntity(BL.GetEntityID))
                                    If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then CBState = CavebotState.Attacking
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

                MyBL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
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

                ConsoleWrite("Welcome " & Client.CharacterName & "!" & Ret & _
                "Don't forget to visit us at: www.tibiatek.com." & Ret & _
                "Please report any bug you may found!" & Ret & _
                "For a list of available commands type: &help.")

                Dim Reader As IO.StreamReader
                'Dim ResetMagicWalls As Integer
                'For ResetMagicWalls = LBound(MagicWall) To UBound(MagicWall)
                '    MagicWall(ResetMagicWalls).Enabled = False
                'Next
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
                    If BGWConnectIrc.IsBusy Then
                        BGWConnectIrc.CancelAsync()
                        System.Threading.Thread.Sleep(500)
                    End If
                    BGWConnectIrc.RunWorkerAsync()
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
                    Kernel.CommandParser.Invoke(GroupMatch.Groups(1).Value)
                Next
                Reader.Close()
                ConsoleWrite("Configuration loaded.")
            Catch ex As System.IO.IOException
                ConsoleError("Unable to load your configuration.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Walker Timer "
        Public Sub WalkerTimerObj_Execute() Handles WalkerTimerObj.OnExecute
            Try
                If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                    Kernel.ConsoleError("You can't use Walker same time as Cavebot")
                    WalkerTimerObj.StopTimer()
                    Exit Sub
                End If
                If TTBState = BotState.Paused Then Exit Sub
                If Walker_Waypoints(WaypointIndex).MoveChar() Then
                    WaypointIndex += 1
                End If

                If WaypointIndex = Walker_Waypoints.Count Then
                    If WalkerLoop Then
                        WaypointIndex = 0
                    Else
                        Kernel.ConsoleWrite("Arrived to Destination.")
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
        Public Function AddWaypointLocation(ByVal FloorChange As Integer, ByVal CurrentZ As Integer) As ITibia.LocationDefinition
            'FloorChange: -1 up to down | +1 down to up
            Try
                Dim TileObjects() As IMapTiles.TileObject
                Dim BL As New BattleList
                Dim TileObject As IMapTiles.TileObject
                Dim ReturnLocations As New SortedList '(Of Integer, ITibia.LocationDefinition)
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                Kernel.ConsoleWrite("Current Z + FloorChange: " & CurrentZ + FloorChange)
                Dim TileObjectID As Integer = 0
                For XAxis As Integer = 1 To 15
                    For YAxis As Integer = 1 To 11
                        TileObjects = Client.MapTiles.GetTileObjects(XAxis, YAxis, Client.MapTiles.WorldZToClientZ(CurrentZ + FloorChange))

                        For Each TileObject In TileObjects
                            TileObjectID = TileObject.GetObjectID

                            Dim CPB As New ClientPacketBuilder(Kernel.Proxy)
                            CPB.AnimatedText(Scripting.ITibia.TextColors.Orange, TileObject.GetMapLocation, "^")
                            If Client.Objects.HasFlags(TileObjectID, IObjects.ObjectFlags.CausesFloorChange) _
                            OrElse Client.Objects.LensHelp(TileObjectID) = IObjects.ObjectLensHelp.Hole _
                            OrElse Client.Objects.LensHelp(TileObjectID) = IObjects.ObjectLensHelp.Stairs _
                            OrElse Client.Objects.HasFlags(TileObjectID, IObjects.ObjectFlags.BlocksPath Or IObjects.ObjectFlags.HasHeight Or IObjects.ObjectFlags.IsImmovable Or IObjects.ObjectFlags.TopOrder2) Then
                                'Kernel.ConsoleWrite("Current Z + FloorChange: " & CurrentZ + FloorChange)
                                Select Case FloorChange
                                    Case 1 '/\
                                        'Kernel.ConsoleWrite("Adding waypoint UP")
                                        Dim ReturnLocation As ITibia.LocationDefinition
                                        If CurrentZ = 7 Then 'You can't read floor 8 from floor 7
                                            ReturnLocation = BL.GetLocation
                                            ReturnLocation.Z += 1
                                            Select Case BL.GetDirection
                                                Case IBattlelist.Directions.Down
                                                    ReturnLocation.Y -= 1
                                                Case IBattlelist.Directions.Left
                                                    ReturnLocation.X += 1
                                                Case IBattlelist.Directions.Right
                                                    ReturnLocation.X -= 1
                                                Case IBattlelist.Directions.Up
                                                    ReturnLocation.Y += 1
                                            End Select
                                        Else
                                            ReturnLocation = TileObject.GetMapLocation
                                            ReturnLocation.X -= 1
                                            ReturnLocation.Y -= 1
                                        End If
                                        'Dim CPB As New ClientPacketBuilder(Kernel.Proxy)
                                        CPB.AnimatedText(Scripting.ITibia.TextColors.Crystal, ReturnLocation, "HERE")
                                        'Kernel.ConsoleWrite(ReturnLocation.X & ":" & ReturnLocation.Y & ":" & ReturnLocation.Z)
                                        If Not ReturnLocations.ContainsKey(Abs(BL.GetDistanceFromLocation(ReturnLocation, True))) Then
                                            ReturnLocations.Add(Abs(BL.GetDistanceFromLocation(ReturnLocation, True)), ReturnLocation)
                                        End If
                                    Case -1 '\/
                                        'Kernel.ConsoleWrite("Adding waypoint DOWN")
                                        Dim Returnlocation As ITibia.LocationDefinition = TileObject.GetMapLocation
                                        Returnlocation.Z = CurrentZ + FloorChange
                                        If Not Returnlocation.Z = 7 Then
                                            Returnlocation.X += 1
                                            Returnlocation.Y += 1
                                        End If
                                        'Kernel.ConsoleWrite("Return Location Z: " & Returnlocation.Z)
                                        'Kernel.ConsoleWrite("----------------")
                                        If Not ReturnLocations.ContainsKey(Abs(BL.GetDistanceFromLocation(Returnlocation, True))) Then
                                            ReturnLocations.Add(Abs(BL.GetDistanceFromLocation(Returnlocation, True)), Returnlocation)
                                        End If
                                End Select
                            End If

                        Next
                    Next
                Next
                If ReturnLocations.Count > 0 Then
                    Return ReturnLocations.GetByIndex(0)
                Else
                    Return New ITibia.LocationDefinition(0, 0, 0) 'Error
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public Sub AutoAddTimerObj_Execute() Handles AutoAddTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                Dim BL As New BattleList
                Dim TD As New TileData
                Dim CameLoc As New ITibia.LocationDefinition
                Dim WaypointLoc As New ITibia.LocationDefinition
                Dim Tries As Integer = 0
                Dim CameDir As New Integer   '0 = From Down to Up, 1 = From Up to Down
                'Add normal Walking time every 10s
                If Walker_Waypoints.Count <> 0 Then 'List is not empty
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    If BL.GetLocation.Z <> LastFloor Then
                        Do Until Tries > 3 '4 Tries to succeed
                            Tries += 1
                            If BL.GetLocation.Z < LastFloor Then CameDir = 1
                            If BL.GetLocation.Z > LastFloor Then CameDir = -1
                            LastFloor = BL.GetLocation.Z
                            If Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Ladder And _
                            Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Rope And _
                            Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Sewer And _
                            Not Walker_Waypoints(Walker_Waypoints.Count - 1).Type = Walker.WaypointType.Shovel Then
                                WaypointLoc = AddWaypointLocation(CameDir, BL.GetLocation.Z)
                                If WaypointLoc.X <> 0 Then 'There were no error
                                    Dim WalkerChar As New Walker
                                    WalkerChar.Coordinates = WaypointLoc
                                    WalkerChar.Type = Walker.WaypointType.StairsOrHole
                                    WalkerChar.Info = ""
                                    Walker_Waypoints.Add(WalkerChar)
                                    AutoAddTime = Now.AddSeconds(5) 'Don't add next right away
                                    Kernel.ConsoleWrite("Stairs/Hole waypoint added.")
                                    Exit Do
                                Else
                                    Kernel.ConsoleError("Error, can't add waypoint. Try #" & Tries & ". Please wait..")
                                    If Tries > 3 Then
                                        Kernel.ConsoleWrite("Couldn't add waypoint. This might affect to cavebot." & Ret & "Check TibiaTek Wiki -> Cavebot for more info.")
                                        LastFloor = BL.GetLocation.Z
                                        AutoAddTime = Now.AddSeconds(5)
                                        Exit Do
                                    End If
                                    LastFloor = LastFloor + CameDir
                                    Threading.Thread.Sleep(2000) 'Wait for 2s before next try
                                End If
                            End If
                        Loop
                    End If
                End If
                If AutoAddTime < Date.Now Then
                    Dim WalkerChar As New Walker
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    UpdatePlayerPos()
                    If Walker_Waypoints.Count = 0 Then
                        WalkerChar.Coordinates = BL.GetLocation
                        WalkerChar.Type = Walker.WaypointType.Walk
                        WalkerChar.Info = ""
                        Walker_Waypoints.Add(WalkerChar)
                        Kernel.ConsoleWrite("Walking waypoint added.")
                        AutoAddTime = Now.AddSeconds(10)
                        Exit Sub
                    End If
                    If Walker_Waypoints(Walker_Waypoints.Count - 1).Coordinates.X <> Kernel.CharacterLoc.X Or _
                       Walker_Waypoints(Walker_Waypoints.Count - 1).Coordinates.Y <> Kernel.CharacterLoc.Y Then
                        WalkerChar.Coordinates = BL.GetLocation
                        WalkerChar.Type = Walker.WaypointType.Walk
                        WalkerChar.Info = ""
                        Walker_Waypoints.Add(WalkerChar)
                        AutoAddTime = Now.AddSeconds(10)
                        Kernel.ConsoleWrite("Walking waypoint added.")
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
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim Cont As New Container
                Dim Amulet As New Scripting.IContainer.ContainerItemDefinition
                Dim NeckSlot As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Neck - 1) * Consts.ItemDist), NeckSlot, 2)
                If NeckSlot = 0 Then 'No amulet, let's change there something :)
                    If Not (New Container).FindItem(Amulet, AmuletID, 0, 0, Consts.MaxContainers - 1) Then
                        Exit Sub
                    End If
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.MoveObject(Amulet, GetInventorySlotAsLocation(ITibia.InventorySlots.Neck), 1)
                    'Core.Proxy.SendPacketToServer(PacketUtils.MoveObject(Amulet, GetInventorySlotAsLocation(InventorySlots.Neck), 1))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Ring Changer "

        Private Sub RingChangerTimerObj_Execute() Handles RingChangerTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim Cont As New Container
                Dim Ring As New Scripting.IContainer.ContainerItemDefinition
                Dim FingerSlot As Integer = 0
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Finger - 1) * Consts.ItemDist), FingerSlot, 2)
                If FingerSlot = 0 Then 'No amulet, let's change there something :)
                    If Not (New Container).FindItem(Ring, RingID, 0, 0, Consts.MaxContainers - 1) Then
                        Exit Sub
                    End If
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.MoveObject(Ring, GetInventorySlotAsLocation(ITibia.InventorySlots.Finger), 1)
                    'Core.Proxy.SendPacketToServer(PacketUtils.MoveObject(Ring, GetInventorySlotAsLocation(InventorySlots.Finger), 1))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Ammo Maker Timer "
        Private Sub AmmoMakerTimerObj_Execute() Handles AmmoMakerTimerObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If Capacity < AmmoMakerMinCap Then Exit Sub
                If ManaPoints < AmmoMakerMinMana OrElse ManaPoints < AmmoMakerSpell.ManaPoints Then Exit Sub
                Dim SP As New ServerPacketBuilder(Proxy)
                Select Case AmmoMakerSpell.Kind
                    Case ISpells.SpellKind.Ammunition
                        SP.Speak(AmmoMakerSpell.Words)
                        'Core.Proxy.SendPacketToServer(PacketUtils.Speak(AmmoMakerSpell.Words))
                    Case ISpells.SpellKind.Incantation
                        Dim SlotToUse As New ITibia.InventorySlots
                        Dim LeftHandSlot As Integer = 0
                        Dim RightHandSlot As Integer = 0
                        Dim Spear As New Scripting.IContainer.ContainerItemDefinition
                        Dim Retries As Integer = 0
                        Dim TempSlot As Integer = 0

                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.LeftHand - 1) * Consts.ItemDist), LeftHandSlot, 2)
                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                        If LeftHandSlot = 0 Then
                            SlotToUse = ITibia.InventorySlots.LeftHand
                        ElseIf RightHandSlot = 0 Then
                            SlotToUse = ITibia.InventorySlots.RightHand
                        Else
                            Kernel.ConsoleError("Both hands are already in use. Please keep one hand free so Ammo Maker can move the Spear there. Ammo Maker is now stopped.")
                            AmmoMakerMinMana = 0
                            AmmoMakerMinCap = 0
                            AmmoMakerTimerObj.StopTimer()
                            Exit Sub
                        End If

                        If Not (New Container).FindItem(Spear, Client.Objects.ID("Spear")) Then
                            Kernel.ConsoleError("Ammo Maker couldn't find a spear. Ammo Maker is now stopped.")
                            AmmoMakerMinMana = 0
                            AmmoMakerMinCap = 0
                            AmmoMakerTimerObj.StopTimer()
                            Exit Sub
                        End If
                        'Moving spear to hand
                        Retries = 0
                        TempSlot = 0
                        Do
                            Retries += 1
                            If Retries > 20 Then
                                AmmoMakerMinMana = 0
                                AmmoMakerMinCap = 0
                                AmmoMakerTimerObj.StopTimer()
                                Kernel.ConsoleError("Ammo Maker is stuck. Can't move Spear from Backpack to Hand. Ammo Maker is now stopped.")
                                Exit Sub
                            End If
                            SP.MoveObject(Spear.ID, Spear.Location, GetInventorySlotAsLocation(SlotToUse), 1)
                            'Core.Proxy.SendPacketToServer(PacketUtils.MoveObject(Spear.ID, Spear.Location, GetInventorySlotAsLocation(SlotToUse), 1))
                            System.Threading.Thread.Sleep(1000)
                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotToUse - 1) * Consts.ItemDist), TempSlot, 2)
                        Loop Until TempSlot = Client.Objects.ID("Spear")

                        'Casting the spell
                        Retries = 0
                        TempSlot = 0
                        Do
                            Retries += 1
                            If Retries > 20 Then
                                AmmoMakerMinMana = 0
                                AmmoMakerMinCap = 0
                                AmmoMakerTimerObj.StopTimer()
                                Kernel.ConsoleError("Ammo Maker couldn't cast the conjure spell. Ammo Maker is now stopped.")
                                Exit Sub
                            End If
                            SP.Speak(AmmoMakerSpell.Words)
                            'Core.Proxy.SendPacketToServer(PacketUtils.Speak(AmmoMakerSpell.Words))
                            System.Threading.Thread.Sleep(1000)
                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotToUse - 1) * Consts.ItemDist), TempSlot, 2)
                        Loop While TempSlot = Client.Objects.ID("Spear")

                        'Moving spear back to the backpack
                        Retries = 0
                        TempSlot = 0
                        Dim EnchantedSpearId As Integer = 0
                        Dim MoveToSlot As New Scripting.IContainer.ContainerItemDefinition
                        Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotToUse - 1) * Consts.ItemDist), EnchantedSpearId, 2)
                        If Not (New Container).FindItem(MoveToSlot, EnchantedSpearId) Then 'Testing if theres already enchanted spears
                            MoveToSlot = Spear
                        End If
                        Do
                            Retries += 1
                            If Retries > 20 Then
                                AmmoMakerMinMana = 0
                                AmmoMakerMinCap = 0
                                AmmoMakerTimerObj.StopTimer()
                                Kernel.ConsoleError("Ammo Maker is stuck, couldn't move spear from hand to backpack. Ammo Maker is now stopped.")
                                Exit Sub
                            End If
                            SP.MoveObject(EnchantedSpearId, GetInventorySlotAsLocation(SlotToUse), MoveToSlot.Location, 1)
                            'Core.Proxy.SendPacketToServer(MoveObject(EnchantedSpearId, GetInventorySlotAsLocation(SlotToUse), MoveToSlot.Location, 1))
                            System.Threading.Thread.Sleep(1000)
                            Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((SlotToUse - 1) * Consts.ItemDist), TempSlot, 2)
                        Loop Until TempSlot = 0
                    Case Else
                        Kernel.ConsoleError("The Spell cannot be used to create or enchant ammunation. Ammo Maker is now stopped.")
                        AmmoMakerTimerObj.StopTimer()
                        AmmoMakerMinMana = 0
                        AmmoMakerMinCap = 0
                        Exit Sub
                End Select
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Anti-Logout Timer "

        Public Sub AntiLogoutObj_Execute() Handles AntiLogoutObj.OnExecute
            Try
                If Not Client.IsConnected Then Exit Sub
                Dim IdleTime As TimeSpan = Date.Now.Subtract(LastActivity)
                If IdleTime.TotalMinutes < 13 Then Exit Sub
                Dim BL As New BattleList
                Dim MyLastDirection As Integer
                Dim RandNum As New Random(Date.Now.Millisecond)
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                MyLastDirection = BL.GetDirection
                Dim SPB As New ServerPacketBuilder(Proxy)
                Select Case BL.GetDirection
                    Case IBattlelist.Directions.Up
                        SPB.CharacterTurn(IBattlelist.Directions.Down)
                        'Proxy.SendPacketToServer(CharacterTurn(IBattlelist.Directions.Down))
                    Case IBattlelist.Directions.Down
                        SPB.CharacterTurn(IBattlelist.Directions.Up)
                        'Proxy.SendPacketToServer(CharacterTurn(IBattlelist.Directions.Up))
                    Case IBattlelist.Directions.Right
                        SPB.CharacterTurn(IBattlelist.Directions.Left)
                        'Proxy.SendPacketToServer(CharacterTurn(IBattlelist.Directions.Left))
                    Case IBattlelist.Directions.Left
                        SPB.CharacterTurn(IBattlelist.Directions.Right)
                        'Proxy.SendPacketToServer(CharacterTurn(IBattlelist.Directions.Right))
                End Select

                System.Threading.Thread.Sleep(RandNum.Next(2000, 5001))
                SPB.Send()
                'Proxy.SendPacketToServer(CharacterTurn(MyLastDirection))
                LastActivity = Date.Now
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " TibiaTek Messages Timer "

        Private Sub TTMessagesTimerObj_OnExecute() Handles TTMessagesTimerObj.OnExecute
            Try
                Dim WClient As New WebClient
                WClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Dim Response As String = WClient.UploadString(BotWebsite & "/messages.php", "POST", "name=" & Web.HttpUtility.UrlEncode(Client.CharacterName) & "&world=" & Web.HttpUtility.UrlEncode(Client.CharacterWorld))
                If Not String.IsNullOrEmpty(Response) Then
                    If System.Int32.TryParse(Response, TTMessages) AndAlso TTMessages > 0 Then
                        Dim CP As New ClientPacketBuilder(Proxy)
                        CP.SystemMessage(SysMessageType.StatusWarning, "You have received a message from the TibiaTek Development team. Type &viewmsg to read it.")
                        'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusWarning, "You have received a message from the TibiaTek Development team. Type &viewmsg to read it."))
                    End If
                End If
            Catch
            End Try
        End Sub

#End Region

#Region " MagicWall Timer "
        Private Sub MagicWallTimerObj_OnExecute() Handles MagicWallTimerObj.OnExecute
            Try
                MagicWallTimerObj.StartTimer()
                Static TimePassed As TimeSpan
                If Not Client.IsConnected Then MagicWallTimerObj.StopTimer()
                Dim Count As Integer = MagicWalls.Count
                For I As Integer = 0 To Count - 1
                    TimePassed = Date.Now.Subtract(MagicWalls(I).LastMagicWallDate)
                    If TimePassed.TotalSeconds > 25 Then
                        Dim NewMW As New MagicWallDefinition
                        NewMW = MagicWalls(I)
                        NewMW.Enabled = False
                        MagicWalls(I) = NewMW
                        Continue For
                    End If
                    If MagicWalls(I).Stage = 0 Then
                        If Int(TimePassed.TotalSeconds) >= 20 Then
                            Dim ClientPacket As New ClientPacketBuilder(Proxy)
                            ClientPacket.AnimatedText(&HD2, MagicWalls(I).Position, "Puff!")
                            ClientPacket.Send()
                            'Proxy.SendPacketToClient(AnimatedText(&HD2, MagicWalls(I).Position, "Puff!"))
                            Dim NewMW As New MagicWallDefinition
                            NewMW = MagicWalls(I)
                            NewMW.Enabled = False
                            MagicWalls(I) = NewMW
                            Continue For
                        End If
                    Else
                        If Int(TimePassed.TotalSeconds) = (20 - MagicWalls(I).Stage) Then
                            Static PrintColor As ITibia.TextColors = ITibia.TextColors.Gold
                            Static BL As New BattleList
                            If BL.GetDistanceFromLocation(MagicWalls(I).Position, False) >= 9 Then Exit Sub
                            Select Case MagicWalls(I).Stage
                                Case 0
                                    PrintColor = ITibia.TextColors.Gold 'Gold
                                Case 1 To 5
                                    PrintColor = ITibia.TextColors.Red 'Red
                                Case 6 To 10
                                    PrintColor = ITibia.TextColors.Orange 'Orage
                                Case 11 To 20
                                    PrintColor = ITibia.TextColors.LightGreen 'Green
                            End Select
                            Dim ClientPacket As New ClientPacketBuilder(Proxy)
                            ClientPacket.AnimatedText(PrintColor, MagicWalls(I).Position, MagicWalls(I).Stage & "s")
                            ClientPacket.Send()
                            'Proxy.SendPacketToClient(AnimatedText(PrintColor, MagicWalls(I).Position, MagicWalls(I).Stage & "s"))
                            Dim NewMW As New MagicWallDefinition
                            NewMW = MagicWalls(I)
                            NewMW.Stage -= 1
                            MagicWalls(I) = NewMW
                        End If
                    End If
                Next
                Static Repeat As Boolean = False
                Do
                    Repeat = False
                    For Each MagicWall As MagicWallDefinition In MagicWalls
                        If Not MagicWall.Enabled Then
                            MagicWalls.Remove(MagicWall)
                            Repeat = True
                            Exit For
                        End If
                    Next
                Loop While Repeat
                If MagicWalls.Count = 0 Then MagicWallTimerObj.StopTimer()
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub MagicWallAdd(ByVal Position As ITibia.LocationDefinition)
            Dim NewMW As MagicWallDefinition
            NewMW.LastMagicWallDate = Date.Now
            NewMW.Position = Position
            NewMW.Stage = 19
            NewMW.Enabled = True
            MagicWalls.Add(NewMW)
            Dim ClientPacket As New ClientPacketBuilder(Proxy)
            ClientPacket.AnimatedText(ITibia.TextColors.Green, Position, "20s")
            ClientPacket.Send()
            'Proxy.SendPacketToClient(AnimatedText(ITibia.TextColors.Green, Position, "20s"))
            If MagicWallTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then
                MagicWallTimerObj.StartTimer()
            End If
        End Sub
#End Region

#Region " Dancer Timer "
        Private Sub DancerTimerObj_OnExecute() Handles DancerTimerObj.OnExecute
            Try
                If Not Client.IsConnected() Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                Dim RandomNumber As New Random(Date.Now.Millisecond)
                Dim BL As New BattleList(IBattlelist.SpecialEntity.Myself)
                Dim Direction As IBattlelist.Directions
                'Core.ConsoleWrite("going dancing")
                Dim Packet As New ServerPacketBuilder(Proxy)
                Do
                    Direction = RandomNumber.Next(0, 4)
                Loop While Direction = BL.GetDirection
                Packet.CharacterTurn(Direction)
                Packet.Send()
                'Core.Proxy.SendPacketToServer(CharacterTurn(Direction))
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Kernel.ConsoleError("Unkown Error occured during Dancer feature. Dancer is now disabled.")
                DancerTimerObj.StopTimer()
            End Try
        End Sub

#End Region

#Region " Auto HealPotion Timer "

        Private Sub PotionTimerObj_Execute() Handles PotionTimerObj.OnExecute
            Try
                If Not Client.IsConnected() Then Exit Sub
                If TTBState = BotState.Paused Then Exit Sub
                If PotionTimerObj.Interval > Consts.HealersCheckInterval Then PotionTimerObj.Interval = Consts.HealersCheckInterval
                If PotionHPRequired = 0 Then
                    PotionTimerObj.StopTimer()
                    Exit Sub
                End If
                If HitPoints > PotionHPRequired Then Exit Sub
                PotionTimerObj.Interval = Consts.HealersAfterHealDelay
                Dim SP As New ServerPacketBuilder(Proxy)
                SP.UseHotkey(PotionID, CharacterID)
                'Proxy.SendPacketToServer(UseHotkey(PotionID, CharacterID))
                'Proxy.SendPacketToClient(CreatureSpeak(Client.CharacterName, MessageType.MonsterSay, 0, "Uh!", CharacterLoc.X, CharacterLoc.Y, CharacterLoc.Z))
                Log("Auto Potioner", "Used Potion on yourself.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Rename Backpack Timer "

        Public Sub RewriteBPNames() 'Used when user closes the container
            Try
                'If LooterTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then Exit Sub
                Dim BP As New Container
                BP.Reset()
                Do
                    'If Not BP.GetName.EndsWith("]") Then
                    Select Case BP.GetName.ToLower
                        Case "backpack"
                            SetContainerName("Backpack " & "[" & BP.GetContainerIndex + 1 & "]", BP.GetContainerIndex)
                        Case "bag"
                            SetContainerName("Bag " & "[" & BP.GetContainerIndex + 1 & "]", BP.GetContainerIndex)
                    End Select
                    'End If
                Loop While BP.NextContainer
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub RenameBackpackObj_OnExecute() Handles RenameBackpackObj.OnExecute
            Try
                'If LooterTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then Exit Sub
                Dim BP As New Container
                BP.Reset()
                Do
                    If Not BP.GetName.EndsWith("]") Then
                        Select Case BP.GetName.ToLower
                            Case "backpack"
                                SetContainerName("Backpack " & "[" & BP.GetContainerIndex + 1 & "]", BP.GetContainerIndex)
                            Case "bag"
                                SetContainerName("Bag " & "[" & BP.GetContainerIndex + 1 & "]", BP.GetContainerIndex)
                        End Select
                    End If
                Loop While BP.NextContainer
            Catch ex As Exception
                MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Kernel.ConsoleError("Unkown Error occured during Renaming Backpacks. Renaming Backpacks is now disabled.")
                RenameBackpackObj.StopTimer()
            End Try
        End Sub

#End Region

#End Region

#Region " IRC Client "

#Region " Connection "

        Public Sub ConnectToIrc()
            Try
                'If Client.IsConnected Then Exit Sub
                'ConsoleError("cli")
                'IrcGenerateNick()
                If Not String.IsNullOrEmpty(Consts.IRCNickname) Then
                    IRCClient.Nick = Consts.IRCNickname
                Else
                    IrcGenerateNick()
                End If
                IRCClient.RealName = Client.CharacterWorld
                IRCClient.User = Environment.MachineName
                IRCClient.Invisible = True
                If Not IRCClient.Connect Then
                    ConsoleError("Unable to connect to ")
                    Exit Sub
                End If
                IRCClient.DoMainThread()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcGenerateNick()
            Dim Nicks() As String = {"TTBOwner", "TTBFan", "TTBKicker", "TTBKiller", "TTBPwner", "TTBRokr", _
             "TTBKrzr", "TTBRazr", "TTBLord", "TTBUser", "TTBPKer", "TTBLurer", "TTBGamer", "TTBLoco", _
             "TTBNeedy", "TTBBeast", "TTBCrzy", "TTBHunter", "TTBRot", "TTBSuper", "TTBLntc", "TTBTurbo", _
             "TTBKilo", "TTBAlpha", "TTBBeta", "TTBOmega", "TTBPhi", "TTBPsych", "TTBMstr", "TTBBravo", "TTBCharlie"}
            Dim R As New Random(System.DateTime.Now.Millisecond)
            IRCClient.Nick = Nicks(R.Next(Nicks.Length)) & R.Next(100, 1000).ToString
        End Sub

#End Region

#Region " Methods "

        Public Sub IrcChannelSpeakOwner(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick, 4, ITibia.ChannelMessageType.Tutor, Message, ChannelID)
                'Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelGM, 5, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcChannelSpeakUnknown(ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak("TibiaTekBot", 0, ITibia.ChannelMessageType.Anonymous, Message, ChannelID)
                'Proxy.SendPacketToClient(CreatureSpeak("TibiaTekBot", MessageType.ChannelCounsellor, 1, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcChannelSpeakOperator(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick, 3, ITibia.ChannelMessageType.Tutor, Message, ChannelID)
                'Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelGM, 4, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcChannelSpeakHalfOperator(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick, 2, ITibia.ChannelMessageType.Tutor, Message, ChannelID)
                ' Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelTutor, 3, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcChannelSpeakVoiced(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick, 1, ITibia.ChannelMessageType.Tutor, Message, ChannelID)
                'Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.ChannelTutor, 2, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub IrcChannelSpeakNormal(ByVal Nick As String, ByVal Message As String, ByVal ChannelID As Integer)
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick, 0, ITibia.ChannelMessageType.Normal, Message, ChannelID)
                ' Proxy.SendPacketToClient(CreatureSpeak(Nick, MessageType.Channel, 1, Message, 0, 0, 0, ChannelID))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function IrcChannelIDToName(ByVal ChannelID As Integer) As String
            Try
                For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In IRCClient.Channels
                    If ChannelKVP.Value.ID = ChannelID Then
                        Return ChannelKVP.Key
                    End If
                Next
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return String.Empty
        End Function

        Public Function IrcChannelIsOpened(ByVal ChannelID As Integer) As Boolean
            For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In IRCClient.Channels
                If ChannelID = ChannelKVP.Value.ID Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function IrcChannelIsOpened(ByRef ChannelName As String) As Boolean
            For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In IRCClient.Channels
                If String.Equals(ChannelName, ChannelKVP.Key, StringComparison.CurrentCultureIgnoreCase) AndAlso ChannelKVP.Value.ID > 0 Then
                    ChannelName = ChannelKVP.Key
                    Return True
                End If
            Next
            Return False
        End Function

        Public Function IrcChannelNameToID(ByVal ChannelName As String) As Integer
            Try
                For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In IRCClient.Channels
                    If String.Equals(ChannelName, ChannelKVP.Key, StringComparison.CurrentCultureIgnoreCase) Then
                        Return ChannelKVP.Value.ID
                    End If
                Next
                Return 0
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

#End Region

#Region " IRC Events "
        Private Sub IrcClient_ChannelJoin(ByVal Nick As String, ByVal Channel As String) Handles IRCClient.ChannelJoin
            'ConsoleWrite(Nick & " joined " & Channel & ".")
        End Sub

        Private Sub IrcClient_ChannelKick(ByVal NickKicker As String, ByVal NickKicked As String, ByVal Reason As String, ByVal Channel As String) Handles IRCClient.ChannelKick
            Try
                'ConsoleWrite(NickKicker & " kicked " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".")
                If IrcChannelIsOpened(Channel) Then
                    IrcChannelSpeakUnknown(NickKicker & " kicked " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".", IrcChannelNameToID(Channel))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_ChannelAction(ByVal Nick As String, ByVal Action As String, ByVal Channel As String) Handles IRCClient.ChannelAction
            Try
                IrcChannelSpeakUnknown(Nick & " " & Action, IrcChannelNameToID(Channel))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_ChannelBroadcast(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Handles IRCClient.ChannelBroadcast
            Try
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.SpeakWithBroadcast(Nick, Message)
                'Proxy.SendPacketToClient(CreatureSpeak("Broadcast from " & Nick, MessageType.Broadcast, 4, Message, 0, 0, 0, IrcChannelNameToID(Channel)))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_ChannelSelfKick(ByVal nickkicker As String, ByVal Reason As String, ByVal Channel As String) Handles IRCClient.ChannelSelfKick
            Try
                IrcChannelSpeakUnknown("You have been kicked from " & Channel & " by " & nickkicker & ". Reason: " & Reason & ".", IrcChannelNameToID(Channel))
                ConsoleError("You have been kicked from " & Channel & " by " & nickkicker & ". Reason: " & Reason & ".")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_TopicChange(ByVal ChannelInfo As IIrcClient.ChannelInformation) Handles IRCClient.ChannelTopicChange
            Try
                Thread.Sleep(1000)
                If IrcChannelIsOpened(ChannelInfo.Name) Then
                    IrcChannelSpeakOperator(ChannelInfo.TopicOwner, ChannelInfo.Topic, IrcChannelNameToID(ChannelInfo.Name))
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_ChannelSelfPart(ByVal Channel As String) Handles IRCClient.ChannelSelfPart
            Try
                ConsoleWrite("You have left " & Channel & ".")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_NickChange(ByVal OldNick As String, ByVal NewNick As String) Handles IRCClient.NickChange
            'If IrcChannelIsOpened(Channel) Then
            '    IrcChannelSpeakOperator(Channel, OldNick & " is now known as " & NickKicked & " from " & Channel & ". Reason: " & Reason & ".", IrcChannelNameToID(Channel))
            'End If
        End Sub

        Private Sub IrcClient_Quit(ByVal Nick As String, ByVal Message As String) Handles IRCClient.QuitIrc
            'ConsoleWrite(Nick & " quits. Reason: " & Message & ".")
        End Sub

        Private Sub IrcClient_ChannelSelfJoin(ByVal Channel As String) Handles IRCClient.ChannelSelfJoin
            Try
                ConsoleWrite("Joined channel " & Channel & ".")
                Dim UsedIDs As New List(Of Integer)
                Dim ChannelID As Integer = 1
                If IrcChannelIsOpened(Channel) Then
                    ConsoleWrite("You have already joined that channel.")
                End If
                For Each ChannelInfo As IIrcClient.ChannelInformation In IRCClient.Channels.Values
                    If ChannelInfo.ID > 0 Then
                        UsedIDs.Add(ChannelInfo.ID)
                    End If
                Next
                Dim R As New Random(System.DateTime.Now.Millisecond)
                Dim CI As IIrcClient.ChannelInformation
                Do
                    ChannelID = R.Next(ITibia.Channel.IRCChannelBegin, ITibia.Channel.IRCChannelEnd) '0..39
                    If Not UsedIDs.Contains(ChannelID) Then
                        For Each ChannelKVP As System.Collections.Generic.KeyValuePair(Of String, IIrcClient.ChannelInformation) In IRCClient.Channels
                            If ChannelKVP.Key = Channel Then
                                CI = ChannelKVP.Value
                                CI.ID = ChannelID
                                IRCClient.Channels(Channel) = CI
                                Exit For
                            End If
                        Next
                        'IRCChannelIDs.Add(ChannelID, Channel)
                        Dim CP As New ClientPacketBuilder(Proxy)
                        CP.OpenChannel(Channel, ChannelID)
                        'OpenIrcChannel(Channel, ChannelID)
                        Exit Do
                    End If
                Loop While True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_Connecting() Handles IRCClient.Connecting
            Try
                ConsoleWrite("Connecting to IRC. Please Wait...")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_Connected() Handles IRCClient.Connected
            Try
                ConsoleWrite("Successfully connected to IRC. Opening channels, please wait...")
                If Not String.IsNullOrEmpty(IRCClient.Nick) AndAlso Not String.IsNullOrEmpty(Consts.IRCPassword) Then
                    IRCClient.Password = Consts.IRCPassword
                    IRCClient.Speak(String.Format("GHOST {0} {1}", IRCClient.Nick, IRCClient.Password), "NICKSERV")
                End If
                IRCClient.Identify()
                If Not String.IsNullOrEmpty(IRCClient.Password) Then
                    IRCClient.Speak(String.Format("IDENTIFY {0}", IRCClient.Password), "NICKSERV")
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_Notice(ByVal Nick As String, ByVal Message As String) Handles IRCClient.Notice
            Try
                If Nick.StartsWith("dairc", StringComparison.CurrentCultureIgnoreCase) Then Exit Sub
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(Nick & "@IRC", 0, Message)
                'Core.Proxy.SendPacketToClient(PacketUtils.CreatureSpeak(Nick & "@IRC", MessageType.PM, 5, Message, 0, 0, 0, ChannelType.Console))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_PrivateMessage(ByVal Nick As String, ByVal Message As String) Handles IRCClient.PrivateMessage
            Dim CP As New ClientPacketBuilder(Proxy)
            CP.Speak(Nick & "@IRC", 0, Message, ITibia.PrivateMessageType.Normal)
            'Core.Proxy.SendPacketToClient(PacketUtils.CreatureSpeak(Nick & "@IRC", MessageType.PM, 5, Message, 0, 0, 0, ChannelType.Console))
        End Sub

        Private Sub IrcClient_Disconnected() Handles IRCClient.Disconnected
            Try
                ConsoleError("Disconnected from IRC.")
                IRCClient.Channels.Clear()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_EndMOTD() Handles IRCClient.EndMOTD
            Try
                If Consts.IRCJoinAfterConnected Then
                    Dim _Channels() As String = Consts.IRCJoinChannels.Split(","c)
                    For Each _Channel As String In _Channels
                        If Not String.IsNullOrEmpty(_Channel) Then
                            IRCClient.Join(_Channel)
                            Thread.Sleep(500)
                        End If
                    Next
                    Thread.Sleep(500)
                    Dim CI As CultureInfo = CultureInfo.CurrentCulture
                    Dim Name As String = CI.Name.Substring(0, 2)
                    Select Case Name.ToLower
                        Case "de"
                            IRCClient.Join("#ttbdeutsch")
                        Case "nl"
                            IRCClient.Join("#ttbnederlands")
                        Case "sv"
                            IRCClient.Join("#ttbsvenska")
                        Case "fi"
                            IRCClient.Join("#ttbsuomi")
                        Case "es"
                            IRCClient.Join("#ttbespanol")
                        Case "pt"
                            IRCClient.Join("#ttbportugues")
                        Case "pl"
                            IRCClient.Join("#ttbpolacy")
                        Case Else
                            IRCClient.Join("#tibiatekbot")
                    End Select
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_Invite(ByVal Nick As String, ByVal Channel As String) Handles IRCClient.Invite
            Try
                Static LastInvite As DateTime = Now
                Static FirstTime As Boolean = True
                If Not FirstTime AndAlso LastInvite.Subtract(Now).TotalSeconds < 10 Then Exit Sub
                FirstTime = False
                Dim CPB As New ClientPacketBuilder(Proxy)
                CPB.SystemMessage(ITibia.SysMessageType.Information, "You have been invited to join " & Channel & " by " & Nick & "@IRC")
                LastInvite = Now
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub IrcClient_ChannelMessage(ByVal Nick As String, ByVal Message As String, ByVal Channel As String) Handles IRCClient.ChannelMessage
            Try
                If IrcChannelIsOpened(Channel) Then
                    Select Case IRCClient.GetUserLevel(Nick, Channel)
                        Case 1
                            IrcChannelSpeakVoiced(Nick & "@IRC", Message, IrcChannelNameToID(Channel))
                        Case 2
                            IrcChannelSpeakHalfOperator(Nick & "@IRC", Message, IrcChannelNameToID(Channel))
                        Case 3
                            IrcChannelSpeakOperator(Nick & "@IRC", Message, IrcChannelNameToID(Channel))
                        Case 4
                            IrcChannelSpeakOwner(Nick & "@IRC", Message, IrcChannelNameToID(Channel))
                        Case Else
                            IrcChannelSpeakNormal(Nick & "@IRC", Message, IrcChannelNameToID(Channel))
                    End Select
                    If Message.ToLower.Contains(IRCClient.Nick.ToLower) Then
                        If (Consts.IRCHighlightOpOnly AndAlso IRCClient.GetUserLevel(Nick, Channel) < 4) Then Exit Sub
                        Dim CPB As New ClientPacketBuilder(Proxy)
                        CPB.SystemMessage(ITibia.SysMessageType.Information, "[" & Channel & "] " & Nick & "@IRC: " & Message)
                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#End Region

#Region " Proxy Events "

        Public Function GetProfileDirectory() As String
            Try
                'If Not Client.IsConnected() Then Throw New Exception("You must be logged in.")
                Dim Path As String = ExecutablePath & "\Profiles\" & Client.CharacterWorld & "\" & Client.CharacterName
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
                Client.Title = BotName & " - " & Client.CharacterName
                ExpCheckerActivated = False
                ExpCheckerTimerObj.StartTimer()
                If StatsTimerObj.State = IThreadTimer.ThreadTimerState.Stopped Then
                    StatsTimerObj.StartTimer()
                    ChatMessageQueueList.Clear()
                    ChatMessageQueueTimerObj.StartTimer()
                End If
                System.GC.Collect()
                Log("Event", "Connected to game server.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub Proxy_ConnectionLost() Handles Proxy.ConnectionLost
            Try
                'ChangeClientTitle(BotName & " - Not Logged In")
                Kernel.NotifyIcon.Text = "TibiaTek Bot v" & BotVersion & " - Not logged in"
                IsGreetingSent = False
                GreetingSentTry = 0
                StopEverything()
                System.GC.Collect()
                Log("Event", "Disconnected from game server.")
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub Client_Closed() Handles Client.Closed
            Try
                Kernel.NotifyIcon.Visible = False
                Log("Event", "The Tibia Client has been closed.")
                End
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

#Region " Packets from Client Parsers "

        Private Sub ClientParseLogout(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseAutoMapWalk(ByRef bytBuffer() As Byte, ByRef Pos As Integer, ByRef Send As Boolean)
            Dim Directions As Integer = GetByte(bytBuffer, Pos)
            Pos += Directions
        End Sub

        Private Sub ClientParsePing(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveNorthEast(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveSouthEast(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveSouthWest(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveNorthWest(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveNorth(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveEast(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveSouth(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterMoveWest(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterTurnNorth(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterTurnEast(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterTurnWest(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseCharacterTurnSouth(ByRef Send As Boolean)
        End Sub

        Private Sub ClientParseMoveObject(ByRef bytBuffer() As Byte, ByRef Pos As Integer, ByRef Send As Boolean)
            Dim Source As ITibia.LocationDefinition = GetLocation(bytBuffer, Pos)
            Dim ItemID As UShort = GetWord(bytBuffer, Pos)
            Dim Slot As Integer = GetByte(bytBuffer, Pos)
            Dim Destination As ITibia.LocationDefinition = GetLocation(bytBuffer, Pos)
            Dim Count As Integer = GetByte(bytBuffer, Pos)
            Dim MyContainer As New Container
            Dim CP As New ClientPacketBuilder(Proxy)
            If Source.X = &HFFFF AndAlso Source.Y = &H4F Then 'containers only
                MyContainer.JumpToContainer(&HF) 'go to that container
                Dim ContainerSize As Integer = MyContainer.GetContainerSize
                If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then 'is fake
                    Send = False
                    If LooterCurrentCategory = 0 Then Exit Sub
                    'thrown to map, or thrown to inventory, or thrown to another bp
                    If (Destination.X < &HFFFF) OrElse (Destination.X = &HFFFF AndAlso Destination.Y < &H40) OrElse (Destination.X = Source.X AndAlso Destination.Y <> Source.Y) Then
                        LootItems.Remove(ItemID)
                        CP.RemoveObjectFromContainer(Slot, Source.Y - &H40)
                        ConsoleWrite(Client.Objects.Name(ItemID) & " (H" & Hex(ItemID) & ") removed from " & MyContainer.GetName & ".")
                    Else
                        CP.SystemMessage(SysMessageType.StatusSmall, "Sorry, not possible.")
                    End If
                End If
            ElseIf (Source.X = &HFFFF AndAlso Source.Y < &H40) OrElse Source.X < &HFFFF OrElse (Source.X = &HFFFF AndAlso Source.Y <> Destination.Y) Then
                If Destination.X = &HFFFF AndAlso Destination.Y = &H4F Then
                    MyContainer.JumpToContainer(&HF) 'go to that container
                    Dim ContainerSize As Integer = MyContainer.GetContainerSize
                    If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then 'is fake
                        Send = False
                        If LooterCurrentCategory = 0 Or ItemID <= 100 Then Exit Sub
                        If LootItems.Add(New LootItems.LootItemDefinition(ItemID, LooterCurrentCategory - 1, 1)) Then
                            If Client.Objects.HasFlags(ItemID, IObjects.ObjectFlags.IsStackable) Then
                                Count = 100
                            ElseIf Client.Objects.HasFlags(ItemID, IObjects.ObjectFlags.IsFluidContainer) Then
                                '   keep count
                            ElseIf Client.Objects.HasExtraByte(ItemID) Then
                                Count = 1
                            End If
                            ConsoleWrite(Client.Objects.Name(ItemID) & " (H" & Hex(ItemID) & ") added to " & MyContainer.GetName & ".")
                            CP.AddObjectToContainer(ItemID, &HF, Count)
                        Else
                            ConsoleError("This item already exists.")
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub ClientParseUseItem(ByRef bytBuffer() As Byte, ByRef Pos As Integer, ByRef Send As Boolean)
            Dim Location As ITibia.LocationDefinition = GetLocation(bytBuffer, Pos)
            Dim ItemID As Integer = GetWord(bytBuffer, Pos)
            Dim Slot As Integer = GetByte(bytBuffer, Pos)
            Dim ContainerIndex As Integer = GetByte(bytBuffer, Pos)
            Dim CP As New ClientPacketBuilder(Proxy)
            Dim SP As New ServerPacketBuilder(Proxy)
            If Client.Objects.HasFlags(ItemID, IObjects.ObjectFlags.IsContainer) Then BagOpened = False
            If Location.Y = &H4F Then
                Dim MyContainer As New Container
                MyContainer.JumpToContainer(&HF)
                Dim ContainerSize As Integer = MyContainer.GetContainerSize
                If MyContainer.IsOpened AndAlso ContainerSize = &H24 Then
                    If String.Compare(MyContainer.GetName, "Loot Categories") = 0 Then 'using a category :O
                        LooterCurrentCategory = Slot + 1
                        CP.CreateContainer(ItemID, &HF, "Loot Category #" & (Slot + 1), &H24, LootItems.GetItemsIDs(Slot), True)
                        'Proxy.SendPacketToClient(CreateContainer(ItemID, &HF, "Loot Category #" & (Slot + 1), &H24, LootItems.GetItemsIDs(Slot), True))
                    Else
                        LootItems.SetLootBackpackIndex(ItemID, 3)
                        LootItems.Save()
                        Kernel.ConsoleWrite(LootItems.GetLootItem(ItemID).GetLootBackpack)
                        CP.SystemMessage(SysMessageType.Information, "Item Information: " & Client.Objects.Name(ItemID) & " (H" & Hex(ItemID) & ").")
                        'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Item Information: " & Client.Objects.Name(ItemID) & " (H" & Hex(ItemID) & ")."))
                    End If
                    Send = False
                    Exit Sub
                End If
            End If
            If Consts.HotkeysCanEquipItems AndAlso (Location.X = &HFFFF AndAlso Location.Y = 0 AndAlso Location.Z = 0) Then 'hotkey
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Ring) Then
                    Dim ItemDef As Scripting.IContainer.ContainerItemDefinition
                    If (New Container).FindItem(ItemDef, ItemID, 0, 0, Consts.MaxContainers - 1) Then
                        If RingChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                            RingID = ItemID
                            'ConsoleWrite("Ring Changer Item: " & Client.Objects.Name(ItemID))
                        End If
                        SP.MoveObject(ItemDef, GetInventorySlotAsLocation(ITibia.InventorySlots.Finger))
                        'Proxy.SendPacketToServer(MoveObject(ItemDef, GetInventorySlotAsLocation(ITibia.InventorySlots.Finger)))
                    Else
                        ConsoleError("Could not find " & Client.Objects.Name(ItemID) & ", make sure it is on an open container.")
                    End If
                    Send = False
                End If
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Neck) Then
                    Dim ItemDef As Scripting.IContainer.ContainerItemDefinition
                    If (New Container).FindItem(ItemDef, ItemID, 0, 0, Consts.MaxContainers - 1) Then
                        If AmuletChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                            AmuletID = ItemID
                            'ConsoleWrite("Amulet Changer Item: " & Client.Objects.Name(ItemID))
                        End If
                        SP.MoveObject(ItemDef, GetInventorySlotAsLocation(ITibia.InventorySlots.Neck))
                        'Proxy.SendPacketToServer(MoveObject(ItemDef, GetInventorySlotAsLocation(ITibia.InventorySlots.Neck)))
                        AmuletID = ItemID
                    Else
                        ConsoleError("Could not find " & Client.Objects.Name(ItemID) & ", make sure it is on an open container.")
                    End If
                    Send = False
                End If
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Ammunition) Then
                    Dim Ammodef As Scripting.IContainer.ContainerItemDefinition
                    Dim Cont As New Container
                    If (New Container).FindItem(Ammodef, ItemID, 0, 0, Consts.MaxContainers - 1) Then

                        If AmmoRestackerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                            AmmoRestackerItemID = ItemID
                            ConsoleWrite("Ammunition Restacker Item: " & Client.Objects.Name(AmmoRestackerItemID))
                        End If
                        SP.MoveObject(Ammodef, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), Cont.GetItemCount)
                    Else
                        ConsoleError("Could not find " & Client.Objects.Name(ItemID) & ", make sure it is on an open container.")
                    End If
                    Send = False
                End If
            ElseIf Consts.EquipItemsOnUse Then
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Neck) Then
                    If AmuletChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                        AmuletID = ItemID
                    End If
                    SP.MoveObject(ItemID, Location, GetInventorySlotAsLocation(ITibia.InventorySlots.Neck), 1)
                    Send = False
                End If
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Ring) Then
                    If RingChangerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                        RingID = ItemID
                    End If
                    SP.MoveObject(ItemID, Location, GetInventorySlotAsLocation(ITibia.InventorySlots.Finger), 1)
                    Send = False
                End If
                If Client.Objects.IsKind(ItemID, IObjects.ObjectKind.Ammunition) Then
                    If AmmoRestackerTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                        AmmoRestackerItemID = ItemID
                    End If
                    SP.MoveObject(ItemID, Location, GetInventorySlotAsLocation(ITibia.InventorySlots.Belt), 100)
                    Send = False
                End If
            End If
            If LearningMode Then
                If Client.Objects.LensHelp(ItemID) = IObjects.ObjectLensHelp.Ladder Then
                    Dim WalkerChar As New Walker
                    WalkerChar.Coordinates = Location
                    WalkerChar.Type = Walker.WaypointType.Ladder
                    WalkerChar.Info = ""
                    Walker_Waypoints.Add(WalkerChar)
                    Kernel.ConsoleWrite("Ladder waypoint added.")
                    AutoAddTime = Now.AddSeconds(5)
                End If
                If Client.Objects.LensHelp(ItemID) = IObjects.ObjectLensHelp.Sewer Then
                    Dim WalkerChar As New Walker
                    WalkerChar.Coordinates = Location
                    WalkerChar.Type = Walker.WaypointType.Sewer
                    WalkerChar.Info = ""
                    Walker_Waypoints.Add(WalkerChar)
                    Kernel.ConsoleWrite("Sewer waypoint added.")
                    AutoAddTime = Now.AddSeconds(5)
                End If
            End If
        End Sub
#End Region

        Private Sub Proxy_PacketFromClient(ByRef bytBuffer() As Byte, ByRef Send As Boolean) Handles Proxy.PacketFromClient
            Try
                Dim RegExp As Regex = New Regex("&([^;]+);?")
                Dim MCollection As MatchCollection
                Dim GroupMatch As Match
                Dim Pos As Integer = 2
                Dim Message As String
                Dim BL As New BattleList
                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                If Consts.DebugOnLog Then Log("FromClient", BytesToStr(bytBuffer))
                'ConsoleRead(BytesToStr(bytBuffer))
                Dim CP As New ClientPacketBuilder(Proxy)
                Dim SP As New ServerPacketBuilder(Proxy)
                Dim ID As UShort = GetByte(bytBuffer, Pos)
                Select Case ID
                    Case &H14 ' Logout
                        ClientParseLogout(Send)
                    Case &H1E ' Ping
                        ClientParsePing(Send)
                    Case &H64 ' Auto Walk
                        ClientParseAutoMapWalk(bytBuffer, Pos, Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H65 ' Character Move North
                        ClientParseCharacterMoveNorth(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H66 ' Character Move East
                        ClientParseCharacterMoveEast(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H67
                        ClientParseCharacterMoveSouth(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H68
                        ClientParseCharacterMoveWest(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H6A
                        ClientParseCharacterMoveNorthEast(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H6B
                        ClientParseCharacterMoveSouthEast(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H6C
                        ClientParseCharacterMoveSouthWest(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H6D
                        ClientParseCharacterMoveNorthWest(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H6F
                        ClientParseCharacterTurnNorth(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H70
                        ClientParseCharacterTurnEast(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H71
                        ClientParseCharacterTurnSouth(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H72
                        ClientParseCharacterTurnWest(Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H78 'move object
                        ClientParseMoveObject(bytBuffer, Pos, Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H82 'use item
                        ClientParseUseItem(bytBuffer, Pos, Send)
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case &H83 'Use Item With
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        Pos += 2
                        Dim Cont As New Scripting.IContainer.ContainerItemDefinition
                        Cont.ContainerIndex = GetByte(bytBuffer, Pos)
                        Cont.Slot = GetWord(bytBuffer, Pos)
                        Cont.ID = GetWord(bytBuffer, Pos)
                        Pos += 1
                        Dim Location As ITibia.LocationDefinition = GetLocation(bytBuffer, Pos)
                        Dim TileId As Integer = GetWord(bytBuffer, Pos)
                        If LearningMode Then
                            Dim WalkerChar As New Walker
                            Select Case Client.Objects.Name(Cont.ID)
                                Case "Rope"
                                    WalkerChar.Type = Walker.WaypointType.Rope
                                    WalkerChar.Info = ""
                                    Kernel.ConsoleWrite("Rope waypoint added.")
                                    AutoAddTime = Now.AddSeconds(5)
                                Case "Shovel", "Light Shovel"
                                    WalkerChar.Type = Walker.WaypointType.Shovel
                                    WalkerChar.Info = ""
                                    Kernel.ConsoleWrite("Shovel waypoint added.")
                                    AutoAddTime = Now.AddSeconds(5)
                                Case Else
                                    Exit Sub
                            End Select
                            WalkerChar.Coordinates = Location
                            Walker_Waypoints.Add(WalkerChar)
                        End If
                    Case &H84 'Use hotkey
                        'Send = False
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        Pos += 13
                    Case &H88 'go to parent
                        Proxy.LastAction = Date.Now.Ticks
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
                    Case &H8A
                        Proxy.LastAction = Date.Now.Ticks
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

                                Data &= vbLf & "-" & Client.CharacterName & " (" & Client.CharacterWorld & ")"
                                If Data.Length > 0 Then
                                    Try
                                        Dim Content As Byte() = System.Text.Encoding.ASCII.GetBytes("feedback=" & System.Web.HttpUtility.UrlEncode(Data))
                                        Dim WClient As New WebClient
                                        WClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                                        Dim URI As New System.Uri("http://www.tibiatek.com/feedback.php")
                                        WClient.UploadDataAsync(URI, "POST", Content)
                                        ConsoleWrite("Thank you for your feedback, it is greatly appreciated.")
                                    Catch
                                        ConsoleError("Sorry, the feedback was not sent properly.")
                                    End Try
                                End If
                        End Select
                    Case &H96 'message
                        SP.AutoSend = False
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        Dim MType As Byte = GetByte(bytBuffer, Pos)
                        Dim MessageType As ITibia.MessageType
                        Dim PrivateMessageType As ITibia.PrivateMessageType
                        Dim ChannelMessageType As ITibia.ChannelMessageType
                        Dim DefaultMessageType As ITibia.DefaultMessageType
                        Select Case MType
                            Case 1 To 3, 10 To 11
                                DefaultMessageType = MType
                                MessageType = ITibia.MessageType.Default
                            Case 4, &HB
                                PrivateMessageType = MType
                                MessageType = ITibia.MessageType.PrivateMessage
                            Case 5, &HA, &HC, &HE
                                ChannelMessageType = MType
                                MessageType = ITibia.MessageType.Channel
                            Case 9 ' broadcast
                                Exit Sub
                            Case Else
                                Exit Sub 'ok unexpected!
                        End Select
                        If MessageType = ITibia.MessageType.Channel AndAlso bytBuffer(4) = ITibia.Channel.Console Then
                            Message = GetString(bytBuffer, 6)
                            Send = False
                            If MessageIsSpell(Message) Then
                                SP.Speak(Message)
                                SP.Send()
                                'MsgBox(Message)
                                Exit Sub
                            End If
                            If Message.StartsWith("&") Then
                                ConsoleRead(Message)
                                MCollection = RegExp.Matches(Message)
                                For Each GroupMatch In MCollection
                                    Kernel.CommandParser.Invoke(GroupMatch.Groups(1).ToString)
                                Next
                            End If
                        ElseIf MessageType = ITibia.MessageType.Channel AndAlso (bytBuffer(4) >= ITibia.Channel.IRCChannelBegin AndAlso bytBuffer(4) < ITibia.Channel.IRCChannelEnd) Then
                            Send = False
                            Dim ChannelID As Int16 = bytBuffer(4)
                            Message = GetString(bytBuffer, 6)
                            If MessageIsSpell(Message) Then
                                SP.Speak(Message)
                                SP.Send()
                                Exit Sub
                            End If
                            Dim Channel As String = IrcChannelIDToName(ChannelID)
                            If IRCClient.Channels.ContainsKey(Channel) Then
                                If Message.StartsWith("&") Then
                                    ConsoleRead(Message)
                                    MCollection = RegExp.Matches(Message)
                                    For Each GroupMatch In MCollection
                                        Kernel.CommandParser.Invoke(GroupMatch.Groups(1).ToString)
                                    Next
                                ElseIf Message.StartsWith("/") Then
                                    Dim Match As Match = Regex.Match(Message.TrimEnd(" "c), "^/(join|nick|users|me)(?:\s(.+))?", RegexOptions.IgnoreCase)
                                    If Match.Success Then
                                        Select Case Match.Groups(1).Value.ToLower
                                            Case "join", "j"
                                                IRCClient.Join(Match.Groups(2).Value)
                                            Case "nick", "n"
                                                IRCClient.Nick = Match.Groups(2).Value
                                                IRCClient.ChangeNick(IRCClient.Nick)
                                            Case "me"
                                                Dim Action As String = Match.Groups(2).Value
                                                If Action.Length > 0 Then
                                                    Dim InvalidWords() As String = {":", "[", "]", "gm", "cm", "admin", "}", "{", "-", "+", "~", "@", "irc"}
                                                    For Each InvalidWord As String In InvalidWords
                                                        If Action.ToLower.Contains(InvalidWord) Then
                                                            Exit Sub
                                                        End If
                                                    Next
                                                    IRCClient.Speak(Chr(1) & "ACTION " & Action & Chr(1), Channel)
                                                End If
                                                IrcChannelSpeakUnknown(IRCClient.Nick & " " & Match.Groups(2).Value, IrcChannelNameToID(Channel))
                                            Case "users", "u"
                                                If Kernel.IRCClient.Channels.ContainsKey(Channel) Then
                                                    Dim TempNick As String = ""
                                                    For Each Nick As String In Kernel.IRCClient.Channels(Channel).Users.Keys
                                                        Dim Temp() As String = {"", "+", "%", "@", "~"}
                                                        TempNick = Temp(IRCClient.GetUserLevel(Nick, Channel)) & Nick
                                                        Kernel.IrcChannelSpeakNormal(Channel, TempNick, IrcChannelNameToID(Channel))
                                                    Next
                                                End If
                                        End Select
                                    End If
                                Else
                                    Select Case IRCClient.GetUserLevel(IRCClient.Nick, Channel)
                                        Case 0
                                            IrcChannelSpeakNormal(IRCClient.Nick, Message, ChannelID)
                                        Case 1
                                            IrcChannelSpeakVoiced(IRCClient.Nick, Message, ChannelID)
                                        Case 2
                                            IrcChannelSpeakHalfOperator(IRCClient.Nick, Message, ChannelID)
                                        Case 3
                                            IrcChannelSpeakOperator(IRCClient.Nick, Message, ChannelID)
                                        Case 4
                                            IrcChannelSpeakOwner(IRCClient.Nick, Message, ChannelID)
                                    End Select
                                    IRCClient.Speak(Message, Channel)
                                End If
                            Else
                                ConsoleError("Unable to send message to the IRC Channel.")
                            End If
                        Else
                            'ConsoleWrite("talking shiz")
                            Dim ChatMessage As New ChatMessageDefinition
                            ChatMessage.Prioritize = True

                            Select Case MessageType
                                Case ITibia.MessageType.PrivateMessage

                                    ChatMessage.PrivateMessageType = PrivateMessageType
                                    ChatMessage.Destinatary = GetString(bytBuffer, Pos)
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    'MsgBox(ChatMessage.Message)
                                    If MessageIsSpell(ChatMessage.Message) Then
                                        SP.Speak(ChatMessage.Message)
                                        SP.Send()
                                        Send = False
                                        Exit Sub
                                    End If
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            Kernel.CommandParser.Invoke(GroupMatch.Groups(1).Value)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    ElseIf Regex.IsMatch(ChatMessage.Destinatary, "^(.+)@irc$", RegexOptions.IgnoreCase) Then
                                        If ChatMessage.Message.StartsWith("/") Then
                                            Dim Match As Match = Regex.Match(ChatMessage.Message.TrimEnd(" "c), "^/(join|nick|users|me|nickserv|msg)(?:\s(.+))?", RegexOptions.IgnoreCase)
                                            If Match.Success Then
                                                Select Case Match.Groups(1).Value.ToLower
                                                    Case "join", "j"
                                                        IRCClient.Join(Match.Groups(2).Value)
                                                    Case "nick", "n"
                                                        IRCClient.Nick = Match.Groups(2).Value
                                                        IRCClient.ChangeNick(IRCClient.Nick)
                                                End Select
                                            End If
                                        End If
                                        CP.SystemMessage(SysMessageType.StatusSmall, "Message sent to " & ChatMessage.Destinatary & ".")
                                        IRCClient.Speak(ChatMessage.Message, ChatMessage.Destinatary.Substring(0, ChatMessage.Destinatary.Length - 4))
                                        Send = False
                                        Exit Sub
                                    End If
                                    SP.Speak(ChatMessage.Destinatary, ChatMessage.Message, ChatMessage.PrivateMessageType)
                                    'bytNewBuffer = Speak(ChatMessage.Destinatary, ChatMessage.Message)
                                Case ITibia.MessageType.Channel
                                    ChatMessage.ChannelMessageType = ChannelMessageType
                                    ChatMessage.Channel = GetWord(bytBuffer, Pos)
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            Kernel.CommandParser.Invoke(GroupMatch.Groups(1).ToString)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    End If
                                    SP.Speak(ChatMessage.Message, ChatMessage.Channel, ChatMessage.ChannelMessageType)
                                    'bytNewBuffer = Speak(ChatMessage.Message, ChatMessage.Channel)
                                Case Else
                                    ChatMessage.DefaultMessageType = DefaultMessageType
                                    ChatMessage.Message = GetString(bytBuffer, Pos)
                                    If ChatMessage.Message.StartsWith("&") Then
                                        MCollection = RegExp.Matches(ChatMessage.Message)
                                        For Each GroupMatch In MCollection
                                            ConsoleRead("&" & GroupMatch.Groups(1).Value)
                                            Kernel.CommandParser.Invoke(GroupMatch.Groups(1).ToString)
                                        Next
                                        If MCollection.Count > 0 Then
                                            Send = False
                                            Exit Sub
                                        End If
                                    End If
                                    SP.Speak(ChatMessage.Message, ChatMessage.DefaultMessageType)
                                    'bytNewBuffer = Speak(ChatMessage.Message, MessageType)
                            End Select
                            Dim TimeElapsed As TimeSpan = Date.Now.Subtract(ChatMessageLastSent)
                            If ChatMessageLastSent = Date.MinValue OrElse TimeElapsed.TotalSeconds >= 3 Then
                                'MsgBox(ChatMessage.DefaultMessageType)
                                SP.Send()
                                'Proxy.SendPacketToServer(bytNewBuffer)
                            Else
                                ChatMessageQueueList.Add(ChatMessage)
                            End If
                            Send = False
                        End If
                    Case &H98 ' Requesting console through Channel List
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        If bytBuffer(3) = ConsoleChannelID Then
                            Send = False
                            'Dim CP As New ClientPacketBuilder(Proxy)
                            CP.OpenChannel(ConsoleName, ConsoleChannelID)
                            'OpenChannel()
                        End If
                    Case &H99 ' Closing channel
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        If bytBuffer(3) > ITibia.Channel.IRCChannelBegin AndAlso bytBuffer(3) <= ITibia.Channel.IRCChannelEnd Then
                            Dim ChannelID As Int16 = bytBuffer(3)
                            Send = False
                            If IrcChannelIsOpened(ChannelID) Then
                                IRCClient.Part(IrcChannelIDToName(ChannelID))
                                IRCClient.Channels.Remove(IrcChannelIDToName(ChannelID))
                            End If
                        End If
                    Case &H9A ' Requesting console given a string
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                        Dim ChannelName As String = GetString(bytBuffer, 3)
                        If String.Compare(ChannelName, "console", True) = 0 Or String.Compare(ChannelName, ConsoleName, True) = 0 Then
                            Send = False
                            'Dim CP As New ClientPacketBuilder(Proxy)
                            CP.OpenChannel(ConsoleName, ConsoleChannelID)
                            'OpenChannel()
                        ElseIf ChannelName.StartsWith("#") AndAlso ChannelName.Length > 1 Then
                            Send = False
                            IRCClient.Join(ChannelName)
                            ConsoleWrite("Opening IRC Channel " & ChannelName & ".")
                        Else
                            If Regex.IsMatch(ChannelName, "^(.+)@IRC$", RegexOptions.IgnoreCase) Then
                                CP.OpenPrivate(ChannelName)
                                Send = False
                            End If
                        End If
                    Case &H64, &H65, &H66, &H67, &H68, &H6A, &H6B, &H6C, &H6D, &H6F, &H70, &H71, &H72
                        Proxy.LastAction = Date.Now.Ticks
                        LastActivity = Date.Now
                    Case Else
                        Proxy.LastAction = Date.Now.Ticks
                        'ConsoleWrite(BytesToStr(bytBuffer))
                End Select
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

        Private Sub Proxy_PacketFromServer(ByRef bytBuffer() As Byte, ByRef Block As Boolean) Handles Proxy.PacketFromServer
            Try
                Static SP As New ServerPacketBuilder(Proxy)
                Static CP As New ClientPacketBuilder(Proxy)
                If Not IsGreetingSent Then
                    Static FirstTime As Boolean = True
                    If FirstTime Then
                        FirstTime = False
                        Client.Title = BotName & " - " & Client.CharacterName
                    End If

                    GreetingSentTry += 1
                    If GreetingSentTry >= 2 Then
                        IsGreetingSent = True
                        CP.OpenChannel(ConsoleName, ConsoleChannelID)
                        'OpenChannel()
                        GreetingTimerObj.StartTimer(1500)
                        Client.MapTiles.RefreshMapBeginning()
                        MapReaderTimerObj.StartTimer()
                        If Consts.TTMessagesEnabled Then
                            TTMessagesTimerObj.StartTimer(2000)
                        End If
                        If Consts.AutoOpenBackpack Then
                            Dim ItemID As Integer = 0
                            Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (ITibia.InventorySlots.Backpack - 1)), ItemID, 2)
                            If Client.Objects.HasFlags(ItemID, IObjects.ObjectFlags.IsContainer) Then
                                SP.UseObject(ItemID, ITibia.InventorySlots.Backpack, 0)
                                'Proxy.SendPacketToServer()
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
                Dim Loc As ITibia.LocationDefinition
                Dim ID As Integer = 0
                Dim PacketLength As UShort = GetWord(bytBuffer, Pos) + 2
                Dim PacketID As Integer = 0
                Dim Word As UShort = 0
                Dim OneByte As Byte = 0
                'Trace.WriteLine("FromServer: " & BytesToStr(bytBuffer))
                'ConsoleWrite(BytesToStr(bytBuffer))
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
                            If ID = &H851 Or ID = &H850 Then
                                MagicWallAdd(Loc)
                            End If
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
                            ElseIf Client.Objects.HasFlags(ID, IObjects.ObjectFlags.IsFluidContainer) Then
                                Pos += 1
                            ElseIf Client.Objects.HasFlags(ID, IObjects.ObjectFlags.IsContainer) Then
                                If TTBState = BotState.Paused Then Exit Sub
                                If LooterTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                                    If String.IsNullOrEmpty(Client.Objects.Name(ID)) Then 'if its known container, skip
                                        Dim BL As New BattleList
                                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                        If BL.GetDistanceFromLocation(Loc) <= Consts.LootMaxDistance Then
                                            If CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                                                If Not CavebotForm.LootFromCorpses.Checked AndAlso Not CavebotForm.EatFromCorpses.Checked Then
                                                    Continue While
                                                End If
                                                'WriteMemory(Consts.ptrGoToX, 0, 1)
                                                'WriteMemory(Consts.ptrGoToY, 0, 1)
                                                'WriteMemory(Consts.ptrGoToZ, 0, 1)
                                                BL.IsWalking = False
                                                CBContainerCount = (New Container).ContainerCount
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
                            ElseIf Client.Objects.HasExtraByte(ID) Then
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
                            ElseIf Client.Objects.HasExtraByte(ID) Then
                                Pos += 1
                            End If
                        Case &H6C 'remove item from map
                            'Trace.WriteLine("FromServer: " & BytesToStr(bytBuffer))
                            Pos += 6 'loc + Integer
                        Case &H6D 'move creature
                            Pos += 11 'loc + Integer + loc
                        Case &H6E 'get container = Container is opened by server and sent to client.
                            LooterNextExecution = 0
                            LootHasChanged = 2
                            Pos += 3 'container index, itemid
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word + 2 'name,size,hasparent
                            OneByte = GetByte(bytBuffer, Pos)
                            For I As Integer = 1 To OneByte
                                ID = GetWord(bytBuffer, Pos)
                                If Client.Objects.HasExtraByte(ID) Then
                                    Pos += 1
                                End If
                            Next
                        Case &H6F 'close container
                            Pos += 1 'containerindex
                        Case &H70 'add item to container
                            Pos += 1
                            ID = GetWord(bytBuffer, Pos)
                            If Client.Objects.HasExtraByte(ID) Then
                                Pos += 1
                            End If
                        Case &H71 'update container item
                            Pos += 2
                            ID = GetWord(bytBuffer, Pos)
                            If Client.Objects.HasExtraByte(ID) Then
                                Pos += 1
                            End If
                        Case &H72 'remove container item
                            Pos += 2
                        Case &H78
                            Pos += 1 'slot
                            ID = GetWord(bytBuffer, Pos)
                            If Client.Objects.HasExtraByte(ID) Then
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
                                If Client.Objects.HasExtraByte(ID) Then
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
                            'ConsoleWrite(BytesToStr(bytBuffer))
                            If TTBState = BotState.Paused Then Exit Sub
                            Dim FromBL As New BattleList
                            Dim ToBl As New BattleList
                            Dim Type As Integer = 0
                            Dim FromFound As Boolean = FromBL.Find(GetLocation(bytBuffer, Pos), True)
                            Dim ToFound As Boolean = ToBl.Find(GetLocation(bytBuffer, Pos), True)
                            Type = GetByte(bytBuffer, Pos)
                            If Not (FromFound And ToFound) Then Continue While
                            'ConsoleWrite("Projectile type: " & Type.ToString & " (" & FromBL.GetName & "->" & ToBl.GetName & ") ")
                            If ComboBotEnabled Then
                                If Type = 32 Then 'SD rune
                                    Dim z As Integer, IsLeader As Boolean = False
                                    For z = 1 To Combobotleaders.Count
                                        If Combobotleaders.Item(z).ToString.ToLower = FromBL.GetName.ToLower Then
                                            IsLeader = True
                                        End If
                                    Next
                                    If IsLeader Then
                                        SP.UseHotkey(Client.Objects.ID("Sudden Death"), ToBl.GetEntityID)
                                        'Proxy.SendPacketToServer(PacketUtils.UseHotkey(, ToBl.GetEntityID))
                                    End If
                                End If
                            End If
                        Case &H86 'direct hit, black square
                            If TTBState = BotState.Paused Then Exit Sub
                            Dim AttackedID As Integer = 0
                            Dim EntityID As Integer = 0
                            EntityID = GetDWord(bytBuffer, Pos)
                            If (AutoAttackerActivated) Then
                                If (New BattleList).IsPlayer(EntityID) OrElse EntityID = AutoAttackerIgnoredID Then Exit Sub
                                Client.ReadMemory(Consts.ptrAttackedEntityID, AttackedID, 4)
                                If AttackedID = 0 Then
                                    Client.WriteMemory(Consts.ptrFollowedEntityID, AutoAttackerIgnoredID, 4)
                                    Client.WriteMemory(Consts.ptrAttackedEntityID, EntityID, 4)
                                    SP.AttackEntity(EntityID)
                                    'Proxy.SendPacketToServer(AttackEntity(EntityID))
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
                                Client.ReadMemory(Consts.ptrLastAttackedEntityID, LastAttackedID, 4)
                                If ID = LastAttackedID Then
                                    Dim BL As New BattleList
                                    Dim Name As String = 0
                                    If Not BL.Find(ID) Then Continue While
                                    Name = BL.GetName()
                                    If Creatures.Creatures.ContainsKey(Name) Then
                                        Dim N As Integer = (NextLevelExp - Experience) / (Creatures.Creatures(Name).Experience * Consts.CreatureExpMultiplier)
                                        CP.SystemMessage(SysMessageType.StatusSmall, "You need to kill " & N & " " & Name & " to level up.")
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
                            Dim Condition As Scripting.ITibia.Conditions = CType(GetWord(bytBuffer, Pos), Scripting.ITibia.Conditions)
                            If Not MagicShieldActivated AndAlso CBool((Condition And Scripting.ITibia.Conditions.MagicShield) = Scripting.ITibia.Conditions.MagicShield) Then 'got magic shield plx
                                MagicShieldActivated = True
                            ElseIf MagicShieldActivated AndAlso Not CBool((Condition And Scripting.ITibia.Conditions.MagicShield)) Then
                                MagicShieldActivated = False
                                CP.SystemMessage(SysMessageType.Information, "Your Magic Shield is now over.")
                                'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Your Magic Shield is now over."))
                            End If
                            Client.RaiseEvent(ITibia.EventKind.CharacterConditionsChanged, New Events.CharacterConditionsChangedEventArgs(Condition))
                        Case &HAA 'received message
                            GetDWord(bytBuffer, Pos)
                            Dim Name As String = ""
                            Dim Level As Integer = 0
                            Dim Message As String = ""
                            Name = GetString(bytBuffer, Pos)
                            Level = GetWord(bytBuffer, Pos)
                            Dim MType As Byte = GetByte(bytBuffer, Pos)
                            Dim DefaultMessageType As ITibia.DefaultMessageType
                            Dim PrivateMessageType As ITibia.PrivateMessageType
                            Dim ChannelMessageType As ITibia.ChannelMessageType
                            Dim MessageType As ITibia.MessageType
                            Select Case MType
                                Case 1 To 3, 10 To 11
                                    DefaultMessageType = MType
                                    MessageType = ITibia.MessageType.Default
                                Case 4, &HB
                                    PrivateMessageType = MType
                                    MessageType = ITibia.MessageType.PrivateMessage
                                Case 5, &HA, &HC, &HE
                                    ChannelMessageType = MType
                                    MessageType = ITibia.MessageType.Channel
                                Case 9 'broadcast
                                    Message = GetString(bytBuffer, Pos)
                                    Continue While
                                Case Else
                                    Exit Sub 'ok unexpected!
                            End Select
                            Select Case MessageType
                                'cant add monstersay or monsteryell here... or we will have the message alarm alerting when there is no reason to do so
                                Case ITibia.MessageType.Default  ', ConstantsModule.MessageType.MonsterSay, ConstantsModule.MessageType.MonsterYell 
                                    Loc = GetLocation(bytBuffer, Pos)
                                    Message = GetString(bytBuffer, Pos)
                                    If DefaultMessageType <> ITibia.DefaultMessageType.MonsterSay AndAlso DefaultMessageType <> ITibia.DefaultMessageType.MonsterYell Then
                                        MessageAlarm(ITibia.MessageType.Default, Name, Level, Loc, Message)
                                    End If
                                    Client.RaiseEvent(ITibia.EventKind.MessageReceived, New Events.MessageReceivedEventArgs(ITibia.MessageType.Default, Name, Level, Loc, Message, DefaultMessageType))
                                Case ITibia.MessageType.Channel
                                    Dim Channel As ITibia.Channel = CType(GetWord(bytBuffer, Pos), ITibia.Channel)
                                    'MsgBox(BytesToStr(bytBuffer))
                                    Message = GetString(bytBuffer, Pos)
                                    If TradeWatcherActive AndAlso Channel = ITibia.Channel.Trade AndAlso Not Name.Equals(Client.CharacterName) Then
                                        If Regex.IsMatch(Message, TradeWatcherRegex, RegexOptions.IgnoreCase) Then
                                            CP.SystemMessage(SysMessageType.Information, "Offer: " & Name & "[" & Level & "]: " & Message)
                                            'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Offer: " & Name & "[" & Level & "]: " & Message))
                                        End If
                                    End If
                                    'MessageAlarm(ChannelMessageType, Name, Level, Loc, Message)
                                    Client.RaiseEvent(ITibia.EventKind.MessageReceived, New Events.MessageReceivedEventArgs(ITibia.MessageType.Channel, Name, Level, Loc, Message, , ChannelMessageType, , Channel))
                                Case ITibia.MessageType.PrivateMessage 'private message
                                    Message = GetString(bytBuffer, Pos)
                                    MessageAlarm(ITibia.MessageType.PrivateMessage, Name, Level, Loc, Message)
                                    Client.RaiseEvent(ITibia.EventKind.MessageReceived, New Events.MessageReceivedEventArgs(ITibia.MessageType.PrivateMessage, Name, Level, Loc, Message, , , PrivateMessageType))
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
                            'ConsoleWrite(BytesToStr(bytBuffer))
                            'Dim Nick As String = GetString(bytBuffer, Pos)
                            'If Regex.IsMatch(Nick, "") Then
                            'skip = False
                            'End If
                            Word = GetWord(bytBuffer, Pos)
                            Pos += Word
                            'Dim Name As String = ""
                            'Name = 
                            'GetString(bytBuffer, Pos)
                            'If Regex.IsMatch(Name, "^[^@]+@irc$", RegexOptions.IgnoreCase) Then

                            'End If
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
                'End
            End Try
        End Sub

        Private Sub MessageAlarm(ByVal MessageType As ITibia.MessageType, ByVal Name As String, ByVal Level As Integer, ByVal Loc As ITibia.LocationDefinition, ByVal Message As String)
            Try
                Dim Alert As Boolean = True
                Dim Player As String
                Dim Output As String = ""
                If String.Compare(Name, Client.CharacterName) = 0 Then Exit Sub
                'If TibiaWindowState = ITibia.WindowStates.Active OrElse Not Consts.FlashTaskbarWhenMessaged Then
                'Exit Sub
                'Client.FlashWindow()
                '					Dim FWI As New Tibia.FlashWInfo(Client.GetWindowHandle, Win32API.FlashWFlags.FLASHW_TIMERNOFG Or Win32API.FlashWFlags.FLASHW_TRAY Or Win32API.FlashWFlags.FLASHW_CAPTION, 0, 0)
                ''				Tibia.FlashWindowEx(FWI)
                'End If
                Select Case MessageType
                    Case ITibia.MessageType.Default
                        If Consts.FlashTaskbarWhenMessaged AndAlso Not Consts.FlashTaskbarWhenPMOnly Then
                            If Not (Not Consts.FlashTaskbarWhenSpell AndAlso MessageIsSpell(Message)) Then
                                'Dim CP As New ClientPacketBuilder(Proxy)
                                'CP.SystemMessage(ITibia.SysMessageType.EventAdvance, "Flashed window!")
                                'CP.SystemMessage(ITibia.SysMessageType.StatusConsoleBlue, "Flashed window!")
                                Client.FlashWindow()
                            End If
                        End If
                        Dim _X, _Y As Integer
                        If Loc.Z <> CharacterLoc.Z Then Exit Sub ' Ignore messages in other floors
                        _X = CharacterLoc.X - Loc.X
                        _Y = CharacterLoc.Y - Loc.Y
                        If Sqrt(Pow(_X, 2) + Pow(_Y, 2)) > 25 Then Exit Sub ' Ignore messages if far screen
                        'ConsoleWrite("(" & Loc.X & "," & Loc.Y & "," & Loc.Z & ") y (" & CharacterLoc.X & "," & CharacterLoc.Y & "," & CharacterLoc.Z & ")")
                        Output = Name & "[" & Level & "] said in public: " & Message
                        Log("Message", Output)
                        If Not AlarmsForm.MessagePublic.Checked Then Exit Sub
                    Case ITibia.MessageType.PrivateMessage
                        If Consts.FlashTaskbarWhenMessaged Then
                            Client.FlashWindow()
                        End If
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
                If TibiaWindowState <> ITibia.WindowStates.Active AndAlso Consts.FlashTaskbarWhenAlarmFires Then
                    Client.FlashWindow()
                End If

                If Consts.MusicalNotesOnAlarm Then
                    Dim CP As New ClientPacketBuilder(Proxy)
                    CP.AnimationEffect(CharacterLoc, ITibia.AnimationEffects.MusicalNotesWhite)
                End If

                'Proxy.SendPacketToClient(MagicEffect(CharacterLoc, MagicEffects.MusicalNotesWhite))
                Dim ChatMessage As New ChatMessageDefinition
                If AlarmsForm.MessagePlaySound.Checked Then
                    Dim Sound As New Audio
                    Try
                        Select Case MessageType
                            Case ITibia.MessageType.Default
                                If AlarmsForm.MessagePublic.Checked Then Sound.Play(ExecutablePath & "\Alarms\Public Message.wav", AudioPlayMode.Background)
                            Case ITibia.MessageType.PrivateMessage
                                If AlarmsForm.MessagePrivate.Checked Then Sound.Play(ExecutablePath & "\Alarms\Private Message.wav", AudioPlayMode.Background)
                        End Select
                    Catch
                    End Try
                End If
                If AlarmsForm.MessageLogOut.Checked Then
                    Dim SP As New ServerPacketBuilder(Proxy)
                    SP.PlayerLogout()
                    'Core.Proxy.SendPacketToServer(PacketUtils.PlayerLogout)
                    Log("Message Alarm", "Logging out.")
                End If

                If AlarmsForm.MessageForwardMessage.Checked AndAlso AlarmsForm.MessageForwardMessageInput.Text.Length > 0 Then
                    ChatMessage.Message = Output
                    ChatMessage.MessageType = ITibia.MessageType.PrivateMessage
                    ChatMessage.PrivateMessageType = ITibia.PrivateMessageType.Normal
                    ChatMessage.Destinatary = AlarmsForm.MessageForwardMessageInput.Text
                    ChatMessage.Prioritize = True
                    ChatMessageQueueList.Add(ChatMessage)
                End If
                If AlarmsForm.MessagePauseBot.Checked Then
                    Kernel.ConsoleWrite("Message alarm was fired while Pause Bot action was enabled." & Ret & _
                                        "Bot is now paused and you can unpause bot typing &state running or disabling Alarms")
                    Kernel.TTBState = BotState.Paused
                End If
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
#End Region

#Region " Console R/W "

        Public Sub ConsoleRead(ByVal strString As String) Implements IKernel.ConsoleRead
            Try
                Log("ConsoleRead", strString)
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Tutor, strString)
                'Proxy.SendPacketToClient(CreatureSpeak(Core.Client.CharacterName, MessageType.ChannelTutor, Level, strString, 0, 0, 0, ChannelType.Console))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub ConsoleWrite(ByVal strString As String) Implements IKernel.ConsoleWrite
            Try
                Log("ConsoleWrite", strString)
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.SystemMessage(SysMessageType.StatusSmall, strString)
                CP.Speak(ConsoleName, 0, ITibia.ChannelMessageType.Normal, strString)
                'Proxy.SendPacketToClient(CreatureSpeak(ConsoleName, MessageType.Channel, ConsoleLevel, strString, 0, 0, 0, ChannelType.Console))
                'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, strString))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub ConsoleError(ByVal strString As String) Implements IKernel.ConsoleError
            Try
                Log("ConsoleError", strString)
                Dim CP As New ClientPacketBuilder(Proxy)
                CP.SystemMessage(SysMessageType.StatusSmall, strString)
                CP.Speak(ConsoleName, 0, ITibia.ChannelMessageType.GameMaster, strString)
                'Proxy.SendPacketToClient(CreatureSpeak(ConsoleName, MessageType.ChannelGM, ConsoleLevel, strString, 0, 0, 0, ChannelType.Console))
                'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, strString))
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

#End Region

#Region " Methods "

#Region " Favored Weapon Add/Remove "
        Function FWAdd(ByVal ItemID As Integer, ByVal Hand As Short, ByVal Monsters As String) As Boolean
            Try
                Dim MonstersArr() As String
                Dim NewFavoredWeapon As KernelModule.WeaponFavoritDefinition
                MonstersArr = Split(Monsters, ", ")
                For i As Short = LBound(MonstersArr) To UBound(MonstersArr)
                    If MonstersArr(i) <> Nothing Then
                        NewFavoredWeapon.WeaponID = ItemID
                        NewFavoredWeapon.Hand = Hand
                        NewFavoredWeapon.Monster = MonstersArr(i).ToLower
                        FavoredWeapon.Add(NewFavoredWeapon)
                    End If
                Next
                Return True
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function
        Function FWRemove(ByVal ItemID As Integer) As Boolean
            Try
                Dim WasRemoved As Boolean = False
                For Each FW As WeaponFavoritDefinition In FavoredWeapon
                    If FW.WeaponID = ItemID Then
                        FW.Monster = Nothing
                        WasRemoved = True
                    End If
                Next
                Return WasRemoved
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function
#End Region

        Public Function NewBattlelist() As Scripting.IBattlelist Implements Scripting.IKernel.NewBattlelist
            Try
                Return New BattleList
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
        End Function

        Public Function NewBattlelist(ByVal Position As Integer) As Scripting.IBattlelist Implements Scripting.IKernel.NewBattlelist
            Try
                Return New BattleList(Position)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
        End Function

        Public Function NewBattlelist(ByVal SE As Scripting.IBattlelist.SpecialEntity) As Scripting.IBattlelist Implements Scripting.IKernel.NewBattlelist
            Try
                Return New BattleList(SE)
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
        End Function

        Public Function NewContainer() As Scripting.IContainer Implements Scripting.IKernel.NewContainer
            Try
                Return New Container()
            Catch Ex As Exception
                MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
        End Function
#End Region

#Region " Properties "
        Public ReadOnly Property GetIrcClient() As IIrcClient Implements IKernel.IrcClient
            Get
                Try
                    Return IRCClient
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property GetSpells() As Scripting.ISpells Implements IKernel.Spells
            Get
                Try
                    Return Spells
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property GetCommandParser() As Scripting.ICommandParser Implements IKernel.CommandParser
            Get
                Try
                    Return CommandParser
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property GetClient() As ITibia Implements IKernel.Client
            Get
                Try
                    Return Client
                Catch Ex As Exception
                    MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property GetProxy() As IProxy Implements IKernel.Proxy
            Get
                Try
                    Return Proxy
                Catch ex As Exception
                    MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property Computer() As Microsoft.VisualBasic.Devices.Computer Implements Scripting.IKernel.Computer
            Get
                Return My.Computer
            End Get
        End Property

        Public ReadOnly Property NotifyIcon() As NotifyIcon Implements Scripting.IKernel.NotifyIcon
            Get
                Return _NotifyIcon
            End Get
        End Property
#End Region

    End Class

End Module
