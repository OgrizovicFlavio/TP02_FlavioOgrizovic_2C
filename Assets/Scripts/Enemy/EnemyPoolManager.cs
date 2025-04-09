using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 20;
    [SerializeField] private bool collectionCheck = true;

    private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(
           CreateEnemy,
           OnGetFromPool,
           OnReleaseToPool,
           OnDestroyPooledObject,
           collectionCheck,
           defaultCapacity,
           maxSize
       );
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.Pool = enemyPool;

        EnemyController controller = enemy.GetComponent<EnemyController>();
        if (controller != null && playerTarget != null)
        {
            controller.target = playerTarget;
        }

        return enemy;
    }

    private void OnGetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);

        var health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.ResetHealth();
        }
    }

    private void OnReleaseToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy(Vector3 position)
    {
        Enemy enemy = enemyPool.Get();
        enemy.transform.position = position;
    }

    public void ReturnEnemyToPool(Enemy enemy)
    {
        enemyPool.Release(enemy);
    }
}
