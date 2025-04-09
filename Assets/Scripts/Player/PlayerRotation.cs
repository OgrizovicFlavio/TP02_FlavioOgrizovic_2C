using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float verticalSensitivity = 2f;
    [SerializeField] private float horizontalSensitivity = 2f;
    [SerializeField] private float minVerticalAngle = -80f;
    [SerializeField] private float maxVerticalAngle = 80f;
    [SerializeField] private Transform cam;

    private float verticalRotation = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

        transform.Rotate(Vector3.up * mouseX); // ROTAR EL CUERPO (HORIZONTAL)

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cam.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); //ROTAR LA CÁMARA (VERTICAL)
    }
}
