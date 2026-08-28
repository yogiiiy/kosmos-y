using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private Coroutine spawnRoutine;
    private float currentSpawnInterval;

    /// <summary>
    /// Dipanggil NightManager pas Night mulai. spawnInterval beda-beda
    /// tiap Night (lihat tabel §4 KOSMOS_Y_V0.2_PLAN.md).
    /// </summary>
    public void StartSpawning(float spawnInterval)
    {
        currentSpawnInterval = spawnInterval;

        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    /// <summary>
    /// Dipanggil NightManager pas Night berakhir (jam capai 02:00).
    /// </summary>
    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnInterval);
            SpawnOneEnemy();
        }
    }

    private void SpawnOneEnemy()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(randomEnemy, point.position, Quaternion.identity);
    }
}