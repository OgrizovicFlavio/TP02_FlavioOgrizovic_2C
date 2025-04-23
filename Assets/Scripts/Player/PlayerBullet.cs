using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private LayerMask enemyLayer;

    private Transform target;
    private Rigidbody rb;
    private Vector3 direction;
    private float collisionDelay = 0.05f;
    private float spawnTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        spawnTime = Time.time;
        direction = Vector3.zero;
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - spawnTime < collisionDelay)
            return;

        NPCController npc = other.GetComponent<NPCController>();
        if (npc != null)
        {
            npc.Die();
        }
        else if (Utilities.CheckLayerInMask(enemyLayer, other.gameObject.layer))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        PoolController.Instance.ReturnObjectToPool(gameObject, ObjectType.PlayerBullet);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
}

