using KOF.Zone.N3Base;
using KOF.Zone.N3Base.GTD;
using KOF.Zone.N3Base.OPD;
using System.Numerics;

namespace KOF.Zone;

public class CGameServerMap
{
    private byte ZoneIndex { get; set; }

    private string MinimapImage { get; set; } = "";
    private string MinimapBigImage { get; set; } = "";

    private const float UnitDistance = 4.0f;
    private const int ViewDistance = 48;

    private int XRegion { get; set; }
    private int ZRegion { get; set; }
    private int MapSize { get; set; }

    public ObjectPostData ObjectPostData { get; set; } = default!;

    private TerrainData TerrainData { get; set; } = default!;

    private short[,] Events = default!;

    public short[,] GetEvents => Events;

    public float MapLength => ObjectPostData.ShapeManager.MapLength;

    public CGameServerMap()
    {
        ObjectPostData = new();
        TerrainData = new();
    }

    public bool LoadObjectPostData(string opdFile) => ObjectPostData.Load(opdFile);
    public bool LoadTerrainData(string gtdFile) => TerrainData.Load(gtdFile);

    public CGameServerMap Load(string opdFile, string gtdFile)
    {
        CGameServerMap gameServerMap = new();

        if (gameServerMap.LoadObjectPostData(opdFile) &&
            gameServerMap.LoadTerrainData(gtdFile))
        {

            gameServerMap.Initialize();
            return gameServerMap;
        }

        return default!;
    }

    private void Initialize()
    {
        XRegion = ZRegion = (int)(ObjectPostData.ShapeManager.MapWidth / 48) + 1;
        MapSize = (int)(GetMapSize() * UnitDistance);

        Events = new short[GetMapSize() + 1, GetMapSize() + 1];

        // set default as moveable
        for (int x = 0; x < GetMapSize() + 1; x++)
        {
            for (int z = 0; z < GetMapSize() + 1; z++)
            {
                Events[x, z] = 1;
            }
        }

        GenerateMoveTable();
        CustomGenerateMoveTable();
    }

    public void GenerateMoveTable()
    {
        TerrainData.MakeMoveTable(Events);
        ObjectPostData.ShapeManager.MakeMoveTable(Events);
    }

    public void CustomGenerateMoveTable()
    {

        var opdMgr = ObjectPostData.ShapeManager;

        for (int i = 0; i < opdMgr.Shapes.Length; i++)
        {
            //opdMgr.Shapes[i].GetName.Contains("wall") ||
            //opdMgr.Shapes[i].GetName.Contains("mora") ||
            //opdMgr.Shapes[i].GetName.Contains("dumbull") ||
            //opdMgr.Shapes[i].GetName.Contains("cactus") ||
            //opdMgr.Shapes[i].GetName.Contains("sign") ||
            //opdMgr.Shapes[i].GetName.Contains("grass") ||
            //opdMgr.Shapes[i].GetName.Contains("plant") ||
            //opdMgr.Shapes[i].GetName.Contains("flower") ||
            //opdMgr.Shapes[i].GetName.Contains("rut") ||
            // opdMgr.Shapes[i].GetName.Contains("ston") ||
            //opdMgr.Shapes[i].GetName.Contains("reed") ||
            //// opdMgr.Shapes[i].GetName.Contains("obj") ||
            ///


            if (opdMgr.Shapes[i].GetName.Contains("bridge") ||
                opdMgr.Shapes[i].GetName.Contains("bush") ||
                opdMgr.Shapes[i].GetName.Contains("fish") ||
                opdMgr.Shapes[i].GetName.Contains("bush") ||
                opdMgr.Shapes[i].GetName.Contains("cactus") ||
                opdMgr.Shapes[i].GetName.Contains("plant") ||
                opdMgr.Shapes[i].GetName.Contains("stair") ||
                opdMgr.Shapes[i].GetName.Contains("gas") ||
                opdMgr.Shapes[i].GetName.Contains("reed") ||
                opdMgr.Shapes[i].GetName.Contains("cho2") ||
                opdMgr.Shapes[i].GetName.Contains("turkey") ||
                opdMgr.Shapes[i].GetName.Contains("tutkey") ||
                opdMgr.Shapes[i].GetName.Contains("clanfgt01") ||
                opdMgr.Shapes[i].GetName.Contains("gras"))
            {

                int p = 8;

                if (!opdMgr.Shapes[i].GetName.Contains("bridge"))
                    p = 1;

                if (opdMgr.Shapes[i].GetName.Contains("clanfgt01"))
                    p = 3;

                int iMinX = -(int)opdMgr.Shapes[i].GetScale.X * p;
                int iMaxX = (int)opdMgr.Shapes[i].GetScale.X * p;
                int iMinY = -(int)opdMgr.Shapes[i].GetScale.Y * p;
                int iMaxY = (int)opdMgr.Shapes[i].GetScale.Y * p;

                for (int dx = iMinX; dx < iMaxX; ++dx)
                {
                    for (int dy = iMinY; dy < iMaxY; ++dy)
                    {
                        var ix = Coordinate2Tile(opdMgr.Shapes[i].GetPosition.X);
                        var iz = Coordinate2Tile(opdMgr.Shapes[i].GetPosition.Z);

                        if (ix + dx < 0 || iz + dy < 0 || ix + dx > GetMapSize() || iz + dy > GetMapSize())
                            continue;

                        if (Events[ix + dx, iz + dy] == 0)
                            Events[ix + dx, iz + dy] = 1;

                        //if(opdMgr.Shapes[i].GetName.Contains("fx"))
                          //  Events[ix + dx, iz + dy] = 1;

                        //if (!tree->IsAttr(x + dx, y + dy, ATTR_BLOCK | ATTR_OBJECT)) {
                        //    if (test_server)
                        //        sys_log(0, "Coordinates %ld x %ld for %s false positive", x, y, m_ch->GetName());

                        //    m_whContinuousInvalid = 0;
                        //    return; //It is all ok because its very close to a movement enabled area (possible misdetection)
                        //}

                    }
                }



            }
        }
    }

    public int CoordinateToTile(float fV) => (int)(fV / 4);

    public int GetMapSize() => TerrainData.GetHeigthMapSize() - 1;

    public int Coordinate2Tile(float fV) { return (int)(fV / 4); }

    public void SetZoneIndex(byte zoneIndex)
    {
        ZoneIndex = zoneIndex;
    }

    public byte GetZoneIndex() => ZoneIndex;

    public void SetMinimapImage(string image, string imageBig)
    {
        MinimapImage = image;
        MinimapBigImage = imageBig;
    }

    public string GetMinimapImage() => MinimapImage;

    public string GetMinimapBigImage() => MinimapBigImage;

    public N3ShapeManager GetShapeManager()
    {
        return ObjectPostData.ShapeManager;
    }

    public float GetHeightBy2DPos(float fX, float fZ)
    {
        return TerrainData.GetHeight(Coordinate2Tile(fX), Coordinate2Tile(fZ));
    }

}
