using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace KOF.Core.Communications;

public class Message
{
    /// <summary>
    ///     The underlying internal buffer.
    /// </summary>
    private byte[] _buffer;

    /// <summary>
    ///     Initializes a new Message.
    /// </summary>
    /// <param name="id">The message unique ID.</param>
    /// <param name="capacity">The pre-allocated capacity.</param>
    public Message(MessageID id, ushort capacity = 0)
    {
        _buffer = new byte[HeaderSize + IDSize + capacity];
        Position = DataOffset;
        ID = id;
    }

    /// <summary>
    ///     Initializes a new Message.
    /// </summary>
    /// <param name="id">The message unique ID.</param>
    /// <param name="capacity">The pre-allocated capacity.</param>
    public Message(byte id, ushort capacity = 0)
        : this(new MessageID(id), capacity)
    {
    }

    internal Message(MessageSize size, Span<byte> buffer)
    {
        _buffer = new byte[HeaderSize + size.Value];
        Position = SizeOffset;
        Write(size);
        Write<byte>(buffer[..size.Value]);
        Position = DataOffset + IDSize;
    }

    public Message(int size, Span<byte> buffer)
        : this(new MessageSize { Value = (ushort)size }, buffer)
    {
    }

    /// <summary>
    ///     The message data size.
    /// </summary>
    public ushort Size
    {
        get
        {
            lock (_buffer)
            {
                var size = MemoryMarshal.Read<MessageSize>(_buffer.AsSpan(SizeOffset,
                    Unsafe.SizeOf<MessageSize>()));
                return size.Value;
            }
        }
        private set
        {
            lock (_buffer)
            {
                ref var size =
                    ref MemoryMarshal.AsRef<MessageSize>(_buffer.AsSpan(SizeOffset,
                        Unsafe.SizeOf<MessageSize>()));
                size.Value = value;
            }
        }
    }

    /// <summary>
    ///     The message ID, used to identify messages.
    /// </summary>
    public MessageID ID
    {
        get
        {
            lock (_buffer)
            {
                var id = MemoryMarshal.Read<MessageID>(_buffer.AsSpan(IDOffset,
                    Unsafe.SizeOf<MessageID>()));
                return id;
            }
        }
        set
        {
            lock (_buffer)
            {
                ref var id =
                    ref MemoryMarshal.AsRef<MessageID>(_buffer.AsSpan(IDOffset,
                        Unsafe.SizeOf<MessageID>()));
                id = value;
                Position += (ushort)Unsafe.SizeOf<MessageID>();
                Size += (ushort)Unsafe.SizeOf<MessageID>();
            }
        }
    }

    /// <summary>
    ///     The capacity allocated for the message without counting the HeaderSize.
    /// </summary>
    public ushort Capacity
    {
        get
        {
            lock (_buffer)
            {
                return (ushort)(_buffer.Length - HeaderSize);
            }
        }
    }

    /// <summary>
    ///     The underlying position for reading/writing from/into the current message.
    ///     This is dangerous to mutate, as it doesn't check where you lead the position.
    ///     And this may cause exceptions or unexpected behaviors when reading/writing.
    /// </summary>
    public ushort Position { get; set; }

    public override string ToString() {
        lock (_buffer) {
            const int bytesPerLine = 16;
            var output = new StringBuilder();
            var asciiOutput = new StringBuilder();
            var length = _buffer.Length - HeaderSize;
            if (length % bytesPerLine != 0)
                length += bytesPerLine - length % bytesPerLine;

            output.Append(
                $"{ID} [{Size:D4} bytes] {Environment.NewLine}");
            for (var x = 0; x <= length; ++x) {
                if (x % bytesPerLine == 0) {
                    if (x > 0) {
                        output.Append($"  {asciiOutput}{Environment.NewLine}");
                        asciiOutput.Clear();
                    }

                    if (x != length)
                        output.Append($"{x:d10}   ");
                }

                if (x < _buffer.Length - HeaderSize) {
                    output.Append($"{_buffer[x + HeaderSize]:X2} ");
                    var ch = (char)_buffer[x + HeaderSize];
                    if (!char.IsControl(ch))
                        asciiOutput.Append($"{ch}");
                    else
                        asciiOutput.Append('.');
                }
                else {
                    output.Append("   ");
                    asciiOutput.Append('.');
                }
            }

            return output.ToString();
        }
    }

    /// <summary>
    ///     Forms a slice out of the current message including the header.
    /// </summary>
    /// <returns></returns>
    public Memory<byte> AsMemory()
    {
        ushort _header = 0x55AA;
        ushort _tail = 0xAA55;

        lock (_buffer)
        { // TODO : _buffer clear ? | Array.Clear(_buffer, 0, _buffer.Length);
            var _fullBuffer = new byte[Size + 6];
            MemoryMarshal.Write(_fullBuffer.AsSpan()[..2], ref _header);
            _buffer.CopyTo(_fullBuffer.AsSpan(2));
            MemoryMarshal.Write(_fullBuffer.AsSpan()[^2..], ref _tail);
            return _fullBuffer.AsMemory();
        }
    }

    /// <summary>
    ///     Forms a slice out of the current message message data, without including the header.
    /// </summary>
    /// <returns></returns>
    public Memory<byte> AsDataMemory()
    {
        lock (_buffer)
        {
            return _buffer.AsMemory(DataOffset);
        }
    }

    /// <summary>
    ///     Forms a slice out of the current message including the header.
    /// </summary>
    /// <returns></returns>
    public Span<byte> AsSpan()
    {
        lock (_buffer)
        {
            return _buffer.AsSpan();
        }
    }

    /// <summary>
    ///     Forms a slice out of the current message data, without including the header.
    /// </summary>
    /// <returns></returns>
    public Span<byte> AsDataSpan()
    {
        lock (_buffer)
        {
            return _buffer.AsSpan(DataOffset);
        }
    }

