using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Player player;
    [SerializeField] private UI_InGame ui;

    [Header("Spawn Settings")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 1;
    private int aliveCount;

    public int AliveCount => aliveCount;

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (true)
        {
            ui?.ShowWave(currentWave);

            SpawnWave(currentWave);

            yield return new WaitUntil(() => aliveCount <= 0);
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
        }
    }

    private void SpawnWave(int wave)
    {
        int bunnyCount = wave + 4;
        int bearCount = wave < 3 ? 0 : wave - 2;
        int elephantCount = wave < 7 ? 0 : wave - 6;

        SpawnEnemies(EnemyType.Bunny, bunnyCount);
        SpawnEnemies(EnemyType.Bear, bearCount);
        SpawnEnemies(EnemyType.Elephant, elephantCount);
    }

    private void SpawnEnemies(EnemyType type, int count)
    {
        if (enemyPool == null || count <= 0)
            return;

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = GetSpawnPoint();
            Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
            Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

            Enemy enemy = enemyPool.Spawn(type, position, rotation, player);
            if (enemy == null)
                continue;

            aliveCount++;
            enemy.OnDeath += HandleEnemyDeath;
        }
    }

    private Transform GetSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
            return null;

        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        if (enemy != null)
            enemy.OnDeath -= HandleEnemyDeath;

        aliveCount = Mathf.Max(0, aliveCount - 1);
    }
}
