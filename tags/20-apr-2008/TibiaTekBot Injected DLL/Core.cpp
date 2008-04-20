#include "stdafx.h"
#include <windows.h>
#include <string>
#include <list>
#include "Constants.h"
#include "Core.h"
#include "Packet.h"
#include "Battlelist.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

#define MAX_TEXT 512

/* DisplayText. Credits for Displaying text goes to Stiju and Zionz. Thanks for the help!*/
Ctext texts[MAX_TEXT] = {0};
list<PlayerText> CreatureTexts;
list<PlayerText> UsedCreatures; //Used to save creatures in screen
list<PlayerText> AddedCreatures;
list<PlayerText> UnusedCreatures;
//BLAddress BLConsts;
int PlayerCount = 0;

int n_text=0;
char g_Text[1024] = "";
int g_Red = 0;
int g_Green = 0;
int g_Blue = 0;
int g_X = 0;
int g_Y = 0;
int g_Font = 1;

/*Address are loaded from Constants.xml file */

bool FirstTime = true;

inline bool InGame(){ return (*Consts::INGAME == 8); }

inline bool PopupOpened() { return (*Consts::POPUP == 11); }

bool ComparePlayerText(PlayerText first, PlayerText second) {
	if(first.CreatureId == second.CreatureId) {
		return true;
	} else {
		return false;
	}
}

int KeyboardEntriesCount = 0;
KeyboardEntry *KeyboardEntries = NULL;
bool KeyboardEnabled = false;
bool KeyboardSayMode = false;
KeyboardModifier KeyboardModifiers;

void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	list<PlayerText>::iterator it;

	//Displaying Original Text
	PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);

	//Write text above player
	DWORD *EntityID = (DWORD*)(lpText - 4);

	//Removing unused text
	if (*EntityID == *Consts::ptrCharacterID) { //New "round" begins in the drawing routine
		if (!UsedCreatures.empty()) {
			CreatureTexts.unique(ComparePlayerText);
			char *TempChar = 0;
			for(it=CreatureTexts.begin(); it!=CreatureTexts.end(); ) {
				TempChar = (*it).DisplayText;
				it = CreatureTexts.erase(it);
				free(TempChar);
			}
		}
		CreatureTexts.assign(UsedCreatures.begin(), UsedCreatures.end());
		if (AddedCreatures.size() > 0) {
			CreatureTexts.insert(CreatureTexts.begin(), AddedCreatures.begin(), AddedCreatures.end()); //Adding Added creatures to the showing list
			AddedCreatures.clear();
		}
		UsedCreatures.clear();
	}

	//Displaying texts
	for(it=CreatureTexts.begin(); it!=CreatureTexts.end(); ) {
		if (*EntityID == (*it).CreatureId) {
			UsedCreatures.push_back(*it);
			PrintText(0x01, nX + (*it).RelativeX, nY + (*it).RelativeY, (*it).TextFont, (*it).cR, (*it).cG, (*it).cB, (*it).DisplayText, 0x00);
			it = CreatureTexts.erase(it);
		} else {
			++it;
		}
	}
}

void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign)
{
	bool *fps = (bool*)Consts::ptrShowFPS;
	if(*fps == true)
	{
		PrintText(nSurface, nX, nY, nFont, nRed, nGreen, nBlue, lpText, nAlign);
		nY += 12;
	}

	for(int i=0; i<MAX_TEXT; i++){
		if(!texts[i].used)
			continue;
		
		strcpy(g_Text,texts[i].text);
		g_Blue = texts[i].b;
		g_Green = texts[i].g;
		g_Red = texts[i].r;
		g_Y = texts[i].y;
		g_X = texts[i].x;
		g_Font = texts[i].font;

		PrintText(0x01, g_X, g_Y, g_Font, g_Red, g_Green, g_Blue, g_Text, 0x00); //0x01 Surface, 0x00 Align
	}
}

DWORD HookCall(DWORD dwAddress, DWORD dwFunction)
{
	DWORD dwOldProtect, dwNewProtect, dwOldCall, dwNewCall;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	dwNewCall = dwFunction - dwAddress - 5;
	memcpy(&callByte[1], &dwNewCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy(&dwOldCall, (LPVOID)(dwAddress+1), 4);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
	return dwOldCall;
}

void UnhookCall(DWORD dwAddress, DWORD dwOldCall)
{
	DWORD dwOldProtect, dwNewProtect;
	BYTE callByte[5] = {0xE8, 0x00, 0x00, 0x00, 0x00};

	memcpy(&callByte[1], &dwOldCall, 4);
	
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(dwAddress), &callByte, 5);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), 5, dwOldProtect, &dwNewProtect);
}

void Nop(DWORD dwAddress, int size)
{
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, PAGE_READWRITE, &dwOldProtect);
	memset((LPVOID)(dwAddress), 0x90, size);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(dwAddress), size, dwOldProtect, &dwNewProtect);
}


