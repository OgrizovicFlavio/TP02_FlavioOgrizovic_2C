using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private GameObject smokeEffectPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float shootingRange = 100f;

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
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingRange, destroyableLayer))
        {

            PlayerBullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = firePoint.position;
            bullet.Set(hit.transform);

            if (smokeEffectPrefab != null)
            {
                Instantiate(smokeEffectPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
}
