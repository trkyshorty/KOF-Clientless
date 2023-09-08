using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using KOF.Core.Enums;

namespace KOF.Core.Models;

public class RouteData
{
    public RouteActionType Action { get; set; }

    public int TargetId { get; set; }

    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public short EventId { get; set; }

    public short ObjectId { get; set; }

    public int NpcId { get; set; }

    public bool NpcEventSend { get; set; }

    public Queue<RouteData> SubQueue = new();

    public bool OnlyPotion { get; set; }
}
