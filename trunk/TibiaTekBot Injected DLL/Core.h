#pragma once
//#pragma comment(lib, "ws2_32.lib")

/*
#pragma pack(push)
#pragma pack(1)
#pragma pack(pop)
*/

#ifdef TIBIA810

const int * WASDPOPUP = 0;//(int*)0x76C328; //Updated to 8.10
const int * INGAME = 0;//(int*)0x76C2C8;// Updated to 8.10

#elif TIBIA800

const int * const WASDPOPUP = (int*)0x766E58;
const int * const INGAME = (int*)0x766DF8;

#elif TIBIA792

const int * const WASDPOPUP = 0;
const int * const INGAME = 0;

#endif
