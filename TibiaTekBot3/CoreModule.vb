Imports System.Runtime.InteropServices, System.Windows.forms, AutoItX3Lib, _
        System.Text.RegularExpressions, System.Math, System.Xml

Public Module CoreModule

    Public Core As New CoreClass

#Region " Structures "

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

    Public Class CoreClass

        Public Structure ChatMessageDefinition
            Dim Prioritize As Boolean
            Dim MessageType As MessageType
            Dim Channel As ChannelType
            Dim Destinatary As String
            Dim Message As String
        End Structure

#Region " Forms "
        Public SelectClientForm As New frmSelectClient
        Public TextMenuForm As New frmMenu
#End Region

#Region " Objects "
        Public Tibia As New TibiaClass
        Public Versions As New VersionsClass
        Public Consts As New ConstantsClass
        Public Spells As New SpellsClass
        Public Definitions As New ItemsClass
        Public WithEvents CharacterTimerObj As ThreadTimer
        Public WithEvents TibiaWindowTimerObj As ThreadTimer
        Public WithEvents LightEffectTimerObj As ThreadTimer
        Public WithEvents ChatMessageQueueTimerObj As ThreadTimer
        Public WithEvents HealTimerObj As ThreadTimer
        Public ChatMessageQueueList As New List(Of ChatMessageDefinition)
        Public AutoIt As AutoItX3
        Public WithEvents ChildWindowTimerObj As ThreadTimer
        Public WithEvents ExpCheckerTimerObj As ThreadTimer
        Public WithEvents SpellTimerObj As ThreadTimer
        Public WithEvents EaterTimerObj As ThreadTimer
        Public WithEvents MapReaderTimerObj As ThreadTimer
        Public WithEvents RunemakerTimerObj As ThreadTimer
        Public Map As MapReader
        Public WithEvents FisherTimerObj As ThreadTimer
        Public WithEvents AdvertiseTimerObj As ThreadTimer
        Public WithEvents FPSChangerTimerObj As ThreadTimer
        Public WithEvents StatsUploaderTimerObj As ThreadTimer
        Public WithEvents PingTimerObj As ThreadTimer
        Public PacketsFromServerQueue As New Queue(Of Byte())
        Public WithEvents PacketsFromServerTimerObj As ThreadTimer

#End Region

#Region " Variables "
        Public InjectionState As InjectionState = InjectionState.Uninjected
        Public State As BotState = BotState.Running
        Public ConstantsLoaded As Boolean = False

        Public TTBHandle As Integer = 0

        Public TibiaWindowState As WindowState = WindowState.Active

        Public CharacterID As Integer = 0
        Public CharacterName As String = ""
        Public CharacterWorld As String = ""
        Public CharacterLevel As Integer = 0
        Public CharacterExperience As Integer = 0
        Public CharacterHitPoints As Integer = 0
        Public CharacterMaxHitPoints As Integer = 0
        Public CharacterManaPoints As Integer = 0
        Public CharacterSoulPoints As Integer = 0
        Public CharacterLoc As New LocationDefinition(0, 0, 0)
        Public CharacterLastLocation As New LocationDefinition(0, 0, 0)
        Public CharacterCapacity As Integer = 0
        Public CharacterConditions As Conditions = Conditions.None

        Public LightI As LightIntensity = LightIntensity.None
        Public LightC As LightColor = LightColor.Darkness

        Public ChatMessageLastSent As Date = Date.MinValue

        Public ExpCheckerActivated As Boolean = False
        Public LastExperience As Integer = 0

        Public FakingTitle As Boolean = False
        Public ShowCreaturesUntilNextLevel As Boolean = False

        Public HealSpell As SpellDefinition
        Public HealMinimumHP As Integer = 0
        Public HealComment As String = ""
        Public SpellManaRequired As Integer = 0
        Public SpellMsg As String = ""

        Public AutoEaterSmart As Integer = 0

        Public RunemakerConjure As ConjureDefinition = Nothing
        Public RunemakerManaPoints As String = ""
        Public RunemakerSoulPoints As Integer = 0

        Public FisherSpeed As UShort = 0
        Public FisherMinimumCapacity As Integer = 0

        Public AdvertiseMsg As String = ""

        Public TradeWatcherActive As Boolean = False
        Public TradeWatcherRegex As String = ""

        Public WASDActive As Boolean = False
        Public WASDSayModeActive As Boolean = False

        Public HotkeyWindowWasOpened As Boolean = False

        Public FrameRateBegin As Integer = 0

        Public NextLevelExp As Integer = 0
        Public CurrentLevelExp As Integer = 0
        Public NextLevelPercentage As Integer = 0

        Public MenuMode As MenuMode = Constants.MenuMode.InsideWindow
        Public ShowingTextMenu As Boolean = False
        Public TextMenuIndex As Integer = 0
#End Region

#Region " Initialization "

        Public Sub New()
            AutoIt = New AutoItX3()
            Consts = New ConstantsClass()
            Spells = New SpellsClass()
            Definitions = New ItemsClass
            State = BotState.Running
            CharacterTimerObj = New ThreadTimer(300) 'fast o.o
            TibiaWindowTimerObj = New ThreadTimer(1000)
            LightEffectTimerObj = New ThreadTimer(500)
            ChatMessageQueueTimerObj = New ThreadTimer(2500)
            HealTimerObj = New ThreadTimer(300)
            SpellTimerObj = New ThreadTimer(1000)
            EaterTimerObj = New ThreadTimer(0)
            MapReaderTimerObj = New ThreadTimer(100)
            Map = New MapReader
            RunemakerTimerObj = New ThreadTimer(1000)
            FisherTimerObj = New ThreadTimer(1000)
            AdvertiseTimerObj = New ThreadTimer(125000)
            ChildWindowTimerObj = New ThreadTimer(500)
            ExpCheckerTimerObj = New ThreadTimer(500)
            FPSChangerTimerObj = New ThreadTimer(1000)
            StatsUploaderTimerObj = New ThreadTimer()
            PingTimerObj = New ThreadTimer(5000)
            PacketsFromServerTimerObj = New ThreadTimer(5)
        End Sub

#End Region

