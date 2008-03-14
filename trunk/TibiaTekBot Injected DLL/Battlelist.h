#pragma once

struct BLAddress
{
	unsigned int *ptrBattlelistBegin;
	unsigned int BLMax;
	unsigned int BLDist;
	unsigned int BLName;
	unsigned int BLXCoordOffset;
	unsigned int BLYCoordOffset;
	unsigned int BLZCoordOffset;
};

struct LocationDefinition
{
	int x;
	int y;
	int z;
};

class Battlelist
{
private:
	int IndexPosition;
	int ID;
	BLAddress BLConst;
public:
	Battlelist(BLAddress Addresses);
	LocationDefinition GetLocation(char *CreatureName);
};