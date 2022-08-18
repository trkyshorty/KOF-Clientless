namespace KOF.Core.Enums;

internal enum PartyUpdateType : byte
{
    Create = 1,             // Party Group Create
    Insert = 2,             // Party Insert Permit
    Joined = 3,             // Party Member Insert
    Leave = 4,              // Party Member Remove
    Dismissed = 5,          // Party Group Delete
    HealthManaChange = 6,   // Party Member HPMP change
    LevelChange = 7,        // Party Member Level change
    ClassChange = 8,        // Party Member Class Change
    StatusChange = 9,       // Party Member Status ( disaster, poison ) Change
    Register = 10,          // Party Message Board Register
    Report = 11,            // Party Request Message Board Messages
    Promote = 28,           // Promotes user to party leader
    CommandChange = 30,
    EffectTarget = 31,
    AlertyEnemy = 32
}