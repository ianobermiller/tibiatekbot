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
                    CmdHeal(MatchObj.Groups)
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
                Case "changer", "amulet changer", "necklace changer", "amulet/necklace changer"
                    cmdChanger(MatchObj.Groups)
                Case "uh", "uher"
                    CmdUH(MatchObj.Groups)
                Case "healfriend"
                    CmdHealFriend(MatchObj.Groups)
                Case Else
                    MsgBox("Error: Wrong Call to Commandparser", MsgBoxStyle.OkOnly, "Error")
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
                Core.StatusMessage("Name Spy is now Disabled.")
                RemoveFeature("Namespy")
            Case 1
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy, &H9090, 2)
                Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy2, &H9090, 2)
                Core.StatusMessage("Name Spy is now Enabled.")
            Case Else
                If Arguments(2).ToString = "hide" Then
                    Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy, Core.Consts.NameSpyDefault, 2)
                    Core.Tibia.Memory.Write(Core.Consts.ptrNameSpy2, Core.Consts.NameSpy2Default, 2)
                    Core.StatusMessage("Name Spy is now Disabled.")
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdNameSpy")
                End If
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
                Core.StatusMessage("Auto Healer is now Disabled.")
                RemoveFeature("Auto Healer")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.HealTimerObj.StopTimer()
                    Core.StatusMessage("Auto Healer is now Paused.")
                    Exit Sub
                End If
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
                                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdHeal")
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
                    Core.StatusMessage("Auto Healer is now Enabled.")
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdHeal")
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
                Core.StatusMessage("WASD is now Disabled.")
                RemoveFeature("WASD")
            Case 1
                Core.Tibia.SetWasd(True)
                Core.StatusMessage("WASD is now Enabled.")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.Tibia.SetWasd(False)
                    Core.StatusMessage("WASD is now Disabled.")
                    Exit Sub
                End If
                MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdWasd")
        End Select
    End Sub

#End Region

#Region " Experience Checker "

    Private Sub CmdExp(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).Value
        Select Case StrToShort(Value)
            Case 0
                Core.ExpCheckerActivated = False
                'Core.LastExperience = 0
                Core.Tibia.Title = BotName & " - " & Core.CharacterName
                Core.StatusMessage("Experience Checker is now Disabled.")
            Case 1
                If Core.FakingTitle Then
                    Core.FakingTitle = False
                End If
                Core.LastExperience = 0
                Core.ExpCheckerActivated = True
                Core.StatusMessage("Experience Checker is now Enabled.")
            Case Else
                If Arguments(2).ToString = "end" Then
                    Core.ExpCheckerActivated = False
                    Core.LastExperience = 0
                    Core.Tibia.Title = BotName & " - " & Core.CharacterName
                    Core.ShowCreaturesUntilNextLevel = False
                    Core.StatusMessage("Experience Checker is now Disabled.")
                    RemoveFeature("Exp Checker")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Value, "creatures?\s+([a-zA-Z]+)", RegexOptions.IgnoreCase)
                If MatchObj.Success Then
                    Select Case StrToShort(MatchObj.Groups(1).Value.ToLower)
                        Case 0
                            Core.ShowCreaturesUntilNextLevel = False
                            Core.StatusMessage("Showing creatures until next level is now Disabled.")
                        Case 1
                            Core.ShowCreaturesUntilNextLevel = True
                            Core.StatusMessage("Showing creatures until next level is now Enabled.")
                        Case Else
                            MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdExp")
                    End Select
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdExp")
                End If
        End Select
    End Sub

#End Region

