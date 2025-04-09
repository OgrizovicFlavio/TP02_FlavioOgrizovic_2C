using UnityEngine;

public class Walkable : MonoBehaviour
{
    private const float ForcePower = 10f;

    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] float force = 2f;

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredVelocity = direction * speed;
        Vector3 deltaVelocity = desiredVelocity - rb.velocity;

        Vector3 moveForce = deltaVelocity * (force * ForcePower * Time.fixedDeltaTime);
        rb.AddForce(moveForce);
    }

    public void MoveTo(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Stop()
    {
        MoveTo(Vector3.zero);
    }
}
