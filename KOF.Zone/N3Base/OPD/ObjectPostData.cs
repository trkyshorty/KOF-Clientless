namespace KOF.Zone.N3Base.OPD;

public class ObjectPostData
{

    public N3ShapeManager ShapeManager { get; init; } = default!;

    public ObjectPostData() { ShapeManager = new(); }

    public bool Load(string opdFile, bool obsolete = false)
    {

        using FileStream fs = new(opdFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        using BinaryReader br = new(fs);

        string mapName = DecryptString(br.ReadBytes(br.ReadInt32()));

        if (!obsolete)
            br.ReadBytes(4); // unknown..

        // Load the file
        bool success = ShapeManager.Load(br, obsolete);

        br.Close();

        return success;
    }


    private string DecryptString(byte[] encrypted)
    {

        const ushort CipherKey1 = 0x6081;
        const ushort CipherKey2 = 0x1608;

        ushort _volatileKey = 0x0816;
        for (int i = 0; i < encrypted.Length; i++)
        {
            var rawByte = encrypted[i];
            var tmpKey = (byte)((_volatileKey & 0xFF00) >> 8);
            var encrpytedByte = (byte)(tmpKey ^ rawByte);
            _volatileKey = (ushort)((rawByte + _volatileKey) * CipherKey1 + CipherKey2);
            encrypted[i] = encrpytedByte;
        }

        return System.Text.Encoding.UTF8.GetString(encrypted);
    }

}