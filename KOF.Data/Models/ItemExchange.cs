namespace KOF.Data.Models;

public class ItemExchange
{
    public int Id { get; protected set; } // 0

    public int[,] NeededItem { get; protected set; }

    public int[,] RewardItem { get; protected set; }

    public ItemExchange(int id, int[,] neededItem, int[,] rewardItem)
    {
        Id = id;
        NeededItem = neededItem;
        RewardItem = rewardItem;
    }
}
