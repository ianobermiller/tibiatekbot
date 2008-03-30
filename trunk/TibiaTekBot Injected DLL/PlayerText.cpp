#include "stdafx.h"
#include <windows.h>
#include <string>
#include "PlayerText.h"

void PlayerText::SetPlayer(char *Player, int Id, char *Text, int Color_R, int Color_G, int Color_B, int X, int Y, int Font, int *Counter)
{
	//Storing values to the object
	PlayerName = Player;
	TextId = Id;
	DisplayText = Text;
	cR = Color_R;
	cG = Color_G;
	cB = Color_B;
	RelativeX = X;
	RelativeY = Y;
	TextFont = Font;
	InUse = true;

	*Counter += 1;
}

void PlayerText::RemovePlayer(int *Counter)
{
	InUse = false;
	*Counter -= 1;
}

