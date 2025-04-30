using UnityEngine;

public class PlayerBullet : Bullet
{
    [Header("Configuration")]
    [SerializeField] private PlayerBulletStats stats;
    [SerializeField] private LayerMask enemyLayer;

    protected override float GetSpeed() => stats.speed;
    protected override float GetCollisionDelay() => 0.05f;

    protected override void HandleCollision(Collider other)
    {
        ImpactEffect impactEffect = PoolManager.Instance.Get<ImpactEffect>();
        if (impactEffect != null)
        {
            impactEffect.transform.position = transform.position;
            impactEffect.transform.rotation = Quaternion.identity;
        }

        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(stats.damage);
        }
    }

    public override void OnReturnToPool() {}

    public override void Disable()
    {
        base.Disable();
    }

    public override void ResetToDefault()
    {
        base.ResetToDefault();
    }
}

