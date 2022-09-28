using System.Numerics;
using KOF.Cryptography;
using KOF.Core.Communications;
using KOF.Core.Enums;
using System.Diagnostics;
using KOF.Core.Extensions;
using KOF.Core.Models;
using KOF.Core.Handlers;
using KOF.Database.Models;
using KOF.Data;
using KOF.Database;

namespace KOF.Core.Services;

public partial class GameService
{
    [MessageHandler(MessageID.WIZ_COMPRESS_PACKET)]
    public Task MsgRecv_Compress(Session session, Message msg)
    {
        ushort compressLength = (ushort)msg.Read<uint>(); // out len.
        ushort uncompressLength = (ushort)msg.Read<uint>(); // in len.
        _ = msg.Read<uint>(); // crc32

        var compressed_data = new byte[compressLength];
        msg.Read(compressed_data.AsSpan());

        byte[] uncompressed_data = LZF.Decompress(compressed_data);
        var uncompressMessage = new Message(uncompressLength, uncompressed_data);

        if (uncompressMessage.ID.Value == MessageID.WIZ_MYINFO)
            session.RespondAsync(uncompressMessage).ConfigureAwait(false);
        else if (uncompressMessage.ID.Value == MessageID.WIZ_REQ_USERIN)
            session.RespondAsync(uncompressMessage).ConfigureAwait(false);
        else if (uncompressMessage.ID.Value == MessageID.WIZ_REQ_NPCIN)
            session.RespondAsync(uncompressMessage).ConfigureAwait(false);

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_MYINFO)]
    public Task MsgRecv_MyInfo(Session session, Message msg)
    {
        session.Client.CharacterHandler.ParseMySelf(session.Client, msg);

        session.SendAsync(MessageBuilder.MsgSend_KnightsProcess((byte)KnightsType.KNIGHTS_TOP10)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_FriendProcess((byte)FriendType.FRIEND_REQUEST)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_Helmet(0x00)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_KnightsProcess((byte)KnightsType.KNIGHTS_ALLY_LIST)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_SkillDataProcess((byte)SkillBarType.SKILL_DATA_LOAD)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_GenieProcess((byte)GenieType.GENIE_UPDATE_REQUEST)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_PROCESS)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_LETTER)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_GameStart((byte)session.Client.Character.GameState, session.Account.Character)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_QuestInit(session.Client.Character.Id)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_SurroundingUserProcess((byte)SurroundingUserType.USER_INFO, session.Account.Character)).ConfigureAwait(false);
        session.SendAsync(MessageBuilder.MsgSend_Rotate(session.Client.Character.Rotation, session.Account.Character)).ConfigureAwait(false);

        var spawnPosition = session.Client.Character.GetPosition();

        session.Client.CharacterHandler.SendMove(spawnPosition, spawnPosition, 0, 0);

        session.Client.CharacterHandler.InitializeController();
        session.Client.CharacterHandler.InitializeSkillList();
        session.Client.CharacterHandler.InitializeSelectedTargetList();

        session.Client.CharacterHandler.InitializeCharacterProcess();

        session.Client.CharacterHandler.MoveToSelectedRouteFinal();

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_GAMESTART)]
    public Task MsgRecv_GameStart(Session session, Message msg)
    {
        session.Client.Character.UntouchableTime = Environment.TickCount + 17000;
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_REQ_USERIN)]
    public Task MsgRecv_PlayerRequested(Session session, Message msg)
    {
        int count = msg.Read<ushort>();

        for (int i = 0; i < count; i++)
        {
            lock (session.Client.CharacterHandler.PlayerList)
            {
                var parsedCharacter = session.Client.CharacterHandler.ParseOther(msg, true);

                if (parsedCharacter.Id != session.Client.Character.Id)
                {
                    var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == parsedCharacter.Id);

                    if (character != null)
                        session.Client.CharacterHandler.PlayerList.Remove(character);

                    session.Client.CharacterHandler.PlayerList.Add(parsedCharacter);
                }
                else
                {
                    //session.Client.Character.SetPosition(parsedCharacter.GetPosition());
                }
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_USER_INOUT)]
    public Task MsgRecv_PlayerInOut(Session session, Message msg)
    {
        InOutType commandType = (InOutType)msg.Read<byte>();

        _ = msg.Read<byte>(); //Only CNKO

        switch (commandType)
        {
            case InOutType.INOUT_IN:
            case InOutType.INOUT_RESPAWN:
            case InOutType.INOUT_WARP:
                {
                    lock (session.Client.CharacterHandler.PlayerList)
                    {
                        var parsedCharacter = session.Client.CharacterHandler.ParseOther(msg, false);

                        if (parsedCharacter.Id != session.Client.Character.Id)
                        {
                            var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == parsedCharacter.Id);

                            if (character != null)
                                session.Client.CharacterHandler.PlayerList.Remove(character);

                            session.Client.CharacterHandler.PlayerList.Add(parsedCharacter);
                        }
                        //else
                            //session.Client.Character.SetPosition(parsedCharacter.GetPosition());
                    }
                }

                break;

            case InOutType.INOUT_OUT:
                ushort id = msg.Read<ushort>();
                lock (session.Client.CharacterHandler.PlayerList)
                    session.Client.CharacterHandler.PlayerList.RemoveAll((x) => x.Id == id);
                break;

            default:
                Debug.WriteLine($"MsgRecv_PlayerInOut:: {commandType}\t{msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_REGIONCHANGE)]
    public Task MsgRecv_PlayerRegionChange(Session session, Message msg)
    {
        byte commandType = msg.Read<byte>();

        if (commandType == 0)
        {
            //lock (session.Client.CharacterHandler.PlayerList)
              //  session.Client.CharacterHandler.PlayerList.Clear();

            return Task.CompletedTask;
        }
        else if (commandType == 1)
        {
            ushort count = msg.Read<ushort>();
            short[] playerIds = new short[count];
            msg.Read(playerIds.AsSpan());

            var playerIdList = playerIds.ToList();

            lock (session.Client.CharacterHandler.PlayerList)
            {
                session.Client.CharacterHandler.PlayerList.ToList().ForEach(x =>
                {
                    if (playerIdList.Contains(x.Id))
                        playerIdList.Remove(x.Id);
                    else
                        session.Client.CharacterHandler.PlayerList.Remove(x);
                });
            }

            //lock (session.Client.CharacterHandler.PlayerList)
            //  session.Client.CharacterHandler.PlayerList.RemoveAll(x => !playerIds.Contains(x.Id));

            return session.SendAsync(MessageBuilder.MsgSend_UserRequest((ushort)playerIdList.Count(), playerIdList.ToArray()));
        }
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_MOVE)]
    public Task MsgRecv_PlayerMovement(Session session, Message msg)
    {
        short socketId = msg.Read<short>();

        float will_x = msg.Read<ushort>() / 10.0f;
        float will_y = msg.Read<ushort>() / 10.0f;
        float will_z = msg.Read<ushort>() / 10.0f;

        short speed = msg.Read<short>();
        byte moveType = msg.Read<byte>();

        if (session.Client.Character.Id == socketId)
        {
            switch (moveType)
            {
                case 255: 
                    {
                        session.Client.Character.Moving = false;
                        session.Client.Character.MovePunishTime = Environment.TickCount;
                        session.Client.Character.SetPosition(new Vector3(will_x, will_y, will_z));
                        session.Client.CharacterHandler.InitializeMoveSpeed();
                    }
                    break;

                case 0: 
                    {
                        if(speed == 0)
                            session.Client.Character.Moving = false;
                        else
                            session.Client.Character.Moving = true; 
                    }
                    break;

                case 1:
                case 3:
                    {
                        session.Client.Character.Moving = true;
                    }
                    break;

                default:
                    Debug.WriteLine($"MsgRecv_PlayerMovement:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                    break;
            }

            session.Client.Character.SetWillPosition(new Vector3(will_x, will_y, will_z));

        }
        else
        {
            if (!session.Client.CharacterHandler.PlayerList.Any(x => x.Id == socketId)) //Ghost Player?
            {
                short[] playerIds = new short[1] { socketId };
                return session.SendAsync(MessageBuilder.MsgSend_UserRequest(1, playerIds));
            }
            else
            {
                var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == socketId);

                if (character != null)
                {
                    character.SetPosition(new Vector3(will_x, will_y, will_z));

                    if (moveType == 1 || moveType == 2)
                        character.Speed = speed;

                    character.MoveType = moveType;

                    if (moveType == 0)
                        character.Moving = false;
                    else
                        character.Moving = true;
                }
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_REQ_NPCIN)]
    public Task MsgRecv_NpcRequested(Session session, Message msg)
    {
        ushort count = msg.Read<ushort>();

        lock (session.Client.CharacterHandler.NpcList)
        {
            for (int i = 0; i < count; i++)
            {
                var parsedCharacter = session.Client.CharacterHandler.ParseNpc(msg);
                var character = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == parsedCharacter.Id);

                if (character != null)
                    session.Client.CharacterHandler.NpcList.Remove(character);

                session.Client.CharacterHandler.NpcList.Add(parsedCharacter);
            }

        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_NPC_INOUT)]
    public Task MsgRecv_NpcInOut(Session session, Message msg)
    {
        InOutType commandType = (InOutType)msg.Read<byte>();

        switch (commandType)
        {
            case InOutType.INOUT_IN:
                lock (session.Client.CharacterHandler.NpcList)
                {
                    var parsedCharacter = session.Client.CharacterHandler.ParseNpc(msg);
                    var character = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == parsedCharacter.Id);

                    if (character != null)
                        session.Client.CharacterHandler.NpcList.Remove(character);

                    session.Client.CharacterHandler.NpcList.Add(parsedCharacter);
                }
                break;

            case InOutType.INOUT_OUT:
                ushort npcId = msg.Read<ushort>();

                lock (session.Client.CharacterHandler.NpcList)
                    session.Client.CharacterHandler.NpcList.RemoveAll((x) => x.Id == npcId);
                break;

            default:
                Debug.WriteLine($"MsgRecv_NpcInOut:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_NPC_REGION)]
    public Task MsgRecv_NpcRegionChange(Session session, Message msg)
    {
        ushort count = msg.Read<ushort>();

        if (count == 0)
        {
            lock (session.Client.CharacterHandler.NpcList)
                session.Client.CharacterHandler.NpcList.Clear();

            return Task.CompletedTask;
        }

        short[] npcIds = new short[count];
        msg.Read(npcIds.AsSpan());

        var npcIdList = npcIds.ToList();

        lock (session.Client.CharacterHandler.NpcList)
        {
            session.Client.CharacterHandler.NpcList.ToList().ForEach(x =>
            {
                if (npcIdList.Contains(x.Id))
                {
                    npcIdList.Remove(x.Id);

                    if (x.State == (byte)StateAction.DYING || x.State == (byte)StateAction.DEATH)
                        session.Client.CharacterHandler.NpcList.Remove(x);
                }
                else
                    session.Client.CharacterHandler.NpcList.Remove(x);
            });

            //session.Client.CharacterHandler.NpcList.RemoveAll(x => !npcIdList.Contains(x.Id));
        }

        return session.SendAsync(MessageBuilder.MsgSend_NpcRequest((ushort)npcIdList.Count(), npcIdList.ToArray()));
    }

    [MessageHandler(MessageID.WIZ_NPC_MOVE)]
    public Task MsgRecv_NpcMovement(Session session, Message msg)
    {
        byte commandType = msg.Read<byte>();

        if (commandType == 1 || commandType == 2)
        {
            short npcId = msg.Read<short>();
            float positionX = msg.Read<ushort>() / 10.0f;
            float positionY = msg.Read<ushort>() / 10.0f;
            float positionZ = msg.Read<ushort>() / 10.0f;

            ushort speed = msg.Read<ushort>();

            if (!session.Client.CharacterHandler.NpcList.Any(x => x.Id == npcId)) //Ghost Monster?
            {
                short[] npcIds = new short[1] { npcId };
                return session.SendAsync(MessageBuilder.MsgSend_NpcRequest(1, npcIds));
            }
            else
            {
                var character = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == npcId);

                if (character != null)
                {
                    character.SetPosition(new Vector3(positionX, positionY, positionZ));
                    character.Speed = (short)speed;
                }
            }
        }
        else
            Debug.WriteLine($"MsgRecv_Npc_Move:: {msg.AsDataSpan().ToHexString()} Not Implemented!");

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_DEAD)]
    public Task MsgRecv_Dead(Session session, Message msg)
    {
        var character = session.Client.Character;

        ushort targetId = msg.Read<ushort>();

        if (character.Id == targetId)
        {
            character.Hp = 0;
            character.Status = 3;
            character.MoveType = 0;
            character.Speed = 45;
            character.State = (byte)StateAction.DEATH;

            character.BuffList.Clear();
        }
        else
        {
            if (targetId >= Config.NPC_BAND)
            {
                var deadCharacter = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == targetId);

                if (deadCharacter != null)
                {
                    deadCharacter.Hp = 0;
                    deadCharacter.Status = 3;
                    deadCharacter.MoveType = 0;
                    deadCharacter.State = (byte)StateAction.DEATH;
                }
            }
            else
            {
                var deadCharacter = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == targetId);

                if (deadCharacter != null)
                {
                    deadCharacter.Hp = 0;
                    deadCharacter.Status = 3;
                    deadCharacter.MoveType = 0;
                    deadCharacter.Speed = 45;
                    deadCharacter.State = (byte)StateAction.DEATH;
                }
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_HP_CHANGE)]
    public Task MsgRecv_HpChange(Session session, Message msg)
    {
        ushort max = msg.Read<ushort>();
        ushort min = msg.Read<ushort>();

        session.Client.Character.Hp = (short)min;
        session.Client.Character.MaxHp = max;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_MSP_CHANGE)]
    public Task MsgRecv_MpChange(Session session, Message msg)
    {
        ushort max = msg.Read<ushort>();
        ushort min = msg.Read<ushort>();

        session.Client.Character.Mp = (short)min;
        session.Client.Character.MaxMp = max;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_EXP_CHANGE)]
    public Task MsgRecv_ExpChange(Session session, Message msg)
    {
        _ = msg.Read<byte>(); // flag
        ulong exp = msg.Read<ulong>();

        session.Client.Character.Experience = (long)exp;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_GOLD_CHANGE)]
    public Task MsgRecv_GoldChange(Session session, Message msg)
    {
        _ = msg.Read<byte>(); // type

        uint changeAmount = msg.Read<uint>();
        uint gold = msg.Read<uint>();

        session.Client.Character.Gold = changeAmount;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_WARP)]
    public Task MsgRecv_Warp(Session session, Message msg)
    {
        session.Client.Character.X = msg.Read<ushort>() / 10.0f;
        session.Client.Character.Y = msg.Read<ushort>() / 10.0f;
        session.Client.Character.Z = msg.Read<ushort>() / 10.0f;

        session.Client.CharacterHandler.SelectTarget(-1);

        session.Client.Character.MovePunishTime = Environment.TickCount;
        
        session.Client.Character.SetPosition(session.Client.Character.GetPosition());
        session.Client.Character.SetMovePosition(Vector3.Zero);

        session.Client.CharacterHandler.InitializeMoveSpeed();

        if (session.Client.CharacterHandler.WarpAfterSyncRoute)
            session.Client.CharacterHandler.MoveToSelectedRouteFinal();

        session.Client.CharacterHandler.WarpAfterSyncRoute = true;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_TARGET_HP)]
    public Task MsgRecv_TargetHp(Session session, Message msg)
    {
        short targetId = msg.Read<short>();
        byte byUpdateImmediately = msg.Read<byte>();
        uint iTargetHPMax = msg.Read<uint>();
        uint iTargetHPCur = msg.Read<uint>();
        short iTargetHPChange = msg.Read<short>();

        var character = session.Client.Character;

        if (targetId == character.Id)
        {
            character.Hp = (short)iTargetHPCur;
            character.MaxHp = (ushort)iTargetHPMax;

            if (character.Hp == 0 || character.MaxHp == 0)
                character.Status = 3;
        }
        else
        {
            if (targetId >= Config.NPC_BAND)
            {
                var Target = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == targetId);

                if (Target != null)
                {
                    Target.Hp = (short)iTargetHPCur;
                    Target.MaxHp = (ushort)iTargetHPMax;

                    if (Target.Hp == 0 || Target.MaxHp == 0)
                    {
                        Target.Status = 3;

                        var bundle = session.Client.CharacterHandler.LootList.FirstOrDefault(x => x.NpcId == targetId);

                        if(bundle != null)
                            bundle.DropTime = Environment.TickCount;
                    }
                }
            }
            else
            {
                var Target = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == targetId);

                if (Target != null)
                {
                    Target.Hp = (short)iTargetHPCur;
                    Target.MaxHp = (ushort)iTargetHPMax;

                    if (Target.Hp == 0 || Target.MaxHp == 0)
                        Target.Status = 3;
                }
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ATTACK)]
    public Task MsgRecv_Attack(Session session, Message msg)
    {
        var commandType = msg.Read<byte>();
        AttackResult result = (AttackResult)msg.Read<byte>();
        switch (result)
        {
            case AttackResult.ATTACK_FAIL:
                break;

            case AttackResult.ATTACK_SUCCESS:
                break;

            case AttackResult.ATTACK_TARGET_DEAD:
            case AttackResult.ATTACK_TARGET_DEAD_OK:
                _ = msg.Read<ushort>(); // attackerId
                var targetId = msg.Read<ushort>();

                if (targetId == session.Client.Character.Id)
                {
                    session.Client.Character.Hp = 0;
                    session.Client.Character.Status = 3;
                    session.Client.Character.MoveType = 0;
                    session.Client.Character.State = (byte)StateAction.DEATH;
                }
                else
                {
                    if (targetId >= Config.NPC_BAND)
                    {
                        var character = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == targetId);

                        if (character != null)
                        {
                            character.Hp = 0;
                            character.Status = 3;
                            character.MoveType = 0;
                            character.State = (byte)StateAction.DEATH;
                        }
                    }
                    else
                    {
                        var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == targetId);

                        if (character != null)
                        {
                            character.Hp = 0;
                            character.Status = 3;
                            character.MoveType = 0;
                        }
                    }
                }
                break;

            default:
                Debug.WriteLine($"MsgRecv_Attack:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_MAGIC_PROCESS)]
    public Task MsgRecv_MagicProcess(Session session, Message msg)
    {
        SkillMagicType skillMagicType = (SkillMagicType)msg.Read<byte>();

        switch (skillMagicType)
        {
            case SkillMagicType.SKILL_MAGIC_TYPE_CASTING:
                {
                    var skillId = msg.Read<int>();
                    var sourceId = msg.Read<short>();
                    var targetId = msg.Read<short>();

                    session.Client.CharacterHandler.SkillCastingProcess(skillId, sourceId, targetId);
                }
                break;

            case SkillMagicType.SKILL_MAGIC_TYPE_FLYING:
                {
                    var skillId = msg.Read<int>();
                    var sourceId = msg.Read<short>();
                    var targetId = msg.Read<short>();

                    session.Client.CharacterHandler.SkillFlyingProcess(skillId, sourceId, targetId);
                }
                break;

            case SkillMagicType.SKILL_MAGIC_TYPE_EFFECTING:
                {
                    var skillId = msg.Read<int>();
                    var sourceId = msg.Read<short>();
                    var targetId = msg.Read<short>();

                    session.Client.CharacterHandler.SkillEffectingProcess(skillId, sourceId, targetId);
                }
                break;

            case SkillMagicType.SKILL_MAGIC_TYPE_FAIL:
                {
                    var skillId = msg.Read<int>();
                    var sourceId = msg.Read<short>();
                    var targetId = msg.Read<short>();

                    List<short> data = new();

                    for (int i = 0; i < 6; i++)
                        data.Add(msg.Read<short>());

                    if (data[3] == -100 || data[3] == -103)
                        session.Client.CharacterHandler.SkillFailedProcess(skillId, sourceId, targetId);
                }
                break;

            case SkillMagicType.SKILL_MAGIC_TYPE_BUFF:
                {
                    var buffType = msg.Read<byte>();

                    switch (buffType)
                    {
                        case (byte)BuffType.BUFF_TYPE_SPEED:
                            session.Client.Character.Speed = 45;
                            break;
                    }

                    session.Client.CharacterHandler.SkillBuffProcess(buffType);
                }
                break;

            default:
                Debug.WriteLine($"MsgRecv_MagicProcess:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_SHOPPING_MALL)]
    public Task MsgRecv_ShoppingMall(Session session, Message msg)
    {
        ShoppingMallType commandType = (ShoppingMallType)msg.Read<byte>();
        switch (commandType)
        {
            case ShoppingMallType.STORE_CLOSE:
                for (int i = Config.SLOT_MAX; i < Config.SLOT_MAX + Config.HAVE_MAX; i++)
                {
                    Inventory Item = new()
                    {
                        Pos = (byte)i,
                        ItemID = msg.Read<uint>(),
                        Durability = msg.Read<ushort>(),
                        Count = msg.Read<ushort>(),
                        Flag = msg.Read<byte>(),
                        RentalTime = msg.Read<short>(),
                        Serial = msg.Read<uint>()
                    };

                    Item.Table = TableHandler.GetItemById((int)Item.ItemID);

                    var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == Item.ItemID);

                    if (supplyFlag != null)
                        Item.SupplyFlag = (byte)supplyFlag.Flag;

                    session.Client.Character.Inventory[i] = Item;
                }
                break;
            case ShoppingMallType.STORE_PROCESS:
            case ShoppingMallType.STORE_LETTER:
                break;

            default:
                Debug.WriteLine($"MsgRecv_ShoppingMall:: {commandType} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_POINT_CHANGE)]
    public Task MsgRecv_MyInfo_PointChange(Session session, Message msg)
    {
        StatType type = (StatType)msg.Read<byte>();

        session.Client.Character.Stats[(int)type - 1] = (byte)msg.Read<short>();
        session.Client.Character.MaxHp = (ushort)msg.Read<short>();
        session.Client.Character.MaxMp = (ushort)msg.Read<short>();
        session.Client.Character.TotalHit = (ushort)msg.Read<short>();
        session.Client.Character.MaxWeight = msg.Read<uint>() / 10;

        session.Client.Character.Points--;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ITEM_MOVE)]
    public Task MsgRecv_ItemMove(Session session, Message msg)
    {
        var result = msg.Read<byte>();

        if (result != 0)
        {
            var subResult = msg.Read<byte>();

            if (subResult != 0)
            {
                session.Client.Character.TotalHit = msg.Read<ushort>();
                session.Client.Character.TotalAc = msg.Read<ushort>();
                session.Client.Character.MaxWeight = msg.Read<uint>() / 10;

                _ = msg.Read<ushort>();

                session.Client.Character.MaxHp = (ushort)msg.Read<short>();
                session.Client.Character.MaxMp = (ushort)msg.Read<short>();

                session.Client.Character.StatsItemBonuses[(int)StatType.STAT_STR - 1] = (byte)msg.Read<short>();
                session.Client.Character.StatsItemBonuses[(int)StatType.STAT_HP - 1] = (byte)msg.Read<short>();
                session.Client.Character.StatsItemBonuses[(int)StatType.STAT_DEX - 1] = (byte)msg.Read<short>();
                session.Client.Character.StatsItemBonuses[(int)StatType.STAT_INT - 1] = (byte)msg.Read<short>();
                session.Client.Character.StatsItemBonuses[(int)StatType.STAT_MP - 1] = (byte)msg.Read<short>();

                session.Client.Character.FireR = (byte)msg.Read<short>();
                session.Client.Character.ColdR = (byte)msg.Read<short>();
                session.Client.Character.LightningR = (byte)msg.Read<short>();
                session.Client.Character.MagicR = (byte)msg.Read<short>();
                session.Client.Character.DiseaseR = (byte)msg.Read<short>();
                session.Client.Character.PoisonR = (byte)msg.Read<short>();
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ITEM_COUNT_CHANGE)]
    public Task MsgRecv_ItemCountChange(Session session, Message msg)
    {
        var totalCount = msg.Read<short>();

        for (int i = 0; i < totalCount; i++)
        {
            var sourcePosition = msg.Read<byte>();
            var destinationPosition = msg.Read<byte>();

            var itemId = msg.Read<uint>();
            var count = msg.Read<uint>();

            var newItem = msg.Read<byte>();
            var durability = msg.Read<ushort>();

            _ = msg.Read<uint>();

            var expirationTime = msg.Read<uint>();

            session.Client.Character.Inventory[14 + destinationPosition].ItemID = itemId;
            session.Client.Character.Inventory[14 + destinationPosition].Count = (ushort)count;
            session.Client.Character.Inventory[14 + destinationPosition].ExpirationTime = expirationTime;

            var itemTable = TableHandler.GetItemById((int)itemId);

            session.Client.Character.Inventory[14 + destinationPosition].Table = itemTable;

            if(newItem == 0)
                session.Client.Character.Inventory[14 + destinationPosition].Durability = durability;
            else
            {
                if(itemTable != null)
                    session.Client.Character.Inventory[14 + destinationPosition].Durability = (ushort)itemTable.Durability;
            }
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_SKILLPT_CHANGE)]
    public Task MsgRecv_SkillChange(Session session, Message msg)
    {
        var type = msg.Read<byte>();
        var value = msg.Read<byte>();

        session.Client.Character.Skills[type] = value;
        session.Client.Character.Skills[0]++;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ITEM_DROP)]
    public Task MsgRecv_ItemBundleDrop(Session session, Message msg)
    {
        var npcId = msg.Read<short>();
        var bundleId = msg.Read<uint>();
        var itemCount = msg.Read<byte>();

        session.Client.CharacterHandler.ItemBundleDrop(npcId, bundleId, itemCount);

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_BUNDLE_OPEN_REQ)]
    public Task MsgRecv_ItemBundleOpen(Session session, Message msg)
    {
        var bundleId = msg.Read<uint>();
        var result = msg.Read<byte>();

        var bundle = session.Client.CharacterHandler.LootList.FirstOrDefault(x => x.BundleId == bundleId);

        if (bundle != null)
        {
            switch (result)
            {
                case 0:
                    {
                        Debug.WriteLine($"MsgRecv_ItemBundleOpen:: Failed");
                        session.Client.CharacterHandler.LootList.RemoveAll(x => x.BundleId == bundleId);
                    }
                    break;

                case 1:
                    {
                        for (int i = 0; i < bundle.ItemCount; i++)
                        {
                            var itemId = msg.Read<uint>();
                            var count = msg.Read<short>();

                            var itemTable = TableHandler.GetItemById((int)itemId);

                            if (itemId == 0 || itemId == 900000000 || itemTable == null)
                                session.Client.CharacterHandler.ItemBundleOpen(bundleId, itemId, (short)i);
                            else
                            {
                                if (itemTable.SellPrice >= session.Client.CharacterHandler.Controller.GetControl("LootMinPrice", 10000) || itemTable.ExtensionBaseIdActive)
                                    session.Client.CharacterHandler.ItemBundleOpen(bundleId, itemId, (short)i);
                            }
                        }

                        session.Client.CharacterHandler.LootList.RemoveAll(x => x.BundleId == bundleId);
                    }
                    break;

                default:
                    Debug.WriteLine($"MsgRecv_ItemBundleOpen :: {bundleId} - {result} Not Implemented!");
                    break;
            }

            
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ITEM_GET)]
    public Task MsgRecv_ItemDroppedGetResult(Session session, Message msg)
    {
        var result = msg.Read<byte>();

        switch (result)
        {
            case 0:
                {
                    var bundleId = msg.Read<uint>();
                    session.Client.CharacterHandler.LootList.RemoveAll(x => x.BundleId == bundleId);
                }
                break;

            case 1:
            case 2:
            case 5:
                {
                    var bundleId = msg.Read<uint>();
                    var pos = msg.Read<byte>();
                    var itemId = msg.Read<uint>();

                    ushort itemCount = 0;

                    if (result == 1 || result == 5)
                        itemCount = msg.Read<ushort>();

                    var gold = msg.Read<uint>();

                    if (pos != 255)
                    {
                        session.Client.Character.Inventory[14 + pos].ItemID = itemId;
                        session.Client.Character.Inventory[14 + pos].Count = itemCount;
                        session.Client.Character.Inventory[14 + pos].Table = TableHandler.GetItemById((int)itemId);

                        var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == itemId);

                        if (supplyFlag != null)
                            session.Client.Character.Inventory[14 + pos].SupplyFlag = (byte)supplyFlag.Flag;
                    }

                    session.Client.Character.Gold = gold;
                    session.Client.CharacterHandler.LootList.RemoveAll(x => x.BundleId == bundleId);
                }
                break;

            case 3:
                {
                    var bundleId = msg.Read<uint>();
                    session.Client.CharacterHandler.LootList.RemoveAll(x => x.BundleId == bundleId);
                }
                break;

            //Inventory ıs full
            case 6:
                Debug.WriteLine($"MsgRecv_ItemDroppedGetResult :: Inventory Full");
                break;

            default:
                Debug.WriteLine($"MsgRecv_ItemDroppedGetResult :: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_WEIGHT_CHANGE)]
    public Task MsgRecv_ItemWeightChange(Session session, Message msg)
    {
        session.Client.Character.Weight = msg.Read<uint>() / 10;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_LEVEL_CHANGE)]
    public Task MsgRecv_MyInfo_LevelChange(Session session, Message msg)
    {
        var id = msg.Read<ushort>();
        var level = msg.Read<byte>();

        if (id == session.Client.Character.Id)
        {
            session.Client.Character.Level = level;

            session.Client.Character.Points = msg.Read<byte>();

            session.Client.Character.Skills[0] = (byte)msg.Read<ushort>();

            session.Client.Character.MaxExperience = msg.Read<long>();
            session.Client.Character.Experience = msg.Read<long>();

            session.Client.Character.MaxHp = msg.Read<ushort>();
            session.Client.Character.Hp = msg.Read<short>();

            session.Client.Character.MaxMp = msg.Read<ushort>();
            session.Client.Character.Mp = msg.Read<short>();

            session.Client.Character.MaxWeight = msg.Read<uint>() / 10;
            session.Client.Character.Weight = msg.Read<uint>() / 10;

            session.Client.CharacterHandler.InitializeSkillList();
        }
        else
        {
            var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == id)!;

            if (character != null)
                character.Level = level;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_REGENE)]
    public Task MsgRecv_Regen(Session session, Message msg)
    {
        session.Client.Character.X = msg.Read<ushort>() / 10.0f;
        session.Client.Character.Y = msg.Read<ushort>() / 10.0f;
        session.Client.Character.Z = msg.Read<ushort>() / 10.0f;

        session.Client.Character.SetPosition(session.Client.Character.GetPosition());
        session.Client.Character.SetMovePosition(Vector3.Zero);

        session.Client.CharacterHandler.SelectTarget(1);

        session.Client.Character.Status = 0;
        session.Client.Character.MoveType = 3;
        session.Client.Character.Speed = 45;
        session.Client.Character.State = 0;

        session.Client.Character.UntouchableTime = Environment.TickCount + 15000;

        session.Client.Character.BuffList.Clear();

        session.Client.CharacterHandler.RunSelectedRoute();

        if (session.Client.Character.IsInMonsterStone() && session.Client.CharacterHandler.Controller != null)
            session.Client.CharacterHandler.Controller.SetControl("MonsterStonePhase", 0);

       return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_DURATION)]
    public Task MsgRecv_DurabilityChange(Session session, Message msg)
    {
        var pos = msg.Read<byte>();
        var durability = msg.Read<ushort>();

        session.Client.Character.Inventory[pos].Durability = durability;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_PARTY)]
    public Task MsgRecv_Party(Session session, Message msg)
    {
        PartyUpdateType type = (PartyUpdateType)msg.Read<byte>();

        switch (type)
        {
            case PartyUpdateType.Create:
                {
                    _ = msg.Read(true, "gb2312");
                    byte commandType = msg.Read<byte>();

                    lock (session.Client.Character.Party)
                    {
                        session.Client.Character.Party.Clear();
                    }
                }
                break;

            case PartyUpdateType.Insert:
                {
                    ushort insertMemberId = msg.Read<ushort>();
                    string invitedName = msg.Read(true, "gb2312");

                    var client = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.Name == invitedName)!;

                    if (client != null)
                        return session.SendAsync(MessageBuilder.MsgSend_PartyAccept(true));
                }
                break;

            case PartyUpdateType.Joined:
                {
                    if (msg.Peek(new byte[] { 0xFF, 0xFF })) // failed or level diff.
                        return Task.CompletedTask;

                    if (msg.Peek(new byte[] { 0xFA, 0xFF })) // failed or level diff.
                        return Task.CompletedTask;

                    if (msg.Peek(new byte[] { 0xFE, 0xFF })) // failed or level diff.
                        return Task.CompletedTask;

                    PartyMember memberJoined = PartyMember.FromMessage(msg);

                    if (memberJoined.Index == 1)
                    {
                        if (session.Client.Character.Party.Leader == null)
                        {
                            session.Client.Character.Party.Leader = new()
                            {
                                MemberId = session.Client.Character.Id,
                                Index = 100,
                                Name = session.Client.Character.Name,
                                MaxHealth = session.Client.Character.MaxHp,
                                Health = (ushort)session.Client.Character.Hp,
                                Level = session.Client.Character.Level,
                                Class = session.Client.Character.Class,
                                MaxMana = session.Client.Character.MaxMp,
                                Mana = (ushort)session.Client.Character.Mp,
                                NationId = session.Client.Character.NationId,
                                LeaderRank = 0
                            };

                            if (!session.Client.Character.Party.Members.Any(x => x.Name == session.Client.Character.Name))
                            {
                                lock (session.Client.Character.Party.Members)
                                    session.Client.Character.Party.Members.Add(session.Client.Character.Party.Leader);
                            }

                            if (!session.Client.Character.Party.Members.Any(x => x.Name == memberJoined.Name))
                            {
                                lock (session.Client.Character.Party.Members)
                                    session.Client.Character.Party.Members.Add(memberJoined);
                            }
                        }
                        else
                        {
                            if (!session.Client.Character.Party.Members.Any(x => x.Name == memberJoined.Name))
                            {
                                lock (session.Client.Character.Party.Members)
                                    session.Client.Character.Party.Members.Add(memberJoined);
                            }
                        }
                    }
                    else if (memberJoined.Index == 100)
                    {
                        session.Client.Character.Party.Leader = memberJoined;

                        if (!session.Client.Character.Party.Members.Any(x => x.Name == memberJoined.Name))
                        {
                            lock (session.Client.Character.Party.Members)
                                session.Client.Character.Party.Members.Add(memberJoined);
                        }
                    }

                    else
                    {
                        if (!session.Client.Character.Party.Members.Any(x => x.Name == memberJoined.Name))
                        {
                            lock (session.Client.Character.Party.Members)
                                session.Client.Character.Party.Members.Add(memberJoined);
                        }
                    }
                }
                break;

            case PartyUpdateType.Leave:
                {
                    var leftMemberId = msg.Read<ushort>();

                    lock (session.Client.Character.Party.Members)
                        session.Client.Character.Party.Members.RemoveAll((partyMember) => partyMember.MemberId == leftMemberId);

                    var leaderExist = session.Client.Character.Party.Members.Any(x => x.Index == 100);

                    if (!leaderExist)
                    {
                        var newLeader = session.Client.Character.Party.Members.OrderBy(x => x.Index).FirstOrDefault()!;

                        if (newLeader != null)
                        {
                            newLeader.Index = 100;
                            session.Client.Character.Party.Leader = newLeader;
                        }
                        else
                        {
                            lock (session.Client.Character.Party)
                                session.Client.Character.Party.Clear();
                        }
                    }
                }
                break;

            case PartyUpdateType.Dismissed:
                {
                    lock (session.Client.Character.Party)
                        session.Client.Character.Party.Clear();
                }
                break;

            case PartyUpdateType.HealthManaChange:
                {
                    var HPMPMemberId = msg.Read<ushort>();

                    session.Client.Character.Party.Members.ForEach((partyMember) =>
                    {
                        if (partyMember.MemberId == HPMPMemberId)
                        {
                            partyMember.MaxHealth = msg.Read<ushort>();
                            partyMember.Health = msg.Read<ushort>();
                            partyMember.MaxMana = msg.Read<ushort>();
                            partyMember.Mana = msg.Read<ushort>();
                        }
                    });
                }

                break;

            case PartyUpdateType.LevelChange:
                {
                    var levelMemberId = msg.Read<ushort>();

                    session.Client.Character.Party.Members.ForEach((partyMember) =>
                    {
                        if (partyMember.MemberId == levelMemberId)
                            partyMember.Level = msg.Read<byte>();
                    });
                }

                break;

            case PartyUpdateType.ClassChange:
                {
                    var classMemberId = msg.Read<ushort>();

                    session.Client.Character.Party.Members.ForEach((partyMember) =>
                    {
                        if (partyMember.MemberId == classMemberId)
                        {
                            partyMember.Class = msg.Read<ushort>();
                        }
                    });
                }
                break;

            case PartyUpdateType.StatusChange:
                {
                    var statusChangeMemberId = msg.Read<ushort>();

                    session.Client.Character.Party.Members.ForEach((partyMember) =>
                    {
                        if (partyMember.MemberId == statusChangeMemberId)
                        {
                            partyMember.PlayerStatus = msg.Read<byte>();
                            partyMember.PlayerStatusBehavior = msg.Read<byte>();
                        }
                    });
                }
                break;

            default:
                Debug.WriteLine($"MsgRecv_Party:: {msg.AsDataSpan().ToHexString()}");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_STATE_CHANGE)]
    public Task MsgRecv_StateChange(Session session, Message msg)
    {
        ushort socketId = msg.Read<ushort>();
        byte subPacket = msg.Read<byte>();
        uint state = msg.Read<uint>();

        switch (subPacket)
        {
            case (byte)StateSubPacket.STATE_CHANGE_SITDOWN:
                {
                    if (socketId >= Config.NPC_BAND)
                    {
                        var character = session.Client.CharacterHandler.NpcList.FirstOrDefault(x => x.Id == socketId);

                        //if (character != null)
                        //  character.State = (byte)state;
                    }
                    else
                    {
                        var character = session.Client.CharacterHandler.PlayerList.FirstOrDefault(x => x.Id == socketId);

                        //if (character != null)
                        //  character.State = (byte)state;
                    }
                }
                break;

            default:
                Debug.WriteLine($"MsgRecv_StateChange:: SubPacket: {subPacket} not implemented!");
                break;
        }
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_PET)]
    public Task MsgRecv_Pet(Session session, Message msg)
    {
        //76 0105060100cf1dd63f
        Debug.WriteLine($"MsgRecv_Pet:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_EFFECT)]
    public Task MsgRecv_Effect(Session session, Message msg)
    {
        Debug.WriteLine($"MsgRecv_Effect:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_GENIE)]
    public Task MsgRecv_Genie(Session session, Message msg)
    {
        //970107150001
        Debug.WriteLine($"MsgRecv_Genie:: {msg.AsDataSpan().ToHexString()} Not Implemented!");
        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ZONE_CHANGE)]
    public Task MsgRecv_ZoneChange(Session session, Message msg)
    {
        byte opcode = msg.Read<byte>();

        switch (opcode)
        {
            case (byte)ZoneChangeOpcode.ZoneChangeLoading:
                session.SendAsync(MessageBuilder.MsgSend_ZoneChange((byte)ZoneChangeOpcode.ZoneChangeLoaded)).ConfigureAwait(false);
                break;
            case (byte)ZoneChangeOpcode.ZoneChangeLoaded:
                session.Client.Character.UntouchableTime = Environment.TickCount + 17000;
                session.SendAsync(MessageBuilder.MsgSend_ZoneChange((byte)ZoneChangeOpcode.ZoneChangeLoaded)).ConfigureAwait(false);
                break;
            case (byte)ZoneChangeOpcode.ZoneChangeTeleport:
                session.Client.Character.Zone = (byte)msg.Read<short>();
                session.Client.Character.Zone = (byte)Character.GetRepresentZone(session.Client.Character.Zone);

                session.Client.Character.X = msg.Read<ushort>() / 10.0f;
                session.Client.Character.Y = msg.Read<ushort>() / 10.0f;
                session.Client.Character.Z = msg.Read<ushort>() / 10.0f;

                _ = msg.Read<byte>(); //VictoryNation

                session.Client.Character.SetPosition(session.Client.Character.GetPosition());
                session.Client.Character.SetMovePosition(Vector3.Zero);

                session.Client.CharacterHandler.RouteQueue.Clear();

                session.Client.Character.UntouchableTime = Environment.TickCount + 17000;

                session.SendAsync(MessageBuilder.MsgSend_SpeedCheck(session.Client.StartTime, true)).ConfigureAwait(false);

                session.Client.CharacterHandler.WarpList.Clear();

                session.Client.Character.Party.Clear();

                ClientHandler.LoadZone(session.Client.Character.Zone).ContinueWith(Task =>
                {
                    session.SendAsync(MessageBuilder.MsgSend_ZoneChange((byte)ZoneChangeOpcode.ZoneChangeLoading)).ConfigureAwait(false);
                });
                break;
            case (byte)ZoneChangeOpcode.ZoneChangeMilitaryCamp:
                Debug.WriteLine($"MsgRecv_ZoneChange:: ZoneChangeMilitaryCamp: {msg.AsDataSpan().ToHexString()} Not Implemented!");
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_WARP_LIST)]
    public Task MsgRecv_WarpList(Session session, Message msg)
    {
        var opcode = msg.Read<byte>();

        switch (opcode)
        {
            case 1: //Warp List Load
                {
                    var warpListCount = msg.Read<short>();

                    session.Client.CharacterHandler.WarpList.Clear();

                    if (warpListCount == 0) return Task.CompletedTask;

                    for (int i = 0; i < warpListCount; i++)
                    {
                        var Id = msg.Read<short>();
                        var name = msg.Read(true, "gb2312");
                        var agreement = msg.Read(true, "gb2312");
                        var zone = msg.Read<short>();
                        var maxUser = msg.Read<short>();
                        var gold = msg.Read<int>();

                        var warpInfo = TableHandler.GetWarpInfoList().FirstOrDefault(x => x.Id == Id)!;

                        if (warpInfo != null)
                        {
                            warpInfo.Zone = zone;
                            warpInfo.MaxUser = maxUser;
                            warpInfo.Gold = gold;

                            session.Client.CharacterHandler.WarpList.Add(warpInfo);
                        }
                    }
                }
                break;

            case 2:
                {
                    var subOpcode = msg.Read<byte>();

                    switch (subOpcode)
                    {
                        case 1: //Valid teleport
                            session.Client.CharacterHandler.WarpList.Clear();
                            break;

                        default:
                            Debug.WriteLine($"MsgRecv_WarpList:: Opcode {opcode} - SubOpcode {subOpcode} : {msg.AsDataSpan().ToHexString()} Not Implemented!");
                            break;
                    }


                }
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_EXCHANGE)]
    public Task MsgRecv_PerTrade(Session session, Message msg)
    {
        var opcode = msg.Read<byte>();

        switch (opcode)
        {
            case (byte)TradeSubPacket.TRADE_REQ:
                {
                    var otherId = msg.Read<short>();
                    session.Client.CharacterHandler.ExhangeRequestProcess(otherId);
                }
                break;

            case (byte)TradeSubPacket.TRADE_AGREE:
                {
                    var result = msg.Read<byte>();

                    if (result == 0)
                    {
                        session.Client.Character.TradedUserId = 0;
                        session.Client.Character.TradeRequestedUserId = 0;
                    }
                    else if (result == 1)
                        session.Client.CharacterHandler.ExhangeAgreeProcess();
                }
                break;

            case (byte)TradeSubPacket.TRADE_ADD:
                {
                    var result = msg.Read<byte>();
                }
                break;

            case (byte)TradeSubPacket.TRADE_OTHER_ADD:
                {
                    var itemId = msg.Read<uint>();
                    var count = msg.Read<uint>();
                    var durability = msg.Read<short>();
                }
                break;

            case (byte)TradeSubPacket.TRADE_DECIDE:
                {
                    Debug.WriteLine($"MsgRecv_PerTrade:: TRADE_DECIDE Not Implemented! :: {msg.AsDataSpan().ToHexString()} ");
                }
                break;

            case (byte)TradeSubPacket.TRADE_OTHER_DECIDE:
                {
                    session.Client.CharacterHandler.ExhangeAgreeProcess();
                    session.SendAsync(MessageBuilder.MsgSend_ExchangeDecision()).ConfigureAwait(false);
                }
                break;

            case (byte)TradeSubPacket.TRADE_DONE:
                {
                    var result = msg.Read<byte>();

                    if (result == 1)
                    {
                        var totalGold = msg.Read<uint>();
                        var itemCount = msg.Read<short>();

                        for (int i = 0; i < itemCount; i++)
                        {
                            var itemPos = msg.Read<byte>();
                            var itemId = msg.Read<uint>();
                            var count = msg.Read<short>();
                            var durability = msg.Read<short>();

                            session.Client.Character.Inventory[14 + itemPos].ItemID = itemId;
                            session.Client.Character.Inventory[14 + itemPos].Count = (ushort)count;
                            session.Client.Character.Inventory[14 + itemPos].Durability = (ushort)durability;
                            session.Client.Character.Inventory[14 + itemPos].Table = TableHandler.GetItemById((int)itemId);

                            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == itemId);

                            if (supplyFlag != null)
                                session.Client.Character.Inventory[14 + itemPos].SupplyFlag = (byte)supplyFlag.Flag;
                        }
                    }

                    session.Client.Character.TradedUserId = 0;
                    session.Client.Character.TradeRequestedUserId = 0;
                }
                break;


            case (byte)TradeSubPacket.TRADE_CANCEL:
                {
                    session.Client.Character.TradedUserId = 0;
                    session.Client.Character.TradeRequestedUserId = 0;
                }
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_QUEST)]
    public Task MsgRecv_Quest(Session session, Message msg)
    {
        var opcode = msg.Read<byte>();

        switch (opcode)
        {
            case 1: // Load Data
                {
                    var questCount = msg.Read<short>();

                    for (int i = 0; i < questCount; i++)
                    {
                        var questId = msg.Read<short>();
                        var questStatus = msg.Read<byte>();

                        var quest = new Quest()
                        {
                            Id = questId,
                            Status = questStatus
                        };

                        quest.Build(session.Client.Character);

                        session.Client.Character.ActiveQuestList.Add(quest);
                    }
                }
                break;

            case 2: // Progression
                {
                    var questId = msg.Read<short>();
                    var questStatus = msg.Read<byte>();

                    switch (questStatus)
                    {
                        case 1:
                        case 2:
                            {
                                var quest = session.Client.Character.ActiveQuestList.FirstOrDefault(x => x.Id == questId)!;

                                if (quest == null)
                                {
                                    quest = new Quest()
                                    {
                                        Id = questId,
                                        Status = questStatus
                                    };

                                    quest.Build(session.Client.Character);

                                    session.Client.Character.ActiveQuestList.Add(quest);
                                }
                                else
                                    quest.Status = questStatus;
                            }
                            break;

                        case 3:
                        case 4:
                            {
                                session.Client.Character.ActiveQuestList.RemoveAll(x => x.Id == questId);
                            }
                            break;
                    }
                }
                break;

            case 9: //Kill Count
                {
                    var type = msg.Read<byte>();
                    var questId = msg.Read<ushort>();

                    var quest = session.Client.Character.ActiveQuestList.FirstOrDefault(x => x.Id == questId)!;

                    switch (type)
                    {
                        case 1:
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    var killCount = msg.Read<ushort>();

                                    if(quest != null)
                                        quest.SetKillCount(i, killCount);

                                    Debug.WriteLine($"{questId} - Kill Count[{i}] : {killCount}");
                                }

                                if (quest != null && quest.IsKillCompleted())
                                    quest.Status = 3;                          
                            }
                            break;

                        case 2:
                            {
                                var killIndex = msg.Read<byte>();
                                var killCount = msg.Read<ushort>();

                                if (quest != null)
                                {
                                    quest.SetKillCount(killIndex - 1, killCount);

                                    if (quest.IsKillCompleted())
                                        quest.Status = 3;
                                }

                                Debug.WriteLine($"{questId} - Kill Count[{killIndex - 1}] : {killCount}");
                            }
                            break;
                    }
                }
                break;

            case 7: 
                {
                    var questId = msg.Read<int>();
                    var questNpcProtoId = msg.Read<ushort>();

                    session.Client.Character.ActiveQuestList.RemoveAll(x => x.Id == questId);
                }
                break;

            case 10:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var rewardItemId = msg.Read<int>();
                        var rewardItemCount = msg.Read<int>();

                        Debug.WriteLine($"Quest Reward - RewardItemId[{rewardItemId}] : RewardItemCount({rewardItemCount})");

                        _ = msg.Read<byte>();
                    }

                    _ = msg.Read<int>();
                    _ = msg.Read<int>();
                }
                break;
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_SELECT_MSG)]
    public Task MsgRecv_SelectMessage(Session session, Message msg)
    {
        _ = msg.Read<short>(); //Npc Proto Id

        var opcode = msg.Read<byte>();
        var questId = msg.Read<int>();


        if(opcode != 7) // TODO: 7 - Monster Stone
        {
            for (int i = 0; i < 10; i++)
                _ = msg.Read<int>(); //Menu Index

            _ = msg.Read<byte>(); //Accept

            session.Client.CharacterHandler.ProcessSelectMessage(opcode, questId);
        }

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_TRADE_NPC)]
    public Task MsgRecv_TradeNpc(Session session, Message msg)
    {
        var npcGroupId = msg.Read<int>();

        session.Client.Character.NpcEventGroup = npcGroupId;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_REPAIR_NPC)]
    public Task MsgRecv_RepairNpc(Session session, Message msg)
    {
        var npcGroupId = msg.Read<int>();

        session.Client.Character.NpcEventGroup = npcGroupId;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_ROTATE)]
    public Task MsgRecv_Rotation(Session session, Message msg)
    {
        var id = msg.Read<short>();
        var rotate = msg.Read<short>() / 100.0f;

        session.Client.Character.Rotation = rotate;

        return Task.CompletedTask;
    }

    [MessageHandler(MessageID.WIZ_MAP_EVENT)]
    public Task MsgRecv_MapEvent(Session session, Message msg)
    {
        byte opcode = msg.Read<byte>();

        switch (opcode)
        {
            case 9:
                {
                    session.Client.Character.LunarWarDressUp = msg.Read<bool>();
                }
                break;
        }

        return Task.CompletedTask;
    }

}





