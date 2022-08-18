using SQLite;

namespace KOF.Database.Models;

[Table("Control")]
public class Control
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Server")]
    public string Server { get; set; } = default!;

    [Column("Form")]
    public string Form { get; set; } = default!;

    [Column("Name")]
    public string Name { get; set; } = default!;

    [Column("Value")]
    public string Value { get; set; } = default!;
}
