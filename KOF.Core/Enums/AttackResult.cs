namespace KOF.Core.Enums;

internal enum AttackResult : byte
{
    ATTACK_FAIL = 0,
    ATTACK_SUCCESS = 1,
    ATTACK_TARGET_DEAD = 2,
    ATTACK_TARGET_DEAD_OK = 3,
    MAGIC_ATTACK_TARGET_DEAD = 4
}