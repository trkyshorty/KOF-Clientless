namespace KOF.Data.Models;

public class Npc
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = default!;
    public int ProtoId { get; protected set; }

    public Npc(int id, string name, int protoId)
    {
        Id = id;
        Name = name;
        ProtoId = protoId;
    }
}
