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
	extern const unsigned int * ptrCharacterID;

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

	/* Displaying Text Stuff */
	extern DWORD ptrPrintName;
	extern DWORD ptrPrintFPS;
	extern DWORD ptrShowFPS;
	extern DWORD ptrNopFPS;
}

enum KeyboardModifier {
	KMNone =	0x0,
	KMAlt =		0x1,
	KMShift =	0x2,
	KMCtrl =	0x4
};

enum KeyboardEntryKind { KEKNone, KEKPressKey };

typedef struct KeyboardEntry {
	KeyboardEntryKind Kind;
	int NewVirtualKey;
	int OldVirtualKey;
	KeyboardModifier NewModifier;
	KeyboardModifier OldModifier;
} KeyboardVKEntry;

extern int KeyboardEntriesCount;
extern KeyboardEntry *KeyboardEntries;

extern bool KeyboardEnabled;
extern bool KeyboardSayMode;

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
}; 

//Display Text Structure
struct PlayerText
{
	char *DisplayText;
	int CreatureId;
	int cR;
	int cG;
	int cB;
	int TextFont;
	int RelativeX;
	int RelativeY;

};



#endif
