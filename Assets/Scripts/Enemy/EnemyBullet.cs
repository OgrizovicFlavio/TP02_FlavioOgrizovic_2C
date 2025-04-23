using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float collisionDelay = 0.05f;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody rb;
    private Vector3 direction;
    private float timer;
    private float spawnTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            PoolController.Instance.ReturnObjectToPool(gameObject, ObjectType.EnemyBullet);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - spawnTime < collisionDelay)
            return;

        if (Utilities.CheckLayerInMask(playerLayer, other.gameObject.layer))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized;
                player.TakeDamage((int)damage, knockbackDir);
            }
        }

        PoolController.Instance.ReturnObjectToPool(gameObject, ObjectType.EnemyBullet);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void OnSpawn()
    {
        timer = 0f;
        spawnTime = Time.time;
    }

    public void OnReturn() { }
}
