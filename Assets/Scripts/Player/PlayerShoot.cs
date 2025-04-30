using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStats stats;

    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float nextFireTime = 0f;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + stats.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;

        PlayerBullet bullet = PoolManager.Instance.Get<PlayerBullet>();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.SetDirection(direction);
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
}