#Region " AfterInjection "

        Public Sub AfterInjection()
            Consts.LoadConstants(GetConfigurationDirectory() & "\" & Versions.Items(Tibia.Version).ConstantsFile)
            Tibia.Memory.Read(Consts.ptrFrameRateBegin, FrameRateBegin, 4)
            ConstantsLoaded = True
            CharacterTimerObj.StartTimer()
            TibiaWindowTimerObj.StartTimer()
            ChildWindowTimerObj.StartTimer()
            ChatMessageQueueTimerObj.StartTimer()
            ExpCheckerTimerObj.StartTimer()
            PingTimerObj.StartTimer()
            PacketsFromServerTimerObj.StartTimer()
        End Sub

#End Region

#Region " Timers "

#Region " Packets From Server Timer "

        Private Sub PacketsFromServerTimerObj_Execute() Handles PacketsFromServerTimerObj.OnExecute
            Try
                Dim bytBuffer() As Byte = PacketsFromServerQueue.Dequeue
                PacketFromServer(bytBuffer)
            Catch Ex As System.InvalidOperationException
                'nuffin
            End Try
        End Sub

#End Region

#Region " Ping Timer "

        Private Sub PingTimerObj_Execute() Handles PingTimerObj.OnExecute
            Static MissedPings As Integer = 0
            If Not Tibia.Ping(1000) Then 'replied
                MissedPings += 1
                If MissedPings >= 3 Then
                    If MessageBox.Show("The Tibia Client is not responding." & vbCrLf & "Would you like to close TibiaTek Bot or keep waiting until it becomes responsive?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                        End
                    Else
                        MissedPings = 0
                    End If
                End If
            Else
                If MissedPings > 0 Then MissedPings = 0
            End If
        End Sub

#End Region

#Region " Child Window Timer "
        Private Sub ChildWindowTimerObj_Execute() Handles ChildWindowTimerObj.OnExecute
            Static Dim ChildWindowBegin As Integer = 0
            Static Dim ChildWindowCaption As String = ""
            'Dim Title As String
            Tibia.Memory.Read(Consts.ptrWindowBegin, ChildWindowBegin, 4)
            If ChildWindowBegin = 0 Then 'no window opened
                If InGame() Then
                    If Not ExpCheckerActivated AndAlso Not FakingTitle Then
                        Tibia.Title = Core.CharacterName
                    End If
                Else
                    Tibia.Title = "Not Logged In"
                End If
            Else
                Tibia.Memory.Read(ChildWindowBegin + Consts.WindowCaptionOffset, ChildWindowCaption)
                If InGame() Then
                    If Not ExpCheckerActivated AndAlso Not FakingTitle Then
                        Tibia.Title = Core.CharacterName
                    End If
                    If Not HotkeyWindowWasOpened AndAlso ChildWindowCaption.Equals("Hotkey Options") Then
                        HotkeyWindowWasOpened = True
                    End If
                Else
                    Tibia.Title = ChildWindowCaption
                End If
            End If
        End Sub
#End Region

#Region " Character Timer "
        Private Sub CharacterTimerObj_OnExecute() Handles CharacterTimerObj.OnExecute
            Static Dim CharacterSelectionIndex As Integer = 0
            Static Dim CharacterListBegin As Integer = 0
            Static Dim Conditions As Integer = 0
            If Not InGame() Then Exit Sub

            ' Character ID
            Core.Tibia.Memory.Read(Consts.ptrCharacterID, CharacterID, 4)

            ' Character Name & World
            Core.Tibia.Memory.Read(Consts.ptrCharacterSelectionIndex, CharacterSelectionIndex, 1)
            Core.Tibia.Memory.Read(Consts.ptrCharacterListBegin, CharacterListBegin, 4)
            Core.Tibia.Memory.Read(CharacterListBegin + (CharacterSelectionIndex * Consts.CharacterListDist), CharacterName)
            Core.Tibia.Memory.Read(CharacterListBegin + (CharacterSelectionIndex * Consts.CharacterListDist) + Consts.CharacterListWorldOffset, CharacterWorld)

            ' Character Level
            Core.Tibia.Memory.Read(Consts.ptrLevel, CharacterLevel, 4)

            ' Character Experience
            Core.Tibia.Memory.Read(Consts.ptrExperience, CharacterExperience, 4)

            ' Character Hit Points
            Core.Tibia.Memory.Read(Consts.ptrHitPoints, CharacterHitPoints, 4)

            ' Character Max Hit Points
            Core.Tibia.Memory.Read(Consts.ptrMaxHitPoints, CharacterMaxHitPoints, 4)

            ' Character Mana Points
            Core.Tibia.Memory.Read(Consts.ptrManaPoints, CharacterManaPoints, 4)

            ' Character Max Mana Points
            'Core.Tibia.Memory.Read(Consts.ptrMaxManaPoints, CharacterMaxManaPoints, 4)

            ' Character Soul Points
            Core.Tibia.Memory.Read(Consts.ptrSoulPoints, CharacterSoulPoints, 4)

            ' Character Loc
            Core.Tibia.Memory.Read(Consts.ptrCoordX, CharacterLoc.X, 4)
            Core.Tibia.Memory.Read(Consts.ptrCoordY, CharacterLoc.Y, 4)
            Core.Tibia.Memory.Read(Consts.ptrCoordZ, CharacterLoc.Z, 1)

            ' Character Capacity
            Core.Tibia.Memory.Read(Consts.ptrCapacity, CharacterCapacity, 4)

            ' Character Conditions
            Core.Tibia.Memory.Read(Consts.ptrConditions, Conditions, 4)
            CharacterConditions = Conditions
        End Sub
#End Region

#Region " Tibia Window Timer "
        Private Sub TibiaWindowTimer_OnExecute() Handles TibiaWindowTimerObj.OnExecute
            Static Dim WP As New Win32API.WindowPlacement
            Static Dim hWnd As Integer = 0
            Static Dim CurrentProcess As Process
            Static Dim TTBProcess As Process
            Static Dim ProcessID As Integer = 1
            If TibiaWindowState = WindowState.Hidden Then
                Return
            End If
            If WP.Length = 0 Then WP.Length = Convert.ToByte(Marshal.SizeOf(GetType(Win32API.WindowPlacement)))
            If Not Win32API.GetWindowPlacement(Tibia.GetWindowHandle, WP) Then
                Return
            End If
            Select Case WP.ShowCmd
                Case Win32API.ShowState.SW_SHOWNORMAL, Win32API.ShowState.SW_SHOWMAXIMIZED
                    hWnd = Win32API.GetForegroundWindow()
                    Win32API.GetWindowThreadProcessId(hWnd, ProcessID)
                    If ProcessID = 1 Then
                        TibiaWindowState = WindowState.Inactive
                        Return
                    End If
                    CurrentProcess = Process.GetProcessById(ProcessID)
                    TTBProcess = Process.GetCurrentProcess()
                    CurrentProcess.Refresh()
                    If CurrentProcess.Id = Tibia.Process.Id OrElse CurrentProcess.Id = TTBProcess.Id Then
                        TibiaWindowState = WindowState.Active
                    Else
                        TibiaWindowState = WindowState.Inactive
                    End If
                Case Win32API.ShowState.SW_SHOWMINIMIZED
                    If TibiaWindowState <> WindowState.Minimized Then TibiaWindowState = WindowState.Minimized
            End Select
        End Sub
#End Region

#Region " Map Reader Timer "
        Public Sub MapReaderTimerObj_Execute() Handles MapReaderTimerObj.OnExecute
            If Not InGame() Then Exit Sub
            If CharacterLastLocation.X <> CharacterLoc.X OrElse _
               CharacterLastLocation.Y <> CharacterLoc.Y OrElse _
               CharacterLastLocation.Z <> CharacterLoc.Z Then
                CharacterLastLocation = CharacterLoc
                Map.Refresh()
            End If
        End Sub
#End Region

#Region " Light Effect Timer "
        Private Sub LightEffectTimerObj_Execute() Handles LightEffectTimerObj.OnExecute
            If Not InGame() Then Exit Sub
            SetLight(LightI, LightC)
        End Sub

        Public Sub SetLight(ByVal LightIntensity As LightIntensity, ByVal LightColor As LightColor)
            Dim BL As New BattleList
            BL.JumpToEntity(SpecialEntity.Myself)
            If (BL.LightIntensity <> LightIntensity) OrElse (BL.LightColor <> LightColor) Then
                BL.LightIntensity = LightIntensity
                BL.LightColor = LightColor
            End If
        End Sub
#End Region

#Region " Chat Message Queue Timer "

        Private Sub ChatMessageQueueTimerObj_Execute() Handles ChatMessageQueueTimerObj.OnExecute
            If State = BotState.Paused Then Exit Sub
            If Not InGame() OrElse State = BotState.Stopped Then
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
                        'Log("You", "Said in default: " & ChatMessage.Message)
                    Case MessageType.Whisper 'whisper
                        bytBuffer = Speak(ChatMessage.Message, MessageType.Whisper)
                        'Log("You", "Whispered in default: " & ChatMessage.Message)
                    Case MessageType.Yell 'yell
                        bytBuffer = Speak(ChatMessage.Message, MessageType.Yell)
                        'Log("You", "Yelled in default: " & ChatMessage.Message)
                    Case MessageType.PM 'pm
                        bytBuffer = Speak(ChatMessage.Destinatary, ChatMessage.Message)
                        'Log("You", "Said to """ & ChatMessage.Destinatary & """: " & ChatMessage.Message)
                    Case MessageType.Channel 'channels
                        bytBuffer = Speak(ChatMessage.Message, ChatMessage.Channel)
                        'Dim Channel As String
                        'Select Case ChatMessage.Channel
                        '    Case ChannelType.Console
                        '        Channel = "Console"
                        '    Case ChannelType.GameChat
                        '        Channel = "Game Chat"
                        '    Case ChannelType.GuildChat
                        '        Channel = "Guild Chat"
                        '    Case ChannelType.Help
                        '        Channel = "Help"
                        '    Case ChannelType.Personal
                        '        Channel = "Personal"
                        '    Case ChannelType.RLChat
                        '        Channel = "RL Chat"
                        '    Case ChannelType.Trade
                        '        Channel = "Trade"
                        '    Case Else
                        '        Channel = "Unknown"
                        'End Select
                        'Log("You", "Said in """ & Channel & """: " & ChatMessage.Message)
                End Select
                'AppObjs.ConsoleRead(CStr(ChatMessage.SentByUser))
                SendPacketToServer(bytBuffer)
                'Proxy.SendPacketToClient(SystemMessage(SysMessageType.StatusSmall, ChatMessage.Message))
            End If
        End Sub

#End Region

#Region " Spell Caster Timer "
        Public Sub SpellTimerObj_Execute() Handles SpellTimerObj.OnExecute
            If Not InGame() Then Exit Sub
            If SpellTimerObj.Interval = 4000 Then SpellTimerObj.Interval = 1000
            If SpellManaRequired = 0 OrElse SpellMsg.Length = 0 Then Exit Sub
            If CharacterManaPoints = 0 Then
                Exit Sub
            ElseIf CharacterManaPoints >= SpellManaRequired Then
                SpellTimerObj.Interval = 4000
                Core.SendPacketToServer(Speak(SpellMsg))
            End If
        End Sub
#End Region

#Region " Auto Healer Timer "

        Private Sub HealTimerObj_Execute() Handles HealTimerObj.OnExecute
            SyncLock HealTimerObj
                If Not InGame() Then Exit Sub
                If HealTimerObj.Interval > Consts.HealersCheckInterval Then HealTimerObj.Interval = Consts.HealersCheckInterval
                If HealMinimumHP = 0 OrElse CharacterHitPoints > HealMinimumHP Then Exit Sub
                Dim Output As String = HealSpell.Words
                If CharacterManaPoints < Spells.GetSpellMana(HealSpell.Name) Then Exit Sub
                If String.Compare(HealSpell.Name, "heal friend", True) = 0 Then
                    Output &= " """ & CharacterName & """"
                End If
                If Not String.IsNullOrEmpty(HealComment) Then Output &= " """"" & HealComment
                HealTimerObj.Interval = Consts.HealersAfterHealDelay
                SendPacketToServer(Speak(Output, MessageType.Normal))
            End SyncLock
        End Sub

#End Region

#Region " Auto Eater Timer "

        Public Sub EaterTimerObj_Execute() Handles EaterTimerObj.OnExecute
            If State <> BotState.Running OrElse Not InGame() Then Exit Sub
            If AutoEaterSmart > 0 AndAlso CharacterHitPoints > AutoEaterSmart Then Exit Sub
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
        End Sub

        Public Function EatFromFloor(ByVal MaxDistance As Double) As Boolean
            Dim TileObj As TileObject
            Dim TileObjects As TileObject()
            For Left As Integer = 1 To 17
                For Top As Integer = 1 To 17
                    Dim Dist As Double = Math.Sqrt(Math.Pow(Left - 8, 2) + Math.Pow(Top - 6, 2))
                    If Dist <= MaxDistance Then
                        TileObjects = Map.GetTileObjects(Left, Top, MapReader.WorldZToClientZ(CharacterLoc.Z))
                        For Each TileObj In TileObjects
                            If Definitions.IsFood(TileObj.GetObjectID) Then
                                Core.SendPacketToServer(UseObjectOnGround(TileObj.GetObjectID, TileObj.GetMapLocation))
                                Return True
                            End If
                        Next
                    End If
                Next
            Next
            Return False
        End Function

        Public Function EatFromContainers() As Boolean
            Dim Container As New Container
            Dim Item As ContainerItemDefinition
            Dim ContainerItemCount = 0
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
                Core.SendPacketToServer(UseObject(Item))
            End If
            Return Found
        End Function
#End Region

#Region " Runemaker Timer "

        Public Sub RunermakerTimerObj_Execute() Handles RunemakerTimerObj.OnExecute
            If Not InGame() OrElse State <> BotState.Running Then Exit Sub
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
            If RunemakerManaPoints = 0 OrElse RunemakerSoulPoints = 0 Then Exit Sub
            'Continue only if there's enough mana
            If CharacterManaPoints < RunemakerManaPoints OrElse CharacterManaPoints < RunemakerConjure.ManaPoints Then
                Exit Sub
            End If
            'Exit if no soulpoints
            If CharacterSoulPoints < RunemakerSoulPoints OrElse CharacterSoulPoints < RunemakerConjure.SoulPoints Then
                RunemakerManaPoints = 0
                RunemakerSoulPoints = 0
                RunemakerTimerObj.StopTimer()
                ConsoleError("You ran out of Soul Points, therefore the Runemaker is now Disabled.")
                Exit Sub
            End If
            'Check that there are no items occupying the belt slot
            Core.Tibia.Memory.Read(Core.Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Core.Consts.ItemDist), BeltSlot, 2)

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
                    SendPacketToServer(MoveObject(BeltSlot, GetInventorySlotAsLocation(InventorySlots.Belt), GetInventorySlotAsLocation(InventorySlots.Backpack), 100))
                    System.Threading.Thread.Sleep(1000)
                    Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
                Loop While BeltSlot <> 0
            End If

            'Find Blank Rune
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

            'Move any object in right hand to arrow slot
            Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
            Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist) + Consts.ItemCountOffset, RightHandSlotCount, 1)
            If RightHandSlot > 0 Then
                FirstRightHandSlot = RightHandSlot
                FirstRightHandSlotCount = RightHandSlotCount
                Count = RightHandSlotCount
                If Count = 0 Then Count = 1
                Retries = 0
                Do
                    Retries += 1
                    If Retries > 20 Then ' Stuck for 10s
                        RunemakerManaPoints = 0
                        RunemakerSoulPoints = 0
                        RunemakerTimerObj.StopTimer()
                        ConsoleError("Runemaker is stuck. Can't move item from right hand to belt/arrow slot. Runemaker is now disabled.")
                        Exit Sub
                    End If
                    SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(InventorySlots.RightHand), GetInventorySlotAsLocation(InventorySlots.Belt), Count))
                    System.Threading.Thread.Sleep(1000)
                    Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.Belt - 1) * Consts.ItemDist), BeltSlot, 2)
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
                SendPacketToServer(MoveObject(BlankRune.ID, BlankRune.Location, GetInventorySlotAsLocation(InventorySlots.RightHand), 1))
                System.Threading.Thread.Sleep(1000)
                Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                SendPacketToServer(Speak(RunemakerConjure.Words))
                System.Threading.Thread.Sleep(1000)
                Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
            Loop Until RightHandSlot <> BlankRuneID

            'move magical rune to backpack
            Retries = 0
            Do
                Retries += 1
                If Retries > 20 Then
                    RunemakerManaPoints = 0
                    RunemakerSoulPoints = 0
                    RunemakerTimerObj.StopTimer()
                    ConsoleError("Runemaker is stuck. Can't move " & RunemakerConjure.Name & " Rune to it's container. Runemaker is now disabled.")
                    Exit Sub
                End If
                SendPacketToServer(MoveObject(RightHandSlot, GetInventorySlotAsLocation(InventorySlots.RightHand), BlankRune.Location, 1))
                System.Threading.Thread.Sleep(1000)
                Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
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
                    SendPacketToServer(MoveObject(FirstRightHandSlot, GetInventorySlotAsLocation(InventorySlots.Belt), GetInventorySlotAsLocation(InventorySlots.RightHand), Count))
                    System.Threading.Thread.Sleep(1000)
                    Core.Tibia.Memory.Read(Consts.ptrInventoryBegin + ((InventorySlots.RightHand - 1) * Consts.ItemDist), RightHandSlot, 2)
                Loop Until RightHandSlot = FirstRightHandSlot
            End If
            System.Threading.Thread.Sleep(5000)

        End Sub
