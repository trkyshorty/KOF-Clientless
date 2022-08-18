using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Enums;

internal enum FriendType : byte
{
    FRIEND_REQUEST = 1,
    FRIEND_REPORT = 2,
    FRIEND_ADD = 3,
    FRIEND_REMOVE = 4
};