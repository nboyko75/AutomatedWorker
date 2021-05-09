#include "device.h"
#include <cfgmgr32.h>
#include <hidsdi.h>
#include <stdexcept>
#include <iostream>

Device::Device(PCWSTR devGuid) : mp_deviceGuid{ devGuid }
{}

void Device::initialize()
{
    if (isInitialized()) throw std::runtime_error{"ERROR_DOUBLE_INITIALIZATION"};

    GUID guid;
    CLSIDFromString(mp_deviceGuid, &guid);
    // HidD_GetHidGuid(&guid);

    ULONG deviceInterfaceListLength = 0;
    CONFIGRET configret = CM_Get_Device_Interface_List_Size(
        &deviceInterfaceListLength,
        &guid,
        nullptr,
        CM_GET_DEVICE_INTERFACE_LIST_PRESENT
    );

    if (CR_SUCCESS != configret) {
        throw std::runtime_error{"ERROR_GETTING_DEVICE_INTERFACE_LIST_SIZE"};
    }
    if (deviceInterfaceListLength <= 1) {
        throw std::runtime_error{"ERROR_EMPTY_DEVICE_INTERFACE_LIST"};
    }

    PWSTR deviceInterfaceList = (PWSTR) malloc(deviceInterfaceListLength * sizeof(WCHAR));
    if (nullptr == deviceInterfaceList) {
        throw std::runtime_error{"ERROR_ALLOCATING_DEVICE_INTERFACE_LIST"};
    }
    ZeroMemory(deviceInterfaceList, deviceInterfaceListLength * sizeof(WCHAR));

    configret = CM_Get_Device_Interface_List(
        &guid,
        nullptr,
        deviceInterfaceList,
        deviceInterfaceListLength,
        CM_GET_DEVICE_INTERFACE_LIST_PRESENT
    );
    if (CR_SUCCESS != configret) {
        free(deviceInterfaceList);
        throw std::runtime_error{"ERROR_GETTING_DEVICE_INTERFACE_LIST"};
    }

    std::wcout << L"mp_devGuid = " << std::wstring(mp_deviceGuid) << std::endl;
    // size_t deviceInterfaceLength = wcslen(mp_deviceInterface);
    HANDLE file = INVALID_HANDLE_VALUE;
    for (PWSTR currentInterface = deviceInterfaceList; *currentInterface; currentInterface += wcslen(currentInterface) + 1) {
        /* std::wcout << L"currentInterface = " << std::wstring(currentInterface) << std::endl;
        if (0 != wcsncmp(currentInterface, mp_deviceInterface, deviceInterfaceLength)) {
            continue;
        } */

        std::wcout << L"CreateFile " << std::wstring(currentInterface) << std::endl;
        file = CreateFile(currentInterface, GENERIC_WRITE, FILE_SHARE_WRITE, nullptr, OPEN_EXISTING, 0, nullptr);
        if (INVALID_HANDLE_VALUE == file) {
            std::wcout << L"CreateFile: INVALID_HANDLE_VALUE " << std::endl;
            continue;
        }

        break;
    }
    free(deviceInterfaceList);
    if (INVALID_HANDLE_VALUE == file) {
        throw std::runtime_error{"ERROR_INVALID_HANDLE_VALUE"};
    }

    mp_deviceHandle = file;
    m_isInitialized = true;
}

bool Device::isInitialized() const
{
    return m_isInitialized;
}

bool Device::isAborted() const
{
    return m_isAborted;
}

void Device::abort()
{
    if (nullptr != mp_deviceHandle && INVALID_HANDLE_VALUE != mp_deviceHandle) {
        CloseHandle(mp_deviceHandle);
    }
    m_isAborted = true;
}

void Device::setOutputReport(PVOID data, DWORD size)
{
    if (!HidD_SetOutputReport(mp_deviceHandle, data, size)) {
        throw std::runtime_error{"ERROR_SET_OUTPUT_REPORT"};
    }
}
