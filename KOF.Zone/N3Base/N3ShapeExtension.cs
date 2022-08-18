using System.Numerics;

namespace KOF.Zone.N3Base;

public class N3ShapeExtension
{

    public Dictionary<string, string[]> PartyMap { get; set; } = new();

    private long Offset { get; set; }
    private string Name { get; set; } = null!;
    private Vector3 Position { get; set; }
    private Quaternion Rot { get; set; }
    private Vector3 Scale { get; set; }

    private int Belong { get; set; }
    private int EventId { get; set; }
    private int EventType { get; set; }
    private int NpcId { get; set; }
    private int NpcStatus { get; set; }

    public Dictionary<string, string[]> GetPartyMap => PartyMap;

    public long GetOffset => Offset;
    public string GetName => Name;
    public Vector3 GetPosition => Position;
    public Quaternion GetRot => Rot;
    public Vector3 GetScale => Scale;

    public int GetBelong => Belong;
    public int GetEventId => EventId;
    public int GetEventType => EventType;
    public int GetNpcId => NpcId;
    public int GetNpcStatus => NpcStatus;

    public void Load(BinaryReader br)
    {
        const int KEY_VECTOR = 0;
        const int KEY_QUATERNION = 1;

        Offset = br.BaseStream.Position;

        Name = System.Text.Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt32()));

        Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Rot = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Scale = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

        for (int i = 0; i < 3; i++)
        {

            uint count = br.ReadUInt32();
            if (count > 0)
            {

                uint type = br.ReadUInt32();
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                float samplingRate = br.ReadSingle();
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                if (type == KEY_VECTOR)
                {
                    Vector3[] vectors = new Vector3[count];
                    for (int j = 0; j < count; j++)
                    {

                        vectors[j].X = br.ReadSingle();
                        vectors[j].Y = br.ReadSingle();
                        vectors[j].Z = br.ReadSingle();
                    }
                }
                else if (type == KEY_QUATERNION)
                {
                    Quaternion[] quaternions = new Quaternion[count];
                    for (int k = 0; k < count; k++)
                    {

                        quaternions[k].X = br.ReadSingle();
                        quaternions[k].Y = br.ReadSingle();
                        quaternions[k].Z = br.ReadSingle();
                        quaternions[k].W = br.ReadSingle();
                    }
                }

            }
        }

        var LengthMeshFile1 = br.ReadInt32();
        if (LengthMeshFile1 > 0)
        { // read mesh fileName 1
            var mesh1 = System.Text.Encoding.UTF8.GetString(br.ReadBytes(LengthMeshFile1));
        }

        var LengthMeshFile2 = br.ReadInt32();
        if (LengthMeshFile2 > 0)
        { // Transform collision Mesh fileName 2
            var mesh2 = System.Text.Encoding.UTF8.GetString(br.ReadBytes(LengthMeshFile2));
        }

        int partCount = br.ReadInt32();
        for (int i = 0; i < partCount; i++)
        {

            // SUB Step 1: read pivot
            Vector3 pivot = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            // SUB Step 2: Read mesh name
            var partyName = System.Text.Encoding.UTF8.GetString(br.ReadBytes(br.ReadInt32()));

            // SUB Step 3: Read material
            Material material = new(br);

            uint textureCount = br.ReadUInt32();
            float texFPS = br.ReadSingle();

            string[] textureNames = new string[textureCount];
            for (int j = 0; j < textureCount; j++)
            {
                var LengthTextureName = br.ReadInt32();
                if (LengthTextureName > 0)
                {
                    var textureName = System.Text.Encoding.UTF8.GetString(br.ReadBytes(LengthTextureName));
                    textureNames.SetValue(textureName, j);
                }
            }
            PartyMap.Add(partyName, textureNames);

        }

        Belong = br.ReadInt32();
        EventId = br.ReadInt32();
        EventType = br.ReadInt32();
        NpcId = br.ReadInt32();
        NpcStatus = br.ReadInt32();

    }
}


public record Material : D3DMATERIAL8
{
    public uint ColorOp { get; protected set; }
    public uint ColorArg1 { get; protected set; }
    public uint ColorArg2 { get; protected set; }
    public int RenderFlags { get; protected set; }
    public uint SrcBlend { get; protected set; }
    public uint DestBlend { get; protected set; }

    public Material(BinaryReader br) : base(br)
    {
        ColorOp = br.ReadUInt32();
        ColorArg1 = br.ReadUInt32();
        ColorArg2 = br.ReadUInt32();
        RenderFlags = br.ReadInt32();
        SrcBlend = br.ReadUInt32();
        DestBlend = br.ReadUInt32();
    }

    public void Load(BinaryReader br)
    {

    }

}

public record D3DMATERIAL8
{
    public D3DCOLORVALUE Diffuse { get; init; } = new();
    public D3DCOLORVALUE Ambient { get; init; } = new();
    public D3DCOLORVALUE Specular { get; init; } = new();
    public D3DCOLORVALUE Emissive { get; init; } = new();
    public float Power { get; set; }

    public D3DMATERIAL8(BinaryReader br)
    {
        //Diffuse = new();
        //Ambient = new();
        //Specular = new();
        //Emissive = new();
        Diffuse.Load(br);
        Ambient.Load(br);
        Specular.Load(br);
        Emissive.Load(br);
        Power = br.ReadSingle();
    }

    public void D3DLoad(BinaryReader br)
    {

    }
}

public record D3DCOLORVALUE
{
    public float r { get; protected set; }
    public float g { get; protected set; }
    public float b { get; protected set; }
    public float a { get; protected set; }

    public void Load(BinaryReader br)
    {
        r = br.ReadSingle();
        g = br.ReadSingle();
        b = br.ReadSingle();
        a = br.ReadSingle();
    }
}