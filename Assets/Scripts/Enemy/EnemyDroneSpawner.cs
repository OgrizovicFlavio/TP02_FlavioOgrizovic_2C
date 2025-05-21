using UnityEngine;

public class EnemyDroneSpawner : Spawner<EnemyDrone>
{
    protected override void SpawnEntityAt(Vector3 position, Quaternion rotation)
    {
        PoolManager.Instance.Get<EnemyDrone>(position, rotation);
    }

    protected override float GetSpawnIntervalFromLevelStats()
    {
        return LevelManager.Instance.Current.enemySpawnInterval;
    }

    protected override int GetSpawnLimitFromLevelStats()
    {
        return LevelManager.Instance.Current.enemiesToDestroy;
    }

    protected override int GetInitialSpawnCountFromLevelStats()
    {
        return LevelManager.Instance.Current.initialEnemySpawnCount;
    }
}
