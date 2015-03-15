# Introduction #

Every command in TibiaTek Bot requires a proper syntax to perform it's task.

All commands have to be preceded by an ampersand (&). Commands are meant to be written in the TibiaTek Bot channel but you can also write them on any other channels.

**Caution:** try to send commands via the TibiaTek Bot channel to avoid blatant mistakes on public channels (by forgetting to put the ampersand (&) before the command name). Moreover, you should also make sure spells are written on other channels, not on the TibiaTek Bot channel because they will not be parsed.

# Commands and Arguments #

[TibiaTek Bot](TibiaTekBot.md) has two kinds of commands. Those that accept "on" or "off" as arguments and those that accept more than that. Let's start with the ones that accept "on" or "off".

## Commands with "on" or "off" Argument ##

These commands are easy to use. Just write command name and then on or off as arguments.

**Examples:**
```
&exp on
&light on
&fpschanger on
&attack on
```

## Commands with More Arguments ##

These commands are fairly easy to use too. If the argument is of variable value there are two things you should know: first, variable text arguments are to be enclosed in quotation marks, meanwhile variable number arguments are to be written without quotation marks and as _whole_ numbers.

**Examples:**
```
&char "Tib Tek Warrior"
&heal 90% "exura"
&runemaker 270 2 "Heavy Magic Missile"
```

There are also commands that accept constant/static arguments. You can recognize them from their &help 

&lt;command&gt;

 output. Constant arguments DO NOT require quotation marks as they are not variable. Take this for example, &help light:
```
Tib Tek Warrior [17]: &help light
TibiaTek Bot [101]: «Light Effect»
Usage: &light <on | off | torch | great torch | ultimate torch | utevo lux | utevo gran lux | utevo vis lux | light wand>.
Example: &light ultimate torch
```

Compare these two commands. In the first example you **have to** use quotation marks, and in the second one you **don't** have to use them.

**Example 1:**
```
TibiaTek Bot [101]: «Character Information Lookup»
Usage: &char "<Player Name>".
Example: &char "Tib Tek Warrior"
```
**Example 2:**
```
TibiaTek Bot [101]: «Configuration Manager»
Usage: &config <load | edit | clear>.
Example: &config edit
```

**Note:** when a command is not written correctly, you will receive this message:
_Invalid format for this command.
For help on the usage, type: &help light._