#End Region

#Region " Fisher Timer "
        Public Sub FisherTimerObj_OnExecute() Handles FisherTimerObj.OnExecute
            If FisherTimerObj.State = ThreadTimerState.Stopped Then Exit Sub
            Static Dim Intervals() As UShort = {1000, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000}
            If Not InGame() Or State <> BotState.Running Then Exit Sub
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
                If CharacterCapacity < FisherMinimumCapacity Then
                    Exit Sub
                End If
            End If
            Dim TileObjects() As TileObject
            Map.Refresh()
            For XXX As Integer = 1 To 15
                For YYY As Integer = 1 To 11
                    TileObjects = Map.GetTileObjects(XXX, YYY, MapReader.WorldZToClientZ(CharacterLoc.Z))
                    If TileObjects.Length = 1 Then
                        TileID = TileObjects(0).GetObjectID
                        If TileID >= &H11F5 And TileID <= &H11FA Then 'There is fish
                            Tiles.Add(TileObjects(0))
                        End If
                    End If
                Next
            Next
            If Tiles.Count > 0 Then
                Dim RandomNumber As New Random(Date.Now.Second)
                Tile = Tiles(RandomNumber.Next(Tiles.Count))
                SendPacketToServer(UseFishingRodOnLocation(FishingRodItemData, Tile.GetMapLocation, Tile.GetObjectID))
                If FisherSpeed > 0 AndAlso Not FisherTimerObj.Interval = FisherSpeed Then
                    FisherTimerObj.Interval = FisherSpeed
                Else
                    FisherTimerObj.Interval = Intervals(RandomNumber.Next(Intervals.Length))
                End If
            End If
        End Sub
