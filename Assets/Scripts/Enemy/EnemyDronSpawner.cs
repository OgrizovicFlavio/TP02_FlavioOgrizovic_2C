using UnityEngine;

public class EnemyDronSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private ObjectType enemyType = ObjectType.EnemyDron;

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
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = PoolController.Instance.GetObjectFromPool(enemyType);
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;
        enemy.SetActive(true);
    }
}
