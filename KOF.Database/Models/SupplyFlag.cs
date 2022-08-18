using SQLite;

namespace KOF.Database.Models;

[Table("SupplyFlag")]
public class SupplyFlag
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public long Id { get; set; }

    [Column("Flag")]
    public int Flag { get; set; }

    [Column("ItemId")]
    public int ItemId { get; set; }
}
