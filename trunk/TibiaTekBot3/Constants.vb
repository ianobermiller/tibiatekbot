Imports System.Xml, System.Globalization, System.Runtime.InteropServices

Public Module Constants

    Public Class ConstantsClass

#Region " Constants"

        Public LatestVersionUrl As String = ""
        Public MusicalNotesOnAlarm As Boolean = False
        Public EatFromFloor As Boolean = False
        Public EatFromFloorFirst As Boolean = False
        Public EatFromFloorMaxDistance As Double = 0
        Public StackerDelay As Integer = 0
        Public ModifyMOTD As Boolean = False
        Public RSAKeyOpenTibia As String = ""
        Public AutoPickUpDelay As Integer = 0
        Public HealersCheckInterval As Integer = 0
        Public HealersAfterHealDelay As Integer = 0
        Public HotkeysCanEquipItems As Boolean = False
        Public EquipItemsOnUse As Boolean = False
        Public AutoPublishLocation As Boolean = False
        Public AutoPublishLocationInterval As Integer = 0
        Public ShowInvisibleCreatures As Boolean = False
        Public FPSWhenActive As Double = 0
        Public FPSWhenInactive As Double = 0
        Public FPSWhenHidden As Double = 0
        Public FPSWhenMinimized As Double = 0
        Public FlashTaskbarWhenAlarmFires As Boolean = False
        Public FlashTaskbarWhenMessaged As Boolean = False
        Public LootEatFromCorpse As Boolean = False
        Public LootMaxDistance As Double = 0
        Public WaypointMaxDistance As Integer = 0
        Public CreatureExpMultiplier As Double = 0

        Public ptrCharacterListBegin As Integer = 0
        Public ptrInGame As Integer = 0
        Public ptrFrameRateBegin As Integer = 0
        Public ptrEnterOneNamePerLine As Integer = 0
        Public ptrCharacterSelectionIndex As Integer = 0
        Public ptrEncryptionKey As Integer = 0
        Public ptrRSAKey As Integer = 0
        Public ptrServerAddressBegin As Integer = 0
        Public ptrServerPortBegin As Integer = 0
        Public ServerAddressDist As Integer = 0
        Public ServerAddressCount As Integer = 0
        Public ptrCharacterID As Integer = 0
        Public ptrBattleListBegin As Integer = 0
        Public ptrFirstContainer As Integer = 0
        Public ptrInventoryBegin As Integer = 0
        Public ptrLevel As Integer = 0
        Public ptrExperience As Integer = 0
        Public ptrFollowedEntityID As Integer = 0
        Public ptrAttackedEntityID As Integer = 0
        Public ptrLastAttackedEntityID As Integer = 0
        Public ptrMaxHitPoints As Integer = 0
        Public ptrHitPoints As Integer = 0
        Public ptrManaPoints As Integer = 0
        Public ptrSoulPoints As Integer = 0
        Public ptrCapacity As Integer = 0
        Public ptrCoordX As Integer = 0
        Public ptrCoordY As Integer = 0
        Public ptrCoordZ As Integer = 0
        Public ptrSecureMode As Integer = 0
        Public ptrFightingMode As Integer = 0
        Public ptrChasingMode As Integer = 0
        Public ptrMapPointer As Integer = 0
        Public ptrConditions As Integer = 0
        Public ptrVipListBegin As Integer = 0
        Public ptrSkillsBegin As Integer = 0
        Public ptrSkillsPercentBegin As Integer = 0
        Public ptrStamina As Integer = 0
        Public ptrGoToX As Integer = 0
        Public ptrGoToY As Integer = 0
        Public ptrGoToZ As Integer = 0
        Public ptrGo As Integer = 0
        Public ptrHotkeyBegin As Integer = 0
        Public ptrWindowBegin As Integer = 0
        Public ptrLevelPercent As Integer = 0
        Public ptrMagicLevelPercent As Integer = 0
        Public ptrScreenTextStart As Integer = 0
        Public ptrScreenTextShow As Integer = 0
        Public ptrScreenTextTimer As Integer = 0
        Public ptrScreenTextColor As Integer = 0

        Public CharacterListDist As Integer = 0
        Public CharacterListWorldOffset As Integer = 0

        Public WindowLeftOffset As Integer = 0
        Public WindowTopOffset As Integer = 0
        Public WindowWidthOffset As Integer = 0
        Public WindowHeightOffset As Integer = 0
        Public WindowButtonPressedOffset As Integer = 0
        Public WindowButton1Offset As Integer = 0
        Public WindowButton2Offset As Integer = 0
        Public WindowButton3Offset As Integer = 0
        Public WindowButton4Offset As Integer = 0
        Public WindowCaptionOffset As Integer = 0
        Public UnlimitedCapacity As Boolean = False

        Public BLMax As Integer = 0
        Public BLDist As Integer = 0
        Public BLNameOffset As Integer = 0
        Public BLCoordXOffset As Integer = 0
        Public BLCoordYOffset As Integer = 0
        Public BLCoordZOffset As Integer = 0
        Public BLDirectionOffset As Integer = 0
        Public BLWalkingOffset As Integer = 0
        Public BLOutfitOffset As Integer = 0
        Public BLHeadCOffset As Integer = 0
        Public BLBodyCOffset As Integer = 0
        Public BLLegsCOffset As Integer = 0
        Public BLFeetCOffset As Integer = 0
        Public BLAddonsOffset As Integer = 0
        Public BLLightIntensityOffset As Integer = 0
        Public BLLightColorOffset As Integer = 0
        Public BLHPPercentOffset As Integer = 0
        Public BLSpeedOffset As Integer = 0
        Public BLOnScreenOffset As Integer = 0
        Public BLSkullOffset As Integer = 0
        Public BLPartyOffset As Integer = 0

        Public MaxContainers As Integer = 0
        Public ContainerDist As UShort = 0
        Public ContainerNameOffset As Integer = 0
        Public ContainerSizeOffset As Integer = 0
        Public ContainerItemCountOffset As Integer = 0
        Public ContainerFirstItemOffset As Integer = 0
        Public ContainerIDOffset As Integer = 0
        Public ContainerHasParentOffset As Integer = 0

        Public ItemDist As Integer = 0
        Public ItemCountOffset As Integer = 0

        Public MapTileIdOffset As Integer = 0
        Public MapObjectIdOffset As Integer = 0
        Public MapObjectDataOffset As Integer = 0
        Public MapObjectExtraDataOffset As Integer = 0
        Public MapObjectDist As Integer = 0
        Public MapTileDist As Integer = 0

        Public DebugOnLog As Boolean = False

        Public StatsUploaderUrl As String = ""
        Public StatsUploaderUserID As String = ""
        Public StatsUploaderPassword As String = ""
        Public StatsUploaderFrequency As Integer = 0
        Public StatsUploaderPath As String = ""
        Public StatsUploaderFilename As String = ""
        Public StatsUploaderSaveOnDiskOnly As Boolean = 0

        Public VipMax As Integer = 0
        Public VipNameOffset As Integer = 0
        Public VipStatusOffset As Integer = 0
        Public VipDist As Integer = 0

        Public SkillsDist As Integer = 0

        Public LootDelay As Integer = 0
        Public LootInBag As Boolean = False
        Public LootInBagDelay As Integer = 0

        Public HotkeyMax As Integer = 0
        Public HotkeyItemDataOffset As UShort = 0
        Public HotkeyItemOffset As Integer = 0
        Public HotkeyItemDist As Integer = 0
        Public HotkeyTextAutoSendOffset As Integer = 0
        Public HotkeyTextAutoSendDist As Integer = 0
        Public HotkeyTextOffset As Integer = 0
        Public HotkeyTextDist As Integer = 0

        Public FrameRateCurrentOffset As Integer = 0
        Public FrameRateLimitOffset As Integer = 0

        Public ptrNameSpy As Integer = 0
        Public ptrNameSpy2 As Integer = 0

        Public NameSpyDefault As Integer = 0
        Public NameSpy2Default As Integer = 0
        Public CavebotAttackerShrinkRadius As Double = 0
        Public CavebotAttackerRadius As Double = 0
        Public LootWithCavebot As Boolean = True
        Public CavebotLootMinCap As Integer = 0

        Public ptrStatusMessage As Integer = 0
        Public ptrStatusMessageTimer As Integer = 0
