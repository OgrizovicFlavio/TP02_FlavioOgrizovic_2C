using UnityEngine;

public class EnemyDroneSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy enemy = PoolManager.Instance.Get<Enemy>();

        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;
    }
}
