using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStats stats;

    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private PlayerLaserBullet bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float nextFireTime = 0f;
    private float nextExplosiveTime = 0f;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            ShootLaser();
            nextFireTime = Time.time + stats.fireRate;
        }

        if (Time.time >= nextExplosiveTime && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShootExplosive();
            nextExplosiveTime = Time.time + stats.explosiveCooldown;
        }
    }

    private void ShootLaser()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;

        PlayerLaserBullet bullet = PoolManager.Instance.Get<PlayerLaserBullet>(firePoint.position, Quaternion.LookRotation(direction));

        bullet.SetDirection(direction);
    }

    private void ShootExplosive()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;

        PlayerExplosiveBullet bullet = PoolManager.Instance.Get<PlayerExplosiveBullet>(firePoint.position, Quaternion.LookRotation(direction));

        bullet.SetDirection(direction);
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
}
