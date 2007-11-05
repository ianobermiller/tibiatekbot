#pragma once
#pragma comment(lib, "ws2_32.lib")


//---------------------------------------------------------------------------
//  OpenThemeData()     - Open the theme data for the specified HWND and 
//
//  hwnd                - window handle of the control/window to be themed
//
//  pszClassList        - class name (or list of names) to match to theme data
//
//---------------------------------------------------------------------------
void SendPacket(const char *packet, int len);

namespace TibiaTekBot3 {

#ifdef TIBIA800

const int * const CODECAVE = (int*)0x76DA20;
const int * const XTEAKEY = (int*)0x7637AC;
const int * const SOCKETADDR = (int*)0x763780;
const int * const WASDPOPUP = (int*)0x766E58;
const int * const INGAME = (int*)0x766DF8;
#define BUFFERADDR (0x75EF60)

#elif TIBIA792

const int * const CODECAVE = 0;
const int * const XTEAKEY = 0x75A8AC;
const int * const SOCKETADDR = 0x;
const int * const WASDISONLINE = 0;
const int * const WASDPOPUP = 0;
const int * const INGAME = 0;
#define INGAME (0)
#define BUFFERADDR (0)
#endif

const int * const CODECAVETTBHANDLE = CODECAVE+1; //4 bytes after CODECAVE

struct Test {
	int number1;
	int number2;
	char name[10];
	int number3;
};

	enum MSGTYPE {
		WM_BEGIN = 0x4C7,
		WM_RECV,
		WM_SEND,
		WM_INJECTED,
		WM_HOOKED,
		WM_UNHOOKED,
		WM_UNINJECTED,
		WM_UNINJECT,
		WM_WASD,
		WM_MENU,
		WM_SHOWMENU,
		WM_PING,
		WM_TEST,
		WM_TEXTMENU,
		WM_TEXTMENUMOVE,
		WM_END
	};

	enum MENUTYPE {
		MN_BEGIN = 0x400,
		MN_MENU,
		MN_OPTIONS,
		MN_ABOUT,
		MN_SM,
		MN_TEST,
		MN_END
	};

	enum WASDSTATE {
		WASD_INACTIVE,
		WASD_ACTIVE
	};

	enum WASDSMSTATE {
		WASD_SM_INACTIVE,
		WASD_SM_ACTIVE
	};

}
