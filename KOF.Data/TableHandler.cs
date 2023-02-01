using KOF.Data.Models;

namespace KOF.Data;

public static class TableHandler
{
    private static readonly TableService _tableService;
    private static List<Item> ItemTable { get; set; } = new();
    private static List<ItemExtension> ItemExtensionTable { get; set; } = new();
    private static List<Skill> SkillTable { get; set; } = new();
    private static List<SkillExtension> SkillExtensionTable { get; set; } = new();
    private static List<Monster> MonsterTable { get; set; } = new();
    private static List<Npc> NpcTable { get; set; } = new();
    private static List<Position> PositionTable { get; set; } = new();
    private static List<ItemSell> ItemSellTable { get; set; } = new();
    private static List<WarpInfo> WarpInfoTable { get; set; } = new();
    private static List<QuestHelper> QuestHelperTable { get; set; } = new();
    private static List<QuestGuide> QuestGuideTable { get; set; } = new();
    private static List<QuestMonsterExchange> QuestMonsterExchangeTable { get; set; } = new();
    private static List<QuestNpcDesc> QuestNpcDescTable { get; set; } = new();
    private static List<QuestItemDesc> QuestItemDescTable { get; set; } = new();
    private static List<ItemExchange> ItemExchangeTable { get; set; } = new();

    static TableHandler()
    {
        _tableService = new TableService();
    }

    public static Task Load()
    {
        return Task.Run(() =>
        {
            LoadItemTable();
            LoadSkillTable();
            LoadMonsterTable();
            LoadNpcTable();
            LoadPositionTable();
            LoadItemSellTable();
            LoadWarpInfoTable();
            LoadQuestHelperTable();
            LoadQuestGuideTable();
            LoadQuestMonsterExchangeTable();
            LoadQuestNpcDescTable();
            LoadQuestItemDescTable();
            LoadItemExchangeTable();
        });
    }

