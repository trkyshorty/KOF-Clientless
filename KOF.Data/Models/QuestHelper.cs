namespace KOF.Data.Models;

public class QuestHelper
{
    public int BaseId { get; set; } // 0
    public int MessageType { get; protected set; } // 2
    public int Level { get; protected set; } // 3
    public int Class { get; protected set; } // 5
    public int Nation { get; protected set; } // 6
    public int QuestType { get; protected set; } // 7
    public int Zone { get; protected set; } // 8
    public int NpcProtoId { get; protected set; } // 9
    public int EventDataIndex { get; protected set; } // 10
    public int EventStatus { get; protected set; } // 11
    public int ItemExchangeIndex { get; protected set; } // 14
    public string LuaName { get; protected set; } // 16
    public int GuideIndex { get; protected set; } // 17
    public int NpcDescIndex { get; protected set; } // 18
    public int QuestSolo { get; protected set; } // 19

    public QuestHelper(int baseId, int messageType, int level, int classId, int nation, int questType, int zone, int npcProtoId, int eventDataIndex, int eventStatus, int itemExchangeIndex, string luaName, int guideIndex, int npcDescIndex, int questSolo)
    {
        BaseId = baseId;
        MessageType = messageType;
        Level = level;
        Class = classId;
        Nation = nation;
        QuestType = questType;
        Zone = zone;
        NpcProtoId = npcProtoId;
        EventDataIndex = eventDataIndex;
        EventStatus = eventStatus;
        ItemExchangeIndex = itemExchangeIndex;
        LuaName = luaName;
        GuideIndex = guideIndex;
        NpcDescIndex = npcDescIndex;
        QuestSolo = questSolo;
    }
}
