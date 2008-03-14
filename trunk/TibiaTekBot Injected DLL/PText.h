#pragma once


class PText
{
private:
	char *DisplayText;
	char *PlayerName;
	int TextId;
	int cR;
	int cG;
	int cB;
	int Font;
public:
	SetPlayer(char *Player, int Id, char *Text, int Color_R, int Color_G, int Color_B, int Font, int *Counter);
	RemovePlayer();
};