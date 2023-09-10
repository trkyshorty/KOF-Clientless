using System.ComponentModel;
using System.Numerics;
using KOF.Core.Enums;
using KOF.Core.Handlers;
using KOF.Data.Models;
using KOF.Zone;

namespace KOF.Core.Models;

public class Character
{
    [Browsable(false)]
    public int m_iJoinReqClanRequierID { get; set; }

    [Browsable(false)]
    public short m_iJoinReqClan { get; set; }

    [Browsable(false)]
    public long bundlecount { get; set; }

    [Browsable(false)]
    public int Id { get; set; }

    public string Name { get; set; } = "";

    [Browsable(false)]
    public short Knight { get; set; }

    [Browsable(true)]
    public byte Level { get; set; }

    [Browsable(false)]
    public short Points { get; set; }

    [Browsable(false)]
    public long MaxExperience { get; set; }

    [Browsable(false)]
    public long Experience { get; set; }

    [Browsable(false)]
    public uint Loyalty { get; set; }

    [Browsable(false)]
    public uint LoyaltyMonthly { get; set; }

    [Browsable(false)]
    public byte Fame { get; set; }

    [Browsable(false)]
    public ushort MaxHp { get; set; }

    [Browsable(false)]
    public short Hp { get; set; }

    [Browsable(false)]
    public ushort MaxMp { get; set; }

    [Browsable(false)]
    public short Mp { get; set; }

    [Browsable(false)]
    public uint MaxWeight { get; set; }

    [Browsable(false)]
    public uint Weight { get; set; }

    [Browsable(false)]
    public byte Race { get; set; }

    [Browsable(false)]
    public ushort Class { get; set; }

    [Browsable(false)]
    public byte Face { get; set; }

    [Browsable(false)]
    public int Hair { get; set; }

    [Browsable(false)]
    public byte Rank { get; set; }

    [Browsable(false)]
    public byte Title { get; set; }

    [Browsable(false)]
    public byte IsHidingHelmet { get; set; }

    [Browsable(false)]
    public byte IsHidingCospre { get; set; }

    [Browsable(false)]
    public float X { get; set; }

    [Browsable(false)]
    public float Y { get; set; }

    [Browsable(false)]
    public float Z { get; set; }

    [Browsable(false)]
    public float GoX { get; set; }

    [Browsable(false)]
    public float GoY { get; set; }

    [Browsable(false)]
    public float GoZ { get; set; }

    [Browsable(false)]
    public float WillX { get; set; }

    [Browsable(false)]
    public float WillY { get; set; }

    [Browsable(false)]
    public float WillZ { get; set; }

    [Browsable(false)]
    public Queue<Vector3> MovePath { get; set; } = new();

    [Browsable(false)]
    public byte[] Stats { get; set; } = new byte[5];

    [Browsable(false)]
    public byte[] StatsItemBonuses { get; set; } = new byte[5];

    [Browsable(false)]
    public ushort TotalHit { get; set; }

    [Browsable(false)]
    public ushort TotalAc { get; set; }

    [Browsable(false)]
    public byte FireR { get; set; }

    [Browsable(false)]
    public byte ColdR { get; set; }

    [Browsable(false)]
    public byte LightningR { get; set; }

    [Browsable(false)]
    public byte MagicR { get; set; }

    [Browsable(false)]
    public byte DiseaseR { get; set; }

    [Browsable(false)]
    public byte PoisonR { get; set; }

    [Browsable(false)]
    public uint Gold { get; set; }

    [Browsable(false)]
    public uint Bank { get; set; }

    [Browsable(false)]
    public byte Authority { get; set; }

    [Browsable(false)]
    public byte KnightsRank { get; set; }

    [Browsable(false)]
    public byte PersonalRank { get; set; }

    [Browsable(false)]
    public byte[] Skills { get; set; } = new byte[10];

    [Browsable(false)]
    public Inventory[] Inventory { get; set; } = new Inventory[75];

    [Browsable(false)]
    public byte AccountStatus { get; set; }

    [Browsable(false)]
    public byte PremiumInUse { get; set; }

    [Browsable(false)]
    public byte IsChicken { get; set; }

    [Browsable(false)]
    public uint MannerPoint { get; set; }

    [Browsable(false)]
    public byte KarusBaseMilitaryCampCount { get; set; }

    [Browsable(false)]
    public byte ElmoradBaseMilitaryCampCount { get; set; }