    /// <summary>
    ///     Reads a value from a message.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <returns>The read value.</returns>
    public T Read<T>() where T : unmanaged
    {
        lock (_buffer)
        {
            var size = Unsafe.SizeOf<T>();
            var value = MemoryMarshal.Read<T>(_buffer.AsSpan(Position, size));
            Position += (ushort)size;
            return value;
        }
    }

    /// <summary>
    ///     Reads a into a <see cref="Span{T}" /> from a message until the span is full.
    /// </summary>
    /// <param name="span">The span to read into</param>
    /// <typeparam name="T">The span inner item type</typeparam>
    public void Read<T>(in Span<T> span) where T : unmanaged
    {
        lock (_buffer)
        {
            var bytesSpan = MemoryMarshal.AsBytes(span);
            var size = bytesSpan.Length;
            _buffer.AsSpan(Position, size).CopyTo(bytesSpan);
            Position += (ushort)size;
        }
    }

    /// <summary>
    ///     Reads a string with a specific encoding from a message.
    /// </summary>
    /// <param name="encoding">The string encoding.</param>
    /// <param name="evenOdd">The string size data <c>Type</c>.</param>
    /// <returns>The read windows-1254(TR) string.</returns>
    public string Read(Encoding encoding, bool evenOdd)
    {
        lock (_buffer)
        {
            var length = evenOdd ? Read<ushort>() : Read<byte>();
            var str = encoding.GetString(_buffer.AsSpan(Position, length));
            Position += length;
            return str;
        }
    }

    /// <summary>
    ///     Reads an windows-1254(TR) encoded string from a message.
    /// </summary>
    /// <param name="evenOdd">The string size data <c>Type</c>. if true is means <c>ushort</c> else <c>byte</c></param>
    /// <returns>The read windows-1254(TR) string.</returns>
    public string Read(bool evenOdd, string encodingName = "windows-1254")
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return Read(Encoding.GetEncoding(encodingName), evenOdd);
    }

    /// <summary>
    ///     Writes a ref value into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <typeparam name="T">The value type</typeparam>
    public void Write<T>(ref T value) where T : unmanaged
    {
        lock (_buffer)
        {
            var size = Unsafe.SizeOf<T>();
            if (Position + size > _buffer.Length)
                Array.Resize(ref _buffer, Position + size);

            MemoryMarshal.Write(_buffer.AsSpan(Position, size), ref value);
            Position += (ushort)size;
            Size = (ushort)(_buffer.Length - HeaderSize);
        }
    }

    /// <summary>
    ///     Writes a value into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <typeparam name="T">The value type.</typeparam>
    public void Write<T>(T value) where T : unmanaged
    {
        Write(ref value);
    }

    /// <summary>
    ///     Writes a <see cref="Span{T}" /> into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="span">The span to write.</param>
    /// <typeparam name="T">The inner span item type.</typeparam>
    public void Write<T>(in ReadOnlySpan<T> span) where T : unmanaged
    {
        lock (_buffer)
        {
            var bytesSpan = MemoryMarshal.AsBytes(span);
            var size = bytesSpan.Length;
            if (Position + size > _buffer.Length)
                Array.Resize(ref _buffer, Position + size);

            bytesSpan.CopyTo(_buffer.AsSpan(Position, size));
            Position += (ushort)size;
            Size = (ushort)(_buffer.Length - HeaderSize);
        }
    }

    /// <summary>
    ///     Write a string value with a specific encoding into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="evenOdd">The string size data <c>Type</c>.</param>
    /// <param name="encoding">The string encoding.</param>
    public void Write(string value, Encoding encoding, bool evenOdd)
    {
        lock (_buffer)
        {
            var bytes = encoding.GetBytes(value);

            if (evenOdd)
                Write((ushort)bytes.Length);
            else
                Write((byte)bytes.Length);


            Write<byte>(bytes);
        }
    }

    /// <summary>
    ///     Writes an windows-1254(TR) encoding string into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="evenOdd">The string size data <c>Type</c>. if true is means <c>UINT16</c> else <c>BYTE</c></param>
    public void Write(string value, bool evenOdd)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Write(value, Encoding.GetEncoding("windows-1254"), evenOdd);
    }

    /// <summary>
    ///     Writes an windows-1254(TR) encoding string into a message, growing it as much as necessary.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <param name="evenOdd">The string size data <c>Type</c>. if true is means <c>UINT16</c> else <c>BYTE</c></param>
    public void Write(string value, string encoding, bool evenOdd)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Write(value, Encoding.GetEncoding(encoding), evenOdd);
    }

    internal void Resize(ushort size)
    {
        lock (_buffer)
        {
            if (size < HeaderSize)
                throw new ArgumentOutOfRangeException(nameof(size));

            Array.Resize(ref _buffer, size);
        }
    }

    internal bool IsReachedEnd()
    {
        lock (_buffer)
        {
            return Position >= Capacity;
        }
    }

    internal bool Peek(Span<byte> pattern)
    {
        lock (_buffer)
        {
            return _buffer.AsSpan(Position, pattern.Length).SequenceEqual(pattern);
        }
    }

    #region Size(s) and Offset(s)

    public const ushort BufferSize = 0x4002;
    public const ushort HeaderSize = 2;
    public const ushort IDSize = 1;
    public const ushort DataSize = BufferSize - HeaderSize;

    public const ushort HeaderOffset = 0;
    public const ushort SizeOffset = HeaderOffset + 0;
    public const ushort IDOffset = HeaderOffset + 2;
    public const ushort DataOffset = HeaderOffset + HeaderSize;

    #endregion Size(s) and Offset(s)
}