#pragma once

#include <string>

typedef void _PrintText(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
static _PrintText *PrintText = (_PrintText*)0x4A3C00;

void SetText(unsigned int TextNum, bool enabled, int nX, int nY, int nRed, int nGreen, int nBlue, int font, std::string lpText);
bool CompareLists(PlayerText first, PlayerText second);
void MyPrintName(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
void MyPrintFps(int nSurface, int nX, int nY, int nFont, int nRed, int nGreen, int nBlue, char* lpText, int nAlign);
DWORD HookCall(DWORD dwAddress, DWORD dwFunction);
void UnhookCall(DWORD dwAddress, DWORD dwOldCall);
void Nop(DWORD dwAddress, int size);
