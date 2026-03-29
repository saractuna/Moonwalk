using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    #region
    public Transform player;
    public float chunkSize = 10f;
    public int spawnRadius = 1;
    public GameObject[] rockPrefabs;
    #endregion

    private Dictionary<Vector2, GameObject> spawnedChunks = new Dictionary<Vector2, GameObject>();

    void Update()
    {
        GenerateChunks();
    }

    void GenerateChunks()
    {
        // Gets the player's current chunk position
        Vector2 playerChunkPos = new Vector2(Mathf.Floor(player.position.x / chunkSize), Mathf.Floor(player.position.y / chunkSize));

        // Iterates over the spawn radius
        for (int x = -spawnRadius; x <= spawnRadius; x++)
        {
            for (int y = -spawnRadius; y <= spawnRadius; y++)
            {
                Vector2 chunkPos = playerChunkPos + new Vector2(x, y);

                // Check if the chunk already exists
                if (!spawnedChunks.ContainsKey(chunkPos))
                {
                    SpawnChunk(chunkPos);
                }
            }
        }
    }

    void SpawnChunk(Vector2 chunkPos)
    {
        if (Random.value < 0.1f) return; // 10% Chance of skipping a chunk, to increate randomness
        if (rockPrefabs.Length == 0) return;

        GameObject chosenPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)]; // This da prefab chosen randomly from da array/list idk

        Vector3 worldPos = new Vector3(chunkPos.x * chunkSize, chunkPos.y * chunkSize, 0f); // This da base position for chunk

        // Applies a random offset for "naturality"
        float offsetRange = chunkSize * 0.3f;
        worldPos.x += Random.Range(-offsetRange, offsetRange);
        worldPos.y += Random.Range(-offsetRange, offsetRange);

        GameObject chunk = Instantiate(chosenPrefab, worldPos, Quaternion.identity, transform); // <-- Parent to this GameObject
        spawnedChunks.Add(chunkPos, chunk);
    }
}