#End Region

#Region " Advertise Timer "

        Public Sub AdvertiseTimerObj_Execute() Handles AdvertiseTimerObj.OnExecute
            If Not InGame() Or State <> BotState.Running Then Exit Sub
            If AdvertiseMsg.Length = 0 Then Exit Sub
            Dim ChatMessage As New ChatMessageDefinition
            ChatMessage.MessageType = MessageType.Channel
            ChatMessage.Channel = ChannelType.Trade
            ChatMessage.Message = AdvertiseMsg
            Core.ChatMessageQueueList.Add(ChatMessage)
        End Sub
#End Region

#Region " Experience Checker Timer "
        Public Sub ExpCheckerTimerObj_Execute() Handles ExpCheckerTimerObj.OnExecute
            If Not InGame() Then Exit Sub
            If (CharacterLevel = 0) Or (CharacterExperience = 0) Then Exit Sub
            If ExpCheckerActivated Then
                If LastExperience > 0 AndAlso CharacterExperience = LastExperience Then
                    Exit Sub
                End If
            End If
            NextLevelExp = Floor(((16 + (2 / 3)) * Pow(CharacterLevel + 1, 3)) - (100 * Pow(CharacterLevel + 1, 2)) + (((283 + (1 / 3)) * (CharacterLevel + 1)) - 200))
            CurrentLevelExp = Floor(((16 + (2 / 3)) * Pow(CharacterLevel, 3)) - (100 * Pow(CharacterLevel, 2)) + (((283 + (1 / 3)) * (CharacterLevel)) - 200))
            NextLevelPercentage = Floor((CharacterExperience - CurrentLevelExp) * 100 / (NextLevelExp - CurrentLevelExp))
            If ExpCheckerActivated Then
                Tibia.Title = CharacterName & " - Exp. For Level " & (CharacterLevel + 1) & ": " & (NextLevelExp - CharacterExperience) & " (" & NextLevelPercentage & "% completed)"
                LastExperience = CharacterExperience
            End If
        End Sub
#End Region

#Region " FPS Changer Timer "

        Private Sub FPSChangerTimerObj_Execute() Handles FPSChangerTimerObj.OnExecute
            If Not InGame() Then
                Tibia.Memory.Write(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenActive))
                System.Threading.Thread.Sleep(1000)
                Exit Sub
            End If
            Select Case TibiaWindowState
                Case WindowState.Active
                    Tibia.Memory.Write(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenActive))
                    System.Threading.Thread.Sleep(1000)
                Case WindowState.Inactive
                    Tibia.Memory.Write(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenInactive))
                    System.Threading.Thread.Sleep(1000)
                Case WindowState.Minimized
                    Tibia.Memory.Write(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenMinimized))
                    System.Threading.Thread.Sleep(1000)
                Case WindowState.Hidden
                    Tibia.Memory.Write(FrameRateBegin + Consts.FrameRateLimitOffset, FPSBToX(Consts.FPSWhenHidden))
                    System.Threading.Thread.Sleep(1000)
            End Select
        End Sub


#End Region

