using System.Runtime.InteropServices;

namespace KOF.Core.Communications;

/// <summary>
///     <see cref="Message"/> size.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1, Size = sizeof(ushort))]
public struct MessageSize : IEquatable<MessageSize>
{
    /// <summary>
    ///     The data size value.
    /// </summary>
    public ushort Value { get; set; }

    public bool Equals(MessageSize other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is MessageSize other && Equals(other);
    }

    public override string ToString()
    {
        return $"[{Value} bytes]";
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(MessageSize left, MessageSize right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MessageSize left, MessageSize right)
    {
        return !(left == right);
    }
}