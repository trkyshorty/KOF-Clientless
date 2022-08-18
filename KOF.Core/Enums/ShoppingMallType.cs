using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Enums;

internal enum ShoppingMallType : byte
{
    STORE_OPEN = 1,
    STORE_CLOSE = 2,
    STORE_BUY = 3,
    STORE_MINI = 4,
    STORE_PROCESS = 5,
    STORE_LETTER = 6
}
