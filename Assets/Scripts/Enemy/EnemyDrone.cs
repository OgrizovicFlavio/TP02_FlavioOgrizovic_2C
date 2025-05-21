using UnityEngine;

public class EnemyDrone : Entity, IDamageable
{
    public static event System.Action OnEnemyDestroyed;

    [SerializeField] private EnemyDroneStats stats;
    [SerializeField] private EnemyDroneHealth health;

    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
    }

    public override void Die()
    {
        if (isDead) return;

        OnEnemyDestroyed?.Invoke();
        DropCoin();

        health.ForceDie();
        base.Die();
    }

    private void DropCoin()
    {
        PoolManager.Instance.Get<Coin>(transform.position, Quaternion.identity);
    }

    protected override void AddScore()
    {
        if (EnemyKillManager.Instance != null)
            EnemyKillManager.Instance.RegisterKill(stats.killValue);
    }

    protected override void HandleReturn()
    {
        PoolManager.Instance.ReturnToPool(this);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        health.ResetHealth();

        EnemyDroneController controller = GetComponent<EnemyDroneController>();
        if (controller != null)
        {
            controller.ResetTarget();
        }
    }

    public override void OnGetFromPool()
    {
        base.OnGetFromPool();
        health.ResetHealth();
    }
}
