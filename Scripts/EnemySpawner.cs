using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region
    public GameObject[] enemyPrefabs;
    public Transform player;
    public float spawnRadius = 20f;
    public float spawnInterval = 3f;
    #endregion

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || player == null) return;

        // Picks a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Places an enemy at the edge of the spawn radius
        Vector3 spawnPosition = player.position + (Vector3)(randomDirection * spawnRadius);

        // Picks a random enemy type from the enemy class
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawns the enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}