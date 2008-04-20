#pragma once


class PlayerText
{
public:
//Variables. You shouldn't set directly values to these (even you could). They are public so that it's faster to acceess to them
//than using PlayerText.GetDisplayText e.g functions. Use SetPlayer and RemovePlayer functions instead.
	char *DisplayText;
	int CreatureId;
	int TextId;
	int cR;
	int cG;
	int cB;
	int TextFont;
	int RelativeX;
	int RelativeY;
	bool InUse;
//Functions
	void SetPlayer(char *Player, int Id, char *Text, int Color_R, int Color_G, int Color_B, int X, int Y, int Font, int *Counter);
	void RemovePlayer(int *Counter);
};