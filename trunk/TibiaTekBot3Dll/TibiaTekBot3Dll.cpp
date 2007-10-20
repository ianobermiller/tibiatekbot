#include "stdafx.h"
#include "afxres.h"
#include "resource.h"
#include <windows.h>
#include <Winsock2.h>
#include "TibiaTekBot3Dll.h"
#include "Encryption.h"
#include "Hooking.h"
#include <stdlib.h>

#ifdef _MANAGED
#pragma managed(push, off)
#endif

using namespace TibiaTekBot3;

/* DLL Injection Related Stuff */
WNDPROC WndProc;
HINSTANCE hMod;
HWND *TibiaWindowHandle;
HWND *TTBWindowHandle;
DWORD TTBProcessID;
HANDLE TTBProcessHandle;
//CRITICAL_SECTION CS_TTBRecv;
CRITICAL_SECTION CS_TTBSend;
void __declspec(noreturn) UninjectSelf(HMODULE);	//function to unload this DLL
void StartUninjectSelf();

/* Main Menu Stuff */
HMENU mainmenu;
void ShowMenu();
void HideMenu();

/* System Menu Stuff */
HMENU subsys;
void AddSysMenu();
void RemSysMenu();
LRESULT __stdcall WindowProc(HWND hWnd, int uMsg, WPARAM wParam, LPARAM lParam);

/* Hooking Stuff */
//int WSAAPI TTBRecv( IN SOCKET s, OUT char FAR * buf, IN int len, IN int flags );
int WSAAPI TTBSend( IN SOCKET s, IN const char FAR * buf, IN int len, IN int flags);
HookInfo g_HookSend;
//HookInfo g_HookRecv;

/* Packet Stuff */
BYTE *packetbuffer;
unsigned long packetsize=0;
unsigned long packetpos=0;
//void parsePacketFromServer(BYTE *xData);
void SendPacket( const char *packet, int len );					//function to send packets
//void __declspec(noreturn) RecvPacket();				//function to recv packets
//void parsePacketFromServer(BYTE *xData);

/* WASD Stuff */
bool bWASD = false;
bool bSayMode = false;

/* Text Menu Stuff  */
bool bTextMenu = false; // If TextMenu == True, we are showing textmenu so hook the keys 0-9
HWND WindowHwnd; //Handle for window

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

