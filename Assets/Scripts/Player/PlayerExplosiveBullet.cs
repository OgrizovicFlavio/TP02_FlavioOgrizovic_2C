using UnityEngine;

public class PlayerExplosiveBullet : Bullet
{
    [Header("Configuration")]
    [SerializeField] private PlayerExplosiveBulletStats stats;

    protected override void FixedUpdate()
    {
        rb.velocity = direction * GetSpeed();
    }

    protected override float GetSpeed() => stats.speed;
    protected override float GetCollisionDelay() => 0.05f;

    protected override void HandleCollision(Collider other)
    {
        PoolManager.Instance.Get<ExplosiveImpactEffect>(transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, stats.explosionRadius);
        foreach (var hit in hits)
        {
            IDamageable target = hit.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(stats.damage);
            }
        }
    }

    public override void OnGetFromPool()
    {
        base.OnGetFromPool();

        rb.angularVelocity = Random.onUnitSphere * 10f;
        rb.velocity = direction * GetSpeed();
    }

    public override void OnReturnToPool() { }

    public override void Disable()
    {
        base.Disable();
    }

    public override void ResetToDefault()
    {
        base.ResetToDefault();
    }
}
