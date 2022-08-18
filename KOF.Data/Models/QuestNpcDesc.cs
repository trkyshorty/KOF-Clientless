namespace KOF.Data.Models;

public class QuestNpcDesc
{
    public int Id { get; protected set; } // 0
    public int NpcProtoId { get; protected set; } // 1
    public int Zone { get; protected set; } // 4
    public int NpcType { get; protected set; } // 5
    public string Name { get; protected set; } // 6
    public string Description { get; protected set; } // 9
    public int X { get; protected set; } // 10
    public int Y { get; protected set; } // 11

    public QuestNpcDesc(int id, int npcProtoId, int zone, int npcType, string name, string description, int x, int y)
    {
        Id = id;
        NpcProtoId = npcProtoId;
        Zone = zone;
        NpcType = npcType;
        Name = name;
        Description = description;
        X = x;
        Y = y;
    }
}
