#pragma once
#include <ntifs.h>

// We store every value here.
// Thanks Zeromemory for the tuts, and Life45 for explaining.
PDEVICE_OBJECT pDeviceObject;
UNICODE_STRING dev, dos;