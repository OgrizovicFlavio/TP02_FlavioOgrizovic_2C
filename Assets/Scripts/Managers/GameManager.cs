using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pool Prefabs")]
    [SerializeField] private PlayerLaserBullet playerBulletPrefab;
    [SerializeField] private EnemyDroneBullet enemyBulletPrefab;
    [SerializeField] private PlayerExplosiveBullet playerExplosiveBulletPrefab;
    [SerializeField] private EnemyDrone enemyPrefab;
    [SerializeField] private Civilian npcCivilianPrefab;
    [SerializeField] private Alien npcAlienPrefab;
    [SerializeField] private LaserImpactEffect laserImpactEffectPrefab;
    [SerializeField] private ExplosiveImpactEffect explosiveImpactEffectPrefab;

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
        PoolManager.Instance.InitializePool(playerExplosiveBulletPrefab, bulletsPerType);
        PoolManager.Instance.InitializePool(laserImpactEffectPrefab, impactEffectCount);
        PoolManager.Instance.InitializePool(explosiveImpactEffectPrefab, impactEffectCount);
        PoolManager.Instance.InitializePool(explosiveImpactEffectPrefab, impactEffectCount);
        PoolManager.Instance.InitializePool<Civilian>(npcCivilianPrefab, npcCount);
        PoolManager.Instance.InitializePool<Alien>(npcAlienPrefab, npcCount);
    }
}
