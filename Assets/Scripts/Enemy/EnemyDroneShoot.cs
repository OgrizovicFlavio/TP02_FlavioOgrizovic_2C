using UnityEngine;

public class EnemyDroneShoot : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private EnemyDroneStats stats;
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

        EnemyDroneBullet bullet = PoolManager.Instance.Get<EnemyDroneBullet>(firePoint.position, Quaternion.LookRotation(shootDirection));

        bullet.SetDirection(shootDirection);
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
