namespace KOF.Data.Models;

public class Skill
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = default!;
    public int SelfAni1 { get; protected set; }
    public int SelfEffect1 { get; protected set; }
    public int SelfPart1 { get; protected set; }
    public int SelfEffect2 { get; protected set; }
    public int SelfPart2 { get; protected set; }
    public int RequiredFlyEffect { get; protected set; }
    public int TargetType { get; protected set; }
    public int Point { get; protected set; }
    public int ClassBaseId { get; protected set; }
    public int Mana { get; protected set; }
    public int ReCastTime { get; protected set; }
    public int UseItem { get; protected set; }
    public int CastTime { get; protected set; }
    private int _CoolDown { get; set; }
    public int CoolDown { get => (_CoolDown * 100); set => _CoolDown = value; }
    public int MaxRange { get; protected set; }
    public int Type1 { get; protected set; }
    public int Type2 { get; protected set; }
    public int BaseId { get; protected set; }
    public int Mastery { get; protected set; }
    public int SkillNumber { get; protected set; }
    public SkillExtension Extension { get; set; } = default!;
    public long SkillUseTime { get; protected set; }
    public long SkillNextUseTime { get; protected set; }
    public int TargetId { get; protected set; }
    public bool Queued { get; protected set; } = false;

    public Skill(int id, string name, int selfAni1,int selfEffect1, int selfPart1, int selfEffect2, int selfPart2, int requiredFlyEffect, int targetType, int point, int classBaseId, int mana, int reCastTime, int useItem, int castTime, int cooldown, int type1, int type2, int maxRange, int baseId)
    {
        Id = id;
        Name = name;
        SelfAni1 = selfAni1;
        SelfEffect1 = selfEffect1;
        SelfPart1 = selfPart1;
        SelfEffect2 = selfEffect2;
        SelfPart2 = selfPart2;
        RequiredFlyEffect = requiredFlyEffect;
        TargetType = targetType;
        Point = point;
        ClassBaseId = classBaseId;
        Mana = mana;
        CastTime = castTime;
        UseItem = useItem;
        ReCastTime = reCastTime;
        CoolDown = cooldown;
        MaxRange = maxRange;
        Type1 = type1;
        Type2 = type2;
        BaseId = baseId;

        SkillNumber = Id % 1000;
        Mastery = (SkillNumber > 100 ? (SkillNumber - (SkillNumber % 100)) / 100 : 0) - 5;
        Mastery = Mastery < 0 ? 0 : Mastery;
    }

    public Skill Clone()
    {
        return (Skill)this.MemberwiseClone();
    }

    public long GetSkillUseTime()
    {
        return SkillUseTime;
    }

    public void UpdateSkillUseTime(long skillUseTime)
    {
        SkillUseTime = skillUseTime;
    }

    public int GetTarget()
    {
        return TargetId;
    }

    public void SetTarget(int targetId)
    {
        TargetId = targetId;
    }

    public bool IsQueued()
    {
        return Queued;
    }

    public void SetQueued(bool b)
    {
        Queued = b;
    }

}
