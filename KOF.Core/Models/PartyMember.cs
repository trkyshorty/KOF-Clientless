using KOF.Core.Communications;
using KOF.Core.Handlers;
using System.ComponentModel;

namespace KOF.Core.Models;
public class PartyMember
{
    [Browsable(false)]
    public short MemberId { get; set; }
    [Browsable(false)]
    public byte Index { get; set; }
    public string Name { get; set; } = "";
    [Browsable(false)]
    public ushort MaxHealth { get; set; }
    [Browsable(false)]
    public ushort Health { get; set; }
    public string Hp { get { return $"{Math.Ceiling((Health * 100) / (float)MaxHealth)}%"; } }
    public string Mp { get { return $"{Math.Ceiling((Mana * 100) / (float)MaxMana)}%"; } }
    public byte Level { get; set; }
    [Browsable(false)]
    public ushort Class { get; set; }
    public string Job { get { return $"{Character.GetRepresentClassName(Class)}"; } }
    [Browsable(false)]
    public ushort MaxMana { get; set; }
    [Browsable(false)]
    public ushort Mana { get; set; }
    [Browsable(false)]
    public byte NationId { get; set; }
    public string Nation { get { return $"{Character.GetNationName(NationId)}"; } }

    [Browsable(false)]
    public byte LeaderRank { get; set; } // maybe..

    /*
           1 , 0 = Cure damage over time
           1 , 1 = Damage over time
           2 , 0 = Cure poison
           2 , 1 = poison (purple)
           3 , 0 = Cure disease
           3 , 1 = disease (green)
           4 , 1 = blind
           5 , 0 = Cure grey HP
           5 , 1 = HP is grey (not sure what this is)
    */
    [Browsable(false)]
    public byte PlayerStatus { get; set; }
    [Browsable(false)]
    public byte PlayerStatusBehavior { get; set; }

    public static PartyMember FromMessage(Message msg) => new()
    {
        Unknown1 = msg.Read<short>(),
        MemberId = msg.Read<short>(),
        Index = msg.Read<byte>(),
        Name = msg.Read(true, "gb2312"),
        MaxHealth = msg.Read<ushort>(),
        Health = msg.Read<ushort>(),
        Level = msg.Read<byte>(),
        Class = msg.Read<ushort>(),
        MaxMana = msg.Read<ushort>(),
        Mana = msg.Read<ushort>(),
        NationId = msg.Read<byte>(),
        LeaderRank = msg.Read<byte>()



    };

    [Browsable(false)]
    public short Unknown1 { get; set; }

    public override string ToString() => Name!;
}