namespace KOF.Zone.N3Base.GTD;
public class TerrainData
{

    private float[,] Heights { get; set; } = null!;
    private int HeightMapSize { get; set; }
    private float UnitDistance { get; set; }

    public int GetHeigthMapSize() => HeightMapSize;

    public bool Load(string gtdFile)
    {
        using FileStream fs = new(gtdFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        using BinaryReader br = new(fs);

        LoadFromStream(br, true);

        br.Close();

        return true;
    }

    public bool LoadFromStream(BinaryReader br, bool readingGtd = false)
    {

        if (readingGtd)
        {
            int nameLength = br.ReadInt32();
            dynamic unknownValue = null!;

            string mapName = "";
            if (nameLength > 2)
            { // new file format
                mapName = DecryptString(br.ReadBytes(nameLength));
                unknownValue = br.ReadBytes(4);
            }
            else
            { // old file format
                mapName = DecryptString(br.ReadBytes(nameLength));
            }
        }

        HeightMapSize = br.ReadInt32();
        if (!readingGtd)
            UnitDistance = br.ReadSingle();

        Heights = new float[HeightMapSize, HeightMapSize];
        for (int z = 0; z < HeightMapSize; z++)
        {
            for (int x = 0; x < HeightMapSize; x++)
            {

                Heights[z, x] = br.ReadSingle();

                if (readingGtd)
                    br.BaseStream.Seek(4, SeekOrigin.Current);

            }
        }

        return true;
    }

    public float GetHeight(float fX, float fZ)
    {
        if (fX >= HeightMapSize || fZ >= HeightMapSize || fX < 0 || fZ < 0)
            return 0.0f;

        return Heights[(int)fX, (int)fZ];
    }

    public bool MakeMoveTable(short[,] events)
    {
        const float NOTMOVE_HEIGHT = 3.0f;

        float Max = 0.0f;
        float Min = 0.0f;

        for (int x = 0; x < HeightMapSize - 1; x++)
        {
            for (int z = 0; z < HeightMapSize - 1; z++)
            {

                Max = float.MinValue;
                Min = float.MaxValue;

                if (Max < Heights[x, z])
                    Max = Heights[x, z];
                if (Min > Heights[x, z])
                    Min = Heights[x, z];

                if (Max < Heights[x, z + 1])
                    Max = Heights[x, z + 1];
                if (Min > Heights[x, z + 1])
                    Min = Heights[x, z + 1];

                if (Max < Heights[x + 1, z])
                    Max = Heights[x + 1, z];
                if (Min > Heights[x + 1, z])
                    Min = Heights[x + 1, z];

                if (Max < Heights[x + 1, z + 1])
                    Max = Heights[x + 1, z + 1];
                if (Min > Heights[x + 1, z + 1])
                    Min = Heights[x + 1, z + 1];

                if (Math.Abs(Max - Min) >= NOTMOVE_HEIGHT)
                {
                    events[x, z] = 0;
                }
            }
        }

        return false;
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
