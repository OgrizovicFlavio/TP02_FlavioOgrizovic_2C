using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform firePoint;

    private float nextFireTime = 0f;
    private Transform player;

    private void Update()
    {
        if (player == null) 
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stats.shootingRange && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + stats.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 shootDirection = (player.position - firePoint.position).normalized;

        EnemyBullet bullet = PoolManager.Instance.Get<EnemyBullet>();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
        bullet.SetDirection(shootDirection);
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
