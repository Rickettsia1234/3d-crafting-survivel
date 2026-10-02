using System.Collections.Generic;
using UnityEngine;

public class VoxelWorld : MonoBehaviour
{
    public GameObject voxelPrefab;
    public Material defaultMaterial;

    public Dictionary<int, GameObject> voxelPrefabDict = new Dictionary<int, GameObject>();
    public Dictionary<Vector2Int, Chunk> chunkDict = new Dictionary<Vector2Int, Chunk>();

    void Start()
    {
        voxelPrefabDict.Add(1, voxelPrefab);

        GenerateInitialChunks();
    }

    private void GenerateInitialChunks()
    {
        int chunkSize = VoxelData.ChunkSize;

        for (int i = -1; i <= 1; i++)
        {
            Vector2Int chunkCoord = new Vector2Int(i, 0);
            Vector3 chunkPos = new Vector3(i * chunkSize, 0, 0);

            GameObject chunkObj = new GameObject("Chunk_" + i + "_0");
            chunkObj.transform.position = chunkPos;
            chunkObj.transform.parent = transform;

            Chunk chunk = chunkObj.AddComponent<Chunk>();
            chunk.Initialize(chunkCoord, defaultMaterial);

            chunkDict.Add(chunkCoord, chunk);
        }
    }
}