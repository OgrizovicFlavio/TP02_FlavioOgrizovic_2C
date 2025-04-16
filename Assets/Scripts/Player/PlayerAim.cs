using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask destroyableLayerMask;

    [Header("Laser Sight")]
    [SerializeField] private GameObject laserSightPrefab;
    [SerializeField] private float range = 100f;

    [Header("UI Crosshair")]
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.red;

    private GameObject laserInstance;
    private LineRenderer lineRenderer;
    private bool laserEnabled = true;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Start()
    {
        laserInstance = Instantiate(laserSightPrefab);
        lineRenderer = laserInstance.GetComponent<LineRenderer>();
        laserInstance.SetActive(true);
    }

    private void LateUpdate()
    {
        HandleLaserToggle();

        if (laserEnabled)
        {
            UpdateLaserPreview();
            if (crosshairImage != null)
                crosshairImage.enabled = false;
        }
        else
        {
            if (crosshairImage != null)
            {
                crosshairImage.enabled = true;
                UpdateCrosshair();
            }
        }
    }

    private void HandleLaserToggle()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            laserEnabled = !laserEnabled;
            laserInstance.SetActive(laserEnabled);
        }
    }

    private void UpdateLaserPreview()
    {
        Ray ray = new Ray(firePoint.position, cam.transform.forward);
        Vector3 endPoint = firePoint.position + cam.transform.forward * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            endPoint = hit.point;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPoint);
    }

    private void UpdateCrosshair()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, range, destroyableLayerMask))
        {
            crosshairImage.color = highlightColor;
        }
        else
        {
            crosshairImage.color = normalColor;
        }
    }

    public void SetCamera(Camera newCam)
    {
        cam = newCam;
    }
}
