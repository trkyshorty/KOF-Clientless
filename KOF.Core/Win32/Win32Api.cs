using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace KOF.Core.Win32;

public class Win32Api
{
    [DllImport("ntdll.dll")]
    public static extern int NtQueryObject(IntPtr ObjectHandle, int
            ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength,
            ref int returnLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

    [DllImport("ntdll.dll")]
    public static extern uint NtQuerySystemInformation(int
        SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength,
        ref int returnLength);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr OpenMutex(UInt32 desiredAccess, bool inheritHandle, string name);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, ushort hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetCurrentProcess();

    [DllImport("user32.dll")]
    public static extern int SetWindowText(IntPtr hWnd, string text);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr VirtualAllocEx(IntPtr hpProcess, IntPtr lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("kernel32")]
    public static extern IntPtr VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int dwFreeType);

    [DllImport("kernel32.dll")]
    public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CreateMailslot(string lpName, uint nMaxMessageSize, uint lReadTimeout, IntPtr lpSecurityAttributes);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetMailslotInfo(IntPtr handle, IntPtr lpMaxMessageSize, out int lpNextSize, out int lpMessageCount, IntPtr lpReadTimeout);

    [DllImport("kernel32.dll")] //GetLastError function
    public static extern UInt32 GetLastError();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool ReadFile(IntPtr handle, byte[] bytes, int numBytesToRead, out int numBytesRead, IntPtr overlapped);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleA", SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
    [DllImport("kernel32.dll")]
    public static extern uint SuspendThread(IntPtr hThread);
    [DllImport("kernel32.dll")]
    public static extern int ResumeThread(IntPtr hThread);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SetActiveWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetStdHandle(UInt32 nStdHandle);

    [DllImport("kernel32.dll")]
    public static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

    public static bool Is64Bit()
    {
        return Marshal.SizeOf(typeof(IntPtr)) == 8 ? true : false;
    }

    public const int MEM_COMMIT = 0x1000;
    public const int MEM_RESERVE = 0x2000;
    public const int MEM_DECOMMIT = 0x4000;
    public const int MEM_RELEASE = 0x8000;

    public const int PAGE_EXECUTE_READWRITE = 0x40;
    public const int PAGE_READWRITE = 0x4;

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        Synchronize = 0x00100000
    }

    [Flags]
    public enum ThreadAccess : int
    {
        TERMINATE = (0x0001),
        SUSPEND_RESUME = (0x0002),
        GET_CONTEXT = (0x0008),
        SET_CONTEXT = (0x0010),
        SET_INFORMATION = (0x0020),
        QUERY_INFORMATION = (0x0040),
        SET_THREAD_TOKEN = (0x0080),
        IMPERSONATE = (0x0100),
        DIRECT_IMPERSONATION = (0x0200)
    }

    public static String AlignDWORD(IntPtr Value)
    {
        return AlignDWORD(Value.ToInt32());
    }

    public static String AlignDWORD(long Value)
    {
        String ADpStr, ADpStr2, ADresultStr;

        ADpStr = Convert.ToString(Value, 16);
        ADpStr2 = "";

        Int32 ADpStrLength = ADpStr.Length;

        int i = 0;
        for (i = 0; i < 8 - ADpStrLength; i++)
        {
            ADpStr2 = ADpStr2.Insert(i, "0");
        }

        int j = 0;
        int t = i;
        for (i = t; i < 8; i++)
        {
            ADpStr2 = ADpStr2.Insert(i, ADpStr[j].ToString());
            j++;
        }

        ADresultStr = "";

        ADresultStr = ADresultStr.Insert(0, ADpStr2[6].ToString());
        ADresultStr = ADresultStr.Insert(1, ADpStr2[7].ToString());
        ADresultStr = ADresultStr.Insert(2, ADpStr2[4].ToString());
        ADresultStr = ADresultStr.Insert(3, ADpStr2[5].ToString());
        ADresultStr = ADresultStr.Insert(4, ADpStr2[2].ToString());
        ADresultStr = ADresultStr.Insert(5, ADpStr2[3].ToString());
        ADresultStr = ADresultStr.Insert(6, ADpStr2[0].ToString());
        ADresultStr = ADresultStr.Insert(7, ADpStr2[1].ToString());

        return ADresultStr.ToUpper();
    }

    public static long AddressDistance(IntPtr Address, IntPtr TargetAddress)
    {
        return AddressDistance(Address.ToInt32(), TargetAddress.ToInt32());
    }

    public static long AddressDistance(long Address, long TargetAddress)
    {
        long Diff = Address - TargetAddress;

        if (Diff > 0)
            return (0xFFFFFFFB - Diff);
        else
            return TargetAddress - Address - 5;
    }

    public static byte[] StringToByte(string text)
    {
        var tmpbyte = new byte[text.Length / 2];
        var count = 0;
        for (int i = 0; i < text.Length; i += 2)
        {
            var val = byte.MinValue;
            try
            {
                if (text.Substring(i, 2) != "XX")
                    val = byte.Parse(text.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);

                tmpbyte[count] = val;
                count++;
            }
            catch (Exception)
            {
            }
        }
        return tmpbyte;
    }

    public static void WriteString(IntPtr Handle, IntPtr Address, string Value)
    {
        byte[] data = Encoding.Default.GetBytes(Value + "\0");
        IntPtr Zero = IntPtr.Zero;
        WriteProcessMemory(Handle, Address, data, data.Length + 1, (int)Zero);
    }

    public static Int32 Read4Byte(IntPtr Handle, IntPtr Address)
    {
        byte[] Buffer = new byte[4];
        Win32Api.ReadProcessMemory(Handle, Address, Buffer, 4, 0);
        return BitConverter.ToInt32(Buffer, 0);
    }

    public static Int32 Read4Byte(IntPtr Handle, long Address)
    {
        return Read4Byte(Handle, new IntPtr(Address));
    }

    public static void Write4Byte(IntPtr Handle, IntPtr Address, Int32 Value)
    {
        Win32Api.WriteProcessMemory(Handle, Address, BitConverter.GetBytes(Value), 4, 0);
    }

    public static void Patch(IntPtr Handle, IntPtr Addr, String Code)
    {
        byte[] PatchByte = StringToByte(Code.ToUpper());
        Win32Api.WriteProcessMemory(Handle, Addr, PatchByte, PatchByte.Length, 0);
    }
}
