#include "stdafx.h"
#include <windows.h>
#include <string>
#include "Constants.h"
#include "Battlelist.h"
#include "Core.h"

Battlelist::Battlelist()
{
	Reset();
}

bool Battlelist::NextEntity()
{
	if (IndexPosition + 1 < Consts::BLMax) {
		IndexPosition++;
		Address += Consts::BLDist/sizeof(int);
		return true;
	}
	return false;
}

bool Battlelist::PrevEntity()
{
	if (IndexPosition > 0) {
		IndexPosition--;
		Address -= Consts::BLDist/sizeof(int);
		return true;
	}
	return false;
}

std::string Battlelist::Name(){
	std::string Result = (char*)(Address + Consts::BLNameOffset/sizeof(int));
	return Result;
}

void Battlelist::Reset()
{
	IndexPosition = 0;
	Address = (unsigned int*)Consts::ptrBattlelistBegin;
}

bool Battlelist::IsVisible()
{
	LocationDefinition loc = GetLocation();
	//char output[128];
	//sprintf(output, "%d,%d,%d - %d,%d,%d",*Consts::ptrCharX,*Consts::ptrCharY,*Consts::ptrCharZ, loc.x, loc.y, loc.z);
	//MessageBoxA(0,output,"coords",0);
	return (abs(*Consts::ptrCharX - loc.x) < 8 && abs(*Consts::ptrCharY - loc.y) < 6 && *Consts::ptrCharZ == loc.z);
}

bool Battlelist::FindByName(Battlelist *BL, std::string EntityName){
	do {
		if (BL->Name() == EntityName)
			return true;
	} while(BL->NextEntity());
	return false;
}

bool Battlelist::IsOnScreen()
{
	return (*(Address + Consts::BLOnScreenOffset/sizeof(int)) == 1);
}

unsigned int Battlelist::HPPercentage()
{
	return *(Address + Consts::BLHPPercentOffset/sizeof(int));
}


bool Battlelist::IsPlayer()
{
	return (ID() < 0x40000000);
}

int Battlelist::GetIndexPosition()
{
	return IndexPosition;
}

unsigned int Battlelist::ID()
{
	return *(Address);
}

LocationDefinition Battlelist::GetLocation()
{
	LocationDefinition ReturnValue = {0, 0, 0};
	LocationDefinition *ld = (LocationDefinition*)(Address + Consts::BLLocationOffset/sizeof(int));
	ReturnValue.x = ld->x;
	ReturnValue.y = ld->y;
	ReturnValue.z = ld->z;
	return ReturnValue;
}
	