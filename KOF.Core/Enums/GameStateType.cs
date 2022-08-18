using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOF.Core.Enums;

public enum GameState : byte
{
    GAME_STATE_NOT_CONNECTED = 0,
    GAME_STATE_CONNECTED = 1,
    GAME_STATE_INGAME = 2
}