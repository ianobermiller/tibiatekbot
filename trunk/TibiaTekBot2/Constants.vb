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

Imports System.xml, TibiaTekBot.DatReader, TibiaTekBot.Constants, System.Globalization

Public Module ConstantsModule

    Public Definitions As Items
    Public Consts As Constants

    Public Class Constants
        Public LatestVersionUrl As String = "http://www.tibiatek.com/version.php"
        Public MusicalNotesOnAlarm As Boolean = True
        Public EatFromFloor As Boolean = True
        Public EatFromFloorFirst As Boolean = True
        Public EatFromFloorMaxDistance As Double = 1.5
        Public AutoEaterInterval As Integer = 30000
        Public AutoEaterSmartInterval As Integer = 60000
        Public AutoStackerDelay As Integer = 1000
        Public ModifyMOTD As Boolean = True
        Public RSAKeyOpenTibia As String = "109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413"
        Public AutoPickUpDelay As Integer = 2000
        Public HealersCheckInterval As Integer = 300
        Public HealersAfterHealDelay As Integer = 300
        Public HotkeysCanEquipItems As Boolean = True
        Public EquipItemsOnUse As Boolean = True
        Public AutoPublishLocation As Boolean = False
        Public AutoPublishLocationInterval As Integer = 600000
        Public ShowInvisibleCreatures As Boolean = True
        Public FPSWhenActive As Double = 10
        Public FPSWhenInactive As Double = 1
        Public FPSWhenHidden As Double = 1
        Public FPSWhenMinimized As Double = 1
        Public FlashTaskbarWhenAlarmFires As Boolean = True
        Public FlashTaskbarWhenMessaged As Boolean = False
        Public LootEatFromCorpse As Boolean = True
        Public LootMaxDistance As Double = 2.9
        Public WaypointMaxDistance As Integer = 100
        Public CreatureExpMultiplier As Double = 1.0
        Public AutoOpenBackpack As Boolean = True

        Public ptrCharacterListBegin As Integer = &H766DBC
        Public CharacterListDist As Integer = &H54
        Public CharacterListWorldOffset As Integer = 30
        Public ptrInGame As Integer = &H766DF8
        Public ptrFrameRateBegin As Integer = &H76793C
        Public ptrEnterOneNamePerLine As Integer = &H594670
        Public ptrCharacterSelectionIndex As Integer = &H766DB8
        Public ptrEncryptionKey As Integer = &H7637AC
        Public ptrRSAKey As Integer = &H593610
        Public ptrServerAddressBegin As Integer = &H75EAE8
        Public ptrServerPortBegin As Integer = &H75EB4C
        Public ServerAddressDist As Integer = &H70
        Public ServerAddressCount As Integer = &HA
        Public ptrCharacterID As Integer = &H60EAD0
        Public ptrBattleListBegin As Integer = &H60EB30
        Public ptrFirstContainer As Integer = &H617000
        Public ptrInventoryBegin As Integer = &H616F88
        Public ptrLevel As Integer = &H60EAC0
        Public ptrExperience As Integer = &H60EAC4
        Public ptrFollowedEntityID As Integer = &H60EA98
        Public ptrAttackedEntityID As Integer = &H60EA9C
        Public ptrLastAttackedEntityID As Integer = &H76DA10
        Public ptrMaxHitPoints As Integer = &H60EAC8
        Public ptrHitPoints As Integer = &H60EACC
        Public ptrManaPoints As Integer = &H60EAB0
        Public ptrMaxManaPoints As Integer = &H60EAAC
        Public ptrSoulPoints As Integer = &H60EAA8
        Public ptrCapacity As Integer = &H60EAA0
        Public ptrCoordX As Integer = &H6198F8
        Public ptrCoordY As Integer = &H6198F4
        Public ptrCoordZ As Integer = &H6198F0
        Public ptrSecureMode As Integer = &H763BCC
        Public ptrFightingMode As Integer = &H763BD4
        Public ptrChasingMode As Integer = &H763BD0
        Public ptrMapPointer As Integer = &H61E408
        Public ptrConditions As Integer = &H60EA58
        Public ptrVipListBegin As Integer = &H60C7F0
        Public ptrSkillsBegin As Integer = &H60EA78
        Public ptrSkillsPercentBegin As Integer = &H60EA5C
        Public ptrStamina As Integer = &H60EAA4
        Public ptrGoToX As Integer = &H60EB14
        Public ptrGoToY As Integer = &H60EB10
        Public ptrGoToZ As Integer = &H60EB0C
        Public ptrGo As Integer = 0
        Public ptrHotkeyBegin As Integer = &H763C18
        Public ptrWindowBegin As Integer = &H6198B4
        Public ptrLevelPercent As Integer = &H60EAB8
        Public ptrMagicLevel As Integer = &H60EABC
        Public ptrMagicLevelPercent As Integer = &H60EAB4

        Public WindowLeftOffset As Integer = &H14
        Public WindowTopOffset As Integer = &H18
        Public WindowWidthOffset As Integer = &H1C
        Public WindowHeightOffset As Integer = &H20
        Public WindowButtonPressedOffset As Integer = &H28
        Public WindowButton1Offset As Integer = &H2C
        Public WindowButton2Offset As Integer = &H2C
        Public WindowButton3Offset As Integer = &H30
        Public WindowButton4Offset As Integer = &H38
        Public WindowCaptionOffset As Integer = &H50
        Public UnlimitedCapacity As Boolean = False

        Public BLMax As Integer = &H96
        Public BLDist As Integer = &HA0
        Public BLNameOffset As Integer = &H4
        Public BLCoordXOffset As Integer = &H24
        Public BLCoordYOffset As Integer = &H28
        Public BLCoordZOffset As Integer = &H2C
        Public BLDirectionOffset As Integer = &H50
        Public BLWalkingOffset As Integer = &H4C
        Public BLOutfitOffset As Integer = &H60
        Public BLHeadCOffset As Integer = &H64
        Public BLBodyCOffset As Integer = &H68
        Public BLLegsCOffset As Integer = &H6C
        Public BLFeetCOffset As Integer = &H70
        Public BLAddonsOffset As Integer = &H74
        Public BLLightIntensityOffset As Integer = &H78
        Public BLLightColorOffset As Integer = &H7C
        Public BLHPPercentOffset As Integer = &H88
        Public BLSpeedOffset As Integer = &H8C
        Public BLOnScreenOffset As Integer = &H90
        Public BLSkullOffset As Integer = &H94
        Public BLPartyOffset As Integer = &H98

        Public MaxContainers As Integer = &H10
        Public ContainerDist As UShort = &H1EC
        Public ContainerNameOffset As Integer = &H10
        Public ContainerSizeOffset As Integer = &H30
        Public ContainerItemCountOffset As Integer = &H38
        Public ContainerFirstItemOffset As Integer = &H3C
        Public ContainerIDOffset As Integer = 4
        Public ContainerHasParentOffset As Integer = &H34

        Public ItemDist As Integer = &HC
        Public ItemCountOffset As Integer = 4

        Public MapTileIdOffset As Integer = 0
        Public MapObjectIdOffset As Integer = 4
        Public MapObjectDataOffset As Integer = 8
        Public MapObjectExtraDataOffset As Integer = &HC
        Public MapObjectDist As Integer = &HC
        Public MapTileDist As Integer = &HAC

        Public DebugOnLog As Boolean = False

        Public StatsUploaderUrl As String = "ftp.server.com"
        Public StatsUploaderUserID As String = "userid"
        Public StatsUploaderPassword As String = "password"
        Public StatsUploaderFrequency As Integer = 300000
        Public StatsUploaderPath As String = "/"
        Public StatsUploaderFilename As String = "stats.xml"
        Public StatsUploaderSaveOnDiskOnly As Boolean = False

        Public VipMax As Integer = &H2C
        Public VipNameOffset As Integer = 4
        Public VipStatusOffset As Integer = &H22
        Public VipDist As Integer = &H2C

        Public SkillsDist As Integer = 4

        Public LootDelay As Integer = 500
        Public LootInBag As Boolean = True
        Public LootInBagDelay As Integer = 500
        Public SmartAttacker As Boolean = True

        Public HotkeyMax As Integer = &H24
        Public HotkeyItemDataOffset As UShort = &H90
        Public HotkeyItemOffset As Integer = &H120
        Public HotkeyItemDist As Integer = 4
        Public HotkeyTextAutoSendOffset As Integer = &H1B0
        Public HotkeyTextAutoSendDist As Integer = 1
        Public HotkeyTextOffset As Integer = &H1D8
        Public HotkeyTextDist As Integer = &H100

        Public FrameRateCurrentOffset As Integer = &H60
        Public FrameRateLimitOffset As Integer = &H58

        Public ptrNameSpy As Integer = &H4DD2D7
        Public ptrNameSpy2 As Integer = &H4DD2E1

        Public NameSpyDefault As Integer = &H4C75
        Public NameSpy2Default As Integer = &H4275
        Public CavebotAttackerShrinkRadius As Double = 1.0
        Public CavebotAttackerRadius As Double = 5.0
        Public LootWithCavebot As Boolean = True
        Public CavebotLootMinCap As Integer = 0

        Public ptrStatusMessage As Integer = &H768458
        Public ptrStatusMessageTimer As Integer = &H768454

        Public MCPatchOffset As Integer = &HF6224
        Public MCPatchReplacement As Integer = &HEB
        Public MCPatchOriginal As Integer = &H75

        Public SpellCasterInterval As Integer = 1000
        Public SpellCasterDelay As Integer = 4000

        Public IRCConnectOnStartUp As Boolean = True
        Public IRCJoinAfterConnected As Boolean = True
        Public IRCNickname As String = String.Empty
        Public IRCPassword As String = String.Empty

        Public AntiLogoutInterval As Integer = 30000

        Public TTMessagesEnabled As Boolean = True
        Public TTMessagesInterval As Integer = 3600000

        Public ptrForYourInformation As Integer = &H595968

        Public Sub New()
            LoadConstants()
        End Sub

        Public Sub LoadConstants()
            Dim Reader As System.Xml.XmlTextReader
            Dim CI As CultureInfo = New CultureInfo("en-US", False)
            Try
                Reader = New System.Xml.XmlTextReader(GetConfigurationDirectory() + "\Constants.xml")
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
                                                    AutoStackerDelay = CInt(Value)
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
                                                Case "ptrCharacterListBegin"
                                                    ptrCharacterListBegin = CInt(Value)
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
                                                Case "ptrMaxManaPoints"
                                                    ptrMaxManaPoints = CInt(Value)
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
                                                Case "ptrMagicLevel"
                                                    ptrMagicLevel = CInt(Value)
                                                Case "ptrMagicLevelPercent"
                                                    ptrMagicLevelPercent = CInt(Value)
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
                                                Case "SmartAttacker"
                                                    SmartAttacker = System.Boolean.Parse(Value)
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
                                                Case "MCPatchOffset"
                                                    MCPatchOffset = CInt(Value)
                                                Case "MCPatchReplacement"
                                                    MCPatchReplacement = CInt(Value)
                                                Case "MCPatchOriginal"
                                                    MCPatchOriginal = CInt(Value)

                                                Case "SpellCasterInterval"
                                                    SpellCasterInterval = CInt(Value)
                                                Case "SpellCasterDelay"
                                                    SpellCasterDelay = CInt(Value)
                                                Case "AutoEaterInterval"
                                                    AutoEaterInterval = CInt(Value)
                                                Case "AutoEaterSmartInterval"
                                                    AutoEaterSmartInterval = CInt(Value)
                                                Case "IRCConnectOnStartUp"
                                                    IRCConnectOnStartUp = System.Boolean.Parse(Value)
                                                Case "IRCJoinAfterConnected"
                                                    IRCJoinAfterConnected = System.Boolean.Parse(Value)
                                                Case "IRCNickname"
                                                    IRCNickname = Value
                                                Case "IRCPassword"
                                                    IRCPassword = Value

                                                Case "AntiLogoutInterval"
                                                    AntiLogoutInterval = CInt(Value)
                                                Case "AutoOpenBackpack"
                                                    AutoOpenBackpack = System.Boolean.Parse(Value)
                                                Case "CharacterListDist"
                                                    CharacterListDist = CInt(Value)
                                                Case "CharacterListWorldOffset"
                                                    CharacterListWorldOffset = CInt(Value)

                                                Case "TTMessagesEnabled"
                                                    TTMessagesEnabled = System.Boolean.Parse(Value)
                                                Case "TTMessagesInterval"
                                                    TTMessagesInterval = CInt(Value)
                                                Case "ptrForYourInformation"
                                                    ptrForYourInformation = CInt(Value)
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

    Public Const Ret As Char = Chr(&HA)
    Public Const BotVersion As String = "2.2.3"
    Public Const TibiaFileVersion As String = "8.10"
    Public Const TibiaProductName As String = "Tibia Player"
    Public Const BotStage As String = "Final" 'Alpha,Beta,Final,etc
    Public Const BotName As String = "TibiaTek Bot"
    Public Const BotWebsite As String = "http://www.tibiatek.com"
    Public Const BotMOTD As String = Ret & "Welcome to TibiaTek Bot " & BotVersion & Ret & _
        "Have a great day!" & Ret & _
        "Dont forget to visit us at:" & Ret & _
        "http://www.tibiatek.com/ and " & Ret & _
        "http://www.xcreations.net/~tpforums/forum/"
    Public Const IRCServer As String = "pacifica.dairc.us"
    Public Const IRCPort As Integer = 6669
    Public Const IRCChannel As String = "#TibiaTekBot"
    Public Const IRCClientVersion As String = "TibiaTek Integrated IRC Client (http://www.tibiatek.com/)"
    Public Const ConsoleChannelID As Integer = &H64
    Public Const ConsoleLevel As Integer = &H65
    Public Const ConsoleName As String = BotName
    Public Const Debug As Boolean = False
    Public Const GNUGPLStatement As String = "    Copyright (C) 2007 TibiaTek Development Team" & vbCrLf & _
    "    This file is part of TibiaTek Bot." & vbCrLf & vbCrLf & _
    "    TibiaTek Bot is free software: you can redistribute it and/or modify" & vbCrLf & _
    "    it under the terms of the GNU General Public License as published by" & vbCrLf & _
    "    the Free Software Foundation, either version 3 of the License, or" & vbCrLf & _
    "    (at your option) any later version." & vbCrLf & vbCrLf & _
    "    TibiaTek Bot is distributed in the hope that it will be useful," & vbCrLf & _
    "    but WITHOUT ANY WARRANTY; without even the implied warranty of" & vbCrLf & _
    "    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the" & vbCrLf & _
    "    GNU General Public License for more details." & vbCrLf & vbCrLf & _
    "    You should have received a copy of the GNU General Public License" & vbCrLf & _
    "    along with TibiaTek Bot. If not, see http://www.gnu.org/licenses/gpl.txt" & vbCrLf & _
    "    or write to the Free Software Foundation, 59 Temple Place - Suite 330," & vbCrLf & _
    "    Boston, MA 02111-1307, USA."

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

    Public Enum ThreadTimerState
        Stopped
        Running
    End Enum

    Public Enum ItemSearchArgs
        Body = 1
        Backpack = 2
        BodyBackpack = 3
    End Enum

    Public Enum SpecialEntity
        Myself
        Attacked
        Followed
    End Enum

    Public Enum HealTypes
        None
        UltimateHealingRune
        ExuraSio
        Both
    End Enum

    Public Enum SkullMark
        None
        Yellow
        Green
        White
        Red
    End Enum

    Public Enum HotkeyUseMode
        WithCrosshair = 0
        OnTarget = 1
        OnYourself = 2
    End Enum

    Public Enum HotkeyType
        None = 0
        Item = 1
        Text = 2
    End Enum

    Public Enum PartyStatus
        None
        Unknown
        Invited
        Member
        Leader
    End Enum

    Public Enum ChannelType As Integer
        Personal = &HFFFF
        GuildChat = 0
        GameChat = 4
        Trade
        RLChat
        Help
        Console = ConsoleChannelID
        IRCChannel
    End Enum

    <Flags()> Public Enum Conditions
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

	Public Enum TextColors
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

    Public Enum MagicEffects
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

    Public Enum Skills
        FistFighting = 0
        ClubFighting
        SwordFighting
        AxeFighting
        DistanceFighting
        Shielding
        Fishing
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

    <Flags()> Public Enum Addons
        None = 0
        First = 1
        Second = 2
        Both = 3
    End Enum

    Public Enum HotkeyCombination
        F1 = 0
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
        SF1
        SF2
        SF3
        SF4
        SF5
        SF6
        SF7
        SF8
        SF9
        SF10
        SF11
        SF12
        CF1
        CF2
        CF3
        CF4
        CF5
        CF6
        CF7
        CF8
        CF9
        CF10
        CF11
        CF12
    End Enum

    Public Enum WindowState As Integer
        Active
        Minimized
        Inactive
        Hidden
    End Enum

    Public Enum Fluids As ULong
        'Another ones
        Water = &H1
        Mana = &HA
        ManaOpenTibia = &H2
    End Enum

    Public Enum CavebotState As Integer
        Walking
        Attacking
        OpeningBody
        Looting
        None
    End Enum

    Public Enum Directions As Integer
        Up = 0
        Right = 1
        Down = 2
        Left = 3
        UpRight = 4
        DownRight = 5
        DownLeft = 6
        UpLeft = 7
    End Enum

    Public Enum SpellKind As Integer
        Rune
        Food
        Ammunition
        Support
        Offensive
        Healing
        Incantation
    End Enum

End Module
