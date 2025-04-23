using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float shootingRange = 50f;
    [SerializeField] private ObjectType bulletType = ObjectType.EnemyBullet;

    private float nextFireTime = 0f;
    private Transform player;

    private void Update()
    {
        if (player == null) 
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= shootingRange && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 shootDirection = (player.position - firePoint.position).normalized;

        GameObject bulletGO = PoolController.Instance.GetObjectFromPool(bulletType);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = Quaternion.LookRotation(shootDirection);
        bulletGO.SetActive(true);

        EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();
        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
