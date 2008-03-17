#if MSC_VER > 100
#pragma once
#endif
#ifndef _CONSTANTS_H_
#define _CONSTANTS_H_


namespace Consts {

	/* General */
	extern const unsigned int * INGAME;
	extern const unsigned int * ptrCharX;
	extern const unsigned int * ptrCharY;
	extern const unsigned int * ptrCharZ;

	/* Keyboard */
	extern const unsigned int * POPUP;

	/* Battlelist */
	extern const unsigned int * ptrBattlelistBegin;
	extern unsigned int BLMax;
	extern unsigned int BLDist;
	extern unsigned int BLNameOffset;
	extern unsigned int BLLocationOffset;
	extern unsigned int BLOnScreenOffset;
	extern unsigned int BLHPPercentOffset;

}

enum KeyboardState {
	None =	0x0,
	Alt =	0x1,
	Shift =	0x2,
	Ctrl =	0x4
};

typedef struct KeyboardVKEntry {
	int VirtualKeyOriginalCode;
	int VirtualKeyNewCode;
	KeyboardState State; //bit 0 = Alt, bit 1 = Shift, bit 2 = Ctrl
} KeyboardVKEntry;

extern int KeyboardVKEntriesCount;
extern KeyboardVKEntry *KeyboardVKEntries;

extern bool KeyboardEnabled;
extern bool KeyboardSayMode;

/*  */
/* DLL Injection Related Stuff */
extern WNDPROC WndProc;
extern HINSTANCE hMod;
extern HWND TibiaWindowHandle;
extern DWORD TibiaProcessID;

/* Pipes */
extern std::string PipeName;
extern bool PipeConnected;
extern HANDLE PipeHandle;
extern HANDLE PipeThread;
extern BYTE Buffer[1024];
extern CRITICAL_SECTION PipeReadCriticalSection;

/* Structures */
struct Ctext
{
	char text[1024];
	int r,g,b;
	int x,y;
	int font;
	bool used;
	int EntityID;
}; //Display Text Structure



#endif
