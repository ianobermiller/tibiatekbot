// TibiaTekBot3Loader.cpp : Defines the entry point for the DLL application.
//
#include "stdafx.h"
#include "windows.h"

#ifdef _MANAGED
#pragma managed(push, off)
#endif

bool APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved){
    return true;
}

bool __stdcall InjectLibrary(int dwProcessId, char *strDll){
	HANDLE hThread;
	HANDLE hProcess;
	hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, dwProcessId);
	if (!hProcess)
		return false;
	LPVOID lpRemoteAddress = VirtualAllocEx(hProcess, 0, strlen(strDll), MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
	if(lpRemoteAddress){
		if(!WriteProcessMemory(hProcess, lpRemoteAddress, strDll, strlen(strDll), 0))
			return false;
		hThread = CreateRemoteThread(hProcess, 0, 0, (LPTHREAD_START_ROUTINE)GetProcAddress(GetModuleHandle(L"Kernel32"), "LoadLibraryA"), lpRemoteAddress, 0, 0);
		if(hThread){
			WaitForSingleObject(hThread, INFINITE);
			VirtualFree(lpRemoteAddress, strlen(strDll), MEM_RELEASE);
			CloseHandle(hProcess);
			return true;
		} else
			VirtualFree(lpRemoteAddress, strlen(strDll), MEM_RELEASE);
	}
	CloseHandle(hProcess);
	return false;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif