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

Imports System.Text.RegularExpressions, TibiaTekBot.frmMain, _
 TibiaTekBot.Constants, _
 TibiaTekBot.CoreModule, System.Diagnostics, System.Runtime.InteropServices, _
 System.ComponentModel, TibiaTekBot.MiscUtils, TibiaTekBot.DatFile, Scripting, System.Net, _
 System.Xml


Public Module CommandParserModule

    Public Sub CommandParser(ByVal Message As String)
        Try
            If Not Core.Client.IsConnected Then Exit Sub
            Dim MatchObj As Match = Regex.Match(Message, "^([a-zA-Z]+)\s*([^;]*)$")
            If MatchObj.Success Then
                Select Case MatchObj.Groups(1).Value.ToLower
                    Case "help", "h", "?", "halp", "f1", "sos", "ayuda", "ajuda", "sos"
                        CmdHelp(MatchObj.Groups)
                    Case "light"
                        CmdLight(MatchObj.Groups)
                    Case "exp", "experience"
                        CmdExp(MatchObj.Groups)
                    Case "attack", "attacker", "atk"
                        CmdAttack(MatchObj.Groups)
                    Case "spell", "spellcaster"
                        CmdSpell(MatchObj.Groups)
                    Case "eat", "eater", "ate"
                        CmdEat(MatchObj.Groups)
                    Case "uh", "uher"
                        CmdUH(MatchObj.Groups)
                    Case "about", "a"
                        CmdAbout()
                    Case "look"
                        CmdLook(MatchObj.Groups)
                    Case "heal", "healer"
                        CmdHeal(MatchObj.Groups)
                    Case "fish", "fisher"
                        CmdFisher(MatchObj.Groups)
                    Case "healfriend"
                        CmdHealFriend(MatchObj.Groups)
                    Case "healparty"
                        CmdHealParty(MatchObj.Groups)
                    Case "faketitle"
                        CmdFakeTitle(MatchObj.Groups)
                    Case "loot", "looter"
                        CmdLoot(MatchObj.Groups)
                    Case "advertise", "advertiser"
                        CmdAdvertise(MatchObj.Groups)
                    Case "getitemid"
                        CmdGetItemId()
                    Case "version", "v"
                        CmdVersion()
                    Case "test"
                        CmdTest(MatchObj.Groups)
                    Case "char", "character"
                        CmdChar(MatchObj.Groups)
                    Case "open"
                        CmdOpen(MatchObj.Groups)
                    Case "guild", "guildmembers", "guilds"
                        CmdGuild(MatchObj.Groups)
                    Case "runemaker"
                        CmdRunemaker(MatchObj.Groups)
                    Case "ammorestacker", "ammo"
                        CmdAmmoRestacker(MatchObj.Groups)
                    Case "pickup"
                        CmdPickUp(MatchObj.Groups)
                    Case "log", "logger"
                        CmdLog(MatchObj.Groups)
                    Case "animation"
                        CmdAnimation(MatchObj.Groups)
                    Case "statsuploader"
                        CmdStatusUploader(MatchObj.Groups)
                    Case "mapviewer", "map", "mapview"
                        CmdMapViewer()
                    Case "watch", "watcher"
                        CmdTradeWatcher(MatchObj.Groups)
                    Case "stacker", "stack"
                        CmdStacker(MatchObj.Groups)
                    Case "config"
                        CmdConfig(MatchObj.Groups)
                    Case "hotkeys", "hotkey"
                        CmdHotkeys(MatchObj.Groups)
                    Case "feedback", "comment", "bugreport", "report", "bug"
                        CmdFeedback(MatchObj.Groups)
                    Case "chameleon", "outfit"
                        CmdChameleon(MatchObj.Groups)
                    Case "reload"
                        CmdReload(MatchObj.Groups)
                    Case "trainer"
                        CmdTrainer(MatchObj.Groups)
                    Case "commands", "command", "list", "listing", "cmd", "cmds"
                        CmdCommands()
                    Case "sendlocation", "sendloc"
                        CmdSendLocation(MatchObj.Groups)
                    Case "fpschanger", "fps"
                        CmdFpsChanger(MatchObj.Groups)
                    Case "rainbow", "rainbowoutfit"
                        CmdRainbow(MatchObj.Groups)
                    Case "namespy", "xray"
                        CmdNameSpy(MatchObj.Groups)
                    Case "website", "web", "homepage", "webpage"
                        CmdWebsite()
                    Case "drinker", "drink", "manadrink", "manadrinker", "mf", "manafluid"
                        CmdDrinker(MatchObj.Groups)
                    Case "cavebot"
                        CmdCaveBot(MatchObj.Groups)
                    Case "walker"
                        CmdWalker(MatchObj.Groups)
                    Case "combo", "combobot"
                        CmdCombobot(MatchObj.Groups)
                    Case "amuletchanger"
                        CmdAmuletChanger(MatchObj.Groups)
                    Case "ringchanger"
                        CmdRingChanger(MatchObj.Groups)
                    Case "irc"
                        CmdIrc(MatchObj.Groups)
                    Case "antilogout", "autoidler", "antiidler", "idler"
                        CmdAntiLogout(MatchObj.Groups)
                    Case "viewmsg"
                        CmdViewMessage()
                    Case "dancer"
                        CmdDancer(MatchObj.Groups)
                    Case "ammomaker", "ammocreater", "makeammo", "ammunitionmaker", "makeammunition"
                        CmdAmmoMaker(MatchObj.Groups)
                    Case Else
                        Core.ConsoleError("This command does not exist." & Ret & _
                            "  For a list of available commands type: &help.")
                End Select
            Else
                Core.ConsoleError("Invalid command syntax.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region " View Message "

    Private Sub CmdViewMessage()

        If Core.TTMessages = 0 Then
            Core.ConsoleError("You have no messages from the TibiaTek Development Team.")
        Else
            Try
                Dim Temp As Integer = 0
				Core.Client.UnprotectMemory(Consts.ptrForYourInformation, 20)
				Core.Client.WriteMemory(Consts.ptrForYourInformation, "Viewing Message")
				Dim WClient As New WebClient
				WClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Dim XMLResponse As String = WClient.UploadString(BotWebsite & "/viewmessages.php", "POST", "name=" & Web.HttpUtility.UrlEncode(Core.Client.CharacterName) & "&world=" & Web.HttpUtility.UrlEncode(Core.Client.CharacterWorld))
                Dim Document As New XmlDocument()
                Document.LoadXml(XMLResponse)
                Dim Messages As XmlElement = Document.Item("Messages")
                For Each Message As XmlElement In Messages
                    If Not String.IsNullOrEmpty(Message.InnerText) Then
                        Dim ClientPacket As New ClientPacketBuilder(Core.Proxy)
                        ClientPacket.FYIBox(Message.InnerText)
                        ClientPacket.Send()
                        'Core.Proxy.SendPacketToClient(FYIBox(Message.InnerText))
                    End If
                Next
                Core.ConsoleWrite("Successfully fetched all messages.")
            Catch Ex As Exception
                Core.ConsoleError("Unable to fetch the messages.")
            End Try
        End If
    End Sub

#End Region

#Region " Irc Command "
    Private Sub CmdIrc(ByVal Arguments As GroupCollection)
        Try
            If Not Core.IRCClient.IsConnected Then
                Core.ConsoleError("You are not connected to IRC.")
                Exit Sub
            End If
            Dim Match As Match = Regex.Match(Arguments(2).Value, "(join|nick|users)\s""?([^""]+)""?", RegexOptions.IgnoreCase)
            If Match.Success Then
                Dim CP As New ClientPacketBuilder(Core.Proxy)
                Select Case Match.Groups(1).Value.ToLower
                    Case "join"
                        If Core.IrcChannelIsOpened(Match.Groups(2).Value) Then
                            CP.OpenChannel(Match.Groups(2).Value, Core.IrcChannelNameToID(Match.Groups(2).Value))
                            'OpenIrcChannel(Match.Groups(2).Value, Core.IrcChannelNameToID(Match.Groups(2).Value))
                        Else
                            Core.IRCClient.Join(Match.Groups(2).Value)
                            Core.ConsoleWrite("You are now joining the channel " & Match.Groups(2).Value & ".")
                        End If
                    Case "nick"
                        If Core.IRCClient.Nick.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) Then
                            Core.ConsoleError("Your current nickname is the same.")
                        Else
                            Core.IRCClient.Nick = Match.Groups(2).Value
                            Core.IRCClient.WriteLine("NICK " & Core.IRCClient.Nick)
                            Core.ConsoleWrite("Trying to change your IRC nickname...")
                        End If
                    Case "users"
                        Dim Channel As String = Match.Groups(2).Value
                        If Core.IrcChannelIsOpened(Channel) Then
                            Dim TempNick As String = ""
                            For Each Nick As String In Core.IRCClient.Channels(Channel).Users.Keys
                                Select Case Core.IRCClient.GetUserLevel(Nick, Channel)
                                    Case 4
                                        TempNick = "~" & Nick
                                    Case 3
                                        TempNick = "@" & Nick
                                    Case 2
                                        TempNick = "%" & Nick
                                    Case 1
                                        TempNick = "+" & Nick
                                    Case Else
                                        TempNick = Nick
                                End Select
                                Core.ConsoleWrite(TempNick)
                            Next
                        Else
                            Core.ConsoleError("You are not in this channel.")
                        End If
                End Select
            Else
                Select Case Arguments(2).Value.ToLower
                    Case "quit"
                        Core.IRCClient.Quit()
                    Case Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End Select
            End If
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region

#Region " Website Command "

	Private Sub CmdWebsite()
		Try
			System.Diagnostics.Process.Start(ConstantsModule.BotWebsite)
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region

#Region " Name Spy Command "

	Private Sub CmdNameSpy(ByVal Arguments As GroupCollection)
		Try
			Select Case StrToShort(Arguments(2).Value)
				Case 0
					Core.Client.WriteMemory(Consts.ptrNameSpy, Consts.NameSpyDefault, 2)
					Core.Client.WriteMemory(Consts.ptrNameSpy2, Consts.NameSpy2Default, 2)
					Core.ConsoleWrite("Name Spy is now Disabled.")
				Case 1
					Core.Client.WriteMemory(Consts.ptrNameSpy, &H9090, 2)
					Core.Client.WriteMemory(Consts.ptrNameSpy2, &H9090, 2)
					Core.NameSpyActivated = True
					Core.ConsoleWrite("Name Spy is now Enabled.")
				Case Else
					Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
			End Select
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region

#Region " FPS Changer Command "

	Private Sub CmdFpsChanger(ByVal Arguments As GroupCollection)
		Try
			Core.FrameRateActive = Consts.FPSWhenActive
			Core.FrameRateInactive = Consts.FPSWhenInactive
			Core.FrameRateMinimized = Consts.FPSWhenMinimized
			Core.FrameRateHidden = Consts.FPSWhenHidden
			Select Case StrToShort(Arguments(2).Value)
				Case 0
					Core.FPSChangerTimerObj.StopTimer()
					Core.Client.SetFramesPerSecond(Core.FrameRateActive)
					Core.ConsoleWrite("FPS Changer is now Disabled.")
				Case 1
					Core.FPSChangerTimerObj.StartTimer()
					Core.ConsoleWrite("FPS Changer is now Enabled.")
				Case Else
					Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
			End Select
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region

