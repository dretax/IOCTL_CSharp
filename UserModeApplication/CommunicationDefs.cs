using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UserModeApplication
{
    public static class CommunicationDefs
    {
        public static readonly uint IO_READ_REQUEST = CTL_CODE(WinApi.FILE_DEVICE_UNKNOWN, 0x0666, WinApi.METHOD_BUFFERED, WinApi.FILE_ANY_ACCESS);

        [StructLayout(LayoutKind.Sequential)]
        public struct KERNEL_READ_REQUEST
        {
            public ulong ProcessId;
            public ulong Address;
            public IntPtr pBuff;
            public ulong Size;
        }

        private static uint CTL_CODE(int deviceType, int function, int method, int access)
        {
            return (uint)(((deviceType) << 16) | ((access) << 14) | ((function) << 2) | (method));
        }
    }
}
