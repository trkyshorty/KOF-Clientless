using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Models
{
    public class Config
    {
        public const int USER_BAND = 0;
        public const int NPC_BAND = 5000;
        public const int INVALID_BAND = 30000;

        public const uint ITEM_GOLD = 900_000_000;
        public const uint KAUL_ITEM = 610_001_000;
        public const uint ITEM_CYPHER_RING = 800_112_000;

        public const int COSP_MAX = 9;              // 9 cospre slots
        public const int SLOT_MAX = 14;             // 14 equipped item slots
        public const int HAVE_MAX = 28;             // 28 inventory slots
        public const int MBAG_COUNT = 2;            // 2 magic bag slots
        public const int MBAG_MAX = 12;             // 12 slots per magic bag

        public const int VIP_HAVE_MAX = 48;         // 48 Vip inventory slots
        public const int WAREHOUSE_MAX = 192;       // warehouse slots
        public const int MAX_MERCH_ITEMS = 12;      // merchant slots

        public const int INVENTORY_MBAG = SLOT_MAX + HAVE_MAX + COSP_MAX;   // 14 + 28 + 9 = 51
        public const int INVENTORY_MBAG1 = INVENTORY_MBAG;
        public const int INVENTORY_MBAG2 = INVENTORY_MBAG + MBAG_MAX;       // 51 + 12 = 63
        public const int INVENTORY_TOTAL = INVENTORY_MBAG2 + MBAG_MAX;      // 63 + 12 = 75

        public const int MAX_ITEM_BUNDLE_DROP_PIECE = 6;
    }
}
