# Introduction #

The Alarms feature lets you easily set the conditions at which the alarms will activate (''ring''). You can reach the Alarms window from ''Tray Icon -> Alarms''.

# Alarms Actions #

All tabs contain an ''Actions'' box. This box configures what actions to take if certain conditions are met. There are four available actions for each alarm type, these are:

**'''Play Sound:''' plays a certain sound depending on the alarm type.** '''Logout:''' attempts to logout the player.
**'''Message Player:''' checks whether a certain player has to be informed.** '''Player To Be Messaged:''' sends a message informing of the activated alarm to the player whose name is written on this input.

## Colored Musical Notes ##
By default, when an alarm is activated musical notes will appear on the character. The notes will vary in color depending on the alarm that was activated.

**'''Red notes:''' battlelist alarm is activated.** '''White notes:''' message alarm is activated.
**'''Yellow notes:''' status alarm is activated.** '''Green notes:''' items alarm is activated.

You can turn off the musical notes by setting as "''False''" the "''`MusicalNotesOnAlarm`''" constant via the [[Editor](Constants.md)].

# Alarm Types #

The Alarms window is divided into four different tabs: Battlelist, Message, Status and Items. These four tabs are the different alarm types respectively. See the image below:

[[Image:![http://www.tibiatek.com/images/manual/TTBAlarms_Tabs.gif](http://www.tibiatek.com/images/manual/TTBAlarms_Tabs.gif)]]

## Battlelist Alarm ##

### Activate Alarm if ###
**'''Player:''' a players comes into sight.** '''Monster/NPC:''' a creature or a NPC comes into sight.
**'''Player Killer:''' a skulled player comes into sight.** '''Multi-Floor:''' checks all other floors. Remember that if you're on the main floor, anything below cannot be detected. And if you are ONE floor below the main floor anything above cannot be detected.

### Ignored Players ###

Add players to this list to exclude from activating this alarm. Write the names in the input below and press ''Add''. If you want to remove a name select it from the list and press ''Remove''.

### Multi-Floor Triggers ###

**'''Below:''' activates detection of anything on the floors below you.** '''Above:''' activates detection of anything on the floors above you.

[[Image:![http://www.tibiatek.com/images/manual/TTBAlarms_Battlelist.gif](http://www.tibiatek.com/images/manual/TTBAlarms_Battlelist.gif)]]

## Message Alarm ##

### Activate Alarm if ###
**'''Public Message:''' a message received on the ''Default'' channel.** '''Private Message:''' a message received privately from another player.

### Ignored Players ###

Add players to this list to exclude from activating this alarm. Write the names in the input below and press ''Add''. If you want to remove a name select it from the list and press ''Remove''.

[[Image:![http://www.tibiatek.com/images/manual/TTBAlarms_Message.gif](http://www.tibiatek.com/images/manual/TTBAlarms_Message.gif)]]

## Status Alarm ##

### Activate Alarm If ###

**'''Hit Points below:''' self-explanatory.** '''Mana Points below:''' self-explanatory.
**'''Soul Points below:''' self-explanatory.** '''Capacity below:''' self-explanatory.

### If Conditioned ###

**'''Combat Sign:''' character is targeted (logout block).** '''Poisoned:''' self-explanatory.
**'''Paralyzed:''' self-explanatory.** '''Burnt:''' self-explanatory.
**'''Drowning:''' self-explanatory.** '''Electrified:''' self-explanatory.

[[Image:![http://www.tibiatek.com/images/manual/TTBAlarms_Status.gif](http://www.tibiatek.com/images/manual/TTBAlarms_Status.gif)]]

## Items Alarm ##

### Items List ###

You can activate a condition by marking one of the list items at the left, and then setting the parameters to the right. The list items are: Food, Blank Runes, Worms, Throwables (Spears) and Ammunition.

'''Fire an alarm when you have:''' use a comparison operator from the drop-down menu and set the desired amount of the list item selected at the left.

Find also in...

**'''Inventory:''' mark to make the bot check if there are items on the inventory that match the selected list item.** '''Floor:''' mark to make the bot check if there are items on the floor that match the selected list item.

[[Image:![http://www.tibiatek.com/images/manual/TTBAlarms_Items.gif](http://www.tibiatek.com/images/manual/TTBAlarms_Items.gif)]]

# Saving and Loading #

You can save your Alarms preferences by clicking on the "''Save''" button located at the bottom left corner of the Alarms window. Alarms are automatically loaded for each character. Each character has a different Alarms configuration.

If you want to reload the ''Alarms.xml'' for the current character press the "''Load''" button. This is specially useful if you modify the Alarms.xml with an external editor.