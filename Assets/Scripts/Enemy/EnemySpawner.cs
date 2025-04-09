using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPoolManager enemyPoolManager;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPoolManager == null || spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[index].position;

        enemyPoolManager.SpawnEnemy(spawnPosition);
    }
}
