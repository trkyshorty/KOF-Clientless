using KOF.Core.Enums;
using KOF.Core.Models;
using KOF.Data.Models;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace KOF.Core.Communications;

public class MessageBuilder
{
    public static Message MsgSend_Cryption()
    {
        return new Message(MessageID.LS_CRYPTION);
    }

    public static Message MsgSend_AccountLoginRequest(string account, string password)
    {
        var msg = new Message(MessageID.LS_LOGIN_REQ);

        msg.Write(account, true);
        msg.Write(password, true);
        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_OTP_Unknown(byte commandType, string crc)
    {
        var msg = new Message(0xFB);

        msg.Write(commandType);
        msg.Write<byte>(Convert.FromHexString(crc));

        return msg;
    }

    public static Message MsgSend_ServerlistRequest()
    {
        return new Message(MessageID.LS_SERVERLIST);
    }

    public static Message MsgSend_KickOut(string account)
    {
        var msg = new Message(MessageID.WIZ_KICKOUT);

        msg.Write(account, true);

        return msg;
    }

    public static Message MsgSend_VersionCheck()
    {
        return new Message(MessageID.WIZ_VERSION_CHECK);
    }

    public static Message MsgSend_AccountLogin(string account, string password)
    {
        var msg = new Message(MessageID.WIZ_LOGIN);

        msg.Write(account, true);
        msg.Write(password, true);
        msg.Write<byte>(0);
        msg.Write<ushort>(0);
        msg.Write<ushort>(1);

        return msg;
    }

    public static Message MsgSend_SelectNation(byte nation)
    {
        var msg = new Message(MessageID.WIZ_SEL_NATION);

        msg.Write(nation);

        return msg;
    }

    public static Message MsgSend_NewCharacterCreate(Lobby lobby)
    {
        var msg = new Message(MessageID.WIZ_NEW_CHAR);

        msg.Write((byte)lobby.Slot);
        msg.Write(lobby.Name, true);
        msg.Write(lobby.Race);
        msg.Write(lobby.Class);
        msg.Write(lobby.Face);

        msg.Write(lobby.R);
        msg.Write(lobby.G);
        msg.Write(lobby.B);

        msg.Write(lobby.Hair);

        switch (lobby.Class)
        {
            case 101 or 201 or 113 or 213: // Karus or Human Warrior & Kurian & Porutu
                {
                    msg.Write((byte)65);
                    msg.Write((byte)65);
                    msg.Write((byte)60);
                    msg.Write((byte)50);
                    msg.Write((byte)50);
                }
                break;
            case 102 or 202: // Karus or Human Rogue
                {

                    msg.Write((byte)60);
                    msg.Write((byte)60);
                    msg.Write((byte)70);
                    msg.Write((byte)50);
                    msg.Write((byte)50);
                }
                break;
            case 103 or 203: // Karus or Human Wizard
                {
                    msg.Write((byte)50);
                    msg.Write((byte)50);
                    msg.Write((byte)70);
                    msg.Write((byte)70);
                    msg.Write((byte)50);
                }
                break;
            case 104 or 204: // Karus or Human Priest
                {
                    msg.Write((byte)50);
                    msg.Write((byte)60);
                    msg.Write((byte)60);
                    msg.Write((byte)70);
                    msg.Write((byte)50);
                }
                break;
        }
        return msg;
    }

    public static Message MsgSend_AllCharacterInfoRequest()
    {
        var msg = new Message(MessageID.WIZ_ALLCHAR_INFO_REQ);

        msg.Write<byte>(1);

        return msg;
    }

    public static Message MsgSend_LoadingLogin()
    {
        var msg = new Message(MessageID.WIZ_LOADING_LOGIN);

        msg.Write<byte>(1);

        return msg;
    }

    public static Message MsgSend_SelectedCharacter(string account, string characterName, byte zone = 21)
    {
        var msg = new Message(MessageID.WIZ_SEL_CHAR);

        msg.Write(account, "gb2312", true);
        msg.Write(characterName, "gb2312", true);
        msg.Write<byte>(1);
        msg.Write(zone);
        msg.Write<byte>(0);
        msg.Write<ushort>(1);

        return msg;
    }

    public static Message MsgSend_ShoppingMall(byte command)
    {
        var msg = new Message(MessageID.WIZ_SHOPPING_MALL);

        switch ((ShoppingMallType)command)
        {
            case ShoppingMallType.STORE_CLOSE:
                msg.Write(command);
                break;
            case ShoppingMallType.STORE_PROCESS:
            case ShoppingMallType.STORE_LETTER:
                msg.Write(command);
                msg.Write(true);
                break;
        }

        return msg;
    }

    public static Message MsgSend_Rental()
    {
        var msg = new Message(MessageID.WIZ_RENTAL);

        msg.Write<byte>(2);
        msg.Write<byte>(3);
        msg.Write<byte>(2);

        return msg;
    }

    public static Message MsgSend_ServerIndex()
    {
        return new Message(MessageID.WIZ_SERVER_INDEX);
    }

    public static Message MsgSend_Helmet(byte commandType)
    {
        var msg = new Message(MessageID.WIZ_HELMET);

        msg.Write<byte>(0);
        msg.Write(commandType);

        return msg;
    }

    public static Message MsgSend_Rotate(float rotateAngle, string characterName)
    {
        var msg = new Message(MessageID.WIZ_ROTATE);

        msg.Write((short)rotateAngle);
        msg.Write(characterName, "gb2312", false);

        return msg;
    }

    public static Message MsgSend_GenieProcess(byte commandType)
    {
        var msg = new Message(MessageID.WIZ_GENIE);

        msg.Write<byte>(1);
        msg.Write(commandType);

        return msg;
    }

    public static Message MsgSend_GameStart(byte commandType, string characterName)
    {
        var msg = new Message(MessageID.WIZ_GAMESTART);

        msg.Write<short>(commandType);
        msg.Write(characterName, "gb2312", false);

        return msg;
    }

    public static Message MsgSend_SurroundingUserProcess(byte commandType, string characterName)
    {
        var msg = new Message(MessageID.WIZ_SURROUNDING_USER);

        msg.Write<short>(commandType);
        msg.Write(characterName, "gb2312", false);

        return msg;
    }

    public static Message MsgSend_Home()
    {
        return new Message(MessageID.WIZ_HOME);
    }

    public static Message MsgSend_Chat(byte commandType, string message)
    {
        var msg = new Message(MessageID.WIZ_CHAT);

        msg.Write(commandType);
        msg.Write(message, "gb2312", true);

        return msg;
    }

    public static Message MsgSend_WhisperRequest(string playerName)
    {
        var msg = new Message(MessageID.WIZ_CHAT_TARGET);

        msg.Write<byte>(1);
        msg.Write(playerName, "gb2312", true);

        return msg;
    }

    public static Message MsgSend_NpcEvent(int npcId)
    {
        var msg = new Message(MessageID.WIZ_NPC_EVENT);

        msg.Write<byte>(1);
        msg.Write(npcId);
        msg.Write(-1); //Quest ID

        return msg;
    }

    public static Message MsgSend_WarpTeleport(short objectId, ushort warpId)
    {
        var msg = new Message(MessageID.WIZ_WARP_LIST);

        msg.Write(objectId);
        msg.Write(warpId);

        return msg;
    }

    public static Message MsgSend_ZoneChange(byte opcode, ushort zoneId = 0)
    {
        var msg = new Message(MessageID.WIZ_ZONE_CHANGE);

        msg.Write(opcode);

        if (zoneId != 0)
        {
            msg.Write(zoneId);
            msg.Write<byte>(0);
            msg.Write<ushort>(0);
        }

        return msg;
    }

    public static Message MsgSend_Regen()
    {
        var msg = new Message(MessageID.WIZ_REGENE);

        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_Move(Vector3 start, Vector3 goal, short moveSpeed = 45, byte moveFlag = 3)
    {
        var msg = new Message(MessageID.WIZ_MOVE);

        /* -------------------
         * ---- moveSpeed ----
         * 45 = Normal
         * 67 = Swift
         * 90 = Light feet
         * -------------------
         * ---- moveFlag -----
         *  1   = Normal 
         *  2   = Stop 
         *  3   = Run 
         * -16  = Backforward 
         *-------------------
         *Tips moveSpeed = -1 and moveFlag = -1 => teleport movement xd
         */

        msg.Write((ushort)(goal.X * 10.0f));
        msg.Write((ushort)(goal.Y * 10.0f));
        msg.Write((ushort)(goal.Z * 10.0f));

        msg.Write(moveSpeed);
        msg.Write(moveFlag);

        msg.Write((ushort)(start.X * 10.0f));
        msg.Write((ushort)(start.Y * 10.0f));
        msg.Write((ushort)(start.Z * 10.0f));

        return msg;
    }

    public static float GetTime(long startTime)
    {
        float fTime = (float)(double)((Stopwatch.GetTimestamp() - startTime) / (double)Stopwatch.Frequency);
        return fTime;
    }

    public static Message MsgSend_SpeedCheck(long startTime, bool bInit = false)
    {
        float fTime = GetTime(startTime);

        var msg = new Message(MessageID.WIZ_SPEEDHACK_CHECK);

        msg.Write(bInit);
        msg.Write(fTime);
        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_HackTool(byte commandType, string crc,int unknown)
    {
        var msg = new Message(MessageID.WIZ_HACKTOOL);

        msg.Write(commandType);
        msg.Write<byte>(Convert.FromHexString(crc));
        msg.Write(unknown);

        return msg;
    }

    public static Message MsgSend_BufferSize()
    {
        var msg = new Message(0x95);

        msg.Write(Message.BufferSize);

        return msg;
    }

    public static Message MsgSend_PartyCreate(string targetName)
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(1);
        msg.Write(targetName, "gb2312", true);
        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_PartyInsert(string targetName)
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(3);
        msg.Write(targetName, "gb2312", true);
        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_PartyAccept(bool accept)
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(2);
        msg.Write(accept);

        return msg;
    }

    public static Message MsgSend_PartyRemove(int socketId)
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(4);
        msg.Write(socketId);

        return msg;
    }


    public static Message MsgSend_PartyPromoteLeader(int socketId)
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(28);
        msg.Write(socketId);

        return msg;
    }

