#include "stdafx.h"

#include <windows.h>
#include <string>
#include "Core.h"
#include "Battlelist.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

#define MAX_TEXT 512
#define PTEXT_FIRST 150

/* DLL Injection Related Stuff */
WNDPROC WndProc;
HINSTANCE hMod;
HWND TibiaWindowHandle;
DWORD TibiaProcessID;
//DWORD TTBProcessID;
//HANDLE TTBProcessHandle;

/* Pipes */
string PipeName;
bool PipeConnected;
HANDLE PipeHandle;
HANDLE PipeThread;
BYTE Buffer[1024];
CRITICAL_SECTION PipeReadCriticalSection;

/* DisplayText. Credits for Displaying text goes to Stiju and Zionz. Thanks for the help!*/
Ctext texts[MAX_TEXT] = {0};
BLAddress BLConsts;
void PrintFunction();
void ShowPlayer();
void GetPlayerInfo(int Surface, int nX, int nY, int Font, int nR, int nG, int nB, char *lpText, int len, int align);

int n_text=0;
char g_Text[1024] = "";
int g_Red = 0;
int g_Green = 0;
int g_Blue = 0;
int g_X = 0;
int g_Y = 0;
int g_Font = 1;

char ttext[1024]="'(use object on target)',0";
char tbuff[2048];

/*Address are loaded from Constants.xml file */
int *ptrCharX;
int *ptrCharY;
int *ptrCharZ;
DWORD address;
DWORD startadr;
DWORD jmpback;
DWORD showfps; //Show fps flag (0-1)
DWORD jmpfps; // jmp to skip fps

DWORD SPAddress; //ShowPlayerAddress
DWORD SPStartAdr;
DWORD SPJmpAdr;
bool FirstTime = true;

/* Constants from TTB */
//int INGAME=0;

inline BYTE ReadByte(BYTE *buffer, int *offset){
	return buffer[(*offset)++];
}

WORD ReadWord(BYTE *buffer, int *offset){
	WORD result;
	result = buffer[*offset]+(buffer[*offset+1]<<8);
	(*offset)+=2;
	return result;
}

DWORD ReadDWord(BYTE *buffer, int *offset){
	DWORD result;
	result = buffer[*offset]+(buffer[*offset+1]<<8)+(buffer[*offset+2]<<0x10)+(buffer[*offset+3]<<0x18);
	(*offset)+=4;
	return result;
}

double ReadDouble(BYTE *buffer, int *offset){
	BYTE a[8];
	double *result;
	int i;
	for (i=0;i<sizeof(double);i++)
		a[i] = buffer[*offset+7-i];
	result = (double*)&a[0];
	(*offset)+=8;
	return *result;
}

string ReadString(BYTE *buffer, int *offset){
	WORD length = ReadWord(buffer, offset);
	string result = "";
	int i;
	for (i=0;i<length;i++)
		result += *(buffer+(*offset)++);
	return result;
}

void InjectDisplay()
{
	BYTE jmpByte[8] = {0xE9, 0x00, 0x00, 0x00, 0x00, 0x90, 0x90, 0x90};
	int pointer = (int)&PrintFunction + 3;
	int jmp = pointer - startadr - 5;
	memcpy(&jmpByte[1], &jmp, 4);
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(startadr), 8, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(startadr), &jmpByte, 8);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(startadr), 8, dwOldProtect, &dwNewProtect);

	//Initialize
	int i;
	for(i=0; i<MAX_TEXT; i++){
		texts[i].used = false;
	}
}

void InjectShowPlayer()
{
	BYTE jmpByte[8] = {0xE9, 0x00, 0x00, 0x00, 0x00, 0x90, 0x90, 0x90};
	int pointer = (int)&ShowPlayer + 3;
	int jmp = pointer - SPStartAdr - 5;
	memcpy(&jmpByte[1], &jmp, 4);
	DWORD dwOldProtect, dwNewProtect;
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(SPStartAdr), 8, PAGE_READWRITE, &dwOldProtect);
	memcpy((LPVOID)(SPStartAdr), &jmpByte, 8);
	VirtualProtectEx(GetCurrentProcess(), (LPVOID)(SPStartAdr), 8, dwOldProtect, &dwNewProtect);
}

