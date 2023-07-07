using KOF.Data.Models;
using System.ComponentModel;

namespace KOF.Core.Models;

public class Inventory
{
    public void CopyTo(Inventory target)
    {
        target.ItemID = ItemID;
        target.Durability = Durability;
        target.Count = Count;
        target.Flag = Flag;
        target.RentalTime = RentalTime;
        target.Serial = Serial;
        target.Table = Table;
        target.ExpirationTime = ExpirationTime;
        target.SupplyFlag = SupplyFlag;
    }

    public byte Pos { get; set; }

    [Browsable(false)]
    public Item Table { get; set; } = default!;

    public uint ItemID { get; set; }
    public string Name => Table?.Name!;
    public ushort Durability { get; set; }
    public int? MaxDurability => Table?.Durability!;
    public ushort Count { get; set; }

    [Browsable(false)]
    public byte Flag { get; set; }

    [Browsable(false)]
    public short RentalTime { get; set; }

    [Browsable(false)]
    public uint Serial { get; set; }

    [Browsable(false)]
    public uint ExpirationTime { get; set; }

    public bool IsEmpty() => ItemID == 0;


    [Browsable(false)]
    public byte SupplyFlag { get; set; }


    public void Reset()
    {
        ItemID = 0;
        Durability = 0;
        Count = 0;
        Flag = 0;
        RentalTime = 0;
        Serial = 0;
        ExpirationTime = 0;
        Table = default!;
    }

    public override string ToString()
    {
        return ItemID.ToString();
    }

}