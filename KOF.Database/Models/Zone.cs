using SQLite;

namespace KOF.Database.Models;

[Table("Zone")]
public class Zone
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; } = "";

    [Column("Image")]
    public string Image { get; set; } = "";

    [Column("ImageBig")]
    public string ImageBig { get; set; } = "";

    [Column("Smd")]
    public string Smd { get; set; } = "";

    [Column("Opd")]
    public string Opd { get; set; } = "";

    [Column("Gtd")]
    public string Gtd { get; set; } = "";
}
