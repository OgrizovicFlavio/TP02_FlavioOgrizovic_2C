using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStats stats;

    [Header("Player Body")]
    [SerializeField] private Transform playerBody;

    [Header("Cameras")]
    [SerializeField] private Transform firstPersonCam;
    [SerializeField] private Transform thirdPersonCam;

    private Transform currentCam;
    private float verticalRotation = 0f;
    private bool isThirdPerson = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentCam = firstPersonCam;
        EnableCamera(firstPersonCam, true);
        EnableCamera(thirdPersonCam, false);

        CameraManager.SetCamera(firstPersonCam.GetComponent<Camera>());
    }

    private void Start()
    {
        UpdateAllHealthBarsCamera(firstPersonCam.GetComponent<Camera>());
    }

    private void Update()
    {
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.C))
        {
            isThirdPerson = !isThirdPerson;
            currentCam = isThirdPerson ? thirdPersonCam : firstPersonCam;
            EnableCamera(firstPersonCam, !isThirdPerson);
            EnableCamera(thirdPersonCam, isThirdPerson);

            Camera activeCamera = currentCam.GetComponent<Camera>();
            CameraManager.SetCamera(activeCamera);

            UpdateAllHealthBarsCamera(activeCamera);

            PlayerAim aim = FindObjectOfType<PlayerAim>();
            if (aim != null)
                aim.SetCamera(activeCamera);

            PlayerShoot shoot = FindObjectOfType<PlayerShoot>();
            if (shoot != null)
                shoot.SetCamera(activeCamera);
        }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * stats.horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * stats.verticalSensitivity;

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, stats.minVerticalAngle, stats.maxVerticalAngle);

        if (currentCam != null)
        {
            currentCam.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    private void EnableCamera(Transform camTransform, bool enable)
    {
        if (camTransform != null)
        {
            Camera cam = camTransform.GetComponent<Camera>();
            if (cam != null)
                cam.enabled = enable;

            AudioListener listener = camTransform.GetComponent<AudioListener>();
            if (listener != null)
                listener.enabled = enable;
        }
    }

    private void UpdateAllHealthBarsCamera(Camera activeCam)
    {
        EnemyHealthBar[] healthBars = FindObjectsOfType<EnemyHealthBar>();
        foreach (EnemyHealthBar hb in healthBars)
        {
            hb.SetCamera(activeCam);
        }
    }
}
