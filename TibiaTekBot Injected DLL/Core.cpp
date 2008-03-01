#include "stdafx.h"

#include <windows.h>
#include <string>
#include "Core.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace std;

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
				const unsigned int* TibiaDat = (const unsigned int*)0x768C9C;
				const unsigned int* HeapEntryStartAddress = (const unsigned int*)(*TibiaDat + 0x8);
				char la[125];
				sprintf(la,"%x",*HeapEntryStartAddress);
				MessageBoxA(0,la,"la",0);
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
            hMod = hModule;
			string CmdArgs = (const char*)0x152310;
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
