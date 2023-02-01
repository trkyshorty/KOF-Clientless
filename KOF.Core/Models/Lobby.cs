using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Models;

public class Lobby
{
    public byte Slot { get; set; }
    public string Name { get; set; } = default!;
    public byte Race { get; set; }
    public ushort Class { get; set; }
    public byte Level { get; set; }
    public byte Face { get; set; }
    public byte Hair { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte Zone { get; set; }
    public byte Unknown1 { get; set; }
    public Inventory[] VisibleEquipment { get; set; } = null!;

    public override string ToString()
        => Name;
}
