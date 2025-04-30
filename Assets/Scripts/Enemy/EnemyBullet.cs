using UnityEngine;

public class EnemyBullet : Bullet
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
            OnReturnToPool();
        }
    }

    public void OnSpawn()
    {
        timer = 0f;
        spawnTime = Time.time;
    }

    public void OnReturn() { }

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
                {
                    player.TakeDamage((int)stats.damage, knockbackDir);
                }
                else
                {
                    target.TakeDamage(stats.damage);
                }
            }
        }
    }

    public override void OnReturnToPool()
    {
        timer = 0f;
    }

    public override void Disable()
    {
        base.Disable();
    }

    public override void ResetToDefault()
    {
        base.ResetToDefault();
        timer = 0f;
    }
}
