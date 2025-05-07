using UnityEngine;

public class EnemyDrone : Entity, IDamageable
{
    [SerializeField] private EnemyDroneHealth health;

    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
    }

    public override void Die()
    {
        if (isDead) return;

        health.ForceDie();
        base.Die();
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