#Region " Send Location Command "

	Private Sub CmdSendLocation(ByVal Arguments As GroupCollection)
		Try
			Dim Value As String = Arguments(2).Value.ToLower
			Dim MatchObj As Match = Regex.Match(Value, """([^""]+)""?")
			If MatchObj.Success Then
				If Not Core.BGWSendLocation.IsBusy Then
					Core.ConsoleWrite("Please wait...")
					Core.SendLocationDestinatary = MatchObj.Groups(1).Value
					Core.BGWSendLocation.RunWorkerAsync()
				Else
					Core.ConsoleError("Busy.")
				End If
			Else
				Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
			End If
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region

#Region " Reload Data Command "

	Private Sub CmdReload(ByVal Arguments As GroupCollection)
		Try
			Dim Value As String = Arguments(2).Value.ToLower
			Try
				Select Case Value
					Case "spells", "spell"
						Core.ConsoleWrite("Please wait...")
						CoreModule.Spells.LoadSpells()
					Case "outfits", "outfit"
						Core.ConsoleWrite("Please wait...")
						CoreModule.Outfits.LoadOutfits()
					Case "items", "item"
						Core.ConsoleWrite("Please wait...")
                        Core.Client.Items.Refresh()
                    Case "constants", "constant", "consts", "const"
                        Core.ConsoleWrite("Please wait...")
                        Consts.LoadConstants()
                    Case "tiles", "tile", "dat"
                        Core.ConsoleWrite("Please wait...")
                        Core.Client.Dat.Refresh()
                    Case Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        Exit Sub
                End Select
                Core.ConsoleWrite("Done reloading.")
            Catch
                Core.ConsoleError("Failed reloading.")
            End Try
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Chameleon Command "

    Private Sub CmdChameleon(ByVal Arguments As GroupCollection)
        Try
            Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "copy ""([^""]+)""?$")
            Dim BL As New BattleList
            Dim Found As Boolean = False
            If MatchObj.Success Then
                BL.Reset()
                Dim Name As String = MatchObj.Groups(1).Value
                Found = False
                Do
                    If BL.IsOnScreen Then
                        If String.Compare(Name, BL.GetName, True) = 0 Then
                            Found = True
                            Exit Do
                        End If
                    End If
                Loop While BL.NextEntity(True)
                If Found Then
                    Dim BL2 As New BattleList
                    BL2.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    BL2.OutfitID = BL.OutfitID
                    BL2.OutfitAddons = BL.OutfitAddons
                    BL2.HeadColor = BL.HeadColor
                    BL2.BodyColor = BL.BodyColor
                    BL2.LegsColor = BL.LegsColor
                    BL2.FeetColor = BL.FeetColor
                    Dim SP As New ServerPacketBuilder(Core.Proxy)
                    SP.ChangeOutfit(BL2.OutfitID, CByte(BL2.HeadColor), CByte(BL2.BodyColor), CByte(BL2.LegsColor), CByte(BL2.FeetColor), CByte(BL2.OutfitAddons))
                    'Core.Proxy.SendPacketToServer(ChangeOutfit(BL2.OutfitID, CByte(BL2.HeadColor), CByte(BL2.BodyColor), CByte(BL2.LegsColor), CByte(BL2.FeetColor), CByte(BL2.OutfitAddons)))
                    Core.ConsoleWrite("Your outfit has been changed to " & BL.GetName & ".")
                End If
            Else
                MatchObj = Regex.Match(Arguments(2).ToString, """([^""]+)(?:""\s+(\d))?")
                If MatchObj.Success Then
                    Dim Request As String = MatchObj.Groups(1).Value
                    Dim Outfit As New OutfitDefinition
                    If Regex.IsMatch(Request, "^\d+$") Then
                        Found = CoreModule.Outfits.GetOutfitByID(CUShort(Request), Outfit)
                    Else
                        Found = CoreModule.Outfits.GetOutfitByName(Request, Outfit)
                    End If
                    If Found Then
                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                        BL.OutfitID = Outfit.ID

                        If Not String.IsNullOrEmpty(MatchObj.Groups(2).Value) Then
                            If CInt(MatchObj.Groups(2).Value) > 3 Then Exit Sub
                            BL.OutfitAddons = CType(CInt(MatchObj.Groups(2).Value), IBattlelist.OutfitAddons)
                        End If
                        Core.ConsoleWrite("Your outfit has been changed to " & Outfit.Name & ".")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region " Auto Trainer Command "

    Private Sub CmdTrainer(ByVal Arguments As GroupCollection)
        Try
            Dim Match As Match = Regex.Match(Arguments(2).Value.ToLower, "([1-9]\d{0,2})%?\s+([1-9]\d{0,2})%?")
            If Match.Success Then
                If CInt(Match.Groups(1).Value) > 99 Then
                    Core.ConsoleError("Minimum Health Percent has to be less than 99%.")
                    Exit Sub
                ElseIf CInt(Match.Groups(2).Value) > 100 Then
                    Core.ConsoleError("Maximum Health Percent has to be less than 100%.")
                    Exit Sub
                ElseIf CInt(Match.Groups(1).Value) >= CInt(Match.Groups(2).Value) Then
                    Core.ConsoleError("Maximum Health Percent has to be higher than Minimum Health Percent.")
                    Exit Sub
                End If
                If Core.AutoTrainerEntities.Count = 0 Then
                    Core.ConsoleError("You have to add entities to the training list.")
                    Exit Sub
                End If
                Core.AutoTrainerMinHPPercent = CInt(Match.Groups(1).Value)
                Core.AutoTrainerMaxHPPercent = CInt(Match.Groups(2).Value)
                Core.AutoTrainerTimerObj.StartTimer()
                Core.ConsoleWrite("Auto Trainer will now attack the entities until " & Core.AutoTrainerMinHPPercent & "% of their health " & _
                 "and after their recover " & Core.AutoTrainerMaxHPPercent & "% of their health.")
            Else
                Select Case StrToShort(Arguments(2).Value)
                    Case 0
                        Core.AutoTrainerMinHPPercent = 0
                        Core.AutoTrainerMaxHPPercent = 0
                        Core.AutoTrainerTimerObj.StopTimer()
                        Core.ConsoleWrite("Auto Trainer is now Disabled.")
                    Case Else
                        Dim BL As New BattleList
                        Select Case Arguments(2).Value
                            Case "add", "agregar", "a"
                                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                                    If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                        Core.ConsoleError("You must be attacking or following something.")
                                        Exit Sub
                                    End If
                                End If
                                If Core.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                                    Core.ConsoleError("This entity is already in your list.")
                                Else
                                    Core.AutoTrainerEntities.Add(BL.GetEntityID)
                                    Core.ConsoleWrite("This entity has been added to your list.")
                                End If
                            Case "remove", "r", "remover", "quitar"
                                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                                    If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                        Core.ConsoleError("You must be attacking or following something.")
                                        Exit Sub
                                    End If
                                End If
                                If Core.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                                    Core.AutoTrainerEntities.Remove(BL.GetEntityID)
                                    Core.ConsoleWrite("This entity has been removed from your list.")
                                Else
                                    Core.ConsoleError("This entity is not on your list.")
                                End If
                            Case "clear"
                                Core.AutoTrainerEntities.Clear()
                                Core.ConsoleWrite("Auto Trainer entities list cleared.")
                            Case Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                End Select
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Help Command "

    Private Sub CmdHelp(ByVal Arguments As GroupCollection)
        Try
            Dim Topic As String = Arguments(2).ToString.ToLower
            Select Case Topic
                Case "light"
                    Core.ConsoleWrite("첣ight Effect�" & Ret & _
                    "Usage: &light <on | off | torch | great torch | ultimate tor" & _
                    "ch | utevo lux | utevo gran lux | utevo vis lux | light wand>." & Ret & _
                    "Example: &light utevo lux." & Ret & _
                    "Comment:" & Ret & _
                    "  When darkness covers the lands, this command proves itsel" & _
                    "f to be very handy." & Ret & _
                    "Note: This command <<does not>> cast any spells whatsoever.")
                Case "exp"
                    Core.ConsoleWrite("첚xperience Checker�" & Ret & _
                    "Usage: &exp <on | creatures <on | off> | off>." & Ret & _
                    "Example: &exp on." & Ret & _
                    "Example: &exp creatures on." & Ret & _
                    "Comment:" & Ret & _
                    " Keep yourself motivated by knowing how much little experie" & _
                    "nce you need until the next level!. With the new show creatures feature, you'll find out how many creatures you have left to kill." & Ret & _
                    "Note: The experience is shown on the title of the Tibia Client.")
                Case "trainer"
                    Core.ConsoleWrite("첔uto Trainer�" & Ret & _
                    "Usage: &trainer <add | remove | clear | stop |<minimum hp %> <maximum hp %>>>." & Ret & _
                    "Example: &trainer 50% 90%." & Ret & _
                    "Comment:" & Ret & _
                    " Train with as many monsters as you want. To add monsters, put them on follow and type &trainer add. " & _
                    "To start training type &trainer <min hp %> <max hp %>, and you will hurt the creatures until <min hp%> and continue attacking after <max hp%>. " & _
                    "To stop, &trainer stop.")
                Case "attack"
                    Core.ConsoleWrite("첔uto Attacker�" & Ret & _
                    "Usage: &attack <on | off | auto | stand | follow | offensive | balanced | " & _
                    "defensive | ""Player Name"" >." & Ret & _
                    "Example: &attack on." & Ret & _
                    "Comment:" & Ret & _
                    " Automatically attack any monsters that attack you, or if set to auto attacks monsters that are in screen (not touching to another player's creatures though)." & Ret & _
                    " To train with slimes, put the slime on follow when issuing &attack on.")
                Case "spell"
                    Core.ConsoleWrite("첯pell Caster�" & Ret & _
                    "Usage: &spell <off | <minimum mana points> <spell words>>." & Ret & _
                    "Example: &spell 400 exura vita """"Magic Level Plx!!." & Ret & _
                    "Comment:" & Ret & _
                    " Never be bothered again because you forgot to cast a spell and you " & _
                    "wasted mana!")
                Case "eat"
                    Core.ConsoleWrite("첔uto Eater/Smart Eater�" & Ret & _
                    "Usage: &eat <on | off | time in seconds | <smart <minimum hit points>> >." & Ret & _
                    "Example: &eat on." & Ret & _
                    "Example: &eat smart 600." & Ret & _
                    "Comment:" & Ret & _
                    " Ever felt hungry because you forgot to eat your meal? The" & _
                    " Auto Eater will make sure you bloat, but will also keep yo" & _
                    "u in a strict diet using the Smart option.")
                Case "uh"
                    Core.ConsoleWrite("첔uto UHer�" & Ret & _
                    "Usage: &uh <hit points | off>." & Ret & _
                    "Example: &uh 120." & Ret & _
                    "Comment:" & Ret & _
                    "  Feel safe because you will always UH yourself before it h" & _
                    "appens!")
                Case "look"
                    Core.ConsoleWrite("첛loor Explorer�" & Ret & _
                   "Usage: &look <around | up | above | down | below>." & Ret & _
                   "Example: &look below." & Ret & _
                   "Command:" & Ret & _
                   "  Find out what's below you before you go down the hole!" & Ret & _
                   "Note: This command won't tell you what's below you if you" & _
                   " are on the ground level, and it won't tell you what's ab" & _
                   "ove you if you are one level below ground.")
                Case "fisher"
                    Core.ConsoleWrite("첔uto Fisher�" & Ret & _
                    "Usage: &fisher <off  | <<minimum capacity> <normal | turbo>>>." & Ret & _
                    "Example: &fisher 6 normal." & Ret & _
                    "Comment:" & Ret & _
                    "  Have you ever tried fishing by yourself and noticed how b" & _
                    "oring and tiresome it gets? Well, now this is the solution " & _
                    "for you! Normal speed and turbo speed selector included ;).")
                Case "runemaker"
                    Core.ConsoleWrite("첮unemaker�" & Ret & _
                    "Usage: &runemaker <minimum mana points> <minimum soul points> ""<spell words or spell name>""." & Ret & _
                    "Example: &runemaker 400 2 ""ultimate healing""." & Ret & _
                    "Comment:" & Ret & _
                    "  TibiaTek Bot wouldn't be complete without a runemaker. T" & _
                    "his one will let you make runes even while hunting." & Ret & _
                    "Note: You must have the arrow slot empty, and there must at" & _
                    " least one container open with blank runes on it.")
                Case "char"
                    Core.ConsoleWrite("첖haracter Information Lookup�" & Ret & _
                    "Usage: &char ""<Player Name>""." & Ret & _
                    "Example: &char ""eternal oblivion""." & Ret & _
                    "Comment:" & Ret & _
                    " This command will let you retrieve the information of a character" & _
                    "without you having to open Tibia.com and search for it yourself.")
                Case "open"
                    Core.ConsoleWrite("첦pen File/Website�" & Ret & _
                    "Usage: &open <""Local File or URL"" | <wiki | character | guild | erig | google | mytibia> ""<search terms>"" >." & Ret & _
                    "Example: &open ""notepad""." & Ret & _
                    "Example: &open wiki ""Banuta Quest""." & Ret & _
                    "Comment:" & Ret & _
                    " This command lets you open any file on your computer. It also " & _
                    "lets you open your browser to search in Tibia Wiki, Tibia.com Character " & _
                    "and Guild Pages, Erig's TOP Players, Google and Mytibia.")
                    'Case "admin"
                    'Core.ConsoleWrite("첮emote Administration�" & Ret & _
                    '"Usage: &admin <password | list | off>" & Ret & _
                    '"Example: &admin iRownz0rx." & Ret & _
                    '"Comment:" & Ret & _
                    '" Allows for remote administration from another player." & Ret & _
                    '"Note: For another player to become administrator, he/she has to send a " & _
                    '"private message to the player running TibiaTek Bot the command name " & _
                    '"followed by the password, example: admin iRownz0rx.")
                Case "pickup"
                    Core.ConsoleWrite("첔uto Pickup�" & Ret & _
                    "Usage: &pickup <on | off>" & Ret & _
                    "Example: &pickup on." & Ret & _
                    "Comment:" & Ret & _
                    " This command will pickup throwable objects automatically for you and " & _
                    "put them in your right hand. If you accidentally put another object in " & _
                    "your right hand it will be moved to your backpack.")
                Case "ammorestacker"
                    Core.ConsoleWrite("첔uto Ammunition Restacker�" & Ret & _
                    "Usage: &ammorestacker <minimum ammunition | off>" & Ret & _
                    "Example: &ammorestacker 50." & Ret & _
                    "Comment:" & Ret & _
                    " Just put the item that you want to restack in your Belt/Arrow Slot " & _
                    "and activate this command, and you'll always be fully equipped." & Ret & _
                    "Note: You will be warned when you are running out of items to restack.")
                Case "log"
                    Core.ConsoleWrite("첚vents Logging�" & Ret & _
                    "Usage: &log <on | off>" & Ret & _
                    "Example: &log on." & Ret & _
                    "Comment:" & Ret & _
                    " Log all events, messages, etc. to Log.txt, useful if you want to know " & _
                    "what really happened." & Ret & _
                    "Note: To be used only when you are Away From Keyboard (AFK).")
                Case "healfriend"
                    Core.ConsoleWrite("첔uto Heal Friend�" & Ret & _
                    "Usage: &healfriend <minimum hit points percent>% ""<uh | sio | both>"" ""<player name>""." & Ret & _
                    "Example: &healfriend 50% ""sio"" ""Cameri deDurp""." & Ret & _
                    "Comment:" & Ret & _
                    "  Auto Heal Friend keeps your friend safe before it''s too late." & Ret & _
                    "Note: You can only heal one friend at a time, if you need to heal more people, use ""healparty"".")
                Case "guild"
                    Core.ConsoleWrite("첝uild Members Lookup�" & Ret & _
                    "Usage: &guild <online | both> ""<guild name>""." & Ret & _
                    "Example: &guild online ""Mercenaries""." & Ret & _
                    "Comment:" & Ret & _
                    "  Find out which guild members are online and which aren't." & Ret & _
                    "Note: Guild name is case-sensitive.")
                Case "faketitle"
                    Core.ConsoleWrite("첛ake Title�" & Ret & _
                    "Usage: &faketitle <off | ""<new title>"">." & Ret & _
                    "Example: &faketitle ""Firefox""." & Ret & _
                    "Comment:" & Ret & _
                    "  Never get caught again by your parents playing Tibia!.")
                Case "advertise"
                    Core.ConsoleWrite("첰rade Channel Advertiser�" & Ret & _
                    "Usage: &advertise ""<advertisement>""." & Ret & _
                    "Example: &advertise ""Sell Giant Sword, Wand of Plage ~ msg me""." & Ret & _
                    "Comment:" & Ret & _
                    "  This command advertises in the trade channel for you every 2 minutes.")
                Case "heal"
                    Core.ConsoleWrite("첔uto Healer�" & Ret & _
                    "Usage: &heal <minimum hit points percent | minimum hit points> ""<healing spell words or spell name>"" [""""<comment>]." & Ret & _
                    "Example: &heal 70% ""Intense Healing"" """"I never die~." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep yourself healthy at all times!.")
                Case "healparty"
                    Core.ConsoleWrite("첔uto Heal Party�" & Ret & _
                    "Usage: &healparty <minimum hit points percent>% ""<sio | uh | both>""." & Ret & _
                    "Example: &healparty 30% ""sio""." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep your party members safe, protect them because you could be next!.")
                Case "loot"
                    Core.ConsoleWrite("첔uto Looter�" & Ret & _
                    "Usage: &loot <on | minimum capacity | off>." & Ret & _
                    "Example: &loot 100." & Ret & _
                    "Comment:" & Ret & _
                    "  Tired of having to open the corpses? With this command you won't have to do anything.")
                Case "statsuploader"
                    Core.ConsoleWrite("첯tats Uploader�" & Ret & _
                    "Usage: &statsuploader <on | off>." & Ret & _
                    "Example: &statsuploader on." & Ret & _
                    "Comment:" & Ret & _
                    "  Generate an XML file with the stats of your character, upload it to the web or save it on your hard disk.")
                Case "mapviewer"
                    Core.ConsoleWrite("첤ap Viewer�" & Ret & _
                    "Usage: &mapviewer." & Ret & _
                    "Example: &mapviewer." & Ret & _
                    "Comment:" & Ret & _
                    "  Show an <<experimental>> map viewer of your current Tibia. For informational purposes only.")
                Case "stacker"
                    Core.ConsoleWrite("첔uto Stacker�" & Ret & _
                    "Usage: &stacker <on | off>." & Ret & _
                    "Example: &stacker on." & Ret & _
                    "Comment:" & Ret & _
                    "  Automatically organize your backpacks with this auto stacker.")
                Case "config"
                    Core.ConsoleWrite("첖onfiguration Manager�" & Ret & _
                    "Usage: &config <load | edit | clear>." & Ret & _
                    "Example: &config edit." & Ret & _
                    "Comment:" & Ret & _
                    "  Want TibiaTek Bot to automatically start all the features you like " & _
                    "right after you log in? Type in the configuration manager the commands " & _
                    "as you would type them normally, each on one line or separated by semi-colons.")
                Case "hotkeys"
                    Core.ConsoleWrite("첞otkey Settings Manager�" & Ret & _
                    "Usage: &hotkeys <save | load>." & Ret & _
                    "Example: &hotkeys save." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep separate hotkey settings for each of your characters!")
                Case "feedback"
                    Core.ConsoleWrite("첛eedback, Comments, Bug reports�" & Ret & _
                    "Usage: &feedback." & Ret & _
                    "Example: &feedback." & Ret & _
                    "Comment:" & Ret & _
                    "  With this command, you can send me comments, bug reports and anything considered as feedback." & Ret & _
                    "Communication between the users and developers is very important! This is completely anonymous.")
                Case "chameleon"
                    Core.ConsoleWrite("첖hameleon�" & Ret & _
                    "Usage: &chameleon <""<outfit name or id>"" [addons 0-3] | copy ""<player name>"">." & Ret & _
                    "Example: &chameleon ""male citizen"" 3." & Ret & _
                    "Example: &chameleon ""dworc voodoomaster""." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to change your outfit to whatever you want, even copy your friend's outfit!")
                Case "watch"
                    Core.ConsoleWrite("첰rade Channel Watcher�" & Ret & _
                    "Usage: &watch <regular expression pattern | off>." & Ret & _
                    "Example: &watch ^.*bps*\s+of\s+uh.*$." & Ret & _
                    "Comment:" & Ret & _
                    "  This command will inform you of any offer in the trade channel that matches the pattern." & Ret & _
                    "See http://en.wikipedia.org/wiki/Regular_expression for more information on regular expressions.")
                Case "reload"
                    Core.ConsoleWrite("첮eload Data�" & Ret & _
                    "Usage: &reload <spells | outfits | items | constants | dat>." & Ret & _
                    "Example: &reload items." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to reload the files from the Data folder.")
                Case "list"
                    Core.ConsoleWrite("첖ommands List�" & Ret & _
                    "Usage: &list." & Ret & _
                    "Example: &list." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to view all the bot's commands (In alphaphetical order).")
                Case "sendlocation"
                    Core.ConsoleWrite("첯end Location�" & Ret & _
                    "Usage: &sendlocation ""<player name>""." & Ret & _
                    "Example: &sendlocation ""Cameri de'Durp." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to send other players a link to a map of your current position." & Ret & _
                    "The link points to a page that has the map of tibia, and a cursor pointing to your current location.")
                Case "rainbow"
                    Core.ConsoleWrite("첮ainbow Outfit�" & Ret & _
                    "Usage: &rainbow <on | off | fast | slow>." & Ret & _
                    "Example: &rainbow fast." & Ret & _
                    "Comment:" & Ret & _
                    "  With Rainbow Outfit you can amaze your friends and another people aroud you." & Ret & _
                    "It changes your outfit color repately." & Ret & _
                    "Note: Everyone will see your outfit changing.")
                Case "fpschanger"
                    Core.ConsoleWrite("첛PS Changer�" & Ret & _
                    "Usage: &fpschanger <on | off>." & Ret & _
                    "Example: &fpschanger on." & Ret & _
                    "Comment: " & Ret & _
                    "  If your computer is running slow, and you want to play Tibia AND do something else at the same time, " & _
                    " this command is right for you, it will enable you to lower/increase the FPS when your Tibia is " & _
                    "active, inactive, minimized and hidden.")
                Case "namespy"
                    Core.ConsoleWrite("첥ame Spy�" & Ret & _
                    "Usage: &namespy <on | off>." & Ret & _
                    "Example: &namespy on." & Ret & _
                    "Comment: " & Ret & _
                    "  See the names/health bar of creatures or other players on a different floor than yours.")
                Case "website"
                    Core.ConsoleWrite("첳ebsite�" & Ret & _
                    "Usage: &website." & Ret & _
                    "Example: &website." & Ret & _
                    "Comment: " & Ret & _
                    "  Opens up TibiaTek Bot's website.")
                Case "drinker"
                    Core.ConsoleWrite("첔uto Manafluid Drinker�" & Ret & _
                    "Usage: &drinker <minimum mana points | off>." & Ret & _
                    "Example: &drinker 350." & Ret & _
                    "Comment: " & Ret & _
                    "  Drinks vials with mana fluid from backpack when mana is lower than given mana." & Ret & _
                    "Note: Uses same delays as auto healer.")
                Case "cavebot"
                    Core.ConsoleWrite("첖avebot�" & Ret & _
                    "Usage: &cavebot <on | off | continue | add <walk | rope | ladder | sewer> <hole | stairs | shovel <up | down | left | right>>" & Ret & _
                    "Example: &cavebot on." & Ret & _
                    "Example: &cavebot add stairs up." & Ret & _
                    "Comment: " & Ret & _
                    "  Cavebot hunts for you in caves. Just define the waypoints add you're ready to go." & Ret & _
                    "Note: Cavebot looting/eating uses same dealays as auto looter/eater." & Ret & _
                    "Note: Check Constants for more options.")
                Case "walker"
                    Core.ConsoleWrite("첳alker�" & Ret & _
                    "Usage: &walker <on | off | continue>" & Ret & _
                    "Example: &walker on." & Ret & _
                    "Comment: " & Ret & _
                    "  Walker simply walks from point A to point B." & Ret & _
                    "Note: When using continue mode, you should make waypoints to go circle" & Ret & _
                    "Note: Walker uses same waypoints as Cavebot, and you can add them with commands &walker add or &cavebot add. " & Ret & _
                    "  (type &help cavebot for more info about adding)")
                Case "combobot", "combo"
                    Core.ConsoleWrite("첖ombobot�" & Ret & _
                    "Usage: &combobot <""Leader Name"" | off>." & Ret & _
                    "Example: &combobot ""Jokuperkele""." & Ret & _
                    "  Makes comboshots even easier (and more effective) what they are normally. " & Ret & _
                    "Note: Combobot fires rune when <leader name> shoots rune and to the same target" & Ret & _
                    "Note: At this point combobot works only with Sudden Death runes.")
                Case "amuletchanger"
                    Core.ConsoleWrite("첔mulet Changer�" & Ret & _
                    "Usage: &amuletchanger <on | off | ""Amulet name"">." & Ret & _
                    "Example: &amuletchanger ""Stone Skin Amulet""." & Ret & _
                    "  Times when you died because you didn't have time to change amulet are now over." & Ret & _
                    "Note: &amuletchanger on is using the amulet you have in your amulet-slot." & Ret & _
                    "Note: Names of amulets are case-sensitive (e.g stone skin amuet <> Stone Skin Amulet)")
                Case "antilogout", "anti-logout"
                    Core.ConsoleWrite("첔nti-Logout�" & Ret & _
                    "Usage: &antilogout <on | off>." & Ret & _
                    "Example: &antilogout on." & Ret & _
                    "Comment: " & Ret & _
                    "  Protects yourself from getting kicked because of inactivity.")
                Case "irc"
                    Core.ConsoleWrite("첟RC�" & Ret & _
                    "Usage: &irc <<users | join> ""<channel>"" | nick ""new nick"" | quit>." & Ret & _
                    "Example: &irc join ""#TibiaTekBot""." & Ret & _
                    "Example: &irc quit." & Ret & _
                    "Comment: " & Ret & _
                    "  Allows you to execute some of the common IRC commands.")
                Case "viewmsg"
                    Core.ConsoleWrite("첲iew Message�" & Ret & _
                    "Usage: &viewmsg." & Ret & _
                    "Example: &viewmsg." & Ret & _
                    "Comment: " & Ret & _
                    "  Let's you read any messages sent to you by the TibiaTek Development Team.")
                Case "dancer"
                    Core.ConsoleWrite("첗ancer�" & Ret & _
                    "Usage: &dancer slow|fast|turbo|on|off." & Ret & _
                    "Example: &dancer on." & Ret & _
                    "Comment " & Ret & _
                    "  Now you can proof that you are not using Multi Client.. Even if you really are.")
                Case "ammomaker", "ammunitionmaker"
                    Core.ConsoleWrite("첔mmunition Maker�" & Ret & _
                    "Usage: <minimum mana points> <minimum capacity> ""<spell words or spell name>""." & Ret & _
                    "Example: &ammomaker 250 100 ""bolt""." & Ret & _
                    "Comment " & Ret & _
                    "  Don't want to pay from ammo but too lazy to make them? With this command you can let bot to handle the " & Ret & _
                    " ammo making, meanwhile you can lay back and take a nice warm cup of coffee.")
                Case Else
                    Select Case Topic.ToLower
                        Case "general", "general tools", "a"
                            Core.ConsoleWrite("General Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Configuration Manager -> &config." & Ret & _
                            "  Hotkeys Settings Manager -> &hotkeys." & Ret & _
                            "  Auto Looter -> &loot." & Ret & _
                            "  Auto Stacker -> &stacker." & Ret & _
                            "  Light Effect -> &light." & Ret & _
                            "  Ammunition Restacker -> &ammorestacker." & Ret & _
                            "  Commands Lister -> &list" & Ret & _
                            "  Walker -> &walker" & Ret & _
                            "  Amulet Changer -> &amuletchanger" & Ret & _
                            "  Ring Changer -> &ringchanger" & Ret & _
                            "  Combobot -> &combobot" & Ret & _
                            "  Dancer -> &dancer")
                        Case "healing", "healing tools", "b"
                            Core.ConsoleWrite("Healing Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Auto UHer -> &uh." & Ret & _
                            "  Auto Healer -> &heal." & Ret & _
                            "  Auto Heal Friend -> &healfriend." & Ret & _
                            "  Auto Heal Party -> &healparty." & Ret & _
                            "  Mana Fluid Drinker -> &drinker")
                        Case "afking", "afking tools", "afk tools", "afk", "c"
                            Core.ConsoleWrite("AFKing Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Spell Caster -> &spell." & Ret & _
                            "  Auto Eater -> &eat." & Ret & _
                            "  Runemaker -> &runemaker." & Ret & _
                            "  Auto Fisher -> &fisher." & Ret & _
                            "  Trade Channel Advertiser -> &advertise." & Ret & _
                            "  Trade Channel Watcher -> &watch." & Ret & _
                            "  Events Logging -> &logger." & Ret & _
                            "  Cavebot -> &cavebot." & Ret & _
                            "  FPS Changer -> &fpschanger." & Ret & _
                            "  Stats Uploader -> &statsuploader." & Ret & _
                            "  Ammo Maker -> &ammomaker." & Ret & _
                            "  Anti-Logout -> &antilogout.") ' & Ret & _
                            '"  Remote Administration -> &admin.") ' & Ret & _
                        Case "info tools", "info", "d"
                            Core.ConsoleWrite("Info Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Experience Checker -> &exp." & Ret & _
                            "  Character Information Lookup -> &char." & Ret & _
                            "  Guild Members Lookup -> &guild." & Ret & _
                            "  Floor Explorer -> &look." & Ret & _
                            "  Name Spy -> &namespy." & Ret & _
                            "  Open File/Websites -> &open." & Ret & _
                            "  Map Viewer -> &mapviewer." & Ret & _
                            "  Send Location -> &sendlocation." & Ret & _
                            "  Get Item IDs -> &getitemid.")
                        Case "training tools", "trainer tools", "trainer", "train", "train tools", "trainers", "e"
                            Core.ConsoleWrite("Training Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Auto Attacker -> &attack." & Ret & _
                            "  Auto Trainer -> &trainer." & Ret & _
                            "  Auto Pickup -> &pickup.")
                        Case "fun tools", "fun", "f"
                            Core.ConsoleWrite("Fun Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Fake Title -> &faketitle." & Ret & _
                            "  Chameleon -> &chameleon." & Ret & _
                            "  Rainbow Outfit -> &rainbow")
                        Case "miscellaneous tools", "misc", "miscellanoeus", "g"
                            Core.ConsoleWrite("Miscellaneous Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Feedback -> &feedback." & Ret & _
                            "  Reload Data -> &reload." & Ret & _
                            "  About Us -> &about." & Ret & _
                            "  View Message -> &viewmsg." & Ret & _
                            "  Website -> &website." & Ret & _
                            "  Version -> &version.")
                        Case Else
                            Core.ConsoleWrite("There are many command categories available, type help followed by the category to get a listing:" & Ret & _
                            "  A. General Tools." & Ret & _
                            "  B. Healing Tools." & Ret & _
                            "  C. AFKing Tools." & Ret & _
                            "  D. Info Tools." & Ret & _
                            "  E. Training Tools." & Ret & _
                            "  F. Fun Tools." & Ret & _
                            "  G. Miscellaneous Tools.")
                    End Select
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Feedback Command "

    Private Sub CmdFeedback(ByVal Arguments As GroupCollection)
        Try
            Core.Client.UnprotectMemory(CType(Consts.ptrEnterOneNamePerLine, System.IntPtr), CType(24, UIntPtr))
            Core.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Thank you for using TTB!")
            Dim CP As New ClientPacketBuilder(Core.Proxy)
            CP.HouseSpellEdit(&HFE, 0, "")
            'Core.Proxy.SendPacketToClient(HouseSpellEdit(&HFE, 0, ""))
            System.Threading.Thread.Sleep(500)
            Core.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Enter one name per line.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Hotkey Settings "

    Private Sub CmdHotkeys(ByVal Arguments As GroupCollection)
        Try
            Dim Argument As String = Arguments(2).Value.ToLower
            Select Case Argument
                Case "save"
                    Core.HotkeySettings.LoadFromMemory()
                    If Core.HotkeySettings.Save() Then
                        Core.ConsoleWrite("Hotkeys saved.")
                    Else
                        Core.ConsoleError("Unable to save hotkeys.")
                    End If
                Case "load", "reload"
                    Core.HotkeySettings.Load()
                    Core.ConsoleWrite("Hotkeys loaded.")
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Configuration Manager "

    Private Sub CmdConfig(ByVal Arguments As GroupCollection)
        Try
            Select Case Arguments(2).Value.ToLower
                Case "edit", "modify", "change"
                    Dim Data As String = ""
                    Try
                        Core.ConsoleWrite("Please wait...")
                        Dim Reader As IO.StreamReader
                        Reader = IO.File.OpenText(Core.GetProfileDirectory() & "\config.txt")
                        Data = Reader.ReadToEnd
                        Reader.Close()
                    Catch
                    Finally
                        Dim Temp As UInteger = 0
                        Core.Client.UnprotectMemory(Consts.ptrEnterOneNamePerLine, CType(24, UIntPtr))
                        Core.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Configuration Manager")
                        Dim CP As New ClientPacketBuilder(Core.Proxy)
                        CP.HouseSpellEdit(&HFF, 0, Data)
                        'Core.Proxy.SendPacketToClient(HouseSpellEdit(&HFF, 0, Data))
                        System.Threading.Thread.Sleep(500)
                        Core.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Enter one name per line.")
                    End Try
                Case "clear", "delete", "del", "cls"
                    Try
                        Core.ConsoleWrite("Please wait...")
                        IO.File.Delete(Core.GetProfileDirectory() & "\config.txt")
                    Catch
                        Core.ConsoleError("Unable to clear your configuration.")
                    Finally
                        Core.ConsoleWrite("Cleared.")
                    End Try
                Case "load", "execute"
                    Try
                        Core.ConsoleWrite("Please wait...")
                        Dim Data As String = ""
                        Dim Reader As IO.StreamReader
                        Reader = IO.File.OpenText(Core.GetProfileDirectory() & "\config.txt")
                        Data = Reader.ReadToEnd
                        Dim MCollection As MatchCollection
                        Dim GroupMatch As Match
                        MCollection = [Regex].Matches(Data, "&([^\n;]+)[;]?")
                        For Each GroupMatch In MCollection
                            CommandParser(GroupMatch.Groups(1).Value)
                        Next
                        Core.ConsoleWrite("Done loading your configuration.")
                    Catch
                        Core.ConsoleError("Unable to load your configuration.")
                    End Try
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Stacker Command "

    Private Sub CmdStacker(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.StackerTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Stacker is now Disabled.")
                Case 1
                    Core.StackerTimerObj.Interval = Consts.AutoStackerDelay
                    Core.StackerTimerObj.StartTimer()
                    Core.ConsoleWrite("Auto Stacker is now Enabled.")
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Map Viewer Command "

    Private Sub CmdMapViewer()
        Try
            If Not Core.BGWMapViewer.IsBusy Then
                Core.ConsoleWrite("Map Viewer is opening. Please wait...")
                Core.BGWMapViewer.RunWorkerAsync()
            Else
                Core.ConsoleError("Map Viewer is already opened.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Looter Command "

    Private Sub CmdLoot(ByVal Arguments As GroupCollection)
        Try
            If Core.CaveBotTimerObj.State = ThreadTimerState.Running Then
                Core.ConsoleError("Cavebot is currently running.")
                Exit Sub
            End If
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.LooterMinimumCapacity = 0
                    Core.LooterTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Looter is now Disabled.")
                Case 1
                    Core.LooterMinimumCapacity = 0
                    Core.LooterTimerObj.StartTimer()
                    Core.ConsoleWrite("Auto Looter is now Enabled." & Ret & "It will loot until capacity reaches 0.")
                Case Else
                    Select Case Arguments(2).Value.ToLower
                        Case "edit"
                            If Core.LooterTimerObj.State = ThreadTimerState.Running Then
                                Core.ConsoleError("Auto Looter must not be Enabled to edit the Loot Items.")
                                Exit Sub
                            End If
                            Core.ConsoleWrite("Please wait...")
                            CoreModule.LootItems.ShowLootCategories()
                        Case Else
                            Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]{0,4})")
                            If MatchObj.Success Then
                                Core.LooterMinimumCapacity = CUShort(MatchObj.Groups(1).Value)
                                Core.ConsoleWrite("Auto Looter is now Enabled." & Ret & "It will loot until capacity reaches " & Core.LooterMinimumCapacity & ".")
                                Core.LooterTimerObj.StartTimer()
                            Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            End If
                    End Select
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region " Stats Uploader "

    Private Sub CmdStatusUploader(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.StatsUploaderTimerObj.StopTimer()
                    Core.ConsoleWrite("Stats Uploader is now Disabled.")
                Case 1
                    If Consts.StatsUploaderSaveOnDiskOnly Then
                        If Consts.StatsUploaderPath.Length = 0 OrElse Consts.StatsUploaderFilename.Length = 0 Then
                            Core.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                            Exit Sub
                        End If
                        Core.UploaderUrl = Consts.StatsUploaderUrl
                        Core.UploaderFilename = Consts.StatsUploaderFilename
                        Core.UploaderPath = Consts.StatsUploaderPath
                        Core.UploaderUserId = Consts.StatsUploaderUserID
                        Core.UploaderPassword = Consts.StatsUploaderPassword
                        Core.UploaderSaveToDiskOnly = Consts.StatsUploaderSaveOnDiskOnly
                        Core.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                        Core.StatsUploaderTimerObj.StartTimer()
                        Core.ConsoleWrite("Stats Uploader is now Enabled.")
                    Else

                        If Consts.StatsUploaderUrl.Length = 0 _
                         OrElse Consts.StatsUploaderUserID.Length = 0 _
                         OrElse Consts.StatsUploaderPassword.Length = 0 _
                         OrElse Consts.StatsUploaderFrequency = 0 Then
                            Core.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                            Exit Sub
                        End If
                        Core.UploaderUrl = Consts.StatsUploaderUrl
                        Core.UploaderFilename = Consts.StatsUploaderFilename
                        Core.UploaderPath = Consts.StatsUploaderPath
                        Core.UploaderUserId = Consts.StatsUploaderUserID
                        Core.UploaderPassword = Consts.StatsUploaderPassword
                        Core.UploaderSaveToDiskOnly = Consts.StatsUploaderSaveOnDiskOnly
                        Core.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                        Core.StatsUploaderTimerObj.StartTimer()
                        Core.ConsoleWrite("Stats Uploader is now Enabled.")
                    End If
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Animation Command "

    Private Sub CmdAnimation(ByVal Arguments As GroupCollection)
        Try
            If Regex.IsMatch(Arguments(2).ToString, "^([1-9]\d?)$") Then
                Dim Num As Integer = CInt(Arguments(2).ToString)
                If Num < 0 OrElse Num > 31 Then Num = 31
                Dim CP As New ClientPacketBuilder(Core.Proxy)
                CP.AnimationEffect(Core.CharacterLoc, CType(Num, ITibia.AnimationEffects))
                'Core.Proxy.SendPacketToClient(MagicEffect(Core.CharacterLoc, CType(Num, MagicEffects)))
                Core.ConsoleWrite("Animation: " & Num & ".")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Test Command "

    Private Sub CmdTest(ByVal Arguments As GroupCollection)
        Try
            Core.ConsoleWrite("Begin Test")
            Dim SPB As New ServerPacketBuilder(Core.Proxy)
            SPB.CharacterTurn(IBattlelist.Directions.Up)
            SPB.CharacterTurn(IBattlelist.Directions.Down)
            SPB.CharacterTurn(IBattlelist.Directions.Left)
            SPB.CharacterTurn(IBattlelist.Directions.Right)
            SPB.Send()
            Core.ConsoleWrite("End Test")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Heal Party Command "

    Private Sub CmdHealParty(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.HealPartyMinimumHPPercentage = 0
                    Core.HealPartyTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Heal Party is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]?)%?\s+""?([^""]+)""?")
                    If MatchObj.Success Then
                        Core.HealPartyMinimumHPPercentage = CInt(MatchObj.Groups(1).Value)
                        Dim HealthType As String = ""
                        Select Case MatchObj.Groups(2).Value.ToLower
                            Case "ultimate healing", "uh", "adura vita"
                                Core.HealPartyHealType = HealTypes.UltimateHealingRune
                                HealthType = "Ultimate Healing."
                            Case "exura sio", "heal friend", "sio"
                                Core.HealPartyHealType = HealTypes.ExuraSio
                                HealthType = "Exura Sio."
                            Case "both"
                                Core.HealPartyHealType = HealTypes.Both
                                HealthType = "both Exura Sio and Ultimate Healing."
                            Case Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select
                        Core.HealPartyTimerObj.StartTimer()
                        Core.ConsoleWrite("Auto Heal Party is now Enabled." & Ret & _
                         "Healing party members when their hit points are less than " & Core.HealPartyMinimumHPPercentage & "% with " & HealthType)
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Healer Command "

    Private Sub CmdHeal(ByVal Arguments As GroupCollection)
        Try
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
                        Dim Match2 As Match = Regex.Match(Match.Groups(1).Value, "([1-9][0-9]?)%")
                        If Match2.Success Then
                            Dim MaxHitPoints As Integer = 0
                            Core.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                            Core.HealMinimumHP = MaxHitPoints * (CInt(Match2.Groups(1).Value) / 100)
                        Else
                            Core.HealMinimumHP = CInt(Match.Groups(1).Value)
                        End If
                        For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                            If Spell.Name.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) OrElse Spell.Words.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) Then
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
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Fake Title Command "

    Private Sub CmdFakeTitle(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).Value
            Select Case StrToShort(Value)
                Case 0
                    Core.FakingTitle = False
                    Dim BL As New BattleList
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    Core.Client.Title = BotName & " - " & BL.GetName
                    Core.ConsoleWrite("Client title restored.")
                Case Else
                    Dim Regexp As New Regex("""([^""]+)""?")
                    Dim Match As Match = Regexp.Match(Value)
                    If Match.Success Then
                        Core.LastExperience = 0
                        If Core.ExpCheckerActivated Then
                            Core.ExpCheckerActivated = False
                            Core.ConsoleWrite("Experience Checker is now Disabled. Fake Title is now Enabled.")
                        End If
                        Core.FakingTitle = True
                        Core.Client.Title = Match.Groups(1).Value
                        Core.ConsoleWrite("Client title changed to '" & Match.Groups(1).Value & "'.")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Logger Command "

    Private Sub CmdLog(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.LoggingEnabled = False
                    Core.ConsoleWrite("Logging is now Disabled.")
                Case 1
                    Core.LoggingEnabled = True
                    Core.ConsoleWrite("Logging is now Enabled.")
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Pickup "

    Private Sub CmdPickUp(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    With Core
                        .PickUpItemID = 0
                        .PickUpTimerObj.StopTimer()
                        .ConsoleWrite("Auto Pickup is now Disabled.")
                    End With
                Case 1
                    Dim RightHandItemID As Integer
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                    If RightHandItemID = 0 OrElse Not Core.Client.Items.IsThrowable(RightHandItemID) Then
                        Core.ConsoleError("You must have a throwable item in your right hand, like a spear, throwing knife, etc.")
                        Exit Sub
                    End If
                    With Core
                        .PickUpItemID = CUShort(RightHandItemID)
                        .PickUpTimerObj.Interval = Consts.AutoPickUpDelay
                        .PickUpTimerObj.StartTimer()
                        .ConsoleWrite("Auto Pickup is now Enabled.")
                    End With
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Runemaker Command "


    Private Sub CmdRunemaker(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.RunemakerManaPoints = 0
                    Core.RunemakerSoulPoints = 0
                    Core.RunemakerTimerObj.StopTimer()
                    Core.ConsoleWrite("Runemaker is now Disabled.")
                Case Else
                    Dim RegExp As New Regex("([1-9][0-9]{1,4})\s+([0-9]{0,3})\s+""([^""]+)""?")
                    Dim Match As Match = RegExp.Match(Value)
                    If Match.Success Then
                        Dim Found As Boolean = False
                        Dim S As New SpellDefinition
                        For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                            If (Spell.Name.Equals(Match.Groups(3).Value, StringComparison.CurrentCultureIgnoreCase) _
                            OrElse Spell.Words.Equals(Match.Groups(3).ToString, StringComparison.CurrentCultureIgnoreCase)) _
                            AndAlso Spell.Kind = SpellKind.Rune Then
                                S = Spell
                                Found = True
                                Exit For
                            End If
                        Next
                        If Found Then
                            Core.RunemakerSpell = S
                            Core.RunemakerManaPoints = CInt(Match.Groups(1).Value)
                            Core.RunemakerSoulPoints = CInt(Match.Groups(2).Value)
                            Core.RunemakerTimerObj.StartTimer()
                            Core.ConsoleWrite("Runemaker will now make " & S.Name & " when you have more than " & _
                             Core.RunemakerManaPoints & " mana points and more than " & Core.RunemakerSoulPoints & " soul points.")
                        Else
                            Core.ConsoleError("Invalid Conjure: Spell Name or Spell Words .")
                        End If
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region " Fisher Command "

    Private Sub CmdFisher(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.FisherMinimumCapacity = 0
                    Core.FisherSpeed = 0
                    Core.FisherTurbo = False
                    Core.FisherTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Fisher is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, "^(\d{1,3})(?:\s+(\S+))?$")
                    If MatchObj.Success Then
                        Select Case MatchObj.Groups(2).Value.ToLower
                            Case "normal", "default", ""
                                Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                                Core.FisherSpeed = 0
                                Core.FisherTurbo = False
                                Core.FisherTimerObj.StartTimer()
                                Core.ConsoleWrite("Auto Fisher is now Enabled.")
                            Case "turbo", "nitro", "fast", "faster", "fastest"
                                Core.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                                Core.FisherSpeed = 500
                                Core.FisherTurbo = True
                                Core.FisherTimerObj.StartTimer()
                                Core.ConsoleWrite("Auto Fisher (Turbo Mode) is now Enabled.")
                            Case Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Trade Channel Watcher Command "

    Private Sub CmdTradeWatcher(ByVal Arguments As GroupCollection)
        Try
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
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Experience Checker Command "

    Private Sub CmdExp(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).Value
            Select Case StrToShort(Value)
                Case 0
                    Core.ExpCheckerActivated = False
                    Core.LastExperience = 0
                    Core.Client.Title = BotName & " - " & Core.Client.CharacterName
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
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Guild Members Command "

    Private Sub CmdGuild(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Dim MatchObj As Match = Regex.Match(Value, "(online|both)\s+""([^""]+)")
            If MatchObj.Success Then
                Core.GuildMembersCommand = MatchObj.Groups(2).ToString
                Core.GuildMembersOnlineOnly = False
                If String.Compare(MatchObj.Groups(1).Value, "online", True) = 0 Then
                    Core.GuildMembersOnlineOnly = True
                End If
                If Not Core.BGWGuildMembersCommand.IsBusy Then
                    Core.ConsoleWrite("Please Wait...")
                    Core.BGWGuildMembersCommand.RunWorkerAsync()
                Else
                    Core.ConsoleError("Busy.")
                End If
            Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Char Command "

    Private Sub CmdChar(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Dim MatchObj As Match = Regex.Match(Value, """([^""]+)")
            If MatchObj.Success Then
                Core.CharCommand = MatchObj.Groups(1).ToString
                If Not Core.BGWCharCommand.IsBusy Then
                    Core.ConsoleWrite("Please Wait...")
                    Core.BGWCharCommand.RunWorkerAsync()
                Else
                    Core.ConsoleError("Busy.")
                End If
            Else
                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Open Command "

    Private Sub CmdOpen(ByVal Arguments As GroupCollection)
        Dim Value As String = Arguments(2).ToString
        Try
            Dim MatchObj As Match = Regex.Match(Value, "^""([^""]+)""?")
            If MatchObj.Success Then
                Core.OpenCommand = MatchObj.Groups(1).ToString
                If Not Core.BGWOpenCommand.IsBusy Then
                    Core.ConsoleWrite("Please Wait...")
                    Core.BGWOpenCommand.RunWorkerAsync()
                Else
                    Core.ConsoleError("Busy.")
                End If
            Else
                MatchObj = Regex.Match(Value, "([a-zA-Z]+)\s+""([^""]+)")
                If MatchObj.Success Then
                    Dim Type As String = MatchObj.Groups(1).ToString
                    Dim Prepend As String = ""
                    Select Case Type.ToLower
                        Case "google"
                            Prepend = "http://www.google.com/search?q="
                        Case "erig"
                            Prepend = "http://www.erig.net/xphist.php?player="
                        Case "wiki"
                            Prepend = "http://tibia.erig.net/Special:Search?search="
                        Case "character"
                            Prepend = "http://www.tibia.com/community/?subtopic=character&name="
                        Case "guild"
                            Prepend = "http://www.tibia.com/community/?subtopic=guilds&page=view&GuildName="
                        Case "mytibia"
                            Prepend = "http://www.mytibia.com/"
                        Case Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            Exit Sub
                    End Select
                    Core.OpenCommand = Prepend & MatchObj.Groups(2).ToString
                    If Not Core.BGWOpenCommand.IsBusy Then
                        Core.ConsoleWrite("Please Wait...")
                        Core.BGWOpenCommand.RunWorkerAsync()
                    Else
                        Core.ConsoleError("Busy.")
                    End If

                Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
            End If
        Catch e As Win32Exception
            Core.ConsoleWrite("Error opening """ & Value & """ with message """ & e.Message & """.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Ammunition Restacker Command "

    Private Sub CmdAmmoRestacker(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.AmmoRestackerItemID = 0
                    Core.AmmoRestackerTimerObj.StopTimer()
                    Core.ConsoleWrite("Ammunition Restacker is now Disabled.")
                Case Else
                    Dim ItemID As Integer
                    Dim ItemCount As Integer
                    Dim RegExp As New Regex("([1-9]\d)")
                    Dim Match As Match = RegExp.Match(Value)
                    If Not Match.Success Then
                        Core.ConsoleError("You must specify the minimum ammunition count between 1 and 99, inclusive.")
                        Exit Sub
                    End If
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), ItemID, 2)
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, ItemCount, 1)
                    If ItemID = 0 OrElse Not Core.Client.Dat.GetInfo(ItemID).IsStackable Then
                        Core.ConsoleError("You must place some of your ammunition on the Belt/Arrow Slot first.")
                        Exit Sub
                    End If
                    Core.AmmoRestackerItemID = ItemID
                    Core.AmmoRestackerOutOfAmmo = False
                    Core.AmmoRestackerMinimumItemCount = CInt(Match.Groups(1).Value)
                    Core.AmmoRestackerTimerObj.StartTimer()
                    Core.ConsoleWrite("Ammunition Restacker is now Enabled.")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Light Effect Command "

    Private Sub CmdLight(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.SetLight(ITibia.LightIntensity.Small, ITibia.LightColor.UtevoLux)
                    Core.LightTimerObj.StopTimer()
                    Core.ConsoleWrite("Light Effect is now Disabled.")
                Case 1
                    Core.LightC = ITibia.LightColor.BrightSword
                    Core.LightI = ITibia.LightIntensity.Huge + 2
                    Core.ConsoleWrite("Full Light Effect is now Enabled.")
                    Core.LightTimerObj.StartTimer()
                Case Else
                    Dim strOutput As String = "{0} Light Effect is now Enabled."
                    Select Case Value.ToLower()
                        Case "torch"
                            Core.LightI = ITibia.LightIntensity.Medium
                            Core.LightC = ITibia.LightColor.Torch
                            Core.ConsoleWrite("Torch Light Effect is now Enabled.")
                        Case "great torch"
                            Core.LightI = ITibia.LightIntensity.VeryLarge
                            Core.LightC = ITibia.LightColor.Torch
                            Core.ConsoleWrite("Great Torch Light Effect is now Enabled.")
                        Case "ultimate torch"
                            Core.LightI = ITibia.LightIntensity.Huge
                            Core.LightC = ITibia.LightColor.Torch
                            Core.ConsoleWrite("Ultimate Torch Light Effect is now Enabled.")
                        Case "utevo lux"
                            Core.LightI = ITibia.LightIntensity.Medium
                            Core.LightC = ITibia.LightColor.UtevoLux
                            Core.ConsoleWrite("Utevo Lux Light Effect is now Enabled.")
                        Case "utevo gran lux"
                            Core.LightI = ITibia.LightIntensity.Large
                            Core.LightC = ITibia.LightColor.UtevoLux
                            Core.ConsoleWrite("Utevo Gran Lux Light Effect is now Enabled.")
                        Case "utevo vis lux"
                            Core.LightI = ITibia.LightIntensity.VeryLarge
                            Core.LightC = ITibia.LightColor.UtevoLux
                            Core.ConsoleWrite("Utevo Vis Lux Light Effect is now Enabled.")
                        Case "light wand"
                            Core.LightI = ITibia.LightIntensity.Large
                            Core.LightC = ITibia.LightColor.LightWand
                            Core.ConsoleWrite("Light Wand Light Effect is now Enabled.")
                        Case Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            Exit Sub
                    End Select
                    Core.LightTimerObj.StartTimer()
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Attacker Command "

    Private Sub CmdAttack(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Dim EntityName As String = ""
            Dim BL As BattleList = New BattleList
            Dim SP As New ServerPacketBuilder(Core.Proxy)
            Select Case StrToShort(Value)
                Case 0
                    Core.AutoAttackerActivated = False
                    Core.AutoAttackerIgnoredID = 0
                    Core.AutoAttackerTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Attacker is now Disabled.")
                Case 1
                    If BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) OrElse BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                        Core.AutoAttackerIgnoredID = BL.GetEntityID
                        EntityName = BL.GetName
                        If Core.AutoAttackerIgnoredID < &H40000000 Then Core.AutoAttackerIgnoredID = 0
                    Else
                        Core.AutoAttackerIgnoredID = 0
                    End If
                    Core.AutoAttackerActivated = True
                    Core.ConsoleWrite("Auto Attacker is now Enabled.")
                    If Core.AutoAttackerIgnoredID > 0 Then
                        Core.ConsoleWrite("  Ignoring: " & EntityName & " (" & Core.AutoAttackerIgnoredID & ").")
                    End If
                Case Else
                    Dim RegExp As New Regex("""([^""]+)""?")
                    Dim Found As Boolean = False
                    Dim Match As Match = RegExp.Match(Value)
                    BL.Reset(True)
                    If Match.Success Then
                        Do
                            If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                                If String.Compare(BL.GetName, Match.Groups(1).Value, True) = 0 Then
                                    Found = True
                                    Exit Do
                                End If
                            End If
                        Loop While BL.NextEntity(True)
                        If Found Then
                            Core.Client.WriteMemory(Consts.ptrSecureMode, 0, 1)
                            SP.ChangeSecureMode(ITibia.SecureMode.Normal)
                            'Core.Proxy.SendPacketToServer(ChangeSecureMode(ITibia.SecureMode.Normal))
                            System.Threading.Thread.Sleep(1000)
                            BL.Attack()
                            Core.ConsoleWrite(String.Format("Attacking entity '{0}'.", BL.GetName))
                        Else
                            Core.ConsoleWrite(String.Format("Entity '{0}' not found.", Match.Groups(1).Value))
                        End If
                    Else
                        Select Case Value.ToLower
                            Case "stop", "s"
                                Core.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
                                Core.Client.WriteMemory(Consts.ptrFollowedEntityID, 0, 4)
                                SP.StopEverything()
                                'Core.Proxy.SendPacketToServer(StopEverything())
                                Core.ConsoleWrite("Stopped everything.")
                            Case "follow", "chase", "chasing", "c", "f"
                                Core.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                                SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                                Core.ConsoleWrite("Opponents will be chased.")
                            Case "stand", "s", "stay"
                                Core.Client.WriteMemory(Consts.ptrChasingMode, 0, 1)
                                SP.ChangeChasingMode(ITibia.ChasingMode.Standing)
                                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Standing))
                                Core.ConsoleWrite("Opponents will not be chased.")
                            Case "offensive", "offense", "o"
                                Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Offensive, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Offensive)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(ITibia.FightingMode.Offensive))
                                Core.ConsoleWrite("Fighting in offensive mode.")
                            Case "balanced", "b", "middle"
                                Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Balanced, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Balanced)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(ITibia.FightingMode.Balanced))
                                Core.ConsoleWrite("Fighting in balanced mode.")
                            Case "defensive", "defense", "d"
                                Core.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Defensive, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Defensive)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Defensive))
                                Core.ConsoleWrite("Fighting in defensive mode.")
                            Case "auto", "automatic"
                                Core.AutoAttackerTimerObj.StartTimer()
                                Core.ConsoleWrite("Attacking creatures automatically.")
                            Case Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Spell Caster Command "

    Private Sub CmdSpell(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.SpellManaRequired = 0
                    Core.SpellMsg = ""
                    Core.SpellTimerObj.StopTimer()
                    Core.ConsoleWrite("Spell Caster is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, "^([1-9][0-9]{1,4})\s+""?(.+)$")
                    If MatchObj.Success Then
                        Core.SpellManaRequired = CUInt(MatchObj.Groups(1).ToString)
                        Core.SpellMsg = MatchObj.Groups(2).ToString
                        Core.SpellTimerObj.StartTimer()
                        Core.ConsoleWrite("Spell Caster is now Enabled." & Ret & _
                         "Casting '" & Core.SpellMsg & "' with " & Core.SpellManaRequired & " or more mana points.")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " About Command "

    Private Sub CmdAbout()
        Try
            Core.ConsoleWrite(BotName & " v" & BotVersion & "." & Ret & _
            "It is written by the TibiaTek Development Team, " & Ret & _
            "and held at http://tibiatekbot.googlecode.com/" & Ret & _
            "For versioning information, type: &version." & Ret & _
            "To go to our website, type: &website.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Eater Command "

    Private Sub CmdEat(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.AutoEaterSmart = 0
                    Core.EaterTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Eater is now Disabled.")
                Case 1
                    Core.AutoEaterSmart = 0
                    Core.EaterTimerObj.Interval = Consts.AutoEaterInterval
                    Core.EaterTimerObj.StartTimer()
                    Core.ConsoleWrite("Auto Eater is now Enabled for every 30 seconds.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "smart\s+([1-9]\d{1,4})")
                    If MatchObj.Success Then
                        Core.AutoEaterSmart = CInt(MatchObj.Groups(1).ToString)
                        Core.EaterTimerObj.Interval = Consts.AutoEaterSmartInterval
                        Core.EaterTimerObj.StartTimer()
                        Core.ConsoleWrite("Auto Eater will eat only when you are below " & Core.AutoEaterSmart & " hit points, once every minute.")
                    Else
                        MatchObj = Regex.Match(Arguments(2).ToString, "([1-9]\d{0,2})")
                        If MatchObj.Success Then
                            Core.AutoEaterSmart = 0
                            Core.EaterTimerObj.Interval = CInt(MatchObj.Groups(1).Value) * 1000
                            Core.EaterTimerObj.StartTimer()
                            Core.ConsoleWrite("Auto Eater is now Enabled for every " & ((Core.EaterTimerObj.Interval / 1000) Mod 1000) & " second(s).")
                        Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End If
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto UHer Command "

    Private Sub CmdUH(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.UHTimerObj.StopTimer()
                    Core.UHHPRequired = 0
                    Core.ConsoleWrite("Auto UHer is now Disabled.")
                Case Else
                    If IsNumeric(Value) AndAlso CInt(Value) > 0 Then
                        Core.UHHPRequired = CUInt(Value)
                        Core.UHId = Core.Client.Items.GetItemID("Ultimate Healing")
                        Core.UHTimerObj.StartTimer()
                        Core.ConsoleWrite("Auto UHer will now 'UH' you if you are below " & Ret & _
                        Core.UHHPRequired & " hit points.")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Heal Friend Command "

    Private Sub CmdHealFriend(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Core.HealFriendCharacterName = ""
                    Core.HealFriendHealthPercentage = 0
                    Core.HealFriendTimerObj.StopTimer()
                    Core.ConsoleWrite("Auto Heal Friend is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]?)%?\s+""([^""]+)""\s+""([^""]+)""?")
                    If MatchObj.Success Then
                        Core.HealFriendHealthPercentage = CUShort(MatchObj.Groups(1).ToString)
                        If Core.HealFriendHealthPercentage < 0 Or Core.HealFriendHealthPercentage > 99 Then
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select
                        Core.HealFriendCharacterName = MatchObj.Groups(3).Value
                        Core.HealFriendTimerObj.StartTimer()
                        Core.ConsoleWrite("Auto Heal Friend is now Enabled." & Ret & _
                         "Healing '" & Core.HealFriendCharacterName & "' when his/her hit points are less than " & Core.HealFriendHealthPercentage & "% with " & HealthType)
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Version Command "

    Private Sub CmdVersion()
        Try
            Core.ConsoleWrite(BotName & " v" & BotVersion & ".")
            Core.ConsoleWrite("Powered By: PProxy v2.0 by CPargermer.")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Look Command "

    Private Sub CmdLook(ByVal Arguments As GroupCollection)
        Try
            Dim Floor As Short = 0
            Dim EntityCount As Integer = 0
            Dim EntityList As New List(Of String)
            Dim EntityListCount As New List(Of Integer)
            Dim EntityListIndex As Integer
            Dim EntityName As String = ""
            Dim Output As String = ""
            Dim I As Integer
            Dim BL As BattleList = New BattleList
            Dim Found As Boolean = False
            Select Case Arguments(2).ToString.ToLower
                Case "down", "below", "downstairs", "v", "\/"
                    Floor = 1
                Case "up", "above", "upstairs", "/\", "^"
                    Floor = -1
                Case "around"
                    Floor = 0
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    Exit Sub
            End Select
            BL.Reset(True)
            Do
                If BL.IsMyself OrElse BL.GetFloor <> Core.CharacterLoc.Z + Floor Then Continue Do
                EntityName = BL.GetName
                EntityListIndex = EntityList.IndexOf(EntityName)
                If EntityListIndex > -1 Then
                    EntityListCount(EntityListIndex) += 1
                Else
                    EntityList.Add(EntityName)
                    EntityListCount.Add(1)
                    EntityCount += 1
                End If
            Loop While BL.NextEntity(True)
            If EntityCount = 0 Then
                Output = "Nothing"
            Else
                For I = 0 To EntityCount - 1
                    If (I > 0) And (I <= EntityCount - 1) Then Output = Output & ", "
                    If EntityListCount(I) = 1 Then
                        Output &= EntityList(I)
                    Else
                        Output &= EntityList(I) & "(" & EntityListCount(I) & "x)"
                    End If
                Next
            End If
            Core.ConsoleWrite("Entities found: " & Output & ".")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Advertise Command "

    Private Sub CmdAdvertise(ByVal Arguments As GroupCollection)
        Try
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
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Get Item ID Command "

    Private Sub CmdGetItemId()
        Try
            Dim Container As New Container()
            Dim I As Integer
            Dim Item As Scripting.IContainer.ContainerItemDefinition
            Dim ItemCount As Integer
            Dim Output As String = ""
            Dim ItemName As String
            Dim ID As Integer
            Core.ConsoleWrite("Getting Item IDs, Please Wait...")
            Core.ConsoleWrite("Inventory: ")
            For E As ITibia.InventorySlots = ITibia.InventorySlots.Head To ITibia.InventorySlots.Belt
                Output = E.ToString & ": "
                Core.Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (E - 1)), ID, 2)
                ItemName = Core.Client.Items.GetItemName(ID)
                If String.Compare(ItemName, "Unknown") = 0 Then
                    Output &= "Unknown H" & Hex(ID)
                Else
                    Output &= ItemName
                End If
                If Core.Client.Dat.GetInfo(ID).IsStackable Then
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (E - 1)) + Consts.ItemCountOffset, ItemCount, 1)
                    Output &= " (x" & ItemCount & ")"
                End If
                Output &= "."
                Core.ConsoleWrite(Output)
            Next

            Container.Reset()
            Do
                If Container.IsOpened Then
                    Output = ""
                    Core.ConsoleWrite("Container #" & Hex(Container.GetContainerIndex + 1) & ": " & Container.GetName() & "")
                    ItemCount = Container.GetItemCount()
                    For I = 0 To ItemCount - 1
                        Item = Container.Items(I)
                        ItemName = Core.Client.Items.GetItemName(Item.ID)
                        If String.Compare(ItemName, "Unknown") = 0 Then
                            Output &= "Unknown H" & Hex(Item.ID)
                        Else
                            Output &= ItemName & " H" & Hex(Item.ID)
                        End If
                        If Core.Client.Dat.GetInfo(Item.ID).IsStackable Then
                            Output &= " (x" & Item.Count & ")"
                        End If
                        If I < ItemCount - 1 Then
                            Output &= ", "
                        Else
                            Output &= "."
                        End If
                    Next
                    Core.ConsoleWrite(Output)
                End If
            Loop While Container.NextContainer()
            Core.ConsoleWrite("Done.")
            Exit Sub
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " List Commands Command "

    Private Sub CmdCommands()
        Try
            Core.ConsoleWrite("Listing all commands. Type &help <command> for help. Example: &help attack." & Ret & _
                "&amuletchanger <on | off | ""Amulet Name"">" & Ret & _
                "&advertise ""<advertisement>""" & Ret & _
                "&ammorestacker <minimum ammunition | off>" & Ret & _
                "&antilogout <interval>" & Ret & _
                "&attack <on | off | auto | stand | follow | offensive | balanced | " & _
                "defensive | ""Player Name"" >" & Ret & _
                "&cavebot <on | off | add <walk | rope | ladder | sewer> <hole | stairs | shovel <up | down | left | right>>" & Ret & _
                "&chameleon <""<outfit name or id>"" <addons 0-3> | copy ""<player name>"">" & Ret & _
                "&char ""<Player Name>""" & Ret & _
                "&config <load | edit | clear>" & Ret & _
                "&combobot ""<Leader Name>"" | Off")
            Core.ConsoleWrite(" " & Ret & _
                "&drinker <minimum mana points | off>" & Ret & _
                "&eat <on | off | time in seconds | <smart <minimum hit points>> >" & Ret & _
                "&exp <on | creatures <on | off> | off>" & Ret & _
                "&faketitle <off | ""<new title>"">" & Ret & _
                "&feedback" & Ret & _
                "&fisher <off  | <<minimum capacity> <normal | turbo>>>" & Ret & _
                "&fpschanger <on | off>" & Ret & _
                "&guild <online | both> ""<guild name>""" & Ret & _
                "&help <command>" & Ret & _
                "&heal <minimum hit points percent | minimum hit points> ""<healing spell words or spell name>"" [""""<comment>]" & Ret & _
                "&healfriend <hit points percent> ""<uh | sio | both>"" ""<player name>""")
            Core.ConsoleWrite(" " & Ret & _
                "&healparty <minimum hit points percent>% ""<sio | uh | both>""" & Ret & _
                "&hotkeys <save | load>" & Ret & _
                "&irc" & Ret & _
                "&light <on | off | torch | great torch | ultimate tor" & _
                "ch | utevo lux | utevo gran lux | utevo vis lux | light wand>" & Ret & _
                "&log <on | off>" & Ret & _
                "&loot <on | minimum capacity | off>" & Ret & _
                "&look <around | up | above | down | below>" & Ret & _
                "&mapviewer" & Ret & _
                "&namespy <on | off>" & Ret & _
                "&open <""Local File or URL"" | <wiki | character | guild | erig | google | mytibia> ""<search terms>"" >" & Ret & _
                "&pickup <on | off>")
            Core.ConsoleWrite(" " & Ret & _
                "&rainbow <on | off | fast | slow>" & Ret & _
                "&reload <spells | outfits | items | constants | dat>" & Ret & _
                "&ringchanger <on | off | ""Ring Name"">" & Ret & _
                "&runemaker <minimum mana points> <minimum soul points> ""<spell words or spell name>""" & Ret & _
                "&spell <off | <minimum mana points> <spell words>>" & Ret & _
                "&stacker <on | off>" & Ret & _
                "&statsuploader <on | off>" & Ret & _
                "&trainer <add | remove | clear | stop |<minimum hp %> <maximum hp %>>>" & Ret & _
                "&uh <hit points | off>" & Ret & _
                "&viewmsg" & Ret & _
                "&walker <on | off | continue>" & Ret & _
                "&watch <regular expression pattern | off>" & Ret & _
                "&website")
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Rainbow Outfit Command "
    Private Sub CmdRainbow(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.ConsoleWrite("Rainbow Outfit is now Disabled")
                    Core.RainbowOutfitTimerObj.StopTimer()
                    Core.RainbowOutfitBody = 0
                    Core.RainbowOutfitFeet = 0
                    Core.RainbowOutfitHead = 0
                    Core.RainbowOutfitLegs = 0
                Case 1
                    Core.ConsoleWrite("Rainbow Outfit is now Enabled")
                    Core.RainbowOutfitBody = 0
                    Core.RainbowOutfitFeet = 10
                    Core.RainbowOutfitHead = 20
                    Core.RainbowOutfitLegs = 30
                    Core.RainbowOutfitTimerObj.Interval = 50
                    Core.RainbowOutfitTimerObj.StartTimer()
                Case Else
                    Select Case Value.ToLower
                        Case "fast"
                            Core.RainbowOutfitBody = 0
                            Core.RainbowOutfitFeet = 10
                            Core.RainbowOutfitHead = 20
                            Core.RainbowOutfitLegs = 30
                            Core.RainbowOutfitTimerObj.Interval = 50
                            Core.ConsoleWrite("Rainbow Outfit is now Enabled with fast speed.")
                            Core.RainbowOutfitTimerObj.StartTimer()
                        Case "slow"
                            Core.RainbowOutfitBody = 0
                            Core.RainbowOutfitFeet = 10
                            Core.RainbowOutfitHead = 20
                            Core.RainbowOutfitLegs = 30
                            Core.RainbowOutfitTimerObj.Interval = 100
                            Core.ConsoleWrite("Rainbow Outfit is now Enabled with low speed.")
                            Core.RainbowOutfitTimerObj.StartTimer()
                        Case Else
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End Select
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Auto Drinker Command "
    Private Sub CmdDrinker(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString.ToLower
            Select Case StrToShort(Value)
                Case 0
                    Core.AutoDrinkerTimerObj.StopTimer()
                    Core.DrinkerManaRequired = 0
                    Core.ConsoleWrite("Auto Drinker is now Disabled.")
                Case Else
                    Dim RegExp As New Regex("^[1-9]\d{1,3}$")
                    Dim Match As Match = RegExp.Match(Value)
                    If Match.Success Then
                        Core.DrinkerManaRequired = Value
                        Core.AutoDrinkerTimerObj.Interval = Consts.HealersCheckInterval
                        Core.AutoDrinkerTimerObj.StartTimer()
                        Core.ConsoleWrite("Auto Drinker is now Enabled.")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " CaveBot Command "
    Private Sub CmdCaveBot(ByVal Arguments As GroupCollection)
        Try
            Dim value As String = Arguments(2).ToString.ToLower
            Dim SP As New ServerPacketBuilder(Core.Proxy)
            Select Case StrToShort(value)
                Case 0
                    Core.LooterTimerObj.StopTimer()
                    Core.AutoAttackerTimerObj.StopTimer()
                    Core.CaveBotTimerObj.StopTimer()
                    Core.EaterTimerObj.StopTimer()
                    Core.EaterTimerObj.Interval = 0
                    Core.WaypointIndex = 0
                    Core.IsOpeningReady = True
                    SP.StopEverything()
                    'Core.Proxy.SendPacketToServer(PacketUtils.AttackEntity(0))
                    Core.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
                    Core.ConsoleWrite("Cavebot is now Disabled.")
                Case 1
                    If Core.Walker_Waypoints.Count = 0 Then
                        Core.ConsoleWrite("No waypoints found.")
                        Exit Sub
                    End If
                    If Consts.LootWithCavebot Then
                        Core.LooterMinimumCapacity = Consts.CavebotLootMinCap
                        Core.LooterTimerObj.StartTimer()
                    End If
                    Core.AutoAttackerTimerObj.StartTimer()
                    Core.CaveBotTimerObj.StartTimer()
                    Core.AutoEaterSmart = 0
                    Core.EaterTimerObj.Interval = 20000
                    Core.EaterTimerObj.StartTimer()
                    Core.IsOpeningReady = True
                    Core.CBCreatureDied = False
                    Core.WaypointIndex = 0
                    Core.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                    SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                    'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                    Core.ConsoleWrite("Cavebot is now Enabled.")
                    Core.CBState = CavebotState.Walking
                Case Else 'ADD or Continue
                    If Arguments(2).ToString = "continue" Then
                        If Core.Walker_Waypoints.Count = 0 Then
                            Core.ConsoleWrite("No waypoints found.")
                            Exit Sub
                        End If
                        Core.WaypointIndex = SelectNearestWaypoint(Core.Walker_Waypoints)
                        If Core.WaypointIndex = -1 Then
                            Core.ConsoleError("No waypoints found on this floor.")
                            Exit Sub
                        End If
                        If Consts.LootWithCavebot Then
                            Core.LooterMinimumCapacity = Consts.CavebotLootMinCap
                            Core.LooterTimerObj.StartTimer()
                        End If
                        Core.AutoAttackerTimerObj.StartTimer()
                        Core.CaveBotTimerObj.StartTimer()
                        Core.AutoEaterSmart = 0
                        Core.EaterTimerObj.Interval = 20000
                        Core.EaterTimerObj.StartTimer()
                        Core.IsOpeningReady = True
                        Core.CBCreatureDied = False
                        Core.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                        Core.ConsoleWrite("Cavebot is now Enabled.")
                        Core.CBState = CavebotState.Walking
                        Exit Sub
                    End If
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "add\s+(walk|ladder|rope|sewer|w|l|r|s)", RegexOptions.IgnoreCase)
                    If MatchObj.Success Then
                        Dim BL As New BattleList
                        Dim Character As New Walker
                        Dim WPType As String
                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                        Character.Coordinates = BL.GetLocation
                        If Walker.CheckDistance = False Then Exit Sub

                        Select Case MatchObj.Groups(1).Value.ToLower
                            Case "walk", "w"
                                Character.Type = Walker.WaypointType.Walk
                                WPType = "W"
                                Core.ConsoleWrite("Walking waypoint added.")
                            Case "ladder", "l"
                                Character.Type = Walker.WaypointType.Ladder
                                WPType = "L"
                                Core.ConsoleWrite("Ladder waypoint added.")
                            Case "rope", "r"
                                Character.Type = Walker.WaypointType.Rope
                                WPType = "R"
                                Core.ConsoleWrite("Rope waypoint added.")
                            Case "sewer", "s"
                                Character.Type = Walker.WaypointType.Sewer
                                WPType = "SE"
                                Core.ConsoleWrite("Sewer waypoint added.")
                            Case Else
                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select

                        Core.Walker_Waypoints.Add(Character)
                    Else
                        MatchObj = Regex.Match(Arguments(2).ToString, "add\s+(hole|stairs*)\s+(up|down|left|right|north|south|east|west)")
                        If MatchObj.Success Then
                            Dim BL As New BattleList
                            Dim Character As New Walker
                            Dim WPType As String
                            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                            Character.Coordinates = BL.GetLocation
                            If Walker.CheckDistance = False Then Exit Sub

                            Character.Type = Walker.WaypointType.StairsOrHole
                            WPType = "S/H"
                            Select Case MatchObj.Groups(2).ToString
                                Case "up", "north"
                                    Character.Coordinates.Y -= 1
                                Case "left", "west"
                                    Character.Coordinates.X -= 1
                                Case "down", "south"
                                    Character.Coordinates.Y += 1
                                Case "right", "east"
                                    Character.Coordinates.X += 1
                                Case Else
                                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                    Exit Sub
                            End Select

                            Core.Walker_Waypoints.Add(Character)
                            Core.ConsoleWrite(MatchObj.Groups(1).ToString & " waypoint added to direction " & MatchObj.Groups(2).ToString & ".")
                        Else
                            MatchObj = Regex.Match(Arguments(2).ToString, "add\swait\s(\d{1,5})")
                            If MatchObj.Success Then
                                Dim WPType As String
                                Dim Character As New Walker
                                Dim BL As New BattleList
                                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                If Walker.CheckDistance = False Then Exit Sub
                                Character.Coordinates = BL.GetLocation
                                Character.Type = Walker.WaypointType.Wait
                                Character.Info = MatchObj.Groups(1).ToString
                                WPType = "WT"
                                Core.Walker_Waypoints.Add(Character)
                                Core.ConsoleWrite("Wait waypoint added.")
                            Else
                                MatchObj = Regex.Match(Arguments(2).ToString, "add\ssay\s+""?(.+)$")
                                If MatchObj.Success Then
                                    If Walker.CheckDistance = False Then Exit Sub
                                    Dim WPType As String
                                    Dim Character As New Walker
                                    Dim BL As New BattleList
                                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                    Character.Coordinates = BL.GetLocation
                                    Character.Type = Walker.WaypointType.Say
                                    Character.Info = MatchObj.Groups(1).ToString
                                    WPType = "S"

                                    Core.Walker_Waypoints.Add(Character)
                                    Core.ConsoleWrite("Say waypoint added.")
                                Else
                                    MatchObj = Regex.Match(Arguments(2).ToString, "add/sshovel\s+(up|down|left|right|north|south|east|west)")
                                    If MatchObj.Success Then
                                        If Walker.CheckDistance = False Then Exit Sub
                                        Dim BL As New BattleList
                                        Dim Character As New Walker
                                        Dim WPType As String
                                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                        Character.Coordinates = BL.GetLocation

                                        Character.Type = Walker.WaypointType.Shovel
                                        WPType = "SH"
                                        Select Case MatchObj.Groups(2).ToString
                                            Case "up", "north"
                                                Character.Info = Walker.Directions.Up
                                            Case "left", "west"
                                                Character.Info = Walker.Directions.Left
                                            Case "down", "south"
                                                Character.Info = Walker.Directions.Down
                                            Case "right", "east"
                                                Character.Info = Walker.Directions.Up
                                            Case Else
                                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                                Exit Sub
                                        End Select
                                        Core.Walker_Waypoints.Add(Character)
                                    Else
                                        MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(learn|auto|automatic|automatically|learning|l)\s(on|off)")
                                        If MatchObj.Success Then
                                            UpdatePlayerPos()
                                            Select Case StrToShort(MatchObj.Groups(2).ToString)
                                                Case 1
                                                    Core.LearningMode = True
                                                    'AutoAddTime = Now.AddSeconds(10)
                                                    Core.LastFloor = Core.CharacterLoc.Z
                                                    Core.AutoAddTimerObj.StartTimer()
                                                    Core.ConsoleWrite("Adding waypoints automatically.")
                                                Case 0
                                                    Core.LearningMode = False
                                                    Core.AutoAddTimerObj.StopTimer()
                                                    Core.ConsoleWrite("Stopped adding waypoints automatically.")
                                            End Select
                                        Else
                                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Walker Command "
    Private Sub CmdWalker(ByVal Arguments As GroupCollection)
        Try
            Dim value As String = Arguments(2).ToString.ToLower
            Select Case StrToShort(value)
                Case 0
                    Core.WalkerTimerObj.StopTimer()
                    Core.WalkerLoop = False
                    Core.WaypointIndex = 0
                    Core.ConsoleWrite("Walker is now Disabled.")
                Case 1
                    If Core.Walker_Waypoints.Count = 0 Then
                        Core.ConsoleError("No Waypoints Found")
                        Exit Sub
                    End If
                    Core.WaypointIndex = 0
                    Core.WalkerTimerObj.StartTimer()
                    Core.WalkerLoop = False
                    Core.ConsoleWrite("Walker is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "continue", RegexOptions.IgnoreCase)
                    If MatchObj.Success Then
                        Core.WalkerTimerObj.StartTimer()
                        Core.WalkerLoop = True
                        Core.ConsoleWrite("Walker On With Continue Mode")
                    Else
                        MatchObj = (Regex.Match(Arguments(2).ToString, "add\s+(walk|ladder|rope|sewer|w|l|r|s)", RegexOptions.IgnoreCase))
                        If MatchObj.Success Then
                            Dim BL As New BattleList
                            Dim Character As New Walker
                            Dim WPType As String
                            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                            Character.Coordinates = BL.GetLocation
                            If Walker.CheckDistance = False Then Exit Sub

                            Select Case MatchObj.Groups(1).Value.ToLower
                                Case "walk", "w"
                                    Character.Type = Walker.WaypointType.Walk
                                    WPType = "W"
                                    Core.ConsoleWrite("Walking waypoint added.")
                                Case "ladder", "l"
                                    Character.Type = Walker.WaypointType.Ladder
                                    WPType = "L"
                                    Core.ConsoleWrite("Ladder waypoint added.")
                                Case "rope", "r"
                                    Character.Type = Walker.WaypointType.Rope
                                    WPType = "R"
                                    Core.ConsoleWrite("Rope waypoint added.")
                                Case "sewer", "s"
                                    Character.Type = Walker.WaypointType.Sewer
                                    WPType = "SE"
                                    Core.ConsoleWrite("Sewer waypoint added.")
                                Case Else
                                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                    Exit Sub
                            End Select

                            Core.Walker_Waypoints.Add(Character)
                        Else
                            MatchObj = Regex.Match(Arguments(2).ToString, "add\s+(hole|stairs*)\s+(up|down|left|right|north|south|east|west)")
                            If MatchObj.Success Then
                                Dim BL As New BattleList
                                Dim Character As New Walker
                                Dim WPType As String
                                BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                Character.Coordinates = BL.GetLocation
                                If Walker.CheckDistance = False Then Exit Sub

                                Character.Type = Walker.WaypointType.StairsOrHole
                                WPType = "S/H"
                                Select Case MatchObj.Groups(2).ToString
                                    Case "up", "north"
                                        Character.Coordinates.Y -= 1
                                    Case "left", "west"
                                        Character.Coordinates.X -= 1
                                    Case "down", "south"
                                        Character.Coordinates.Y += 1
                                    Case "right", "east"
                                        Character.Coordinates.X += 1
                                    Case Else
                                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                        Exit Sub
                                End Select

                                Core.Walker_Waypoints.Add(Character)
                                Core.ConsoleWrite(MatchObj.Groups(1).ToString & " waypoint added to direction " & MatchObj.Groups(2).ToString & ".")
                            Else
                                MatchObj = Regex.Match(Arguments(2).ToString, "add\swait\s(\d{1,5})")
                                If MatchObj.Success Then
                                    Dim WPType As String
                                    Dim Character As New Walker
                                    Dim BL As New BattleList
                                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                    If Walker.CheckDistance = False Then Exit Sub
                                    Character.Coordinates = BL.GetLocation
                                    Character.Type = Walker.WaypointType.Wait
                                    Character.Info = MatchObj.Groups(1).ToString
                                    WPType = "WT"
                                    Core.Walker_Waypoints.Add(Character)
                                    Core.CavebotForm.Waypointslst.Items.Add(WPType & ": Wait: " & Character.Info)
                                    Core.ConsoleWrite("Wait waypoint added.")
                                Else
                                    MatchObj = Regex.Match(Arguments(2).ToString, "add\ssay\s+""?(.+)$")
                                    If MatchObj.Success Then
                                        If Walker.CheckDistance = False Then Exit Sub
                                        Dim WPType As String
                                        Dim Character As New Walker
                                        Dim BL As New BattleList
                                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                        Character.Coordinates = BL.GetLocation
                                        Character.Type = Walker.WaypointType.Say
                                        Character.Info = MatchObj.Groups(1).ToString
                                        WPType = "S"

                                        Core.Walker_Waypoints.Add(Character)
                                        Core.ConsoleWrite("Say waypoint added.")
                                    Else
                                        MatchObj = Regex.Match(Arguments(2).ToString, "add/sshovel\s+(up|down|left|right|north|south|east|west)")
                                        If MatchObj.Success Then
                                            If Walker.CheckDistance = False Then Exit Sub
                                            Dim BL As New BattleList
                                            Dim Character As New Walker
                                            Dim WPType As String
                                            BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                            Character.Coordinates = BL.GetLocation

                                            Character.Type = Walker.WaypointType.Shovel
                                            WPType = "SH"
                                            Select Case MatchObj.Groups(2).ToString
                                                Case "up", "north"
                                                    Character.Info = Walker.Directions.Up
                                                Case "left", "west"
                                                    Character.Info = Walker.Directions.Left
                                                Case "down", "south"
                                                    Character.Info = Walker.Directions.Down
                                                Case "right", "east"
                                                    Character.Info = Walker.Directions.Up
                                                Case Else
                                                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                                    Exit Sub
                                            End Select
                                            Core.Walker_Waypoints.Add(Character)
                                        Else
                                            MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(learn|auto|automatic|automatically|learning|l)\s(on|off)")
                                            If MatchObj.Success Then
                                                UpdatePlayerPos()
                                                Select Case StrToShort(MatchObj.Groups(2).ToString)
                                                    Case 1
                                                        Core.LastFloor = Core.CharacterLoc.Z
                                                        Core.LearningMode = True
                                                        'AutoAddTime = Now.AddSeconds(10)
                                                        Core.AutoAddTimerObj.StartTimer()
                                                        Core.ConsoleWrite("Adding waypoints automatically.")
                                                    Case 0
                                                        Core.LearningMode = False
                                                        Core.AutoAddTimerObj.StopTimer()
                                                        Core.ConsoleWrite("Stopped adding waypoints automatically.")
                                                End Select
                                            Else
                                                Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Combobot Command "
    Private Sub CmdCombobot(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.ComboBotEnabled = False
                    Core.ConsoleWrite("Combobot is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, """([^""]+)")
                    If MatchObj.Success Then
                        Core.ComboBotLeader = MatchObj.Groups(1).ToString
                        Core.ComboBotEnabled = True
                        Core.ConsoleWrite("Combobot is now Enabled with Leader: " & Core.ComboBotLeader)
                        Exit Sub
                    Else
                        If Value = "leader" Then
                            Dim BL As New BattleList
                            BL.Reset()
                            If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) OrElse BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                If BL.IsPlayer Then
                                    Core.ComboBotLeader = BL.GetName
                                    Core.ComboBotEnabled = True
                                    Core.ConsoleWrite("Combobot is now Enabled with Leader: " & Core.ComboBotLeader)
                                    Exit Sub
                                Else
                                    Core.ConsoleError("You can only set players as leader.")
                                    Exit Sub
                                End If
                            Else
                                Core.ConsoleError("You need to Attack/Follow player to set him/her as leader.")
                                Exit Sub
                            End If
                            Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End If
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Amulet/Necklace Changer"
    Private Sub CmdAmuletChanger(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).Value
            Select Case StrToShort(Value)
                Case 0
                    Core.AmuletChangerTimerObj.StopTimer()
                    Core.AmuletID = 0
                    Core.ConsoleWrite("Amulet/Necklace Changer is now Disabled.")
                Case 1
                    Dim ItemID As Integer
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Neck - 1) * Consts.ItemDist), ItemID, 2)
                    If ItemID = 0 Then
                        Core.ConsoleError("You are not wearing any amulet. Please equip the amulet that you want to restack.")
                        Exit Sub
                    End If
                    Core.AmuletID = ItemID
                    Core.AmuletChangerTimerObj.StartTimer()
                    Core.ConsoleWrite("Amulet/Necklace Changer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, """([^""]+)")
                    If MatchObj.Success Then
                        Core.AmuletID = Core.Client.Items.GetItemID(MatchObj.Groups(1).ToString)
                        If Core.AmuletID = 0 AndAlso Core.Client.Items.IsNeck(Core.AmuletID) Then
                            Core.ConsoleError("Invalid Amulet/Necklace Name.")
                            Exit Sub
                        End If
                        Core.AmuletChangerTimerObj.StartTimer()
                        Core.ConsoleWrite("Amulet/Necklace Changer is now Enabled.")
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Ring Changer"
    Private Sub CmdRingChanger(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).Value)
                Case 0
                    Core.RingChangerTimerObj.StopTimer()
                    Core.RingID = 0
                    Core.ConsoleWrite("Ring Changer is now Disabled.")
                Case 1
                    Dim ItemID As Integer
                    Core.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Finger - 1) * Consts.ItemDist), ItemID, 2)
                    If ItemID = 0 Then
                        Core.ConsoleError("You are not wearing any ring. Please equip the ring that you want to change.")
                        Exit Sub
                    End If
                    Core.RingID = ItemID
                    Core.RingChangerTimerObj.StartTimer()
                    Core.ConsoleWrite("Ring Changer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, """([^""]+)")
                    If MatchObj.Success Then
                        Core.RingID = Core.Client.Items.GetItemID(MatchObj.Groups(1).ToString)
                        If Core.RingID = 0 AndAlso Core.Client.Items.IsRing(Core.RingID) Then
							Core.ConsoleError("Invalid Ring Name.")
							Exit Sub
						End If
						Core.RingChangerTimerObj.StartTimer()
						Core.ConsoleWrite("Ring Changer is now Enabled.")
					Else
						Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
					End If
			End Select
		Catch Ex As Exception
			MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
#End Region

#Region " Anti-Logout"
    Private Sub CmdAntiLogout(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).Value)
                Case 0
                    Core.AntiLogoutObj.StopTimer()
                    Core.ConsoleWrite("Anti-Logout is now Disabled.")
                Case 1
                    Core.LastActivity = Date.Now
                    Core.AntiLogoutObj.Interval = Consts.AntiLogoutInterval
                    Core.AntiLogoutObj.StartTimer()
                    Core.ConsoleWrite("Anti-Logout is now Enabled.")
                Case Else
                    Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Dancer "
    Private Sub CmdDancer(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).Value)
                Case 0
                    Core.DancerTimerObj.StopTimer()
                    Core.ConsoleWrite("Dancer is now Disabled.")
                Case 1
                    Core.DancerTimerObj.Interval = 1000
                    Core.DancerTimerObj.StartTimer()
                    Core.ConsoleWrite("Dancer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, "(\w+)")
                    If MatchObj.Success Then
                        Select Case MatchObj.Groups(1).ToString.ToLower
                            Case "random"
                                Core.DancerTimerObj.Interval = (New Random()).Next(300, 1500)
                                Core.DancerTimerObj.StartTimer()
                                Core.ConsoleWrite("Dancer is now Enabled with Random Speed")
                            Case "slower"
                                Core.DancerTimerObj.Interval = 1000
                                Core.DancerTimerObj.StartTimer()
                                Core.ConsoleWrite("Dancer is now Enabled with Slower Speed")
                            Case "slow"
                                Core.DancerTimerObj.Interval = 500
                                Core.DancerTimerObj.StartTimer()
                                Core.ConsoleWrite("Dancer is now Enabled with Slow Speed")
                            Case "fast"
                                Core.DancerTimerObj.Interval = 300
                                Core.DancerTimerObj.StartTimer()
                                Core.ConsoleWrite("Dancer is now Enabled with Fast Speed.")
                            Case "turbo"
                                Core.DancerTimerObj.Interval = 200
                                Core.DancerTimerObj.StartTimer()
                                Core.ConsoleWrite("Dancer is now Enabled with Turbo Mode")
                            Case Else
                                Core.ConsoleWrite("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Ammo Maker "
    Private Sub CmdAmmoMaker(ByVal Arguments As GroupCollection)
        Try
            Dim Value As String = Arguments(2).ToString
            Select Case StrToShort(Value)
                Case 0
                    Core.AmmoMakerMinMana = 0
                    Core.AmmoMakerMinCap = 0
                    Core.AmmoMakerTimerObj.StopTimer()
                    Core.ConsoleWrite("Ammunition Maker is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, "([1-9][0-9]{1,4})\s+([0-9]{0,3})\s+""([^""]+)""?")
                    If MatchObj.Success Then
                        Dim Found As Boolean = False
                        Dim S As New SpellDefinition
                        For Each Spell As SpellDefinition In CoreModule.Spells.SpellsList
                            If (Spell.Name.Equals(MatchObj.Groups(3).Value, StringComparison.CurrentCultureIgnoreCase) _
                            OrElse Spell.Words.Equals(MatchObj.Groups(3).ToString, StringComparison.CurrentCultureIgnoreCase)) _
                            AndAlso (Spell.Kind = SpellKind.Ammunition Or Spell.Kind = SpellKind.Incantation) Then
                                S = Spell
                                Found = True
                                Exit For
                            End If
                        Next
                        If Found Then
                            Core.AmmoMakerSpell = S
                            Core.AmmoMakerMinMana = CInt(MatchObj.Groups(1).Value)
                            Core.AmmoMakerMinCap = CInt(MatchObj.Groups(2).Value)
                            Core.AmmoMakerTimerObj.StartTimer()
                            Core.ConsoleWrite("Ammo Maker is now Enabled.")
                        Else
                            Core.ConsoleError("Invalid Conjure: Spell Name or Spell Words .")
                        End If
                    Else
                        Core.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Module