    [Browsable(false)]
    public byte KarusEslantMilitaryCampCount { get; set; }

    [Browsable(false)]
    public byte ElmoradEslantMilitaryCampCount { get; set; }

    [Browsable(false)]
    public byte MoradonMilitaryCampCount { get; set; }

    [Browsable(false)]
    public byte GenieStatus { get; set; }

    [Browsable(false)]
    public ushort GenieReaminingTime { get; set; }

    [Browsable(false)]
    public Clan Knights { get; set; } = new();

    [Browsable(false)]
    public byte PartyLeader { get; set; }

    [Browsable(false)]
    public byte InvisibilityType { get; set; }

    [Browsable(false)]
    public byte TeamColor { get; set; }

    [Browsable(false)]
    public float Rotation { get; set; }

    [Browsable(false)]
    public byte Status { get; set; }

    [Browsable(false)]
    public uint StatusSize { get; set; }

    [Browsable(false)]
    public byte NeedParty { get; set; }

    [Browsable(false)]
    public byte Zone { get; set; }

    [Browsable(false)]
    public string Area { get { return GetRepresentZoneName(Zone); } }

    [Browsable(false)]
    public Inventory[] VisibleEquip { get; set; } = new Inventory[16];

    [Browsable(false)]
    public ushort ProtoId { get; set; }

    [Browsable(false)]
    public ushort PictureId { get; set; }

    [Browsable(false)]
    public uint SellingGroup { get; set; }

    [Browsable(false)]
    public byte FamilyType { get; set; }

    [Browsable(false)]
    public uint Unknown1 { get; set; }

    [Browsable(false)]
    public uint Unknown2 { get; set; }

    [Browsable(false)]
    public byte Unknown3 { get; set; }

    [Browsable(false)]
    public uint Unknown4 { get; set; }

    [Browsable(false)]
    public ushort ModelSize { get; set; }

    [Browsable(false)]
    public uint Weapon1 { get; set; }

    [Browsable(false)]
    public uint Weapon2 { get; set; }

    [Browsable(false)]
    public byte ModelGroup { get; set; }

    [Browsable(false)]
    public byte MonsterOrNpc { get; set; }

    [Browsable(false)]
    public short Speed { get; set; } = 45;

    [Browsable(false)]
    public byte MoveType { get; set; }

    [Browsable(false)]
    public bool Moving { get; set; }

    [Browsable(false)]
    public long MoveSendTime { get; set; }

    [Browsable(false)]
    public long MovePunishTime { get; set; }

    [Browsable(false)]
    public int TargetId { get; set; }

    [Browsable(false)]
    public long TargetHpUpdateTime { get; set; }

    [Browsable(false)]
    public short SelectedObject { get; set; }

    [Browsable(false)]
    public Monster Monster { get; set; } = default!;

    [Browsable(false)]
    public Npc Npc { get; set; } = default!;

    [Browsable(false)]
    public int UntouchableTime { get; set; } = Environment.TickCount + 15000;

    [Browsable(false)]
    public Party Party { get; set; } = new();

    [Browsable(false)]
    public Dictionary<byte, Skill> BuffList { get; set; } = new();

    [Browsable(false)]
    public List<Skill> SkillList { get; set; } = new();

    [Browsable(false)]
    public List<Skill> SelectedSkillList { get; set; } = new();

    [Browsable(false)]
    public List<Monster> SelectedTargetList { get; set; } = new();

    [Browsable(false)]
    public GameState GameState { get; set; } = GameState.GAME_STATE_NOT_CONNECTED;

    [Browsable(false)]
    public byte State { get; set; } = (byte)StateAction.BASIC;

    [Browsable(false)]
    public int TradeRequestedUserId { get; set; }

    [Browsable(false)]
    public int TradedUserId { get; set; }

    [Browsable(false)]
    public bool IsTrading { get { return TradedUserId != 0 || TradeRequestedUserId != 0; } }

    public string Job { get { return GetRepresentClassName(Class); } }

    [Browsable(false)]
    public List<Quest> ActiveQuestList { get; set; } = new();

    [Browsable(false)]
    public byte NationId { get; set; }

    [Browsable(false)]
    public int NpcEventId { get; set; }

    [Browsable(false)]
    public int NpcEventGroup { get; set; }

    [Browsable(false)]
    public bool LunarWarDressUp { get; set; }

