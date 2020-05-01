using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UserModeApplication
{
    public class DriverComms
    {
        private readonly IntPtr driverHandle;

        public DriverComms(string registryPath)
        {
            driverHandle = WinApi.CreateFileA(registryPath, FileAccess.ReadWrite,
                FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        }

        public bool HasValidHandle()
        {
            return driverHandle != WinApi.INVALID_HANDLE_VALUE;
        }

        public IntPtr ReadVirtualMemory(ulong ProcessId, ulong ReadAddress, ulong Size)
        {
            IntPtr Buffer = IntPtr.Zero;

            CommunicationDefs.KERNEL_READ_REQUEST ReadRequest;

            ReadRequest.ProcessId = ProcessId;
            ReadRequest.Address = ReadAddress;

            //send the 'address' of the buffer so our driver can know where to write the data
            ReadRequest.pBuff = Buffer;

            ReadRequest.Size = Size;

            IntPtr operationPointer = MarshalUtility.CopyStructToMemory(ReadRequest);
            int operationSize = Marshal.SizeOf<CommunicationDefs.KERNEL_READ_REQUEST>();

            // send code to our driver with the arguments
            if (WinApi.DeviceIoControl(driverHandle, CommunicationDefs.IO_READ_REQUEST, operationPointer, operationSize, operationPointer, operationSize, IntPtr.Zero, IntPtr.Zero))
            {
                //return our buffer
                return Buffer;
            }

            // Marshal.FreeHGlobal(operationPointer);
            return Buffer;
        }
    }
}
