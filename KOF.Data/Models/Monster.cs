namespace KOF.Data.Models;

public class Monster
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = default!;
    public int ProtoId { get; protected set; }
    public int Boss { get; protected set; }
    public int Size { get; protected set; }

    public Monster(int id, string name, int protoId, int boss, int size)
    {
        Id = id;
        Name = name;
        ProtoId = protoId;
        Boss = boss;
        Size = size;
    }
}
