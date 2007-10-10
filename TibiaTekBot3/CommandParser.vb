Imports System.Text.RegularExpressions, System.Diagnostics, System.Runtime.InteropServices, _
    System.ComponentModel, TibiaTekBot3.CoreModule, TibiaTekBot3.CoreModule.CoreClass

Public Module CommandParserModule

    Public Sub CommandParser(ByVal Message As String)

        Dim MatchObj As Match = Regex.Match(Message, "^([a-zA-Z]+)\s*([^;]*)$")
        If MatchObj.Success Then
            Select Case MatchObj.Groups(1).Value.ToLower
                'Function calls here
                Case "test"
                    CmdTest()
                Case "light"
                    CmdLight(MatchObj.Groups)
                Case "stop", "resume", "pause", "start", "halt", "continue", "freeze"
                    CmdBotState(MatchObj.Groups)
                Case "exp"
                    CmdExp(MatchObj.Groups)
                Case "heal", "heal", "autoheal", "autohealer"

                Case "spell", "spellcaster"
                    CmdSpell(MatchObj.Groups)
                Case "eat", "eater", "ate"
                    CmdEat(MatchObj.Groups)
                Case "runemaker"
                    CmdRunemaker(MatchObj.Groups)
                Case "fish", "fisher"
                    CmdFisher(MatchObj.Groups)
                Case "advertise", "advertiser"
                    CmdAdvertise(MatchObj.Groups)
                Case "watch", "watcher"
                    CmdTradeWatcher(MatchObj.Groups)
                Case "wasd"
                    CmdWasd(MatchObj.Groups)
                Case "fpschanger", "fps"
                    CmdFpsChanger(MatchObj.Groups)
                Case "statsuploader"
                    CmdStatusUploader(MatchObj.Groups)
                Case "namespy"
                    CmdNameSpy(MatchObj.Groups)
                Case Else
                    Core.ConsoleError("This command does not exist." & Ret & _
                     "  For a list of available commands type: &help.")
            End Select
        End If
    End Sub
    'And Functions here!

#Region " Test Command "
    Public Sub CmdTest()
        Core.ConsoleWrite("Begin Test")
        Core.SendPacketToClient(PacketUtils.TestPacket)
        Core.ConsoleWrite("End Test")
    End Sub
#End Region

#Region " Name Spy Command "

    Private Sub CmdNameSpy(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).Value)
            Case 0
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy, Core.Consts.NameSpyDefault, 2)
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy2, Core.Consts.NameSpy2Default, 2)
                Core.ConsoleWrite("Name Spy is now Disabled.")
            Case 1
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy, &H9090, 2)
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy2, &H9090, 2)
                Core.ConsoleWrite("Name Spy is now Enabled.")
            Case Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
        End Select
    End Sub

#End Region

