using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPoolManager poolManager;
    private int poolIndex;

    public void SetPoolInfo(EnemyPoolManager manager, int index)
    {
        poolManager = manager;
        poolIndex = index;
    }

    public void ReturnToPool()
    {
        poolManager.ReturnEnemyToPool(this, poolIndex);
    }
}
