namespace KOF.Data.Models;

public class SkillExtension
{
    public int Number { get; protected set; }
    public int Id { get; protected set; }
    public int ArrowCount { get; protected set; }
    public int PotionType { get; protected set; }
    public int PotionValue { get; protected set; }
    public int BuffType { get; protected set; }
    public int AreaRadius { get; protected set; }
    public int BuffDurationBase { get; protected set; }
    public int BuffDuration { get; protected set; }
    public bool IsPotion { get; protected set; }
    public bool IsHealthPotion { get; protected set; }
    public bool IsManaPotion { get; protected set; }

    public SkillExtension(int number, int id)
    {
        Number = number;
        Id = id;
    }

    public SkillExtension(int number, int id, int arrowCount)
    {
        Number = number;
        Id = id;
        ArrowCount = arrowCount;
    }

    public SkillExtension(int number, int id, int potionType, int potionValue)
    {
        Number = number;
        Id = id;
        PotionType = potionType;
        PotionValue = potionValue;

        IsHealthPotion = PotionType == 1;
        IsManaPotion = PotionType == 2;

        IsPotion = IsHealthPotion || IsManaPotion;
    }

    public SkillExtension(int number, int id, int buffType, int areaRadius, int buffDurationBase)
    {
        Number = number;
        Id = id;
        BuffType = buffType;
        AreaRadius = areaRadius;
        BuffDurationBase = buffDurationBase;

        BuffDuration = BuffDurationBase * 100;
    }
}
