namespace KOF.Core.Enums;

public enum SkillMagicTargetType : uint
{
	TARGET_SELF = 1,            
	TARGET_FRIEND_WITHME = 2,    
	TARGET_FRIEND_ONLY = 3,     
	TARGET_PARTY = 4,            
	TARGET_NPC_ONLY = 5,     
	TARGET_PARTY_ALL = 6,        
	TARGET_ENEMY_ONLY = 7, 
	TARGET_ALL = 8,              
	TARGET_AREA_ENEMY = 10,     
	TARGET_AREA_FRIEND = 11,
	TARGET_AREA_ALL = 12,       
	TARGET_AREA = 13,            
	TARGET_DEAD_FRIEND_ONLY = 25,
	TARGET_UNKNOWN = 0xffffffff
}