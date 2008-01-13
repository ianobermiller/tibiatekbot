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

Imports Scripting, System.Text.RegularExpressions, System.Xml, System.Net, _
    System.Runtime.InteropServices, System.ComponentModel

Public Class CommandParser
    Implements ICommandParser

    Private Commands As Dictionary(Of String, ICommandParser.CommandCallback)

    Public Sub New()
        Commands = New Dictionary(Of String, ICommandParser.CommandCallback)
        AddDefaultCommands()
    End Sub

    Private Sub AddDefaultCommands()
        Try
            Add(New String() {"help", "h", "?", "halp", "f1", "sos", "ayuda", "ajuda", "sos"}, AddressOf CmdHelp)
            Add(New String() {"exp", "experience"}, AddressOf CmdExp)
            Add(New String() {"attack", "atk", "attacker", "autoattack"}, AddressOf CmdAttack)
            Add(New String() {"spell", "spellcaster"}, AddressOf CmdSpell)
            Add(New String() {"eat", "eater", "ate", "autoeater", "autoeat"}, AddressOf CmdEat)
            Add(New String() {"uh", "autouh", "uher", "autouher"}, AddressOf CmdUH)
            Add(New String() {"heal", "healer", "autoheal", "autohealer"}, AddressOf CmdHeal)
            Add(New String() {"fish", "fisher", "autofish", "autofisher"}, AddressOf CmdFisher)
            Add(New String() {"loot", "looter", "autolooter", "autoloot"}, AddressOf CmdLoot)
            Add(New String() {"advertise", "advertiser"}, AddressOf CmdAdvertise)
            Add(New String() {"guild", "guildmembers"}, AddressOf CmdGuild)
            Add(New String() {"runemaker", "runemake"}, AddressOf CmdRunemaker)
            Add(New String() {"ammo", "ammorestacker"}, AddressOf CmdAmmoRestacker)
            Add(New String() {"pickup", "autopickup"}, AddressOf CmdPickUp)
            Add(New String() {"watch", "tradewatcher", "watcher", "tradewatch", "watchtrade"}, AddressOf CmdTradeWatcher)
            Add(New String() {"stack", "stacker", "autostacker", "autostack"}, AddressOf CmdStacker)
            Add(New String() {"feedback", "bug", "comment", "bugreport", "report"}, AddressOf CmdFeedback)
            Add(New String() {"trainer", "autotrainer", "train", "autotrain"}, AddressOf CmdTrainer)
            Add(New String() {"chameleon", "outfit", "outfits"}, AddressOf CmdChameleon)
            Add(New String() {"commands", "list", "command", "listing", "cmd", "cmds"}, AddressOf CmdCommands)
            Add(New String() {"fps", "fpschanger"}, AddressOf CmdFpsChanger)
            Add(New String() {"rainbow", "rainbowoutfit"}, AddressOf CmdRainbow)
            Add(New String() {"namespy", "xray"}, AddressOf CmdNameSpy)
            Add(New String() {"website", "home", "homepage", "webpage"}, AddressOf CmdWebsite)
            Add(New String() {"drinker", "drink", "manadrinker", "mf", "manafluiddrinker", "manafluid"}, AddressOf CmdDrinker)
            Add(New String() {"combo", "combobot"}, AddressOf CmdCombobot)
            Add(New String() {"amulet", "amuletchanger", "ammychanger", "ammy"}, AddressOf CmdAmuletChanger)
            Add(New String() {"ring", "ringchanger"}, AddressOf CmdRingChanger)
            Add(New String() {"antilog", "antilogout", "antiidle", "antiidler"}, AddressOf CmdAntiLogout)
            Add(New String() {"dance", "dancer"}, AddressOf CmdDancer)
            Add(New String() {"ammomaker", "ammomake", "makeammo", "ammunitionmaker", "makeammunition"}, AddressOf CmdAmmoMaker)
            Add(New String() {"botstate", "state"}, AddressOf CmdBotState)
            Add("light", AddressOf CmdLight)
            Add("about", AddressOf CmdAbout)
            Add("look", AddressOf CmdLook)
            Add("healfriend", AddressOf CmdHealFriend)
            Add("healparty", AddressOf CmdHealParty)
            Add("faketitle", AddressOf CmdFakeTitle)
            Add("getitemid", AddressOf CmdGetItemId)
            Add("version", AddressOf CmdVersion)
            Add("test", AddressOf CmdTest)
            Add("char", AddressOf CmdChar)
            Add("open", AddressOf CmdOpen)
            Add("animation", AddressOf CmdAnimation)
            Add("statsuploader", AddressOf CmdStatsUploader)
            Add("mapviewer", AddressOf CmdMapViewer)
            Add("config", AddressOf CmdConfig)
            Add("hotkeys", AddressOf CmdHotkeys)
            Add("sendlocation", AddressOf CmdSendLocation)
            Add("cavebot", AddressOf CmdCaveBot)
            Add("walker", AddressOf CmdWalker)
            Add("irc", AddressOf CmdIrc)
            Add("viewmsg", AddressOf CmdViewMessage)
            Add("log", AddressOf CmdLog)
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function Add(ByVal CommandNames() As String, ByVal Callback As ICommandParser.CommandCallback) As Boolean Implements ICommandParser.Add
        Try
            If CommandNames.Length > 0 OrElse Callback Is Nothing Then
                For Each CommandName As String In CommandNames
                    CommandName = CommandName.ToLower
                    If Add(CommandName, Callback) = False Then
                        Return False
                    End If
                Next
                Return True
            Else
                Return False
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Add(ByVal CommandName As String, ByVal Callback As ICommandParser.CommandCallback) As Boolean Implements Scripting.ICommandParser.Add
        Try
            CommandName = CommandName.ToLower
            If Commands.ContainsKey(CommandName) Then
                Return False
            Else
                Commands.Add(CommandName, Callback)
                Return True
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Invoke(ByVal CommandAndParameter As String) As Boolean Implements ICommandParser.Invoke
        Try
            Dim MatchObj As Match = Regex.Match(CommandAndParameter, "^([a-zA-Z]+)\s*([^;]*)$")
            If MatchObj.Success Then
                Return Invoke(MatchObj.Groups(1).Value, MatchObj.Groups)
            End If
            Return False
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Invoke(ByVal CommandName As String, ByVal Arguments As System.Text.RegularExpressions.GroupCollection) As Boolean Implements Scripting.ICommandParser.Invoke
        Try
            If Commands.ContainsKey(CommandName) Then
                Commands(CommandName).Invoke(Arguments)
                Return True
            Else
                Kernel.ConsoleError("This command does not exist." & Ret & _
                    "  For a list of available commands type: &help.")
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Remove(ByVal CommandName As String) As Boolean Implements Scripting.ICommandParser.Remove
        Try
            If Commands.ContainsKey(CommandName) Then
                Commands.Remove(CommandName)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function Remove(ByVal CommandNames() As String) As Boolean Implements ICommandParser.Remove
        Try
            For Each CommandName As String In CommandNames
                If Not Remove(CommandName) Then Return False
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#Region " View Message "

    Private Sub CmdViewMessage(ByVal Arguments As GroupCollection)

        If Kernel.TTMessages = 0 Then
            Kernel.ConsoleError("You have no messages from the TibiaTek Development Team.")
        Else
            Try
                Dim Temp As Integer = 0
                Kernel.Client.UnprotectMemory(Consts.ptrForYourInformation, 20)
                Kernel.Client.WriteMemory(Consts.ptrForYourInformation, "Viewing Message")
                Dim WClient As New WebClient
                WClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Dim XMLResponse As String = WClient.UploadString(BotWebsite & "/viewmessages.php", "POST", "name=" & Web.HttpUtility.UrlEncode(Kernel.Client.CharacterName) & "&world=" & Web.HttpUtility.UrlEncode(Kernel.Client.CharacterWorld))
                Dim Document As New XmlDocument()
                Document.LoadXml(XMLResponse)
                Dim Messages As XmlElement = Document.Item("Messages")
                For Each Message As XmlElement In Messages
                    If Not String.IsNullOrEmpty(Message.InnerText) Then
                        Dim ClientPacket As New ClientPacketBuilder(Kernel.Proxy)
                        ClientPacket.FYIBox(Message.InnerText)
                        ClientPacket.Send()
                        'Core.Proxy.SendPacketToClient(FYIBox(Message.InnerText))
                    End If
                Next
                Kernel.ConsoleWrite("Successfully fetched all messages.")
            Catch Ex As Exception
                Kernel.ConsoleError("Unable to fetch the messages.")
            End Try
        End If
    End Sub

#End Region

