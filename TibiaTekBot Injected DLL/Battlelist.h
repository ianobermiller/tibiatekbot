#pragma once
#ifndef _BATTLELIST_H_
#define _BATTLELIST_H_

struct LocationDefinition
{
	unsigned int x;
	unsigned int y;
	unsigned int z;
};

class Battlelist;

class Battlelist
{
private:
	unsigned int *Address;
	unsigned int IndexPosition;

public:
	/* Constructors/Destructors */
	Battlelist();
	//~Battlelist();

	std::string Name();
	void Reset();
	unsigned int ID();
	bool NextEntity();
	bool PrevEntity();
	bool IsVisible();
	int GetIndexPosition();
	bool IsOnScreen();
	bool IsPlayer();
	unsigned int HPPercentage();
	static bool FindByName(Battlelist*, std::string);
	LocationDefinition GetLocation();
};

#endif
