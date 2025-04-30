using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStats stats;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Effects")]
    [SerializeField] private ParticleSystem[] thrusters;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        HoverIfTooLow();
    }

    private void LateUpdate()
    {
        Vector3 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, stats.xMin, stats.xMax);
        pos.y = Mathf.Clamp(pos.y, stats.yMin, stats.yMax);
        pos.z = Mathf.Clamp(pos.z, stats.zMin, stats.zMax);

        rb.position = pos;
    }

    private void Move()
    {
        float z = Input.GetAxis("Vertical");

        bool isThrusting = z > 0.1f;

        foreach (var thrust in thrusters)
        {
            if (isThrusting && !thrust.isPlaying)
            {
                thrust.Play();
            }
            else if (!isThrusting && thrust.isPlaying)
            {
                thrust.Stop();
            }
        }

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

        if (direction != Vector3.zero)
        {
            rb.AddForce(direction * stats.speed, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity *= 0.95f;
        }

        Vector3 velocity = rb.velocity;

        if (rb.velocity.magnitude > stats.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * stats.maxSpeed;
        }

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        if (animator != null)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            animator.SetFloat("Horizontal", inputX);
            animator.SetFloat("Vertical", inputZ);
        }
    }

    private void HoverIfTooLow()
    {
        if (rb.position.y < stats.hoverMinHeight)
        {
            rb.AddForce(Vector3.up * stats.hoverForce, ForceMode.Acceleration);
        }
    }
}