#End Region

        Public Sub New(Optional ByVal AutoLoad As Boolean = False)
            If AutoLoad Then LoadConstants(GetConfigurationDirectory() & "\" & Core.Versions.Items(Core.Tibia.Version).ConstantsFile)
        End Sub

        Public Sub LoadConstants(ByVal ConstantsFile As String)
            Dim Reader As System.Xml.XmlTextReader
            Dim CI As CultureInfo = New CultureInfo("en-US", False)
            Try
                Reader = New System.Xml.XmlTextReader(ConstantsFile)
                Reader.WhitespaceHandling = WhitespaceHandling.None
                Dim Value As String = ""
                Dim Name As String = ""
                While Reader.Read()
                    If Reader.NodeType = XmlNodeType.Element Then
                        Select Case Reader.Name
                            Case "Constants"
                                While Reader.Read()
                                    If Reader.NodeType = XmlNodeType.Element AndAlso Reader.Name = "Const" Then
                                        If Reader.HasAttributes Then
                                            Name = Reader.GetAttribute("Name")
                                            Value = Reader.GetAttribute("Value")
                                            If Not String.IsNullOrEmpty(Value) AndAlso Value.Chars(0) = "H" Then Value = "&" & Value
                                            Select Case Name
                                                Case "LatestVersionUrl"
                                                    LatestVersionUrl = Value
                                                Case "MusicalNotesOnAlarm"
                                                    MusicalNotesOnAlarm = System.Boolean.Parse(Value)
                                                Case "EatFromFloor"
                                                    EatFromFloor = System.Boolean.Parse(Value)
                                                Case "EatFromFloorFirst"
                                                    EatFromFloorFirst = System.Boolean.Parse(Value)
                                                Case "EatFromFloorMaxDistance"
                                                    EatFromFloorMaxDistance = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "AutoStackerDelay"
                                                    StackerDelay = CInt(Value)
                                                Case "ModifyMOTD"
                                                    ModifyMOTD = System.Boolean.Parse(Value)
                                                Case "RSAKeyOpenTibia"
                                                    RSAKeyOpenTibia = Value
                                                Case "LootEatFromCorpse"
                                                    LootEatFromCorpse = System.Boolean.Parse(Value)
                                                Case "AutoPickUpDelay"
                                                    AutoPickUpDelay = CInt(Value)
                                                Case "HotkeysCanEquipItems"
                                                    HotkeysCanEquipItems = System.Boolean.Parse(Value)
                                                Case "EquipItemsOnUse"
                                                    EquipItemsOnUse = System.Boolean.Parse(Value)
                                                Case "HealersCheckInterval"
                                                    HealersCheckInterval = CInt(Value)
                                                Case "HealersAfterHealDelay"
                                                    HealersAfterHealDelay = CInt(Value)
                                                Case "AutoPublishLocation"
                                                    AutoPublishLocation = System.Boolean.Parse(Value)
                                                Case "AutoPublishLocationInterval"
                                                    AutoPublishLocationInterval = CInt(Value)
                                                    If AutoPublishLocationInterval < 300000 Then AutoPublishLocationInterval = 300000
                                                Case "ShowInvisibleCreatures"
                                                    ShowInvisibleCreatures = System.Boolean.Parse(Value)
                                                Case "FPSWhenActive"
                                                    FPSWhenActive = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "FPSWhenInactive"
                                                    FPSWhenInactive = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "FPSWhenHidden"
                                                    FPSWhenHidden = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "FPSWhenMinimized"
                                                    FPSWhenMinimized = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "FlashTaskbarWhenAlarmFires"
                                                    FlashTaskbarWhenAlarmFires = System.Boolean.Parse(Value)
                                                Case "FlashTaskbarWhenMessaged"
                                                    FlashTaskbarWhenMessaged = System.Boolean.Parse(Value)
                                                Case "LootMaxDistance"
                                                    LootMaxDistance = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "CavebotAttackerShrinkRadius"
                                                    CavebotAttackerShrinkRadius = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "CreatureExpMultiplier"
                                                    CreatureExpMultiplier = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "ptrLastAttackedEntityID"
                                                    ptrLastAttackedEntityID = CInt(Value)
                                                Case "UnlimitedCapacity"
                                                    UnlimitedCapacity = System.Boolean.Parse(Value)
                                                Case "ptrInGame"
                                                    ptrInGame = CInt(Value)
                                                Case "ptrNameSpy"
                                                    ptrNameSpy = CInt(Value)
                                                Case "ptrNameSpy2"
                                                    ptrNameSpy2 = CInt(Value)
                                                Case "ptrFrameRateBegin"
                                                    ptrFrameRateBegin = CInt(Value)
                                                Case "ptrEnterOneNamePerLine"
                                                    ptrEnterOneNamePerLine = CInt(Value)
                                                Case "ptrHotkeyBegin"
                                                    ptrHotkeyBegin = CInt(Value)
                                                Case "ptrCharacterListBegin"
                                                    ptrCharacterListBegin = CInt(Value)
                                                Case "ptrCharacterSelectionIndex"
                                                    ptrCharacterSelectionIndex = CInt(Value)
                                                Case "ptrEncryptionKey"
                                                    ptrEncryptionKey = CInt(Value)
                                                Case "ptrRSAKey"
                                                    ptrRSAKey = CInt(Value)
                                                Case "ptrServerAddressBegin"
                                                    ptrServerAddressBegin = CInt(Value)
                                                Case "ptrServerPortBegin"
                                                    ptrServerPortBegin = CInt(Value)
                                                Case "ServerAddressDist"
                                                    ServerAddressDist = CInt(Value)
                                                Case "ServerAddressCount"
                                                    ServerAddressCount = CInt(Value)
                                                Case "ptrCharacterID"
                                                    ptrCharacterID = CInt(Value)
                                                Case "ptrBattleListBegin"
                                                    ptrBattleListBegin = CInt(Value)
                                                Case "ptrFirstContainer"
                                                    ptrFirstContainer = CInt(Value)
                                                Case "ptrInventoryBegin"
                                                    ptrInventoryBegin = CInt(Value)
                                                Case "ptrLevel"
                                                    ptrLevel = CInt(Value)
                                                Case "ptrExperience"
                                                    ptrExperience = CInt(Value)
                                                Case "ptrFollowedEntityID"
                                                    ptrFollowedEntityID = CInt(Value)
                                                Case "ptrAttackedEntityID"
                                                    ptrAttackedEntityID = CInt(Value)
                                                Case "ptrMaxHitPoints"
                                                    ptrMaxHitPoints = CInt(Value)
                                                Case "ptrHitPoints"
                                                    ptrHitPoints = CInt(Value)
                                                Case "ptrManaPoints"
                                                    ptrManaPoints = CInt(Value)
                                                Case "ptrSoulPoints"
                                                    ptrSoulPoints = CInt(Value)
                                                Case "ptrCapacity"
                                                    ptrCapacity = CInt(Value)
                                                Case "ptrCoordX"
                                                    ptrCoordX = CInt(Value)
                                                Case "ptrCoordY"
                                                    ptrCoordY = CInt(Value)
                                                Case "ptrCoordZ"
                                                    ptrCoordZ = CInt(Value)
                                                Case "ptrSecureMode"
                                                    ptrSecureMode = CInt(Value)
                                                Case "ptrFightingMode"
                                                    ptrFightingMode = CInt(Value)
                                                Case "ptrChasingMode"
                                                    ptrChasingMode = CInt(Value)
                                                Case "ptrMapPointer"
                                                    ptrMapPointer = CInt(Value)
                                                Case "ptrConditions"
                                                    ptrConditions = CInt(Value)
                                                Case "ptrVipListBegin"
                                                    ptrVipListBegin = CInt(Value)
                                                Case "ptrSkillsBegin"
                                                    ptrSkillsBegin = CInt(Value)
                                                Case "ptrSkillsPercentBegin"
                                                    ptrSkillsPercentBegin = CInt(Value)
                                                Case "ptrStamina"
                                                    ptrStamina = CInt(Value)
                                                Case "ptrGoToX"
                                                    ptrGoToX = CInt(Value)
                                                Case "ptrGoToY"
                                                    ptrGoToY = CInt(Value)
                                                Case "ptrGoToZ"
                                                    ptrGoToZ = CInt(Value)
                                                Case "ptrGo"
                                                    ptrGo = CInt(Value)
                                                Case "ptrWindowBegin"
                                                    ptrWindowBegin = CInt(Value)
                                                Case "ptrLevelPercent"
                                                    ptrLevelPercent = CInt(Value)
                                                Case "ptrMagicLevelPercent"
                                                    ptrMagicLevelPercent = CInt(Value)
                                                Case "CharacterListDist"
                                                    CharacterListDist = CInt(Value)
                                                Case "CharacterListWorldOffset"
                                                    CharacterListWorldOffset = CInt(Value)
                                                Case "WindowLeftOffset"
                                                    WindowLeftOffset = CInt(Value)
                                                Case "WindowTopOffset"
                                                    WindowTopOffset = CInt(Value)
                                                Case "WindowWidthOffset"
                                                    WindowWidthOffset = CInt(Value)
                                                Case "WindowHeightOffset"
                                                    WindowHeightOffset = CInt(Value)
                                                Case "WindowButtonPressedOffset"
                                                    WindowButtonPressedOffset = CInt(Value)
                                                Case "WindowButton1Offset"
                                                    WindowButton1Offset = CInt(Value)
                                                Case "WindowButton2Offset"
                                                    WindowButton2Offset = CInt(Value)
                                                Case "WindowButton3Offset"
                                                    WindowButton3Offset = CInt(Value)
                                                Case "WindowButton4Offset"
                                                    WindowButton4Offset = CInt(Value)
                                                Case "WindowCaptionOffset"
                                                    WindowCaptionOffset = CInt(Value)
                                                Case "BLMax"
                                                    BLMax = CInt(Value)
                                                Case "BLDist"
                                                    BLDist = CInt(Value)
                                                Case "BLNameOffset"
                                                    BLNameOffset = CInt(Value)
                                                Case "BLCoordXOffset"
                                                    BLCoordXOffset = CInt(Value)
                                                Case "BLCoordYOffset"
                                                    BLCoordYOffset = CInt(Value)
                                                Case "BLCoordZOffset"
                                                    BLCoordZOffset = CInt(Value)
                                                Case "BLDirectionOffset"
                                                    BLDirectionOffset = CInt(Value)
                                                Case "BLWalkingOffset"
                                                    BLWalkingOffset = CInt(Value)
                                                Case "BLOutfitOffset"
                                                    BLOutfitOffset = CInt(Value)
                                                Case "BLHeadCOffset"
                                                    BLHeadCOffset = CInt(Value)
                                                Case "BLBodyCOffset"
                                                    BLBodyCOffset = CInt(Value)
                                                Case "BLLegsCOffset"
                                                    BLLegsCOffset = CInt(Value)
                                                Case "BLFeetCOffset"
                                                    BLFeetCOffset = CInt(Value)
                                                Case "BLAddonsOffset"
                                                    BLAddonsOffset = CInt(Value)
                                                Case "BLLightIntensityOffset"
                                                    BLLightIntensityOffset = CInt(Value)
                                                Case "BLLightColorOffset"
                                                    BLLightColorOffset = CInt(Value)
                                                Case "BLHPPercentOffset"
                                                    BLHPPercentOffset = CInt(Value)
                                                Case "BLSpeedOffset"
                                                    BLSpeedOffset = CInt(Value)
                                                Case "BLOnScreenOffset"
                                                    BLOnScreenOffset = CInt(Value)
                                                Case "BLSkullOffset"
                                                    BLSkullOffset = CInt(Value)
                                                Case "BLPartyOffset"
                                                    BLPartyOffset = CInt(Value)
                                                Case "MaxContainers"
                                                    MaxContainers = CInt(Value)
                                                Case "ContainerDist"
                                                    ContainerDist = CUShort(Value)
                                                Case "ContainerNameOffset"
                                                    ContainerNameOffset = CInt(Value)
                                                Case "ContainerSizeOffset"
                                                    ContainerSizeOffset = CInt(Value)
                                                Case "ContainerItemCountOffset"
                                                    ContainerItemCountOffset = CInt(Value)
                                                Case "ContainerFirstItemOffset"
                                                    ContainerFirstItemOffset = CInt(Value)
                                                Case "ContainerIDOffset"
                                                    ContainerIDOffset = CInt(Value)
                                                Case "ContainerHasParentOffset"
                                                    ContainerHasParentOffset = CInt(Value)
                                                Case "ItemDist"
                                                    ItemDist = CInt(Value)
                                                Case "ItemCountOffset"
                                                    ItemCountOffset = CInt(Value)
                                                Case "MapObjectIdOffset"
                                                    MapObjectIdOffset = CInt(Value)
                                                Case "MapObjectDataOffset"
                                                    MapObjectDataOffset = CInt(Value)
                                                Case "MapObjectExtraDataOffset"
                                                    MapObjectExtraDataOffset = CInt(Value)
                                                Case "MapObjectDist"
                                                    MapObjectDist = CInt(Value)
                                                Case "MapTileDist"
                                                    MapTileDist = CInt(Value)
                                                Case "DebugOnLog"
                                                    DebugOnLog = System.Boolean.Parse(Value)
                                                Case "ClearLogOnStartup"
                                                    If (System.Boolean.Parse(Value)) Then
                                                        If IO.File.Exists("Log.txt") Then
                                                            IO.File.Delete("Log.txt")
                                                        End If
                                                    End If
                                                Case "StatsUploaderUrl"
                                                    StatsUploaderUrl = Value
                                                Case "StatsUploaderUserID"
                                                    StatsUploaderUserID = Value
                                                Case "StatsUploaderPassword"
                                                    StatsUploaderPassword = Value
                                                Case "StatsUploaderFrequency"
                                                    StatsUploaderFrequency = CUInt(Value) ', NumberStyles.HexNumber, CI)
                                                Case "StatsUploaderPath"
                                                    StatsUploaderPath = Value
                                                Case "StatsUploaderFilename"
                                                    StatsUploaderFilename = Value
                                                Case "StatsUploaderSaveOnDiskOnly"
                                                    StatsUploaderSaveOnDiskOnly = System.Boolean.Parse(Value)
                                                Case "VipMax"
                                                    VipMax = CInt(Value)
                                                Case "VipNameOffset"
                                                    VipNameOffset = CInt(Value)
                                                Case "VipStatusOffset"
                                                    VipStatusOffset = CInt(Value)
                                                Case "VipDist"
                                                    VipDist = CInt(Value)
                                                Case "SkillsDist"
                                                    SkillsDist = CInt(Value)
                                                Case "LootDelay"
                                                    LootDelay = CInt(Value)
                                                Case "LootInBag"
                                                    LootInBag = System.Boolean.Parse(Value)
                                                Case "LootInBagDelay"
                                                    LootInBagDelay = CInt(Value)
                                                Case "HotkeyMax"
                                                    HotkeyMax = CInt(Value)
                                                Case "HotkeyItemDataOffset"
                                                    HotkeyItemDataOffset = CInt(Value)
                                                Case "HotkeyItemOffset"
                                                    HotkeyItemOffset = CInt(Value)
                                                Case "HotkeyItemDist"
                                                    HotkeyItemDist = CInt(Value)
                                                Case "HotkeyTextAutoSendOffset"
                                                    HotkeyTextAutoSendOffset = CInt(Value)
                                                Case "HotkeyTextAutoSendDist"
                                                    HotkeyTextAutoSendDist = CInt(Value)
                                                Case "HotkeyTextOffset"
                                                    HotkeyTextOffset = CInt(Value)
                                                Case "HotkeyTextDist"
                                                    HotkeyTextDist = CInt(Value)
                                                Case "FrameRateCurrentOffset"
                                                    FrameRateCurrentOffset = CInt(Value)
                                                Case "FrameRateLimitOffset"
                                                    FrameRateLimitOffset = CInt(Value)
                                                Case "NameSpyDefault"
                                                    NameSpyDefault = CInt(Value)
                                                Case "NameSpy2Default"
                                                    NameSpy2Default = CInt(Value)
                                                Case "LootWithCavebot"
                                                    LootWithCavebot = System.Boolean.Parse(Value)
                                                Case "CavebotLootMinCap"
                                                    CavebotLootMinCap = CInt(Value)
                                                Case "ptrStatusMessage"
                                                    ptrStatusMessage = CInt(Value)
                                                Case "ptrStatusMessageTimer"
                                                    ptrStatusMessageTimer = CInt(Value)
                                                Case "WaypointMaxDistance"
                                                    WaypointMaxDistance = CInt(Value)
                                                Case "CavebotAttackerRadius"
                                                    CavebotAttackerRadius = System.Double.Parse(Value, NumberStyles.Number, CI)
                                                Case "ptrScreenTextStart"
                                                    ptrScreenTextStart = CInt(Value)
                                                Case "ptrScreenTextShow"
                                                    ptrScreenTextShow = CInt(Value)
                                                Case "ptrScreenTextTimer"
                                                    ptrScreenTextTimer = CInt(Value)
                                                Case "ptrScreenTextColor"
                                                    ptrScreenTextColor = CInt(Value)
                                            End Select
                                        End If
                                    ElseIf Reader.NodeType = XmlNodeType.EndElement AndAlso Reader.Name = "Constants" Then
                                        Exit While
                                    End If
                                End While
                        End Select
                    End If
                End While
                Reader.Close()
            Catch Ex As Exception
                MessageBox.Show("Failed to load Constants.xml properly. Error Message: " & Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End
            End Try
        End Sub

    End Class

    Public Const BotName As String = "TibiaTek Bot Beta"
    Public Const BotShortName As String = "TTB Beta"
    Public Const BotWebsite As String = "http://www.tibiatekbot.com/"
    Public Const BotForum As String = "http://www.tibiatekbot.com/forums/"
    Public Const ConsoleChannelID As Integer = &H64
    Public Const ConsoleLevel As Integer = &H539
    Public Const ConsoleName As String = BotName
    Public Const Ret As Char = vbCrLf

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure Test
        Dim Number1 As Integer
        Dim Number2 As Integer
        <MarshalAs(UnmanagedType.ByValArray, ArraySubType:=UnmanagedType.U1, SizeConst:=10)> _
        Dim Name() As Byte
        Dim Number3 As Integer
    End Structure

    Public Enum MenuMode
        FloatingWindow
        InsideWindow
        InsideMenu
    End Enum

    Public Enum SMTO As Integer
        Normal = 0
        Block = 1
        AbortIfHung = 2
        NoTimeoutIfNotHung = 8
    End Enum


    Public Enum InjectionState As Byte
        Uninjected
        Injected
        Failed
    End Enum

    Public Enum WM
        Begin = &H4C7 'DO NOT USE
        Recv
        Send
        Injected
        Hooked
        Unhooked
        Uninjected
        Uninject
        WASD
        Menu
        ShowMenu
        Ping
        Test
        TextMenu
        TextMenuMove
        [End] 'DO NOT USE
    End Enum

    Public Enum CodeCave
        TibiaHandleOffset = 0
        TTBHandleOffset = 4
    End Enum

    Public Enum MenuType
        Menu = &H401
        Options
        About
        SwitchMenu
        Test
    End Enum

    Public Enum MessageType
        Normal = 1
        Whisper = 2
        Yell = 3
        PM = 4
        PMGM = &HB
        Channel = 5
        Broadcast = 9
        ChannelGM = &HA
        ChannelTutor = &HC
        ChannelCounsellor = &HE
        MonsterSay = &H10
        MonsterYell = &H11
    End Enum

    Public Enum SysMessageType
        StatusWarning = &H12
        EventAdvance
        EventDefault
        StatusDefault
        Information
        StatusSmall
        StatusConsoleBlue
        StatusConsoleRed
    End Enum

    Public Enum ChannelType
        Personal = &HFFFF
        GuildChat = 0
        GameChat = 4
        Trade
        RLChat
        Help
        Console = &H64
    End Enum

    Public Enum SpecialEntity
        Myself
        Attacked
        Followed
    End Enum

    <Flags()> Public Enum Addons
        None = 0
        First = 1
        Second = 2
        Full = 3
    End Enum

    Public Enum PartyStatus
        None
        Unknown
        Invited
        Member
        Leader
    End Enum

    Public Enum SkullMark
        None
        Yellow
        Green
        White
        Red
    End Enum

    Public Enum WindowState As Integer
        Active
        Minimized
        Inactive
        Hidden
    End Enum

    Public Enum BotState As Integer
        Stopped
        Paused
        Running
    End Enum

    <Flags()> Public Enum Conditions As Integer
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
        All = Poison Or Burnt Or Electrified Or Beer Or MagicShield Or Paralized Or Haste Or CombatSign Or Drowning
    End Enum

    Public Enum LightColor
        Darkness = 0
        BrightSword = &H8F
        UtevoLux = &HD7
        Torch = &HCE
        LightWand = &HD1
    End Enum

    Public Enum LightIntensity
        None = 0
        VerySmall = 2
        Small = 4
        Medium = 6
        Large = 8
        VeryLarge = 10
        Huge = 12
        Inmense = 14
    End Enum

    <Flags()> Public Enum ItemKind
        Unknown = &H0 'void? xd, any item that doesnt have any category
        Equipment = &H1
        Helmet = &H2
        Armor = &H4
        Leg = &H8
        Footwear = &H10
        Shield = &H20
        SingleHandedWeapon = &H40 'for rods and wands too
        DoubleHandedWeapon = &H80 'for bows/xbows too
        Ammunition = &H100 'arros & bolts
        Throwable = &H200 'spears, snowballs, throwing stars, etc
        Tool = &H400 'rope, shovel, light shovel, pick, etc
        Valuable = &H800 'gems, creature products, books, etc
        Ring = &H1000
        Neck = &H2000 'amulets and necklaces, scarf
        Container = &H4000 'bags, backpacks, depots, wardrobes, all those
        Food = &H8000
        FluidContainer = &H10000
        LightSource = &H20000 'torch, ???
        MagicField = &H80000 'fire field, poison field, purple field, smoke field...etc
        Door = &H100000 'closed doors, open doors
        Special = &H200000 'mailboxes, switches?
        RopeSpot = &H400000
        Teleport = &H800000 'ramps, stairs, teleports
        UsableTeleport = &H1000000 'ladders
        UsableTeleport2 = &H2000000 'sewers
        BlockedTeleport = &H4000000 'hole covered with rocks...
        Blocking = &H8000000 'blocks the path, but you can walk over them
        FullBlocking = &H10000000 'won't let you walk over them at all
        Rune = &H20000000
    End Enum

    Public Enum InventorySlots
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

    Public Enum Skills
        FistFighting
        ClubFighting
        SwordFighting
        AxeFighting
        DistanceFighting
        Shielding
        Fishing
    End Enum

    Public Enum ShowTextColors
        Yellow = 1
        LightBlue = 4
        White = 6
        Red = 9
        Orange = 12
        Green = 22
        DarkBlue = 24
    End Enum

    Public Enum HealTypes
        None
        UltimateHealingRune
        ExuraSio
        Both
    End Enum

    Public Enum Fluids As ULong
        'Another ones
        Water = &H1
        Mana = &HA
        ManaOpenTibia = &H2
    End Enum
End Module
