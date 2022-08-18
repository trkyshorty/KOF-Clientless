namespace KOF.Core.Extensions;

public static class SpanExtensions
{
    public static ushort GetUShortFromFirstTwoBytes(this byte[] data)
    {
        return (ushort)((data[1] << 8) + (data[0] & 0xff));
    }

    public static ushort GetUShortFromFirstTwoBytes(this Memory<byte> data)
        => data.ToArray().GetUShortFromFirstTwoBytes();

    public static ushort GetUShortFromFirstTwoBytes(this Span<byte> data)
        => data.ToArray().GetUShortFromFirstTwoBytes();

    public static byte[] AddByteToArray(this byte[] bArray, byte newByte)
    {
        byte[] newArray = new byte[bArray.Length + 1];
        bArray.CopyTo(newArray, 1);
        newArray[0] = newByte;
        return newArray;
    }

    public static byte[] AddByteToArray(this Span<byte> bArray, byte newByte)
        => bArray.ToArray().AddByteToArray(newByte);

    public static string ToHexString(this Span<byte> bArray) => Convert.ToHexString(bArray).ToLower();

    public static string GetTimeStamp() => $"{DateTime.Now:HH:mm:ss}";
}