#Region " Start/Pause/Resume/Stop Command "

    Private Sub CmdBotState(ByVal Arguments As GroupCollection)
        Select Case Arguments(1).Value
            Case "stop", "halt"
                Core.Stop()
                Core.StatusMessage("The bot is now stopped. All operations have been canceled.")
            Case "resume", "start", "continue"
                Core.Resume()
                Core.StatusMessage("The bot is now resuming all operations.")
            Case "pause", "freeze"
                Core.Pause()
                Core.StatusMessage("The bot is now paused.")
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
                Core.StatusMessage("Light Effect is now Disabled.")
                RemoveFeature("Light Effects")
            Case 1
                Core.LightC = LightColor.BrightSword
                Core.LightI = LightIntensity.Inmense
                Core.StatusMessage("Full Light Effect is now Enabled.")
                Core.LightEffectTimerObj.StartTimer()
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.SetLight(LightIntensity.Small, LightColor.UtevoLux)
                    Core.LightEffectTimerObj.StopTimer()
                    Core.StatusMessage("Light Effect is now Disabled.")
                    Exit Sub
                End If
                Dim strOutput As String = "{0} Light Effect is now Enabled."
                Select Case Value.ToLower()
                    Case "torch"
                        Core.LightI = LightIntensity.Medium
                        Core.LightC = LightColor.Torch
                        Core.StatusMessage("Torch Light Effect is now Enabled.")
                    Case "great torch"
                        Core.LightI = LightIntensity.VeryLarge
                        Core.LightC = LightColor.Torch
                        Core.StatusMessage("Great Torch Light Effect is now Enabled.")
                    Case "ultimate torch"
                        Core.LightI = LightIntensity.Huge
                        Core.LightC = LightColor.Torch
                        Core.StatusMessage("Ultimate Torch Light Effect is now Enabled.")
                    Case "utevo lux"
                        Core.LightI = LightIntensity.Medium
                        Core.LightC = LightColor.UtevoLux
                        Core.StatusMessage("Utevo Lux Light Effect is now Enabled.")
                    Case "utevo gran lux"
                        Core.LightI = LightIntensity.Large
                        Core.LightC = LightColor.UtevoLux
                        Core.StatusMessage("Utevo Gran Lux Light Effect is now Enabled.")
                    Case "utevo vis lux"
                        Core.LightI = LightIntensity.VeryLarge
                        Core.LightC = LightColor.UtevoLux
                        Core.StatusMessage("Utevo Vis Lux Light Effect is now Enabled.")
                    Case "light wand"
                        Core.LightI = LightIntensity.Large
                        Core.LightC = LightColor.LightWand
                        Core.StatusMessage("Light Wand Light Effect is now Enabled.")
                    Case Else
                        MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdLight")
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
                Core.StatusMessage("Runemaker is now Disabled.")
                RemoveFeature("Runemaker")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.RunemakerTimerObj.StopTimer()
                    Core.StatusMessage("Runemaker Paused.")
                    Exit Sub
                End If
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
                        Core.StatusMessage("Runemaker will now make " & C.Name & " when you have more than " & _
                            Core.RunemakerManaPoints & " mana points and more than " & Core.RunemakerSoulPoints & " soul points.")
                    Else
                        MsgBox("Invalid Conjure: Spell Name or Spell Words .")
                    End If
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdRunemaker")
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
                Core.StatusMessage("Auto Fisher is now Disabled.")
                RemoveFeature("Auto Fisher")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.FisherTimerObj.StopTimer()
                    Core.StatusMessage("Fisher Paused.")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Value, "^(\d{1,3})(?:\s+(\S+))?$")
                If MatchObj.Success Then
                    Select Case MatchObj.Groups(2).Value.ToLower
                        Case "normal", "default", ""
                            Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                            Core.FisherSpeed = 0
                            Core.FisherTimerObj.StartTimer()
                            Core.StatusMessage("Auto Fisher is now Enabled.")
                        Case "turbo", "nitro", "fast", "faster", "fastest"
                            Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                            Core.FisherSpeed = 500
                            Core.FisherTimerObj.StartTimer()
                            Core.StatusMessage("Auto Fisher (Turbo Mode) is now Enabled.")
                        Case Else
                            MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdFisher")
                    End Select
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdFisher")
                End If
        End Select
    End Sub

#End Region

