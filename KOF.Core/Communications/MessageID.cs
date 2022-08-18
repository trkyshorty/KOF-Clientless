using System.Runtime.InteropServices;

namespace KOF.Core.Communications;

/// <summary>
///     Specify a unique <see cref="Message" /> ID, used to identify messages.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1, Size = sizeof(byte))]
public struct MessageID : IEquatable<MessageID>, IEquatable<byte>
{
    /// <summary>
    ///     Initializes a MessageID with a full specific ID (a.k.a Opcode).
    /// </summary>
    /// <param name="value">The ID value.</param>
    public MessageID(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///     The full value of the ID (a.k.a. Opcode).
    /// </summary>
    public byte Value { get; set; }

    public bool Equals(MessageID other)
    {
        return Value == other.Value;
    }

    public bool Equals(byte other)
    {
        return Value == other;
    }

    public override string ToString()
    {
        return $"[{Value:X2}]";
    }

    public override bool Equals(object? obj)
    {
        return obj is MessageID other && Equals(other);
    }

    public static bool operator ==(MessageID left, MessageID right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MessageID left, MessageID right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    #region Known PacketIDs

    /// <summary>
    /// A list of <see cref="MessageID"/>s values (a.k.a. Opcodes).
    /// </summary>
    public const byte
            LS_VERSION_REQ = 0x01,
            LS_DOWNLOADINFO_REQ = 0x02,
            LS_CRYPTION = 0xF2,
            LS_LOGIN_REQ = 0xF3,
            LS_MGAME_LOGIN = 0xF4,
            LS_SERVERLIST = 0xF5,
            LS_NEWS = 0xF6,
            LS_UNKF7 = 0xF7,
            LS_OTP_UNKNOWN =  0xFB;

    /// <summary>
    /// A list of <see cref="MessageID"/>s values (a.k.a. Opcodes).
    /// </summary>
    public const byte
            WIZ_LOGIN = 0x01,       // Account Login
            WIZ_NEW_CHAR = 0x02,        // Create Character DB
            WIZ_DEL_CHAR = 0x03,        // Delete Character DB
            WIZ_SEL_CHAR = 0x04,        // Select Character
            WIZ_SEL_NATION = 0x05,      // Select Nation
            WIZ_MOVE = 0x06,        // Move ( 1 Second )
            WIZ_USER_INOUT = 0x07,      // User Info Insert,delete
            WIZ_ATTACK = 0x08,      // General Attack 
            WIZ_ROTATE = 0x09,      // Rotate
            WIZ_NPC_INOUT = 0x0A,       // Npc Info Insert,delete
            WIZ_NPC_MOVE = 0x0B,        // Npc Move ( 1 Second )
            WIZ_ALLCHAR_INFO_REQ = 0x0C,        // Account All Character Info Request
            WIZ_GAMESTART = 0x0D,       // Request Other User,Npc Info
            WIZ_MYINFO = 0x0E,      // User Detail Data Download
            WIZ_LOGOUT = 0x0F,      // Request Logout
            WIZ_CHAT = 0x10,        // User Chatting..
            WIZ_DEAD = 0x11,        // User Dead
            WIZ_REGENE = 0x12,      // User	Regeneration
            WIZ_TIME = 0x13,        // Game Timer
            WIZ_WEATHER = 0x14,     // Game Weather
            WIZ_REGIONCHANGE = 0x15,        // Region UserInfo Receive
            WIZ_REQ_USERIN = 0x16,      // Client Request UnRegistered User List
            WIZ_HP_CHANGE = 0x17,       // Current HP Download
            WIZ_MSP_CHANGE = 0x18,      // Current MP Download
            WIZ_ITEM_LOG = 0x19,        // Send To Agent for Writing Log
            WIZ_EXP_CHANGE = 0x1A,      // Current EXP Download
            WIZ_LEVEL_CHANGE = 0x1B,        // Max HP,MP,SP,Weight,Exp Download
            WIZ_NPC_REGION = 0x1C,      // Npc Region Change Receive
            WIZ_REQ_NPCIN = 0x1D,       // Client Request UnRegistered NPC List
            WIZ_WARP = 0x1E,        // User Remote Warp
            WIZ_ITEM_MOVE = 0x1F,       // User Item Move
            WIZ_NATION_CHAT = 0x19,     // Nation Chat
            WIZ_NPC_EVENT = 0x20,       // User Click Npc Event
            WIZ_ITEM_TRADE = 0x21,      // Item Trade 
            WIZ_TARGET_HP = 0x22,       // Attack Result Target HP 
            WIZ_ITEM_DROP = 0x23,       // Zone Item Insert
            WIZ_BUNDLE_OPEN_REQ = 0x24,     // Zone Item list Request
            WIZ_TRADE_NPC = 0x25,       // ITEM Trade start
            WIZ_ITEM_GET = 0x26,        // Zone Item Get
            WIZ_ZONE_CHANGE = 0x27,     // Zone Change
            WIZ_POINT_CHANGE = 0x28,       // Str,Sta,dex,intel,cha,point up down
            WIZ_STATE_CHANGE = 0x29,        // User Sitdown or Stand
            WIZ_LOYALTY_CHANGE = 0x2A,      // Nation Contribution
            WIZ_VERSION_CHECK = 0x2B,       // Client version check 
            WIZ_CRYPTION = 0x2C,        // Cryption
            WIZ_USERLOOK_CHANGE = 0x2D,     // User Slot Item Resource Change
            WIZ_NOTICE = 0x2E,      // Update Notice Alarm 
            WIZ_PARTY = 0x2F,       // Party Related Packet
            WIZ_EXCHANGE = 0x30,        // Exchange Related Packet
            WIZ_MAGIC_PROCESS = 0x31,       // Magic Related Packet
            WIZ_SKILLPT_CHANGE = 0x32,      // User changed particular skill point
            WIZ_OBJECT_EVENT = 0x33,        // Map Object Event Occur ( ex : Bind Point Setting )
            WIZ_CLASS_CHANGE = 0x34,        // 10 level over can change class 
            WIZ_CHAT_TARGET = 0x35,     // Select Private Chanting User
            WIZ_CONCURRENTUSER = 0x36,      // Current Game User Count
            WIZ_DATASAVE = 0x37,        // User GameData DB Save Request
            WIZ_DURATION = 0x38,        // Item Durability Change
            WIZ_TIMENOTIFY = 0x39,      // Time Adaption Magic Time Notify Packet ( 2 Seconds )
            WIZ_REPAIR_NPC = 0x3A,      // Item Trade,Upgrade and Repair
            WIZ_ITEM_REPAIR = 0x3B,     // Item Repair Processing
            WIZ_KNIGHTS_PROCESS = 0x3C,     // Knights Related Packet..
            WIZ_ITEM_COUNT_CHANGE = 0x3D,   // Item cout change.  
            WIZ_KNIGHTS_LIST = 0x3E,        // All Knights List Info download
            WIZ_ITEM_REMOVE = 0x3F,     // Item Remove from inventory
            WIZ_OPERATOR = 0x40,        // Operator Authority Packet
            WIZ_SPEEDHACK_CHECK = 0x41,     // Speed Hack Using Check
            WIZ_COMPRESS_PACKET = 0x42,     // Data Compressing Packet
            WIZ_SERVER_CHECK = 0x43,        // Server Status Check Packet
            WIZ_CONTINOUS_PACKET = 0x44,        // Region Data Packet 
            WIZ_WAREHOUSE = 0x45,       // Warehouse Open,In,Out
            WIZ_SERVER_CHANGE = 0x46,       // When you change the server
            WIZ_REPORT_BUG = 0x47,      // Report Bug to the manager
            WIZ_HOME = 0x48,   // 'Come back home' by Seo Taeji & Boys
            WIZ_FRIEND_PROCESS = 0x49,      // Get the status of your friend
            WIZ_GOLD_CHANGE = 0x4A,     // When you get the gold of your enemy.
            WIZ_WARP_LIST = 0x4B,       // Warp List by NPC or Object
            WIZ_VIRTUAL_SERVER = 0x4C,      // Battle zone Server Info packet	(IP,Port)
            WIZ_ZONE_CONCURRENT = 0x4D,     // Battle zone concurrent users request packet
            WIZ_CORPSE = 0x4e,      // To have your corpse have an ID on top of it.
            WIZ_PARTY_BBS = 0x4f,       // For the party wanted bulletin board service..
            WIZ_MARKET_BBS = 0x50,      // For the market bulletin board service...
            WIZ_KICKOUT = 0x51,     // Account ID forbid duplicate connection
            WIZ_CLIENT_EVENT = 0x52,        // Client Event (for quest)
            WIZ_MAP_EVENT = 0x53,       // Map Event (for dressing up Golden Shells,or the Map Views at Ship War)
            WIZ_WEIGHT_CHANGE = 0x54,       // Notify change of weight
            WIZ_SELECT_MSG = 0x55,      // Select Event Message...
            WIZ_NPC_SAY = 0x56,     // Select Event Message...
            WIZ_BATTLE_EVENT = 0x57,        // Battle Event Result
            WIZ_AUTHORITY_CHANGE = 0x58,        // Authority change 
            WIZ_EDIT_BOX = 0x59,        // Activate/Receive info from Input_Box.
            WIZ_SANTA = 0x5A,       // Activate motherfucking Santa Claus!!! :(
            WIZ_ITEM_UPGRADE = 0x5B,
            WIZ_ZONEABILITY = 0x5E,
            WIZ_EVENT = 0x5F,
            WIZ_STEALTH = 0x60,     // stealth related.
            WIZ_ROOM_PACKETPROCESS = 0x61,      // room system
            WIZ_ROOM = 0x62,
            WIZ_CLAN_BATTLE = 0x63,     // Clan Battle
            WIZ_QUEST = 0x64,
            WIZ_KISS = 0x66,
            WIZ_RECOMMEND_USER = 0x67,
            WIZ_MERCHANT = 0x68,
            WIZ_MERCHANT_INOUT = 0x69,
            WIZ_SHOPPING_MALL = 0x6A,
            WIZ_SERVER_INDEX = 0x6B,
            WIZ_EFFECT = 0x6C,
            WIZ_SIEGE = 0x6D,
            WIZ_NAME_CHANGE = 0x6E,
            WIZ_WEBPAGE = 0x6F,
            WIZ_CAPE = 0x70,
            WIZ_PREMIUM = 0x71,
            WIZ_HACKTOOL = 0x72,
            WIZ_RENTAL = 0x73,
            WIZ_ITEM_EXPIRATION = 0x74,
            WIZ_CHALLENGE = 0x75,
            WIZ_PET = 0x76,
            WIZ_CHINA = 0x77,       // we shouldn't need to worry about this
            WIZ_KING = 0x78,
            WIZ_SKILLDATA = 0x79,
            WIZ_PROGRAMCHECK = 0x7A,
            WIZ_BIFROST = 0x7B,
            WIZ_REPORT = 0x7C,
            WIZ_LOGOSSHOUT = 0x7D,
            WIZ_RANK = 0x80,
            WIZ_STORY = 0x81,
            WIZ_NATION_TRANSFER = 0x82,
            WIZ_ZONE_TERRAIN = 0x83,
            WIZ_MOVING_TOWER = 0x84,
            WIZ_BDWINFO = 0x85,
            WIZ_MINING = 0x86,
            WIZ_HELMET = 0x87,
            WIZ_PVP = 0x88,
            WIZ_CHANGE_HAIR = 0x89,     // Changes hair colour/facial features at character selection
            WIZ_KAUL_A = 0x8A,
            WIZ_VIP_STORAGE = 0x8B,
            WIZ_KAUL_C = 0x8C,
            WIZ_GENDER_CHANGE = 0x8D,
            WIZ_DEATH_LIST = 0x90,
            WIZ_CLANPOINTS_BATTLE = 0x91,       // not sure
            WIZ_KURIAN = 0x92,
            WIZ_GENIE = 0x97,
            WIZ_SURROUNDING_USER = 0x98,
            WIZ_ACHIEVE = 0x99,
            WIZ_SEALEXP = 0x9A,
            WIZ_SP_CHANGE = 0x9B,
            WIZ_LOADING_LOGIN = 0x9F,
            WIZ_TEST_PACKET = 0xFF;        // Test packet

    #endregion
}
