using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace KOF.Zone.N3Base;

public class N3ShapeManager
{
#pragma warning disable IDE0051 // Remove unused private members
    private const int CELL_MAIN_DEVIDE = 4;
    private const int CELL_SUB_SIZE = 4;
    private const int CELL_MAIN_SIZE = CELL_MAIN_DEVIDE * CELL_MAIN_DEVIDE;
    private const int MAX_CELL_MAIN = 4096 / CELL_MAIN_SIZE;
    private const int MAX_CELL_SUB = MAX_CELL_MAIN * CELL_MAIN_DEVIDE;
    private const int VIEW_DISTANCE = 48;
#pragma warning restore IDE0051 // Remove unused private members

    public float MapWidth { get; protected set; }
    public float MapLength { get; protected set; }
    public int CollisionFaceCount { get; protected set; }
    public CellMain[,] Cells { get; protected set; } = default!;
    public Vector3[] Collisions { get; protected set; } = default!;
    public N3ShapeExtension[] Shapes { get; protected set; } = default!;


    public bool Load(BinaryReader br, bool obsolete = false)
    {
        const int OBJ_SHAPE_EXTRA = 0x1000;

        if (!LoadCollisionData(br))
            return false;

        int shapeCount = br.ReadInt32();
        if (shapeCount > 0)
        {

            N3ShapeExtension Shape = default!;
            Shapes = new N3ShapeExtension[shapeCount];
            uint objectType = 0;

            int halfWay = shapeCount / 2;
            for (int i = 0; i < shapeCount; i++)
            {
                objectType = br.ReadUInt32();

                if ((objectType & OBJ_SHAPE_EXTRA) == 1)
                    Shape = new();
                else
                    Shape = new();

                Shape.Load(br);

                if (!obsolete && halfWay - 1 == i)
                {
                    var mapNameHalfWay = br.ReadBytes(br.ReadInt32());
                }
                else if (obsolete && halfWay - 1 == i)
                {
                    Debug.WriteLine($"ShapeData halfway offset (mode old) : {br.BaseStream.Position}");
                }

                Shapes.SetValue(Shape, i);

            }

        }

        return true;
    }

    private bool Create(float mapWidth, float mapLength)
    {

        if (mapWidth <= 0.0f || mapWidth > MAX_CELL_MAIN * CELL_MAIN_SIZE ||
            mapLength <= 0.0f || mapLength > MAX_CELL_MAIN * CELL_MAIN_SIZE)
            return false;

        MapWidth = mapWidth;
        MapLength = mapLength;

        return true;
    }
    private bool LoadCollisionData(BinaryReader br)
    {
        const int CELL_MAIN_DEVIDE = 4;
        const int CELL_MAIN_SIZE = CELL_MAIN_DEVIDE * CELL_MAIN_DEVIDE;
        const int MAX_CELL_MAIN = 4096 / CELL_MAIN_SIZE;

        MapWidth = br.ReadSingle();
        MapLength = br.ReadSingle();

        Create(MapWidth, MapLength);

        CollisionFaceCount = br.ReadInt32();
        if (CollisionFaceCount > 0)
        {

            Collisions = new Vector3[CollisionFaceCount * 3];

            for (int i = 0; i < CollisionFaceCount * 3; i++)
            {

                Collisions[i].X = br.ReadSingle();
                Collisions[i].Y = br.ReadSingle();
                Collisions[i].Z = br.ReadSingle();
            }

        }


        // Cell data
        Cells = new CellMain[MAX_CELL_MAIN, MAX_CELL_MAIN];

        int z = 0;
        for (float fZ = 0.0f; fZ < MapLength; fZ += CELL_MAIN_SIZE, z++)
        {

            int x = 0;
            for (float fX = 0.0f; fX < MapWidth; fX += CELL_MAIN_SIZE, x++)
            {

                uint exits = br.ReadUInt32();
                if (exits == 0)
                    continue;

                Cells[x, z] = new CellMain() { SubCells = new CellSub[4, 4] };
                Cells[x, z].Load(br);
            }
        }

        Console.WriteLine($"Load Collision \tMap Width: {MapWidth}, Map Length: {MapLength}, Collision Count {CollisionFaceCount}");

        return true;
    }