#Region " Stats Uploader Timer "
        Private Sub StatsUploaderTimerObj_Execute() Handles StatsUploaderTimerObj.OnExecute
            'Try
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
            xmlLevel.InnerText = CharacterLevel

            Dim xmlExperience As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Experience", "")
            xmlExperience.InnerText = CharacterExperience

            Dim xmlCurrentLevelExperience As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "CurrentLevelExperience", "")
            xmlCurrentLevelExperience.InnerText = CurrentLevelExp

            Dim xmlNextLevelExperience As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "NextLevelExperience", "")
            xmlNextLevelExperience.InnerText = NextLevelExp

            Dim xmlExpForNextLevel As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "ExpForNextLevel", "")
            xmlExpForNextLevel.InnerText = (NextLevelExp - CharacterExperience)

            Dim xmlHitPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "HitPoints", "")
            xmlHitPoints.InnerText = CharacterHitPoints

            Dim xmlManaPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "ManaPoints", "")
            xmlManaPoints.InnerText = CharacterManaPoints

            Dim xmlSoulPoints As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "SoulPoints", "")
            xmlSoulPoints.InnerText = CharacterSoulPoints

            Dim xmlCapacity As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Capacity", "")
            Dim Capacity As Integer = 0
            Core.Tibia.Memory.Read(Consts.ptrCapacity, Capacity, 2)
            xmlCapacity.InnerText = Capacity

            Dim xmlStamina As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Stamina", "")
            Dim Stamina As Integer = 0
            Core.Tibia.Memory.Read(Consts.ptrStamina, Stamina, 4)
            Dim StaminaTime As TimeSpan = TimeSpan.FromSeconds(Stamina)
            xmlStamina.InnerText = StaminaTime.ToString

            Dim xmlSkills As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Skills", "")
            Dim Skill As Integer = 0
            Dim SkillPercent As Integer = 0

            Dim xmlFistFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "FistFighting", "")
            Dim xmlFistFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.FistFighting * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.FistFighting * Consts.SkillsDist), SkillPercent, 1)
            xmlFistFighting.InnerText = Skill
            xmlFistFightingP.InnerText = SkillPercent
            xmlFistFighting.Attributes.Append(xmlFistFightingP)
            xmlSkills.AppendChild(xmlFistFighting)

            Dim xmlClubFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "ClubFighting", "")
            Dim xmlClubFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.ClubFighting * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.ClubFighting * Consts.SkillsDist), SkillPercent, 1)
            xmlClubFighting.InnerText = Skill
            xmlClubFightingP.InnerText = SkillPercent
            xmlClubFighting.Attributes.Append(xmlClubFightingP)
            xmlSkills.AppendChild(xmlClubFighting)

            Dim xmlSwordFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "SwordFighting", "")
            Dim xmlSwordFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.SwordFighting * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.SwordFighting * Consts.SkillsDist), SkillPercent, 1)
            xmlSwordFighting.InnerText = Skill
            xmlSwordFightingP.InnerText = SkillPercent
            xmlSwordFighting.Attributes.Append(xmlSwordFightingP)
            xmlSkills.AppendChild(xmlSwordFighting)

            Dim xmlAxeFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "AxeFighting", "")
            Dim xmlAxeFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.AxeFighting * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.AxeFighting * Consts.SkillsDist), SkillPercent, 1)
            xmlAxeFighting.InnerText = Skill
            xmlAxeFightingP.InnerText = SkillPercent
            xmlAxeFighting.Attributes.Append(xmlAxeFightingP)
            xmlSkills.AppendChild(xmlAxeFighting)

            Dim xmlDistanceFighting As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "DistanceFighting", "")
            Dim xmlDistanceFightingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.DistanceFighting * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.DistanceFighting * Consts.SkillsDist), SkillPercent, 1)
            xmlDistanceFighting.InnerText = Skill
            xmlDistanceFightingP.InnerText = SkillPercent
            xmlDistanceFighting.Attributes.Append(xmlDistanceFightingP)
            xmlSkills.AppendChild(xmlDistanceFighting)

            Dim xmlShielding As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Shielding", "")
            Dim xmlShieldingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.Shielding * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.Shielding * Consts.SkillsDist), SkillPercent, 1)
            xmlShielding.InnerText = Skill
            xmlShieldingP.InnerText = SkillPercent
            xmlShielding.Attributes.Append(xmlShieldingP)
            xmlSkills.AppendChild(xmlShielding)

            Dim xmlFishing As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Fishing", "")
            Dim xmlFishingP As XmlAttribute = xmlFile.CreateAttribute("Percent")
            Core.Tibia.Memory.Read(Consts.ptrSkillsBegin + (Skills.Fishing * Consts.SkillsDist), Skill, 1)
            Core.Tibia.Memory.Read(Consts.ptrSkillsPercentBegin + (Skills.Fishing * Consts.SkillsDist), SkillPercent, 1)
            xmlFishing.InnerText = Skill
            xmlFishingP.InnerText = SkillPercent
            xmlFishing.Attributes.Append(xmlFishingP)
            xmlSkills.AppendChild(xmlFishing)

            Dim xmlBattlelist As XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Battlelist", "")
            BL.Reset(True)
            Do
                If BL.IsOnScreen AndAlso Not BL.IsMyself Then
                    Dim xmlEntity As System.Xml.XmlNode = xmlFile.CreateNode(Xml.XmlNodeType.Element, "Entity", "")
                    Dim Loc As LocationDefinition = BL.GetLocation
                    Dim X As XmlAttribute = xmlFile.CreateAttribute("X")
                    X.Value = Loc.X
                    Dim Y As XmlAttribute = xmlFile.CreateAttribute("Y")
                    Y.Value = Loc.Y
                    Dim Z As XmlAttribute = xmlFile.CreateAttribute("Z")
                    Z.Value = Loc.Z
                    Dim HP As XmlAttribute = xmlFile.CreateAttribute("HP")
                    HP.Value = BL.GetHPPercentage
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
                    xmlContainerSize.InnerText = Container.GetContainerSize
                    Dim xmlContainerItems As XmlAttribute = xmlFile.CreateAttribute("Items")
                    xmlContainerItems.InnerText = ContainerItemCount
                    xmlContainer.Attributes.Append(xmlContainerName)
                    xmlContainer.Attributes.Append(xmlContainerItems)
                    xmlContainer.Attributes.Append(xmlContainerSize)
                    For I As Integer = 0 To ContainerItemCount - 1
                        Item = Container.Items(I)
                        Dim xmlItem As XmlNode = xmlFile.CreateNode(XmlNodeType.Element, "Item", "")
                        Dim xmlItemName As XmlAttribute = xmlFile.CreateAttribute("Name")
                        xmlItemName.InnerText = Definitions.GetItemName(Item.ID)
                        Dim xmlItemID As XmlAttribute = xmlFile.CreateAttribute("ID")
                        xmlItemID.InnerText = Item.ID
                        Dim xmlItemCount As XmlAttribute = xmlFile.CreateAttribute("Count")
                        xmlItemCount.InnerText = Item.Count
                        Dim xmlItemSlot As XmlAttribute = xmlFile.CreateAttribute("Slot")
                        xmlItemSlot.InnerText = Item.Slot
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

            If Consts.StatsUploaderSaveOnDiskOnly Then
                xmlFile.Save(Consts.StatsUploaderPath & Consts.StatsUploaderFilename)
            Else
                xmlFile.Save("temp.xml")
                'Dim CS As New CaptureScreen.CaptureScreen
                'CS.CaptureScreenToFile("screenshot.jpg", ImageFormat.Jpeg)
                If IO.File.Exists("temp.xml") Then  'AndAlso IO.File.Exists("screenshot.jpg")
                    MsgBox("ftp://" & Consts.StatsUploaderUserID & ":" & Consts.StatsUploaderPassword & "@" & Consts.StatsUploaderUrl & Consts.StatsUploaderPath & Consts.StatsUploaderFilename)
                    Client.UploadFile("ftp://" & Consts.StatsUploaderUserID & ":" & Consts.StatsUploaderPassword & "@" & Consts.StatsUploaderUrl & Consts.StatsUploaderPath & Consts.StatsUploaderFilename, "Temp.xml")
                    'Client.UploadFile("ftp://" & Consts.StatsUploaderUserID & ":" & Consts.StatsUploaderPassword & "@" & Consts.StatsUploaderUrl & Consts.StatsUploaderPath & "screenshot.jpg", "screenshot.jpg")
                    IO.File.Delete("temp.xml")
                    'IO.File.Delete("screenshot.jpg")
                End If
            End If
            'Catch
            'Finally
            'StatsUploaderIsBusy = False
            'End Try
        End Sub
#End Region

#End Region

#Region " Bot State "
        Public Sub Start()
            State = BotState.Running
        End Sub

        Public Sub [Resume]()
            State = BotState.Running
        End Sub

        Public Sub Pause()
            State = BotState.Paused
        End Sub

        Public Sub [Stop]()
            ' DO NOT ADD TibiaWindowTimer
            State = BotState.Stopped

            'CharacterTimerObj.StopTimer()
            LightEffectTimerObj.StopTimer()
            SpellMsg = ""
            SpellManaRequired = 0
            SpellTimerObj.StopTimer()
            EaterTimerObj.StopTimer()
            RunemakerTimerObj.StopTimer()
            RunemakerManaPoints = 0
            RunemakerSoulPoints = 0
            ExpCheckerActivated = False
            FakingTitle = False
            ExpCheckerTimerObj.StopTimer()

            ChatMessageQueueList.Clear()
            ChatMessageQueueTimerObj.StopTimer()
        End Sub
