using UnityEngine;

public class NPCController : Entity, IDamageable
{
    public static event System.Action OnNPCDestroyed;

    [Header("Configuration")]
    [SerializeField] protected NPCStats stats;

    private Vector3 targetPosition;
    private float idleTimer;
    private bool isMoving = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetState(NPCState.Idle);
        PickNewDestination();
    }

    private void Update()
    {
        if (isDead) return;

        if (isMoving)
        {
            MoveTowardsTarget();
        }
        else
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
                PickNewDestination();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 nextPosition = transform.position + direction * stats.moveSpeed * Time.deltaTime;

        rb.MovePosition(nextPosition);
        transform.forward = direction;

        SetState(NPCState.Running);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            isMoving = false;
            idleTimer = stats.idleTime;
            SetState(NPCState.Idle);
        }
    }

    private void PickNewDestination()
    {
        Vector2 offset = Random.insideUnitCircle * stats.wanderRadius;
        targetPosition = transform.position + new Vector3(offset.x, 0, offset.y);
        isMoving = true;
    }

    private void SetState(NPCState state)
    {
        if (animator != null)
            animator.SetInteger("State", (int)state);
    }

    public void TakeDamage(float amount)
    {
        Die();
    }

    public override void Die()
    {
        if (isDead) return;

        OnNPCDestroyed?.Invoke();
        DropCoin();
        SetState(NPCState.Dying);
        base.Die();
    }

    protected virtual void DropCoin() { }

    public override void OnSpawn()
    {
        base.OnSpawn();
        SetState(NPCState.Idle);
    }

    public override void OnReturn()
    {
        base.OnReturn();
        SetState(NPCState.Idle);
    }
}