    public void MakeMoveTable(short[,] moveArray)
    {

        for (int bx = 0; bx < MAX_CELL_MAIN; bx++)
        {

            for (int bz = 0; bz < MAX_CELL_MAIN; bz++)
            {

                if (Cells[bx, bz].SubCells != default)
                {

                    for (int sx = 0; sx < CELL_MAIN_DEVIDE; sx++)
                    {

                        for (int sz = 0; sz < CELL_MAIN_DEVIDE; sz++)
                        {

                            if (Cells[bx, bz].SubCells[sx, sz].CCPolyCount > 0)
                            {


                                int ix = bx * CELL_MAIN_DEVIDE + sx;
                                int iz = bz * CELL_MAIN_DEVIDE + sz;

                                moveArray[ix, iz] = 0;

                            }

                        }

                    }

                }

            }

        }

    }


    public bool CheckCollision(Vector3 initialPosition, Vector3 newPosition, Vector3 direction)
    {

        CellSub[] ppCells = new CellSub[128];
        int subcellCount = SubCellPathThru(initialPosition, newPosition, ppCells);
        if (subcellCount <= 0 || subcellCount > 128)
            return false;

        Vector3 ColTmp = Vector3.Zero;
        uint nIndex0, nIndex1, nIndex2;
        float fT = 0.0f, fU = 0.0f, fV = 0.0f, fDistTmp, fDistClosest;

        fDistClosest = float.MaxValue;


        for (int i = 0; i < subcellCount; i++)
        {
            if (ppCells[i].CCPolyCount <= 0)
                continue;
            for (int j = 0; j < ppCells[i].CCPolyCount; j++)
            {

                nIndex0 = ppCells[i].CCPolyVertIndices[j * 3];
                nIndex1 = ppCells[i].CCPolyVertIndices[j * 3 + 1];
                nIndex2 = ppCells[i].CCPolyVertIndices[j * 3 + 2];

                if (!_IntersectTriangle(initialPosition, direction, Collisions[nIndex0], Collisions[nIndex1], Collisions[nIndex2], fT, fU, fV, ColTmp))
                    continue;
                if (!_IntersectTriangle(newPosition, direction, Collisions[nIndex0], Collisions[nIndex1], Collisions[nIndex2]))
                {

                    fDistTmp = Vector3.DistanceSquared(initialPosition, ColTmp);

                    if (fDistTmp < fDistClosest)
                    {

                        fDistClosest = fDistTmp;

                    }

                }
            }
        }

        if (fDistClosest != float.MaxValue)
            return true;

        return false;
    }