void SetText(unsigned int TextNum, bool enabled, int nX, int nY, int nRed, int nGreen, int nBlue, int font, char *lpText)
{
	if(TextNum > MAX_TEXT-1){return;}
	if(font<1 || font >4){return;}
	if(nRed > 0xFF || nRed < 0){return;}
	if(nGreen > 0xFF || nGreen < 0){return;}
	if(nBlue > 0xFF || nBlue < 0){return;}

	texts[TextNum].r=nRed;
	texts[TextNum].g=nGreen;
	texts[TextNum].b=nBlue;
	texts[TextNum].x=nX;
	texts[TextNum].y=nY;
	texts[TextNum].used = enabled;
	texts[TextNum].font = font;
	sprintf(texts[TextNum].text, "%s", lpText);
}

void ShowPlayer()
{
	__asm
	{
		call GetPlayerInfo;
		//add ESP,0x28;
		jmp SPJmpAdr;
	}
	
}

int GetFreeSlot()
{
	int i;
	for(i=150; i<MAX_TEXT; i++){
		if(!texts[i].used){return i;}
	}
	return 0;
}

void GetPlayerInfo(int Surface, int nX, int nY, int Font, int nR, int nG, int nB, char *lpText, int len, int aligment)
{
	static Battlelist BL(BLConsts);
	
	if (strcmp(lpText,"Seymour")==0){
		if (abs(*ptrCharX - BL.GetLocation("Seymour").x) < 8 && abs(*ptrCharY - BL.GetLocation("Seymour").y) < 6 && *ptrCharZ == BL.GetLocation("Seymour").z) {
		SetText(150, true, nX, nY-15, 255, 0, 0, 2, "TTB Rox!");
		}
	}
	if (abs(*ptrCharX - BL.GetLocation("Seymour").x) >= 8 || abs(*ptrCharY - BL.GetLocation("Seymour").y) >= 6 || *ptrCharZ != BL.GetLocation("Seymour").z) {
		texts[150].used = false;
	}
	__asm
	{
		push aligment;
		push len;
		push lpText;
		push nB;
		push nG;
		push nR;
		push Font;
		push nY;
		push nX;
		push Surface;
		call SPAddress;
		add ESP, 0x28;
	}
}