#End Region

        Public Function InGame() As Boolean
            Static Dim InGame_ As Integer = 0
            Core.Tibia.Memory.Read(Consts.ptrInGame, InGame_, 1)
            If MapReaderTimerObj.State = ThreadTimerState.Stopped Then MapReaderTimerObj.StartTimer()
            Return InGame_ = 8
        End Function

        Public Sub WndProc(ByRef M As Message)
            Select Case M.Msg
                Case WM.Test
                    'Try
                    Dim wParam As Integer = M.WParam.ToInt32
                    Dim lParam As Integer = M.LParam.ToInt32
                    Dim test As Test
                    test.Number1 = 0
                    test.Number2 = 0
                    test.Number3 = 0
                    Dim name As String
                    Dim CurByte As Byte = 0
                    Dim bytBuffer(M.WParam.ToInt32 - 1) As Byte
                    Dim Address As Integer = M.LParam.ToInt32
                    For I As Integer = 0 To bytBuffer.Length - 1
                        Tibia.Memory.Read(Address + I, CurByte)
                        bytBuffer(I) = CurByte
                    Next
                    Dim handle As GCHandle = GCHandle.Alloc(bytBuffer, GCHandleType.Pinned)
                    test = Marshal.PtrToStructure(handle.AddrOfPinnedObject, GetType(Test))
                    name = System.Text.Encoding.ASCII.GetString(test.Name).Replace(vbNullChar, "")
                    MsgBox("number1 = " & Hex(test.Number1) & " & number2 = " & Hex(test.Number2) & " & number3 = " & Hex(test.Number3) & " & name = " & name & ".")
                    MsgBox("wParam = " & Hex(wParam) & " & lParam = " & Hex(lParam))
                    'Catch ex As Exception
                    '   MsgBox(ex.Message)
                    'End Try
                Case WM.Ping
                    ConsoleWrite("Pinged")
                    M.Result = 1
                Case WM.Recv 'packetfromserver
                    Dim CurByte As Integer = 0
                    Dim bytBuffer(M.WParam.ToInt32 - 1) As Byte
                    Dim Address As Integer = M.LParam.ToInt32
                    For I As Integer = 0 To bytBuffer.Length - 1
                        Tibia.Memory.Read(Address + I, CurByte, 1)
                        bytBuffer(I) = CByte(CurByte)
                    Next
                    PacketsFromServerQueue.Enqueue(bytBuffer)
                    'PacketFromServer(bytBuffer)
                    M.Result = M.WParam.ToInt32
                Case WM.Send 'packetfromclient
                    Dim CurByte As Integer = 0
                    Dim Send As Boolean = True
                    Dim bytBuffer(M.WParam.ToInt32 - 1) As Byte
                    Dim Address As Integer = M.LParam.ToInt32
                    'this has to be FAST
                    For I As Integer = 0 To bytBuffer.Length - 1
                        Tibia.Memory.Read(Address + I, CurByte, 1)
                        bytBuffer(I) = CByte(CurByte)
                    Next
                    PacketFromClient(bytBuffer, Send)
                    If Not Send Then
                        M.Result = New System.IntPtr(0)
                    Else
                        M.Result = New System.IntPtr(M.WParam.ToInt32)
                    End If
                Case WM.Injected
                    If M.WParam.ToInt32 = 1 Then
                        InjectionState = InjectionState.Injected
                        M.Result = 1
                    Else
                        InjectionState = InjectionState.Failed
                        M.Result = 0
                        End
                    End If
                    'M.Result = 500
                Case WM.Hooked
                    ConsoleWrite("Hooked " & M.WParam.ToInt32 & " " & M.LParam.ToInt32)
                Case WM.Unhooked
                    ConsoleWrite("UnHooked " & M.WParam.ToInt32 & " " & M.LParam.ToInt32)
                Case WM.Uninjected
                    InjectionState = InjectionState.Uninjected
                    [Stop]()
                    Tibia.Show()
                    End
                Case WM.WASD
                    WASDActive = CBool(M.WParam.ToInt32)
                    WASDSayModeActive = CBool(M.LParam.ToInt32)
                Case WM.ShowMenu
                    M.Result = 0
                    Select Case CType(M.WParam.ToInt32, MenuType)
                        Case MenuType.Menu
                            frmMain.CMMenu.Show(New Point(M.LParam.ToInt32 >> &H10, M.LParam.ToInt32 And &HFFFF))
                        Case MenuType.Options
                            'options plx
                        Case MenuType.About
                            frmMain.CMAbout.Show(New Point(M.LParam.ToInt32 >> &H10, M.LParam.ToInt32 And &HFFFF))
                        Case MenuType.SwitchMenu
                            Tibia.HideMenu()
                            frmMain.Show()
                        Case MenuType.Test
                            CommandParser("test")
                        Case Else
                    End Select
                    'M.Result = 0
                Case WM.TextMenu
                    Select Case M.WParam
                        Case 1 'Show
                            If Not ShowingTextMenu Then
                                ShowingTextMenu = True
                                TextMenuForm.Show()
                            End If
                            If M.LParam.ToInt32 > 0 Then
                                Dim Input As Integer = M.LParam.ToInt32
                                Input -= AscW("0")
                                With TextMenuForm
                                    Select Case TextMenuIndex
                                        Case 0 'main menu
                                            Select Case Input
                                                Case 1
                                                    TextMenuIndex = 1 'General Tools
                                                Case 2
                                                    TextMenuIndex = 2 'AFK Tools
                                                Case 3
                                                    TextMenuIndex = 4 'Healing Tools
                                                Case 4
                                                    TextMenuIndex = 5 'Info Tools
                                                Case 5
                                                    TextMenuIndex = 7 'Training Tools
                                                Case 6
                                                    TextMenuIndex = 8 'Fun Tools
                                                Case 7
                                                    TextMenuIndex = 9 'Misc
                                                Case 8
                                                    TextMenuIndex = 10 'Hide
                                                Case 9
                                                    TextMenuIndex = 47 ' Bot state
                                                Case 0
                                                    Tibia.SendMessage(WM.TextMenu, 0, 0)
                                            End Select
                                        Case 1 'general tools
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0
                                                Case 1
                                                    TextMenuIndex = 11 'Config Manager
                                                Case 2
                                                    TextMenuIndex = 12 ' Looger
                                                Case 3
                                                    TextMenuIndex = 13 ' Stacker
                                                Case 4
                                                    TextMenuIndex = 14 ' Light
                                                Case 5
                                                    TextMenuIndex = 15 ' Ammo Restacker
                                                Case 6
                                                    MsgBox("Commands List")
                                                Case Else
                                            End Select
                                        Case 2 'afk tools 1
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0
                                                Case 1
                                                    MsgBox("Alarms")
                                                Case 2
                                                    TextMenuIndex = 16 ' Spell caster
                                                Case 3
                                                    TextMenuIndex = 17 ' Auto Eater
                                                Case 4
                                                    TextMenuIndex = 18 ' Runemaker
                                                Case 5
                                                    TextMenuIndex = 19 ' Auto Fisher
                                                Case 6
                                                    TextMenuIndex = 20 ' Trade Advertiser
                                                Case 7
                                                    TextMenuIndex = 21 ' Trade Watcher
                                                Case 8
                                                    TextMenuIndex = 22 ' Cavebot
                                                Case 9
                                                    TextMenuIndex = 3 ' AFK Tools 2
                                            End Select
                                        Case 3 ' Afk tools 2
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                                Case 1
                                                    TextMenuIndex = 23 ' Stats uploader
                                                Case 2
                                                    TextMenuIndex = 24 ' FPS Changer
                                                Case 3
                                                    TextMenuIndex = 25 ' Auto Log
                                                Case 4
                                                    TextMenuIndex = 26 ' Auto Connect
                                                Case 5
                                                    MsgBox("Auto Backpack Opener")
                                            End Select
                                        Case 4 'Healing Tools
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0 'Main menu
                                                Case 1
                                                    TextMenuIndex = 27 ' Auto healer
                                                Case 2
                                                    TextMenuIndex = 28 ' Auto UHer
                                                Case 3
                                                    TextMenuIndex = 29 ' Auto heal friend
                                                Case 4
                                                    TextMenuIndex = 30 ' Auto heal party
                                                Case 5
                                                    TextMenuIndex = 31 ' Manafluid drinker
                                            End Select
                                        Case 5 'Info Tools 1
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0 'Main menu
                                                Case 1
                                                    TextMenuIndex = 32 'Exp check
                                                Case 2
                                                    MsgBox("Character Info")
                                                Case 3
                                                    MsgBox("Guild Members Info")
                                                Case 4
                                                    TextMenuIndex = 34 ' Floor explorer
                                                Case 5
                                                    TextMenuIndex = 35 ' Namespy
                                                Case 6
                                                    TextMenuIndex = 36 'Open file/website
                                                Case 7
                                                    MsgBox("Send Location")
                                                Case 8
                                                    MsgBox("Get item ids")
                                                Case 9
                                                    TextMenuIndex = 6 ' Info Tools 2
                                            End Select
                                        Case 6 'Info tools 2
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 5 'Info tools 1
                                                Case 1
                                                    MsgBox("Training Information")
                                                Case 2
                                                    MsgBox("Floorspy")
                                            End Select
                                        Case 7 'Training Tools
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0 ' Main menu
                                                Case 1
                                                    TextMenuIndex = 37 ' Auto Attacker
                                                Case 2
                                                    TextMenuIndex = 40 ' Auto Trainer
                                                Case 3
                                                    TextMenuIndex = 41 ' Auto pickup
                                            End Select
                                        Case 8 ' fun tools
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0 'Main menu
                                                Case 1
                                                    TextMenuIndex = 42 ' Fake Title
                                                Case 2
                                                    TextMenuIndex = 43 ' Chameleon
                                                Case 3
                                                    TextMenuIndex = 44 ' Rainbow outfit
                                            End Select
                                        Case 9 ' Misc Tools
                                            Select Case Input
                                                Case 0
                                                    TextMenuIndex = 0 ' Main menu
                                                Case 1
                                                    TextMenuIndex = 45 ' WASD
                                                Case 2
                                                    MsgBox("Mage bomb/combo")
                                                Case 3
                                                    MsgBox("Screenshooter")
                                                Case 4
                                                    MsgBox("Const Editor")
                                                Case 5
                                                    MsgBox("Feedback")
                                                Case 6
                                                    TextMenuIndex = 46 ' Reload Config files
                                            End Select
                                        Case 10 'Hide
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Tibia Client")
                                                Case 2
                                                    MsgBox("TTB Window")
                                                Case 0
                                                    TextMenuIndex = 0 'Main menu
                                            End Select
                                        Case 11 'Config Manager
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Load")
                                                Case 2
                                                    MsgBox("Edit")
                                                Case 3
                                                    MsgBox("Clear")
                                                Case 0
                                                    TextMenuIndex = 1 ' General Menu
                                            End Select
                                        Case 12 'Auto Looter
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Edit")
                                                Case 3
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 1 ' General Menu
                                            End Select
                                        Case 13 'Auto Stacker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 1 'General Menu
                                            End Select
                                        Case 14 'Light Effects
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Torch")
                                                Case 3
                                                    MsgBox("Great Torch")
                                                Case 4
                                                    MsgBox("Ultimate Torch")
                                                Case 5
                                                    MsgBox("Utevo Lux")
                                                Case 6
                                                    MsgBox("Utevo Gran Lux")
                                                Case 7
                                                    MsgBox("Utevo Vis Lux")
                                                Case 8
                                                    MsgBox("Light Wand")
                                                Case 9
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 1 ' General Menu
                                            End Select
                                        Case 15 'Ammunition Restacker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 1 ' General Menu
                                            End Select
                                        Case 16 'Spell Caster
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 17 'Auto Eater
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 18 'Runemaker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 19 'Runemaker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Turbo")
                                                Case 3
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 20 'Trade Channel Advertiser
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 21 'Trade Channel Watcher
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 22 'Cavebot
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 2 'Afk Tools 1
                                            End Select
                                        Case 23 'Stats uploader
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 3 'Afk Tools 2
                                            End Select
                                        Case 24 'Fps Changer
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 3 'Afk Tools 2
                                            End Select
                                        Case 25 'Auto Log
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 3 'Afk Tools 2
                                            End Select
                                        Case 26 'Auto Connect
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 3 'Afk Tools 2
                                            End Select
                                        Case 27 'Auto Healer
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 4 'Healing Tools
                                            End Select
                                        Case 28 'Auto UHer
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 4 'Healing Tools
                                            End Select
                                        Case 29 'Auto heal friend
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 4 'Healing tools                                                    
                                            End Select
                                        Case 30 'Auto heal party
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 4 'Healing tools
                                            End Select
                                        Case 31 'Mana fluid drinker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 4 'Healing Tools
                                            End Select
                                        Case 32 'Exp Checker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    TextMenuIndex = 33 'Creatures
                                                Case 3
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 5 'Info tools 1
                                            End Select
                                        Case 33 'Creatures
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 32 'Exp Checker
                                            End Select
                                        Case 34 'Floor Explorer
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Above")
                                                Case 2
                                                    MsgBox("Around")
                                                Case 3
                                                    MsgBox("Below")
                                                Case 0
                                                    TextMenuIndex = 5 ' Info tools 1
                                            End Select
                                        Case 35 'Name spy
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 5 'Info Tools 1
                                            End Select
                                        Case 36 'Open File/websites
                                            Select Case Input
                                                Case 1
                                                    MsgBox("File")
                                                Case 2
                                                    MsgBox("Tibia wiki")
                                                Case 3
                                                    MsgBox("Tibia.com Char Page")
                                                Case 4
                                                    MsgBox("Tibia.com Guild Page")
                                                Case 5
                                                    MsgBox("Erig.net Highscore page")
                                                Case 6
                                                    MsgBox("Google")
                                                Case 7
                                                    MsgBox("My Tibia")
                                                Case 0
                                                    TextMenuIndex = 5 'Info Tools 1
                                            End Select
                                        Case 37 'Auto Attacker
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Automatic")
                                                Case 3
                                                    TextMenuIndex = 38 'Stance
                                                Case 4
                                                    TextMenuIndex = 39 'Fighting
                                                Case 5
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 7 'Trainer tools
                                            End Select
                                        Case 38 'Stance
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Stand")
                                                Case 2
                                                    MsgBox("Follow")
                                                Case 0
                                                    TextMenuIndex = 37 'Auto Attacker
                                            End Select
                                        Case 39 'Fightning
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Offensive")
                                                Case 2
                                                    MsgBox("Balanced")
                                                Case 3
                                                    MsgBox("Defensive")
                                                Case 0
                                                    TextMenuIndex = 37 'Auto Attacker
                                            End Select
                                        Case 40 'Auto Trainer
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Add")
                                                Case 3
                                                    MsgBox("Remove")
                                                Case 4
                                                    MsgBox("Clear")
                                                Case 5
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 7 'Training tools
                                            End Select
                                        Case 41 'Auto Pickup
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 7 'Training tools
                                            End Select
                                        Case 42 'Fake Titlte
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 8 'Fun Tools
                                            End Select
                                        Case 43 'Chameleon
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Change Outfit")
                                                Case 2
                                                    MsgBox("Copy Outfit")
                                                Case 0
                                                    TextMenuIndex = 8 'Fun Tools
                                            End Select
                                        Case 44 'Rainbow Outfit
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Fast")
                                                Case 3
                                                    MsgBox("Slow")
                                                Case 4
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 8 'Fun Tools
                                            End Select
                                        Case 45 'WASD
                                            Select Case Input
                                                Case 1
                                                    MsgBox("On")
                                                Case 2
                                                    MsgBox("Off")
                                                Case 0
                                                    TextMenuIndex = 9 'Misc Tools
                                            End Select
                                        Case 46 'Reload Config Files
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Spells")
                                                Case 2
                                                    MsgBox("Outfits")
                                                Case 3
                                                    MsgBox("Items")
                                                Case 4
                                                    MsgBox("Constants")
                                                Case 5
                                                    MsgBox("Tibia.dat")
                                                Case 0
                                                    TextMenuIndex = 9 'Misc Files
                                            End Select
                                        Case 47 'Bot State
                                            Select Case Input
                                                Case 1
                                                    MsgBox("Start")
                                                Case 2
                                                    MsgBox("Pause")
                                                Case 3
                                                    MsgBox("Stop")
                                                Case 0
                                                    TextMenuIndex = 0 'Main Menu
                                            End Select
                                        Case Else
                                            MsgBox("Not done")
                                    End Select
                                End With
                            End If
                        Case 0 'Hide
                            ShowingTextMenu = False
                            TextMenuIndex = 0
                            TextMenuForm.Hide()
                        Case Else
                    End Select
                    M.Result = 0
                Case WM.TextMenuMove
                    If ShowingTextMenu Then
                        TextMenuForm.BringToFront()
                        TextMenuForm.Location = New Point(M.LParam.ToInt32 And &HFFFF, M.LParam.ToInt32 >> &H10)
                    End If
                    M.Result = 0
            End Select

        End Sub

