using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private int[,,] voxelMap;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    public Vector2Int ChunkCoord { get; private set; }

    public void Initialize(Vector2Int coord, Material material)
    {
        ChunkCoord = coord;
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshRenderer.material = material;

        int size = VoxelData.ChunkSize;
        voxelMap = new int[size, 1, size];

        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                voxelMap[x, 0, z] = 1;
            }
        }

        GenerateMesh();
    }

    public void GenerateMesh()
    {
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();

        int size = VoxelData.ChunkSize;

        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                if (voxelMap[x, 0, z] == 0) continue;

                Vector3 blockPos = new Vector3(x, 0, z);

                for (int face = 0; face < 6; face++)
                {
                    if (!HasNeighbor(x, 0, z, face))
                    {
                        int vertexIndex = vertices.Count;

                        for (int i = 0; i < 6; i++)
                        {
                            int triangleVertex = VoxelData.FaceTriangles[face][i];
                            vertices.Add(blockPos + VoxelData.Vertices[triangleVertex]);
                            triangles.Add(vertexIndex + i);
                        }

                        uvs.Add(new Vector2(0, 0));
                        uvs.Add(new Vector2(0, 1));
                        uvs.Add(new Vector2(1, 0));
                        uvs.Add(new Vector2(1, 0));
                        uvs.Add(new Vector2(0, 1));
                        uvs.Add(new Vector2(1, 1));
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private bool HasNeighbor(int x, int y, int z, int face)
    {
        Vector3 normal = VoxelData.FaceNormals[face];
        int targetX = x + (int)normal.x;
        int targetY = y + (int)normal.y;
        int targetZ = z + (int)normal.z;

        int size = VoxelData.ChunkSize;

        if (targetX < 0 || targetX >= size || targetY < 0 || targetY >= 1 || targetZ < 0 || targetZ >= size)
        {
            return false;
        }

        return voxelMap[targetX, targetY, targetZ] != 0;
    }
}