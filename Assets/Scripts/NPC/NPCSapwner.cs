using UnityEngine;

public class NPCSpawner : Spawner<NPCController>
{
    protected override void SpawnEntityAt(Vector3 position, Quaternion rotation)
    {
        bool spawnCivilian = Random.value < 0.5f;

        if (spawnCivilian)
            PoolManager.Instance.Get<Civilian>(position, rotation);
        else
            PoolManager.Instance.Get<Alien>(position, rotation);
    }

    protected override float GetSpawnIntervalFromLevelStats()
    {
        return LevelManager.Instance.Current.npcSpawnInterval;
    }

    protected override int GetSpawnLimitFromLevelStats()
    {
        return LevelManager.Instance.Current.npcSpawnLimit;
    }

    protected override int GetInitialSpawnCountFromLevelStats()
    {
        return LevelManager.Instance.Current.initialNPCSpawnCount;
    }
}
