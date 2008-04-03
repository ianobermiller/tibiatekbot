#include "stdafx.h"
#include <string>
#include <windows.h>
#include "Constants.h"

namespace Consts {

	/* General */
	const unsigned int * INGAME = 0;
	const unsigned int * ptrCharX = 0;
	const unsigned int * ptrCharY = 0;
	const unsigned int * ptrCharZ = 0;
	const unsigned int * ptrCharacterID = 0;

	/* Keyboard */
	const unsigned int * POPUP = NULL;

	/* Battlelist */
	const unsigned int * ptrBattlelistBegin = 0;
	unsigned int BLMax = 0;
	unsigned int BLDist = 0;
	unsigned int BLNameOffset = 0x4;
	unsigned int BLLocationOffset = 0;
	unsigned int BLOnScreenOffset = 0;
	unsigned int BLHPPercentOffset = 0;

	/* Displaying Text Stuff */
	DWORD ptrPrintName = 0;
	DWORD ptrPrintFPS = 0;
	DWORD ptrShowFPS = 0;
	DWORD ptrNopFPS = 0;
}

/* DLL Injection Related Stuff */
WNDPROC WndProc = 0;
HINSTANCE hMod = 0;
HWND TibiaWindowHandle = 0;
DWORD TibiaProcessID = 0;

/* Pipes */
std::string PipeName;
bool PipeConnected = false;
HANDLE PipeHandle = 0;
HANDLE PipeThread = 0;
BYTE Buffer[1024] = {0};
CRITICAL_SECTION PipeReadCriticalSection;

