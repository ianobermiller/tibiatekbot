#include "stdafx.h"
#include <string>
#include <windows.h>
#include "Constants.h"

namespace Consts {

	/* General */
	const unsigned int * INGAME = NULL;
	const unsigned int * ptrCharX = NULL;
	const unsigned int * ptrCharY = NULL;
	const unsigned int * ptrCharZ = NULL;

	/* Keyboard */
	const unsigned int * POPUP = NULL;

	/* Battlelist */
	const unsigned int * ptrBattlelistBegin = NULL;
	unsigned int BLMax = 0;
	unsigned int BLDist = 0;
	unsigned int BLNameOffset = 0;
	unsigned int BLLocationOffset = 0;
	unsigned int BLOnScreenOffset = 0;
	unsigned int BLHPPercentOffset = 0;
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