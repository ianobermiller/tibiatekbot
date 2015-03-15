# Introduction #

Constants editor lets you modify the Constants.xml file which is located at TTB\_DIR\Config\Constants.xml. You can reach the Constants Editor from Tray Icon -> Constants Editor.

# Content #

Constants.xml file contains every data that [Bot](TibiaTek.md) uses. You can find the very memory addresses, **but what's more important: you can find the options of the bot.** In case you want to use FPS Changer you can define the FPS for the changer. These are the default values for the FPS changer:
```
FPSWhenActive: 100
FPSWhenInactive: 1
FPSWhenMinimized: 1
FPSWhenHidden: 1
```

As you see the names of the options are pretty self-explanatory.

# How to Use #

Open Constants Editor and you'll see two columns. One with the names and another with the values. Double click a value to change it.
**IMPORTANT:** If the value is a number, remember to add a decimal point and a zero even if  the value is a whole number (e.g use 2.0 instead of 2).
When the value is a Boolean: True or False (On or Off respectively). Don't put anything else on these values.

**If you aren't 100% sure of what you are doing make a backup copy of the Constants.xml file first!**

# Constants and Explanations #

|**Name**|**Default Value**|**Description**|
|:-------|:----------------|:--------------|
|IRCConnectOnStartUp|True|Enables automatic connection to the IRC server irc.quakenet.org after character login. |
|IRCJoinAfterConnected|True|Enables automatic joining to the IRC channel #tibiatekbot at irc.quakenet.org after connecting to this server. |
|TTMessagesEnabled|True|Allow TTB to poll the TibiaTek database for any feedback responses from the TibiaTek Development Team.|
|TTMessagesInterval|3600000|Interval at which the bot polls for feedback responses.|
|IRCNickname|  |Sets the default nickname for the IRC Channel.|
|IRCPassword|  |Sets the default password for the IRC Nickname.|
|LatestVersionUrl|http://www.tibiatek.com/version.php|Location where TibiaTek bot checks if it's up to date, do not change this.|
|AutoPublishLocation|False|Option for &sendlocation command, enables automatic publishing of the character's location. |
|AutoPublishLocationInterval|600000|Option for &sendlocation command, time interval between publishing of the character's location.|
|CreatureExpMultiplier|1.0|Used in &exp creature on|off command. Change this when using bot with Private Server what has different experience rate.|
|ShowInvisibleCreatures|True|Show invisible creatures as white druids automatically.|
|MusicalNotesOnAlarm|True|When alarming show musical note animation top of you character.|
|DebugOnLog|False|Used for debugging.|
|ClearLogOnStartup|True|Used for debugging|
|LootWithCavebot|True|Option for &cavebot command. You can determine if you want to  loot with the cavebot. You can also define this value from the Cavebot window.|
|CavebotLootMinCap|0 |Option for &cavebot command. Tells the Cavebot when it's time to stop looting. You can also define it from the Cavebot window.|
|CavebotAttackerRadius|5.0|Option for &cavebot and &attacker commands. Determines the distance to attack between the cavebot/attacker and the creature. You may want to change this if you are hunting in open areas or a narrow cave.|
|CavebotAttackerShrinkRadius|1.0|Used by the cavebot to calculate other players' distance from creatures. Makes the cavebot ignore any monsters found within other players' specific radii. This is because players usually avoid attacking a creature if another player closer to it than you are.|
|WaypointMaxDistance|100|Option to set the waypoints max distance|
|LootDelay|500|Option for &loot command. Delay at which the Auto Looter will start gathering items. The value is in milliseconds.|
|LootInBag|True|Option for &loot command. Determines if the Auto Looter opens bags inside monsters to loot them.|
|LootInBagDelay|500|Option for &loot command. Delay at which the Auto Looter will start gathering items from the bags. The value is in milliseconds.|
|LootEatFromCorpse|True|Option for &loot command. Determines if the Auto Looter eats the food inside corpses.|
|LootMaxDistance|2.9|Option for &loot command. Determines the max distance for the Auto Looter to walk and open the corpse. If you want it too loot just the tiles around you, use the value 1.5.|
|AutoOpenBackpack|True|Automatically open your backpack after logging in.|
|EatFromFloor|True|Option for &eat command. Determines if Auto Eater eats from floor too.|
|EatFromFloorFirst|True| Option for &eat command. Determines if the Auto Eater eats first from the floor, and then from backpack.|
|EatFromFloorMaxDistance|1.5|Option for &eat command. Determines the maximum distance for eating the food from the floor.|
|AutoEaterInterval|30000| Option for &eat command. Determines the default interval at which it should try to eat.|
|AutoEaterSmartInterval|"60000|Option for &eat command. Determines the interval at which it should try to eat when the smart option is enabled.|
|AutoPickUpDelay|2000|Option for &pickup command. Determines the delay for the Auto Pickup to pickup the item. The value is in milliseconds.|
|HealersCheckInterval|300|Option for &heal command. Determines the delay for Auto Healer to check your heal. The value is in milliseconds.|
|HealersAfterHealDelay|300|Option for &heal command. Determines the delay after healing. Value is in milliseconds.|
|SpellCasterInterval|1000|Option for &spell command. Determines the interval at which it checks your minimum mana points to decide whether to cast the spell or not.|
|SpellCasterDelay|4000|Option for &spell command. Determines the delay after casting.|
|HotkeysCanEquipItems|True|Determines can you bind hotkeys to equip items.|
|EquipItemsOnUse|True|Determines can you equip items on use.|
|AutoStackerDelay|1000|Option for &stacker command. Determines the delay for the Auto Stacker. It stacks one item at a time. The value is in milliseconds.|
|ModifyMOTD|True|Use modified MOTD when connecting.|
|SmartAttacker|True|If enabled, auto attacker will attack to the closest enemy.|
|FPSWhenActive|10|Determines the Frame Per Second when the Tibia Window is active.|
|FPSWhenInactive|1 |Determines the Frame Per Second when the Tibia Window is inactive.|
|FPSWhenMinimized|1 |Determines the Frame Per Second when the Tibia Window is minimized.|
|FPSWhenHidden|1 |Determines the Frame Per Second when the Tibia Window is hidden.|
|FlashTaskbarWhenMessaged|False|Determines if the taskbar flashes when a message (Public or Private) arrives.|
|FlashTaskbarWhenAlarmFires|True|Determines if the taskbar flashes when an alarm fires.|
|StatsUploaderSaveOnDiskOnly|False|Option for &uploadstats command. Determines if the stats are to be uploaded/saved only to your disk. Use this if you don't have FTP acces to any server.|
|StatsUploaderUrl|ftp.server.com|Determines the URL of the FTP server.|
|StatsUploaderPath|/ |Path where to upload the stats in the FTP server.|
|StatsUploaderFilename|stats.xml|Filename of the file to be uploaded.|
|StatsUploaderUserID|userid|Username used to connect to the FTP server.|
|StatsUploaderPassword|password|Password used to connect to the FTP server.|
|StatsUploaderFrequency|30000|Time interval for the Stats Uploader to upload yourstats.|
|AntiLogoutInterval|30000|Time interval for the Anti Logout feature to check how long you have been idle.|

