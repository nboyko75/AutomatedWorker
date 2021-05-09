#include "keyboard.h"
#include <stdexcept>

Keyboard::Keyboard() : Device{ L"{884b96c3-56ef-11d1-bc8c-00a0c91405dd}" }
{}

void Keyboard::initialize()
{
    if (isInitialized()) throw std::runtime_error{"ERROR_DOUBLE_INITIALIZATION"};
    Device::initialize();
}

void Keyboard::type(BYTE keyCode)
{
    BYTE keyCodes[6] = {KEY_NONE};

    keyCodes[0] = keyCode;
    sendKeyboardReport(keyCodes);

    keyCodes[0] = KEY_NONE;
    sendKeyboardReport(keyCodes);
}

void Keyboard::abort()
{
    Device::abort();
}

void Keyboard::sendKeyboardReport(BYTE *keyCodes)
{
    Report report;
    report.reportId             = REPORT_ID;
    report.modifiers            = m_modifiers;
    report._reserved            = 0x00;
    std::memcpy(report.keyCodes, keyCodes, 6);

    setOutputReport(&report, static_cast<DWORD>(sizeof(Report)));
}
