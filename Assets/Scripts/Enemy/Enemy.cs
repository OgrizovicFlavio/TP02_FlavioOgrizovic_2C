using UnityEngine;

public class Enemy : Entity, IDamageable
{
    [SerializeField] private EnemyHealth health;

    public void TakeDamage(float amount) //IDamageable
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

        EnemyController controller = GetComponent<EnemyController>();
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
