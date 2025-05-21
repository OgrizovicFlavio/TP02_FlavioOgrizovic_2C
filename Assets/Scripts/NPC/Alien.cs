using UnityEngine;

public class Alien : NPCController
{
    protected override void AddScore()
    {
        if (EnemyKillManager.Instance != null)
            EnemyKillManager.Instance.RegisterKill(stats.killValue);
    }

    protected override void DropCoin()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * 2f;
        PoolManager.Instance.Get<Coin>(spawnPosition, Quaternion.identity);
    }
}
