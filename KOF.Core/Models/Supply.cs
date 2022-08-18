using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using KOF.Core.Enums;
using System.ComponentModel;

namespace KOF.Core.Models;

public class Supply
{
    public bool Enable { get; set; }
    [Browsable(false)]
    public int SellingGroup { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; } = "";
    public int Count { get; set; }

    public Supply(bool enable, int sellingGroup, int itemId, string name, int count)
    {
        Enable = enable;
        SellingGroup = sellingGroup;
        ItemId = itemId;
        Name = name;
        Count = count;
    }
}
