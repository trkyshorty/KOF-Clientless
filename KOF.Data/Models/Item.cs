using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Data.Models;

public class Item
{
    public int BaseId { get; protected set; } 
    public int ExtensionNumber { get; protected set; }
    private string _Name { get; set; } = default!;
    public string Name { get => Extension != null ? $"{_Name} (+{Extension.BaseId})" : _Name; set => _Name = value; }
    public bool ExtensionBaseIdActive { get; protected set; }
    public int KindId { get; protected set; }
    public int AttachPoint { get; protected set; }
    public int RaceId { get; protected set; }
    public int ClassId { get; protected set; }
    public int Damage { get; protected set; }
    public int Range { get; protected set; }
    public int Weight { get; protected set; }
    private int _Durability { get; set; }
    public int Durability { get => Extension != null ? Extension.Durability + _Durability : _Durability; set => _Durability = value; }
    public int SellPrice { get; protected set; }
    public int Defense { get; protected set; }
    public int IsCountable { get; protected set; }
    public int Effect1 { get; protected set; }
    public int Effect2 { get; protected set; }
    public int ReqMinLevel { get; protected set; }
    public bool IsPet { get; protected set; }
    public int ReqStatStrength { get; protected set; }
    public int ReqStatHealth { get; protected set; }
    public int ReqStatDexterity { get; protected set; } 
    public int ReqStatIntellience { get; protected set; }
    public int ReqStatMagicPower { get; protected set; }
    public int ItemScrollGrade { get; protected set; }
    public ItemExtension Extension { get; set; } = default!;
    public int SellingGroup { get; protected set; }

    public Item(int baseId,
        int itemExtensionNumber,
        string name,
        int extensionBaseIdActiveId,
        int kindId,
        int slotId,
        int raceId,
        int classId,
        int damage,
        int range,
        int weight,
        int durability,
        int sellPrice,
        int defense,
        int isCountable,
        int effect1,
        int effect2,
        int reqMinLevel,
        int isPetId,
        int reqStatStrength,
        int reqStatHealth,
        int reqStatDexterity,
        int reqStatIntellience,
        int reqStatMagicPower,
        int sellingGroup,
        int itemScrollGrade)
    {
        BaseId = baseId;
        ExtensionNumber = itemExtensionNumber;
        Name = name;
        ExtensionBaseIdActive = Convert.ToBoolean(extensionBaseIdActiveId);
        KindId = kindId;
        AttachPoint = slotId;
        RaceId = raceId;
        ClassId = classId;
        Damage = damage;
        Range = range;
        Weight = weight;
        Durability = durability;
        SellPrice = sellPrice;
        Defense = defense;
        IsCountable = isCountable;
        Effect1 = effect1;
        Effect2 = effect2;
        ReqMinLevel = reqMinLevel;
        IsPet = Convert.ToBoolean(isPetId);
        ReqStatStrength = reqStatStrength;
        ReqStatHealth = reqStatHealth;
        ReqStatDexterity = reqStatDexterity;
        ReqStatIntellience = reqStatIntellience;
        ReqStatMagicPower = reqStatMagicPower;
        ItemScrollGrade = itemScrollGrade;
        SellingGroup = sellingGroup;
    }

}
