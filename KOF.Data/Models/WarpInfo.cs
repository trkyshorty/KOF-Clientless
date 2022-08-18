using System.ComponentModel;
using System.Numerics;

namespace KOF.Data.Models;

public class WarpInfo
{
    [Browsable(false)]
    public int Id { get; protected set; }
    [Browsable(false)]
    public int ObjectId { get; set; }
    public string Name { get; protected set; } = default!;
    [Browsable(false)]
    public string Agreement { get; protected set; } = default!;
    [Browsable(false)]
    public int Zone { get; set; }
    [Browsable(false)]
    public int MaxUser { get; set; }
    public int Gold { get; set; }

    public WarpInfo(int id, string name, string agreement)
    {
        Id = id;
        Name = name;
        Agreement = agreement;
    }
}