void PrintFunction()
{
	
	int i;
	char flagfps; 
	memcpy(&flagfps, (DWORD*)showfps, 1);

	for(i=0; i<MAX_TEXT; i++){
		if(!texts[i].used){continue;}

		sprintf(g_Text,texts[i].text);
		g_Blue = texts[i].b;
		g_Green = texts[i].g;
		g_Red = texts[i].r;
		g_Y = texts[i].y;
		g_X = texts[i].x;
		g_Font = texts[i].font;

		__asm
		{
			push 0x00; // Align
			push offset g_Text; // Text
			push g_Blue; // Blue
			push g_Green; // Green
			push g_Red; // Red
			push g_Font; // Font number 1 - 4
			push g_Y; // Y
			push g_X; // X
			push 0x01; // Surface
			call address;
		}
	}

	//Fix broken FPS counter
	if(flagfps==1){
		__asm
		{
			jmp jmpback;
		}
	}else{
		__asm
		{
			jmp jmpfps;
		}
	}
	
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





inline bool InGame(){ return (*INGAME == 8); }



LRESULT __stdcall WindowProc(HWND hWnd, int uMsg, WPARAM wParam, LPARAM lParam){
	//Beep(2000,50);
    return CallWindowProc(WndProc, hWnd, uMsg, wParam, lParam );
}

void PipeOnRead(){
	int position=0;
	WORD len  = 0;
	len = ReadWord(Buffer, &position);
	BYTE PacketID = ReadByte(Buffer, &position);
	switch (PacketID){
		case 1: // Set Constant
			{	
				string ConstantName = ReadString(Buffer, &position);
				if (ConstantName == "ptrInGame"){
					INGAME = (const int*)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrWASDPopup") {
					WASDPOPUP = (const int*)ReadDWord(Buffer, &position);
				} else if (ConstantName == "TibiaWindowHandle") {
					TibiaWindowHandle = (HWND)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrDisplayAddress") { //Display
					address = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrDisplayStartAddress") {
					startadr = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrDisplayJmpBack") {
					jmpback = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrDisplayShowFps") {
					showfps = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrDisplayJmpFps") {
					jmpfps = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrSPAddress") {
					SPAddress = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrSPStartAdr") {
					SPStartAdr = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrSPJmpAdr") {
					SPJmpAdr = (const DWORD)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrBattlelistBegin") {
					BLConsts.ptrBattlelistBegin = (unsigned int*)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLMax") {
					BLConsts.BLMax = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLDist") {
					BLConsts.BLDist = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLName") {
					BLConsts.BLName = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLXCoordOffset") {
					BLConsts.BLXCoordOffset = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLYCoordOffset") {
					BLConsts.BLYCoordOffset = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "BLZCoordOffset") {
					BLConsts.BLZCoordOffset = (const int)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharX") {
					ptrCharX = (int*)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharY") {
					ptrCharY = (int*)ReadDWord(Buffer, &position);
				} else if (ConstantName == "ptrCharZ") {
					ptrCharZ = (int*)ReadDWord(Buffer, &position);
				}
			}
			break;
		case 2: // Hook WND_PROC
			{
				BYTE Hook = ReadByte(Buffer, &position);
				if (Hook){
					WndProc = (WNDPROC)SetWindowLongPtr(TibiaWindowHandle, GWLP_WNDPROC, (LONG)WindowProc);
				} else {
					SetWindowLongPtr(TibiaWindowHandle, GWLP_WNDPROC, (LONG)WndProc);
				}
			}
			break;
		case 3: // Test
			{
				//const unsigned int* HeapHandlePointer = (const unsigned int*)0x772500;
				//const unsigned int MaxItems = 7441;
				//const unsigned int structSize = 19*sizeof(int);

				//PROCESS_HEAP_ENTRY phe;
				//phe.lpData = NULL;
				//unsigned int HeapEntryStartAddress=0;
				//while (HeapWalk((HANDLE)*HeapHandlePointer, &phe)) {
				//	if ((unsigned int)phe.cbData == MaxItems*structSize) {
				//		HeapEntryStartAddress = (unsigned int)phe.lpData;
				//		break;
				//	}
				//}
				/*const unsigned int* TibiaDat = (const unsigned int*)0x768C9C;
				const unsigned int* HeapEntryStartAddress = (const unsigned int*)(*TibiaDat + 0x8);
				char la[125];
				sprintf(la,"%x",*HeapEntryStartAddress);
				MessageBoxA(0,la,"la",0);*/
				Battlelist BL(BLConsts);
				char la[125];
				LocationDefinition Dist = {0, 0, 0};
				Dist.x = abs(*ptrCharX - BL.GetLocation("Seymour").x);
				Dist.y = abs(*ptrCharY - BL.GetLocation("Seymour").y);
				sprintf(la, "%d, %d", Dist.x, Dist.y);
				MessageBoxA(0, la, "Distances: x,y", 0);
			}
			break;
		case 4: // DisplayText
			{
				int TextId = ReadByte(Buffer, &position);
				int PosX = ReadWord(Buffer, &position);
				int PosY = ReadWord(Buffer, &position);
				int ColorRed = ReadWord(Buffer, &position);
				int ColorGreen = ReadWord(Buffer, &position);
				int ColorBlue = ReadWord(Buffer, &position);
				int Font = ReadWord(Buffer, &position);
				string Text = ReadString(Buffer, &position);

				char *lpText;
				
				lpText = new char [Text.size() +1];
				strcpy (lpText, Text.c_str());

				SetText(TextId, true, PosX, PosY, ColorRed, ColorGreen, ColorBlue, Font, lpText);
			}
			break;
		case 5: //RemoveText
			{
				int TextId = ReadByte(Buffer, &position);
				if(TextId > MAX_TEXT-1){break;}
				texts[TextId].used = false;
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
				/* Testing that every constant have a value */
				if(address == 0){break;}
				if(startadr == 0){break;}
				if(jmpback == 0){break;}
				if(showfps == 0){break;}
				if(jmpfps == 0){break;}

				InjectDisplay();
				InjectShowPlayer();
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
			MessageBoxA(0, "Pipe connection failed!", "DLL - Fatal Error", MB_ICONERROR);
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
		MessageBoxA(0, "Failed waiting for pipe, maybe pipe is not ready?.", "DLL - Fatal Error", 0);
	}
}




extern "C" bool APIENTRY DllMain (HMODULE hModule, DWORD reason, LPVOID reserved){
	switch (reason){
		case DLL_PROCESS_ATTACH:
        {
			//InjectShowPlayer();
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
