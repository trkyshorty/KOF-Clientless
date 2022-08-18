using SQLite;

namespace KOF.Database.Models;

[Table("Route")]
public class Route
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; } = "";

    [Column("Zone")]
    public int Zone { get; set; }

    [Column("Data")]
    public string Data { get; set; } = "[]";

}