#Region " Irc Command "
    Private Sub CmdIrc(ByVal Arguments As GroupCollection)
        Try
            If Not Kernel.IRCClient.IsConnected Then
                Kernel.ConsoleError("You are not connected to IRC.")
                Exit Sub
            End If
            Dim Match As Match = Regex.Match(Arguments(2).Value, "(join|nick|users)\s""?([^""]+)""?", RegexOptions.IgnoreCase)
            If Match.Success Then
                Dim CP As New ClientPacketBuilder(Kernel.Proxy)
                Select Case Match.Groups(1).Value.ToLower
                    Case "join"
                        If Kernel.IrcChannelIsOpened(Match.Groups(2).Value) Then
                            CP.OpenChannel(Match.Groups(2).Value, Kernel.IrcChannelNameToID(Match.Groups(2).Value))
                            'OpenIrcChannel(Match.Groups(2).Value, Core.IrcChannelNameToID(Match.Groups(2).Value))
                        Else
                            Kernel.IRCClient.Join(Match.Groups(2).Value)
                            Kernel.ConsoleWrite("You are now joining the channel " & Match.Groups(2).Value & ".")
                        End If
                    Case "nick"
                        If Kernel.IRCClient.Nick.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) Then
                            Kernel.ConsoleError("Your current nickname is the same.")
                        Else
                            Kernel.IRCClient.Nick = Match.Groups(2).Value
                            Kernel.IRCClient.WriteLine("NICK " & Kernel.IRCClient.Nick)
                            Kernel.ConsoleWrite("Trying to change your IRC nickname...")
                        End If
                    Case "users"
                        Dim Channel As String = Match.Groups(2).Value
                        If Kernel.IrcChannelIsOpened(Channel) Then
                            Dim TempNick As String = ""
                            For Each Nick As String In Kernel.IRCClient.Channels(Channel).Users.Keys
                                Select Case Kernel.IRCClient.GetUserLevel(Nick, Channel)
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
                                Kernel.ConsoleWrite(TempNick)
                            Next
                        Else
                            Kernel.ConsoleError("You are not in this channel.")
                        End If
                End Select
            Else
                Select Case Arguments(2).Value.ToLower
                    Case "quit"
                        Kernel.IRCClient.Quit()
                    Case Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End Select
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Website Command "

    Private Sub CmdWebsite(ByVal Arguments As GroupCollection)
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
                    Kernel.Client.WriteMemory(Consts.ptrNameSpy, Consts.NameSpyDefault, 2)
                    Kernel.Client.WriteMemory(Consts.ptrNameSpy2, Consts.NameSpy2Default, 2)
                    Kernel.ConsoleWrite("Name Spy is now Disabled.")
                Case 1
                    Kernel.Client.WriteMemory(Consts.ptrNameSpy, &H9090, 2)
                    Kernel.Client.WriteMemory(Consts.ptrNameSpy2, &H9090, 2)
                    Kernel.NameSpyActivated = True
                    Kernel.ConsoleWrite("Name Spy is now Enabled.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " FPS Changer Command "

    Private Sub CmdFpsChanger(ByVal Arguments As GroupCollection)
        Try
            Kernel.FrameRateActive = Consts.FPSWhenActive
            Kernel.FrameRateInactive = Consts.FPSWhenInactive
            Kernel.FrameRateMinimized = Consts.FPSWhenMinimized
            Kernel.FrameRateHidden = Consts.FPSWhenHidden
            Select Case StrToShort(Arguments(2).Value)
                Case 0
                    Kernel.FPSChangerTimerObj.StopTimer()
                    Kernel.Client.SetFramesPerSecond(Kernel.FrameRateActive)
                    Kernel.ConsoleWrite("FPS Changer is now Disabled.")
                Case 1
                    Kernel.FPSChangerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("FPS Changer is now Enabled.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                If Not Kernel.BGWSendLocation.IsBusy Then
                    Kernel.ConsoleWrite("Please wait...")
                    Kernel.SendLocationDestinatary = MatchObj.Groups(1).Value
                    Kernel.BGWSendLocation.RunWorkerAsync()
                Else
                    Kernel.ConsoleError("Busy.")
                End If
            Else
                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                        Kernel.ConsoleWrite("Please wait...")
                        Kernel.Spells.LoadSpells()
                    Case "outfits", "outfit"
                        Kernel.ConsoleWrite("Please wait...")
                        Kernel.Outfits.LoadOutfits()
                    Case "items", "item"
                        Kernel.ConsoleWrite("Please wait...")
                        Kernel.Client.Items.Refresh()
                    Case "constants", "constant", "consts", "const"
                        Kernel.ConsoleWrite("Please wait...")
                        Consts.LoadConstants()
                    Case "tiles", "tile", "dat"
                        Kernel.ConsoleWrite("Please wait...")
                        Kernel.Client.Dat.Refresh()
                    Case Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        Exit Sub
                End Select
                Kernel.ConsoleWrite("Done reloading.")
            Catch
                Kernel.ConsoleError("Failed reloading.")
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
                    Dim SP As New ServerPacketBuilder(Kernel.Proxy)
                    SP.ChangeOutfit(BL2.OutfitID, CByte(BL2.HeadColor), CByte(BL2.BodyColor), CByte(BL2.LegsColor), CByte(BL2.FeetColor), CByte(BL2.OutfitAddons))
                    'Core.Proxy.SendPacketToServer(ChangeOutfit(BL2.OutfitID, CByte(BL2.HeadColor), CByte(BL2.BodyColor), CByte(BL2.LegsColor), CByte(BL2.FeetColor), CByte(BL2.OutfitAddons)))
                    Kernel.ConsoleWrite("Your outfit has been changed to " & BL.GetName & ".")
                End If
            Else
                MatchObj = Regex.Match(Arguments(2).ToString, """([^""]+)(?:""\s+(\d))?")
                If MatchObj.Success Then
                    Dim Request As String = MatchObj.Groups(1).Value
                    Dim Outfit As New IOutfits.OutfitDefinition
                    If Regex.IsMatch(Request, "^\d+$") Then
                        Found = Kernel.Outfits.GetOutfitByID(CUShort(Request), Outfit)
                    Else
                        Found = Kernel.Outfits.GetOutfitByName(Request, Outfit)
                    End If
                    If Found Then
                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                        BL.OutfitID = Outfit.ID

                        If Not String.IsNullOrEmpty(MatchObj.Groups(2).Value) Then
                            If CInt(MatchObj.Groups(2).Value) > 3 Then Exit Sub
                            BL.OutfitAddons = CType(CInt(MatchObj.Groups(2).Value), IBattlelist.OutfitAddons)
                        End If
                        Kernel.ConsoleWrite("Your outfit has been changed to " & Outfit.Name & ".")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
                Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.ConsoleError("Minimum Health Percent has to be less than 99%.")
                    Exit Sub
                ElseIf CInt(Match.Groups(2).Value) > 100 Then
                    Kernel.ConsoleError("Maximum Health Percent has to be less than 100%.")
                    Exit Sub
                ElseIf CInt(Match.Groups(1).Value) >= CInt(Match.Groups(2).Value) Then
                    Kernel.ConsoleError("Maximum Health Percent has to be higher than Minimum Health Percent.")
                    Exit Sub
                End If
                If Kernel.AutoTrainerEntities.Count = 0 Then
                    Kernel.ConsoleError("You have to add entities to the training list.")
                    Exit Sub
                End If
                Kernel.AutoTrainerMinHPPercent = CInt(Match.Groups(1).Value)
                Kernel.AutoTrainerMaxHPPercent = CInt(Match.Groups(2).Value)
                Kernel.AutoTrainerTimerObj.StartTimer()
                Kernel.ConsoleWrite("Auto Trainer will now attack the entities until " & Kernel.AutoTrainerMinHPPercent & "% of their health " & _
                 "and after their recover " & Kernel.AutoTrainerMaxHPPercent & "% of their health.")
            Else
                Select Case StrToShort(Arguments(2).Value)
                    Case 0
                        Kernel.AutoTrainerMinHPPercent = 0
                        Kernel.AutoTrainerMaxHPPercent = 0
                        Kernel.AutoTrainerTimerObj.StopTimer()
                        Kernel.ConsoleWrite("Auto Trainer is now Disabled.")
                    Case Else
                        Dim BL As New BattleList
                        Select Case Arguments(2).Value
                            Case "add", "agregar", "a"
                                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                                    If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                        Kernel.ConsoleError("You must be attacking or following something.")
                                        Exit Sub
                                    End If
                                End If
                                If Kernel.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                                    Kernel.ConsoleError("This entity is already in your list.")
                                Else
                                    Kernel.AutoTrainerEntities.Add(BL.GetEntityID)
                                    Kernel.ConsoleWrite("This entity has been added to your list.")
                                End If
                            Case "remove", "r", "remover", "quitar"
                                If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                                    If Not BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                        Kernel.ConsoleError("You must be attacking or following something.")
                                        Exit Sub
                                    End If
                                End If
                                If Kernel.AutoTrainerEntities.Contains(BL.GetEntityID) Then
                                    Kernel.AutoTrainerEntities.Remove(BL.GetEntityID)
                                    Kernel.ConsoleWrite("This entity has been removed from your list.")
                                Else
                                    Kernel.ConsoleError("This entity is not on your list.")
                                End If
                            Case "clear"
                                Kernel.AutoTrainerEntities.Clear()
                                Kernel.ConsoleWrite("Auto Trainer entities list cleared.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                End Select
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Help Command "

    Public Sub CmdHelp(ByVal Arguments As GroupCollection)
        Try
            Dim Topic As String = Arguments(2).ToString.ToLower
            Select Case Topic
                Case "light"
                    Kernel.ConsoleWrite("«Light Effect»" & Ret & _
                    "Usage: &light <on | off | torch | great torch | ultimate tor" & _
                    "ch | utevo lux | utevo gran lux | utevo vis lux | light wand>." & Ret & _
                    "Example: &light utevo lux." & Ret & _
                    "Comment:" & Ret & _
                    "  When darkness covers the lands, this command proves itsel" & _
                    "f to be very handy." & Ret & _
                    "Note: This command <<does not>> cast any spells whatsoever.")
                Case "exp"
                    Kernel.ConsoleWrite("«Experience Checker»" & Ret & _
                    "Usage: &exp <on | creatures <on | off> | off>." & Ret & _
                    "Example: &exp on." & Ret & _
                    "Example: &exp creatures on." & Ret & _
                    "Comment:" & Ret & _
                    " Keep yourself motivated by knowing how much little experie" & _
                    "nce you need until the next level!. With the new show creatures feature, you'll find out how many creatures you have left to kill." & Ret & _
                    "Note: The experience is shown on the title of the Tibia Client.")
                Case "trainer"
                    Kernel.ConsoleWrite("«Auto Trainer»" & Ret & _
                    "Usage: &trainer <add | remove | clear | stop |<minimum hp %> <maximum hp %>>>." & Ret & _
                    "Example: &trainer 50% 90%." & Ret & _
                    "Comment:" & Ret & _
                    " Train with as many monsters as you want. To add monsters, put them on follow and type &trainer add. " & _
                    "To start training type &trainer <min hp %> <max hp %>, and you will hurt the creatures until <min hp%> and continue attacking after <max hp%>. " & _
                    "To stop, &trainer stop.")
                Case "attack"
                    Kernel.ConsoleWrite("«Auto Attacker»" & Ret & _
                    "Usage: &attack <on | off | auto | stand | follow | offensive | balanced | " & _
                    "defensive | ""Player Name"" >." & Ret & _
                    "Example: &attack on." & Ret & _
                    "Comment:" & Ret & _
                    " Automatically attack any monsters that attack you, or if set to auto attacks monsters that are in screen (not touching to another player's creatures though)." & Ret & _
                    " To train with slimes, put the slime on follow when issuing &attack on.")
                Case "spell"
                    Kernel.ConsoleWrite("«Spell Caster»" & Ret & _
                    "Usage: &spell <off | <minimum mana points> <spell words>>." & Ret & _
                    "Example: &spell 400 exura vita """"Magic Level Plx!!." & Ret & _
                    "Comment:" & Ret & _
                    " Never be bothered again because you forgot to cast a spell and you " & _
                    "wasted mana!")
                Case "eat"
                    Kernel.ConsoleWrite("«Auto Eater/Smart Eater»" & Ret & _
                    "Usage: &eat <on | off | time in seconds | <smart <minimum hit points>> >." & Ret & _
                    "Example: &eat on." & Ret & _
                    "Example: &eat smart 600." & Ret & _
                    "Comment:" & Ret & _
                    " Ever felt hungry because you forgot to eat your meal? The" & _
                    " Auto Eater will make sure you bloat, but will also keep yo" & _
                    "u in a strict diet using the Smart option.")
                Case "uh"
                    Kernel.ConsoleWrite("«Auto UHer»" & Ret & _
                    "Usage: &uh <hit points | off>." & Ret & _
                    "Example: &uh 120." & Ret & _
                    "Comment:" & Ret & _
                    "  Feel safe because you will always UH yourself before it h" & _
                    "appens!")
                Case "look"
                    Kernel.ConsoleWrite("«Floor Explorer»" & Ret & _
                   "Usage: &look <around | up | above | down | below>." & Ret & _
                   "Example: &look below." & Ret & _
                   "Command:" & Ret & _
                   "  Find out what's below you before you go down the hole!" & Ret & _
                   "Note: This command won't tell you what's below you if you" & _
                   " are on the ground level, and it won't tell you what's ab" & _
                   "ove you if you are one level below ground.")
                Case "fisher"
                    Kernel.ConsoleWrite("«Auto Fisher»" & Ret & _
                    "Usage: &fisher <off  | <<minimum capacity> <normal | turbo>>>." & Ret & _
                    "Example: &fisher 6 normal." & Ret & _
                    "Comment:" & Ret & _
                    "  Have you ever tried fishing by yourself and noticed how b" & _
                    "oring and tiresome it gets? Well, now this is the solution " & _
                    "for you! Normal speed and turbo speed selector included ;).")
                Case "runemaker"
                    Kernel.ConsoleWrite("«Runemaker»" & Ret & _
                    "Usage: &runemaker <minimum mana points> <minimum soul points> ""<spell words or spell name>""." & Ret & _
                    "Example: &runemaker 400 2 ""ultimate healing""." & Ret & _
                    "Comment:" & Ret & _
                    "  TibiaTek Bot wouldn't be complete without a runemaker. T" & _
                    "his one will let you make runes even while hunting." & Ret & _
                    "Note: You must have the arrow slot empty, and there must at" & _
                    " least one container open with blank runes on it.")
                Case "char"
                    Kernel.ConsoleWrite("«Character Information Lookup»" & Ret & _
                    "Usage: &char ""<Player Name>""." & Ret & _
                    "Example: &char ""eternal oblivion""." & Ret & _
                    "Comment:" & Ret & _
                    " This command will let you retrieve the information of a character" & _
                    "without you having to open Tibia.com and search for it yourself.")
                Case "open"
                    Kernel.ConsoleWrite("«Open File/Website»" & Ret & _
                    "Usage: &open <""Local File or URL"" | <wiki | character | guild | erig | google | mytibia> ""<search terms>"" >." & Ret & _
                    "Example: &open ""notepad""." & Ret & _
                    "Example: &open wiki ""Banuta Quest""." & Ret & _
                    "Comment:" & Ret & _
                    " This command lets you open any file on your computer. It also " & _
                    "lets you open your browser to search in Tibia Wiki, Tibia.com Character " & _
                    "and Guild Pages, Erig's TOP Players, Google and Mytibia.")
                    'Case "admin"
                    'Core.ConsoleWrite("«Remote Administration»" & Ret & _
                    '"Usage: &admin <password | list | off>" & Ret & _
                    '"Example: &admin iRownz0rx." & Ret & _
                    '"Comment:" & Ret & _
                    '" Allows for remote administration from another player." & Ret & _
                    '"Note: For another player to become administrator, he/she has to send a " & _
                    '"private message to the player running TibiaTek Bot the command name " & _
                    '"followed by the password, example: admin iRownz0rx.")
                Case "pickup"
                    Kernel.ConsoleWrite("«Auto Pickup»" & Ret & _
                    "Usage: &pickup <on | off>" & Ret & _
                    "Example: &pickup on." & Ret & _
                    "Comment:" & Ret & _
                    " This command will pickup throwable objects automatically for you and " & _
                    "put them in your right hand. If you accidentally put another object in " & _
                    "your right hand it will be moved to your backpack.")
                Case "ammorestacker"
                    Kernel.ConsoleWrite("«Auto Ammunition Restacker»" & Ret & _
                    "Usage: &ammorestacker <minimum ammunition | off>" & Ret & _
                    "Example: &ammorestacker 50." & Ret & _
                    "Comment:" & Ret & _
                    " Just put the item that you want to restack in your Belt/Arrow Slot " & _
                    "and activate this command, and you'll always be fully equipped." & Ret & _
                    "Note: You will be warned when you are running out of items to restack.")
                Case "log"
                    Kernel.ConsoleWrite("«Events Logging»" & Ret & _
                    "Usage: &log <on | off>" & Ret & _
                    "Example: &log on." & Ret & _
                    "Comment:" & Ret & _
                    " Log all events, messages, etc. to Log.txt, useful if you want to know " & _
                    "what really happened." & Ret & _
                    "Note: To be used only when you are Away From Keyboard (AFK).")
                Case "healfriend"
                    Kernel.ConsoleWrite("«Auto Heal Friend»" & Ret & _
                    "Usage: &healfriend <minimum hit points percent>% ""<uh | sio | both>"" ""<player name>""." & Ret & _
                    "Example: &healfriend 50% ""sio"" ""Cameri deDurp""." & Ret & _
                    "Comment:" & Ret & _
                    "  Auto Heal Friend keeps your friend safe before it''s too late." & Ret & _
                    "Note: You can only heal one friend at a time, if you need to heal more people, use ""healparty"".")
                Case "guild"
                    Kernel.ConsoleWrite("«Guild Members Lookup»" & Ret & _
                    "Usage: &guild <online | both> ""<guild name>""." & Ret & _
                    "Example: &guild online ""Mercenaries""." & Ret & _
                    "Comment:" & Ret & _
                    "  Find out which guild members are online and which aren't." & Ret & _
                    "Note: Guild name is case-sensitive.")
                Case "faketitle"
                    Kernel.ConsoleWrite("«Fake Title»" & Ret & _
                    "Usage: &faketitle <off | ""<new title>"">." & Ret & _
                    "Example: &faketitle ""Firefox""." & Ret & _
                    "Comment:" & Ret & _
                    "  Never get caught again by your parents playing Tibia!.")
                Case "advertise"
                    Kernel.ConsoleWrite("«Trade Channel Advertiser»" & Ret & _
                    "Usage: &advertise ""<advertisement>""." & Ret & _
                    "Example: &advertise ""Sell Giant Sword, Wand of Plage ~ msg me""." & Ret & _
                    "Comment:" & Ret & _
                    "  This command advertises in the trade channel for you every 2 minutes.")
                Case "heal"
                    Kernel.ConsoleWrite("«Auto Healer»" & Ret & _
                    "Usage: &heal <minimum hit points percent | minimum hit points> ""<healing spell words or spell name>"" [""""<comment>]." & Ret & _
                    "Example: &heal 70% ""Intense Healing"" """"I never die~." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep yourself healthy at all times!.")
                Case "healparty"
                    Kernel.ConsoleWrite("«Auto Heal Party»" & Ret & _
                    "Usage: &healparty <minimum hit points percent>% ""<sio | uh | both>""." & Ret & _
                    "Example: &healparty 30% ""sio""." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep your party members safe, protect them because you could be next!.")
                Case "loot"
                    Kernel.ConsoleWrite("«Auto Looter»" & Ret & _
                    "Usage: &loot <on | minimum capacity | off>." & Ret & _
                    "Example: &loot 100." & Ret & _
                    "Comment:" & Ret & _
                    "  Tired of having to open the corpses? With this command you won't have to do anything.")
                Case "statsuploader"
                    Kernel.ConsoleWrite("«Stats Uploader»" & Ret & _
                    "Usage: &statsuploader <on | off>." & Ret & _
                    "Example: &statsuploader on." & Ret & _
                    "Comment:" & Ret & _
                    "  Generate an XML file with the stats of your character, upload it to the web or save it on your hard disk.")
                Case "mapviewer"
                    Kernel.ConsoleWrite("«Map Viewer»" & Ret & _
                    "Usage: &mapviewer." & Ret & _
                    "Example: &mapviewer." & Ret & _
                    "Comment:" & Ret & _
                    "  Show an <<experimental>> map viewer of your current Tibia. For informational purposes only.")
                Case "stacker"
                    Kernel.ConsoleWrite("«Auto Stacker»" & Ret & _
                    "Usage: &stacker <on | off>." & Ret & _
                    "Example: &stacker on." & Ret & _
                    "Comment:" & Ret & _
                    "  Automatically organize your backpacks with this auto stacker.")
                Case "config"
                    Kernel.ConsoleWrite("«Configuration Manager»" & Ret & _
                    "Usage: &config <load | edit | clear>." & Ret & _
                    "Example: &config edit." & Ret & _
                    "Comment:" & Ret & _
                    "  Want TibiaTek Bot to automatically start all the features you like " & _
                    "right after you log in? Type in the configuration manager the commands " & _
                    "as you would type them normally, each on one line or separated by semi-colons.")
                Case "hotkeys"
                    Kernel.ConsoleWrite("«Hotkey Settings Manager»" & Ret & _
                    "Usage: &hotkeys <save | load>." & Ret & _
                    "Example: &hotkeys save." & Ret & _
                    "Comment:" & Ret & _
                    "  Keep separate hotkey settings for each of your characters!")
                Case "feedback"
                    Kernel.ConsoleWrite("«Feedback, Comments, Bug reports»" & Ret & _
                    "Usage: &feedback." & Ret & _
                    "Example: &feedback." & Ret & _
                    "Comment:" & Ret & _
                    "  With this command, you can send me comments, bug reports and anything considered as feedback." & Ret & _
                    "Communication between the users and developers is very important! This is completely anonymous.")
                Case "chameleon"
                    Kernel.ConsoleWrite("«Chameleon»" & Ret & _
                    "Usage: &chameleon <""<outfit name or id>"" [addons 0-3] | copy ""<player name>"">." & Ret & _
                    "Example: &chameleon ""male citizen"" 3." & Ret & _
                    "Example: &chameleon ""dworc voodoomaster""." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to change your outfit to whatever you want, even copy your friend's outfit!")
                Case "watch"
                    Kernel.ConsoleWrite("«Trade Channel Watcher»" & Ret & _
                    "Usage: &watch <regular expression pattern | off>." & Ret & _
                    "Example: &watch ^.*bps*\s+of\s+uh.*$." & Ret & _
                    "Comment:" & Ret & _
                    "  This command will inform you of any offer in the trade channel that matches the pattern." & Ret & _
                    "See http://en.wikipedia.org/wiki/Regular_expression for more information on regular expressions.")
                Case "reload"
                    Kernel.ConsoleWrite("«Reload Data»" & Ret & _
                    "Usage: &reload <spells | outfits | items | constants | dat>." & Ret & _
                    "Example: &reload items." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to reload the files from the Data folder.")
                Case "list"
                    Kernel.ConsoleWrite("«Commands List»" & Ret & _
                    "Usage: &list." & Ret & _
                    "Example: &list." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to view all the bot's commands (In alphaphetical order).")
                Case "sendlocation"
                    Kernel.ConsoleWrite("«Send Location»" & Ret & _
                    "Usage: &sendlocation ""<player name>""." & Ret & _
                    "Example: &sendlocation ""Cameri de'Durp." & Ret & _
                    "Comment:" & Ret & _
                    "  Use this command to send other players a link to a map of your current position." & Ret & _
                    "The link points to a page that has the map of tibia, and a cursor pointing to your current location.")
                Case "rainbow"
                    Kernel.ConsoleWrite("«Rainbow Outfit»" & Ret & _
                    "Usage: &rainbow <on | off | fast | slow>." & Ret & _
                    "Example: &rainbow fast." & Ret & _
                    "Comment:" & Ret & _
                    "  With Rainbow Outfit you can amaze your friends and another people aroud you." & Ret & _
                    "It changes your outfit color repately." & Ret & _
                    "Note: Everyone will see your outfit changing.")
                Case "fpschanger"
                    Kernel.ConsoleWrite("«FPS Changer»" & Ret & _
                    "Usage: &fpschanger <on | off>." & Ret & _
                    "Example: &fpschanger on." & Ret & _
                    "Comment: " & Ret & _
                    "  If your computer is running slow, and you want to play Tibia AND do something else at the same time, " & _
                    " this command is right for you, it will enable you to lower/increase the FPS when your Tibia is " & _
                    "active, inactive, minimized and hidden.")
                Case "namespy"
                    Kernel.ConsoleWrite("«Name Spy»" & Ret & _
                    "Usage: &namespy <on | off>." & Ret & _
                    "Example: &namespy on." & Ret & _
                    "Comment: " & Ret & _
                    "  See the names/health bar of creatures or other players on a different floor than yours.")
                Case "website"
                    Kernel.ConsoleWrite("«Website»" & Ret & _
                    "Usage: &website." & Ret & _
                    "Example: &website." & Ret & _
                    "Comment: " & Ret & _
                    "  Opens up TibiaTek Bot's website.")
                Case "drinker"
                    Kernel.ConsoleWrite("«Auto Manafluid Drinker»" & Ret & _
                    "Usage: &drinker <minimum mana points | off>." & Ret & _
                    "Example: &drinker 350." & Ret & _
                    "Comment: " & Ret & _
                    "  Drinks vials with mana fluid from backpack when mana is lower than given mana." & Ret & _
                    "Note: Uses same delays as auto healer.")
                Case "cavebot"
                    Kernel.ConsoleWrite("«Cavebot»" & Ret & _
                    "Usage: &cavebot <on | off | continue | load ""Waypoint file"" | add <walk | rope | ladder | sewer> <hole | stairs | shovel <up | down | left | right>>" & Ret & _
                    "Example: &cavebot on." & Ret & _
                    "Example: &cavebot add stairs up." & Ret & _
                    "Comment: " & Ret & _
                    "  Cavebot hunts for you in caves. Just define the waypoints add you're ready to go." & Ret & _
                    "Note: Cavebot looting/eating uses same dealays as auto looter/eater." & Ret & _
                    "Note: Check Constants for more options.")
                Case "walker"
                    Kernel.ConsoleWrite("«Walker»" & Ret & _
                    "Usage: &walker <on | off | continue | load ""Waypoint file""" & Ret & _
                    "Example: &walker on." & Ret & _
                    "Comment: " & Ret & _
                    "  Walker simply walks from point A to point B." & Ret & _
                    "Note: When using continue mode, you should make waypoints to go circle" & Ret & _
                    "Note: Walker uses same waypoints as Cavebot, and you can add them with commands &walker add or &cavebot add. " & Ret & _
                    "  (type &help cavebot for more info about adding)")
                Case "combobot", "combo"
                    Kernel.ConsoleWrite("«Combobot»" & Ret & _
                    "Usage: &combobot <""Leader Name"" | off>." & Ret & _
                    "Example: &combobot ""Jokuperkele""." & Ret & _
                    "  Makes comboshots even easier (and more effective) what they are normally. " & Ret & _
                    "Note: Combobot fires rune when <leader name> shoots rune and to the same target" & Ret & _
                    "Note: At this point combobot works only with Sudden Death runes.")
                Case "amuletchanger"
                    Kernel.ConsoleWrite("«Amulet Changer»" & Ret & _
                    "Usage: &amuletchanger <on | off | ""Amulet name"">." & Ret & _
                    "Example: &amuletchanger ""Stone Skin Amulet""." & Ret & _
                    "  Times when you died because you didn't have time to change amulet are now over." & Ret & _
                    "Note: &amuletchanger on is using the amulet you have in your amulet-slot." & Ret & _
                    "Note: Names of amulets are case-sensitive (e.g stone skin amuet <> Stone Skin Amulet)")
                Case "antilogout", "anti-logout"
                    Kernel.ConsoleWrite("«Anti-Logout»" & Ret & _
                    "Usage: &antilogout <on | off>." & Ret & _
                    "Example: &antilogout on." & Ret & _
                    "Comment: " & Ret & _
                    "  Protects yourself from getting kicked because of inactivity.")
                Case "irc"
                    Kernel.ConsoleWrite("«IRC»" & Ret & _
                    "Usage: &irc <<users | join> ""<channel>"" | nick ""new nick"" | quit>." & Ret & _
                    "Example: &irc join ""#TibiaTekBot""." & Ret & _
                    "Example: &irc quit." & Ret & _
                    "Comment: " & Ret & _
                    "  Allows you to execute some of the common IRC commands.")
                Case "viewmsg"
                    Kernel.ConsoleWrite("«View Message»" & Ret & _
                    "Usage: &viewmsg." & Ret & _
                    "Example: &viewmsg." & Ret & _
                    "Comment: " & Ret & _
                    "  Let's you read any messages sent to you by the TibiaTek Development Team.")
                Case "dancer"
                    Kernel.ConsoleWrite("«Dancer»" & Ret & _
                    "Usage: &dancer slow|fast|turbo|on|off." & Ret & _
                    "Example: &dancer on." & Ret & _
                    "Comment " & Ret & _
                    "  Now you can proof that you are not using Multi Client.. Even if you really are.")
                Case "ammomaker", "ammunitionmaker"
                    Kernel.ConsoleWrite("«Ammunition Maker»" & Ret & _
                    "Usage: <minimum mana points> <minimum capacity> ""<spell words or spell name>""." & Ret & _
                    "Example: &ammomaker 250 100 ""bolt""." & Ret & _
                    "Comment " & Ret & _
                    "  Don't want to pay from ammo but too lazy to make them? With this command you can let bot to handle the " & Ret & _
                    " ammo making, meanwhile you can lay back and take a nice warm cup of coffee.")
                Case Else
                    Select Case Topic.ToLower
                        Case "general", "general tools", "a"
                            Kernel.ConsoleWrite("General Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Configuration Manager -> &config." & Ret & _
                            "  Hotkeys Settings Manager -> &hotkeys." & Ret & _
                            "  Auto Looter -> &loot." & Ret & _
                            "  Auto Stacker -> &stacker." & Ret & _
                            "  Light Effect -> &light." & Ret & _
                            "  Ammunition Restacker -> &ammorestacker." & Ret & _
                            "  Commands Lister -> &list" & Ret & _
                            "  FPS Changer -> &fpschanger." & Ret & _
                            "  Walker -> &walker" & Ret & _
                            "  Amulet Changer -> &amuletchanger" & Ret & _
                            "  Ring Changer -> &ringchanger" & Ret & _
                            "  Combobot -> &combobot" & Ret & _
                            "  Dancer -> &dancer")
                        Case "healing", "healing tools", "b"
                            Kernel.ConsoleWrite("Healing Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Auto UHer -> &uh." & Ret & _
                            "  Auto Healer -> &heal." & Ret & _
                            "  Auto Heal Friend -> &healfriend." & Ret & _
                            "  Auto Heal Party -> &healparty." & Ret & _
                            "  Mana Fluid Drinker -> &drinker")
                        Case "afking", "afking tools", "afk tools", "afk", "c"
                            Kernel.ConsoleWrite("AFKing Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Spell Caster -> &spell." & Ret & _
                            "  Auto Eater -> &eat." & Ret & _
                            "  Runemaker -> &runemaker." & Ret & _
                            "  Auto Fisher -> &fisher." & Ret & _
                            "  Trade Channel Advertiser -> &advertise." & Ret & _
                            "  Trade Channel Watcher -> &watch." & Ret & _
                            "  Events Logging -> &logger." & Ret & _
                            "  Cavebot -> &cavebot." & Ret & _
                            "  Stats Uploader -> &statsuploader." & Ret & _
                            "  Ammo Maker -> &ammomaker." & Ret & _
                            "  Anti-Logout -> &antilogout.") ' & Ret & _
                            '"  Remote Administration -> &admin.") ' & Ret & _
                        Case "info tools", "info", "d"
                            Kernel.ConsoleWrite("Info Tools:" & Ret & _
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
                            Kernel.ConsoleWrite("Training Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Auto Attacker -> &attack." & Ret & _
                            "  Auto Trainer -> &trainer." & Ret & _
                            "  Auto Pickup -> &pickup.")
                        Case "fun tools", "fun", "f"
                            Kernel.ConsoleWrite("Fun Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Fake Title -> &faketitle." & Ret & _
                            "  Chameleon -> &chameleon." & Ret & _
                            "  Rainbow Outfit -> &rainbow")
                        Case "miscellaneous tools", "misc", "miscellanoeus", "g"
                            Kernel.ConsoleWrite("Miscellaneous Tools:" & Ret & _
                            "  NAME -> COMMAND" & Ret & _
                            "  Feedback -> &feedback." & Ret & _
                            "  Reload Data -> &reload." & Ret & _
                            "  About Us -> &about." & Ret & _
                            "  View Message -> &viewmsg." & Ret & _
                            "  Website -> &website." & Ret & _
                            "  Version -> &version.")
                        Case Else
                            Kernel.ConsoleWrite("There are many command categories available, type help followed by the category to get a listing:" & Ret & _
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
            Kernel.Client.UnprotectMemory(CType(Consts.ptrEnterOneNamePerLine, System.IntPtr), CType(24, UIntPtr))
            Kernel.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Thank you for using TTB!")
            Dim CP As New ClientPacketBuilder(Kernel.Proxy)
            CP.HouseSpellEdit(&HFE, 0, "")
            'Core.Proxy.SendPacketToClient(HouseSpellEdit(&HFE, 0, ""))
            System.Threading.Thread.Sleep(500)
            Kernel.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Enter one name per line.")
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
                    Kernel.HotkeySettings.LoadFromMemory()
                    If Kernel.HotkeySettings.Save() Then
                        Kernel.ConsoleWrite("Hotkeys saved.")
                    Else
                        Kernel.ConsoleError("Unable to save hotkeys.")
                    End If
                Case "load", "reload"
                    Kernel.HotkeySettings.Load()
                    Kernel.ConsoleWrite("Hotkeys loaded.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                        Kernel.ConsoleWrite("Please wait...")
                        Dim Reader As IO.StreamReader
                        Reader = IO.File.OpenText(Kernel.GetProfileDirectory() & "\config.txt")
                        Data = Reader.ReadToEnd
                        Reader.Close()
                    Catch
                    Finally
                        Dim Temp As UInteger = 0
                        Kernel.Client.UnprotectMemory(Consts.ptrEnterOneNamePerLine, CType(24, UIntPtr))
                        Kernel.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Configuration Manager")
                        Dim CP As New ClientPacketBuilder(Kernel.Proxy)
                        CP.HouseSpellEdit(&HFF, 0, Data)
                        'Core.Proxy.SendPacketToClient(HouseSpellEdit(&HFF, 0, Data))
                        System.Threading.Thread.Sleep(500)
                        Kernel.Client.WriteMemory(Consts.ptrEnterOneNamePerLine, "Enter one name per line.")
                    End Try
                Case "clear", "delete", "del", "cls"
                    Try
                        Kernel.ConsoleWrite("Please wait...")
                        IO.File.Delete(Kernel.GetProfileDirectory() & "\config.txt")
                    Catch
                        Kernel.ConsoleError("Unable to clear your configuration.")
                    Finally
                        Kernel.ConsoleWrite("Cleared.")
                    End Try
                Case "load", "execute"
                    Try
                        Kernel.ConsoleWrite("Please wait...")
                        Dim Data As String = ""
                        Dim Reader As IO.StreamReader
                        Reader = IO.File.OpenText(Kernel.GetProfileDirectory() & "\config.txt")
                        Data = Reader.ReadToEnd
                        Dim MCollection As MatchCollection
                        Dim GroupMatch As Match
                        MCollection = [Regex].Matches(Data, "&([^\n;]+)[;]?")
                        For Each GroupMatch In MCollection
                            Kernel.CommandParser.Invoke(GroupMatch.Groups(1).Value)
                        Next
                        Kernel.ConsoleWrite("Done loading your configuration.")
                    Catch
                        Kernel.ConsoleError("Unable to load your configuration.")
                    End Try
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.StackerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Stacker is now Disabled.")
                Case 1
                    Kernel.StackerTimerObj.Interval = Consts.AutoStackerDelay
                    Kernel.StackerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Auto Stacker is now Enabled.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Map Viewer Command "

    Private Sub CmdMapViewer(ByVal Arguments As GroupCollection)
        Try
            If Not Kernel.BGWMapViewer.IsBusy Then
                Kernel.ConsoleWrite("Map Viewer is opening. Please wait...")
                Kernel.BGWMapViewer.RunWorkerAsync()
            Else
                Kernel.ConsoleError("Map Viewer is already opened.")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto Looter Command "

    Private Sub CmdLoot(ByVal Arguments As GroupCollection)
        Try
            If Kernel.CaveBotTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                Kernel.ConsoleError("Cavebot is currently running.")
                Exit Sub
            End If
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Kernel.LooterMinimumCapacity = 0
                    Kernel.LooterTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Looter is now Disabled.")
                Case 1
                    Kernel.LooterMinimumCapacity = 0
                    Kernel.LooterTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Auto Looter is now Enabled." & Ret & "It will loot until capacity reaches 0.")
                Case Else
                    Select Case Arguments(2).Value.ToLower
                        Case "edit"
                            If Kernel.LooterTimerObj.State = IThreadTimer.ThreadTimerState.Running Then
                                Kernel.ConsoleError("Auto Looter must not be Enabled to edit the Loot Items.")
                                Exit Sub
                            End If
                            Kernel.ConsoleWrite("Please wait...")
                            Kernel.LootItems.ShowLootCategories()
                        Case Else
                            Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]{0,4})")
                            If MatchObj.Success Then
                                Kernel.LooterMinimumCapacity = CUShort(MatchObj.Groups(1).Value)
                                Kernel.ConsoleWrite("Auto Looter is now Enabled." & Ret & "It will loot until capacity reaches " & Kernel.LooterMinimumCapacity & ".")
                                Kernel.LooterTimerObj.StartTimer()
                            Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            End If
                    End Select
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region " Stats Uploader "

    Private Sub CmdStatsUploader(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Kernel.StatsUploaderTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Stats Uploader is now Disabled.")
                Case 1
                    If Consts.StatsUploaderSaveOnDiskOnly Then
                        If Consts.StatsUploaderPath.Length = 0 OrElse Consts.StatsUploaderFilename.Length = 0 Then
                            Kernel.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                            Exit Sub
                        End If
                        Kernel.UploaderUrl = Consts.StatsUploaderUrl
                        Kernel.UploaderFilename = Consts.StatsUploaderFilename
                        Kernel.UploaderPath = Consts.StatsUploaderPath
                        Kernel.UploaderUserId = Consts.StatsUploaderUserID
                        Kernel.UploaderPassword = Consts.StatsUploaderPassword
                        Kernel.UploaderSaveToDiskOnly = Consts.StatsUploaderSaveOnDiskOnly
                        Kernel.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                        Kernel.StatsUploaderTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Stats Uploader is now Enabled.")
                    Else

                        If Consts.StatsUploaderUrl.Length = 0 _
                         OrElse Consts.StatsUploaderUserID.Length = 0 _
                         OrElse Consts.StatsUploaderPassword.Length = 0 _
                         OrElse Consts.StatsUploaderFrequency = 0 Then
                            Kernel.ConsoleError("Please edit your Constants.xml file accordingly to use the Stats Uploader.")
                            Exit Sub
                        End If
                        Kernel.UploaderUrl = Consts.StatsUploaderUrl
                        Kernel.UploaderFilename = Consts.StatsUploaderFilename
                        Kernel.UploaderPath = Consts.StatsUploaderPath
                        Kernel.UploaderUserId = Consts.StatsUploaderUserID
                        Kernel.UploaderPassword = Consts.StatsUploaderPassword
                        Kernel.UploaderSaveToDiskOnly = Consts.StatsUploaderSaveOnDiskOnly
                        Kernel.StatsUploaderTimerObj.Interval = Consts.StatsUploaderFrequency
                        Kernel.StatsUploaderTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Stats Uploader is now Enabled.")
                    End If
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                Dim CP As New ClientPacketBuilder(Kernel.Proxy)
                CP.AnimationEffect(Kernel.CharacterLoc, CType(Num, ITibia.AnimationEffects))
                'Core.Proxy.SendPacketToClient(MagicEffect(Core.CharacterLoc, CType(Num, MagicEffects)))
                Kernel.ConsoleWrite("Animation: " & Num & ".")
            End If
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Test Command "

    Private Sub CmdTest(ByVal Arguments As GroupCollection)
        Try
            Kernel.ConsoleWrite("Begin Test")
            Dim P As New Packet()
            P.AddByte(&H28)
            Kernel.Proxy.SendToClient(P)
            Kernel.ConsoleWrite("End Test")
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
                    Kernel.HealPartyMinimumHPPercentage = 0
                    Kernel.HealPartyTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Heal Party is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]?)%?\s+""?([^""]+)""?")
                    If MatchObj.Success Then
                        Kernel.HealPartyMinimumHPPercentage = CInt(MatchObj.Groups(1).Value)
                        Dim HealthType As String = ""
                        Select Case MatchObj.Groups(2).Value.ToLower
                            Case "ultimate healing", "uh", "adura vita"
                                Kernel.HealPartyHealType = HealTypes.UltimateHealingRune
                                HealthType = "Ultimate Healing."
                            Case "exura sio", "heal friend", "sio"
                                Kernel.HealPartyHealType = HealTypes.ExuraSio
                                HealthType = "Exura Sio."
                            Case "both"
                                Kernel.HealPartyHealType = HealTypes.Both
                                HealthType = "both Exura Sio and Ultimate Healing."
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select
                        Kernel.HealPartyTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto Heal Party is now Enabled." & Ret & _
                         "Healing party members when their hit points are less than " & Kernel.HealPartyMinimumHPPercentage & "% with " & HealthType)
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.HealTimerObj.StopTimer()
                    Kernel.HealMinimumHP = 0
                    Kernel.HealComment = ""
                    Kernel.ConsoleWrite("Auto Healer is now Disabled.")
                Case Else
                    Dim RegExp As New Regex("([1-9][0-9]{0,4}%?)\s+""([^""]+)(?:""?(?:\s*""""([^""]+))?)?")
                    Dim Match As Match = RegExp.Match(Value)
                    If Match.Success Then
                        Dim Match2 As Match = Regex.Match(Match.Groups(1).Value, "([1-9][0-9]?)%")
                        If Match2.Success Then
                            Dim MaxHitPoints As Integer = 0
                            Kernel.Client.ReadMemory(Consts.ptrMaxHitPoints, MaxHitPoints, 2)
                            Kernel.HealMinimumHP = MaxHitPoints * (CInt(Match2.Groups(1).Value) / 100)
                        Else
                            Kernel.HealMinimumHP = CInt(Match.Groups(1).Value)
                        End If
                        For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                            If Spell.Name.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) OrElse Spell.Words.Equals(Match.Groups(2).Value, StringComparison.CurrentCultureIgnoreCase) Then
                                Select Case Spell.Name.ToLower
                                    Case "light healing", "heal friend", "mass healing", "intense healing", "ultimate healing"
                                        Kernel.HealSpell = Spell
                                        Exit For
                                    Case Else
                                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                        Exit Sub
                                End Select
                            End If
                        Next
                        If Match.Groups(3).Value.Length > 0 Then
                            Kernel.HealComment = Match.Groups(3).Value
                        Else
                            Kernel.HealComment = ""
                        End If
                        Kernel.HealTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto Healer is now Enabled.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.FakingTitle = False
                    Dim BL As New BattleList
                    BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                    Kernel.Client.Title = BotName & " - " & BL.GetName
                    Kernel.ConsoleWrite("Client title restored.")
                Case Else
                    Dim Regexp As New Regex("""([^""]+)""?")
                    Dim Match As Match = Regexp.Match(Value)
                    If Match.Success Then
                        Kernel.LastExperience = 0
                        If Kernel.ExpCheckerActivated Then
                            Kernel.ExpCheckerActivated = False
                            Kernel.ConsoleWrite("Experience Checker is now Disabled. Fake Title is now Enabled.")
                        End If
                        Kernel.FakingTitle = True
                        Kernel.Client.Title = Match.Groups(1).Value
                        Kernel.ConsoleWrite("Client title changed to '" & Match.Groups(1).Value & "'.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.LoggingEnabled = False
                    Kernel.ConsoleWrite("Logging is now Disabled.")
                Case 1
                    Kernel.LoggingEnabled = True
                    Kernel.ConsoleWrite("Logging is now Enabled.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    With Kernel
                        .PickUpItemID = 0
                        .PickUpTimerObj.StopTimer()
                        .ConsoleWrite("Auto Pickup is now Disabled.")
                    End With
                Case 1
                    Dim RightHandItemID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandItemID, 2)
                    If RightHandItemID = 0 OrElse Not Kernel.Client.Items.IsThrowable(RightHandItemID) Then
                        Kernel.ConsoleError("You must have a throwable item in your right hand, like a spear, throwing knife, etc.")
                        Exit Sub
                    End If
                    With Kernel
                        .PickUpItemID = CUShort(RightHandItemID)
                        .PickUpTimerObj.Interval = Consts.AutoPickUpDelay
                        .PickUpTimerObj.StartTimer()
                        .ConsoleWrite("Auto Pickup is now Enabled.")
                    End With
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.RunemakerManaPoints = 0
                    Kernel.RunemakerSoulPoints = 0
                    Kernel.RunemakerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Runemaker is now Disabled.")
                Case Else
                    Dim RegExp As New Regex("([1-9][0-9]{1,4})\s+([0-9]{0,3})\s+""([^""]+)""?")
                    Dim Match As Match = RegExp.Match(Value)
                    If Match.Success Then
                        Dim Found As Boolean = False
                        Dim S As New ISpells.SpellDefinition
                        For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                            If (Spell.Name.Equals(Match.Groups(3).Value, StringComparison.CurrentCultureIgnoreCase) _
                            OrElse Spell.Words.Equals(Match.Groups(3).ToString, StringComparison.CurrentCultureIgnoreCase)) _
                            AndAlso Spell.Kind = ISpells.SpellKind.Rune Then
                                S = Spell
                                Found = True
                                Exit For
                            End If
                        Next
                        If Found Then
                            Kernel.RunemakerSpell = S
                            Kernel.RunemakerManaPoints = CInt(Match.Groups(1).Value)
                            Kernel.RunemakerSoulPoints = CInt(Match.Groups(2).Value)
                            Kernel.RunemakerTimerObj.StartTimer()
                            Kernel.ConsoleWrite("Runemaker will now make " & S.Name & " when you have more than " & _
                             Kernel.RunemakerManaPoints & " mana points and more than " & Kernel.RunemakerSoulPoints & " soul points.")
                        Else
                            Kernel.ConsoleError("Invalid Conjure: Spell Name or Spell Words .")
                        End If
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.FisherMinimumCapacity = 0
                    Kernel.FisherSpeed = 0
                    Kernel.FisherTurbo = False
                    Kernel.FisherTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Fisher is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, "^(\d{1,3})(?:\s+(\S+))?$")
                    If MatchObj.Success Then
                        Select Case MatchObj.Groups(2).Value.ToLower
                            Case "normal", "default", ""
                                Kernel.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                                Kernel.FisherSpeed = 0
                                Kernel.FisherTurbo = False
                                Kernel.FisherTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Auto Fisher is now Enabled.")
                            Case "turbo", "nitro", "fast", "faster", "fastest"
                                Kernel.FisherMinimumCapacity = CInt(MatchObj.Groups(1).Value)
                                Kernel.FisherSpeed = 500
                                Kernel.FisherTurbo = True
                                Kernel.FisherTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Auto Fisher (Turbo Mode) is now Enabled.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.TradeWatcherActive = False
                    Kernel.TradeWatcherRegex = ""
                    Kernel.ConsoleWrite("Trade Channel Watcher is now Disabled.")
                Case Else
                    If String.IsNullOrEmpty(Value) Then
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        Exit Sub
                    End If
                    Dim RegExp As Regex
                    Try
                        RegExp = New Regex(Value)
                        Kernel.TradeWatcherRegex = Value
                        Kernel.TradeWatcherActive = True
                        Kernel.ConsoleWrite("Trade Channel Watcher will now match advertisements with the following pattern '" & Kernel.TradeWatcherRegex & "'. Make sure the Trade channel is opened.")
                    Catch ex As Exception
                        Kernel.ConsoleError("Sorry, but this is not a valid regular expression." & Ret & _
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
                    Kernel.ExpCheckerActivated = False
                    Kernel.LastExperience = 0
                    Kernel.Client.Title = BotName & " - " & Kernel.Client.CharacterName
                    Kernel.ConsoleWrite("Experience Checker is now Disabled.")
                Case 1
                    If Kernel.FakingTitle Then
                        Kernel.FakingTitle = False
                        Kernel.ConsoleWrite("Fake Title is now Disabled.")
                    End If
                    Kernel.LastExperience = 0
                    Kernel.ExpCheckerActivated = True
                    Kernel.ConsoleWrite("Experience Checker is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, "creatures?\s+([a-zA-Z]+)", RegexOptions.IgnoreCase)
                    If MatchObj.Success Then
                        Select Case StrToShort(MatchObj.Groups(1).Value.ToLower)
                            Case 0
                                Kernel.ShowCreaturesUntilNextLevel = False
                                Kernel.ConsoleWrite("Showing creatures until next level is now Disabled.")
                            Case 1
                                Kernel.ShowCreaturesUntilNextLevel = True
                                Kernel.ConsoleWrite("Showing creatures until next level is now Enabled.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                Kernel.GuildMembersCommand = MatchObj.Groups(2).ToString
                Kernel.GuildMembersOnlineOnly = False
                If String.Compare(MatchObj.Groups(1).Value, "online", True) = 0 Then
                    Kernel.GuildMembersOnlineOnly = True
                End If
                If Not Kernel.BGWGuildMembersCommand.IsBusy Then
                    Kernel.ConsoleWrite("Please Wait...")
                    Kernel.BGWGuildMembersCommand.RunWorkerAsync()
                Else
                    Kernel.ConsoleError("Busy.")
                End If
            Else
                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                Kernel.CharCommand = MatchObj.Groups(1).ToString
                If Not Kernel.BGWCharCommand.IsBusy Then
                    Kernel.ConsoleWrite("Please Wait...")
                    Kernel.BGWCharCommand.RunWorkerAsync()
                Else
                    Kernel.ConsoleError("Busy.")
                End If
            Else
                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                Kernel.OpenCommand = MatchObj.Groups(1).ToString
                If Not Kernel.BGWOpenCommand.IsBusy Then
                    Kernel.ConsoleWrite("Please Wait...")
                    Kernel.BGWOpenCommand.RunWorkerAsync()
                Else
                    Kernel.ConsoleError("Busy.")
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
                            Prepend = "http://tibia.wikia.com/wiki/"
                        Case "character"
                            Prepend = "http://www.tibia.com/community/?subtopic=character&name="
                        Case "guild"
                            Prepend = "http://www.tibia.com/community/?subtopic=guilds&page=view&GuildName="
                        Case "mytibia"
                            Prepend = "http://www.mytibia.com/"
                        Case Else
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            Exit Sub
                    End Select
                    If Type.ToLower = "wiki" Then
                        Kernel.OpenCommand = Prepend & MatchObj.Groups(2).ToString.Replace(" ", "_")
                    Else
                        Kernel.OpenCommand = Prepend & MatchObj.Groups(2).ToString
                    End If
                    If Not Kernel.BGWOpenCommand.IsBusy Then
                        Kernel.ConsoleWrite("Please Wait...")
                        Kernel.BGWOpenCommand.RunWorkerAsync()
                    Else
                        Kernel.ConsoleError("Busy.")
                    End If

                Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                End If
            End If
        Catch e As Win32Exception
            Kernel.ConsoleWrite("Error opening """ & Value & """ with message """ & e.Message & """.")
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
                    Kernel.AmmoRestackerItemID = 0
                    Kernel.AmmoRestackerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Ammunition Restacker is now Disabled.")
                Case Else
                    Dim ItemID As Integer
                    Dim ItemCount As Integer
                    Dim RegExp As New Regex("([1-9]\d)")
                    Dim Match As Match = RegExp.Match(Value)
                    If Not Match.Success Then
                        Kernel.ConsoleError("You must specify the minimum ammunition count between 1 and 99, inclusive.")
                        Exit Sub
                    End If
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist), ItemID, 2)
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Belt - 1) * Consts.ItemDist) + Consts.ItemCountOffset, ItemCount, 1)
                    If ItemID = 0 OrElse Not Kernel.Client.Dat.GetInfo(ItemID).IsStackable Then
                        Kernel.ConsoleError("You must place some of your ammunition on the Belt/Arrow Slot first.")
                        Exit Sub
                    End If
                    Kernel.AmmoRestackerItemID = ItemID
                    Kernel.AmmoRestackerOutOfAmmo = False
                    Kernel.AmmoRestackerMinimumItemCount = CInt(Match.Groups(1).Value)
                    Kernel.AmmoRestackerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Ammunition Restacker is now Enabled.")
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
                    Kernel.SetLight(ITibia.LightIntensity.Small, ITibia.LightColor.UtevoLux)
                    Kernel.LightTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Light Effect is now Disabled.")
                Case 1
                    Kernel.LightC = ITibia.LightColor.BrightSword
                    Kernel.LightI = ITibia.LightIntensity.Huge + 2
                    Kernel.ConsoleWrite("Full Light Effect is now Enabled.")
                    Kernel.LightTimerObj.StartTimer()
                Case Else
                    Dim strOutput As String = "{0} Light Effect is now Enabled."
                    Select Case Value.ToLower()
                        Case "torch"
                            Kernel.LightI = ITibia.LightIntensity.Medium
                            Kernel.LightC = ITibia.LightColor.Torch
                            Kernel.ConsoleWrite("Torch Light Effect is now Enabled.")
                        Case "great torch"
                            Kernel.LightI = ITibia.LightIntensity.VeryLarge
                            Kernel.LightC = ITibia.LightColor.Torch
                            Kernel.ConsoleWrite("Great Torch Light Effect is now Enabled.")
                        Case "ultimate torch"
                            Kernel.LightI = ITibia.LightIntensity.Huge
                            Kernel.LightC = ITibia.LightColor.Torch
                            Kernel.ConsoleWrite("Ultimate Torch Light Effect is now Enabled.")
                        Case "utevo lux"
                            Kernel.LightI = ITibia.LightIntensity.Medium
                            Kernel.LightC = ITibia.LightColor.UtevoLux
                            Kernel.ConsoleWrite("Utevo Lux Light Effect is now Enabled.")
                        Case "utevo gran lux"
                            Kernel.LightI = ITibia.LightIntensity.Large
                            Kernel.LightC = ITibia.LightColor.UtevoLux
                            Kernel.ConsoleWrite("Utevo Gran Lux Light Effect is now Enabled.")
                        Case "utevo vis lux"
                            Kernel.LightI = ITibia.LightIntensity.VeryLarge
                            Kernel.LightC = ITibia.LightColor.UtevoLux
                            Kernel.ConsoleWrite("Utevo Vis Lux Light Effect is now Enabled.")
                        Case "light wand"
                            Kernel.LightI = ITibia.LightIntensity.Large
                            Kernel.LightC = ITibia.LightColor.LightWand
                            Kernel.ConsoleWrite("Light Wand Light Effect is now Enabled.")
                        Case Else
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            Exit Sub
                    End Select
                    Kernel.LightTimerObj.StartTimer()
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
            Dim SP As New ServerPacketBuilder(Kernel.Proxy)
            Select Case StrToShort(Value)
                Case 0
                    Kernel.AutoAttackerActivated = False
                    Kernel.AutoAttackerIgnoredID = 0
                    Kernel.AutoAttackerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Attacker is now Disabled.")
                Case 1
                    If BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) OrElse BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) Then
                        Kernel.AutoAttackerIgnoredID = BL.GetEntityID
                        EntityName = BL.GetName
                        If Kernel.AutoAttackerIgnoredID < &H40000000 Then Kernel.AutoAttackerIgnoredID = 0
                    Else
                        Kernel.AutoAttackerIgnoredID = 0
                    End If
                    Kernel.AutoAttackerActivated = True
                    Kernel.ConsoleWrite("Auto Attacker is now Enabled.")
                    If Kernel.AutoAttackerIgnoredID > 0 Then
                        Kernel.ConsoleWrite("  Ignoring: " & EntityName & " (" & Kernel.AutoAttackerIgnoredID & ").")
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
                            Kernel.Client.WriteMemory(Consts.ptrSecureMode, 0, 1)
                            SP.ChangeSecureMode(ITibia.SecureMode.Normal)
                            'Core.Proxy.SendPacketToServer(ChangeSecureMode(ITibia.SecureMode.Normal))
                            System.Threading.Thread.Sleep(1000)
                            BL.Attack()
                            Kernel.ConsoleWrite(String.Format("Attacking entity '{0}'.", BL.GetName))
                        Else
                            Kernel.ConsoleWrite(String.Format("Entity '{0}' not found.", Match.Groups(1).Value))
                        End If
                    Else
                        Select Case Value.ToLower
                            Case "stop", "s"
                                Kernel.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
                                Kernel.Client.WriteMemory(Consts.ptrFollowedEntityID, 0, 4)
                                SP.StopEverything()
                                'Core.Proxy.SendPacketToServer(StopEverything())
                                Kernel.ConsoleWrite("Stopped everything.")
                            Case "follow", "chase", "chasing", "c", "f"
                                Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                                SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                                Kernel.ConsoleWrite("Opponents will be chased.")
                            Case "stand", "s", "stay"
                                Kernel.Client.WriteMemory(Consts.ptrChasingMode, 0, 1)
                                SP.ChangeChasingMode(ITibia.ChasingMode.Standing)
                                'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Standing))
                                Kernel.ConsoleWrite("Opponents will not be chased.")
                            Case "offensive", "offense", "o"
                                Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Offensive, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Offensive)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(ITibia.FightingMode.Offensive))
                                Kernel.ConsoleWrite("Fighting in offensive mode.")
                            Case "balanced", "b", "middle"
                                Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Balanced, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Balanced)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(ITibia.FightingMode.Balanced))
                                Kernel.ConsoleWrite("Fighting in balanced mode.")
                            Case "defensive", "defense", "d"
                                Kernel.Client.WriteMemory(Consts.ptrFightingMode, ITibia.FightingMode.Defensive, 1)
                                SP.ChangeFightingMode(ITibia.FightingMode.Defensive)
                                'Core.Proxy.SendPacketToServer(ChangeFightingMode(FightingMode.Defensive))
                                Kernel.ConsoleWrite("Fighting in defensive mode.")
                            Case "auto", "automatic"
                                Kernel.AutoAttackerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Attacking creatures automatically.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.SpellManaRequired = 0
                    Kernel.SpellMsg = ""
                    Kernel.SpellTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Spell Caster is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, "^([1-9][0-9]{1,4})\s+""?(.+)$")
                    If MatchObj.Success Then
                        Kernel.SpellManaRequired = CUInt(MatchObj.Groups(1).ToString)
                        Kernel.SpellMsg = MatchObj.Groups(2).ToString
                        Kernel.SpellTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Spell Caster is now Enabled." & Ret & _
                         "Casting '" & Kernel.SpellMsg & "' with " & Kernel.SpellManaRequired & " or more mana points.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " About Command "

    Private Sub CmdAbout(ByVal Arguments As GroupCollection)
        Try
            Kernel.ConsoleWrite(BotName & " v" & BotVersion & "." & Ret & _
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
                    Kernel.AutoEaterSmart = 0
                    Kernel.EaterTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Eater is now Disabled.")
                Case 1
                    Kernel.AutoEaterSmart = 0
                    Kernel.EaterTimerObj.Interval = Consts.AutoEaterInterval
                    Kernel.EaterTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Auto Eater is now Enabled for every 30 seconds.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "smart\s+([1-9]\d{1,4})")
                    If MatchObj.Success Then
                        Kernel.AutoEaterSmart = CInt(MatchObj.Groups(1).ToString)
                        Kernel.EaterTimerObj.Interval = Consts.AutoEaterSmartInterval
                        Kernel.EaterTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto Eater will eat only when you are below " & Kernel.AutoEaterSmart & " hit points, once every minute.")
                    Else
                        MatchObj = Regex.Match(Arguments(2).ToString, "([1-9]\d{0,2})")
                        If MatchObj.Success Then
                            Kernel.AutoEaterSmart = 0
                            Kernel.EaterTimerObj.Interval = CInt(MatchObj.Groups(1).Value) * 1000
                            Kernel.EaterTimerObj.StartTimer()
                            Kernel.ConsoleWrite("Auto Eater is now Enabled for every " & ((Kernel.EaterTimerObj.Interval / 1000) Mod 1000) & " second(s).")
                        Else
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.UHTimerObj.StopTimer()
                    Kernel.UHHPRequired = 0
                    Kernel.ConsoleWrite("Auto UHer is now Disabled.")
                Case Else
                    If IsNumeric(Value) AndAlso CInt(Value) > 0 Then
                        Kernel.UHHPRequired = CUInt(Value)
                        Kernel.UHId = Kernel.Client.Items.GetItemID("Ultimate Healing")
                        Kernel.UHTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto UHer will now 'UH' you if you are below " & Ret & _
                        Kernel.UHHPRequired & " hit points.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.HealFriendCharacterName = ""
                    Kernel.HealFriendHealthPercentage = 0
                    Kernel.HealFriendTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Auto Heal Friend is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "([1-9][0-9]?)%?\s+""([^""]+)""\s+""([^""]+)""?")
                    If MatchObj.Success Then
                        Kernel.HealFriendHealthPercentage = CUShort(MatchObj.Groups(1).ToString)
                        If Kernel.HealFriendHealthPercentage < 0 Or Kernel.HealFriendHealthPercentage > 99 Then
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                            Exit Sub
                        End If
                        Dim HealthType As String = ""
                        Select Case MatchObj.Groups(2).ToString.ToLower
                            Case "ultimate healing", "uh", "adura vita"
                                Kernel.HealFriendHealType = HealTypes.UltimateHealingRune
                                HealthType = "Ultimate Healing."
                            Case "exura sio", "heal friend", "sio"
                                Kernel.HealFriendHealType = HealTypes.ExuraSio
                                HealthType = "Exura Sio."
                            Case "both"
                                Kernel.HealFriendHealType = HealTypes.Both
                                HealthType = "both Exura Sio and Ultimate Healing."
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select
                        Kernel.HealFriendCharacterName = MatchObj.Groups(3).Value
                        Kernel.HealFriendTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto Heal Friend is now Enabled." & Ret & _
                         "Healing '" & Kernel.HealFriendCharacterName & "' when his/her hit points are less than " & Kernel.HealFriendHealthPercentage & "% with " & HealthType)
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Version Command "

    Private Sub CmdVersion(ByVal Arguments As GroupCollection)
        Try
            Kernel.ConsoleWrite(BotName & " v" & BotVersion & ".")
            Kernel.ConsoleWrite("Powered By: PProxy v2.0 by CPargermer.")
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
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    Exit Sub
            End Select
            BL.Reset(True)
            Do
                If BL.IsMyself OrElse BL.GetFloor <> Kernel.CharacterLoc.Z + Floor Then Continue Do
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
            Kernel.ConsoleWrite("Entities found: " & Output & ".")
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
                    Kernel.AdvertiseMsg = ""
                    Kernel.AdvertiseTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Trade Channel Advertiser is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, """([^""]+)""?")
                    If MatchObj.Success Then
                        'OpenChannel("Trade", ChannelType.Trade)
                        Kernel.AdvertiseMsg = MatchObj.Groups(1).ToString
                        Kernel.ConsoleWrite("Trade Channel Advertiser is now Enabled. Make sure the Trade Channel is opened.")
                        Kernel.AdvertiseTimerObj.StartTimer(1000)
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Get Item ID Command "

    Private Sub CmdGetItemId(ByVal Arguments As GroupCollection)
        Try
            Dim Container As New Container()
            Dim I As Integer
            Dim Item As Scripting.IContainer.ContainerItemDefinition
            Dim ItemCount As Integer
            Dim Output As String = ""
            Dim ItemName As String
            Dim ID As Integer
            Kernel.ConsoleWrite("Getting Item IDs, Please Wait...")
            Kernel.ConsoleWrite("Inventory: ")
            For E As ITibia.InventorySlots = ITibia.InventorySlots.Head To ITibia.InventorySlots.Belt
                Output = E.ToString & ": "
                Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (E - 1)), ID, 2)
                ItemName = Kernel.Client.Items.GetItemName(ID)
                If String.Compare(ItemName, "Unknown") = 0 Then
                    Output &= "Unknown H" & Hex(ID)
                Else
                    Output &= ItemName
                End If
                If Kernel.Client.Dat.GetInfo(ID).IsStackable Then
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + (Consts.ItemDist * (E - 1)) + Consts.ItemCountOffset, ItemCount, 1)
                    Output &= " (x" & ItemCount & ")"
                End If
                Output &= "."
                Kernel.ConsoleWrite(Output)
            Next

            Container.Reset()
            Do
                If Container.IsOpened Then
                    Output = ""
                    Kernel.ConsoleWrite("Container #" & Hex(Container.GetContainerIndex + 1) & ": " & Container.GetName() & "")
                    ItemCount = Container.GetItemCount()
                    For I = 0 To ItemCount - 1
                        Item = Container.Items(I)
                        ItemName = Kernel.Client.Items.GetItemName(Item.ID)
                        If String.Compare(ItemName, "Unknown") = 0 Then
                            Output &= "Unknown H" & Hex(Item.ID)
                        Else
                            Output &= ItemName & " H" & Hex(Item.ID)
                        End If
                        If Kernel.Client.Dat.GetInfo(Item.ID).IsStackable Then
                            Output &= " (x" & Item.Count & ")"
                        End If
                        If I < ItemCount - 1 Then
                            Output &= ", "
                        Else
                            Output &= "."
                        End If
                    Next
                    Kernel.ConsoleWrite(Output)
                End If
            Loop While Container.NextContainer()
            Kernel.ConsoleWrite("Done.")
            Exit Sub
        Catch Ex As Exception
            MessageBox.Show("TargetSite: " & Ex.TargetSite.Name & vbCrLf & "Message: " & Ex.Message & vbCrLf & "Source: " & Ex.Source & vbCrLf & "Stack Trace: " & Ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " List Commands Command "

    Private Sub CmdCommands(ByVal Arguments As GroupCollection)
        Try
            Kernel.ConsoleWrite("Listing all commands. Type &help <command> for help. Example: &help attack." & Ret & _
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
            Kernel.ConsoleWrite(" " & Ret & _
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
            Kernel.ConsoleWrite(" " & Ret & _
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
            Kernel.ConsoleWrite(" " & Ret & _
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
                    Kernel.ConsoleWrite("Rainbow Outfit is now Disabled")
                    Kernel.RainbowOutfitTimerObj.StopTimer()
                    Kernel.RainbowOutfitBody = 0
                    Kernel.RainbowOutfitFeet = 0
                    Kernel.RainbowOutfitHead = 0
                    Kernel.RainbowOutfitLegs = 0
                Case 1
                    Kernel.ConsoleWrite("Rainbow Outfit is now Enabled")
                    Kernel.RainbowOutfitBody = 0
                    Kernel.RainbowOutfitFeet = 10
                    Kernel.RainbowOutfitHead = 20
                    Kernel.RainbowOutfitLegs = 30
                    Kernel.RainbowOutfitTimerObj.Interval = 50
                    Kernel.RainbowOutfitTimerObj.StartTimer()
                Case Else
                    Select Case Value.ToLower
                        Case "fast"
                            Kernel.RainbowOutfitBody = 0
                            Kernel.RainbowOutfitFeet = 10
                            Kernel.RainbowOutfitHead = 20
                            Kernel.RainbowOutfitLegs = 30
                            Kernel.RainbowOutfitTimerObj.Interval = 50
                            Kernel.ConsoleWrite("Rainbow Outfit is now Enabled with fast speed.")
                            Kernel.RainbowOutfitTimerObj.StartTimer()
                        Case "slow"
                            Kernel.RainbowOutfitBody = 0
                            Kernel.RainbowOutfitFeet = 10
                            Kernel.RainbowOutfitHead = 20
                            Kernel.RainbowOutfitLegs = 30
                            Kernel.RainbowOutfitTimerObj.Interval = 100
                            Kernel.ConsoleWrite("Rainbow Outfit is now Enabled with low speed.")
                            Kernel.RainbowOutfitTimerObj.StartTimer()
                        Case Else
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.AutoDrinkerTimerObj.StopTimer()
                    Kernel.DrinkerManaRequired = 0
                    Kernel.ConsoleWrite("Auto Drinker is now Disabled.")
                Case Else
                    Dim RegExp As New Regex("^[1-9]\d{1,3}$")
                    Dim Match As Match = RegExp.Match(Value)
                    If Match.Success Then
                        Kernel.DrinkerManaRequired = Value
                        Kernel.AutoDrinkerTimerObj.Interval = Consts.HealersCheckInterval
                        Kernel.AutoDrinkerTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Auto Drinker is now Enabled.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
            Dim SP As New ServerPacketBuilder(Kernel.Proxy)
            Select Case StrToShort(value)
                Case 0
                    Kernel.LooterTimerObj.StopTimer()
                    Kernel.AutoAttackerTimerObj.StopTimer()
                    Kernel.CaveBotTimerObj.StopTimer()
                    Kernel.EaterTimerObj.StopTimer()
                    Kernel.EaterTimerObj.Interval = 0
                    Kernel.WaypointIndex = 0
                    Kernel.IsOpeningReady = True
                    SP.StopEverything()
                    'Core.Proxy.SendPacketToServer(PacketUtils.AttackEntity(0))
                    Kernel.Client.WriteMemory(Consts.ptrAttackedEntityID, 0, 4)
                    Kernel.ConsoleWrite("Cavebot is now Disabled.")
                Case 1
                    If Kernel.Walker_Waypoints.Count = 0 Then
                        Kernel.ConsoleWrite("No waypoints found.")
                        Exit Sub
                    End If
                    If Consts.LootWithCavebot Then
                        Kernel.LooterMinimumCapacity = Consts.CavebotLootMinCap
                        Kernel.LooterTimerObj.StartTimer()
                    End If
                    Kernel.AutoAttackerTimerObj.StartTimer()
                    Kernel.CaveBotTimerObj.StartTimer()
                    Kernel.AutoEaterSmart = 0
                    Kernel.EaterTimerObj.Interval = 20000
                    Kernel.EaterTimerObj.StartTimer()
                    Kernel.IsOpeningReady = True
                    Kernel.CBCreatureDied = False
                    Kernel.WaypointIndex = 0
                    Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                    SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                    'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                    Kernel.ConsoleWrite("Cavebot is now Enabled.")
                    Kernel.CBState = CavebotState.Walking
                Case Else 'ADD or Continue
                    If Arguments(2).ToString.ToLower = "continue" Then
                        If Kernel.Walker_Waypoints.Count = 0 Then
                            Kernel.ConsoleWrite("No waypoints found.")
                            Exit Sub
                        End If
                        Kernel.WaypointIndex = SelectNearestWaypoint(Kernel.Walker_Waypoints)
                        If Kernel.WaypointIndex = -1 Then
                            Kernel.ConsoleError("No waypoints found on this floor.")
                            Exit Sub
                        End If
                        If Consts.LootWithCavebot Then
                            Kernel.LooterMinimumCapacity = Consts.CavebotLootMinCap
                            Kernel.LooterTimerObj.StartTimer()
                        End If
                        Kernel.AutoAttackerTimerObj.StartTimer()
                        Kernel.CaveBotTimerObj.StartTimer()
                        Kernel.AutoEaterSmart = 0
                        Kernel.EaterTimerObj.Interval = 20000
                        Kernel.EaterTimerObj.StartTimer()
                        Kernel.IsOpeningReady = True
                        Kernel.CBCreatureDied = False
                        Kernel.Client.WriteMemory(Consts.ptrChasingMode, 1, 1)
                        SP.ChangeChasingMode(ITibia.ChasingMode.Chasing)
                        'Core.Proxy.SendPacketToServer(ChangeChasingMode(ChasingMode.Chasing))
                        Kernel.ConsoleWrite("Cavebot is now Enabled.")
                        Kernel.CBState = CavebotState.Walking
                        Exit Sub
                    End If
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "add\s+(walk|ladder|rope|sewer|w|l|r|se)", RegexOptions.IgnoreCase)
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
                                Kernel.ConsoleWrite("Walking waypoint added.")
                            Case "ladder", "l"
                                Character.Type = Walker.WaypointType.Ladder
                                WPType = "L"
                                Kernel.ConsoleWrite("Ladder waypoint added.")
                            Case "rope", "r"
                                Character.Type = Walker.WaypointType.Rope
                                WPType = "R"
                                Kernel.ConsoleWrite("Rope waypoint added.")
                            Case "sewer", "se"
                                Character.Type = Walker.WaypointType.Sewer
                                WPType = "SE"
                                Kernel.ConsoleWrite("Sewer waypoint added.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                Exit Sub
                        End Select

                        Kernel.Walker_Waypoints.Add(Character)
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
                                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                    Exit Sub
                            End Select

                            Kernel.Walker_Waypoints.Add(Character)
                            Kernel.ConsoleWrite(MatchObj.Groups(1).ToString & " waypoint added to direction " & MatchObj.Groups(2).ToString & ".")
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
                                Kernel.Walker_Waypoints.Add(Character)
                                Kernel.ConsoleWrite("Wait waypoint added.")
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

                                    Kernel.Walker_Waypoints.Add(Character)
                                    Kernel.ConsoleWrite("Say waypoint added.")
                                Else
                                    MatchObj = Regex.Match(Arguments(2).ToString, "add\sshovel\s+(up|down|left|right|north|south|east|west)")
                                    If MatchObj.Success Then
                                        If Walker.CheckDistance = False Then Exit Sub
                                        Dim BL As New BattleList
                                        Dim Character As New Walker
                                        Dim WPType As String
                                        BL.JumpToEntity(IBattlelist.SpecialEntity.Myself)
                                        Character.Coordinates = BL.GetLocation

                                        Character.Type = Walker.WaypointType.Shovel
                                        WPType = "SH"
                                        Select Case MatchObj.Groups(1).ToString
                                            Case "up", "north"
                                                Character.Info = Walker.Directions.Up
                                            Case "left", "west"
                                                Character.Info = Walker.Directions.Left
                                            Case "down", "south"
                                                Character.Info = Walker.Directions.Down
                                            Case "right", "east"
                                                Character.Info = Walker.Directions.Up
                                            Case Else
                                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                                Exit Sub
                                        End Select
                                        Kernel.Walker_Waypoints.Add(Character)
                                        Kernel.ConsoleWrite("Shovel waypoint added to direction " & MatchObj.Groups(1).ToString)
                                    Else
                                        MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(learn|auto|automatic|automatically|learning|l)\s(on|off)")
                                        If MatchObj.Success Then
                                            UpdatePlayerPos()
                                            Select Case StrToShort(MatchObj.Groups(2).ToString)
                                                Case 1
                                                    Kernel.LearningMode = True
                                                    'AutoAddTime = Now.AddSeconds(10)
                                                    Kernel.LastFloor = Kernel.CharacterLoc.Z
                                                    Kernel.AutoAddTimerObj.StartTimer()
                                                    Kernel.ConsoleWrite("Adding waypoints automatically.")
                                                Case 0
                                                    Kernel.LearningMode = False
                                                    Kernel.AutoAddTimerObj.StopTimer()
                                                    Kernel.ConsoleWrite("Stopped adding waypoints automatically.")
                                            End Select
                                        Else
                                            MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(load|append)\s+""?(.+)""$")
                                            If MatchObj.Success Then
                                                Dim Path As String = MatchObj.Groups(2).ToString
                                                If Path.StartsWith("\") Then ' "\...\waypoint.xml"
                                                    Path = Application.StartupPath & Path
                                                ElseIf Not Path.Contains("\") Then ' "waypoint.xml"
                                                    Path = MiscUtils.GetWaypointsDirectory & "\" & Path
                                                End If ' else "C:\..\waypoint.xml"

                                                If IO.File.Exists(Path) Then
                                                    WalkerModule.Load(Path)
                                                    Kernel.ConsoleWrite("Loading waypoints compeleted.")
                                                Else
                                                    Kernel.ConsoleError("Unable to find waypoint file.")
                                                    Exit Sub
                                                End If
                                            Else
                                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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

#Region " Walker Command "
    Private Sub CmdWalker(ByVal Arguments As GroupCollection)
        Try
            Dim value As String = Arguments(2).ToString.ToLower
            Select Case StrToShort(value)
                Case 0
                    Kernel.WalkerTimerObj.StopTimer()
                    Kernel.WalkerLoop = False
                    Kernel.WaypointIndex = 0
                    Kernel.ConsoleWrite("Walker is now Disabled.")
                Case 1
                    If Kernel.Walker_Waypoints.Count = 0 Then
                        Kernel.ConsoleError("No Waypoints Found")
                        Exit Sub
                    End If
                    Kernel.WaypointIndex = 0
                    Kernel.WalkerTimerObj.StartTimer()
                    Kernel.WalkerLoop = False
                    Kernel.ConsoleWrite("Walker is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).ToString, "continue", RegexOptions.IgnoreCase)
                    If MatchObj.Success Then
                        Kernel.WalkerTimerObj.StartTimer()
                        Kernel.WalkerLoop = True
                        Kernel.ConsoleWrite("Walker On With Continue Mode")
                    Else
                        MatchObj = (Regex.Match(Arguments(2).ToString, "add\s+(walk|ladder|rope|sewer|w|l|r|se)", RegexOptions.IgnoreCase))
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
                                    Kernel.ConsoleWrite("Walking waypoint added.")
                                Case "ladder", "l"
                                    Character.Type = Walker.WaypointType.Ladder
                                    WPType = "L"
                                    Kernel.ConsoleWrite("Ladder waypoint added.")
                                Case "rope", "r"
                                    Character.Type = Walker.WaypointType.Rope
                                    WPType = "R"
                                    Kernel.ConsoleWrite("Rope waypoint added.")
                                Case "sewer", "s"
                                    Character.Type = Walker.WaypointType.Sewer
                                    WPType = "SE"
                                    Kernel.ConsoleWrite("Sewer waypoint added.")
                                Case Else
                                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                    Exit Sub
                            End Select

                            Kernel.Walker_Waypoints.Add(Character)
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
                                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                        Exit Sub
                                End Select

                                Kernel.Walker_Waypoints.Add(Character)
                                Kernel.ConsoleWrite(MatchObj.Groups(1).ToString & " waypoint added to direction " & MatchObj.Groups(2).ToString & ".")
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
                                    Kernel.Walker_Waypoints.Add(Character)
                                    Kernel.CavebotForm.Waypointslst.Items.Add(WPType & ": Wait: " & Character.Info)
                                    Kernel.ConsoleWrite("Wait waypoint added.")
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

                                        Kernel.Walker_Waypoints.Add(Character)
                                        Kernel.ConsoleWrite("Say waypoint added.")
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
                                            Select Case MatchObj.Groups(1).ToString
                                                Case "up", "north"
                                                    Character.Info = Walker.Directions.Up
                                                Case "left", "west"
                                                    Character.Info = Walker.Directions.Left
                                                Case "down", "south"
                                                    Character.Info = Walker.Directions.Down
                                                Case "right", "east"
                                                    Character.Info = Walker.Directions.Up
                                                Case Else
                                                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                                    Exit Sub
                                            End Select
                                            Kernel.Walker_Waypoints.Add(Character)
                                        Else
                                            MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(learn|auto|automatic|automatically|learning|l)\s(on|off)")
                                            If MatchObj.Success Then
                                                UpdatePlayerPos()
                                                Select Case StrToShort(MatchObj.Groups(2).ToString)
                                                    Case 1
                                                        Kernel.LastFloor = Kernel.CharacterLoc.Z
                                                        Kernel.LearningMode = True
                                                        'AutoAddTime = Now.AddSeconds(10)
                                                        Kernel.AutoAddTimerObj.StartTimer()
                                                        Kernel.ConsoleWrite("Adding waypoints automatically.")
                                                    Case 0
                                                        Kernel.LearningMode = False
                                                        Kernel.AutoAddTimerObj.StopTimer()
                                                        Kernel.ConsoleWrite("Stopped adding waypoints automatically.")
                                                End Select
                                            Else
                                                MatchObj = Regex.Match(Arguments(2).ToString.ToLower, "(load|append)\s+""?(.+)""$")
                                                If MatchObj.Success Then
                                                    Dim Path As String = MatchObj.Groups(2).ToString
                                                    If Path.StartsWith("\") Then ' "\...\waypoint.xml"
                                                        Path = Application.StartupPath & Path
                                                    ElseIf Not Path.Contains("\") Then ' "waypoint.xml"
                                                        Path = MiscUtils.GetWaypointsDirectory & "\" & Path
                                                    End If ' else "C:\..\waypoint.xml"

                                                    If IO.File.Exists(Path) Then
                                                        WalkerModule.Load(Path)
                                                        Kernel.ConsoleWrite("Loading waypoints compeleted.")
                                                    Else
                                                        Kernel.ConsoleError("Unable to find waypoint file.")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                                                End If
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
                    Kernel.ComboBotEnabled = False
                    Kernel.ConsoleWrite("Combobot is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, """([^""]+)")
                    If MatchObj.Success Then
                        Kernel.ComboBotLeader = MatchObj.Groups(1).ToString
                        Kernel.ComboBotEnabled = True
                        Kernel.ConsoleWrite("Combobot is now Enabled with Leader: " & Kernel.ComboBotLeader)
                        Exit Sub
                    Else
                        If Value = "leader" Then
                            Dim BL As New BattleList
                            BL.Reset()
                            If BL.JumpToEntity(IBattlelist.SpecialEntity.Attacked) OrElse BL.JumpToEntity(IBattlelist.SpecialEntity.Followed) Then
                                If BL.IsPlayer Then
                                    Kernel.ComboBotLeader = BL.GetName
                                    Kernel.ComboBotEnabled = True
                                    Kernel.ConsoleWrite("Combobot is now Enabled with Leader: " & Kernel.ComboBotLeader)
                                    Exit Sub
                                Else
                                    Kernel.ConsoleError("You can only set players as leader.")
                                    Exit Sub
                                End If
                            Else
                                Kernel.ConsoleError("You need to Attack/Follow player to set him/her as leader.")
                                Exit Sub
                            End If
                            Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.AmuletChangerTimerObj.StopTimer()
                    Kernel.AmuletID = 0
                    Kernel.ConsoleWrite("Amulet/Necklace Changer is now Disabled.")
                Case 1
                    Dim ItemID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Neck - 1) * Consts.ItemDist), ItemID, 2)
                    If ItemID = 0 Then
                        Kernel.ConsoleError("You are not wearing any amulet. Please equip the amulet that you want to restack.")
                        Exit Sub
                    End If
                    Kernel.AmuletID = ItemID
                    Kernel.AmuletChangerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Amulet/Necklace Changer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, """([^""]+)")
                    If MatchObj.Success Then
                        Kernel.AmuletID = Kernel.Client.Items.GetItemID(MatchObj.Groups(1).ToString)
                        If Kernel.AmuletID = 0 AndAlso Kernel.Client.Items.IsNeck(Kernel.AmuletID) Then
                            Kernel.ConsoleError("Invalid Amulet/Necklace Name.")
                            Exit Sub
                        End If
                        Kernel.AmuletChangerTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Amulet/Necklace Changer is now Enabled.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.RingChangerTimerObj.StopTimer()
                    Kernel.RingID = 0
                    Kernel.ConsoleWrite("Ring Changer is now Disabled.")
                Case 1
                    Dim ItemID As Integer
                    Kernel.Client.ReadMemory(Consts.ptrInventoryBegin + ((ITibia.InventorySlots.Finger - 1) * Consts.ItemDist), ItemID, 2)
                    If ItemID = 0 Then
                        Kernel.ConsoleError("You are not wearing any ring. Please equip the ring that you want to change.")
                        Exit Sub
                    End If
                    Kernel.RingID = ItemID
                    Kernel.RingChangerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Ring Changer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, """([^""]+)")
                    If MatchObj.Success Then
                        Kernel.RingID = Kernel.Client.Items.GetItemID(MatchObj.Groups(1).ToString)
                        If Kernel.RingID = 0 AndAlso Kernel.Client.Items.IsRing(Kernel.RingID) Then
                            Kernel.ConsoleError("Invalid Ring Name.")
                            Exit Sub
                        End If
                        Kernel.RingChangerTimerObj.StartTimer()
                        Kernel.ConsoleWrite("Ring Changer is now Enabled.")
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.AntiLogoutObj.StopTimer()
                    Kernel.ConsoleWrite("Anti-Logout is now Disabled.")
                Case 1
                    Kernel.LastActivity = Date.Now
                    Kernel.AntiLogoutObj.Interval = Consts.AntiLogoutInterval
                    Kernel.AntiLogoutObj.StartTimer()
                    Kernel.ConsoleWrite("Anti-Logout is now Enabled.")
                Case Else
                    Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.DancerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Dancer is now Disabled.")
                Case 1
                    Kernel.DancerTimerObj.Interval = 1000
                    Kernel.DancerTimerObj.StartTimer()
                    Kernel.ConsoleWrite("Dancer is now Enabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, "(\w+)")
                    If MatchObj.Success Then
                        Select Case MatchObj.Groups(1).ToString.ToLower
                            Case "random"
                                Kernel.DancerTimerObj.Interval = (New Random()).Next(300, 1500)
                                Kernel.DancerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Dancer is now Enabled with Random Speed")
                            Case "slower"
                                Kernel.DancerTimerObj.Interval = 1000
                                Kernel.DancerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Dancer is now Enabled with Slower Speed")
                            Case "slow"
                                Kernel.DancerTimerObj.Interval = 500
                                Kernel.DancerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Dancer is now Enabled with Slow Speed")
                            Case "fast"
                                Kernel.DancerTimerObj.Interval = 300
                                Kernel.DancerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Dancer is now Enabled with Fast Speed.")
                            Case "turbo"
                                Kernel.DancerTimerObj.Interval = 200
                                Kernel.DancerTimerObj.StartTimer()
                                Kernel.ConsoleWrite("Dancer is now Enabled with Turbo Mode")
                            Case Else
                                Kernel.ConsoleWrite("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
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
                    Kernel.AmmoMakerMinMana = 0
                    Kernel.AmmoMakerMinCap = 0
                    Kernel.AmmoMakerTimerObj.StopTimer()
                    Kernel.ConsoleWrite("Ammunition Maker is now Disabled.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Value, "([1-9][0-9]{1,4})\s+([0-9]{0,3})\s+""([^""]+)""?")
                    If MatchObj.Success Then
                        Dim Found As Boolean = False
                        Dim S As New ISpells.SpellDefinition
                        For Each Spell As ISpells.SpellDefinition In Kernel.Spells.SpellsList
                            If (Spell.Name.Equals(MatchObj.Groups(3).Value, StringComparison.CurrentCultureIgnoreCase) _
                            OrElse Spell.Words.Equals(MatchObj.Groups(3).ToString, StringComparison.CurrentCultureIgnoreCase)) _
                            AndAlso (Spell.Kind = ISpells.SpellKind.Ammunition Or Spell.Kind = ISpells.SpellKind.Incantation) Then
                                S = Spell
                                Found = True
                                Exit For
                            End If
                        Next
                        If Found Then
                            Kernel.AmmoMakerSpell = S
                            Kernel.AmmoMakerMinMana = CInt(MatchObj.Groups(1).Value)
                            Kernel.AmmoMakerMinCap = CInt(MatchObj.Groups(2).Value)
                            Kernel.AmmoMakerTimerObj.StartTimer()
                            Kernel.ConsoleWrite("Ammo Maker is now Enabled.")
                        Else
                            Kernel.ConsoleError("Invalid Conjure: Spell Name or Spell Words .")
                        End If
                    Else
                        Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Bot State "
    Private Sub CmdBotState(ByVal Arguments As GroupCollection)
        Try
            Select Case StrToShort(Arguments(2).ToString)
                Case 0
                    Kernel.TTBState = BotState.Paused
                    Kernel.ConsoleWrite("Tibia Tek Bot is now paused.")
                Case 1
                    Kernel.TTBState = BotState.Running
                    Kernel.ConsoleWrite("Tibia Tek Bot is now running again.")
                Case Else
                    Dim MatchObj As Match = Regex.Match(Arguments(2).Value, "(\w+)")
                    If MatchObj.Success Then
                        Select Case MatchObj.Groups(1).ToString.ToLower
                            Case "pause", "stop", "paused", "stopped", "disable", "disabled"
                                Kernel.TTBState = BotState.Paused
                                Kernel.ConsoleWrite("Tibia Tek Bot is now paused.")
                            Case "unpause", "running", "activate", "activated", "run"
                                Kernel.TTBState = BotState.Running
                                Kernel.ConsoleWrite("Tibia Tek Bot is now running again.")
                            Case Else
                                Kernel.ConsoleError("Invalid format for this command." & Ret & "For help on the usage, type: &help " & Arguments(1).Value & ".")
                        End Select
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show("TargetSite: " & ex.TargetSite.Name & vbCrLf & "Message: " & ex.Message & vbCrLf & "Source: " & ex.Source & vbCrLf & "Stack Trace: " & ex.StackTrace & vbCrLf & vbCrLf & "Please report this error to the developers, be sure to take a screenshot of this message box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class
