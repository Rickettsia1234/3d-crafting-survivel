using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Transform playerTransform;
    public VoxelWorld voxelWorld;
    public int loadDistance = 2;

    private Queue<Vector2Int> chunkQueue = new Queue<Vector2Int>();
    private bool isGenerating = false;

    void Update()
    {
        Vector2Int playerChunkCoord = new Vector2Int(
            Mathf.FloorToInt(playerTransform.position.x / VoxelData.ChunkSize),
            Mathf.FloorToInt(playerTransform.position.z / VoxelData.ChunkSize)
        );

        for (int x = -loadDistance; x <= loadDistance; x++)
        {
            Vector2Int targetCoord = new Vector2Int(playerChunkCoord.x + x, 0);

            if (!voxelWorld.chunkDict.ContainsKey(targetCoord) && !chunkQueue.Contains(targetCoord))
            {
                chunkQueue.Enqueue(targetCoord);
            }
        }

        if (!isGenerating && chunkQueue.Count > 0)
        {
            StartCoroutine(GenerateChunkAsync());
        }
    }

    private IEnumerator GenerateChunkAsync()
    {
        isGenerating = true;

        while (chunkQueue.Count > 0)
        {
            Vector2Int coord = chunkQueue.Dequeue();
            int chunkSize = VoxelData.ChunkSize;
            Vector3 chunkPos = new Vector3(coord.x * chunkSize, 0, 0);

            GameObject chunkObj = new GameObject("Chunk_" + coord.x + "_" + coord.y);
            chunkObj.transform.position = chunkPos;
            chunkObj.transform.parent = voxelWorld.transform;

            Chunk chunk = chunkObj.AddComponent<Chunk>();
            chunk.Initialize(coord, voxelWorld.defaultMaterial);

            voxelWorld.chunkDict.Add(coord, chunk);

            yield return null;
        }

        isGenerating = false;
    }
}