void SetText(unsigned int TextNum, bool enabled, int nX, int nY, int nRed, int nGreen, int nBlue, int font, char *lpText)
{
	if(TextNum > MAX_TEXT-1){return;}
	if(font<1 || font >4){return;}
	if(nRed > 0xFF || nRed < 0){return;}
	if(nGreen > 0xFF || nGreen < 0){return;}
	if(nBlue > 0xFF || nBlue < 0){return;}

	strcpy(texts[TextNum].text, lpText);
	texts[TextNum].r=nRed;
	texts[TextNum].g=nGreen;
	texts[TextNum].b=nBlue;
	texts[TextNum].x=nX;
	texts[TextNum].y=nY;
	texts[TextNum].used = enabled;
	texts[TextNum].font = font;
}

void __declspec(noreturn) UninjectSelf(HMODULE Module)
{
   __asm
   {
      push -2
      push 0
      push Module
      mov eax, TerminateThread
      push eax
      mov eax, FreeLibrary
      jmp eax
   }
}

LRESULT __stdcall WindowProc(HWND hWnd, int uMsg, WPARAM wParam, LPARAM lParam){
	static int i;
	switch (uMsg) {
		case WM_CHAR:
			{
				/*
				if (wParam == 'q'){
					int t = 0x000001;
					CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_SHIFT, t);
					t=0x000001;
					CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_F1, t );
					t=0xc0000001;
					CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_F1,t );
					t=0xc0000001;
					CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_SHIFT, t);
					return 0;
				}*/
				if (KeyboardEnabled && InGame() && !PopupOpened()) {
					if (!KeyboardSayMode){
						switch(wParam)
						{
							case 0x0D: // ENTER
								{
									KeyboardSayMode = true;
									return 0;
								}
							case 0x02:
							case 0x03:
							case 0x05:
							case 0x06:
							case 0x07:
							case 0x08:
							case 0x09:
							case 0x0B:
							case 0x0C:
							case 0x0E:
							case 0x0F:
							case 0x12:
							case 0x14:
							case 0x15:
							case 0x16:
							case 0x17:
							case 0x18:
							case 0x1A:
							case 0x1B:
								break;
							default:
								return 0;
						}
					}
				}
			}
			break;
		case WM_KEYUP:
			{
				switch (wParam){
					case VK_CONTROL:	KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers & !KMCtrl);	break;
					case VK_SHIFT:		KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers & !KMShift);	break;
					case VK_MENU:		KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers & !KMAlt);		break;
				}
			}
			break;
		case WM_KEYDOWN:
			{
				/*
				if (wParam == 'q'){

					int t = 0x2A0001;
					CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_SHIFT, t);
					t=0x3b0001;
					CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_F1, t );
					t=0xc03b0001;
					CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_F1,t );
					t=0xc02a0001;
					CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_SHIFT, t);
					
					return 0;
				}*/
				/*if (wParam == VK_F1){
					char output[256];
					FILE *fp = fopen("c:/test.txt","a+");
					sprintf(output, "wParam=%x,lParam=%x\n", wParam, lParam);
					fprintf(fp, output);
					fclose(fp);
					break;
					//CallWindowProc(WndProc, hWnd, uMsg, wParam, lParam );
				}*/

				switch (wParam){
					case VK_CONTROL:	KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers | KMCtrl);		break;
					case VK_SHIFT:		KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers | KMShift);	break;
					case VK_MENU:		KeyboardModifiers = (KeyboardModifier)(KeyboardModifiers | KMAlt);		break;
				}
				if (KeyboardEnabled && InGame() && !PopupOpened()){
					if (KeyboardSayMode){
						if (wParam == VK_ESCAPE){
							KeyboardSayMode = false;
							return 0;
						} else if(wParam == VK_RETURN) {
							KeyboardSayMode = false;
							break;
						}
					} else {
						for (i=0;i<KeyboardEntriesCount;i++){
							if (KeyboardEntries[i].OldVirtualKey == wParam
								&& ((KeyboardModifier)(KeyboardEntries[i].OldModifier & 0x7) == KeyboardModifiers)){
								if (KeyboardEntries[i].Kind == KEKPressKey){
									if ((KeyboardEntries[i].NewModifier & KMCtrl) == KMCtrl)
										CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_CONTROL, 0x00000001);
									if ((KeyboardEntries[i].NewModifier & KMShift) == KMShift)
										CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_SHIFT, 0x00000001);
									if ((KeyboardEntries[i].NewModifier & KMAlt) == KMAlt)
										CallWindowProc(WndProc, hWnd, WM_KEYDOWN, VK_MENU, 0x00000001);
									CallWindowProc(WndProc, hWnd, WM_KEYDOWN, KeyboardEntries[i].NewVirtualKey, 0x00000001);
									CallWindowProc(WndProc, hWnd, WM_KEYUP, KeyboardEntries[i].NewVirtualKey, 0xC0000001);
									if ((KeyboardEntries[i].NewModifier & KMCtrl) == KMCtrl)
										CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_CONTROL, 0xC0000001);
									if ((KeyboardEntries[i].NewModifier & KMShift) == KMShift)
										CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_SHIFT, 0xC0000001);
									if ((KeyboardEntries[i].NewModifier & KMAlt) == KMAlt)
										CallWindowProc(WndProc, hWnd, WM_KEYUP, VK_MENU, 0xC0000001);
								}
								break;
							}
						}
						return 0;
					}
				}
			}
			break;
	}
    return CallWindowProc(WndProc, hWnd, uMsg, wParam, lParam );
}


