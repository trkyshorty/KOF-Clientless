using System.Text;
using System.Runtime.InteropServices;

namespace KOF.Cryptography;

public static class Password {
    private static readonly byte[] _encodingArray = { 0x1a, 0x1f, 0x11, 0x0a, 0x1e, 0x10, 0x18, 0x02, 0x1d, 0x08, 0x14, 0x0f, 0x1c, 0x0b, 0x0d, 0x04, 0x13, 0x17, 0x00, 0x0c, 0x0e, 0x1b, 0x06, 0x12, 0x15, 0x03, 0x09, 0x07, 0x16, 0x01, 0x19, 0x05, 0x12, 0x1d, 0x07, 0x19, 0x0f, 0x1f, 0x16, 0x1b, 0x09, 0x1a, 0x03, 0x0d, 0x13, 0x0e, 0x14, 0x0b, 0x05, 0x02, 0x17, 0x10, 0x0a, 0x18, 0x1c, 0x11, 0x06, 0x1e, 0x00, 0x15, 0x0c, 0x08, 0x04, 0x01 };
    private static readonly byte[] _alphabetArray = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a };

    private static uint StepA(uint ins) {
        uint res = 0;
        uint dat;
        for (int i = 0; i < 64; i++) {
            dat = ins - (ins & 0xFFFFFFFE);
            ins >>= 1;

            if (dat == 1)
                res += dat << _encodingArray[i];

            if (ins == 0)
                return res;
        }

        return res;
    }
    private static void StepB(uint ins, out string encoded) {
        var outChar = new List<byte>();

        for (int i = 0; i < 7; i++) {
            uint dat = (uint)((ulong)ins * 0x38e38e39 >> 35);
            ins -= dat * 9 << 2;

            if (ins < 36)
                outChar.Add(_alphabetArray[ins]);

            ins = dat;
        }

        encoded = Encoding.UTF8.GetString(outChar.ToArray());
    }
    public static string PasswordHash(this string inputPassword) {
        uint[] dat = ByteArrayToUintArray(Encoding.Default.GetBytes(inputPassword.ToCharArray()));

        string tmpEncodedString = string.Empty;

        for (int i = 0; i < dat.Length; i++) {
            StepB(StepA(dat[i] + 0x3E8), out string tmpValue);

            if (dat.Length > 1)
                tmpEncodedString += tmpValue;
            else
                tmpEncodedString = tmpValue;
        }

        return tmpEncodedString;
    }
    private static uint[] ByteArrayToUintArray(byte[] data) {
        int lenght = (data.Length + 3) / 4;
        uint[] data2 = new uint[lenght];
        GCHandle pinnedArray = GCHandle.Alloc(data2, GCHandleType.Pinned);
        IntPtr ptr = pinnedArray.AddrOfPinnedObject();
        Marshal.Copy(data, 0, ptr, data.Length);
        pinnedArray.Free();
        return data2;
    }
}