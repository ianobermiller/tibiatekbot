#include "stdafx.h"
#include <string>
#include <windows.h>
#include "Constants.h"

namespace Consts {

	/* General */
	const unsigned int * INGAME = (unsigned int *)0x76c2c8;
	const unsigned int * ptrCharX = (unsigned int *)0x61e9c8;
	const unsigned int * ptrCharY = (unsigned int *)0x61e9c4;
	const unsigned int * ptrCharZ = (unsigned int *)0x61e9c0;

	/* Keyboard */
	const unsigned int * POPUP = NULL;

	/* Battlelist */
	const unsigned int * ptrBattlelistBegin = (unsigned int *)0x613bd0;
	unsigned int BLMax = 0x96;
	unsigned int BLDist = 0xA0;
	unsigned int BLNameOffset = 0x4;
	unsigned int BLLocationOffset = 0x24;
	unsigned int BLOnScreenOffset = 0x90;
	unsigned int BLHPPercentOffset = 0x88;

	/* Displaying Text Stuff */
	DWORD ptrPrintName = 0x4E228A;
	DWORD ptrPrintFPS = 0x44E753;
	DWORD ptrShowFPS = 0x611874;
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

int KeyboardVKEntriesCount = 0;
KeyboardVKEntry *KeyboardVKEntries = NULL;

bool KeyboardEnabled = false;
bool KeyboardSayMode = false;