using SQLite;

namespace KOF.Database.Models;

[Table("Migration")]
public class Migration
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    [Column("File")]
    public string File { get; set; } = "";
}
