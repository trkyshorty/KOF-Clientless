using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Enums;

public enum StateSubPacket : byte
{
	STATE_CHANGE_SITDOWN = 1,
	STATE_CHANGE_RECRUIT_PARTY = 2,
	STATE_CHANGE_SIZE = 3,
	STATE_CHANGE_ACTION = 4,
	STATE_CHANGE_VISIBLE = 5
}