#Region " Advertise Command "

    Private Sub CmdAdvertise(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.AdvertiseTimerObj.StopTimer()
                Core.StatusMessage("Trade Channel Advertiser is now Disabled.")
                RemoveFeature("Advertiser")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.AdvertiseTimerObj.StopTimer()
                    Core.StatusMessage("Trade Channel Advertiser is now Paused.")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, """([^""]+)""?")
                If MatchObj.Success Then
                    'OpenChannel("Trade", ChannelType.Trade)
                    Core.AdvertiseMsg = MatchObj.Groups(1).ToString
                    Core.StatusMessage("Trade Channel Advertiser is now Enabled. Make sure the Trade Channel is opened.")
                    Core.AdvertiseTimerObj.StartTimer(1000)
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdAdvertiser")
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
                Core.StatusMessage("Trade Channel Watcher is now Disabled.")
                RemoveFeature("Watcher")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.TradeWatcherActive = False
                    Core.StatusMessage("Trade Channel Watcher is now Paused.")
                    Exit Sub
                End If
                If String.IsNullOrEmpty(Value) Then
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdTradeWatcher")
                    Exit Sub
                End If
                Dim RegExp As Regex
                Try
                    RegExp = New Regex(Value)
                    Core.TradeWatcherRegex = Value
                    Core.TradeWatcherActive = True
                    Core.StatusMessage("Trade Channel Watcher will now match advertisements with the following pattern '" & Core.TradeWatcherRegex & "'. Make sure the Trade channel is opened.")
                Catch ex As Exception
                    MsgBox("Sorry, but the regular expression you inserted is not valid.")
                    frmSubForms.AdvertiserOnOff.Text = "Activate"
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
                Core.StatusMessage("FPS Changer is now Disabled.")
                RemoveFeature("FPS Changer")
            Case 1
                Core.FPSChangerTimerObj.StartTimer()
                Core.StatusMessage("FPS Changer is now Enabled.")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.FPSChangerTimerObj.StopTimer()
                    Core.Tibia.Memory.Write(Core.FrameRateBegin + Core.Consts.FrameRateLimitOffset, FPSBToX(Core.Consts.FPSWhenActive))
                    Core.StatusMessage("FPS Changer is now Paused.")
                    Exit Sub
                End If
                MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdFpsChanger")
        End Select
    End Sub

#End Region

#Region " Stats Uploader "

    Private Sub CmdStatusUploader(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).ToString)
            Case 0
                Core.StatsUploaderTimerObj.StopTimer()
                Core.StatusMessage("Stats Uploader is now Disabled.")
                RemoveFeature("Stats Uploader")
            Case 1
                If Core.Consts.StatsUploaderSaveOnDiskOnly Then
                    If Core.Consts.StatsUploaderPath.Length = 0 OrElse Core.Consts.StatsUploaderFilename.Length = 0 Then
                        MsgBox("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                        Exit Sub
                    End If
                    Core.StatsUploaderTimerObj.Interval = Core.Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                    Core.StatusMessage("Stats Uploader is now Enabled.")
                Else
                    If Core.Consts.StatsUploaderUrl.Length = 0 _
                        OrElse Core.Consts.StatsUploaderUserID.Length = 0 _
                        OrElse Core.Consts.StatsUploaderPassword.Length = 0 _
                        OrElse Core.Consts.StatsUploaderFrequency = 0 Then
                        MsgBox("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                        Exit Sub
                    End If
                    Core.StatsUploaderTimerObj.Interval = Core.Consts.StatsUploaderFrequency
                    Core.StatsUploaderTimerObj.StartTimer()
                    Core.StatusMessage("Stats Uploader is now Enabled.")
                End If
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.StatsUploaderTimerObj.StopTimer()
                    Core.StatusMessage("Stats Uploader is now Paused.")
                    Exit Sub
                End If
                MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdStatsUploader")
        End Select
    End Sub

#End Region

#Region " Amulet/Necklace Changer"
    Private Sub cmdChanger(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).Value)
            Case 0
                Core.AmuletChangerTimerObj.StopTimer()
                Core.AmuletId = 0
                Core.StatusMessage("Amulet/Necklace Changer is now Disabled.")
                RemoveFeature("Amulet Changer")
            Case 1
                Dim TempId As Integer
                Core.Tibia.Memory.Read(Core.Consts.ptrInventoryBegin + ((InventorySlots.Neck - 1) * Core.Consts.ItemDist), TempId, 2)
                If TempId = 0 Then
                    MsgBox("You are not wearing any amulet. Please select amulet you want to change")
                    frmSubForms.ChangerOnOff.Checked = False
                    Exit Sub
                End If
                Core.AmuletId = TempId
                Core.AmuletChangerTimerObj.StartTimer()
                Core.StatusMessage("Amulet/Necklace Chaner is now Enabled.")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.AmuletChangerTimerObj.StopTimer()
                    Core.StatusMessage("Amulet/Necklace Changer is now Paused.")
                    Exit Sub
                End If
                Core.AmuletId = Core.Definitions.GetItemID(Arguments(2).ToString)
                If Core.AmuletId = 0 Then
                    MsgBox("Invalid Amulet/Necklace Name")
                    frmSubForms.ChangerOnOff.Checked = False
                    Exit Sub
                End If
                Core.AmuletChangerTimerObj.StartTimer()
                Core.StatusMessage("Amulet/Necklace Changer is now Enabled.")
        End Select
    End Sub
#End Region

#Region " Auto UHer "
    Private Sub CmdUH(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Select Case StrToShort(Value)
            Case 0
                Core.UHTimerObj.StopTimer()
                Core.UHHPRequired = 0
                Core.StatusMessage("Auto UHer is now Disabled.")
                RemoveFeature("Auto UHer")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.UHTimerObj.StopTimer()
                    Core.StatusMessage("Auto UHer is now Paused.")
                    Exit Sub
                End If
                Dim RegExp As New Regex("[1-9][0-9]{0,4}%?")
                Dim Match As Match = RegExp.Match(Value)
                If Match.Success Then
                    Dim Match2 As Match = Regex.Match(Value, "([1-9][0-9])%")
                    If Not Match2.Success Then
                        Core.UHHPRequired = CUInt(Value)
                        Core.UHTimerObj.StartTimer()
                        Core.StatusMessage("Auto UHer will now 'UH' you if you are below " & Core.UHHPRequired & " hit points.")
                    Else
                        Core.UHHPRequired = Core.CharacterMaxHitPoints * (CInt(Match2.Groups(1).Value) / 100)
                        Core.UHTimerObj.StartTimer()
                        Core.StatusMessage("Auto UHer will now 'UH' you if you are below " & Value & " hit points.")
                    End If
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdUH")
                    frmSubForms.UHerHptxtbox.Text = "Activate"
                End If
        End Select
    End Sub
#End Region

#Region " Auto Heal Friend Command "

    Private Sub CmdHealFriend(ByVal Arguments As GroupCollection)
        Select Case StrToShort(Arguments(2).ToString)
            Case 0
                Core.HealFriendCharacterName = ""
                Core.HealFriendHealthPercentage = 0
                Core.HealFriendTimerObj.StopTimer()
                RemoveFeature("Heal Friend")
                Core.StatusMessage("Auto Heal Friend is now Disabled.")
            Case Else
                If Arguments(2).ToString = "pause" Then
                    Core.HealFriendTimerObj.StopTimer()
                    Core.StatusMessage("Auto Heal Friend is now Paused.")
                    Exit Sub
                End If
                Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]?)%?\s+""([^""]+)""\s+""([^""]+)""?")
                If MatchObj.Success Then
                    Core.HealFriendHealthPercentage = CUShort(MatchObj.Groups(1).ToString)
                    If Core.HealFriendHealthPercentage < 0 Or Core.HealFriendHealthPercentage > 99 Then
                        MsgBox("Invalid Percentage. Please specify persentages between 1-99")
                        frmSubForms.HFOnOff.Text = "Activate"
                        Exit Sub
                    End If
                    Dim HealthType As String = ""
                    Select Case MatchObj.Groups(2).ToString.ToLower
                        Case "ultimate healing", "uh", "adura vita"
                            Core.HealFriendHealType = HealTypes.UltimateHealingRune
                            HealthType = "Ultimate Healing."
                        Case "exura sio", "heal friend", "sio"
                            Core.HealFriendHealType = HealTypes.ExuraSio
                            HealthType = "Exura Sio."
                        Case "both"
                            Core.HealFriendHealType = HealTypes.Both
                            HealthType = "both Exura Sio and Ultimate Healing."
                        Case Else
                            MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdHealFriend")
                            frmSubForms.HFOnOff.Text = "Activate"
                            Exit Sub
                    End Select
                    Core.HealFriendCharacterName = MatchObj.Groups(3).Value
                    Core.HealFriendTimerObj.StartTimer()
                    Core.StatusMessage("Healing '" & Core.HealFriendCharacterName & "' when his/her hit points are less than " & Core.HealFriendHealthPercentage & "% with " & HealthType)
                Else
                    MsgBox("Invalid Type! Please contact the Developers. Error occured in CmdHealFriend")
                    frmSubForms.HFOnOff.Text = "Activate"
                End If
        End Select
    End Sub

#End Region

End Module
