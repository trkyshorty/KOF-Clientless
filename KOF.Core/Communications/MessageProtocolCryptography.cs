namespace KOF.Core.Communications;

/// <summary>
///     Determine what options are enabled in the Cryptography.
/// </summary>
[Flags]
public enum MessageProtocolCryptography : byte
{
    /// <summary>
    ///     Options are disabled for Cryptography.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Indicates that JvCryption are enabled for encryption.
    /// </summary>
    JvCryption = 1,

    /// <summary>
    ///     Indicates that AES are enabled for encryption.
    /// </summary>
    AES = 2
}