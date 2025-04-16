using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [Header("Enemy Setup")]
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform playerTarget;

    [Header("Pool Settings")]
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;

    private List<Enemy>[] enemyPools;

    private void Awake()
    {
        enemyPools = new List<Enemy>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = new List<Enemy>();

            for (int j = 0; j < defaultCapacity; j++)
            {
                Enemy enemy = Instantiate(enemyPrefabs[i]);
                enemy.SetPoolInfo(this, i);
                enemy.gameObject.SetActive(false);
                SetupEnemy(enemy);
                enemyPools[i].Add(enemy);
            }
        }
    }

    private void SetupEnemy(Enemy enemy)
    {
        var controller = enemy.GetComponent<EnemyController>();
        if (controller != null && playerTarget != null)
            controller.SetTarget(playerTarget);

        var shooter = enemy.GetComponent<EnemyShoot>();
        if (shooter != null && playerTarget != null)
            shooter.SetTarget(playerTarget);

        var healthBar = enemy.GetComponentInChildren<EnemyHealthBar>();
        if (healthBar != null && Camera.main != null)
            healthBar.SetCamera(Camera.main);
    }

    public void SpawnEnemy(Vector3 position)
    {
        int poolIndex = Random.Range(0, enemyPrefabs.Length);
        Enemy enemy = GetEnemyFromPool(poolIndex);

        enemy.transform.position = position;
        enemy.gameObject.SetActive(true);

        var health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
            health.ResetHealth();
    }

    private Enemy GetEnemyFromPool(int poolIndex)
    {
        foreach (var enemy in enemyPools[poolIndex])
        {
            if (!enemy.gameObject.activeInHierarchy)
                return enemy;
        }

        if (enemyPools[poolIndex].Count < maxSize)
        {
            Enemy newEnemy = Instantiate(enemyPrefabs[poolIndex]);
            newEnemy.SetPoolInfo(this, poolIndex);
            SetupEnemy(newEnemy);
            newEnemy.gameObject.SetActive(false);
            enemyPools[poolIndex].Add(newEnemy);
            return newEnemy;
        }

        Debug.LogWarning("Límite de enemigos alcanzado.");
        return null;
    }

    public void ReturnEnemyToPool(Enemy enemy, int poolIndex)
    {
        if (poolIndex >= 0 && poolIndex < enemyPools.Length)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
