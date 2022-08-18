using System;
using System.Collections.Generic;
namespace KOF.Data.Models;

public class ItemExtension
{
    public int Number { get; protected set; }
    public int BaseId { get; protected set; } 
    public string Name { get; protected set; }
    public int ItemBaseId { get; protected set; }
    public int TypeId { get; protected set; }
    public int Damage { get; protected set; }
    public int AttackIntervalPercentage { get; protected set; }
    public int AttackPowerRate { get; protected set; }
    public int DodgeRate { get; protected set; }
    public int Durability { get; protected set; }
    public int PriceMultiply { get; protected set; }
    public int Defense { get; protected set; }
    public int DaggerDefense { get; protected set; }
    public int JamadarDefense { get; protected set; }
    public int SwordDefense { get; protected set; }
    public int ClubDefense { get; protected set; }
    public int AxeDefense { get; protected set; }
    public int SpearDefense { get; protected set; }
    public int ArrowDefense { get; protected set; }
    public int FireDamage { get; protected set; }
    public int GlacierDamage { get; protected set; }
    public int LightningDamage { get; protected set; }
    public int PosionDamage { get; protected set; }
    public int HpRecovery { get; protected set; }
    public int MpDamage { get; protected set; }
    public int MpRecovery { get; protected set; }
    public int ReturnPhysicalDamage { get; protected set; }
    public int StrengthBonus { get; protected set; }
    public int HealthBonus { get; protected set; }
    public int DexterityBonus { get; protected set; }
    public int IntellienceBonus { get; protected set; }
    public int MagicPowerBonus { get; protected set; }
    public int HpBonus { get; protected set; }
    public int MpBonus { get; protected set; }
    public int ResistanceToFlame { get; protected set; }
    public int ResistanceToGlacier { get; protected set; }
    public int ResistanceToLightning { get; protected set; }
    public int ResistanceToMagic { get; protected set; }
    public int ResistanceToPosion { get; protected set; }
    public int ResistanceToCurse { get; protected set; }
    public int ReqStatStrength { get; protected set; }
    public int ReqStatHealth { get; protected set; }
    public int ReqStatDexterity { get; protected set; }
    public int ReqStatIntellience { get; protected set; }
    public int ReqStatMagicPower { get; protected set; }

    public ItemExtension(int number,
        int baseId,
        string name,
        int itemBaseId,
        int typeId,
        int damage,
        int attackIntervalPercentage,
        int attackPowerRate,
        int dodgeRate,
        int durability,
        int priceMultiply,
        int defense,
        int daggerDefense,
        int jamadarDefense,
        int swordDefense,
        int clubDefense,
        int axeDefense,
        int spearDefense,
        int arrowDefense,
        int fireDamage,
        int glacierDamage,
        int lightningDamage,
        int posionDamage,
        int hpRecovery,
        int mpDamage,
        int mpRecovery,
        int returnPhysicalDamage,
        int strengthBonus,
        int healthBonus,
        int dexterityBonus,
        int intellienceBonus,
        int magicPowerBonus,
        int hpBonus,
        int mpBonus,
        int resistanceToFlame,
        int resistanceToGlacier,
        int resistanceToLightning,
        int resistanceToMagic,
        int resistanceToPosion,
        int resistanceToCurse,
        int reqStatStrength,
        int reqStatHealth,
        int reqStatDexterity,
        int reqStatIntellience,
        int reqStatMagicPower)
    {
        Number = number;
        BaseId = baseId;
        Name = name;
        ItemBaseId = itemBaseId;
        TypeId = typeId;
        Damage = damage;
        AttackIntervalPercentage = attackIntervalPercentage;
        AttackPowerRate = attackPowerRate;
        DodgeRate = dodgeRate;
        Durability = durability;
        PriceMultiply = priceMultiply;
        Defense = defense;
        DaggerDefense = daggerDefense;
        JamadarDefense = jamadarDefense;
        SwordDefense = swordDefense;
        ClubDefense = clubDefense;
        AxeDefense = axeDefense;
        SpearDefense = spearDefense;
        ArrowDefense = arrowDefense;
        FireDamage = fireDamage;
        GlacierDamage = glacierDamage;
        LightningDamage = lightningDamage;
        PosionDamage = posionDamage;
        HpRecovery = hpRecovery;
        MpDamage = mpDamage;
        MpRecovery = mpRecovery;
        ReturnPhysicalDamage = returnPhysicalDamage;
        StrengthBonus = strengthBonus;
        HealthBonus = healthBonus;
        DexterityBonus = dexterityBonus;
        IntellienceBonus = intellienceBonus;
        MagicPowerBonus = magicPowerBonus;
        HpBonus = hpBonus;
        MpBonus = mpBonus;
        ResistanceToFlame = resistanceToFlame;
        ResistanceToGlacier = resistanceToGlacier;
        ResistanceToLightning = resistanceToLightning;
        ResistanceToMagic = resistanceToMagic;
        ResistanceToPosion = resistanceToPosion;
        ResistanceToCurse = resistanceToCurse;
        ReqStatStrength = reqStatStrength;
        ReqStatHealth = reqStatHealth;
        ReqStatDexterity = reqStatDexterity;
        ReqStatIntellience = reqStatIntellience;
        ReqStatMagicPower = reqStatMagicPower;
    }

    public ItemExtType GetExtensionType()
    {
        return (ItemExtType)TypeId;
    }
}

public enum ItemExtType : byte
{
    Normal = 0,
    Magic = 1,
    Rare = 2,
    Craft = 3,
    Unique = 4,
    Upgrade = 5,
    Event = 6,
    Pet = 7,
    Cospre = 8,
    Minevra = 9,
    Rebith = 11,
    Reverse = 12,
}
