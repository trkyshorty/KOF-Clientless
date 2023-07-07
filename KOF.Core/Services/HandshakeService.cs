using KOF.Core.Communications;
using System.Text;

namespace KOF.Core.Services;

public class HandshakeService
{
    private ulong PublicKey { get; set; } = 0;
    private byte[] AESKey { get; set; } = null!;

    [MessageHandler(MessageID.LS_CRYPTION)]
    public Task MsgRecv_Cryption(Session session, Message msg)
    {
        var protocol = session.Protocol;

        PublicKey = msg.Read<ulong>();
        if (PublicKey != 0)
        {
            protocol.JvCryption = new();
            if (protocol.JvCryption.SetSessionKey(PublicKey))
                protocol.Cryptography = MessageProtocolCryptography.JvCryption;
        }
        else
            protocol.Cryptography = MessageProtocolCryptography.None;

        protocol.State = MessageProtocolState.Completed;
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_VERSION_CHECK)]
    public Task MsgRecv_VersionCheck(Session session, Message msg)
    {
        var protocol = session.Protocol;

        _ = msg.Read<ushort>(); // client version
        var key = msg.Read(false);
        AESKey = Encoding.Default.GetBytes(key);

        protocol.AES = new(AESKey);
        protocol.Cryptography = MessageProtocolCryptography.AES;

        protocol.State = MessageProtocolState.Completed;
        return Task.CompletedTask;
    }
}

