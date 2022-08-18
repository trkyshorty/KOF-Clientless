using System.Diagnostics;

namespace KOF.Core.Communications;

/// <summary>
///     Implements a KnightOnline Client messaging protocol.
/// </summary>
internal class ClientMessageProtocol : MessageProtocol
{
    /// <summary>
    ///     Initializes a new Client messaging protocol.
    /// </summary>
    public ClientMessageProtocol()
    {
        Option = MessageProtocolOption.None;
        State = MessageProtocolState.WaitSetup;
    }

    protected override void Validate(Session session, Message msg)
    {
        #region Recv_Hook

        switch (msg.ID.Value)
        {
            case MessageID.WIZ_NPC_MOVE:
            case MessageID.WIZ_MOVE:
            case MessageID.WIZ_ROTATE:
            case MessageID.WIZ_LEVEL_CHANGE:
            case MessageID.WIZ_OBJECT_EVENT:
            case MessageID.WIZ_ATTACK:
            case MessageID.WIZ_USER_INOUT:
            case MessageID.WIZ_REQ_USERIN:
            case MessageID.WIZ_CHAT:
            case MessageID.WIZ_SURROUNDING_USER:
            case MessageID.WIZ_LOGOSSHOUT:
            case MessageID.WIZ_NATION_CHAT:
            case MessageID.WIZ_HELMET:
            case MessageID.WIZ_MERCHANT:
            case MessageID.WIZ_MERCHANT_INOUT:
            case MessageID.WIZ_USERLOOK_CHANGE:
            case MessageID.WIZ_COMPRESS_PACKET:
            case MessageID.WIZ_PARTY:
            case MessageID.WIZ_MAGIC_PROCESS:
            case MessageID.WIZ_REQ_NPCIN:
            case MessageID.WIZ_DEAD:
            case MessageID.WIZ_NPC_REGION:
            case MessageID.WIZ_REGIONCHANGE:
            case MessageID.WIZ_STATE_CHANGE:
            case MessageID.WIZ_NPC_INOUT:
            case MessageID.WIZ_TARGET_HP:
            case MessageID.WIZ_HP_CHANGE:
            case MessageID.WIZ_MSP_CHANGE:
            case MessageID.WIZ_BUNDLE_OPEN_REQ:
            case MessageID.WIZ_ITEM_GET:
            case MessageID.WIZ_EXP_CHANGE:
            case MessageID.WIZ_ITEM_DROP:
            case MessageID.WIZ_ITEM_MOVE:
            case MessageID.WIZ_WEIGHT_CHANGE:
            case MessageID.WIZ_PET:
            case MessageID.WIZ_EFFECT:
            case MessageID.WIZ_SPEEDHACK_CHECK:
            case MessageID.WIZ_GENIE:
            case MessageID.WIZ_ITEM_COUNT_CHANGE:
                break;

            default:
                Debug.WriteLine($"{DateTime.Now:HH:mm:ss} RECV: {Convert.ToHexString(msg.AsDataSpan()).ToLower()}");
                break;
        }

        #endregion
    }

    protected override void Sign(Session session, Message msg)
    {
        #region Send_Hook

        switch (msg.ID.Value)
        {
            //case MessageID.WIZ_NPC_MOVE:
            //case MessageID.WIZ_MOVE:
            //case MessageID.WIZ_ROTATE:
            //case MessageID.WIZ_LEVEL_CHANGE:
            //case MessageID.WIZ_OBJECT_EVENT:
            //case MessageID.WIZ_ATTACK:
            //case MessageID.WIZ_USER_INOUT:
            //case MessageID.WIZ_REQ_USERIN:
            //case MessageID.WIZ_CHAT:
            //case MessageID.WIZ_SURROUNDING_USER:
            //case MessageID.WIZ_LOGOSSHOUT:
            //case MessageID.WIZ_NATION_CHAT:
            //case MessageID.WIZ_HELMET:
            //case MessageID.WIZ_MERCHANT:
            //case MessageID.WIZ_MERCHANT_INOUT:
            //case MessageID.WIZ_USERLOOK_CHANGE:
            //case MessageID.WIZ_COMPRESS_PACKET:
            //case MessageID.WIZ_PARTY:
            //case MessageID.WIZ_MAGIC_PROCESS:
            //case MessageID.WIZ_REQ_NPCIN:
            //case MessageID.WIZ_DEAD:
            //case MessageID.WIZ_NPC_REGION:
            //case MessageID.WIZ_REGIONCHANGE:
            //case MessageID.WIZ_STATE_CHANGE:
            //case MessageID.WIZ_NPC_INOUT:
            //case MessageID.WIZ_TARGET_HP:
            //case MessageID.WIZ_HP_CHANGE:
            //case MessageID.WIZ_MSP_CHANGE:
            //case MessageID.WIZ_BUNDLE_OPEN_REQ:
            //case MessageID.WIZ_ITEM_GET:
            //case MessageID.WIZ_EXP_CHANGE:
            //case MessageID.WIZ_ITEM_DROP:
            //case MessageID.WIZ_ITEM_MOVE:
            //case MessageID.WIZ_WEIGHT_CHANGE:
            //case MessageID.WIZ_PET:
            //case MessageID.WIZ_EFFECT:
            //case MessageID.WIZ_SPEEDHACK_CHECK:
            //case MessageID.WIZ_GENIE:
            //case MessageID.WIZ_ITEM_COUNT_CHANGE:
            //    break;

            default:
                Debug.WriteLine($"{DateTime.Now:HH:mm:ss} SEND:[{Sequence}] {Convert.ToHexString(msg.AsDataSpan()).ToLower()}");
                break;
        }

        #endregion

    }
}