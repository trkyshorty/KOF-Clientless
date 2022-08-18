using System.Security.Cryptography;

namespace KOF.Cryptography;

/// <summary>
///     Implication of the AES-128-CBC-PKCS7 algorithm.
/// </summary>
public class AES : IDisposable {
    private readonly byte[] InitialVector   = { 0x32, 0x4E, 0xAA, 0x58, 0xBC, 0xB3, 0xAE, 0xE3, 0x6B, 0xC7, 0x4C, 0x56, 0x36, 0x47, 0x34, 0xF2 };
    private readonly byte[] PrivateKey      = { 0x87, 0x1F, 0xE5, 0x23, 0x78, 0xA1, 0x88, 0xAD, 0x22, 0xCF, 0x5E, 0xAA, 0x5B, 0x18, 0x1E, 0x67 };

    /// <summary>
    ///     Algorithm that used for encrypting and decrypting data
    /// </summary>
    private readonly SymmetricAlgorithm Algorithm = null!;

    /// <summary>
    /// Public (shared) Key.
    /// </summary>
    private byte[] PublicKey = null!;

    /// <summary>
    /// </summary>
    /// <param name="algorithm">algorithm used for encryption, Aes if null</param>
    /// <param name="key">key used for encryption.</param>
    /// <exception cref="ArgumentException">can't create AES: key is invalid</exception>
    public AES(byte[] key = null!) {
        ArgumentNullException.ThrowIfNull(key, $"{nameof(key)} is null");

        Algorithm = Aes.Create("AesManaged")!;
        if (Algorithm.ValidKeySize(key.Length * 8))
            Algorithm.Key = PublicKey = key;
        else
            throw new ArgumentException("Can't create AES: key is invalid.");
    }

    ~AES() {
        Dispose(false);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing) {
        ReleaseUnmanagedResources();
        if (disposing)
            Algorithm.Dispose();
    }

    private void ReleaseUnmanagedResources() {
        PublicKey = null!;
    }

    /// <summary>
    /// Encrypt a byte[]
    /// </summary>
    /// <param name="data"></param>
    /// <returns>encrypted data</returns>
    /// <exception cref="ArgumentNullException">can't encrypt data: data is null</exception>
    public Span<byte> Encrypt(Span<byte> data) {
        ArgumentNullException.ThrowIfNull(data.ToArray(), $"{nameof(data)} is null");

        using var ms = new MemoryStream();
        using var encrpyter = Algorithm.CreateEncryptor(Algorithm.Key,InitialVector);
        using var cs = new CryptoStream(ms,encrpyter,CryptoStreamMode.Write);
        cs.Write(data.ToArray(), 0, data.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    /// <summary>
    /// Decrypt a byte[]
    /// </summary>
    /// <param name="data"></param>
    /// <param name="flag"><see cref="PrivateKey"/> the second private key.</param>
    /// <returns>decrypted data</returns>
    /// <exception cref="ArgumentException">can't decrypt data: data is invalid</exception>
    public Span<byte> Decrypt(Span<byte> data, bool flag = false) {
        if (data == null || data.Length <= 4)
            throw new ArgumentException("Can't decrypt data: data is invalid");

        using var ms = new MemoryStream();
        using var decrypter = Algorithm.CreateDecryptor(flag ? PrivateKey : PublicKey, InitialVector);
        using var cs = new CryptoStream(ms, decrypter, CryptoStreamMode.Write);
        cs.Write(data.ToArray(), 0, data.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    /// <summary>
    /// Return the current key
    /// </summary>
    /// <returns>encryption key</returns>
    public byte[] GetKey()
        => Algorithm.Key;
}