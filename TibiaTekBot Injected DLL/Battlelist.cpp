#include "stdafx.h"
#include <windows.h>
#include <string>
#include "Battlelist.h"

Battlelist::Battlelist(BLAddress Addresses)
{
	BLConst.BLDist = Addresses.BLDist;
	BLConst.BLMax = Addresses.BLMax;
	BLConst.BLName = Addresses.BLName;
	BLConst.BLXCoordOffset = Addresses.BLXCoordOffset;
	BLConst.BLYCoordOffset = Addresses.BLYCoordOffset;
	BLConst.BLZCoordOffset = Addresses.BLZCoordOffset;
	BLConst.ptrBattlelistBegin = Addresses.ptrBattlelistBegin;
	IndexPosition = 0;
	ID = 0;
	//MessageBoxA(0, "Battlelist Constructor Done!", "Info", 0);
}

LocationDefinition Battlelist::GetLocation(char *CreatureName)
{
	LocationDefinition ReturnValue = {0, 0, 0};
	int *TempValue = 0;
	int i = 0;
	char* Name = 0;
	unsigned int *BLEntry;
	for(BLEntry = (unsigned int*)BLConst.ptrBattlelistBegin; (unsigned int)BLEntry < ((unsigned int)BLConst.ptrBattlelistBegin + BLConst.BLMax * BLConst.BLDist); BLEntry += BLConst.BLDist/sizeof(int)) {
		Name = (char*)(BLEntry + 1);
		if (strcmp(Name, CreatureName)==0) {
			LocationDefinition *ld = (LocationDefinition*)(BLEntry + BLConst.BLXCoordOffset/sizeof(int));
			ReturnValue.x = ld->x;
			ReturnValue.y = ld->y;
			ReturnValue.z = ld->z;
			return ReturnValue;
		}
	}
	return ReturnValue;
}
			