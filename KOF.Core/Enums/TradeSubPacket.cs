namespace KOF.Core.Enums;

internal enum TradeSubPacket : byte
{
    TRADE_REQ = 1,
    TRADE_AGREE = 2,
    TRADE_ADD = 3,
    TRADE_OTHER_ADD = 4,
    TRADE_DECIDE = 5,
    TRADE_OTHER_DECIDE = 6,
    TRADE_DONE = 7,
    TRADE_CANCEL = 8
}