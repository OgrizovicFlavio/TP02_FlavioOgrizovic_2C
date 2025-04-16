using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Constraints")]
    [SerializeField] private float xMin = -49f;
    [SerializeField] private float xMax = 49f;
    [SerializeField] private float zMin = -49f;
    [SerializeField] private float zMax = 49f;
    [SerializeField] private float yMin = 0f;
    [SerializeField] private float yMax = 60f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Vector3 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        pos.z = Mathf.Clamp(pos.z, zMin, zMax);
    
        rb.position = pos;
    }

    private void Move()
    {
        float z = Input.GetAxis("Vertical");
        float y = 0;
        float x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space)) // SUBE
            y = 1;
        else if (Input.GetKey(KeyCode.LeftControl)) // BAJA
            y = -1;

        if (Mathf.Abs(z) < 0.01f && Mathf.Abs(x) < 0.01f && Mathf.Abs(y) < 0.01f)
            return;


        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * z + right * x + Vector3.up * y).normalized;
        
        rb.AddForce(direction * speed, ForceMode.Acceleration);

        Vector3 velocity = rb.velocity;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
