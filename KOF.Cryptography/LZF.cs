namespace KOF.Cryptography;

/// <summary>
/// Improved C# LZF Compressor, a very small data compression library. The compression algorithm is extremely fast. 
/// </summary>
public sealed class LZF {
    /// <summary>
    /// Decompress outputBytes
    /// </summary>
    /// <returns></returns>
    public static byte[] Decompress(byte[] inputBytes) {
        // Starting guess, increase it later if needed
        int outputByteCountGuess = inputBytes.Length * 2;
        byte[] tempBuffer = new byte[outputByteCountGuess];
        int byteCount = Lzf_decompress(inputBytes, ref tempBuffer);

        // If byteCount is 0, then increase buffer and try again
        while (byteCount == 0) {
            outputByteCountGuess *= 2;
            tempBuffer = new byte[outputByteCountGuess];
            byteCount = Lzf_decompress(inputBytes, ref tempBuffer);
        }

        byte[] outputBytes = new byte[byteCount];
        Buffer.BlockCopy(tempBuffer, 0, outputBytes, 0, byteCount);
        return outputBytes;
    }

    /// <summary>
    /// Decompresses the data using LibLZF algorithm
    /// </summary>
    /// <param name="input">Reference to the data to decompress</param>
    /// <param name="output">Reference to a buffer which will contain the decompressed data</param>
    /// <returns>Returns decompressed size</returns>
    public static int Lzf_decompress(byte[] input, ref byte[] output) {
        int inputLength = input.Length;
        int outputLength = output.Length;

        uint iidx = 0;
        uint oidx = 0;

        do {
            uint ctrl = input[iidx++];

            if (ctrl < 1 << 5) /* literal run */
            {
                ctrl++;

                if (oidx + ctrl > outputLength) {
                    //SET_ERRNO (E2BIG);
                    return 0;
                }

                do
                    output[oidx++] = input[iidx++];
                while (--ctrl != 0);
            }
            else /* back reference */
              {
                uint len = ctrl >> 5;

                int reference = (int)(oidx - ((ctrl & 0x1f) << 8) - 1);

                if (len == 7)
                    len += input[iidx++];

                reference -= input[iidx++];

                if (oidx + len + 2 > outputLength) {
                    //SET_ERRNO (E2BIG);
                    return 0;
                }

                if (reference < 0) {
                    //SET_ERRNO (EINVAL);
                    return 0;
                }

                output[oidx++] = output[reference++];
                output[oidx++] = output[reference++];

                do
                    output[oidx++] = output[reference++];
                while (--len != 0);
            }
        }
        while (iidx < inputLength);

        return (int)oidx;
    }

}