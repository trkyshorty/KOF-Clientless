using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Enums;

public enum StateAction : byte
{
	BASIC = 0,
	ATTACK = 1,
	GUARD = 2,
	STRUCK = 3,  
	DYING = 4,   
	DEATH = 5,   
	SPELLMAGIC = 6, 
	SITDOWN = 7,
}