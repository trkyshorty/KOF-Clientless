using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Models
{
    public class Clan
    {
        public ushort Index { get; set; }
        public byte Flag { get; set; } 
        public byte Nation { get; set; } 
        public byte Grade { get; set; }
        public byte Ranking { get; set; }
        public string Name { get; set; } = default!;
        public string Chief { get; set; } = default!;
        public string ViceChief_1 { get; set; } = default!;
        public string ViceChief_2 { get; set; } = default!;
        public string ViceChief_3 { get; set; } = default!;
        public string ClanNotice { get; set; } = default!;
        public ulong Money { get; set; }
        public ushort Domination { get; set; }
        public uint ClanPointFund { get; set; }
        public ushort MarkVersion { get; set; }
        public ushort MarkLen { get; set; }
        public ushort Cape { get; set; }
        public byte CapeR { get; set; }
        public byte CapeG { get; set; }
        public byte CapeB { get; set; }
        public ushort Alliance { get; set; }
        public ushort AllianceReq { get; set; }
        public byte ClanPointMethod { get; set; }
        public byte SiegeFlag { get; set; }
        public ushort Lose { get; set; }
        public ushort Victory { get; set; }
        public bool IsInAlliance() => Alliance > 0;

        public dynamic UnknownValueA { get; set; }
        public dynamic UnknownValueB { get; set; }
        public dynamic UnknownValueC { get; set; }
    }
}
