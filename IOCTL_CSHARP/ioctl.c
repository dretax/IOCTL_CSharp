#pragma warning( disable : 4100 4047 4024 4022 4201 4311 4057 4213 4189 4081 4189 4706 4214 4459 4273 4127)

#include "ioctl.h"
#include <ntddk.h>
#include <ntdef.h>
#include "communication.h"
#include "ntos.h"
#include "data.h"
#include "messages.h"


NTSTATUS UnloadDriver(PDRIVER_OBJECT pDriverObject)
{
	DebugMessageNormal("======================================\n");
	DebugMessageNormal("IOCTL Basic Setup for C# Apps By DreTaX\n");
	DebugMessageNormal("My mentor: Life45\n");
	DebugMessageNormal("Shutting down...\n");
	IoDeleteSymbolicLink(&dos);
	IoDeleteDevice(pDriverObject->DeviceObject);

	return STATUS_SUCCESS;
}

// Driver Entrypoint
NTSTATUS DriverEntry(PDRIVER_OBJECT pDriverObject, PUNICODE_STRING pRegistryPath)
{
	DebugMessageNormal("======================================\n");
	DebugMessageNormal("IOCTL Basic Setup for C# Apps By DreTaX\n");
	DebugMessageNormal("My mentor: Life45\n");
	DebugMessageNormal("Starting...\n");

	RtlInitUnicodeString(&dev, L"\\Device\\dretaxioctlsample");
	RtlInitUnicodeString(&dos, L"\\DosDevices\\dretaxioctlsample");

	IoCreateDevice(pDriverObject, 0, &dev, FILE_DEVICE_UNKNOWN, FILE_DEVICE_SECURE_OPEN, FALSE, &pDeviceObject);
	IoCreateSymbolicLink(&dos, &dev);

	pDriverObject->MajorFunction[IRP_MJ_CREATE] = CreateCall;
	pDriverObject->MajorFunction[IRP_MJ_CLOSE] = CloseCall;
	pDriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = IoControl;
	pDriverObject->DriverUnload = UnloadDriver;

	pDeviceObject->Flags |= DO_DIRECT_IO;
	pDeviceObject->Flags &= ~DO_DEVICE_INITIALIZING;

	return STATUS_SUCCESS;
}