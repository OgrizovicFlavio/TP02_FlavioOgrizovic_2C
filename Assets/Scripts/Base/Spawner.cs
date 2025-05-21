using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Transform[] spawnPoints;

    private float timer = 0f;
    private int currentSpawnIndex = 0;
    private int activeCount = 0;
    private int spawnLimit;
    private float spawnInterval;
    private int initialSpawnCount;

    protected abstract void SpawnEntityAt(Vector3 position, Quaternion rotation);
    protected abstract float GetSpawnIntervalFromLevelStats();
    protected abstract int GetSpawnLimitFromLevelStats();
    protected abstract int GetInitialSpawnCountFromLevelStats();

    protected virtual void Start()
    {
        Entity.OnAnyEntityDestroyed += HandleEntityDestroyed;

        spawnInterval = GetSpawnIntervalFromLevelStats();
        spawnLimit = GetSpawnLimitFromLevelStats();
        initialSpawnCount = GetInitialSpawnCountFromLevelStats();

        for (int i = 0; i < spawnPoints.Length && activeCount < Mathf.Min(initialSpawnCount, spawnLimit); i++)
        {
            SpawnEntityAt(spawnPoints[i].position, spawnPoints[i].rotation);
            activeCount++;
        }
    }

    protected virtual void OnDestroy()
    {
        Entity.OnAnyEntityDestroyed -= HandleEntityDestroyed;
    }

    protected virtual void Update()
    {
        if (activeCount >= spawnLimit)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawnNext();
        }
    }

    private void TrySpawnNext()
    {
        if (spawnPoints.Length == 0 || activeCount >= spawnLimit)
            return;

        var point = spawnPoints[currentSpawnIndex];
        SpawnEntityAt(point.position, point.rotation);
        activeCount++;

        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;
    }

    private void HandleEntityDestroyed(Entity entity)
    {
        if (activeCount > 0)
            activeCount--;
    }
}
