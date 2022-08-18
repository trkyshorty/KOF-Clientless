using System.Runtime.InteropServices;

namespace KOF.Cryptography;

/// <summary>
///     Implication of the JvCryption algorithm.
/// </summary>
public class JvCryption {

    #region Variable Declaration
    /// <summary>
    /// Private (no shared) Key.
    /// </summary>
    private ulong PrivateKey { get; set; }

    /// <summary>
    /// Public (shared) Key.
    /// </summary>
    private ulong PublicKey { get; set; }

    /// <summary>
    /// Temporary Key.
    /// </summary>
    private ulong TempKey { get; set; }
    #endregion

    #region Constructor
    public JvCryption(byte mode = 0x02) => PrivateKey = mode switch
    {
        0x00 => 0x1234567890123456,     /* for v1298 and below */
        0x01 => 0x1257091582190465,     /* beginning from 13xx until late 14xx's */
        0x02 => 0x1207500120128966,     /* above 17xx */
        _ => 0x1207500120128966
    };

    public JvCryption(ulong key) => PrivateKey = key;
    #endregion

    #region (Private Methods)
    private void Init() {
        TempKey = PublicKey ^ PrivateKey;
    }

    private void JvEncryptionFast(byte[] data) {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var rKey = 2157;
        var pKey = BitConverter.GetBytes(TempKey);
        var lKey = (byte)(data.Length * 0x9D & 0xFF);

        for (int i = 0; i < data.Length; i++) {
            var rsk = (byte)(rKey >> 8 & 0xFF);
            data[i] = (byte)(data[i] ^ rsk ^ pKey[i % 8] ^ lKey);
            rKey *= 2171;
        }
    }

    private int JvDecryptionWithCRC32(byte[] data) {
        int result;
        JvEncryptionFast(data);

        // if (crc32(dataout, len - 4, -1) == *(unsigned long*)(len - 4 + dataout))
        uint calculatedchecksym = BitConverter.ToUInt32(data[4..],0);
        uint checksumvalue = CRC32.Compute(data[4..]);
        if (checksumvalue == calculatedchecksym)
            result = data.Length - 4;
        else
            result = -1;

        return result;
    }

    #endregion

    #region (Public Methods)
    public bool SetSessionKey(ulong key) {
        PublicKey = key;
        Init();
        return true;
    }

    public byte[] Encrypt(uint sequence, byte[] data) {
        data = AddSequenceToArray(data, sequence);
        byte[] content = new byte[sizeof(int) + data.Length];
        data.AsSpan().CopyTo(content);

        uint crc32 = CRC32.Compute(data);
        MemoryMarshal.Write(content.AsSpan()[^4..], ref crc32);

        JvEncryptionFast(content);
        return content;
    }

    public Span<byte> Decrypt(byte[] data) {
        int result = JvDecryptionWithCRC32(data);
        return data.AsSpan()[5..];
    }

    private byte[] AddSequenceToArray(byte[] bArray, uint sequence) {
        byte[] newArray = new byte[bArray.Length + sizeof(uint)];
        byte[] newUint32Array = BitConverter.GetBytes(sequence);
        bArray.CopyTo(newArray, 4);
        newUint32Array.CopyTo(newArray, 0);
        return newArray;
    }
    #endregion
}