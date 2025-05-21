using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
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
    [SerializeField] private Coin coinPrefab;

    private void Start()
    {
        PoolManager.Instance.ReturnAllActiveObjects();
        LevelTimerManager.Instance?.StartTimer();

        var stats = LevelManager.Instance.Current;

        PoolManager.Instance.InitializePool(enemyPrefab, stats.enemyPoolSize);
        PoolManager.Instance.InitializePool(enemyBulletPrefab, stats.bulletsPullSize);
        PoolManager.Instance.InitializePool(playerBulletPrefab, stats.bulletsPullSize);
        PoolManager.Instance.InitializePool(playerExplosiveBulletPrefab, stats.bulletsPullSize);
        PoolManager.Instance.InitializePool(laserImpactEffectPrefab, stats.impactEffectPoolSize);
        PoolManager.Instance.InitializePool(explosiveImpactEffectPrefab, stats.impactEffectPoolSize);
        PoolManager.Instance.InitializePool<Civilian>(npcCivilianPrefab, stats.npcPoolSize);
        PoolManager.Instance.InitializePool<Alien>(npcAlienPrefab, stats.npcPoolSize);
        PoolManager.Instance.InitializePool(coinPrefab, 20);
    }

    protected override void OnAwaken() { }

    public void ResetGame()
    {
        CoinManager.Instance?.ResetCoins();
        EnemyKillManager.Instance?.ResetKills();
        LevelTimerManager.Instance?.ResetTimer();

        PoolManager.Instance?.ReturnAllActiveObjects();
    }
}
