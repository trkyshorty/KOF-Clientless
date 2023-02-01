using System.Numerics;
using System.Diagnostics;
using System.Text.Json;
using KOF.Core.Communications;
using KOF.Core.Enums;
using KOF.Core.Models;
using KOF.Data.Models;
using KOF.Data;
using KOF.Database;
using AStar;
using AStar.Options;
using KOF.Database.Models;

namespace KOF.Core.Handlers;

public class CharacterHandler : IDisposable
{
    private Client Client { get; set; } = default!;
    public Controller Controller { get; set; } = default!;
    public Character MySelf { get; set; } = new();
    public List<Character> NpcList { get; set; } = new();
    public List<Character> PlayerList { get; set; } = new();
    public Queue<RouteData> RouteQueue { get; set; } = new();
    public List<Loot> LootList { get; set; } = new();
    public Queue<Skill> SkillQueue { get; set; } = new();
    public long SkillQueueProcessTime { get; set; } = Environment.TickCount;
    public Queue<Skill> AttackQueue { get; set; } = new();
    public long AttackQueueNextProcessTime { get; set; } = 0;
    public long BasicAttackLastTime { get; set; } = Environment.TickCount;
    private bool MovingToLoot { get; set; } = false;
    public bool WarpAfterSyncRoute { get; set; } = true;
    public List<WarpInfo> WarpList { get; set; } = new();
    public List<Quest> QuestList { get; set; } = new();

