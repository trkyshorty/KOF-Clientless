using SQLite;

namespace KOF.Database.Models;

[Table("Server")]
public class Server
{
    [PrimaryKey, AutoIncrement]
    [Column("Id")]
    public long Id { get; set; }

    [Column("Name")]
    public string Name { get; set; } = "";

    [Column("GatewayIp")]
    public string GatewayIp { get; set; } = "";

    [Column("GatewayPort")]
    public ushort GatewayPort { get; set; } = 15100;

    [Column("AgentIp")]
    public string AgentIp { get; set; } = "";

    [Column("AgentPort")]
    public ushort AgentPort { get; set; } = 15001;

    [Column("Platform")]
    public long Platform { get; set; }
}