void PipeOnRead(){
	int position=0;
	WORD len  = 0;
	len = Packet::ReadWord(Buffer, &position);
	BYTE PacketID = Packet::ReadByte(Buffer, &position);
	switch (PacketID){
		case 1: // Set Constant
			{	
				string ConstantName = Packet::ReadString(Buffer, &position);
				if (ConstantName == "ptrInGame"){
					Consts::INGAME = (const unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrWASDPopup") {
					Consts::POPUP = (const unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "TibiaWindowHandle") {
					TibiaWindowHandle = (HWND)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrBattlelistBegin") {
					Consts::ptrBattlelistBegin = (unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPrintName") {
					Consts::ptrPrintName = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPrintFPS") {
					Consts::ptrPrintFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrShowFPS") {
					Consts::ptrShowFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrPrintTextFunc") {
					PrintText = (_PrintText*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLMax") {
					Consts::BLMax = (const int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLDist") {
					Consts::BLDist = (const int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLNameOffset") {
					Consts::BLNameOffset = (const int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLLocationOffset") {
					Consts::BLLocationOffset = (const int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLOnScreenOffset") {
					Consts::BLOnScreenOffset = (unsigned int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLHPPercentOffset") {
					Consts::BLHPPercentOffset = (unsigned int)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharX") {
					Consts::ptrCharX = (unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharY") {
					Consts::ptrCharY = (unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharZ") {
					Consts::ptrCharZ = (unsigned int*)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrNopFPS") {
					Consts::ptrNopFPS = (DWORD)Packet::ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharacterID") {
					Consts::ptrCharacterID = (unsigned int*)Packet::ReadDWord(Buffer, &position);
				}
			}
			break;
		case 2: // Hook WND_PROC
			{
				BYTE Hook = Packet::ReadByte(Buffer, &position);
				if (Hook){
					WndProc = (WNDPROC)SetWindowLongPtr(TibiaWindowHandle, GWLP_WNDPROC, (LONG)WindowProc);
				} else {
					SetWindowLongPtr(TibiaWindowHandle, GWLP_WNDPROC, (LONG)WndProc);
				}
			}
			break;
		case 3: // Testing
			{
				//Battlelist BL;
				//BL.Reset();
				//if (Battlelist::FindByName(&BL, "Seymour")) {
				PlayerText Creature = {0};
					Creature.cB = 0x55;
					Creature.cG = 0x55;
					Creature.cR = 0xFF;
					Creature.CreatureId = *Consts::ptrCharacterID;//(int)BL.ID();
					Creature.DisplayText = "PWNS";
					Creature.RelativeX = 0;
					Creature.RelativeY = -10;
					Creature.TextFont = 1;

					AddedCreatures.push_back(Creature);
				//}
			}
			break;
		case 4: // DisplayText
			{
				int TextId = Packet::ReadByte(Buffer, &position);
				int PosX = Packet::ReadWord(Buffer, &position);
				int PosY = Packet::ReadWord(Buffer, &position);
				int ColorRed = Packet::ReadWord(Buffer, &position);
				int ColorGreen = Packet::ReadWord(Buffer, &position);
				int ColorBlue = Packet::ReadWord(Buffer, &position);
				int Font = Packet::ReadWord(Buffer, &position);
				string Text = Packet::ReadString(Buffer, &position);

				char *lpText = (char*)malloc((Text.size()+1)*sizeof(char));
				strcpy(lpText, Text.c_str());

				SetText(TextId, true, PosX, PosY, ColorRed, ColorGreen, ColorBlue, Font, lpText);
			}
			break;
		case 5: //RemoveText
			{
				int TextId = Packet::ReadByte(Buffer, &position);
				if(TextId < MAX_TEXT)
					texts[TextId].used = false;
					free(texts[TextId].text);
			}
			break;
		case 6: //Remove All
			{
				int i;
				for(i=0; i<MAX_TEXT; i++){
					texts[i].used = false;
				}
			}
			break;
		case 7: //Inject Display
			{
				BYTE Inject = Packet::ReadByte(Buffer, &position);
				if(Inject) {
					/* Testing that every constant have a value */
					if(!Consts::ptrPrintFPS || !Consts::ptrPrintName || !Consts::ptrShowFPS) {
						MessageBoxA(0, "Error. All the constant doesn't contain a value", "Error", 0);
						break;
					}
					HookCall(Consts::ptrPrintName, (DWORD)&MyPrintName);
					HookCall(Consts::ptrPrintFPS, (DWORD)&MyPrintFps);
					Nop(Consts::ptrNopFPS, 6); //Showing the FPS all the time..
				} else {
					//TODO Restore Calls
				}
			}
			break;
		case 8: // Keyboard Enable/Disable
			{
				BYTE enabled = Packet::ReadByte(Buffer, &position);
				KeyboardEnabled = (enabled == 1);
				KeyboardSayMode = false;
			}
			break;
		case 9: // Keyboard Populate VK Entries
			{
				int entries = Packet::ReadDWord(Buffer, &position);
				if (KeyboardEntries){
					delete [] KeyboardEntries;
				}
				KeyboardEntries = new KeyboardEntry[entries];
				int i;
				for (i=0;i<entries;i++){
					KeyboardEntries[i].Kind = (KeyboardEntryKind)Packet::ReadByte(Buffer, &position);
					KeyboardEntries[i].NewVirtualKey = Packet::ReadByte(Buffer, &position);
					KeyboardEntries[i].OldVirtualKey = Packet::ReadByte(Buffer, &position);
					KeyboardEntries[i].NewModifier  = (KeyboardModifier)Packet::ReadByte(Buffer, &position);
					KeyboardEntries[i].OldModifier  = (KeyboardModifier)Packet::ReadByte(Buffer, &position);
				}
				KeyboardEntriesCount = entries;
			}
			break;
		case 0xA: //Set Text Above Player
			{	
				//TODO Doesn't work yet..
				int Id = Packet::ReadDWord(Buffer, &position);
                int nX = Packet::ReadWord(Buffer, &position);
                int nY = Packet::ReadWord(Buffer, &position);
				int Pos = Packet::ReadWord(Buffer, &position);
                int ColorR = Packet::ReadWord(Buffer, &position);
                int ColorG = Packet::ReadWord(Buffer, &position);
                int ColorB = Packet::ReadWord(Buffer, &position);
                int TxtFont = Packet::ReadWord(Buffer, &position);
                string Text = Packet::ReadString(Buffer, &position);
                char *lpText = (char*)calloc(Text.size() + 1, sizeof(char));
                strcpy(lpText, Text.c_str());
                PlayerText Creature = {0};
                Creature.cB = ColorB;
                Creature.cG = ColorG;
                Creature.cR = ColorR;
                Creature.CreatureId = Id;
                Creature.DisplayText = lpText;
                Creature.RelativeX = nX;
				if (Pos) {
					Creature.RelativeY = nY;
				} else {
					Creature.RelativeY = -nY;
				}
                Creature.TextFont = TxtFont;

                AddedCreatures.push_back(Creature);
			}
			break;
	}

}

void PipeThreadProc(HMODULE Module){
	DWORD br;
	if (WaitNamedPipeA(PipeName.c_str(), NMPWAIT_WAIT_FOREVER)) {
		PipeHandle = CreateFileA(PipeName.c_str(), GENERIC_READ | GENERIC_WRITE , 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
		PipeConnected = PipeHandle > 0;
		if (!PipeConnected){
			MessageBoxA(0, "Pipe connection failed!", "TibiaTekBot Injected DLL - Fatal Error", MB_ICONERROR);
			return;
		} else {
			do {
				EnterCriticalSection(&PipeReadCriticalSection);
				if (!ReadFile(PipeHandle, Buffer, 1024, &br, NULL))
					break;
				PipeOnRead();
				LeaveCriticalSection(&PipeReadCriticalSection);
			} while (true);
		}
	} else {
		MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?.", "TibiaTekBot Injected DLL - Fatal Error", 0);
	}
}




extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved){
	switch (reason){
		case DLL_PROCESS_ATTACH:
        {
			hMod = hModule;
			string CmdArgs = GetCommandLineA();
			int pos = CmdArgs.find("-pipe:");
			PipeName = "\\\\.\\pipe\\ttb" + CmdArgs.substr(pos + 6, 5);
			InitializeCriticalSection(&PipeReadCriticalSection);
			PipeConnected=false;
			PipeThread = CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)PipeThreadProc, hMod, NULL, NULL);
		}
        break;
		case DLL_PROCESS_DETACH:
		{
			TerminateThread(PipeThread, EXIT_SUCCESS);
			DeleteCriticalSection(&PipeReadCriticalSection);
		}
		break;
    }
    return true;
}
