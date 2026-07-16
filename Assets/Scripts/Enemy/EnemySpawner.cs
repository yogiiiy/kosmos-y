using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnAllEnemies();
    }

    private void SpawnAllEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(slimePrefab, point.position, Quaternion.identity);
        }
    }
}