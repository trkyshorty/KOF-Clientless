namespace KOF.Data.Models;

public class QuestItemDesc
{
    public int Id { get; protected set; } // 0
    public int ItemId { get; protected set; } // 1
    public int NpcDescIndex { get; protected set; } // 2

    public QuestItemDesc(int id, int itemId, int npcDescIndex)
    {
        Id = id;
        ItemId = itemId;
        NpcDescIndex = npcDescIndex;
    }
}