    private int SubCellPathThru(Vector3 vFrom, Vector3 vAt, CellSub[] ppSubCells)
    {

        if (ppSubCells.Equals(default(CellSub)))
            return 0;

        // ¹üÀ§¸¦ Á¤ÇÏ°í..
        int xx1 = 0, xx2 = 0, zz1 = 0, zz2 = 0;

        if (vFrom.X < vAt.X) { xx1 = (int)(vFrom.X / CELL_SUB_SIZE); xx2 = (int)(vAt.X / CELL_SUB_SIZE); }
        else { xx1 = (int)(vAt.X / CELL_SUB_SIZE); xx2 = (int)(vFrom.X / CELL_SUB_SIZE); }

        if (vFrom.Z < vAt.Z) { zz1 = (int)(vFrom.Z / CELL_SUB_SIZE); zz2 = (int)(vAt.Z / CELL_SUB_SIZE); }
        else { zz1 = (int)(vAt.Z / CELL_SUB_SIZE); zz2 = (int)(vFrom.Z / CELL_SUB_SIZE); }

        bool bPathThru;
        float fZMin, fZMax, fXMin, fXMax;
        int nSubCellCount = 0;
        for (int z = zz1; z <= zz2; z++) // ¹üÀ§¸¸Å­ Ã³¸®..
        {
            fZMin = z * CELL_SUB_SIZE;
            fZMax = (z + 1) * CELL_SUB_SIZE;
            for (int x = xx1; x <= xx2; x++)
            {
                fXMin = x * CELL_SUB_SIZE;
                fXMax = (x + 1) * CELL_SUB_SIZE;

                // Cohen thuderland algorythm
                uint dwOC0 = 0, dwOC1 = 0; // OutCode 0, 1
                if (vFrom.Z > fZMax)
                    dwOC0 |= 0xf000;
                if (vFrom.Z < fZMin)
                    dwOC0 |= 0x0f00;
                if (vFrom.X > fXMax)
                    dwOC0 |= 0x00f0;
                if (vFrom.X < fXMin)
                    dwOC0 |= 0x000f;
                if (vAt.Z > fZMax)
                    dwOC1 |= 0xf000;
                if (vAt.Z < fZMin)
                    dwOC1 |= 0x0f00;
                if (vAt.X > fXMax)
                    dwOC1 |= 0x00f0;
                if (vAt.X < fXMin)
                    dwOC1 |= 0x000f;

                bPathThru = false;
                if ((dwOC0 & dwOC1) == 1)
                    bPathThru = false; // µÎ ³¡Á¡ÀÌ °°Àº º¯ÀÇ ¿ÜºÎ¿¡ ÀÖ´Ù.
                else if (dwOC0 == 0 && dwOC1 == 0)
                    bPathThru = true;// ¼±ºĞÀÌ »ç°¢Çü ³»ºÎ¿¡ ÀÖÀ½
                else if (dwOC0 == 0 && dwOC1 != 0 || dwOC0 != 0 && dwOC1 == 0)
                    bPathThru = true;// ¼±ºĞ ÇÑÁ¡Àº ¼¿ÀÇ ³»ºÎ¿¡ ÇÑÁ¡Àº ¿ÜºÎ¿¡ ÀÖÀ½.
                else if ((dwOC0 & dwOC1) == 0) // µÎ …LÁ¡ ¸ğµÎ ¼¿ ¿ÜºÎ¿¡ ÀÖÁö¸¸ ÆÇ´ÜÀ» ´Ù½Ã ÇØ¾ß ÇÑ´Ù.
                {
                    float fXCross = vFrom.X + (fZMax - vFrom.Z) * (vAt.X - vFrom.X) / (vAt.Z - vFrom.Z); // À§ÀÇ º¯°úÀÇ ±³Â÷Á¡À» °è»êÇÏ°í..
                    if (fXCross < fXMin)
                        bPathThru = false; // ¿ÏÀüÈ÷ ¿Ü°û¿¡ ÀÖ´Ù.
                    else
                        bPathThru = true; // °ÉÃ³ÀÖ´Ù.
                }

                if (false == bPathThru)
                    continue;

                // Ãæµ¹ Á¤º¸¸¦ ½á¾ß ÇÑ´Ù..
                int nX = x / CELL_MAIN_DEVIDE;
                int nZ = z / CELL_MAIN_DEVIDE;
                if (nX < 0 || nX >= MAX_CELL_MAIN || nZ < 0 && nZ >= MAX_CELL_MAIN)
                    continue; // ¸ŞÀÎ¼¿¹Ù±ù¿¡ ÀÖÀ½ Áö³ª°£´Ù.
                if (Cells[nX, nZ].Equals(default(CellMain)))
                    continue; // ¸ŞÀÎ¼¿ÀÌ ³ÎÀÌ¸é Áö³ª°£´Ù..

                int nXSub = x % CELL_MAIN_DEVIDE;
                int nZSub = z % CELL_MAIN_DEVIDE;

                ppSubCells[nSubCellCount] = Cells[nX, nZ].SubCells[nXSub, nZSub];
                nSubCellCount++;
            } // end of for(int x = xx1; x <= xx2; x++)
        } // end of for(int z = zz1; z <= zz2; z++) // ¹üÀ§¸¸Å­ Ã³¸®..

        return nSubCellCount; // °ÉÄ£ ¼¿ Æ÷ÀÎÅÍ µ¹·ÁÁÖ±â..
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool _IntersectTriangle(Vector3 vOrig, Vector3 vDir,
                              Vector3 v0, Vector3 v1, Vector3 v2,
                              float fT, float fU, float fV, Vector3 pVCol = default)
    {
        // Find vectors for two edges sharing vert0
        Vector3 vEdge1, vEdge2;

        vEdge1 = v1 - v0;
        vEdge2 = v2 - v0;

        // Begin calculating determinant - also used to calculate U parameter
        Vector3 pVec = Vector3.Cross(vEdge1, vEdge2);
        float fDet = Vector3.Dot(vDir, pVec);

        //pVec.Cross(vEdge1, vEdge2);
        //fDet = pVec.Dot(vDir);
        if (fDet > -0.0001f)
            return false;

        pVec = Vector3.Cross(vDir, vEdge2);

        // If determinant is near zero, ray lies in plane of triangle
        fDet = Vector3.Dot(pVec, vEdge1);
        if (fDet < 0.0001f)
            return false;

        // Calculate distance from vert0 to ray origin
        Vector3 tVec = vOrig - v0;

        // Calculate U parameter and test bounds
        fU = Vector3.Dot(pVec, tVec);
        if (fU < 0.0f || fU > fDet)
            return false;

        // Prepare to test V parameter
        Vector3 qVec = Vector3.Cross(tVec, vEdge1);
        //qVec.Cross(tVec, vEdge1);

        // Calculate V parameter and test bounds
        fV = Vector3.Dot(qVec, vDir);// vDir.Dot(qVec);
        if (fV < 0.0f || fU + fV > fDet)
            return false;

        // Calculate t, scale parameters, ray intersects triangle
        fT = Vector3.Dot(qVec, vEdge2);// vEdge2.Dot(qVec);
        float fInvDet = 1.0f / fDet;
        fT *= fInvDet;
        fU *= fInvDet;
        fV *= fInvDet;

        if (pVCol != default)
            pVCol = vOrig + vDir * fT;
        return fT >= 0.0f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool _IntersectTriangle(Vector3 vOrig, Vector3 vDir, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        // Find vectors for two edges sharing vert0
        // Begin calculating determinant - also used to calculate U parameter
        float fDet, fT, fU, fV;
        Vector3 vEdge1, vEdge2, tVec, pVec, qVec;

        vEdge1 = v1 - v0;
        vEdge2 = v2 - v0;

        pVec = Vector3.Cross(vEdge1, vEdge2);
        fDet = Vector3.Dot(vDir, pVec);// pVec.Dot(vDir);
        if (fDet > -0.0001f)
            return false;

        pVec = Vector3.Cross(vDir, vEdge2);

        // If determinant is near zero, ray lies in plane of triangle
        fDet = Vector3.Dot(pVec, vEdge1);// vEdge1.Dot(pVec);
        if (fDet < 0.0001f)
            return false;

        // Calculate distance from vert0 to ray origin
        tVec = vOrig - v0;

        // Calculate U parameter and test bounds
        fU = Vector3.Dot(pVec, tVec);// tVec.Dot(pVec);
        if (fU < 0.0f || fU > fDet)
            return false;

        // Prepare to test V parameter
        qVec = Vector3.Cross(tVec, vEdge1);

        // Calculate V parameter and test bounds
        fV = Vector3.Dot(qVec, vDir);// vDir.Dot(qVec);
        if (fV < 0.0f || fU + fV > fDet)
            return false;

        // Calculate t, scale parameters, ray intersects triangle
        fT = Vector3.Dot(qVec, vEdge2) / fDet;
        return fT >= 0.0f;
    }


}

//            if (obsolete) {

//                objectEvents = new ObjectEvent[shapeCount];
//                for (int i = 0; i<shapeCount; i++) {
//                    ObjectEvent old_objectEvent = new()
//                    {
//                        ByNation = (byte)br.ReadInt32(),
//                        ObjectId = br.ReadInt16(),
//                        ByType = br.ReadInt16(),
//                        AssociatedNpcId = br.ReadInt16(),
//                        ByStatus = (byte)br.ReadUInt16(),
//                        PositionX = br.ReadSingle(),
//                        PositionY = br.ReadSingle(),
//                        PositionZ = br.ReadSingle(),
//                        ByLife = 1
//                    };
//objectEvents.SetValue(old_objectEvent, i);
//                }

//            }
//            else {

//    objectEvents = new ObjectEvent[shapeCount];
//    for (int i = 0; i < shapeCount; i++) {

//        ObjectEvent _objectEvent = new()
//        {
//            ObjectId = br.ReadInt16(),
//            AssociatedNpcId = br.ReadInt16(),
//            ByType = br.ReadByte(),
//            ByNation = br.ReadByte(),
//            ByStatus = br.ReadByte(),
//            PositionX = br.ReadSingle(),
//            PositionY = br.ReadSingle(),
//            PositionZ = br.ReadSingle(),
//            ByLife = 1
//        };

//        objectEvents.SetValue(_objectEvent, i);
//    }

//}

public struct CellMain
{

    /// <summary>
    ///     Shape Count.
    /// </summary>
    public int ShapeCount;

    /// <summary>
    ///     Shape Indices.
    /// </summary>
    public ushort[] ShapeIndices;

    public CellSub[,] SubCells;

    public void Load(BinaryReader br)
    {
        const int CELL_MAIN_DEVIDE = 4;

        ShapeCount = br.ReadInt32();

        if (ShapeCount > 0)
        {

            ShapeIndices = new ushort[ShapeCount];
            for (int i = 0; i < ShapeCount; i++)
            {

                ShapeIndices[i] = br.ReadUInt16();

            }
        }

        for (int z = 0; z < CELL_MAIN_DEVIDE; z++)
        {
            for (int x = 0; x < CELL_MAIN_DEVIDE; x++)
            {

                SubCells[x, z].Load(br);

            }
        }

    }
}

public struct CellSub
{

    /// <summary>
    ///     Collision Check Polygon Count
    /// </summary>
    public int CCPolyCount;

    /// <summary>
    ///     Collision Check Polygon Vertex Indices -> <see href="CCPolyCount" /> * 3
    /// </summary>
    public uint[] CCPolyVertIndices;

    public void Load(BinaryReader br)
    {

        CCPolyCount = br.ReadInt32();

        if (CCPolyCount > 0)
        {

            CCPolyVertIndices = new uint[CCPolyCount * 3];
            for (int i = 0; i < CCPolyCount * 3; i++)
            {

                CCPolyVertIndices[i] = br.ReadUInt32();

            }
        }
    }
}

public struct ObjectEvent
{

    /// <summary>
    ///     100th Squad - Karus Bind Point 
    ///     200th Division Elmorad Bind Point 
    ///     Division 1100 - Karus Gatess 
    ///     Division 1200 - Elmorad Gates
    /// </summary>
    public short ObjectId;

    /// <summary>
    ///     NPC ID to Associated
    /// </summary>
    public short AssociatedNpcId;

    /// <summary>
    ///     0 - bind point.. 
    ///     1 - glottis opening left and right 
    ///     2 - glottis opening up and down 
    ///     3 - lever
    /// </summary>
    public short ByType;

    /// <summary>
    ///     Indicates the object's belonging nation (0 = all, 1 = karus, 2 = elmo)
    /// </summary>
    public byte ByNation;

    /// <summary>
    ///      Status flag for gate, lever like objects. (1 on, 0 off)
    /// </summary>
    public short ByStatus;

    /// <summary>
    ///     Object Position X.
    /// </summary>
    public float PositionX;

    /// <summary>
    ///     Object Position Y.
    /// </summary>
    public float PositionY;

    /// <summary>
    ///     Object Position Z.
    /// </summary>
    public float PositionZ;

    /// <summary>
    ///     Indicates if object is destroyed or not.
    /// </summary>
    public byte ByLife;

}