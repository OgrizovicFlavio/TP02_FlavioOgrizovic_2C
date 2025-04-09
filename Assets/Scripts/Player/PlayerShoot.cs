using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayerMask;

    [SerializeField] private Image crosshairImage;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.red;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserDuration = 1f;
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private GameObject impactEffect;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        UpdateCrosshair();

        if (Input.GetButtonDown("Fire1")) // CLICK IZQUIERDO
        {
            Shoot();
        }
    }

    private void UpdateCrosshair()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward); //Rayo desde el firepoint hacia adelante.

        if (Physics.Raycast(ray, out RaycastHit hit, range, destroyableLayerMask))
        {
            crosshairImage.color = highlightColor; //Si apunta a un objeto con Enemy mask, la mira se pone roja.
        }
        else
        {
            crosshairImage.color = normalColor; //Blanco por default.
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward); //Rayo del disparo.

        smokeEffect.Play();

        Vector3 endPoint = firePoint.position + firePoint.forward * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range, destroyableLayerMask)) 
        {
            endPoint = hit.point; //Si impacta, termina.

            EnemyHealth enemy = hit.transform.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); //Aplica daño
            }

            GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 2f);
        }

        StartCoroutine(ShowLaser(firePoint.position, endPoint));
    }

    private IEnumerator ShowLaser(Vector3 start, Vector3 end)
    {
        GameObject laser = Instantiate(laserPrefab);
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);
    }
}
