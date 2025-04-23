using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;

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
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // centro de la pantalla
        Vector3 direction = ray.direction;

        GameObject bulletGO = PoolController.Instance.GetObjectFromPool(ObjectType.PlayerBullet);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = Quaternion.LookRotation(direction);
        bulletGO.SetActive(true);

        PlayerBullet bullet = bulletGO.GetComponent<PlayerBullet>();
        if (bullet != null)
        {
            bullet.SetDirection(direction);
        }
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
}
