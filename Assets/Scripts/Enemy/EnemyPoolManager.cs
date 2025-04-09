using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolManager : MonoBehaviour
{
    [Header("Enemy Setup")]
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform playerTarget;

    [Header("Pool Settings")]
    [SerializeField] private int defaultCapacity = 10; //Cantidad inicial en la pool.
    [SerializeField] private int maxSize = 20; //Cantidad máxima en la pool.
    [SerializeField] private bool collectionCheck = true; //Verifico duplicados.

    private IObjectPool<Enemy>[] enemyPools; //Arreglo de pools, una por cada tipo de enemigo.

    private void Awake()
    {
        enemyPools = new IObjectPool<Enemy>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            int index = i;
            enemyPools[i] = new ObjectPool<Enemy>(
                () => CreateEnemy(enemyPrefabs[index], enemyPools[index]),
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                collectionCheck,
                defaultCapacity,
                maxSize
            );
        }
    }

    private Enemy CreateEnemy(Enemy prefab, IObjectPool<Enemy> pool) //Crea un enemigo nuevo, le asigna la pool y configura su target.
    {
        Enemy enemy = Instantiate(prefab);
        enemy.Pool = pool;

        //Si tiene script de movimiento, asigno el target.
        EnemyController controller = enemy.GetComponent<EnemyController>();
        if (controller != null && playerTarget != null)
        {
            controller.SetTarget(playerTarget);
        }

        //Si tiene script de disparo, asigno el target.
        EnemyShoot shooter = enemy.GetComponent<EnemyShoot>();
        if (shooter != null && playerTarget != null)
        {
            shooter.SetTarget(playerTarget);
        }

        return enemy;
    }

    private void OnGetFromPool(Enemy enemy) // Cuando el enemigo es reutilizado desde la pool.
    {
        enemy.gameObject.SetActive(true); // Lo activo.

        // Reinicio su vida.
        var health = enemy.GetComponent<EnemyHealth>(); 
        if (health != null)
        {
            health.ResetHealth();
        }

        var controller = enemy.GetComponent<EnemyController>();
        if (controller != null && playerTarget != null)
        {
            controller.SetTarget(playerTarget);
        }

        var shooter = enemy.GetComponent<EnemyShoot>();
        if (shooter != null && playerTarget != null)
        {
            shooter.SetTarget(playerTarget);
        }
    }

    private void OnReleaseToPool(Enemy enemy) //Devuelto a la pool.
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Enemy enemy) //Se destruye definitivamente.
    {
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy(Vector3 position) //Método uqe usa EnemySpawner para pedir enemigos de la pool.
    {
        if (enemyPools.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPools.Length); //Elige el tipo de enemigo.
        Enemy enemy = enemyPools[randomIndex].Get(); //Lo pide a la pool correspondiente.
        enemy.transform.position = position; //Lo posiciona en la escena.
    }

    public void ReturnEnemyToPool(Enemy enemy) //Método que se llama cuando el enemigo "muere" y quiere volver a la pool.
    {
        enemy.Pool?.Release(enemy);
    }
}
