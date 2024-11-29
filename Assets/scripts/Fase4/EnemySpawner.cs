using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // O prefab do inimigo
    public int enemyCount = 5; // NÃºmero de inimigos a serem criados
    public Vector2 spawnAreaMin; // Coordenadas mÃ­nimas de spawn
    public Vector2 spawnAreaMax; // Coordenadas mÃ¡ximas de spawn

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void nSpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}