## Memory Addresses And Offsets for Tibia 8.00 ##
_Do not change these unless you know what you are doing_
|**Name**|**Default Value**|
|:-------|:----------------|
|ptrWindowBegin|H6198B4|
|WindowLeftOffset|H14|
|WindowTopOffset|H18|
|WindowWidthOffset|H1C|
|WindowHeightOffset|H20|
|WindowCaptionOffset|H50|
|WindowButtonPressedOffset|H28|
|WindowButton1Offset|H2C|
|WindowButton2Offset|H30|
|WindowButton3Offset|H34|
|WindowButton4Offset|H38|
|ptrInGame|H766DF8|
|ptrFrameRateBegin|H76793C|
|ptrEnterOneNamePerLine|H594670|
|ptrForYourInformation|H595968|
|ptrCharacterSelectionIndex|H766DB8|
|ptrEncryptionKey|H7637AC|
|ptrRSAKey|H593610|
|RSAKeyOpenTibia|109120132967...|
|ptrServerAddressBegin|H75EAE8|
|ptrServerPortBegin|H75EB4C|
|ServerAddressDist|H70|
|ServerAddressCount|HA|
|ptrCharacterID|H60EAD0|
|ptrBattleListBegin|H60EB30|
|ptrFirstContainer|H617000|
|ptrInventoryBegin|H616f88|
|ptrLevel|H60EAC0|
|ptrLevelPercent|H60EAB8|
|ptrMagicLevel|H60EABC|
|ptrMagicLevelPercent|H60EAB4|
|ptrExperience|H60EAC4|
|ptrFollowedEntityID|H60EA98|
|ptrAttackedEntityID|H60EA9C|
|ptrLastAttackedEntityID|H76DA10|
|ptrMaxHitPoints|H60EAC8|
|ptrHitPoints|H60EACC|
|ptrMaxManaPoints|H60EAAC|
|ptrManaPoints|H60EAB0|
|ptrSoulPoints|H60EAA8|
|ptrCapacity|H60EAA0|
|ptrCoordX|H6198F8|
|ptrCoordY|H6198F4|
|ptrCoordZ|H6198F0|
|ptrSecureMode|H763BCC|
|ptrFightingMode|H763BD4|
|ptrChasingMode|H763BD0|
|ptrMapPointer|H61E408|
|ptrConditions|H60EA58|
|ptrVipListBegin|H60C7F0|
|ptrSkillsBegin|H60EA78|
|ptrSkillsPercentBegin|H60EA5C|
|ptrStamina|H60EAA4|
|ptrGoToX|H60EB14|
|ptrGoToY|H60EB10|
|ptrGoToZ|H60EB0C|
|ptrHotkeyBegin|H763C18|
|ptrStatusMessage|H768458|
|ptrStatusMessageTimer|H768454|
|ptrCharacterListBegin|H766DBC|
|CharacterListDist|H54|
|CharacterListWorldOffset|H30|
|HotkeyMax|H24|
|HotkeyItemDataOffset|H90|
|HotkeyItemOffset|H120|
|HotkeyItemDist|4 |
|HotkeyTextAutoSendOffset|H1B0|
|HotkeyTextAutoSendDist|1 |
|HotkeyTextOffset|H1D8|
|HotkeyTextDist|H100|
|BLMax|H96|
|BLDist|HA0|
|BLNameOffset|H4|
|BLCoordXOffset|H24|
|BLCoordYOffset|H28|
|BLCoordZOffset|H2C|
|BLDirectionOffset|H50|
|BLWalkingOffset|H4C|
|BLOutfitOffset|H60|
|BLHeadCOffset|H64|
|BLBodyCOffset|H68|
|BLLegsCOffset|H6C|
|BLFeetCOffset|H70|
|BLAddonsOffset|H74|
|BLLightIntensityOffset|H78|
|BLLightColorOffset|H7C|
|BLHPPercentOffset|H88|
|BLSpeedOffset|H8C|
|BLOnScreenOffset|H90|
|BLSkullOffset|H94|
|BLPartyOffset|H98|
|MaxContainers|H10|
|ContainerDist|H1EC|
|ContainerIDOffset|4 |
|ContainerNameOffset|H10|
|ContainerSizeOffset|H30|
|ContainerHasParentOffset|H34|
|ContainerItemCountOffset|H38|
|ContainerFirstItemOffset|H3C|
|ItemDist|HC|
|ItemCountOffset|4 |
|MapObjectIdOffset|4 |
|MapObjectDataOffset|8 |
|MapObjectExtraDataOffset|HC|
|MapObjectDist|HC|
|MapTileDist|HAC|
|VipMax|H64|
|VipNameOffset|4 |
|VipStatusOffset|H22|
|VipDist|H2C|
|SkillsDist|4 |
|FrameRateCurrentOffset|H60|
|FrameRateLimitOffset|H58|
|ptrNameSpy|H4DD2D7|
|ptrNameSpy2|H4DD2E1|
|NameSpyDefault|H4C75|
|NameSpy2Default|H4275|
|MCPatchOffset|HF6224|
|MCPatchReplacement|HEB|
|MCPatchOriginal|H75|