using UnityEngine;

public class EnemyDroneBullet : Bullet
{
    [Header("Configuration")]
    [SerializeField] private EnemyBulletStats stats;
    [SerializeField] private LayerMask playerLayer;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stats.lifeTime)
        {
            PoolManager.Instance.ReturnToPool(this);
        }
    }

    protected override float GetSpeed() => stats.speed;

    protected override float GetCollisionDelay() => stats.collisionDelay;

    protected override void HandleCollision(Collider other)
    {
        if (Utilities.CheckLayerInMask(playerLayer, other.gameObject.layer))
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;

                if (target is PlayerHealth player)
                    player.TakeDamage(stats.damage, knockbackDir);
                else
                    target.TakeDamage(stats.damage);
            }
        }

        PoolManager.Instance.ReturnToPool(this);
    }

    public override void OnGetFromPool()
    {
        base.OnGetFromPool();
        timer = 0f;
    }

    public override void OnReturnToPool()
    {
        timer = 0f;
    }

    public override void ResetToDefault()
    {
        base.ResetToDefault();
        timer = 0f;
    }
}
