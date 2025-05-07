using UnityEngine;

public class PlayerLaserBullet : Bullet
{
    [Header("Configuration")]
    [SerializeField] private PlayerLaserBulletStats stats;
    [SerializeField] private LayerMask enemyLayer;

    protected override float GetSpeed() => stats.speed;
    protected override float GetCollisionDelay() => 0.05f;

    protected override void HandleCollision(Collider other)
    {
        PoolManager.Instance.Get<LaserImpactEffect>(transform.position, Quaternion.identity);

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