#Region " Auto Healer Command "

    Private Sub CmdHeal(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString.ToLower
        Select Case StrToShort(Value)
            Case 0
                Core.HealTimerObj.StopTimer()
                Core.HealMinimumHP = 0
                Core.HealComment = ""
                Core.ConsoleWrite("Auto Healer is now Disabled.")
            Case Else
                Dim RegExp As New Regex("([1-9][0-9]{0,4}%?)\s+""([^""]+)(?:""?(?:\s*""""([^""]+))?)?")
                Dim Match As Match = RegExp.Match(Value)
                If Match.Success Then
                    Dim Match2 As Match = Regex.Match(Match.Groups(1).Value, "([1-9][0-9])%")
                    If Match2.Success Then
                        'Dim MaxHitPoints As Integer = 0
                        'Core.Read(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                        Core.HealMinimumHP = Core.CharacterMaxHitPoints * (CInt(Match2.Groups(1).Value) / 100)
                    Else
                        Core.HealMinimumHP = CInt(Match.Groups(1).Value)
                    End If
                    For Each Spell As SpellDefinition In Core.Spells.SupportiveSpells
                        If String.Compare(Spell.Name, Match.Groups(2).Value, True) = 0 OrElse String.Compare(Spell.Words, Match.Groups(2).Value, True) = 0 Then
                            Select Case Spell.Name.ToLower
                                Case "light healing", "heal friend", "mass healing", "intense healing", "ultimate healing"
                                    Core.HealSpell = Spell
                                    Exit For
                                Case Else
                                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                    Exit Sub
                            End Select
                        End If
                    Next
                    If Match.Groups(3).Value.Length > 0 Then
                        Core.HealComment = Match.Groups(3).Value
                    Else
                        Core.HealComment = ""
                    End If
                    Core.HealTimerObj.StartTimer()
                    Core.ConsoleWrite("Auto Healer is now Enabled.")
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
        End Select
    End Sub

#End Region

#Region " WASD "

    Private Sub CmdWasd(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).Value
        Select Case StrToShort(Value)
            Case 0
                Core.Tibia.SetWasd(False)
                Core.ConsoleWrite("WASD is now Disabled.")
            Case 1
                Core.Tibia.SetWasd(True)
                Core.ConsoleWrite("WASD is now Enabled.")
            Case Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
        End Select
    End Sub

#End Region

#Region " Experience Checker "

    Private Sub CmdExp(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).Value
        Select Case StrToShort(Value)
            Case 0
                Core.ExpCheckerActivated = False
                Core.LastExperience = 0
                Core.Tibia.Title = BotName & " - " & Core.CharacterName
                Core.ConsoleWrite("Experience Checker is now Disabled.")
            Case 1
                If Core.FakingTitle Then
                    Core.FakingTitle = False
                    Core.ConsoleWrite("Fake Title is now Disabled.")
                End If
                Core.LastExperience = 0
                Core.ExpCheckerActivated = True
                Core.ConsoleWrite("Experience Checker is now Enabled.")
            Case Else
                Dim MatchObj As Match = Regex.Match(Value, "creatures?\s+([a-zA-Z]+)", RegexOptions.IgnoreCase)
                If MatchObj.Success Then
                    Select Case StrToShort(MatchObj.Groups(1).Value.ToLower)
                        Case 0
                            Core.ShowCreaturesUntilNextLevel = False
                            Core.ConsoleWrite("Showing creatures until next level is now Disabled.")
                        Case 1
                            Core.ShowCreaturesUntilNextLevel = True
                            Core.ConsoleWrite("Showing creatures until next level is now Enabled.")
                        Case Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End Select
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
        End Select
    End Sub

#End Region

#Region " Start/Pause/Resume/Stop Command "

    Private Sub CmdBotState(ByVal Arguments As GroupCollection)
        Select Case Arguments(1).Value
            Case "stop", "halt"
                Core.Stop()
                Core.ConsoleWrite("The bot is now stopped. All operations have been canceled.")
            Case "resume", "start", "continue"
                Core.Resume()
                Core.ConsoleWrite("The bot is now resuming all operations.")
            Case "pause", "freeze"
                Core.Pause()
                Core.ConsoleWrite("The bot is now paused.")
        End Select
    End Sub

#End Region

#Region " Light Effect Command "

    Private Sub CmdLight(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.SetLight(LightIntensity.Small, LightColor.UtevoLux)
                Core.LightEffectTimerObj.StopTimer()
                Core.ConsoleWrite("Light Effect is now Disabled.")
            Case 1
                Core.LightC = LightColor.BrightSword
                Core.LightI = LightIntensity.Inmense
                Core.ConsoleWrite("Full Light Effect is now Enabled.")
                Core.LightEffectTimerObj.StartTimer()
            Case Else
                Dim strOutput As String = "{0} Light Effect is now Enabled."
                Select Case Value.ToLower()
                    Case "torch"
                        Core.LightI = LightIntensity.Medium
                        Core.LightC = LightColor.Torch
                        Core.ConsoleWrite("Torch Light Effect is now Enabled.")
                    Case "great torch"
                        Core.LightI = LightIntensity.VeryLarge
                        Core.LightC = LightColor.Torch
                        Core.ConsoleWrite("Great Torch Light Effect is now Enabled.")
                    Case "ultimate torch"
                        Core.LightI = LightIntensity.Huge
                        Core.LightC = LightColor.Torch
                        Core.ConsoleWrite("Ultimate Torch Light Effect is now Enabled.")
                    Case "utevo lux"
                        Core.LightI = LightIntensity.Medium
                        Core.LightC = LightColor.UtevoLux
                        Core.ConsoleWrite("Utevo Lux Light Effect is now Enabled.")
                    Case "utevo gran lux"
                        Core.LightI = LightIntensity.Large
                        Core.LightC = LightColor.UtevoLux
                        Core.ConsoleWrite("Utevo Gran Lux Light Effect is now Enabled.")
                    Case "utevo vis lux"
                        Core.LightI = LightIntensity.VeryLarge
                        Core.LightC = LightColor.UtevoLux
                        Core.ConsoleWrite("Utevo Vis Lux Light Effect is now Enabled.")
                    Case "light wand"
                        Core.LightI = LightIntensity.Large
                        Core.LightC = LightColor.LightWand
                        Core.ConsoleWrite("Light Wand Light Effect is now Enabled.")
                    Case Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        Exit Sub
                End Select
                Core.LightEffectTimerObj.StartTimer()
        End Select
    End Sub



#End Region

#Region " Spell Caster Command "

    Private Sub CmdSpell(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).ToString)
            Case 0
                Core.SpellManaRequired = 0
                Core.SpellMsg = ""
                Core.SpellTimerObj.StopTimer()
                Core.StatusMessage("Spell Caster is now Disabled.")
                RemoveFeature("Spellcaster")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.SpellTimerObj.StopTimer()
                    Core.StatusMessage("Spell Caster is now Paused.")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "^([1-9][0-9]{1,4})\s+""?(.+)$")
                If MatchObj.Success Then
                    Core.SpellManaRequired = CUInt(MatchObj.Groups(1).ToString)
                    Core.SpellMsg = MatchObj.Groups(2).ToString
                    Core.SpellTimerObj.StartTimer()
                    Core.StatusMessage("Spell Caster is now Enabled.")
                Else
                    Core.StatusMessage("Error in Spellcaster")
                End If
        End Select
    End Sub
#End Region

#Region " Auto Eater Command "

    Private Sub CmdEat(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.AutoEaterSmart = 0
                Core.EaterTimerObj.StopTimer()
                Core.StatusMessage("Auto Eater is now Disabled.")
                RemoveFeature("Auto Eater")
            Case 1
                Core.AutoEaterSmart = 0
                Core.EaterTimerObj.Interval = 30000
                Core.EaterTimerObj.StartTimer()
                Core.StatusMessage("Auto Eater is now Enabled for every 30 seconds.")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.EaterTimerObj.StopTimer()
                    Core.StatusMessage("Auto Eater is now Paused.")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "smart\s+([1-9][0-9]{1,4})")
                If MatchObj.Success Then
                    Core.AutoEaterSmart = CInt(MatchObj.Groups(1).ToString)
                    Core.EaterTimerObj.Interval = 60000
                    Core.EaterTimerObj.StartTimer()
                    Core.StatusMessage("Auto Eater will eat only when you are below " & Core.AutoEaterSmart & " hit points, once every minute.")
                Else
                    MatchObj = Regex.Match(Arguments(2).ToString, "(\d{1,3})")
                    If MatchObj.Success Then
                        Core.AutoEaterSmart = 0
                        Core.EaterTimerObj.Interval = CInt(MatchObj.Groups(1).ToString) * 1000
                        Core.EaterTimerObj.StartTimer()
                        Core.StatusMessage("Auto Eater is now Enabled for every " & ((Core.EaterTimerObj.Interval / 1000) Mod 1000) & " second(s).")
                    Else
                        MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdEat")
                    End If
                End If
        End Select

    End Sub

#End Region

#Region " Runemaker Command "

    Private Sub CmdRunemaker(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.RunemakerManaPoints = 0
                Core.RunemakerSoulPoints = 0
                Core.RunemakerTimerObj.StopTimer()
                Core.ConsoleWrite("Runemaker is now Disabled.")
            Case Else
                Dim RegExp As New Regex("([1-9][0-9]{1,4})\s+([1-9][0-9]{0,2})\s+""([^""]+)""?")
                Dim Match As Match = RegExp.Match(Value)
                If Match.Success Then
                    Dim Found As Boolean = False
                    Dim C As New ConjureDefinition
                    For Each Conjure As ConjureDefinition In Core.Spells.Conjures
                        If String.Compare(Conjure.Name, Match.Groups(3).ToString, True) = 0 _
                        OrElse String.Compare(Conjure.Words, Match.Groups(3).ToString, True) = 0 Then
                            C = Conjure
                            Found = True
                            Exit For
                        End If
                    Next
                    If Found Then
                        Core.RunemakerConjure = C
                        Core.RunemakerManaPoints = CShort(Match.Groups(1).ToString)
                        Core.RunemakerSoulPoints = CInt(Match.Groups(2).ToString)
                        Core.RunemakerTimerObj.StartTimer()
                        Core.ConsoleWrite("Runemaker will now make " & C.Name & " when you have more than " & _
                            Core.RunemakerManaPoints & " mana points and more than " & Core.RunemakerSoulPoints & " soul points.")
                    Else
                        Core.ConsoleError("Invalid Conjure: Spell Name or Spell Words .")
                    End If
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
        End Select
    End Sub

#End Region

#Region " Fisher Command "

    Private Sub CmdFisher(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.FisherMinimumCapacity = 0
                Core.FisherSpeed = 0
                Core.FisherTimerObj.StopTimer()
                Core.ConsoleWrite("Auto Fisher is now Disabled.")
            Case Else
                Dim MatchObj As Match = Regex.Match(Value, "^(\d{1,3})(?:\s+(\S+))?$")
                If MatchObj.Success Then
                    Select Case MatchObj.Groups(2).Value.ToLower
                        Case "normal", "default", ""
                            Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                            Core.FisherSpeed = 0
                            Core.FisherTimerObj.StartTimer()
                            Core.ConsoleWrite("Auto Fisher is now Enabled.")
                        Case "turbo", "nitro", "fast", "faster", "fastest"
                            Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                            Core.FisherSpeed = 500
                            Core.FisherTimerObj.StartTimer()
                            Core.ConsoleWrite("Auto Fisher (Turbo Mode) is now Enabled.")
                        Case Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End Select
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
        End Select
    End Sub

#End Region

#Region " Advertise Command "

    Private Sub CmdAdvertise(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.AdvertiseMsg = ""
                Core.AdvertiseTimerObj.StopTimer()
                Core.ConsoleWrite("Trade Channel Advertiser is now Disabled.")
            Case Else
                Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, """([^""]+)""?")
                If MatchObj.Success Then
                    'OpenChannel("Trade", ChannelType.Trade)
                    Core.AdvertiseMsg = MatchObj.Groups(1).ToString
                    Core.ConsoleWrite("Trade Channel Advertiser is now Enabled. Make sure the Trade Channel is opened.")
                    Core.AdvertiseTimerObj.StartTimer(1000)
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
        End Select

    End Sub

#End Region

#Region " Trade Channel Watcher Command "

    Private Sub CmdTradeWatcher(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.TradeWatcherActive = False
                Core.TradeWatcherRegex = ""
                Core.ConsoleWrite("Trade Channel Watcher is now Disabled.")
            Case Else
                If String.IsNullOrEmpty(Value) Then
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    Exit Sub
                End If
                Dim RegExp As Regex
                Try
                    RegExp = New Regex(Value)
                    Core.TradeWatcherRegex = Value
                    Core.TradeWatcherActive = True
                    Core.ConsoleWrite("Trade Channel Watcher will now match advertisements with the following pattern '" & Core.TradeWatcherRegex & "'. Make sure the Trade channel is opened.")
                Catch ex As Exception
                    Core.ConsoleError("Sorry, but this is not a valid regular expression." & Ret & _
                    "See http://en.wikipedia.org/wiki/Regular_expression for more information on regular expressions.")
                End Try
        End Select
    End Sub

#End Region

#Region " FPS Changer Command "

    Private Sub CmdFpsChanger(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).Value)
            Case 0
                Core.FPSChangerTimerObj.StopTimer()
                Core.Tibia.Memory.Write(Core.FrameRateBegin + Core.Consts.FrameRateLimitOffset, FPSBToX(Core.Consts.FPSWhenActive))
                Core.ConsoleWrite("FPS Changer is now Disabled.")
            Case 1
                Core.FPSChangerTimerObj.StartTimer()
                Core.ConsoleWrite("FPS Changer is now Enabled.")
            Case Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
        End Select
    End Sub

#End Region

#Region " Stats Uploader "

    Private Sub CmdStatusUploader(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).ToString)
            Case 0
                Core.StatsUploaderTimerObj.StopTimer()
                Core.ConsoleWrite("Stats Uploader is now Disabled.")
            Case 1
                If Core.Consts.StatsUploaderSaveOnDiskOnly Then
                    If Core.Consts.StatsUploaderPath.Length = 0 OrElse Core.Consts.StatsUploaderFilename.Length = 0 Then
                        Core.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                        Exit Sub
                    End If
                    Core.StatsUploaderTimerObj.Interval = Core.Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                    Core.ConsoleWrite("Stats Uploader is now Enabled.")
                Else
                    If Core.Consts.StatsUploaderUrl.Length = 0 _
                        OrElse Core.Consts.StatsUploaderUserID.Length = 0 _
                        OrElse Core.Consts.StatsUploaderPassword.Length = 0 _
                        OrElse Core.Consts.StatsUploaderFrequency = 0 Then
                        Core.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                        Exit Sub
                    End If
                    Core.StatsUploaderTimerObj.Interval = Core.Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                    Core.ConsoleWrite("Stats Uploader is now Enabled.")
                End If
            Case Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
        End Select
    End Sub

#End Region

End Module