    public string Nation { get { return GetNationName(NationId); } }

    public static string GetNationName(int NationId)
    {
        switch (NationId)
        {
            case 1:
                return "Karus";
            case 2:
                return "El Morad";
            default:
                return "Unknown";
        }
    }

    public static string GetRepresentZoneName(int zoneId)
    {
        switch (zoneId)
        {
            case (int)ZoneInfo.ZONE_KARUS:
            case (int)ZoneInfo.ZONE_KARUS2:
                return "Luferson Castle";

            case (int)ZoneInfo.ZONE_ELMORAD:
            case (int)ZoneInfo.ZONE_ELMORAD2:
                return "Elmorad Castle";

            case (int)ZoneInfo.ZONE_KARUS_ESLANT:
            case (int)ZoneInfo.ZONE_ELMORAD_ESLANT:
            case (int)ZoneInfo.ZONE_KARUS_ESLANT2:
            case (int)ZoneInfo.ZONE_ELMORAD_ESLANT2:
                return "Eslant";

            case (int)ZoneInfo.ZONE_MORADON:
            case (int)ZoneInfo.ZONE_MORADON2:
                return "Moradon";

            case (int)ZoneInfo.ZONE_DELOS:
                return "Delos";

            case (int)ZoneInfo.ZONE_BIFROST:
                return "Bifrost";

            case (int)ZoneInfo.ZONE_DESPERATION_ABYSS:
                return "Abyss Dungeon";

            case (int)ZoneInfo.ZONE_HELL_ABYSS:
                return "Hell Abyss Dungeon";

            case (int)ZoneInfo.ZONE_DRAGON_CAVE:
                return "Felankor Lair";

            case (int)ZoneInfo.ZONE_ARENA:
                return "Arena";

            case (int)ZoneInfo.ZONE_ORC_ARENA:
                return "Orc Prisoner Arena";

            case (int)ZoneInfo.ZONE_BLOOD_DON_ARENA:
                return "Blood Don Arena";

            case (int)ZoneInfo.ZONE_GOBLIN_ARENA:
                return "Goblin Arena";

            case (int)ZoneInfo.ZONE_CAITHAROS_ARENA:
                return "Caitharos Arena";

            case (int)ZoneInfo.ZONE_FORGOTTEN_TEMPLE:
                return "Forgetten Temple";

            case (int)ZoneInfo.ZONE_BATTLE:
                return "Napies Gorge";

            case (int)ZoneInfo.ZONE_BATTLE2:
                return "Alseids Prairie";

            case (int)ZoneInfo.ZONE_BATTLE3:
                return "Nieds Triangle";

            case (int)ZoneInfo.ZONE_BATTLE4:
                return "Nereid's Island";

            case (int)ZoneInfo.ZONE_BATTLE5:
                return "Zipang";

            case (int)ZoneInfo.ZONE_BATTLE6:
                return "Oreads";

            case (int)ZoneInfo.ZONE_SNOW_BATTLE:
                return "Snow War";

            case (int)ZoneInfo.ZONE_RONARK_LAND:
                return "Ronark Land";

            case (int)ZoneInfo.ZONE_ARDREAM:
                return "Ardream";

            case (int)ZoneInfo.ZONE_RONARK_LAND_BASE:
                return "Ronark Land Base";

            case (int)ZoneInfo.ZONE_KROWAZ_DOMINION:
                return "Krowaz Dominion";

            case (int)ZoneInfo.ZONE_MONSTER_STONE1:
            case (int)ZoneInfo.ZONE_MONSTER_STONE2:
            case (int)ZoneInfo.ZONE_MONSTER_STONE3:
                return "Monster Stone";

            case (int)ZoneInfo.ZONE_BORDER_DEFENSE_WAR:
                return "Border Defense War";

            case (int)ZoneInfo.ZONE_CHAOS_DUNGEON:
                return "Chaos Dungeon";

            case (int)ZoneInfo.ZONE_UNDER_CASTLE:
                return "Under The Castle";

            case (int)ZoneInfo.ZONE_JURAD_MOUNTAIN:
                return "Juriad Mountain";

            case (int)ZoneInfo.ZONE_PRISON:
                return "Prison";

            case (int)ZoneInfo.ZONE_ISILOON_ARENA:
                return "Isillion Arena";

            case (int)ZoneInfo.ZONE_FELANKOR_ARENA:
                return "Felankor Arena";

            case (int)ZoneInfo.ZONE_DRAKI_TOWER:
                return "Draki Tower";

            default:
                return "Unknown";
        }
    }