    public static Task LoadItemTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "item_org_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var tableModel = table.Select(value => new Item(
            Convert.ToInt32(value[0]),      // BaseId
            Convert.ToInt32(value[1]),      // ExtensionId
            value[2],                       // Name
            Convert.ToInt32(value[4]),      // ExtBaseIdActive
            Convert.ToInt32(value[10]),     // KindId
            Convert.ToInt32(value[12]),     // SlotId
            Convert.ToInt32(value[13]),     // RaceId
            Convert.ToInt32(value[14]),     // ClassId
            Convert.ToInt32(value[15]),     // Damage
            Convert.ToInt32(value[17]),     // Range
            Convert.ToInt32(value[18]),     // Weight
            Convert.ToInt32(value[19]),     // MaxDurability
            Convert.ToInt32(value[20]),     // SellPrice
            Convert.ToInt32(value[22]),     // Defense
            Convert.ToInt32(value[23]),     // IsCountable
            Convert.ToInt32(value[24]),     // Effect1
            Convert.ToInt32(value[25]),     // Effect2
            Convert.ToInt32(value[26]),     // ReqMinLevel
            Convert.ToInt32(value[28]),     // IsPet
            Convert.ToInt32(value[30]),     // ReqStatStrength
            Convert.ToInt32(value[31]),     // ReqStatHealth
            Convert.ToInt32(value[32]),     // ReqStatDexterity
            Convert.ToInt32(value[33]),     // ReqStatIntellience
            Convert.ToInt32(value[34]),     // ReqStatMagicPower
            Convert.ToInt32(value[35]),     // SellingGroup
            Convert.ToInt32(value[36])      // ItemScrollGrade
            ))
            .OrderBy(x => x.BaseId)
            .ToArray();

        ItemTable.AddRange(tableModel);

        var files = Directory.GetFiles(dataDirectory)
                          .Where(x => x.ToLower().Contains("item") && x.ToLower().Contains("_ext_") && x.ToLower().Contains("_us"))
                          .OrderBy(x => x)
                          .ToArray();

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var data = _tableService.GetList(_tableService.GetTable(fileInfo.FullName));
            var number = Convert.ToInt32(fileInfo.Name.ToLower().Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[2]);

            var model = data.Select(value => new ItemExtension(number,
                        Convert.ToInt32(value[0]),      // ExtensionId
                        value[1],                       // Name
                        Convert.ToInt32(value[2]),      // BaseId
                        Convert.ToInt32(value[7]),      // TypeId
                        Convert.ToInt32(value[8]),      // Damage
                        Convert.ToInt32(value[9]),      // AttackIntervalPercentage
                        Convert.ToInt32(value[10]),     // AttackPowerRate
                        Convert.ToInt32(value[11]),     // DodgeRate
                        Convert.ToInt32(value[12]),     // MaxDurability
                        Convert.ToInt32(value[13]),     // PriceMultiply
                        Convert.ToInt32(value[14]),     // Defense
                        Convert.ToInt32(value[15]),     // DaggerDefense
                        Convert.ToInt32(value[16]),     // JamadarDefense
                        Convert.ToInt32(value[17]),     // SwordDefense
                        Convert.ToInt32(value[18]),     // ClubDefense
                        Convert.ToInt32(value[19]),     // AxeDefense
                        Convert.ToInt32(value[20]),     // SpearDefense
                        Convert.ToInt32(value[21]),     // ArrowDefense
                        Convert.ToInt32(value[22]),     // FireDamage
                        Convert.ToInt32(value[23]),     // GlacierDamage
                        Convert.ToInt32(value[24]),     // LightningDamage
                        Convert.ToInt32(value[25]),     // PosionDamage
                        Convert.ToInt32(value[26]),     // HpRecovery
                        Convert.ToInt32(value[27]),     // MpDamage
                        Convert.ToInt32(value[28]),     // MpRecovery
                        Convert.ToInt32(value[29]),     // ReturnPhysicalDamage
                        Convert.ToInt32(value[31]),     // StrengthBonus
                        Convert.ToInt32(value[32]),     // HealthBonus
                        Convert.ToInt32(value[33]),     // DexterityBonus
                        Convert.ToInt32(value[34]),     // IntellienceBonus
                        Convert.ToInt32(value[35]),     // MagicPowerBonus
                        Convert.ToInt32(value[36]),     // HpBonus
                        Convert.ToInt32(value[37]),     // MpBonus
                        Convert.ToInt32(value[38]),     // ResistanceToFlame
                        Convert.ToInt32(value[39]),     // ResistanceToGlacier
                        Convert.ToInt32(value[40]),     // ResistanceToLightning
                        Convert.ToInt32(value[41]),     // ResistanceToMagic
                        Convert.ToInt32(value[42]),     // ResistanceToPosion
                        Convert.ToInt32(value[43]),     // ResistanceToCurse
                        Convert.ToInt32(value[49]),     // ReqStatStrength
                        Convert.ToInt32(value[50]),     // ReqStatHealth
                        Convert.ToInt32(value[51]),     // ReqStatDexterity
                        Convert.ToInt32(value[52]),     // ReqStatIntellience
                        Convert.ToInt32(value[53])      // ReqStatMagicPower
                        ))
                        .OrderBy(x => x.BaseId)
                        .ToArray();

            ItemExtensionTable.AddRange(model);
        }

        return Task.CompletedTask;
    }

    public static Item GetItemById(int itemId)
    {
        if (itemId < 0) return default!;

        var baseId = itemId / 1000 * 1000;

        var item = ItemTable.FirstOrDefault(x => x.BaseId == baseId);

        if (item != null)
            item.Extension = GetItemExtensionById(item.ExtensionNumber, itemId);

        return item!;
    }

    public static ItemExtension GetItemExtensionById(int number, int itemId)
    {
        var baseId = itemId % 10;

        if (itemId >= 1000000000)
            baseId = itemId % 1000;

        if (baseId == 0)
            return default!;

        return ItemExtensionTable.FirstOrDefault(x => x.Number == number && x.BaseId == baseId)!;
    }

    public static Task LoadSkillTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "skill_magic_main_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new Skill(
            Convert.ToInt32(x[0]),          // Id 
            x[2],                           // Name
            Convert.ToInt32(x[4]),          // SelfAni1 
            Convert.ToInt32(x[7]),          // SelfEffect1 
            Convert.ToInt32(x[8]),          // SelfPart1 
            Convert.ToInt32(x[9]),          // SelfEffect2 
            Convert.ToInt32(x[10]),         // SelfPart2
            Convert.ToInt32(x[11]),         // RequiredFlyEffect 
            Convert.ToInt32(x[14]),         // TargetType 
            Convert.ToInt32(x[15]),         // Point 
            Convert.ToInt32(x[16]),         // ClassBaseId 
            Convert.ToInt32(x[17]),         // Mana 
            Convert.ToInt32(x[20]),         // CastTime 
            Convert.ToInt32(x[21]),         // UseItem 
            Convert.ToInt32(x[22]),         // ReCastTime 
            Convert.ToInt32(x[23]),         // CooldownBase 
            Convert.ToInt32(x[28]),         // Type1
            Convert.ToInt32(x[29]),         // Type2
            Convert.ToInt32(x[30]),         // MaxRange 
            Convert.ToInt32(x[34])          // BaseId
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        SkillTable.AddRange(model);

        var files = Directory.GetFiles(dataDirectory)
            .Where(x =>
            {
                var fileInfo = new FileInfo(x);
                return fileInfo.Name.ToLower().Contains("skill_magic_") && fileInfo.Name.Length <= "skill_magic_99.tbl".Length;
            })
            .OrderBy(x => x)
            .ToArray();

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var data = _tableService.GetList(_tableService.GetTable(fileInfo.FullName));
            var number = Convert.ToInt32(fileInfo.Name.ToLower().Split(new string[] { "_", ".tbl" }, StringSplitOptions.RemoveEmptyEntries)[2]);

            switch (number)
            {
                case 2:
                    SkillExtensionTable.AddRange(data.Select(x => new SkillExtension(number, Convert.ToInt32(x[0]), Convert.ToInt32(x[5]))).ToArray());
                    break;
                case 3:
                    SkillExtensionTable.AddRange(data.Select(x => new SkillExtension(number, Convert.ToInt32(x[0]), Convert.ToInt32(x[2]), Convert.ToInt32(x[3]))).ToArray());
                    break;
                case 4:
                    SkillExtensionTable.AddRange(data.Select(x => new SkillExtension(number, Convert.ToInt32(x[0]), Convert.ToInt32(x[1]), Convert.ToInt32(x[2]), Convert.ToInt32(x[3]))).ToArray());
                    break;
                default:
                    SkillExtensionTable.AddRange(data.Select(x => new SkillExtension(number, Convert.ToInt32(x[0]))).ToArray());
                    break;
            }
        }

        SkillTable.ForEach(x => x.Extension = SkillExtensionTable.FirstOrDefault(y => y.Id == x.Id && y.Number == x.Type1)!);

        LoadCustomSkillTable();

        return Task.CompletedTask;
    }

    private static void LoadCustomSkillTable()
    {
        var godMode = new Skill(500344, "God Mode", -1, 2401, -1, 0, 0, 0, 1, 0, 0, 0, 9, 0, 0, 10, 4, 0, 0, 490006);
        godMode.Extension = new SkillExtension(4, 500344, 1, 0, 3600);
        SkillTable.Add(godMode);

        var hyperNoah = new Skill(500094, "Hyper Noah", -1, 2401, -1, 0, 0, 0, 1, 0, 0, 0, 9, 0, 0, 10, 4, 0, 0, 504004);
        hyperNoah.Extension = new SkillExtension(4, 500094, 169, 0, 7200);
        SkillTable.Add(hyperNoah);

        var drakiBlessing = new Skill(494099, "Draki Blessing", -1, 2401, -1, 0, 0, 0, 1, 0, 0, 0, 9, 0, 0, 10, 4, 0, 0, 511560);
        drakiBlessing.Extension = new SkillExtension(4, 494099, 1, 0, 3600);
        SkillTable.Add(drakiBlessing);


        List<int> classBaseList = new List<int>();
        
        classBaseList.Add(1020);
        classBaseList.Add(1070);
        classBaseList.Add(1080);
        classBaseList.Add(2020);
        classBaseList.Add(2070);
        classBaseList.Add(2080);

        classBaseList.ForEach(x =>
        {
            var superArcher = new Skill(999897, "Super Archer", 26, 500, 12, 0, 0, 532, 7, 0, x, 190, 13, 391010000, 13, 10, 2, 0, 0, 102003);
            superArcher.Extension = new SkillExtension(2, 999897, 8);
            SkillTable.Add(superArcher);
        });

       

    }

    public static List<Skill> GetSkillList()
    {
        return SkillTable;
    }

    public static Task LoadMonsterTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "mob_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new Monster(
            Convert.ToInt32(x[0]),          // Id 
            x[1],                           // Name
            Convert.ToInt32(x[2]),          // ProtoId
            Convert.ToInt32(x[3]),          // Boss 
            Convert.ToInt32(x[5])           // Size 
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        MonsterTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<Monster> GetMonsterList()
    {
        return MonsterTable;
    }

    public static Task LoadNpcTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "k_npc_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new Npc(
            Convert.ToInt32(x[0]),          // Id 
            x[1],                           // Name
            Convert.ToInt32(x[2])           // ProtoId
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        NpcTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<Npc> GetNpcList()
    {
        return NpcTable;
    }

    public static Task LoadPositionTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "npcmopmap_info_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new Position(
            Convert.ToInt32(x[0]),          // Id 
            Convert.ToInt32(x[1]),          // NpcId
            Convert.ToInt32(x[2]),          // Zone
            Convert.ToInt32(x[4]),          // X
            Convert.ToInt32(x[5]),          // Y
            x[6],                           // Name
            Convert.ToInt32(x[7])           // Type
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        PositionTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<Position> GetPositionList()
    {
        return PositionTable;
    }

    public static Task LoadItemSellTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "itemsell_table.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => {

            List<int> itemList = new();

            for (int i = 0; i < 24; i++)
                itemList.Add(Convert.ToInt32(x[i + 2]));

            return new ItemSell(Convert.ToInt32(x[1]), itemList);
        })
        .OrderBy(x => x.SellingGroup)
        .ToArray();

        ItemSellTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<ItemSell> GetItemSellList()
    {
        return ItemSellTable;
    }

    public static ItemSell GetItemSellByGroupId(int sellingGroup)
    {
        return ItemSellTable.FirstOrDefault(x => x.SellingGroup == sellingGroup)!;
    }

    public static Task LoadWarpInfoTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "warpinfo.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new WarpInfo(
            Convert.ToInt32(x[0]),          // WarpId 
            x[1],                           // Name
            x[5]                            // Agrement
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        WarpInfoTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<WarpInfo> GetWarpInfoList()
    {
        return WarpInfoTable;
    }

    public static Task LoadQuestHelperTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "quest_helper.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new QuestHelper(
            Convert.ToInt32(x[0]),      // Base ID
            Convert.ToInt32(x[2]),      // Message Type
            Convert.ToInt32(x[3]),      // Level
            Convert.ToInt32(x[5]),      // Class Id
            Convert.ToInt32(x[6]),      // Nation
            Convert.ToInt32(x[7]),      // Quest Type
            Convert.ToInt32(x[8]),      // Zone
            Convert.ToInt32(x[9]),      // Npc Proto ID
            Convert.ToInt32(x[10]),     // Event Data Index
            Convert.ToInt16(x[11]),     // Event Status
            Convert.ToInt16(x[14]),     // Item Exchange Index
            x[16],                      // Lua Name
            Convert.ToInt32(x[17]),     // Guide Index
            Convert.ToInt32(x[18]),     // Npc Desc Index
            Convert.ToInt32(x[19])      // Quest Solo
            ))
            .OrderBy(x => x.NpcDescIndex)
            .OrderBy(x => x.EventDataIndex)
            .ToArray();

        QuestHelperTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<QuestHelper> GetQuestHelperList()
    {
        return QuestHelperTable;
    }

    public static Task LoadQuestGuideTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "quest_guide_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new QuestGuide(
            Convert.ToInt32(x[0]),      // Id
            Convert.ToInt32(x[1]),      // Min Level
            x[3],                       // Title
            x[4]                        // Description
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        QuestGuideTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<QuestGuide> GetQuestGuideList()
    {
        return QuestGuideTable;
    }

    public static Task LoadQuestMonsterExchangeTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "quest_monster_exchange.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new QuestMonsterExchange(
            Convert.ToInt32(x[0]),      // Id
            new int[,] {                // Target Npc Proto Array
                { Convert.ToInt32(x[1]), Convert.ToInt32(x[5]) },
                { Convert.ToInt32(x[6]), Convert.ToInt32(x[10]) },
                { Convert.ToInt32(x[11]), Convert.ToInt32(x[15]) },
                { Convert.ToInt32(x[16]), Convert.ToInt32(x[20]) }
            }
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        QuestMonsterExchangeTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<QuestMonsterExchange> GetQuestMonsterExchangeList()
    {
        return QuestMonsterExchangeTable;
    }

    public static Task LoadQuestNpcDescTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "quest_npc_desc_us.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new QuestNpcDesc(
            Convert.ToInt32(x[0]),      // Id
            Convert.ToInt32(x[1]),      // Npc Proto Id
            Convert.ToInt32(x[4]),      // Zone
            Convert.ToInt32(x[5]),      // Npc Type
            x[6],                       // Name
            x[9],                       // Description
            Convert.ToInt32(x[10]),     // X
            Convert.ToInt32(x[11])      // Y
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        QuestNpcDescTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<QuestNpcDesc> GetQuestNpcDescList()
    {
        return QuestNpcDescTable;
    }

    public static Task LoadQuestItemDescTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "quest_item_desc.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new QuestItemDesc(
            Convert.ToInt32(x[0]),      // Id
            Convert.ToInt32(x[1]),      // Item Id
            Convert.ToInt32(x[2])       // Npc Desc Index
            ))
            .ToArray();

        QuestItemDescTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<QuestItemDesc> GetQuestItemDescList()
    {
        return QuestItemDescTable;
    }

    public static Task LoadItemExchangeTable()
    {
        string dataDirectory = Directory.GetCurrentDirectory() + "\\data\\table";

        if (!Directory.Exists(dataDirectory))
            return Task.CompletedTask;

        var fileName = Path.Combine(dataDirectory, "item_exchange.tbl");

        if (!File.Exists(fileName))
            return Task.CompletedTask;

        var table = _tableService.GetList(_tableService.GetTable(fileName));

        var model = table.Select(x => new ItemExchange(
            Convert.ToInt32(x[0]),          // Id
            new int[,] {                    // Needed Item Array
                { Convert.ToInt32(x[3]), Convert.ToInt32(x[4]) },
                { Convert.ToInt32(x[5]), Convert.ToInt32(x[6]) },
                { Convert.ToInt32(x[7]), Convert.ToInt32(x[8]) },
                { Convert.ToInt32(x[9]), Convert.ToInt32(x[10]) },
                { Convert.ToInt32(x[11]), Convert.ToInt32(x[12]) }
            },
            new int[,] {                    // Reward Item Array
                { Convert.ToInt32(x[13]), Convert.ToInt32(x[14]) },
                { Convert.ToInt32(x[15]), Convert.ToInt32(x[16]) },
                { Convert.ToInt32(x[17]), Convert.ToInt32(x[18]) },
                { Convert.ToInt32(x[19]), Convert.ToInt32(x[20]) },
                { Convert.ToInt32(x[21]), Convert.ToInt32(x[22]) }
            }
            ))
            .OrderBy(x => x.Id)
            .ToArray();

        ItemExchangeTable.AddRange(model);

        return Task.CompletedTask;
    }

    public static List<ItemExchange> GetItemExchangeList()
    {
        return ItemExchangeTable;
    }

}