void SendPacket(const char *packet, int len){
	try {
		int flags = 0;
		int **SocketPtr = (int**)SOCKETADDR;
		int a = *(*SocketPtr+1);
		send(a, packet, len, flags);
	} catch (...) {
		MessageBox(*TibiaWindowHandle, L"SendPacket -> Unable to send packet to server.", L"TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void AddSysMenu(){
	try {
		HMENU sys = GetSystemMenu(*TibiaWindowHandle, FALSE);
		AppendMenu(sys, MF_SEPARATOR, 0, 0);
		subsys = CreatePopupMenu();
		AppendMenu(subsys, MF_STRING, 0x401, L"&Menu");
		AppendMenu(subsys, MF_STRING, 0x402, L"&Options");
		AppendMenu(subsys, MF_STRING, 0x403, L"&About");
		AppendMenu(subsys, MF_STRING, 0x404, L"&Switch Menu Mode");
		AppendMenu(subsys, MF_STRING, 0x405, L"&Test");
		AppendMenu(sys, MF_POPUP, (UINT_PTR)subsys, L"TibiaTek Bot");

		/* Set TTB Icon */
		HICON icon = (HICON)LoadImage(hMod, MAKEINTRESOURCE(IDI_ICON1), IMAGE_ICON, 13,13,LR_LOADTRANSPARENT );
		ICONINFO ii;
		GetIconInfo(icon, &ii);
		DeleteObject(ii.hbmMask);
		DestroyIcon(icon);
		SetMenuItemBitmaps(sys, (UINT)subsys, MF_BYCOMMAND, ii.hbmColor, 0);
	} catch (...){
		MessageBox(*TibiaWindowHandle, L"AddSysMenu -> Unable to add TibiaTek Bot submenu to system menu.", L"TibiaTek Bot - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void RemSysMenu(){
	try {
		HMENU sys = GetSystemMenu(*TibiaWindowHandle, false);
		RemoveMenu(sys, (UINT)subsys, MF_BYCOMMAND);
		DestroyMenu(subsys);
		RemoveMenu(sys, GetMenuItemCount(sys)-1, MF_BYPOSITION);
	} catch (...){
		MessageBox(*TibiaWindowHandle, L"RemSysMenu -> Unable to remove TibiaTek Bot submenu from system menu.", L"TibiaTek Bot - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void ShowMenu(){
	try {
		if (mainmenu)
			DestroyMenu(mainmenu);
		mainmenu = CreateMenu();
		AppendMenu(mainmenu, MF_STRING, 0x401, L"&Menu");
		AppendMenu(mainmenu, MF_STRING, 0x402, L"&Options");
		AppendMenu(mainmenu, MF_STRING, 0x403, L"&About");
		AppendMenu(mainmenu, MF_STRING, 0x404, L"&Switch Menu Mode");
		AppendMenu(mainmenu, MF_STRING, 0x405, L"&Test");
		SetMenu(*TibiaWindowHandle, mainmenu);
		DrawMenuBar(*TibiaWindowHandle);
	} catch (...){
		MessageBox(*TibiaWindowHandle, L"ShowMenu -> Unable to add menu to Tibia.", L"TibiaTek Bot - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void HideMenu(){
	try {
		DestroyMenu(mainmenu);
		SetMenu(*TibiaWindowHandle, 0);
		DrawMenuBar(*TibiaWindowHandle);
		mainmenu = (HMENU)0;
	} catch (...){
		MessageBox(*TibiaWindowHandle, L"HideMenu -> Unable to remove menu from Tibia.", L"TibiaTek Bot - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void ReadBytesFromMemory(int address, BYTE *buffer, int length){
	try {
		static BYTE b;
		static SIZE_T np;
		for (int i=0; i<length; i++){
			ReadProcessMemory(TTBProcessHandle, (LPCVOID)(address+i), (LPVOID)&b, 1, &np);
			buffer[i] = b;
		}
	} catch (...){
		MessageBox(*TibiaWindowHandle, L"ReadBytesFromMemory -> Unable to read memory bytes from process.", L"TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
		StartUninjectSelf();
	}
}

void StartUninjectSelf(){
	try {
		if (mainmenu)
			HideMenu();
		SetWindowLongPtr(*TibiaWindowHandle, GWLP_WNDPROC, (LONG)WndProc);
		CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)UninjectSelf, hMod, NULL, NULL);
	} catch (...) {
		MessageBox(*TibiaWindowHandle, L"StartUninjectSelf -> Unable to uninject from process.", L"TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

bool Ping(int timeout = 2000){
	try {
		unsigned long result = 0;
		SendMessageTimeout(*TTBWindowHandle, WM_PING, 0,0, SMTO_ABORTIFHUNG | SMTO_NOTIMEOUTIFNOTHUNG, timeout, (PDWORD_PTR)&result);
		return (result == 1);
	} catch(...){
		MessageBoxA(*TibiaWindowHandle, "Ping -> Unable to ping the TibiaTek Bot process.", "TibiaTek Bot  - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
	}
}

void UninjectIfHung(){
	if (!Ping(1000)) {
		MessageBoxA(*TibiaWindowHandle, "TibiaTek Bot seems not responsive, it will automatically uninject itself from the Tibia process.", "TibiaTek Bot - Warning", MB_ICONWARNING & MB_TOPMOST & MB_OK);
		StartUninjectSelf();
	}
}

bool InGame(){
	return (*INGAME == 8);
}

void myTest(){
	struct Test *test = new struct Test;
	test->number1 = 0xA;
	test->number2 = 0xB;
	test->number3 = 0xC;
	strcpy(test->name, "arturo");
	SendMessage(*TTBWindowHandle, WM_TEST, sizeof(struct Test), (LPARAM)test);
	delete test;
}
/*
int WSAAPI TTBRecv(IN SOCKET s, OUT char FAR * buf, IN int len, IN int flags){
	//Step 1: get the data packets from the real server
	static unsigned long iRet=0;
	static int result=0;
	EnterCriticalSection(&CS_TTBRecv);
	try {
		iRet = recv(s, buf, len, flags);	
		if (iRet > 0 && InGame()){
			unsigned long bufferpos = 0;
			do {
				if (packetpos == 0){
					packetsize = (unsigned long)((BYTE)buf[bufferpos]) + (unsigned long)(((BYTE)buf[bufferpos+1])*256) + 2;
					packetbuffer = new BYTE[packetsize];
					for (unsigned long i=0;i<min(iRet-bufferpos,packetsize);i++)
						packetbuffer[i] = (BYTE)buf[i+bufferpos];
					packetpos = min(iRet-bufferpos,packetsize);
					bufferpos += min(iRet-bufferpos,packetsize);
				} else {
					for (unsigned long i=0;i<min(iRet,packetsize - packetpos);i++)
						packetbuffer[i+packetpos] = (BYTE)buf[i+bufferpos];
					bufferpos += min(iRet,packetsize - packetpos);
					packetpos += min(iRet,packetsize - packetpos);
				}
				if (packetpos == packetsize){
					packetpos = 0;
					BYTE *xData = getDecryptedCopy(packetbuffer, packetsize);
					if(packetsize != 0) {
						result = 0;
						SendMessageTimeout(*TTBWindowHandle, WM_RECV, (WPARAM)packetsize, (LPARAM)(xData+2), SMTO_ABORTIFHUNG | SMTO_NOTIMEOUTIFNOTHUNG, 1000, (PDWORD_PTR)&result);
						if (!result) StartUninjectSelf();
					}
					delete[] xData;
					delete[] packetbuffer;
				}
			} while (bufferpos < iRet);
		} 
	} catch (...){
		MessageBoxA(*TibiaWindowHandle, "TTBRecv -> Unable to receive packet from server.", "TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
		StartUninjectSelf();
	}
	LeaveCriticalSection(&CS_TTBRecv);
	return iRet;
}

void parsePacketFromServer(BYTE *xData){
	
}
*/
int WSAAPI TTBSend(IN SOCKET s, IN const char FAR * buf, IN int len, IN int flags){
	static int iRet = 0;
	static int i;
	try {
		EnterCriticalSection(&CS_TTBSend);
		i = len;
		if(len > 0 && InGame()) {
		BYTE *xData = getDecryptedCopy((BYTE*)buf, len);
		// parsePacketFromClient();
		i = (int)SendMessage(*TTBWindowHandle, WM_SEND, (WPARAM)len, (LPARAM)xData);
		delete[] xData;
		}
		if (i != 0) iRet = send(s, buf, i,flags );
	} catch (...){
		MessageBoxA(*TibiaWindowHandle, "TTBSend -> Unable to receive packet from client.", "TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
		StartUninjectSelf();
	}
	LeaveCriticalSection(&CS_TTBSend);
	return iRet;

}

LRESULT __stdcall WindowProc(HWND hWnd, int uMsg, WPARAM wParam, LPARAM lParam)
{
	switch(uMsg){
	case WM_TEXTMENU:
		{
			PostMessage(*TTBWindowHandle, WM_TEXTMENU, !bTextMenu, 0);
			bTextMenu = !bTextMenu;
			//if(bTextMenu) { 
			//	PostMessage(*TTBWindowHandle, WM_TEXTMENU, 1, 0);
			//	bTextMenu = true;
			//} else {
			//	PostMessage(*TTBWindowHandle, WM_TEXTMENU, 0, 0);
			//	bTextMenu = false;
			//}
		}
		break;
	case WM_PING:
		{
			return 1;
		}
	case WM_SYSCOMMAND:
	case WM_COMMAND:
		{
			try {
				if (wParam == MN_TEST){
					Beep(0x7ff, 100);
					myTest();
					break;
				}
				if (wParam > MN_BEGIN && wParam < MN_END){
					POINT p;
					GetCursorPos(&p);
					//SetForegroundWindow(*TTBWindowHandle);
					DWORD point = (p.x<< 0x10) + ((WORD)p.y);
					SendMessage(*TTBWindowHandle, WM_SHOWMENU, wParam, point);
				}
			} catch (...){
				MessageBoxA(*TibiaWindowHandle, "WindowProc -> Unable to send WM_SHOWMENU to TibiaTek Bot.", "TibiaTek Bot - Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
			}
		}
		break;
	case WM_MENU:
		{
			if (wParam){
				if (!mainmenu) ShowMenu();
			} else {
				if (mainmenu) HideMenu();
			}
		}
		break;
	case WM_SEND:
		{
			try {
				BYTE *buffer = new BYTE[(int)wParam + 2];
				ReadBytesFromMemory(lParam, buffer+2, wParam);
				buffer[0] = (BYTE)wParam;
				buffer[1] = (BYTE)((int)wParam >> 8);
				BYTE *packet = getEncryptedCopy(buffer, (int)wParam+2);
				SendPacket((const char*)packet, wParam+2);
				delete[] buffer;
				delete[] packet;
			} catch (...){
				MessageBoxA(*TibiaWindowHandle, "WindowProc -> Unable to send packet to server through WM_SEND.", "TibiaTek Bot - Fatal Error", MB_ICONERROR & MB_TOPMOST & MB_OK);
				StartUninjectSelf();
			}
			return 1;
		}
	case WM_RECV:
		{
			return 1;
		}
	case WM_UNINJECT:
		{
			StartUninjectSelf();
		}
		break;
	case WM_WASD:
		{
			//UninjectIfHung();
			bWASD = (wParam == WASD_ACTIVE);
			bSayMode = (lParam == WASD_SM_ACTIVE);
			return 1;
		}
	case WM_CHAR:
		{
			if(bTextMenu && (wParam >= '0' && wParam <= '9')){
				return 0;
			}
			if(bWASD && !bSayMode && InGame() && *WASDPOPUP != 11 && !bTextMenu)
			{
				switch(wParam)
				{
				case 'y':
				case 'Y':
					{
						bSayMode = true;
						SendMessage(*TTBWindowHandle, WM_WASD, (WPARAM)WASD_ACTIVE, (LPARAM)WASD_SM_ACTIVE);
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
				case 0x0D:
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
			break;
		}
	case WM_KEYDOWN:
		{
			if (bTextMenu && (wParam >= '0' && wParam <= '9')){
				PostMessage(*TTBWindowHandle, WM_TEXTMENU, 1, wParam);
				return 0;
			} else if(wParam == VK_PRIOR) {
				bTextMenu = !bTextMenu;
				//PostMessage(*TTBWindowHandle, WM_TEXTMENU, bTextMenu, 0);
			} else if (wParam == VK_NEXT) {
				//Beep(0x7D0, 100);
			}
			if (bWASD && InGame() && *WASDPOPUP != 11) {
				if(bSayMode) {
					if(wParam == VK_ESCAPE || wParam == VK_RETURN)
					{
						bSayMode = false;
						SendMessage(*TTBWindowHandle, WM_WASD, (WPARAM)WASD_ACTIVE, (LPARAM)WASD_SM_INACTIVE);
						return 1;
					}
				} else {
					if(wParam == 'W')
						wParam = VK_UP;
					else if(wParam == 'A')
						wParam = VK_LEFT;
					else if(wParam == 'S')
						wParam = VK_DOWN;
					else if(wParam == 'D')
						wParam = VK_RIGHT;
				}
			}
		}
		break;
	case WM_MOVE:
		{
			if (bTextMenu)
				PostMessage(*TTBWindowHandle, WM_TEXTMENUMOVE, 0, lParam);
			//if SendMessage is used, the tibia window waits until the text menu is shown
		}
	}
    return CallWindowProc(WndProc, hWnd, uMsg, wParam, lParam );
}
/*
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX); 

	wcex.style			= CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc	= (WNDPROC)WndProc;
	wcex.cbClsExtra		= 0;
	wcex.cbWndExtra		= 0;
	wcex.hInstance		= hInstance;
	wcex.hIcon			= NULL;
	wcex.hCursor		= LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground	= (HBRUSH)(COLOR_WINDOW+1);
	wcex.lpszMenuName	= NULL;
	wcex.lpszClassName	= (LPCWSTR)szWindowClass;
	wcex.hIconSm		= NULL;

	return RegisterClassEx(&wcex);
}
*/
extern "C" bool APIENTRY DllMain (HMODULE hModule,
                       DWORD reason,
                       LPVOID reserved)
{
    switch (reason)
    {
      case DLL_PROCESS_ATTACH:
        {
//			InitializeCriticalSection(&CS_TTBRecv);
			InitializeCriticalSection(&CS_TTBSend);
			packetpos=0;
			packetsize=0;
	
            hMod = hModule;
			TibiaWindowHandle = (HWND*)CODECAVE;
			TTBWindowHandle = (HWND*)CODECAVETTBHANDLE;

			GetWindowThreadProcessId(*TTBWindowHandle, &TTBProcessID);
			if (!TTBProcessID){
				PostMessage(*TTBWindowHandle, WM_INJECTED, 0, 0);
				return true;
			}
			TTBProcessHandle = OpenProcess(PROCESS_ALL_ACCESS, false, TTBProcessID);
			if (!TTBProcessHandle){
				PostMessage(*TTBWindowHandle, WM_INJECTED, 0, 0);
				return true;
			}

			WndProc = (WNDPROC)SetWindowLongPtr(*TibiaWindowHandle, GWLP_WNDPROC, (LONG)WindowProc);

			g_HookSend.bHooked=FALSE;
			g_HookSend.pfnHookFunc=(PROC)TTBSend;
			g_HookSend.pfnOriginalFunc=NULL;
			g_HookSend.ppfnIATAddress=NULL;
			g_HookSend.pszDll="WS2_32.DLL";
			g_HookSend.pszFuncName="send";
			Hook(&g_HookSend);

			PostMessage(*TTBWindowHandle, WM_HOOKED, 0, 1);
/*
			g_HookRecv.bHooked=FALSE;
			g_HookRecv.pfnHookFunc=(PROC)TTBRecv;
			g_HookRecv.pfnOriginalFunc=NULL;
			g_HookRecv.ppfnIATAddress=NULL;
			g_HookRecv.pszDll="WS2_32.DLL";
			g_HookRecv.pszFuncName="recv";
			Hook(&g_HookRecv);

			PostMessage(*TTBWindowHandle, WM_HOOKED, 0, 2);
*/
			/* Replace Tibia's Icon with TTB's */
			HICON icon = LoadIcon(hMod, MAKEINTRESOURCE(IDI_ICON1));
			PostMessage(*TibiaWindowHandle, WM_SETICON, ICON_BIG, (LPARAM)icon);

			/* Add TTB Menu to System Menu */
			AddSysMenu();

			/* Tell TTB we are fully Injected */
			PostMessage(*TTBWindowHandle, WM_INJECTED, 1, 0); //if wParam were zero, failed to inject
        }
        break;
      case DLL_PROCESS_DETACH:
        {
			/* Unhook Send and Recv */
			UnHook(&g_HookSend);
			PostMessage(*TTBWindowHandle, WM_UNHOOKED, 0, 1);
//			UnHook(&g_HookRecv);
//			PostMessage(*TTBWindowHandle, WM_UNHOOKED, 0, 2);

//			DeleteCriticalSection(&CS_TTBRecv);
			DeleteCriticalSection(&CS_TTBSend);

			/* Restore Tibia's title */
			SetWindowText(*TibiaWindowHandle, L"Tibia   ");

			/* Put Tibia's own icon back */
			//HICON icon = LoadIcon(hMod, MAKEINTRESOURCE(IDI_ICON2));
			//PostMessage(*TibiaWindowHandle, WM_SETICON, ICON_BIG, (LPARAM)icon);
			PostMessage(*TibiaWindowHandle, WM_SETICON, ICON_SMALL, 0);
			PostMessage(*TibiaWindowHandle, WM_SETICON, ICON_BIG, 0);

			/* Clean system menu */
			RemSysMenu();

			if (TTBProcessHandle)
				CloseHandle(TTBProcessHandle);

			PostMessage(*TTBWindowHandle, WM_UNINJECTED, 0, 0);
        }
        break;
    }

    return true;
}