    public static int GetRepresentZone(int zoneId)
    {
        switch (zoneId)
        {

            case (int)ZoneInfo.ZONE_KARUS:
            case (int)ZoneInfo.ZONE_KARUS2:
                return (int)ZoneInfo.ZONE_KARUS;

            case (int)ZoneInfo.ZONE_ELMORAD: 
            case (int)ZoneInfo.ZONE_ELMORAD2:
                return (int)ZoneInfo.ZONE_ELMORAD;

            case (int)ZoneInfo.ZONE_KARUS_ESLANT: 
            case (int)ZoneInfo.ZONE_KARUS_ESLANT2:
                return (int)ZoneInfo.ZONE_KARUS_ESLANT;

            case (int)ZoneInfo.ZONE_ELMORAD_ESLANT:
            case (int)ZoneInfo.ZONE_ELMORAD_ESLANT2:
                return (int)ZoneInfo.ZONE_ELMORAD_ESLANT;

            case (int)ZoneInfo.ZONE_MORADON:
            case (int)ZoneInfo.ZONE_MORADON2:
                return (int)ZoneInfo.ZONE_MORADON;

            default:
                return zoneId;
        }
    }

    public static string GetRepresentClassName(int classId)
    {
        switch (classId)
        {
            case (int)ClassType.CLASS_KA_WARRIOR:
            case (int)ClassType.CLASS_KA_BERSERKER:
            case (int)ClassType.CLASS_KA_GUARDIAN:
            case (int)ClassType.CLASS_EL_WARRIOR:
            case (int)ClassType.CLASS_EL_BLADE:
            case (int)ClassType.CLASS_EL_PROTECTOR:
                return "Warrior";

            case (int)ClassType.CLASS_KA_ROGUE:
            case (int)ClassType.CLASS_KA_HUNTER:
            case (int)ClassType.CLASS_KA_PENETRATOR:
            case (int)ClassType.CLASS_EL_ROGUE:
            case (int)ClassType.CLASS_EL_RANGER:
            case (int)ClassType.CLASS_EL_ASSASIN:
                return "Rogue";

            case (int)ClassType.CLASS_KA_PRIEST:
            case (int)ClassType.CLASS_KA_SHAMAN:
            case (int)ClassType.CLASS_KA_DARKPRIEST:
            case (int)ClassType.CLASS_EL_PRIEST:
            case (int)ClassType.CLASS_EL_CLERIC:
            case (int)ClassType.CLASS_EL_DRUID:
                return "Priest";

            case (int)ClassType.CLASS_KA_WIZARD:
            case (int)ClassType.CLASS_KA_SORCERER:
            case (int)ClassType.CLASS_KA_NECROMANCER:
            case (int)ClassType.CLASS_EL_WIZARD:
            case (int)ClassType.CLASS_EL_MAGE:
            case (int)ClassType.CLASS_EL_ENCHANTER:
                return "Mage";

            default:
                return "Unknown";
        }
    }

    public static int GetRepresentClass(int classId)
    {
        switch (classId)
        {
            case (int)ClassType.CLASS_KA_WARRIOR:
            case (int)ClassType.CLASS_KA_BERSERKER:
            case (int)ClassType.CLASS_KA_GUARDIAN:
            case (int)ClassType.CLASS_EL_WARRIOR:
            case (int)ClassType.CLASS_EL_BLADE:
            case (int)ClassType.CLASS_EL_PROTECTOR:
                return (int)ClassRepresentType.CLASS_REPRESENT_WARRIOR;

            case (int)ClassType.CLASS_KA_ROGUE:
            case (int)ClassType.CLASS_KA_HUNTER:
            case (int)ClassType.CLASS_KA_PENETRATOR:
            case (int)ClassType.CLASS_EL_ROGUE:
            case (int)ClassType.CLASS_EL_RANGER:
            case (int)ClassType.CLASS_EL_ASSASIN:
                return (int)ClassRepresentType.CLASS_REPRESENT_ROGUE;

            case (int)ClassType.CLASS_KA_WIZARD:
            case (int)ClassType.CLASS_KA_SORCERER:
            case (int)ClassType.CLASS_KA_NECROMANCER:
            case (int)ClassType.CLASS_EL_WIZARD:
            case (int)ClassType.CLASS_EL_MAGE:
            case (int)ClassType.CLASS_EL_ENCHANTER:
                return (int)ClassRepresentType.CLASS_REPRESENT_WIZARD;

            case (int)ClassType.CLASS_KA_PRIEST:
            case (int)ClassType.CLASS_KA_SHAMAN:
            case (int)ClassType.CLASS_KA_DARKPRIEST:
            case (int)ClassType.CLASS_EL_PRIEST:
            case (int)ClassType.CLASS_EL_CLERIC:
            case (int)ClassType.CLASS_EL_DRUID:
                return (int)ClassRepresentType.CLASS_REPRESENT_PRIEST;

            default:
                return (int)ClassRepresentType.CLASS_REPRESENT_UNKNOWN;
        }
    }

