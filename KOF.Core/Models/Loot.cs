using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Models
{
    public class Loot
    {
        public uint BundleId { get; set; }
        public int NpcId { get; set; }
        public Vector3 Position { get; set; }
        public int DropTime { get; set; }
        public byte ItemCount { get; set; }
        public bool Opened { get; set; }
    }
}
