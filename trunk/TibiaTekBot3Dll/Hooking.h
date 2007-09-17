#pragma once
#pragma comment(lib, "imagehlp.lib")
/* Structures */
typedef struct 
{
	PCSTR pszDll;				//DLL containing the hooked function
	PCSTR pszFuncName;			//name of the hooked function
	PROC  pfnHookFunc;			//pointer to the function that should be called instead of the original function
	PROC  pfnOriginalFunc;		//pointer to the original function
	PROC* ppfnIATAddress;		//address of the function in the IAT
	BOOL  bHooked;				//hooked? (yes/no)
} HookInfo;

VOID Hook(HookInfo *pHookInfo);					//function to set a hook
VOID UnHook(HookInfo *pHookInfo);					//function to remove a hook
