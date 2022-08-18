using SQLite;
using System.ComponentModel;
using System.Text.Json;

namespace KOF.Database.Models;

[Table("Account")]
public class Account
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public long Id { get; set; }

    [Column("Login")]
    public string Login { get; set; } = default!;

    [Browsable(false)]
    [Column("Password")]
    public string Password { get; set; } = default!;

    [Column("Server")]
    public string Server { get; set; } = default!;

    [Browsable(false)]
    [Column("NationId")]
    public byte NationId { get; set; }

    [Browsable(false)]
    [Column("Character")]
    public string Character { get; set; } = "";

    [Browsable(false)]
    [Column("CharacterData")]
    public string CharacterData { get; set; } = "[]";

    [Browsable(false)]
    [Column("GroupColor")]
    public string GroupColor { get; set; } = "#FFFFFF";

    public string Nation
    {
        get
        {
            return NationId == 2 ? "El Morad" : "Karus";
        }
    }
    public int Level { get; set; }
    public string Job { get; set; } = "";
    public string State { get; set; } = "Offline";


}
