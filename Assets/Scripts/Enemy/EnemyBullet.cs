using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy bullet hit: " + other.gameObject.name);
        if (Utilities.CheckLayerInMask(playerLayer, other.gameObject.layer))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                player.TakeDamage((int)damage, knockbackDir);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Set(Transform target)
    {
        direction = (target.position - transform.position).normalized;
    }
}