#Region " SendToServer "

        Public Sub SendPacketToServer(ByVal bytBuffer() As Byte)
            If Fix(bytBuffer.Length / 8) <> (bytBuffer.Length / 8) Then
                ReDim Preserve bytBuffer(((Fix(bytBuffer.Length / 8) + 1) * 8) - 1)
            End If
            Dim MyPointer As IntPtr = Marshal.AllocHGlobal(bytBuffer.Length)
            Marshal.Copy(bytBuffer, 0, MyPointer, bytBuffer.Length)
            If Not Tibia.SendMessage(WM.Send, bytBuffer.Length, MyPointer.ToInt32) = 1 Then
                MsgBox("error, not sent?")
            End If
        End Sub

#End Region

#Region " SendToClient "

        Public Sub SendPacketToClient(ByVal bytBuffer() As Byte)
            If Fix(bytBuffer.Length / 8) <> (bytBuffer.Length / 8) Then
                ReDim Preserve bytBuffer(((Fix(bytBuffer.Length / 8) + 1) * 8) - 1)
            End If
            Dim MyPointer As IntPtr = Marshal.AllocHGlobal(bytBuffer.Length)
            Marshal.Copy(bytBuffer, 0, MyPointer, bytBuffer.Length)
            'MsgBox(bytBuffer.Length)
            If Tibia.SendMessage(WM.Recv, bytBuffer.Length, MyPointer.ToInt32) = 1 Then
                'MsgBox("sent")
            Else
                MsgBox("error, not sent?")
            End If
        End Sub