    public static Message MsgSend_PartyDestroy()
    {
        var msg = new Message(MessageID.WIZ_PARTY);

        msg.Write<byte>(5);

        return msg;
    }

    public static Message MsgSend_TargetHealthRequest(int targetId, byte byUpdateImmediately)
    {
        var msg = new Message(MessageID.WIZ_TARGET_HP);

        msg.Write(targetId);
        msg.Write(byUpdateImmediately);

        return msg;
    }

    public static Message MsgSend_UserRequest(ushort count, int[] playerIds)
    {
        var msg = new Message(MessageID.WIZ_REQ_USERIN);

        ReadOnlySpan<byte> bytesArray = MemoryMarshal.AsBytes(playerIds.AsSpan());

        msg.Write(count);
        msg.Write(bytesArray);

        return msg;
    }

    public static Message MsgSend_NpcRequest(ushort count, int[] npcIds)
    {
        var msg = new Message(MessageID.WIZ_REQ_NPCIN);

        ReadOnlySpan<byte> bytesArray = MemoryMarshal.AsBytes(npcIds.AsSpan());

        msg.Write(count);
        msg.Write(bytesArray);

        return msg;
    }

    public static Message MsgSend_StartSkillCastingAtTargetPacket(Skill skill, int socketId, int targetId)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_CASTING);
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(targetId);

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);

        msg.Write((short)skill.CastTime);

        return msg;
    }

    public static Message MsgSend_StartSkillCastingAtPosPacket(Skill skill, int socketId, Vector3 targetPosition)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);// byte 31

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_CASTING); //byte 
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(-1);

        msg.Write<int>((short)(targetPosition.X));
        msg.Write<int>((short)(targetPosition.Z));
        msg.Write<int>((short)(targetPosition.Y));

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);

        msg.Write((short)skill.CastTime);

        return msg;
    }

    public static Message MsgSend_StartFlyingAtTarget(Skill skill, int socketId, int targetId, Vector3 targetPosition, ushort arrowIndex = 0)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_FLYING);
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(targetId);

        msg.Write<int>((short)(targetPosition.X));
        msg.Write<int>((short)(targetPosition.Z));
        msg.Write<int>((short)(targetPosition.Y));

        msg.Write(arrowIndex);

        msg.Write(0);
        msg.Write(0);

        msg.Write((short)0);

        return msg;
    }

    public static Message MsgSend_StartSkillMagicAtTargetPacket(Skill skill, int socketId, int targetId, Vector3 targetPosition, ushort arrowIndex = 0)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_EFFECTING);
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(targetId);

        if (skill.CastTime == 0)
        {
            msg.Write(1);
            msg.Write(1);
            msg.Write(0);
        }
        else
        {
            msg.Write(targetPosition.X);
            msg.Write(targetPosition.Z);
            msg.Write(targetPosition.Y);


        }

        msg.Write(arrowIndex);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);
        msg.Write(0);



        return msg;
    }

    public static Message MsgSend_StartSkillMagicAtPosPacket(Skill skill, int socketId, Vector3 targetPosition)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_EFFECTING);
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(-1);

        msg.Write<int>((short)(targetPosition.X));
        msg.Write<int>((short)(targetPosition.Z));
        msg.Write<int>((short)(targetPosition.Y));

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);

        msg.Write<short>(0);

        return msg;
    }

    public static Message MsgSend_StartMagicAtTarget(Skill skill, int socketId, int targetId, Vector3 targetPosition, ushort arrowIndex = 0)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_FAIL);
        msg.Write(skill.Id);
        msg.Write(socketId);
        msg.Write(targetId);

        msg.Write((int)targetPosition.X);
        msg.Write((int)targetPosition.Z);
        msg.Write((int)targetPosition.Y);

        msg.Write(-101);

        msg.Write(arrowIndex);

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        return msg;
    }

    public static Message MsgSend_CancelSkillPacket(uint skillId, int socketId)
    {
        var msg = new Message(MessageID.WIZ_MAGIC_PROCESS);

        msg.Write((byte)SkillMagicType.SKILL_MAGIC_TYPE_CANCEL);
        msg.Write(skillId);
        msg.Write(socketId);
        msg.Write(socketId);

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        msg.Write(0);
        msg.Write(0);
        msg.Write(0);

        return msg;
    }

    public static Message MsgSend_CancelSkillPacket(Skill skill, int socketId)
    {
        return MsgSend_CancelSkillPacket((uint)skill.Id, socketId);
    }

    public static Message MsgSend_Attack(int targetId, float interval = 1f, float distance = 2f)
    {
        var msg = new Message(MessageID.WIZ_ATTACK);

        msg.Write<byte>(1);
        msg.Write<byte>(1);

        msg.Write(targetId);
        msg.Write((ushort)(interval * 100));
        msg.Write((ushort)(distance * 10));

        msg.Write<byte>(0);
        msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_ObjectEvent(short eventId, int npcId)
    {
        var msg = new Message(MessageID.WIZ_OBJECT_EVENT);

        msg.Write(eventId);
        msg.Write(npcId);

        return msg;
    }

    public static Message MsgSend_ItemRemove(byte slotType, byte pos, uint itemId)
    {
        var msg = new Message(MessageID.WIZ_ITEM_REMOVE);

        msg.Write(slotType); //1 = Equipment - 2 = Inventory
        msg.Write(pos);
        msg.Write(itemId);

        return msg;
    }

    public static Message MsgSend_AbilityPointChange(byte type, short point)
    {
        var msg = new Message(MessageID.WIZ_POINT_CHANGE);

        msg.Write(type);
        msg.Write(point);

        return msg;
    }

    public static Message MsgSend_SkillPointChange(byte type)
    {
        var msg = new Message(MessageID.WIZ_SKILLPT_CHANGE);

        msg.Write(type);

        return msg;
    }

    public static Message MsgSend_RequestItemBundleOpen(int bundleId)
    {
        var msg = new Message(MessageID.WIZ_BUNDLE_OPEN_REQ);

        msg.Write(bundleId);

        return msg;
    }

    public static Message MsgSend_RequestItemBundleGet(int bundleId, int itemId, short index)
    {
        var msg = new Message(MessageID.WIZ_ITEM_GET);

        msg.Write(bundleId);
        msg.Write(itemId);
        msg.Write(index);

        return msg;
    }

    public static Message MsgSend_InventoryItemMoveProcess(byte type, byte direction, uint itemId, byte currentPosition, byte targetPosition)
    {
        /*
         -----------------
         --- direction ---
         -----------------
         - Inv -> Arm 0x01
         - Arm -> Inv 0x02
         - Inv -> Inv 0x03
         - Arm -> Arm 0x04
         ------------------
        */

        var msg = new Message(MessageID.WIZ_ITEM_MOVE);

        msg.Write(type);
        msg.Write(direction);
        msg.Write(itemId);
        msg.Write(currentPosition);
        msg.Write(targetPosition);

        return msg;
    }

    public static Message MsgSend_ItemRepairRequest(byte direction, byte itemPosition, int npcId, uint itemId)
    {
        /*
         -----------------
         --- direction ---
         -----------------
         - Equipment 0x01
         - Inventory 0x02
         ------------------
        */

        var msg = new Message(MessageID.WIZ_ITEM_REPAIR);

        msg.Write(direction);
        msg.Write(itemPosition);
        msg.Write(npcId);
        msg.Write(itemId);

        return msg;
    }

    public static Message MsgSend_WarehouseGetIn(int npcId, uint itemId, byte page, byte currentPosition, byte targetSource, uint quantity)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(2);
        msg.Write(npcId);
        msg.Write(itemId);
        msg.Write(page);
        msg.Write(currentPosition);
        msg.Write(targetSource);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_WarehouseGetOut(int npcId, uint itemId, byte page, byte currentPosition, byte targetSource, uint quantity)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(3);
        msg.Write(npcId);
        msg.Write(itemId);
        msg.Write(page);
        msg.Write(currentPosition);
        msg.Write(targetSource);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_WarehouseToWarehouse(int npcId, uint itemId, byte page, byte currentPosition, byte targetSource)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(4);
        msg.Write(npcId);
        msg.Write(itemId);
        msg.Write(page);
        msg.Write(currentPosition);
        msg.Write(targetSource);

        return msg;
    }

    public static Message MsgSend_WarehouseInventoryToInventory(int npcId, uint itemId, byte page, byte currentPosition, byte targetSource)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(5);
        msg.Write(npcId);
        msg.Write(itemId);
        msg.Write(page);
        msg.Write(currentPosition);
        msg.Write(targetSource);

        return msg;
    }

    public static Message MsgSend_WarehouseGetInGold(int npcId, uint quantity)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(2);
        msg.Write(npcId);
        msg.Write(900_000_000);
        msg.Write<byte>(255);
        msg.Write<byte>(255);
        msg.Write<byte>(255);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_WarehouseGetOutGold(int npcId, uint quantity)
    {
        var msg = new Message(MessageID.WIZ_WAREHOUSE);

        msg.Write<byte>(2);
        msg.Write(npcId);
        msg.Write(900_000_000);
        msg.Write<byte>(255);
        msg.Write<byte>(255);
        msg.Write<byte>(255);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_ExhangeRequest(int playerId, byte near = 1)
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_REQ);
        msg.Write(playerId);
        msg.Write(near);

        return msg;
    }

    public static Message MsgSend_ExhangeAgree(bool yesNo)
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_AGREE);
        msg.Write(yesNo);

        return msg;
    }

    public static Message MsgSend_ExhangeAdd(byte currentPosition, uint itemId, uint quantity)
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_ADD);
        msg.Write(currentPosition);
        msg.Write(itemId);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_ExhangeAddGold(uint quantity)
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_ADD);
        msg.Write<byte>(255);
        msg.Write(900_000_000);
        msg.Write(quantity);

        return msg;
    }

    public static Message MsgSend_ExchangeDecision()
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_DECIDE);

        return msg;
    }

    public static Message MsgSend_ExchangeCancel()
    {
        var msg = new Message(MessageID.WIZ_EXCHANGE);

        msg.Write((byte)TradeSubPacket.TRADE_CANCEL);

        return msg;
    }

    public static Message MsgSend_ItemTradeBuy(int npcId, uint sellingGroup, uint itemId, byte inventoryItemPosition, short count, byte shopPage, byte shopItemPosition)
    {
        var msg = new Message(MessageID.WIZ_ITEM_TRADE);

        msg.Write<byte>(1);
        msg.Write(sellingGroup);
        msg.Write(npcId);

        msg.Write<byte>(1); // Todo: Multiple item count

        msg.Write(itemId);
        msg.Write(inventoryItemPosition);
        msg.Write(count);
        msg.Write(shopPage);
        msg.Write(shopItemPosition);

        return msg;
    }

    public static Message MsgSend_ItemTradeSell(int npcId, uint sellingGroup, uint itemId, byte inventoryItemPosition, short count)
    {
        var msg = new Message(MessageID.WIZ_ITEM_TRADE);

        msg.Write<byte>(2);
        msg.Write(sellingGroup);
        msg.Write(npcId);

        msg.Write<byte>(1); // Todo: Multiple item count

        msg.Write(itemId);
        msg.Write(inventoryItemPosition);
        msg.Write(count);
        //msg.Write<byte>(0);

        return msg;
    }

    public static Message MsgSend_FriendProcess(byte command)
    {
        var msg = new Message(MessageID.WIZ_FRIEND_PROCESS);

        msg.Write(command);

        return msg;
    }

    public static Message MsgSend_KnightsProcess(byte command)
    {
        var msg = new Message(MessageID.WIZ_KNIGHTS_PROCESS);

        msg.Write(command);

        switch (command)
        {
            case 0x22:
                msg.Write<byte>(0);
                break;
        }

        return msg;
    }

    public static Message MsgSend_SkillDataProcess(ushort command)
    {
        var msg = new Message(MessageID.WIZ_SKILLDATA);

        msg.Write(command);

        return msg;
    }

    public static Message MsgSend_ClassPure()
    {
        var msg = new Message(MessageID.WIZ_CLASS_CHANGE);

        msg.Write(ClassChange.CLASS_CHANGE_PURE);

        return msg;
    }

    public static Message MsgSend_StatChangeReq(ushort classId)
    {
        var msg = new Message(MessageID.WIZ_CLASS_CHANGE);

        msg.Write(ClassChange.CLASS_CHANGE_REQ);
        msg.Write(classId);

        return msg;
    }

    public static Message MsgSend_ClassResetStaatPoint()
    {
        var msg = new Message(MessageID.WIZ_CLASS_CHANGE);

        msg.Write(ClassChange.CLASS_ALL_POINT);

        return msg;
    }

    public static Message MsgSend_ClassResetSkillPoint()
    {
        var msg = new Message(MessageID.WIZ_CLASS_CHANGE);

        msg.Write(ClassChange.CLASS_SKILL_POINT);

        return msg;
    }

    public static Message MsgSend_ClassPointChangePriceQuery()
    {
        var msg = new Message(MessageID.WIZ_CLASS_CHANGE);
        msg.Write(ClassChange.CLASS_POINT_CHANGE_PRICE_QUERY);

        msg.Write<byte>(1);

        return msg;
    }

    public static Message MsgSend_QuestInit(int socketId)
    {
        var msg = new Message(MessageID.WIZ_QUEST);

        msg.Write<byte>(3);
        msg.Write(socketId);
        msg.Write<short>(0);

        return msg;
    }

    public static Message MsgSend_QuestCompleted(uint questId)
    {
        var msg = new Message(MessageID.WIZ_QUEST);

        msg.Write<byte>(4);
        msg.Write(questId);

        return msg;
    }

    public static Message MsgSend_QuestRemove(uint questId)
    {
        var msg = new Message(MessageID.WIZ_QUEST);

        msg.Write<byte>(5);
        msg.Write(questId);

        return msg;
    }

    public static Message MsgSend_QuestTake(uint questId)
    {
        var msg = new Message(MessageID.WIZ_QUEST);

        msg.Write<byte>(6);
        msg.Write(questId);

        return msg;
    }

    public static Message MsgSend_QuestGive(uint questId)
    {
        var msg = new Message(MessageID.WIZ_QUEST);

        msg.Write<byte>(7);
        msg.Write(questId);

        return msg;
    }

    public static Message MsgSend_SelectMenu(byte menuIndex, string luaName, bool accept = false)
    {
        var msg = new Message(MessageID.WIZ_SELECT_MSG);

        msg.Write(menuIndex);
        msg.Write(luaName, false);

        if (accept)
            msg.Write<byte>(255);

        return msg;
    }

    public static Message MsgSend_Event(byte opcode, uint itemId)
    {
        var msg = new Message(MessageID.WIZ_EVENT);

        msg.Write(opcode);
        msg.Write(itemId);

        return msg;
    }

    public static Message MsgSend_ExpSeal(bool on_off) {
        var msg = new Message(MessageID.WIZ_SEALEXP);

        if (on_off)
            msg.Write<byte>(0x01);
        else
            msg.Write<byte>(0x02);

        return msg;
    }

}