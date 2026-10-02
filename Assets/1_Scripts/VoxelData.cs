using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkSize = 3;

    public static readonly Vector3[] Vertices = new Vector3[]
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(1, 1, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 1),
        new Vector3(1, 1, 1),
        new Vector3(0, 1, 1)
    };

    public static readonly int[][] FaceTriangles = new int[][]
    {
        new int[] { 0, 3, 1, 1, 3, 2 },
        new int[] { 5, 6, 4, 4, 6, 7 },
        new int[] { 3, 7, 2, 2, 7, 6 },
        new int[] { 1, 5, 0, 0, 5, 4 },
        new int[] { 4, 7, 0, 0, 7, 3 },
        new int[] { 1, 2, 5, 5, 2, 6 }
    };

    public static readonly Vector3[] FaceNormals = new Vector3[]
    {
        new Vector3(0, 0, -1),
        new Vector3(0, 0, 1),
        new Vector3(0, 1, 0),
        new Vector3(0, -1, 0),
        new Vector3(-1, 0, 0),
        new Vector3(1, 0, 0)
    };
}