#End Region

#Region " Uninject "

        Public Sub Uninject()
            Tibia.SendMessage(WM.Uninject, 0, 0)
        End Sub

#End Region

#Region " PacketFromClient "

        Private Sub PacketFromClient(ByVal bytBuffer() As Byte, ByRef Send As Boolean)
            'MsgBox(BytesToStr(bytBuffer))
            Dim Pos As Integer = 4
            Dim ID As Integer = GetByte(bytBuffer, Pos)
            'Core.ConsoleWrite(Hex(ID))
            Select Case ID
                Case &H1E 'ping
                Case &H82 'use item
                Case &H83 'use item with
                Case &H96 'message sent
                    'Beep()
                    'Send = False
            End Select
        End Sub

#End Region

#Region " PacketFromServer "

        Private Sub PacketFromServer(ByVal bytBuffer() As Byte)
            'Core.ConsoleWrite(BytesToStr(bytBuffer))
            Dim Pos As Integer = 0
            Dim PacketLength As UShort = GetWord(bytBuffer, Pos) + 2
            Dim Loc As LocationDefinition
            'Pos = 2
            Dim PacketID As Integer = 0
            PacketID = GetByte(bytBuffer, Pos)
            'Core.ConsoleWrite(Hex(PacketID))
            Select Case PacketID
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
                            'MessageAlarm(MessageType, Name, Level, Loc, Message) 'Alarms not set yet
                        Case Constants.MessageType.MonsterSay, Constants.MessageType.MonsterYell
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
                            If TradeWatcherActive AndAlso ChanType = ChannelType.Trade AndAlso Not Name.Equals(CharacterName) Then
                                If Regex.IsMatch(Message, TradeWatcherRegex, RegexOptions.IgnoreCase) Then
                                    'Proxy.SendPacketToClient(SystemMessage(SysMessageType.Information, "Offer: " & Name & "[" & Level & "]: " & Message))
                                    'For now let's just put that text to the console
                                    ConsoleWrite("Offer: " & Name & "[" & Level & "]: " & Message)
                                End If
                            End If
                            'Log("Message", Name & "[" & Level & "] said in " & Channel & ": " & Message) 'Logging not enabled
                        Case MessageType.PM, MessageType.PMGM 'private message
                            Message = GetString(bytBuffer, Pos)
                            'MessageAlarm(MessageType, Name, Level, Loc, Message) 'Alarms not added yet
                    End Select
            End Select
        End Sub

#End Region

#Region " Console/Screen "
        Public Sub ConsoleRead(ByVal strString As String)
            'No need yet
        End Sub

        Public Sub ConsoleWrite(ByVal strString As String)
            If frmMain.ConsoleWindow.Text = "" Then
                frmMain.ConsoleWindow.Text = strString
            Else
                frmMain.ConsoleWindow.Text = frmMain.ConsoleWindow.Text & vbNewLine & strString
            End If

        End Sub

        Public Sub ConsoleError(ByVal strString As String)
            Dim ErrorMsg As String = "Error: " & strString
            If frmMain.ConsoleWindow.Text = "" Then
                frmMain.ConsoleWindow.Text = ErrorMsg
            Else
                frmMain.ConsoleWindow.Text = frmMain.ConsoleWindow.Text & vbNewLine & ErrorMsg
            End If
        End Sub

        Public Sub ConsoleClear()
            frmMain.ConsoleWindow.Clear()
        End Sub

        Public Sub ScreenWrite(ByVal strString As String, ByVal Color As ShowTextColors)
            Dim Number As Integer
            'Tibia.Memory.Read(&H768664, Number, 2)
            'Tibia.Memory.Write(Consts.ptrScreenTextStart, strString & Chr(0))
            'Tibia.Memory.Write(Consts.ptrScreenTextColor, Color)
            'Tibia.Memory.Write(&H768664, Number + 1)
            Tibia.Memory.Write(Consts.ptrScreenTextTimer, 4000)
            'Tibia.Memory.Write(&H76866B, 0)
            'Tibia.Memory.Write(&H76866A, 0)
            'Tibia.Memory.Write(Consts.ptrScreenTextShow, 1)
        End Sub
#End Region

    End Class

End Module