    public CharacterHandler()
    {

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            MySelf = new();

            lock (NpcList)
                NpcList.Clear();

            lock (PlayerList)
                PlayerList.Clear();

            lock (AttackQueue)
                AttackQueue.Clear();

            lock (SkillQueue)
                SkillQueue.Clear();

            lock (RouteQueue)
                RouteQueue.Clear();

            lock (LootList)
                LootList.Clear();

            lock (WarpList)
                WarpList.Clear();

            lock (QuestList)
                QuestList.Clear();
        }
    }

    ~CharacterHandler()
    {
        Dispose(false);
    }

    public void InitializeSelectedTargetList()
    {
        var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

        MySelf.SelectedTargetList = TableHandler.GetMonsterList()
                .FindAll(x => GetNpcList().Any(y => y.ProtoId == x.Id) || selectedTargetIds.Contains(x.Id))
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();
    }

    public void InitializeSkillList()
    {
        MySelf.SkillList = TableHandler.GetSkillList().Select(x => x.Clone()).ToList();

        var selectedSkillList = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedSkillList", "[]"))!;

        MySelf.SelectedSkillList = MySelf.SkillList.FindAll(x => selectedSkillList.Contains(x.Id)).Select(x => x.Clone()).ToList();
    }

    public void InitializeController()
    {
        Controller = new Controller(Client.Account);
    }

    public void InitializeCharacterProcess()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                if (MySelf.GameState != GameState.GAME_STATE_INGAME)
                    return;

                await ProcessRouteQueue();
                await ProcessController();
                await ProcessMove();
                await ProcessTargetAndAction();
                await ProcessAttack();
                await ProcessAttackQueue();
                await ProcessSkillQueue();
                await ProcessAutoLoot();
                await ProcessQuest();

                await Task.Delay(1);
            }
        });

        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));

                if (MySelf.GameState != GameState.GAME_STATE_INGAME)
                    return;

                await Client.Session.SendAsync(MessageBuilder.MsgSend_SpeedCheck(Client.StartTime));
            }
        });
    }

    public bool IsRouting()
    {
        return RouteQueue.Count > 0;
    }

    public bool IsMovingToLoot()
    {
        return MovingToLoot;
    }

    public bool IsUntouchable()
    {
        return MySelf.UntouchableTime > Environment.TickCount;
    }

    public bool Connected()
    {
        if (Client?.Session == null)
            return false;

        return Client.Session.Connected;
    }

    public GameState GetGameState()
    {
        return MySelf.GameState;
    }
    
    public void ParseMySelf(Client client, Message msg)
    {
        Client = client;

        MySelf.Id = msg.Read<int>();
        MySelf.Name = msg.Read(false, "gb2312");

        MySelf.X = (msg.Read<ushort>() / 10.0f);
        MySelf.Y = (msg.Read<ushort>() / 10.0f);
        MySelf.Z = (msg.Read<ushort>() / 10.0f);

        MySelf.SetPosition(MySelf.GetPosition());

        MySelf.NationId = msg.Read<byte>();
        MySelf.Race = msg.Read<byte>();
        MySelf.Class = msg.Read<ushort>();
        MySelf.Face = msg.Read<byte>();
        MySelf.Hair = msg.Read<int>();
        MySelf.Rank = msg.Read<byte>();
        MySelf.Title = msg.Read<byte>();
        MySelf.IsHidingHelmet = msg.Read<byte>();
        MySelf.IsHidingCospre = msg.Read<byte>();
        MySelf.Level = msg.Read<byte>();
        MySelf.Points = msg.Read<short>();
        MySelf.MaxExperience = msg.Read<long>();
        MySelf.Experience = msg.Read<long>();
        MySelf.Loyalty = msg.Read<uint>();
        MySelf.LoyaltyMonthly = msg.Read<uint>();
        MySelf.Knight = msg.Read<short>();
        MySelf.Fame = msg.Read<byte>();

        if (MySelf.IsInClan())
        {
            MySelf.Knights.Alliance = msg.Read<ushort>();
            MySelf.Knights.Flag = msg.Read<byte>();
            MySelf.Knights.Name = msg.Read(false, "gb2312");
            MySelf.Knights.Grade = msg.Read<byte>();
            MySelf.Knights.Ranking = msg.Read<byte>();
            MySelf.Knights.MarkVersion = msg.Read<ushort>();

            if (MySelf.Knights.IsInAlliance())
            {
                if (MySelf.IsKing())
                {
                    msg.Read<ushort>();
                    msg.Read<uint>();
                }
                else
                {
                    MySelf.Knights.Cape = msg.Read<ushort>();
                    MySelf.Knights.CapeR = msg.Read<byte>();
                    MySelf.Knights.CapeG = msg.Read<byte>();
                    MySelf.Knights.CapeB = msg.Read<byte>();
                    msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(0));
                }
            }
            else
            {
                if (MySelf.IsKing())
                {
                    msg.Read<ushort>();
                    msg.Read<uint>();
                }
                else
                {
                    MySelf.Knights.Cape = msg.Read<ushort>();
                    MySelf.Knights.CapeR = msg.Read<byte>();
                    MySelf.Knights.CapeG = msg.Read<byte>();
                    MySelf.Knights.CapeB = msg.Read<byte>();
                    msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(0));
                    msg.Read<byte>(); // 02
                    msg.Read<byte>(); // 03
                    msg.Read<byte>(); // 04
                    msg.Read<byte>(); // 05
                }
            }
        }
        else
        {
            if (MySelf.IsKing())
            {
                msg.Read<ushort>(); //99
                msg.Read<uint>(); // 0
            }
            else
            {
                _ = msg.Read<ushort>();    // Knights.m_sCape 
                _ = msg.Read<byte>();      // Knights.m_bCapeR
                _ = msg.Read<byte>();      // Knights.m_bCapeG
                _ = msg.Read<byte>();      // Knights.m_bCapeB
                msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(9));
            }
            //TODO: remaining fix..
            // msg.SkipBytes(12); // 18 -> 22
            msg.Read<ulong>();
            msg.Read<uint>();
        }

        MySelf.MaxHp = msg.Read<ushort>();
        MySelf.Hp = msg.Read<short>();

        MySelf.MaxMp = msg.Read<ushort>();
        MySelf.Mp = msg.Read<short>();

        MySelf.MaxWeight = msg.Read<uint>() / 10;
        MySelf.Weight = msg.Read<uint>() / 10;

        MySelf.Stats[(int)StatType.STAT_STR - 1] = msg.Read<byte>();
        MySelf.StatsItemBonuses[(int)StatType.STAT_STR - 1] = msg.Read<byte>();
        MySelf.Stats[(int)StatType.STAT_HP - 1] = msg.Read<byte>();
        MySelf.StatsItemBonuses[(int)StatType.STAT_HP - 1] = msg.Read<byte>();
        MySelf.Stats[(int)StatType.STAT_DEX - 1] = msg.Read<byte>();
        MySelf.StatsItemBonuses[(int)StatType.STAT_DEX - 1] = msg.Read<byte>();
        MySelf.Stats[(int)StatType.STAT_INT - 1] = msg.Read<byte>();
        MySelf.StatsItemBonuses[(int)StatType.STAT_INT - 1] = msg.Read<byte>();
        MySelf.Stats[(int)StatType.STAT_MP - 1] = msg.Read<byte>();
        MySelf.StatsItemBonuses[(int)StatType.STAT_MP - 1] = msg.Read<byte>();

        MySelf.TotalHit = msg.Read<ushort>();
        MySelf.TotalAc = msg.Read<ushort>();

        MySelf.FireR = msg.Read<byte>();
        MySelf.ColdR = msg.Read<byte>();
        MySelf.LightningR = msg.Read<byte>();
        MySelf.MagicR = msg.Read<byte>();
        MySelf.DiseaseR = msg.Read<byte>();
        MySelf.PoisonR = msg.Read<byte>();

        MySelf.Gold = msg.Read<uint>();
        MySelf.Authority = msg.Read<byte>();
        MySelf.KnightsRank = msg.Read<byte>();
        MySelf.PersonalRank = msg.Read<byte>();

        for (int s = 0; s < 9; s++)
            MySelf.Skills[s] = msg.Read<byte>();

        for (int i = 0; i < Config.INVENTORY_TOTAL; i++)
        {
            Inventory pItem = new()
            {
                Pos = (byte)i,
                ItemID = msg.Read<uint>(),
                Durability = msg.Read<ushort>(),
                Count = msg.Read<ushort>(),
                Flag = msg.Read<byte>(),
                RentalTime = msg.Read<short>(),
                Serial = msg.Read<uint>(),
            };

            pItem.Table = TableHandler.GetItemById((int)pItem.ItemID);

            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().FirstOrDefault(x => x.ItemId == pItem.ItemID);

            if (supplyFlag != null)
                pItem.SupplyFlag = (byte)supplyFlag.Flag;

            if (pItem.Table != null && pItem.Table.KindId == 151) // 151 - Pet Item
            {
                var petName = msg.Read(true, "gb2312");     // pet name
                var petAttack = msg.Read<byte>();           // pet attack
                var petLevel = msg.Read<byte>();            // pet level
                var petExp = msg.Read<uint>();              // pet exp
                var petSatisFaction = msg.Read<short>();    // SatisFaction ?? xd
                var unknown1 = msg.Read<short>();           // unknown value1
                var unknown2 = msg.Read<byte>();            // unknown value2
            }
            else if (pItem.ItemID == Config.ITEM_CYPHER_RING)
            {
                //TODO: Cypher Ring
            }
            else
                pItem.ExpirationTime = msg.Read<uint>();

            MySelf.Inventory.SetValue(pItem, i);
        }

        MySelf.AccountStatus = msg.Read<byte>();

        int m_bPremiumCount = msg.Read<byte>();
        for (int pre = 0; pre < m_bPremiumCount; pre++)
        {
            msg.Read<byte>();
            msg.Read<ushort>();
        }

        MySelf.PremiumInUse = msg.Read<byte>();
        MySelf.IsChicken = msg.Read<byte>();
        MySelf.MannerPoint = msg.Read<uint>();

        MySelf.KarusBaseMilitaryCampCount = msg.Read<byte>();
        MySelf.ElmoradBaseMilitaryCampCount = msg.Read<byte>();
        MySelf.KarusEslantMilitaryCampCount = msg.Read<byte>();
        MySelf.ElmoradEslantMilitaryCampCount = msg.Read<byte>();
        MySelf.MoradonMilitaryCampCount = msg.Read<byte>();

        MySelf.GenieStatus = msg.Read<byte>();

       /* _ = msg.Read<uint>();
        _ = msg.Read<uint>();
        _ = msg.Read<long>();
        _ = msg.Read<long>();
        _ = msg.Read<ushort>(); // cover title
        _ = msg.Read<ushort>(); // skill title
        _ = msg.Read<byte>(); // ??*/

        MySelf.MoveType = 3;
        MySelf.GameState = GameState.GAME_STATE_INGAME;

    }

    public Character ParseOther(Message msg, bool isGroup)
    {
        var character = new Character();

        if (isGroup)
        {
            _ = msg.Read<byte>();
        }

        character.Id = msg.Read<int>();

        character.Name = msg.Read(false, "gb2312");
        character.NationId = msg.Read<byte>();
        _ = msg.Read<byte>();
        _ = msg.Read<byte>();
        character.Knight = msg.Read<short>();

        if (character.IsInClan())
        {
            character.Knights = new();
            character.Knights.Alliance = msg.Read<ushort>();
            character.Knights.Flag = msg.Read<byte>();
            character.Knights.Name = msg.Read(false, "gb2312");
            character.Knights.Grade = msg.Read<byte>();
            character.Knights.Ranking = msg.Read<byte>();
            character.Knights.MarkVersion = msg.Read<ushort>();

            if (character.Knights.IsInAlliance())
            {
                if (character.Rank == 1)
                {
                    msg.Read<ushort>();
                    msg.Read<uint>();
                }
                else
                {
                    character.Knights.Cape = msg.Read<ushort>();
                    character.Knights.CapeR = msg.Read<byte>();
                    character.Knights.CapeG = msg.Read<byte>();
                    character.Knights.CapeB = msg.Read<byte>();
                    msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(0));
                    msg.Read<byte>();
                }
            }
            else
            {
                if (character.Rank == 1)
                {
                    msg.Read<ushort>();
                    msg.Read<uint>();
                }
                else
                {
                    character.Knights.Cape = msg.Read<ushort>();
                    character.Knights.CapeR = msg.Read<byte>();
                    character.Knights.CapeG = msg.Read<byte>();
                    character.Knights.CapeB = msg.Read<byte>();
                    msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(0));
                    msg.Read<byte>(); // 02
                    msg.Read<byte>(); // 03
                    msg.Read<byte>(); // 04
                    msg.Read<byte>(); // 05
                }
            }

        }
        else
        {
            if (character.Rank == 1)
            {
                msg.Read<ushort>(); //99
                msg.Read<uint>(); // 0
            }
            else
            {
                _ = msg.Read<ushort>();    // Knights.m_sCape 
                _ = msg.Read<byte>();      // Knights.m_bCapeR
                _ = msg.Read<byte>();      // Knights.m_bCapeG
                _ = msg.Read<byte>();      // Knights.m_bCapeB
                _ = msg.Read<byte>(); // << ((pKnights->m_byFlag > 1 && pKnights->m_byGrade < 3) ? uint8(1) : uint8(9));
            }
            //TODO: klanlı ve klansız bilgileri doğrula
            // msg.SkipBytes(12); // 18 -> 22
            msg.Read<uint>();
            msg.Read<uint>();
            msg.Read<byte>();
        }

        character.Level = msg.Read<byte>();
        character.Race = msg.Read<byte>();
        character.Class = msg.Read<ushort>();

        character.X = (msg.Read<short>() / 10.0f);
        character.Y = (msg.Read<short>() / 10.0f);
        character.Z = (msg.Read<short>() / 10.0f);

        character.Face = msg.Read<byte>();
        character.Hair = msg.Read<int>(); // hair;R;G;B

        character.Status = msg.Read<byte>();
        character.StatusSize = msg.Read<uint>();
        character.NeedParty = msg.Read<byte>();
        character.Authority = msg.Read<byte>();
        character.PartyLeader = msg.Read<byte>();
        character.InvisibilityType = msg.Read<byte>();
        character.TeamColor = msg.Read<byte>();
        character.IsHidingCospre = msg.Read<byte>();
        character.IsHidingHelmet = msg.Read<byte>();

        character.Rotation = msg.Read<short>();
        character.IsChicken = msg.Read<byte>();
        character.Rank = msg.Read<byte>();

        _ = msg.Read<byte>();
        _ = msg.Read<byte>();

        character.KnightsRank = msg.Read<byte>();
        character.PersonalRank = msg.Read<byte>();

        var loop = MySelf.LunarWarDressUp ? 9 : 15;

        _ = msg.Read<byte>();

        for (byte idx = 0; idx < loop; idx++)
        {
            Inventory pItem = new()
            {
                Pos = idx,
                ItemID = msg.Read<uint>(),
                Durability = msg.Read<ushort>(),
                Flag = msg.Read<byte>()
            };

            character.VisibleEquip.SetValue(pItem, idx);
        }

        character.Zone = msg.Read<byte>();
        character.Zone = (byte)Character.GetRepresentZone(character.Zone);

        character.SetPosition(character.GetPosition());

        byte[] tmp = new byte[16]; //TODO : eksikleri tamamla
        msg.Read(tmp.AsSpan());

        return character;
    }

    public Character ParseNpc(Message msg)
    {
        var character = new Character();

        character.Id = msg.Read<int>();
        character.ProtoId = msg.Read<ushort>();
        character.MonsterOrNpc = msg.Read<byte>();
        character.PictureId = msg.Read<ushort>();
        character.Unknown1 = msg.Read<uint>();
        character.FamilyType = msg.Read<byte>();
        character.SellingGroup = msg.Read<uint>();
        character.ModelSize = msg.Read<ushort>();
        character.Weapon1 = msg.Read<uint>();
        character.Weapon2 = msg.Read<uint>();

        if (character.ProtoId == 0)
        {
            var petOwnerName = msg.Read(false, "gb2312"); // own Name
            var petName = msg.Read(false, "gb2312"); // pet Name
        }

        character.ModelGroup = msg.Read<byte>();
        character.Level = msg.Read<byte>();

        character.X = (msg.Read<ushort>() / 10.0f);
        character.Y = (msg.Read<ushort>() / 10.0f);
        character.Z = (msg.Read<ushort>() / 10.0f);

        character.Zone = MySelf.Zone;
        character.Zone = (byte)Character.GetRepresentZone(character.Zone);

        character.SetPosition(character.GetPosition());

        character.Status = (byte)msg.Read<uint>();
        _ = msg.Read<byte>();
        _ = msg.Read<uint>();
        character.Rotation = msg.Read<short>();

        if (character.MonsterOrNpc == 1)
        {
            character.Monster = TableHandler.GetMonsterList().SingleOrDefault(x => x.Id == character.ProtoId)!;

            if (character.Monster != null)
                character.Name = character.Monster.Name;
        }
        else
        {
            character.Npc = TableHandler.GetNpcList().SingleOrDefault(x => x.Id == character.ProtoId)!;

            if (character.Npc != null)
                character.Name = character.Npc.Name;
        }

        return character;
    }

    public List<Character> GetNpcList()
    {
        lock (NpcList)
        {
            return new List<Character>(NpcList);
        }
    }

    public List<Character> GetPlayerList()
    {
        lock (PlayerList)
        {
            return new List<Character>(PlayerList);
        }
    }

    public void SelectTarget(int targetId)
    {
        if (MySelf.TargetId == targetId)
            return;

        MySelf.TargetId = targetId;

        if (MySelf.TargetId != -1)
            UpdateTargetHp(MySelf.TargetId, 1);
    }

    public Character GetTarget(int targetId = -1)
    {
        if (targetId == -1)
            targetId = MySelf.GetTargetId();

        if (targetId >= Config.NPC_BAND)
            return GetNpcList().FirstOrDefault(x => x.Id == targetId)!;
        else
            return GetPlayerList().FirstOrDefault(x => x.Id == targetId)!;
    }

    public void UpdateTargetHp(int targetId, byte init = 0)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_TargetHealthRequest(targetId, init)).ConfigureAwait(false);
        MySelf.TargetHpUpdateTime = Stopwatch.GetTimestamp();
    }

    public void Attack()
    {
        if (IsRouting())
            return;

        var target = GetTarget();

        if (target == null || target.IsDead())
            return;

        var rand = new Random();

        var skill = MySelf.SelectedSkillList
            .FirstOrDefault(x =>
            {
                if (x == null)
                    return false;

                if (x.SelfPart1 == -1)
                    return false;

                if (AttackQueue.Any(y => y != null && y.Id == x.Id))
                    return false;

                if (Environment.TickCount - x.GetSkillUseTime() < (x.CoolDown) || MySelf.Mp <= x.Mana)
                    return false;

                return true;
            });
        //.Skip(rand.Next(0, MySelf.SelectedSkillList.Count()))
        //.Take(1)
        //.FirstOrDefault();

        if (skill == null)
            return;

        AttackQueue.Enqueue(skill);
    }

    public void SelfProtection()
    {
        var rand = new Random();

        var skill = MySelf.SelectedSkillList
            .FirstOrDefault(x =>
            {
                if (x == null)
                    return false;

                if (x.SelfPart1 != -1)
                    return false;

                if (SkillQueue.Any(y => y != null && y.Id == x.Id))
                    return false;

                if (Environment.TickCount - x.GetSkillUseTime() < (x.CoolDown) || MySelf.Mp <= x.Mana)
                    return false;

                if (SkillBuffEffected((byte)x.Extension.BuffType))
                    return false;

                return true;
            });
        //.Skip(rand.Next(0, MySelf.SelectedSkillList.Count()))
        //.Take(1)
        //.FirstOrDefault();

        if (skill != null)
            SkillQueue.Enqueue(skill);
    }
    private Task ProcessAttack()
    {
        try
        {
            if (Controller == null) return Task.CompletedTask;
            if (!Controller.GetControl("Attack", false)) return Task.CompletedTask;
            if (GetGameState() != GameState.GAME_STATE_INGAME || IsUntouchable() || IsRouting()) return Task.CompletedTask;
            if (MySelf.IsTrading) return Task.CompletedTask;

            var attackRange = Controller.GetControl("AttackRange", 45);

            if (MySelf.GetTargetId() != -1)
            {
                var target = GetNpcList().FirstOrDefault(x => x?.Id == MySelf.GetTargetId());

                if (target != null)
                {
                    if (target.IsDead() || Vector3.Distance(MySelf.GetPosition(), target.GetPosition()) >= (float)attackRange)
                        return Task.CompletedTask;

                    Attack();
                }
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }

        return Task.CompletedTask;
    }

    private Task ProcessTargetAndAction()
    {
        try
        {
            if (Controller == null) return Task.CompletedTask;
            if (!Controller.GetControl("Attack", false)) return Task.CompletedTask;
            if (GetGameState() != GameState.GAME_STATE_INGAME || IsRouting()) return Task.CompletedTask;
            if (MySelf.IsTrading) return Task.CompletedTask;

            //bool test = false;

            //if (test)
            //{
            //    CharacterHandler.SelectTarget(20573); // KUKLA TEST
            //    return;
            //}

            var selectedTargetIds = JsonSerializer.Deserialize<List<int>>(Controller.GetControl("SelectedTargetList", "[]"))!;

            var followedClient = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME && x.Character.Name == Controller.GetControl("Follow", ""))!;

            if (Controller.GetControl("FollowTargetSync", true)
                && followedClient != null
                && !followedClient.CharacterHandler.IsRouting()
                && !followedClient.Character.IsTrading
                && GetGameState() == GameState.GAME_STATE_INGAME)
            {
                if (followedClient.Character.GetTargetId() != MySelf.GetTargetId()
                    && Vector3.Distance(followedClient.Character.GetPosition(), MySelf.GetPosition()) <= 150)
                    SelectTarget(followedClient.Character.GetTargetId());
            }
            else
            {
                var targetSearchRange = Controller.GetControl("TargetSearchRange", 45);

                if (MySelf.GetTargetId() != -1)
                {
                    var target = GetNpcList().FirstOrDefault(x => x?.Id == MySelf.GetTargetId());

                    if (target != null)
                    {
                        if (target.IsDead() || Vector3.Distance(MySelf.GetPosition(), target.GetPosition()) >= (float)targetSearchRange)
                        {
                           SelectTarget(-1);
                        }
                        else
                        {
                            if (!IsRouting() && !IsMovingToLoot() && Controller.GetControl("MoveToTarget", true) && target.GetPosition() != MySelf.GetPosition())
                                MySelf.SetMovePosition(target.GetPosition());
                        }
                    }
                    else
                        SelectTarget(-1);
                }
                else
                {
                    Character target = default!;

                    if (selectedTargetIds.Count() > 0)
                    {
                        target = GetNpcList()
                           .FindAll(x => !x.IsDead() && selectedTargetIds.Contains(x.ProtoId) && x.MonsterOrNpc == 1 && Vector3.Distance(x.GetPosition(), MySelf.GetPosition()) < (float)targetSearchRange)
                           .OrderBy(x => Vector3.Distance(MySelf.GetPosition(), x.GetPosition()))
                           ?.FirstOrDefault()!;
                    }
                    else
                    {
                        target = GetNpcList()
                           .FindAll(x => !x.IsDead() && x.MonsterOrNpc == 1 && Vector3.Distance(x.GetPosition(), MySelf.GetPosition()) < (float)targetSearchRange)
                           .OrderBy(x => Vector3.Distance(MySelf.GetPosition(), x.GetPosition()))
                           ?.FirstOrDefault()!;
                    }

                    if (target != null)
                        SelectTarget(target.Id);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }

        return Task.CompletedTask;
    }

    private async Task ProcessAttackQueue()
    {
        if (AttackQueue.Count == 0 || MySelf.GameState != GameState.GAME_STATE_INGAME)
            return;

        if (AttackQueueNextProcessTime >= Environment.TickCount)
            return;

        var skill = AttackQueue.Dequeue();

        var target = GetTarget();

        if (target != null && !target.IsDead())
        {
            if (skill.Type1 == 1 || skill.Type1 == 3)
            {
                float distance = Vector3.Distance(MySelf.GetPosition(), target.GetPosition());

                if (distance <= 2.0f && Environment.TickCount - BasicAttackLastTime > 1100)
                {
                    await Client.Session.SendAsync(MessageBuilder.MsgSend_Attack(MySelf.TargetId, 1.11f, 1.0f));
                    BasicAttackLastTime = Environment.TickCount;
                }
            }

            await UseSkill(skill, target);
        }

        AttackQueueNextProcessTime = Environment.TickCount + (int)Controller.GetControl("AttackSpeed", 1000);
    }

    private async Task ProcessSkillQueue()
    {
        if (SkillQueue.Count == 0 || MySelf.GameState != GameState.GAME_STATE_INGAME)
            return;

        if (SkillQueueProcessTime > Environment.TickCount)
            return;

        var skill = SkillQueue.Dequeue();

        var target = GetTarget(skill.GetTarget());

        if (target != null)
        {
            if (!target.IsDead())
                await UseSkill(skill, target);
        }
        else
            await UseSkill(skill, MySelf);

        SkillQueueProcessTime = Environment.TickCount + (skill.CastTime * 100);

    }

    public void CancelSkill(Skill skill)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_CancelSkillPacket(skill, MySelf.Id)).ConfigureAwait(false);
    }

    private async Task UseSkill(Skill skill, Character target)
    {
        if (target == null || target.IsDead()) return;

        switch (skill.TargetType)
        {
            case (int)SkillMagicTargetType.TARGET_SELF:
            case (int)SkillMagicTargetType.TARGET_FRIEND_WITHME:
            case (int)SkillMagicTargetType.TARGET_FRIEND_ONLY:
            case (int)SkillMagicTargetType.TARGET_PARTY:
            case (int)SkillMagicTargetType.TARGET_NPC_ONLY:
            case (int)SkillMagicTargetType.TARGET_ENEMY_ONLY:
            case (int)SkillMagicTargetType.TARGET_ALL:
            case (int)SkillMagicTargetType.TARGET_AREA_FRIEND:
            case (int)SkillMagicTargetType.TARGET_AREA_ALL:
            case (int)SkillMagicTargetType.TARGET_DEAD_FRIEND_ONLY:
                {
                    await Task.Run(async () =>
                    {
                        // Super Archer
                        if(skill.Id == 999897)
                        {
                            var arrowShower = MySelf.SkillList.First(
                                x => x.Name == "arrow shower" && 
                                x.ClassBaseId >= 100 && 
                                x.ClassBaseId.ToString().Substring(0, 3) == MySelf.Class.ToString());

                            if (arrowShower != null)
                                await UseSkill(arrowShower, target);

                            await Task.Delay(300);

                            var multipleShot = MySelf.SkillList.First(
                                x => x.Name == "multiple shot" && 
                                x.ClassBaseId >= 100 && 
                                x.ClassBaseId.ToString().Substring(0, 3) == MySelf.Class.ToString());

                            if (multipleShot != null)
                                await UseSkill(multipleShot, target);
                        } 
                        else
                        {
                            if (skill.CastTime != 0)
                            {
                                if (MySelf.IsMoving())
                                    SendMove(MySelf.GetPosition(), MySelf.GetPosition(), 0, 0);

                                await Client.Session.SendAsync(MessageBuilder.MsgSend_StartSkillCastingAtTargetPacket(skill, MySelf.Id, target.Id));

                                if(skill.Extension.Number != 2)
                                    await Task.Delay(skill.CastTime * 100);
                            }

                            if ((skill.Extension.ArrowCount == 0 && skill.RequiredFlyEffect != 0) || skill.Extension.ArrowCount == 1)
                                await Client.Session.SendAsync(MessageBuilder.MsgSend_StartFlyingAtTarget(skill, MySelf.Id, target.Id, target.GetPosition()));

                            if (skill.Extension.ArrowCount > 1)
                            {
                                await Client.Session.SendAsync(MessageBuilder.MsgSend_StartFlyingAtTarget(skill, MySelf.Id, target.Id, MySelf.GetPosition(), 1));

                                float distance = Vector3.Distance(MySelf.GetPosition(), target.GetPosition());

                                int arrowCount = 1;

                                if(distance <= 1.0f)
                                    arrowCount = 5;
                                else if (distance <= 2.0f)
                                    arrowCount = 4;
                                else if (distance <= 3.0f)
                                    arrowCount = 3;
                                else if (distance < 16.0f)
                                    arrowCount = 2;

                                for (ushort i = 0; i < arrowCount; i++)
                                {
                                    await Client.Session.SendAsync(MessageBuilder.MsgSend_StartSkillMagicAtTargetPacket(skill, MySelf.Id, target.Id, MySelf.GetPosition(), (ushort)(i + 1)));
                                    await Client.Session.SendAsync(MessageBuilder.MsgSend_StartMagicAtTarget(skill, MySelf.Id, target.Id, MySelf.GetPosition(), (ushort)(i + 1)));
                                }
                            }
                            else
                                await Client.Session.SendAsync(MessageBuilder.MsgSend_StartSkillMagicAtTargetPacket(skill, MySelf.Id, target.Id, target.GetPosition()));
                        }
                    });
                }
                break;

            case (int)SkillMagicTargetType.TARGET_AREA:
            case (int)SkillMagicTargetType.TARGET_PARTY_ALL:
            case (int)SkillMagicTargetType.TARGET_AREA_ENEMY:
                {
                    await Task.Run(async () =>
                    {
                        if (skill.CastTime != 0)
                        {
                            if (MySelf.IsMoving())
                                SendMove(MySelf.GetPosition(), MySelf.GetPosition(), 0, 0);

                            await Client.Session.SendAsync(MessageBuilder.MsgSend_StartSkillCastingAtPosPacket(skill, MySelf.Id, MySelf.GetPosition()));

                            if (skill.Extension.Number != 2)
                                await Task.Delay(skill.CastTime * 100);
                        }

                        if (skill.RequiredFlyEffect != 0 || skill.Extension.ArrowCount == 1)
                            await Client.Session.SendAsync(MessageBuilder.MsgSend_StartFlyingAtTarget(skill, MySelf.Id, -1, target.GetPosition()));

                        await Client.Session.SendAsync(MessageBuilder.MsgSend_StartSkillMagicAtPosPacket(skill, MySelf.Id, target.GetPosition()));
                    });
                }
                break;
        }

        skill.UpdateSkillUseTime(Environment.TickCount);
    }

    public void SkillCastingProcess(int skillId, int sourceId, int targetId)
    {
        var skill = MySelf.SkillList.FirstOrDefault(x => x.Id == skillId)!;

        if (skill == null) return;
    }

    public void SkillFlyingProcess(int skillId, int sourceId, int targetId)
    {
        var skill = MySelf.SkillList.FirstOrDefault(x => x.Id == skillId)!;

        if (skill == null) return;
    }

    public void SkillEffectingProcess(int skillId, int sourceId, int targetId)
    {
        var skill = MySelf.SkillList.FirstOrDefault(x => x.Id == skillId)!;

        if (skill == null) return;

        if (MySelf.Id == sourceId || MySelf.Id == targetId)
        {
            if (skill.Type1 == 4)
            {
                Skill? outSkill;
                if (!MySelf.BuffList.TryGetValue((byte)skill.Extension.BuffType, out outSkill))
                    MySelf.BuffList.Add((byte)skill.Extension.BuffType, skill);

                switch (skill.BaseId)
                {
                    case 101001:
                    case 107010:
                        MySelf.Speed = 67;
                        break;

                    case 107725:
                        MySelf.Speed = 90;
                        break;
                }
            }
        }

        var character = PlayerList.FirstOrDefault(x => x.Id == targetId && x.Id != MySelf.Id)!;

        if (character != null)
        {
            if (skill.Type1 == 4 && skill.Extension.BuffType == (int)BuffType.BUFF_TYPE_SPEED)
            {
                switch (skill.BaseId)
                {
                    case 101001:
                    case 107010:
                        character.Speed = 67;
                        break;

                    case 107725:
                        character.Speed = 90;
                        break;
                }
            }
        }
    }

    public void InitializeMoveSpeed()
    {
        Skill? skill;
        if (MySelf.BuffList.TryGetValue((byte)BuffType.BUFF_TYPE_SPEED, out skill))
        {
            switch (skill.BaseId)
            {
                case 101001: //Sprint
                case 107010: //Swift
                    MySelf.Speed = 67;
                    break;

                case 107725: //Light Feet
                    MySelf.Speed = 90;
                    break;
            }
        }
        else
            MySelf.Speed = 45;
    }

    public void SkillFailedProcess(int skillId, int sourceId, int targetId)
    {
        if (MySelf.Id == sourceId)
        {
            var skill = MySelf.SelectedSkillList.FirstOrDefault(x => x.Id == skillId)!;

            //if (skill != null)
            //skill.UpdateSkillUseTime(0);
        }

        Debug.WriteLine($"SkillFailedProcess: skillId({skillId}), sourceId({sourceId}), targetId({targetId})");
    }

    public void SkillBuffProcess(byte buffType)
    {
        MySelf.BuffList.Remove(buffType);
    }

    public bool SkillBuffEffected(byte buffType)
    {
        return MySelf.BuffList.ContainsKey(buffType);
    }

    public void Town(bool afterSyncRoute = false)
    {
        WarpAfterSyncRoute = afterSyncRoute;
        Client.Session.SendAsync(MessageBuilder.MsgSend_Home()).ConfigureAwait(false);
    }

    public void Regen()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_Regen()).ConfigureAwait(false);
    }

    public void RemoveItem(byte slotType, byte pos, uint itemId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ItemRemove(slotType, pos, itemId)).ConfigureAwait(false);
        Client.Session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);
    }

    public void UseItem(uint itemId)
    {
        var item = TableHandler.GetItemById((int)itemId);

        if (item != null)
        {
            var skill = MySelf.SkillList.FirstOrDefault(x => x.Id == (uint)item.Effect1
                    && !SkillQueue.Any(y => y.Id == x.Id)
                    && Environment.TickCount - x.GetSkillUseTime() > (x.CoolDown));

            if (skill != null)
                SkillQueue.Enqueue(skill);
        }
    }

    public int GetArmDestinationIndex(Item item)
    {
        switch ((ItemAttachPos)item.AttachPoint)
        {
            case ItemAttachPos.ITEM_ATTACH_POS_DUAL:
                {
                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT].ItemID != 0 && MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT].ItemID != 0)
                        return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT;

                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT].ItemID == 0)
                        return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT;
                    else
                    {
                        if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT].Table.AttachPoint == (int)ItemAttachPos.ITEM_ATTACH_POS_TWOHAND_RIGHT)
                            return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT;
                        else
                            return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT;
                    }
                }

            case ItemAttachPos.ITEM_ATTACH_POS_HAND_RIGHT:
                return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT;

            case ItemAttachPos.ITEM_ATTACH_POS_HAND_LEFT:
                return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT;

            case ItemAttachPos.ITEM_ATTACH_POS_TWOHAND_RIGHT:
                {
                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT].ItemID != 0 && MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT].ItemID != 0)
                        return -1;
                    else
                        return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT;
                }

            case ItemAttachPos.ITEM_ATTACH_POS_TWOHAND_LEFT:
                {
                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_RIGHT].ItemID != 0 && MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT].ItemID != 0)
                        return -1;
                    else
                        return (int)ItemSlotPos.ITEM_SLOT_POS_HAND_LEFT;
                }

            case ItemAttachPos.ITEM_ATTACH_POS_EAR:
                {
                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_EAR_RIGHT].ItemID == 0)
                        return (int)ItemSlotPos.ITEM_SLOT_POS_EAR_RIGHT;

                    if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_EAR_LEFT].ItemID == 0)
                        return (int)ItemSlotPos.ITEM_SLOT_POS_EAR_LEFT;

                    return (int)ItemSlotPos.ITEM_SLOT_POS_EAR_RIGHT;
                }

            case ItemAttachPos.ITEM_ATTACH_POS_HEAD:
                return (int)ItemSlotPos.ITEM_SLOT_POS_HEAD;

            case ItemAttachPos.ITEM_ATTACH_POS_NECK:
                return (int)ItemSlotPos.ITEM_SLOT_POS_NECK;

            case ItemAttachPos.ITEM_ATTACH_POS_UPPER:
                return (int)ItemSlotPos.ITEM_SLOT_POS_UPPER;

            case ItemAttachPos.ITEM_ATTACH_POS_CLOAK:
                return (int)ItemSlotPos.ITEM_SLOT_POS_SHOULDER;

            case ItemAttachPos.ITEM_ATTACH_POS_BELT:
                return (int)ItemSlotPos.ITEM_SLOT_POS_BELT;

            case ItemAttachPos.ITEM_ATTACH_POS_FINGER:
                if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_RING_RIGHT].ItemID == 0)
                    return (int)ItemSlotPos.ITEM_SLOT_POS_RING_RIGHT;

                if (MySelf.Inventory[(int)ItemSlotPos.ITEM_SLOT_POS_RING_LEFT].ItemID == 0)
                    return (int)ItemSlotPos.ITEM_SLOT_POS_RING_LEFT;

                return (int)ItemSlotPos.ITEM_SLOT_POS_RING_RIGHT;

            case ItemAttachPos.ITEM_ATTACH_POS_LOWER:
                return (int)ItemSlotPos.ITEM_SLOT_POS_LOWER;

            case ItemAttachPos.ITEM_ATTACH_POS_ARM:
                return (int)ItemSlotPos.ITEM_SLOT_POS_GLOVES;

            case ItemAttachPos.ITEM_ATTACH_POS_FOOT:
                return (int)ItemSlotPos.ITEM_SLOT_POS_SHOES;
        }

        return -1;
    }

    public void EquipItem(uint itemId, byte currentPosition)
    {
        var item = TableHandler.GetItemById((int)itemId);

        if (item != null)
        {
            var targetPosition = GetArmDestinationIndex(item);

            if (targetPosition == -1)
                return;

            MySelf.Inventory[targetPosition].Reset();

            MySelf.Inventory[currentPosition].CopyTo(MySelf.Inventory[targetPosition]);
            MySelf.Inventory[currentPosition].Reset();

            Client.Session.SendAsync(MessageBuilder.MsgSend_InventoryItemMoveProcess(1, 1, itemId, (byte)(currentPosition - 14), (byte)targetPosition)).ConfigureAwait(false);
            Client.Session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);
        }
    }

    public void EquipOreads(uint itemId, byte currentPosition)
    {
        var item = TableHandler.GetItemById((int)itemId);

        if (item != null)
        {
            Client.Session.SendAsync(MessageBuilder.MsgSend_InventoryItemMoveProcess(1, 3, itemId, (byte)(currentPosition - 14), (byte)35)).ConfigureAwait(false);
            Client.Session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);
        }
    }

    //Client.Session.SendAsync(MessageBuilder.MsgSend_InventoryItemMoveProcess(1, 3, itemId, (byte)0, (byte)35)).ConfigureAwait(false);
    //Client.Session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte) ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);

    public void AbilityPointChange(byte type, short point)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_AbilityPointChange(type, point)).ConfigureAwait(false);
    }

    public void SkillPointChange(byte type)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_SkillPointChange(type)).ConfigureAwait(false);
    }

    public void PartySend(string Name)
    {
        if (!MySelf.Party.IsInParty())
            _ = Client.Session.SendAsync(MessageBuilder.MsgSend_PartyCreate(Name)).ConfigureAwait(false);
        else
            _ = Client.Session.SendAsync(MessageBuilder.MsgSend_PartyInsert(Name)).ConfigureAwait(false);
    }

    public void PartySend(Character target)
    {
        if (target.Party.IsInParty()) return;
        if (MySelf.Zone != target.Zone) return;

        if (!((MySelf.Level <= (int)(target.Level * 1.5) && MySelf.Level >= (target.Level * 2 / 3))
            || (MySelf.Level <= (target.Level + 8) && MySelf.Level >= ((target.Level) - 8))))
            return;

        if (!MySelf.Party.IsInParty())
            _ = Client.Session.SendAsync(MessageBuilder.MsgSend_PartyCreate(target.Name)).ConfigureAwait(false);
        else
            _ = Client.Session.SendAsync(MessageBuilder.MsgSend_PartyInsert(target.Name)).ConfigureAwait(false);
    }

    public void PartyAccept()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_PartyAccept(true)).ConfigureAwait(false);
    }

    public void PartyDestroy()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_PartyDestroy()).ConfigureAwait(false);
    }

    public void PartyRemove(int memberId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_PartyRemove(memberId)).ConfigureAwait(false);
    }
    public void PartyPromoteLeader(int memberId)
    {
        if (!MySelf.Party.IsInParty()) return;
        if (MySelf.Party.Leader.Name != MySelf.Name) return;

        Client.Session.SendAsync(MessageBuilder.MsgSend_PartyPromoteLeader(memberId)).ConfigureAwait(false);
    }

    public void ItemBundleDrop(int npcId, uint bundleId, byte itemCount)
    {
        if (Controller != null)
        {
            if (!Controller.GetControl("EnableLoot", true))
                return;
        }

        var loot = new Loot();

        var character = NpcList.FirstOrDefault(x => x.Id == npcId);

        if (character == null) return;

        loot.BundleId = bundleId;
        loot.NpcId = npcId;

        loot.Position = character.GetPosition();

        loot.DropTime = Environment.TickCount;
        loot.ItemCount = itemCount;

        LootList.Add(loot);
    }

    public void ItemBundleOpen(uint bundleId, uint itemId, short index)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_RequestItemBundleGet((int)bundleId, (int)itemId, index)).ConfigureAwait(false);
    }

    public void ItemBuy(int npcId, int itemSellingGroup, int ItemId, short itemCount)
    {
        var itemExist = MySelf.Inventory.FirstOrDefault(x => x.ItemID == ItemId);

        int slot = -1;

        int fixedItemCount = itemCount;

        if (itemExist != null)
        {
            slot = itemExist.Pos;
            fixedItemCount = Math.Abs(itemExist.Count - fixedItemCount);
        }
        else
        {
            var findEmptyPosition = MySelf.Inventory.FirstOrDefault(y => y.Pos >= 14 && y.ItemID == 0);

            if (findEmptyPosition != null)
                slot = findEmptyPosition.Pos;
        }

        if (slot == -1) return;

        var shopItemList = TableHandler.GetItemSellList().FindAll(x => x.SellingGroup == itemSellingGroup);

        if (shopItemList == null) return;

        Dictionary<int, List<int>> shopItemListDict = new();

        byte shopPage = 0;

        shopItemList.ForEach(x =>
        {
            shopItemListDict.Add(shopPage++, x.ItemList);
        });

        foreach (KeyValuePair<int, List<int>> shopItem in shopItemListDict)
        {
            if (!shopItem.Value.Any(x => x == ItemId)) continue;

            var shopItemPosition = shopItem.Value.FindIndex(x => x == ItemId);

            Client.Session.SendAsync(MessageBuilder.MsgSend_ItemTradeBuy(
                npcId,
                (uint)itemSellingGroup,
                (uint)ItemId,
                (byte)(slot - 14),
                (short)fixedItemCount,
                (byte)shopItem.Key,
                (byte)shopItemPosition
                )).ConfigureAwait(false);
        }
    }

    public void ItemSell(int npcId, int npcGroup, int ItemId, short itemCount)
    {
        var item = MySelf.Inventory.FirstOrDefault(x => x.ItemID == ItemId)!;

        if (item.ItemID == 0 || item.Pos < 14) return;

        Client.Session.SendAsync(MessageBuilder.MsgSend_ItemTradeSell(
            npcId,
            (uint)npcGroup,
            (uint)ItemId,
            (byte)(item.Pos - 14),
            (short)item.Count
            )).ConfigureAwait(false);
    }

    public void ItemRepair(byte direction, int npcId, Inventory item)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ItemRepairRequest(
            direction,
            item.Pos,
            npcId,
            item.ItemID
            )).ConfigureAwait(false);

        item.Durability = (ushort)item.MaxDurability!;
    }

    public void Route(List<RouteData> routeData)
    {
        var firstPosition = routeData.FirstOrDefault()!;

        if (firstPosition == null) return;

        if (firstPosition.Action != RouteActionType.TOWN)
        {
            var movePath = GenerateMovePath(MySelf.Zone, MySelf.GetPosition(), new Vector3(firstPosition.X, firstPosition.Y, firstPosition.Z));

            if (movePath.Count() > 0)
            {
                movePath.Reverse();

                movePath.ForEach(x => routeData.Insert(0, new RouteData() { Action = RouteActionType.MOVE, X = x.X, Y = x.Y, Z = x.Z }));
            }
        }

        RouteQueue = new Queue<RouteData>(routeData);
    }

    private async Task ProcessRouteQueue()
    {
        if (RouteQueue.Count == 0)
            return;

        if (MySelf.GameState != GameState.GAME_STATE_INGAME)
            return;

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == MySelf.Zone)!;

        if (zoneData == null)
            return;

        var route = RouteQueue.Peek();

        var movePosition = new Vector3((float)Math.Round(route.X, 1), (float)Math.Round(route.Y, 1), 0.0f);

        movePosition.Z = (float)Math.Round(zoneData.GetHeightBy2DPos(movePosition.X, movePosition.Y), 1);

        switch (route.Action)
        {
            case RouteActionType.MOVE:
                {
                    if (MySelf.GetPosition() != movePosition)
                        MySelf.SetMovePosition(movePosition);
                    else
                        RouteQueue.Dequeue();
                }
                break;

            case RouteActionType.TOWN:
                {
                    Town();
                    RouteQueue.Dequeue();
                }
                break;

            case RouteActionType.OBJECTEVENT:
                if (MySelf.GetPosition() != movePosition)
                    MySelf.SetMovePosition(movePosition);
                else
                {
                    ObjectEvent(route.EventId, route.ObjectId);
                    RouteQueue.Dequeue();
                }
                break;

            case RouteActionType.NPCEVENT:
                if (MySelf.GetPosition() != movePosition)
                    MySelf.SetMovePosition(movePosition);
                else
                {
                    NpcEvent(route.NpcId);

                     var character = NpcList.FirstOrDefault(x => x.Id == route.NpcId)!;

                    if (character != null) {
                        // Self action
                        Client.Session.Client.CharacterHandler.LoadNpcQuestList((short)character.ProtoId);

                        // Forward action to all followers
                        var followers = Client.Session.Client.CharacterHandler.GetFollowersAtSameZone();

                        followers.ForEach(x =>
                        {
                            x.CharacterHandler.LoadNpcQuestList((short)character.ProtoId);
                        });

                        RouteQueue.Dequeue();
                    }
                }
                break;

            case RouteActionType.SKILLOPEN:
                var skillTeacher = GetNpcList().FirstOrDefault(x => x.Name.Contains("Kaishan", StringComparison.InvariantCultureIgnoreCase));

                if (skillTeacher != null)
                {
                    if (MySelf.GetPosition() != skillTeacher.GetPosition())
                        MySelf.SetMovePosition(skillTeacher.GetPosition());
                    else
                    {
                        NpcEvent(skillTeacher.Id);
                        QuestGive(4062);
                        SelectMenu(0, "18004_Kaisan.lua", false);
                        QuestCompleted(4062);

                        Controller.SetControl("SelectedSkillList", "[]");
                        Client.Session.Client.CharacterHandler.InitializeSkillList();

                        RouteQueue.Dequeue();
                    }
                }
                break;

            case RouteActionType.SUPPLY:
                {
                    if (MySelf.GetPosition() != movePosition && route.SubQueue.Count == 0)
                        MySelf.SetMovePosition(movePosition);
                    else
                    {
                        if (route.SubQueue.Count == 0)
                        {
                            await Task.Delay(3000);

                            var potion = GetNpcList().FirstOrDefault(x => x.Name.Contains("Potion", StringComparison.InvariantCultureIgnoreCase));

                            if (potion != null)
                                route.SubQueue.Enqueue(new RouteData() { Action = RouteActionType.POTION, TargetId = potion.Id, X = potion.X, Y = potion.Y, Z = potion.Z });

                            var sundries = GetNpcList().FirstOrDefault(x => x.Name.Contains("Sundries", StringComparison.InvariantCultureIgnoreCase));

                            if (sundries != null)
                                route.SubQueue.Enqueue(new RouteData() { Action = RouteActionType.SUNDRIES, TargetId = sundries.Id, X = sundries.X, Y = sundries.Y, Z = sundries.Z });

                            if (route.SubQueue.Count == 0)
                                RouteQueue.Dequeue();
                        }
                        else
                        {
                            var subRoute = route.SubQueue.Peek();

                            movePosition = new Vector3((float)Math.Round(subRoute.X, 1), (float)Math.Round(subRoute.Y, 1), (float)Math.Round(subRoute.Z, 1));

                            if (MySelf.GetPosition() != movePosition)
                                MySelf.SetMovePosition(movePosition);
                            else
                            {
                                switch (subRoute.Action)
                                {
                                    case RouteActionType.POTION:
                                        {
                                            var supplyItemListPotion = JsonSerializer.Deserialize<List<Supply>>(Controller.GetControl("SupplyList", "[]"))!;

                                            if (!subRoute.NpcEventSend)
                                            {
                                                NpcEvent(subRoute.TargetId);
                                                subRoute.NpcEventSend = true;
                                            }

                                            await Task.Delay(3000);

                                            if (MySelf.NpcEventGroup != 0)
                                            {
                                                for (int i = Config.SLOT_MAX; i < Config.SLOT_MAX + Config.HAVE_MAX; i++)
                                                {
                                                    var item = MySelf.Inventory[i];

                                                    if (item.ItemID != 0 && item.Table != null)
                                                    {
                                                        if (SQLiteHandler.Table<SupplyFlag>().Any(x => x.Flag == (int)SupplyFlagType.FLAG_SELL && x.ItemId == item.ItemID))
                                                            ItemSell(subRoute.TargetId, MySelf.NpcEventGroup, (int)item.ItemID, (short)item.Count);
                                                    }
                                                }

                                                StoreClose();

                                                supplyItemListPotion.ForEach(x =>
                                                {
                                                    if (!x.Enable || MySelf.NpcEventGroup != x.SellingGroup)
                                                        return;

                                                    ItemBuy(subRoute.TargetId, MySelf.NpcEventGroup, x.ItemId, (short)x.Count);
                                                });

                                                StoreClose();

                                                MySelf.NpcEventGroup = 0;

                                            }

                                            route.SubQueue.Dequeue();
                                        }
                                        break;

                                    case RouteActionType.SUNDRIES:
                                        {
                                            var supplyItemListSundries = JsonSerializer.Deserialize<List<Supply>>(Controller.GetControl("SupplyList", "[]"))!;

                                            if (!subRoute.NpcEventSend)
                                            {
                                                NpcEvent(subRoute.TargetId);
                                                subRoute.NpcEventSend = true;
                                            }

                                            await Task.Delay(3000);

                                            if (MySelf.NpcEventGroup != 0)
                                            {
                                                for (int i = Config.SLOT_MAX; i < Config.SLOT_MAX + Config.HAVE_MAX; i++)
                                                {
                                                    var item = MySelf.Inventory[i];

                                                    if (item.ItemID != 0 && item.Table != null)
                                                    {
                                                        if (SQLiteHandler.Table<SupplyFlag>().Any(x => x.Flag == (int)SupplyFlagType.FLAG_SELL && x.ItemId == item.ItemID))
                                                            ItemSell(subRoute.TargetId, MySelf.NpcEventGroup, (int)item.ItemID, (short)item.Count);
                                                    }
                                                }

                                                StoreClose();

                                                supplyItemListSundries.ForEach(x =>
                                                {
                                                    if (!x.Enable || MySelf.NpcEventGroup != x.SellingGroup)
                                                        return;

                                                    ItemBuy(subRoute.TargetId, MySelf.NpcEventGroup, x.ItemId, (short)x.Count);
                                                });

                                                StoreClose();

                                                for (byte i = 0; i < Config.SLOT_MAX; i++)
                                                {
                                                    switch (i)
                                                    {
                                                        case 1:
                                                        case 4:
                                                        case 6:
                                                        case 8:
                                                        case 10:
                                                        case 12:
                                                        case 13:
                                                            {
                                                                var item = MySelf.Inventory.FirstOrDefault(x => x.Pos == i)!;

                                                                if (item.ItemID != 0)
                                                                    ItemRepair(1, subRoute.TargetId, item);
                                                            }
                                                            break;
                                                    }
                                                }

                                                MySelf.NpcEventGroup = 0;

                                            }

                                            route.SubQueue.Dequeue();
                                        }
                                        break;
                                }

                                if (route.SubQueue.Count == 0)
                                    RouteQueue.Dequeue();
                            }
                        }
                    }

                }
                break;
        }
    }

    public void LoadQuestList()
    {
        var questHelper = TableHandler.GetQuestHelperList()
            .FindAll(x => (x.Class == 5 || x.Class == Character.GetRepresentClass(MySelf.Class)) &&
                x.EventStatus == 4 &&
                (x.Nation == MySelf.NationId || x.Nation == 3) &&
                x.Zone == Character.GetRepresentZone(MySelf.Zone) &&
                x.QuestType == 1);

        if (questHelper == null) return;

        QuestList.Clear();

        questHelper.ForEach(x =>
        {
            var questGuide = TableHandler.GetQuestGuideList().FirstOrDefault(y => y.Id == x.GuideIndex);

            if (questGuide == null) return;

            if (MySelf.Level < questGuide.MinLevel) return;
            if (MySelf.ActiveQuestList.Any(y => y.Id == x.EventDataIndex && (y.Status == 2 || y.Status == 1))) return;

            QuestList.Add(new Quest()
            {
                Id = x.EventDataIndex,
                BaseId = x.BaseId,
                LuaName = x.LuaName,
                Title = questGuide.Title,
                Description = questGuide.Description,
                MinLevel = questGuide.MinLevel,
                Status = x.EventStatus,
                NpcProtoId = x.NpcProtoId,
            });
        });

    }

    public void LoadNpcQuestList(int npcId)
    {
        var questHelper = TableHandler.GetQuestHelperList()
            .FindAll(x => (x.Class == 5 || x.Class == Character.GetRepresentClass(MySelf.Class)) &&
                x.EventStatus == 4 &&
                (x.Nation == MySelf.NationId || x.Nation == 3) &&
                x.Zone == Character.GetRepresentZone(MySelf.Zone) &&
                x.NpcProtoId == npcId &&
                x.QuestType == 1);

        if (questHelper == null) return;

        QuestList.Clear();

        questHelper.ForEach(x =>
        {
            var questGuide = TableHandler.GetQuestGuideList().FirstOrDefault(y => y.Id == x.GuideIndex);

            if (questGuide == null) return;

            if (MySelf.Level < questGuide.MinLevel) return;
            if (MySelf.ActiveQuestList.Any(y => y.Id == x.EventDataIndex && (y.Status == 2 || y.Status == 1))) return;

            QuestList.Add(new Quest()
            {
                Id = x.EventDataIndex,
                BaseId = x.BaseId,
                LuaName = x.LuaName,
                Title = questGuide.Title,
                Description = questGuide.Description,
                MinLevel = questGuide.MinLevel,
                Status = x.EventStatus,
                NpcProtoId = x.NpcProtoId,
            });
        });

    }

    public void StatChangeReq()
    {
        Route(new List<RouteData>()
        {
            new RouteData() { Action = RouteActionType.TOWN, X = 0, Y = 0, Z = 0 },
            new RouteData() { Action = RouteActionType.MOVE, X = 816, Y = 707, Z = 0 },
            new RouteData() { Action = RouteActionType.SKILLOPEN, X = 816, Y = 707, Z = 0  }
        });
    }

    public void SendMove(Vector3 startPosition, Vector3 movePosition, short moveSpeed, byte moveType)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_Move(startPosition, movePosition, moveSpeed, moveType)).ConfigureAwait(false);

        if ((moveSpeed != 0 && moveType == 0) || (moveType != 0))
            MySelf.Moving = true;
        else
            MySelf.Moving = false;

        MySelf.MoveSendTime = Environment.TickCount;
    }

    public Vector3 MoveTowards(Vector3 current, Vector3 target)
    {
        float maxDistanceDelta = 6.75f;

        switch (MySelf.Speed)
        {
            case 67: //Sprint - Swift
                maxDistanceDelta = 10.125f;
                break;
            case 90: //Light Feet
                maxDistanceDelta = 13.5f;
                break;
        }

        return MoveTowards(current, target, maxDistanceDelta);
    }

    /// <summary>
    ///     Move to wards
    /// </summary>
    /// <remarks>https://github.com/Unity-Technologies/UnityCsReference/blob/c84064be69f20dcf21ebe4a7bbc176d48e2f289c/Runtime/Export/Math/Vector3.cs#L61</remarks>
    public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
    {
        // avoid vector ops because current scripting backends are terrible at inlining
        float toVector_x = target.X - current.X;
        float toVector_y = target.Y - current.Y;
        float toVector_z = target.Z - current.Z;

        float sqdist = toVector_x * toVector_x + toVector_y * toVector_y + toVector_z * toVector_z;

        if (sqdist == 0 || maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta)
            return target;

        var dist = (float)Math.Sqrt(sqdist);

        float X = (float)Math.Round(current.X + toVector_x / dist * maxDistanceDelta, 1);
        float Y = (float)Math.Round(current.Y + toVector_y / dist * maxDistanceDelta, 1);
        float Z = (float)Math.Round(current.Z + toVector_z / dist * maxDistanceDelta, 1);

        return new Vector3(X, Y, Z);
    }

    private Task ProcessMove()
    {
        if (MySelf.IsDead() || MySelf.GameState != GameState.GAME_STATE_INGAME)
            return Task.CompletedTask;

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == MySelf.Zone)!;

        if (zoneData == null)
            return Task.CompletedTask;

        var startPosition = MySelf.GetPosition();
        var movePosition = MySelf.GetMovePosition();

        if (movePosition.Equals(Vector3.Zero))
            return Task.CompletedTask;

        if ((Environment.TickCount - MySelf.MovePunishTime) <= 1000)
            return Task.CompletedTask;

        if (Controller.GetControl("SpeedhackCheckbox", false))
        {
            if ((Environment.TickCount - MySelf.MoveSendTime) <= 300)
                return Task.CompletedTask;

            var moveTowards = MoveTowards(startPosition, movePosition);

            //if (startPosition == moveTowards)
               // MySelf.SetMovePosition(Vector3.Zero);
            //else
                SendMove(startPosition, moveTowards, MySelf.Speed, 0);

            MySelf.SetPosition(moveTowards);
        }
        else
        {
            if ((Environment.TickCount - MySelf.MoveSendTime) <= 1750)
                return Task.CompletedTask;

            var moveTowards = MoveTowards(startPosition, movePosition);

            if (startPosition == moveTowards)
            {
                if (MySelf.IsMoving())
                    SendMove(startPosition, startPosition, 0, 0);

                MySelf.SetMovePosition(Vector3.Zero);
            }
            else
            {
                if (!MySelf.IsMoving())
                    SendMove(startPosition, moveTowards, MySelf.Speed, 1);
                else
                    SendMove(startPosition, moveTowards, MySelf.Speed, 3);
            }

            MySelf.SetPosition(moveTowards);
        }

        return Task.CompletedTask;
    }

    private Task ProcessController()
    {
        if (MySelf.GameState != GameState.GAME_STATE_INGAME)
            return Task.CompletedTask;

        if (Controller != null)
        {
            var followedClient = ClientHandler.ClientList.ToList()
                .FirstOrDefault(x =>
                    x != null &&
                    x.CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME
                    && x.Character.Name == Controller?.GetControl("Follow", ""));

            if (followedClient != null)
            {
                if (followedClient.CharacterHandler.Controller != null)
                {
                    if (Controller.GetControl("SelectedRoute", 0) != followedClient.CharacterHandler.Controller.GetControl("SelectedRoute", 0))
                        Controller.SetControl("SelectedRoute", followedClient.CharacterHandler.Controller.GetControl("SelectedRoute", 0));
                }
            }

            if (followedClient != null)
            {
                if (!followedClient.CharacterHandler.IsRouting() &&
                    !followedClient.Character.IsTrading &&
                    followedClient.Character.Zone == MySelf.Zone)
                {
                    // Moving to followed client
                    if (!MySelf.IsDead() && !IsRouting() && !IsMovingToLoot() && !MySelf.IsTrading
                        && followedClient.Character.GetPosition() != MySelf.GetPosition())
                    {
                        var followPosition = followedClient.Character.GetPosition();

                        MySelf.SetMovePosition(followPosition);
                    }

                    // Update target list from followed client
                    if (MySelf.SelectedTargetList != followedClient.Character.SelectedTargetList)
                    {
                        MySelf.SelectedTargetList = followedClient.Character.SelectedTargetList;
                        Controller.SetControl("SelectedTargetList", JsonSerializer.Serialize(MySelf.SelectedTargetList.Select(e => e.Id)));
                    }
                }
            }

            if (MySelf.IsDead() && Controller.GetControl("RegenerateWhenDie", true))
                Regen();
        }

        return Task.CompletedTask;
    }

    private Task ProcessAutoLoot()
    {
        if (MySelf.IsDead() || IsRouting() || MySelf.IsTrading || MySelf.GameState != GameState.GAME_STATE_INGAME)
            return Task.CompletedTask;

        LootList.RemoveAll(x => x != null && (Environment.TickCount - x.DropTime) >= 35000);

        if (LootList.Count == 0)
        {
            MovingToLoot = false;
            return Task.CompletedTask;
        }

        if (Controller.GetControl("MoveToLootCheckBox", false))
        {
            var loot = LootList.FindAll(x => x != null && !x.Opened && (Environment.TickCount - x.DropTime) >= 5000).OrderBy(x => x.DropTime).FirstOrDefault()!;

            if (loot == null)
                return Task.CompletedTask;

            MovingToLoot = true;

            if (MySelf.GetPosition() != loot.Position)
                MySelf.SetMovePosition(loot.Position);
            else
            {
                loot.Opened = true;

                //if (MySelf.IsMoving())
                    //SendMove(MySelf.GetPosition(), MySelf.GetPosition(), 45, 0);

                Client.Session.SendAsync(MessageBuilder.MsgSend_RequestItemBundleOpen((int)loot.BundleId)).ConfigureAwait(false);

                MovingToLoot = false;
            }
        }
        else
        {
            MovingToLoot = false;

            var loot = LootList
                .FindAll(x => x != null && !x.Opened &&
                                (Environment.TickCount - x.DropTime) >= 5000 &&
                                Vector3.Distance(MySelf.GetPosition(), x.Position) <= 8.0f)
                .OrderBy(x => x.DropTime)
                .FirstOrDefault()!;

            if (loot == null)
                return Task.CompletedTask;

            loot.Opened = true;

            //if (MySelf.IsMoving())
                //SendMove(MySelf.GetPosition(), MySelf.GetPosition(), 45, 0);

            Client.Session.SendAsync(MessageBuilder.MsgSend_RequestItemBundleOpen((int)loot.BundleId)).ConfigureAwait(false);
        }

        return Task.CompletedTask;
    }

    public void ZoneChange(byte zoneId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ZoneChange((byte)ZoneChangeOpcode.ZoneChangeMilitaryCamp, zoneId)).ConfigureAwait(false);
    }

    public void WarpTeleport(WarpInfo warpInfo)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_WarpTeleport(MySelf.SelectedObject, (ushort)warpInfo.Id)).ConfigureAwait(false);
    }

    public void ObjectEvent(short eventId, short objectId)
    {
        MySelf.SelectedObject = eventId;
        Client.Session.SendAsync(MessageBuilder.MsgSend_ObjectEvent(eventId, objectId)).ConfigureAwait(false);
    }

    public void ExhangeRequest(int targetId)
    {
        MySelf.TradeRequestedUserId = MySelf.Id;
        Client.Session.SendAsync(MessageBuilder.MsgSend_ExhangeRequest(targetId)).ConfigureAwait(false);
    }

    public void ExhangeRequestProcess(int targetId)
    {
        if (Controller == null)
            return;

        MySelf.TradeRequestedUserId = targetId;

        var master = PlayerList.FirstOrDefault(x => x.Id == targetId && x.Name == Controller.GetControl("MasterCharacter", ""))!;

        if (master != null)
            ExhangeAgree();
    }

    public void ExhangeAgree()
    {
        MySelf.TradedUserId = MySelf.TradeRequestedUserId;
        MySelf.TradeRequestedUserId = 0;

        Client.Session.SendAsync(MessageBuilder.MsgSend_ExhangeAgree(true)).ConfigureAwait(false);
    }

    public void ExhangeAgreeProcess()
    {
        if (Controller == null)
            return;

        MySelf.TradedUserId = MySelf.TradeRequestedUserId;
        MySelf.TradeRequestedUserId = 0;

        var master = PlayerList.FirstOrDefault(x => x.Name == Controller.GetControl("MasterCharacter", ""))!;

        if (master != null)
        {
            for (int i = Config.SLOT_MAX; i < Config.SLOT_MAX + Config.HAVE_MAX; i++)
            {
                var item = MySelf.Inventory[i];

                if (item.ItemID != 0 && item.Table != null)
                {
                    if (SQLiteHandler.Table<SupplyFlag>().Any(x => x.Flag == (int)SupplyFlagType.FLAG_TRADE_TO_MASTER && x.ItemId == item.ItemID))
                    {
                        ExhangeAdd((byte)(item.Pos - Config.SLOT_MAX), item.ItemID, item.Count);
                    }
                }
            }

            var giveNoah = Controller.GetControl("MasterGiveNoahAmount", 0);

            Client.Session.SendAsync(MessageBuilder.MsgSend_ExhangeAddGold((uint)giveNoah)).ConfigureAwait(false);

            ExhangeDecision();
        }
    }

    public void ExhangeAdd(byte currentPosition, uint itemId, uint quantity)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ExhangeAdd(currentPosition, itemId, quantity)).ConfigureAwait(false);
    }

    public void ExhangeDecision()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ExchangeDecision()).ConfigureAwait(false);
    }

    public void ExhangeCancel()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ExchangeCancel()).ConfigureAwait(false);
    }

    public void QuestTake(uint questId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_QuestTake(questId)).ConfigureAwait(false);
    }

    public void QuestGive(uint questId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_QuestGive(questId)).ConfigureAwait(false);
    }

    public void QuestRemove(uint questId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_QuestRemove(questId)).ConfigureAwait(false);
    }

    public void QuestCompleted(uint questId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_QuestCompleted(questId)).ConfigureAwait(false);
    }

    public void SelectMenu(byte menuIndex, string luaName, bool accept)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_SelectMenu(menuIndex, luaName, accept)).ConfigureAwait(false);
    }

    public void NpcEvent(int npcId)
    {
        MySelf.NpcEventId = npcId;
        Client.Session.SendAsync(MessageBuilder.MsgSend_NpcEvent(npcId)).ConfigureAwait(false);
    }

    public void Event(byte opCode, uint itemId)
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_Event(opCode, itemId)).ConfigureAwait(false);
    }

    public void ProcessSelectMessage(byte opcode, int questId)
    {
        var questHelper = TableHandler.GetQuestHelperList().FirstOrDefault(x => x.EventDataIndex == questId);

        if (questHelper != null)
        {
            /*switch (opcode)
            {
                case 2:
                    Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        SelectMenu(0, questHelper.LuaName, false);
                    });
                    break;

                case 4:
                    Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        SelectMenu(0, questHelper.LuaName, true);
                    });
                    break;
            }*/

        }
    }

    public Task ProcessQuest()
    {
        if (MySelf.IsDead() || MySelf.GameState != GameState.GAME_STATE_INGAME)
            return Task.CompletedTask;

        MySelf.ActiveQuestList.FindAll(x => x.Status == 1).ForEach(quest =>
        {
            if (quest.CollectItem != null)
            {
                quest.CollectItem.ToList().ForEach(item =>
                {
                    if (item.ItemId == 900000000)
                        item.ItemCount = (int)MySelf.Gold;
                    else
                    {
                        var itemInventoryExist = MySelf.Inventory.FirstOrDefault(x => x != null && x.ItemID == item.ItemId);

                        if (itemInventoryExist != null && itemInventoryExist.Count > 0)
                            item.ItemCount = itemInventoryExist.Count;
                        else
                            item.ItemCount = 0;
                    }
                });

                if (quest.IsCollectCompleted())
                {
                    var questHelper = TableHandler.GetQuestHelperList()
                        .FirstOrDefault(x => x.EventDataIndex == quest.Id && x.EventStatus == 3);

                    if (questHelper != null)
                        quest.BaseId = questHelper.BaseId;

                    quest.Status = 3;
                }

            }
        });

        return Task.CompletedTask;
    }

    public static List<Vector3> GenerateMovePath(byte zoneId, Vector3 startPosition, Vector3 endPosition)
    {
        List<Vector3> movePath = new List<Vector3>();

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == zoneId)!;

        if (zoneData == null)
            return movePath;

        var pathfinderOptions = new PathFinderOptions
        {
            //PunishChangeDirection = false,
            //UseDiagonals = true,
            //HeuristicFormula = AStar.Heuristics.HeuristicFormula.DiagonalShortCut,
            SearchLimit = 50000000
        };

        var worldGrid = new WorldGrid(zoneData.GetEvents);
        var pathfinder = new PathFinder(worldGrid, pathfinderOptions);

        int startX = (int)(startPosition.X / (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));
        int startY = (int)(startPosition.Y / (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));

        int endX = (int)(endPosition.X / (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));
        int endY = (int)(endPosition.Y / (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));

        AStar.Position[] paths = pathfinder.FindPath(new AStar.Position(startX, startY), new AStar.Position(endX, endY));

        if (paths.Length == 1)
            return movePath;

        int step = 10;

        if (paths.Length < step)
            step = paths.Length;

        for (int i = 0; i < paths.Length; i += step)
        {
            int positionX = (int)(paths[i].Row * (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));
            int positionY = (int)(paths[i].Column * (zoneData.GetShapeManager().MapLength / zoneData.GetMapSize()));

            var position = new Vector3(positionX, positionY, (float)Math.Round(zoneData.GetHeightBy2DPos(positionX, positionY), 1));

            movePath.Add(position);
        }

        return movePath;
    }

    public bool IsNeedSupply(List<Supply> supplyList)
    {
        bool needSupply = false;

        supplyList.ForEach(x =>
        {
            if (!x.Enable) return;

            var item = MySelf.Inventory.FirstOrDefault(y => y != null && y.ItemID == x.ItemId);

            if (item == null || item.Count < 3)
                needSupply = true;
        });

        return needSupply;
    }

    public bool IsNeedRepair()
    {
        for (byte i = 0; i < Config.SLOT_MAX; i++)
        {
            switch (i)
            {
                case 1:
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                case 13:
                    {
                        var item = MySelf.Inventory.FirstOrDefault(x => x != null && x.Pos == i)!;

                        if (item != null && item.ItemID != 0 && item.Durability == 0)
                            return true;
                    }
                    break;
            }
        }

        return false;
    }

    public bool IsInventoryFull()
    {
        return GetInventoryEmptySlotCount() == 0;
    }

    public int GetInventoryEmptySlotCount()
    {
        int count = 0;

        for (int i = Config.SLOT_MAX; i < Config.SLOT_MAX + Config.HAVE_MAX; i++)
        {
            var item = MySelf.Inventory[i];

            if (item != null && item.ItemID == 0)
                count++;
        }

        return count;
    }

    public void StoreClose()
    {
        Client.Session.SendAsync(MessageBuilder.MsgSend_ShoppingMall((byte)ShoppingMallType.STORE_CLOSE)).ConfigureAwait(false);
    }

    public void RunSelectedRoute()
    {
        if (Controller == null)
            return;

        var route = SQLiteHandler.Table<Route>().SingleOrDefault(x => x.Id == Controller.GetControl("SelectedRoute", 0));

        if (route == null)
            return;

        if (route != null && route.Zone == MySelf.Zone)
            Route(JsonSerializer.Deserialize<List<RouteData>>(route.Data)!);
    }

    public void MoveToSelectedRouteFinal()
    {
        if (Controller == null)
            return;

        var route = SQLiteHandler.Table<Route>().SingleOrDefault(x => x.Id == Controller.GetControl("SelectedRoute", 0));

        if (route == null)
            return;

        var routeData = JsonSerializer.Deserialize<List<RouteData>>(route.Data)!;

        if (routeData != null && route.Zone == MySelf.Zone)
        {
            var routeFinal = routeData.Last();

            var routeLastPosition = new Vector3((float)Math.Round(routeFinal.X, 1), (float)Math.Round(routeFinal.Y, 1), (float)Math.Round(routeFinal.Z, 1));

            if (MySelf.GetPosition() != routeLastPosition && !MySelf.IsDead())
            {
                Route(new List<RouteData>()
                {
                    new RouteData() { Action = RouteActionType.MOVE, X = routeLastPosition.X, Y = routeLastPosition.Y, Z = routeLastPosition.Z },
                });
            }
        }
    }

    public List<Client> GetFollowersAtSameZone()
    {
        return ClientHandler.ClientList.ToList()
            .FindAll(x =>
                x != null &&
                x.CharacterHandler.GetGameState() == GameState.GAME_STATE_INGAME && 
                MySelf.Name == x.CharacterHandler.Controller?.GetControl("Follow", "") &&
                x.CharacterHandler.MySelf.Zone == MySelf.Zone);
    }

}


