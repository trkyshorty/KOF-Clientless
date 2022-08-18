using KOF.Cryptography;
using KOF.Core.Extensions;

namespace KOF.Core.Communications;

/// <summary>
///     Implements a KnightOnline messaging protocol.
/// </summary>
internal abstract class MessageProtocol
{
    /// <summary>
    ///     The JvCryption used for encryption.
    /// </summary>
    internal JvCryption JvCryption = null!;

    /// <summary>
    ///     The AES used for encryption.
    /// </summary>
    internal AES AES = null!;

    /// <summary>
    ///     The protocol option.
    /// </summary>
    internal MessageProtocolOption Option;

    /// <summary>
    ///     The protocol state.
    /// </summary>
    internal MessageProtocolState State = MessageProtocolState.None;

    /// <summary>
    ///     The Cryptography.
    /// </summary>
    internal MessageProtocolCryptography Cryptography = MessageProtocolCryptography.None;

    /// <summary>
    ///     Indicate if the Handshake process is done, and this protocol ready to process other messages.
    /// </summary>
    internal bool Ready => State == MessageProtocolState.Completed;

    /// <summary>
    ///     The Sequence used for sequencing the messages.
    /// </summary>
    private byte SequenceQueue { get; set; } = 1;
    internal byte Sequence
    {
        get
        {
            if (SequenceQueue == 251)
                Sequence = 1;
            return SequenceQueue;
        }
        set => SequenceQueue = value;
    }

    /// <summary>
    ///     Validates a <see cref="Message" /> using its CRC and Sequence.
    /// </summary>
    /// <param name="msg">The message to validate.</param>
    protected abstract void Validate(Session session, Message msg); // recv_hook

    /// <summary>
    ///     Signs a <see cref="Message" /> by setting its CRC and Sequence.
    /// </summary>
    /// <param name="msg">The message to sign.</param>
    protected abstract void Sign(Session session, Message msg); // send_hook

    /// <summary>
    ///     Decodes a raw buffer into a ready to use <see cref="Message" />.
    /// </summary>
    /// <param name="size">The 2 bytes masked message size from the raw buffer.</param>
    /// <param name="buffer">The remaining message raw buffer.</param>
    /// <returns>The decoded ready to use <see cref="Message" />.</returns>
    internal Message Decode(Session session, MessageSize size, Span<byte> buffer)
    {

        if (Cryptography.HasFlag(MessageProtocolCryptography.JvCryption))
        {
            buffer = JvCryption.Decrypt(buffer.ToArray());
            size.Value = (ushort)buffer.Length;
        }

        if (Cryptography.HasFlag(MessageProtocolCryptography.AES))
        {
            buffer = AES.Decrypt(buffer[1..], buffer[0] == 2);
            size.Value = (ushort)buffer.Length;
        }

        var msg = new Message(size, buffer);
        Validate(session, msg);

        return msg;
    }

    /// <summary>
    ///     Encodes a <see cref="Message" /> into a raw ready to send buffer.
    /// </summary>
    /// <param name="msg">The message to be encoded.</param>
    /// <returns>The raw ready to send buffer.</returns>
    internal Memory<byte> Encode(Session session, Message msg)
    {
        Sign(session, msg);

        if (Cryptography.HasFlag(MessageProtocolCryptography.JvCryption))
        {
            var encryptedData = JvCryption.Encrypt(Sequence, msg.AsDataSpan().ToArray());
            Sequence++;
            var encryptedMsg = new Message(encryptedData.Length, encryptedData.AsSpan());
            return encryptedMsg.AsMemory();
        }

        if (Cryptography.HasFlag(MessageProtocolCryptography.AES))
        {
            var encryptedData = AES.Encrypt(msg.AsDataSpan().AddByteToArray(Sequence));
            Sequence++;
            var encryptedMsg = new Message(encryptedData.Length + 1, encryptedData.AddByteToArray(1));
            return encryptedMsg.AsMemory();
        }

        return msg.AsMemory();
    }
}