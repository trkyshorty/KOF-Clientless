namespace KOF.Data.Models;

public class QuestMonsterExchange
{
    public int Id { get; protected set; } // 0
    public int[,] TargetNpcProto { get; protected set; }

    public QuestMonsterExchange(int id, int[,] targetNpcProto)
    {
        Id = id;
        TargetNpcProto = targetNpcProto;
    }
}
