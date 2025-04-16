using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject smokeEffectPrefab;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float shootingRange = 50f;

    private float nextFireTime = 0f;
    private Transform player;

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= shootingRange && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Enemy is shooting at: " + player.name);

        Ray ray = new Ray(firePoint.position, firePoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingRange, destroyableLayer))
        {
            EnemyBullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = firePoint.position;
            bullet.Set(hit.transform);

            if (smokeEffectPrefab != null)
            {
                Instantiate(smokeEffectPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
