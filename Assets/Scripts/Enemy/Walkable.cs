using UnityEngine;

public class Walkable : MonoBehaviour
{
    private const float ForcePower = 10f;

    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float force = 2f;

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredVelocity = direction * speed; //Velocidad deseada en base a la dirección actual.
        Vector3 deltaVelocity = desiredVelocity - rb.velocity; //Diferencia entre la velocidad deseada y la actual del rigidbody.

        Vector3 moveForce = deltaVelocity * (force * ForcePower * Time.fixedDeltaTime); //Fuerza proporcional a esa diferencia.
        rb.AddForce(moveForce); //Aplico fuerza.
    }

    public void MoveTo(Vector3 newDirection) //Asigna una dirección al movimiento.
    {
        direction = newDirection;
    }

    public void Stop() //Detiene el movimiento.
    {
        MoveTo(Vector3.zero);
    }
}
