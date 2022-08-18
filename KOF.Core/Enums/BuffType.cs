namespace KOF.Core.Enums;

internal enum BuffType : byte
{
	BUFF_TYPE_NONE = 0,
	BUFF_TYPE_HP_MP = 1,
	BUFF_TYPE_AC = 2,
	BUFF_TYPE_SIZE = 3,
	BUFF_TYPE_DAMAGE = 4,
	BUFF_TYPE_ATTACK_SPEED = 5,
	BUFF_TYPE_SPEED = 6,
	BUFF_TYPE_STATS = 7,
	BUFF_TYPE_RESISTANCES = 8,
	BUFF_TYPE_ACCURACY = 9,
	BUFF_TYPE_MAGIC_POWER = 10,
	BUFF_TYPE_EXPERIENCE = 11,
	BUFF_TYPE_WEIGHT = 12,
	BUFF_TYPE_WEAPON_DAMAGE = 13,
	BUFF_TYPE_WEAPON_AC = 14,
	BUFF_TYPE_LOYALTY = 15,
	BUFF_TYPE_NOAH_BONUS = 16,
	BUFF_TYPE_PREMIUM_MERCHANT = 17,
	BUFF_TYPE_ATTACK_SPEED_ARMOR = 18,  // Berserker
	BUFF_TYPE_DAMAGE_DOUBLE = 19,  // Critical Point
	BUFF_TYPE_DISABLE_TARGETING = 20,  // Smoke Screen / Light Shock
	BUFF_TYPE_BLIND = 21,  // Blinding (Strafe)
	BUFF_TYPE_FREEZE = 22,  // Freezing Distance
	BUFF_TYPE_INSTANT_MAGIC = 23,  // Instantly Magic
	BUFF_TYPE_DECREASE_RESIST = 24,  // Minor resist
	BUFF_TYPE_MAGE_ARMOR = 25,  // Fire / Ice / Lightning Armor
	BUFF_TYPE_PROHIBIT_INVIS = 26,  // Source Marking
	BUFF_TYPE_RESIS_AND_MAGIC_DMG = 27,  // Elysian Web
	BUFF_TYPE_TRIPLEAC_HALFSPEED = 28,  // Wall of Iron
	BUFF_TYPE_BLOCK_CURSE = 29,  // Counter Curse
	BUFF_TYPE_BLOCK_CURSE_REFLECT = 30,  // Curse Refraction
	BUFF_TYPE_MANA_ABSORB = 31,  // Outrage / Frenzy
	BUFF_TYPE_IGNORE_WEAPON = 32,  // Weapon cancellation
	BUFF_TYPE_VARIOUS_EFFECTS = 33,  // ... whatever the event item grants.
	BUFF_TYPE_PASSION_OF_SOUL = 35,  // Passion of the Soul
	BUFF_TYPE_FIRM_DETERMINATION = 36,  // Firm Determination
	BUFF_TYPE_SPEED2 = 40,  // Cold Wave
	BUFF_TYPE_UNK_EXPERIENCE = 42,  // unknown buff type, used for something relating to XP.
	BUFF_TYPE_ATTACK_RANGE_ARMOR = 43,  // Inevitable Murderous
	BUFF_TYPE_MIRROR_DAMAGE_PARTY = 44,  // Minak's Thorn
	BUFF_TYPE_DAGGER_BOW_DEFENSE = 45,  // Eskrima
	BUFF_TYPE_STUN = 47,  // Lighting Skill 
	BUFF_TYPE_LOYALTY_AMOUNT = 55,  // Santa's Present
	BUFF_TYPE_NO_RECALL = 150, // "Cannot use against the ones to be summoned"
	BUFF_TYPE_REDUCE_TARGET = 151, // "Reduction" (reduces target's stats, but enlarges their character to make them easier to attack)
	BUFF_TYPE_SILENCE_TARGET = 152, // Silences the target to prevent them from using any skills (or potions)
	BUFF_TYPE_NO_POTIONS = 153, // "No Potion" prevents target from using potions.
	BUFF_TYPE_KAUL_TRANSFORMATION = 154, // Transforms the target into a Kaul (a pig thing), preventing you from /town'ing or attacking, but increases defense.
	BUFF_TYPE_UNDEAD = 155, // User becomes undead, increasing defense but preventing the use of potions and converting all health received into damage.
	BUFF_TYPE_UNSIGHT = 156, // Blocks the caster's sight (not the target's).
	BUFF_TYPE_BLOCK_PHYSICAL_DAMAGE = 157, // Blocks all physical damage.
	BUFF_TYPE_BLOCK_MAGICAL_DAMAGE = 158, // Blocks all magical/skill damage.
	BUFF_TYPE_UNK_POTION = 159, // unknown potion, "Return of the Warrior", "Comeback potion", perhaps some sort of revive?
	BUFF_TYPE_SLEEP = 160, // Zantman (Sandman)
	BUFF_TYPE_INVISIBILITY_POTION = 163, // "Unidentified potion"
	BUFF_TYPE_GODS_BLESSING = 164, // Increases your defense/max HP 
	BUFF_TYPE_HELP_COMPENSATION = 165, // Compensation for using the help system (to help, ask for help, both?)

	BUFF_TYPE_HYPER_NOAH = 169, //Hyper Noah
}