    public bool IsDead()
    {
        return (State == (byte)StateAction.DYING || State == (byte)StateAction.DEATH) || (MaxHp != 0 && Hp == 0);
    }

    public bool IsKing()
    {
        return Rank == 1;
    }

    public bool IsInClan()
    {
        return Knight > 0;
    }

    public bool IsMoving()
    {
        return Moving;
    }

    public bool IsInMonsterStone()
    {
        return Zone == (byte)ZoneInfo.ZONE_MONSTER_STONE1 || Zone == (byte)ZoneInfo.ZONE_MONSTER_STONE2 || Zone == (byte)ZoneInfo.ZONE_MONSTER_STONE3;
    }

    public void SetPosition(Vector3 position)
    {
        X = (float)Math.Round(position.X, 1);
        Y = (float)Math.Round(position.Y, 1);

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Zone)!;

        if (zoneData != null)
            Z = (float)Math.Round(zoneData.GetHeightBy2DPos(position.X, position.Y), 1);
        else
            Z = (float)Math.Round(position.Z, 1);
    }

    public Vector3 GetPosition()
    {
        return new((float)Math.Round(X, 1), (float)Math.Round(Y, 1), (float)Math.Round(Z, 1));
    }

    public Vector2 GetPosition2D()
    {
        return new((float)Math.Round(X, 1), (float)Math.Round(Y, 1));
    }

    public void SetMovePosition(Vector3 position)
    {
        if (GetMovePosition() == position) return;

        GoX = (float)Math.Round(position.X, 1);
        GoY = (float)Math.Round(position.Y, 1);

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Zone)!;

        if (!position.Equals(Vector3.Zero) && zoneData != null)
            GoZ = (float)Math.Round(zoneData.GetHeightBy2DPos(position.X, position.Y), 1);
        else
            GoZ = (float)Math.Round(position.Z, 1);

        MovePath.Clear();
    }

    public Vector3 GetMovePosition()
    {
        return new((float)Math.Round(GoX, 1), (float)Math.Round(GoY, 1), (float)Math.Round(GoZ, 1));
    }

    public void SetWillPosition(Vector3 position)
    {
        WillX = (float)Math.Round(position.X, 1);
        WillY = (float)Math.Round(position.Y, 1);

        var zoneData = ClientHandler.ZoneList.FirstOrDefault(x => x.GetZoneIndex() == Zone)!;

        if (!position.Equals(Vector3.Zero) && zoneData != null)
            WillZ = (float)Math.Round(zoneData.GetHeightBy2DPos(position.X, position.Y), 1);
        else
            WillZ = (float)Math.Round(position.Z, 1);
    }

    public Vector3 GetWillPosition()
    {
        return new((float)Math.Round(WillX, 1), (float)Math.Round(WillY, 1), (float)Math.Round(WillZ, 1));
    }

    public int GetTargetId()
    {
        return TargetId;
    }

    internal  Clan FromKnightsMessage(KOF.Core.Communications.Message msg) => new()
    {
        Alliance = msg.Read<ushort>(),
        Flag = msg.Read<byte>(),
        Name = msg.Read(false),
        Grade = msg.Read<byte>(),
        Ranking = msg.Read<byte>(),
        MarkVersion = msg.Read<ushort>(),
        Cape = msg.Read<ushort>(),
        CapeR = msg.Read<byte>(),
        CapeG = msg.Read<byte>(),
        CapeB = msg.Read<byte>(),
        UnknownValueA = msg.Read<short>(),
        UnknownValueB = msg.Read<short>(),
        UnknownValueC = msg.Read<byte>()
    };

}
