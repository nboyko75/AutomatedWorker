#pragma once
#include "mouse.h"

extern "C" {
	__declspec(dllexport) Mouse* CreateMouse();
	__declspec(dllexport) void DisposeMouse(Mouse* mouse);
	__declspec(dllexport) void leftButtonDown(Mouse* mouse);
	__declspec(dllexport) void leftButtonUp(Mouse* mouse);
	__declspec(dllexport) void leftButtonClick(Mouse* mouse);
	__declspec(dllexport) void rightButtonDown(Mouse* mouse);
	__declspec(dllexport) void rightButtonUp(Mouse* mouse);
	__declspec(dllexport) void rightButtonClick(Mouse* mouse);
	__declspec(dllexport) void middleButtonDown(Mouse* mouse);
	__declspec(dllexport) void middleButtonUp(Mouse* mouse);
	__declspec(dllexport) void middleButtonClick(Mouse* mouse);
	__declspec(dllexport) void moveBy(Mouse* mouse, int x, int y);
	__declspec(dllexport) void moveTo(Mouse* mouse, int x, int y);
}