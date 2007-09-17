#include "stdafx.h"
#include "Windows.h"
#include <imagehlp.h>
#include "Hooking.h"

VOID UnHook(HookInfo *pHookInfo){
	if(!pHookInfo->bHooked)
		return;
	MEMORY_BASIC_INFORMATION mbi;
	VirtualQuery(pHookInfo->ppfnIATAddress, &mbi, sizeof(MEMORY_BASIC_INFORMATION));
	if (!VirtualProtect(mbi.BaseAddress, mbi.RegionSize, PAGE_READWRITE, &mbi.Protect))
		return;
	*(pHookInfo->ppfnIATAddress) = *(pHookInfo->pfnOriginalFunc);
	DWORD dwOldProtect;
	VirtualProtect(mbi.BaseAddress, mbi.RegionSize,	mbi.Protect, &dwOldProtect);
	pHookInfo->bHooked = FALSE;
}


VOID Hook(HookInfo *pHookInfo){
	if(pHookInfo->bHooked)
		return;
	pHookInfo->bHooked = FALSE;
	PROC pfnOrig = NULL;	
	HMODULE hmod = NULL;
	hmod = LoadLibraryA(pHookInfo->pszDll);
	pfnOrig = GetProcAddress(GetModuleHandleA(pHookInfo->pszDll),pHookInfo->pszFuncName);
	pHookInfo->pfnOriginalFunc = pfnOrig;
	if(!pfnOrig)
		return;

	HMODULE hmodCaller = GetModuleHandleA(NULL);
	
	ULONG ulSize;
	// Get the address of the module's import section
	PIMAGE_IMPORT_DESCRIPTOR pImportDesc = (PIMAGE_IMPORT_DESCRIPTOR)ImageDirectoryEntryToData(hmodCaller,
		TRUE, 
		IMAGE_DIRECTORY_ENTRY_IMPORT, 
		&ulSize
	);

	if (!pImportDesc)
		return;

	while (pImportDesc->Name){
		PSTR pszModName = (PSTR)((PBYTE) hmodCaller + pImportDesc->Name);
		if (!stricmp(pszModName, pHookInfo->pszDll))
			break;   // Found
		pImportDesc++;
	} // while
	if (!pImportDesc->Name)
		return;

	// Get caller's IAT 
	PIMAGE_THUNK_DATA pThunk = (PIMAGE_THUNK_DATA)( (PBYTE) hmodCaller + pImportDesc->FirstThunk);

	PROC pfnCurrent = pfnOrig;	
	// Replace current function address with new one
	while (pThunk->u1.Function)	{
		// Get the address of the function address
		PROC* ppfn = (PROC*)&pThunk->u1.Function;
		// Is this the function we're looking for?
		BOOL bFound = (*ppfn == pfnCurrent);
		if (bFound)	{
			pHookInfo->ppfnIATAddress = ppfn;
			MEMORY_BASIC_INFORMATION mbi;
			VirtualQuery(ppfn, &mbi, sizeof(MEMORY_BASIC_INFORMATION));
			// In order to provide writable access to this part of the 
			// memory we need to change the memory protection
			if (!VirtualProtect(mbi.BaseAddress, mbi.RegionSize, PAGE_READWRITE, &mbi.Protect))
				return;
			// Hook the function.
			*ppfn = *(pHookInfo->pfnHookFunc);
			// Restore the protection back
            DWORD dwOldProtect;
			VirtualProtect(mbi.BaseAddress, mbi.RegionSize, mbi.Protect, &dwOldProtect);
			break;
		} // if
		pThunk++;
	} // while
	pHookInfo->bHooked = TRUE;
	return;
}
