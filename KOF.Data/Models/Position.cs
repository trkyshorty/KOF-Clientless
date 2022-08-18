namespace KOF.Data.Models;

public class Position
{
    public int Id { get; protected set; }
    public int NpcId { get; protected set; }
    public int Zone { get; protected set; }
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public string Name { get; protected set; } = default!;
    public int Type { get; protected set; }

    public Position(int id, int npcId, int zone, int x, int y, string name, int type)
    {
        Id = id;
        NpcId = npcId;
        Zone = zone;
        X = x;
        Y = y;
        Name = name;
        Type = type;
    }
}
