namespace KOF.Data.Models;

public class ItemSell
{
    public int SellingGroup { get; protected set; }

    public List<int> ItemList { get; protected set; }

    public ItemSell(int sellingGroup, List<int> itemList)
    {
        SellingGroup = sellingGroup;
        ItemList = itemList;
    }
}
