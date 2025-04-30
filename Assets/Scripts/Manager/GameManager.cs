using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Prefabs")]
    [SerializeField] private PlayerBullet playerBulletPrefab;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private NPCController npcCivilianPrefab;
    [SerializeField] private NPCController npcAlienPrefab;
    [SerializeField] private ImpactEffect impactEffectPrefab;

    [Header("Pool Settings")]
    [SerializeField] private int bulletsPerType = 30;
    [SerializeField] private int enemyCount = 20;
    [SerializeField] private int npcCount = 20;
    [SerializeField] private int impactEffectCount = 40;

    private void Start()
    {
        PoolManager.Instance.InitializePool(enemyPrefab, enemyCount);
        PoolManager.Instance.InitializePool(enemyBulletPrefab, bulletsPerType);
        PoolManager.Instance.InitializePool(playerBulletPrefab, bulletsPerType);
        PoolManager.Instance.InitializePool(impactEffectPrefab, impactEffectCount);
        PoolManager.Instance.InitializePool(npcCivilianPrefab, npcCount);
        PoolManager.Instance.InitializePool(npcAlienPrefab, npcCount);
    }
}
