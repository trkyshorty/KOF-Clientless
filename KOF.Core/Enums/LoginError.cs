namespace KOF.Core.Enums;

internal enum LoginError : byte
{
    AUTH_SUCCESS = 1,
    AUTH_NOT_FOUND = 2,
    AUTH_INVALID = 3,
    AUTH_BANNED = 4,
    AUTH_IN_GAME = 5,
    AUTH_ERROR = 6,
    AUTH_AGREEMENT = 15,
    AUTH_OTP = 16,
    AUTH_FAILED = 255
}