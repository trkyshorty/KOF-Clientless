namespace KOF.Core.Models
{
    public class ShopItem
    {
        public byte Pos { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public short Count { get; set; } = 1;
    }
}
