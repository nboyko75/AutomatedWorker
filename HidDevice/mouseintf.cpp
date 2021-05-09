#include "mouse.h"
#include <iostream>
#include "mouseintf.h"

Mouse* CreateMouse()
{
    Mouse* mouse = new Mouse();
    try {
        mouse->initialize();
    }
    catch (const std::runtime_error& e) {
        std::cout << std::string("Mouse initialization: ") + e.what() << std::endl;
        return NULL;
    }
    return mouse;
}

void DisposeMouse(Mouse* mouse)
{
    if (mouse != NULL)
    {
        delete mouse;
        mouse = NULL;
    }
}

void leftButtonDown(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->leftButtonDown();
    }
}

void leftButtonUp(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->leftButtonUp();
    }
}

void leftButtonClick(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->leftButtonClick();
    }
}

void rightButtonDown(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->rightButtonDown();
    }
}

void rightButtonUp(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->rightButtonUp();
    }
}

void rightButtonClick(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->rightButtonClick();
    }
}

void middleButtonDown(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->middleButtonDown();
    }
}

void middleButtonUp(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->middleButtonUp();
    }
}

void middleButtonClick(Mouse* mouse)
{
    if (mouse != NULL)
    {
        mouse->middleButtonClick();
    }
}

void moveBy(Mouse* mouse, int x, int y)
{
    if (mouse != NULL)
    {
        mouse->moveBy(x, y);
    }
}

void moveTo(Mouse* mouse, int x, int y)
{
    if (mouse != NULL)
    {
        mouse->moveTo(x, y);
    }
}