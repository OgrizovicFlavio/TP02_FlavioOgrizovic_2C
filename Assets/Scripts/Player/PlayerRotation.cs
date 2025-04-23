using UnityEngine;
using UnityEngine.UI;

public class PlayerRotation : MonoBehaviour
{
    [Header("Sensivity Settings")]
    [SerializeField] private float verticalSensitivity = 2f;
    [SerializeField] private float horizontalSensitivity = 2f;
    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;

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

            UpdateAllHealthBarsCamera(currentCam.GetComponent<Camera>());

            PlayerAim aim = FindObjectOfType<PlayerAim>();
            if (aim != null)
            {
                aim.SetCamera(currentCam.GetComponent<Camera>());
            }

            PlayerShoot shoot = FindObjectOfType<PlayerShoot>();
            if (shoot != null)
            {
                shoot.SetCamera(currentCam.GetComponent<Camera>());
            }
        }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * mouseX); // rota el jugador (horizontal)

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        if (currentCam != null)
        {
            currentCam.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // rota la cámara (vertical)
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
