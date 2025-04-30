using UnityEngine;

public class NPCController : Entity, IDamageable
{
    [Header("NPC Settings")]
    [SerializeField] private NPCType npcType = NPCType.Civilian;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float wanderRadius = 15f;
    [SerializeField] private float idleTime = 2f;

    private Vector3 targetPosition;
    private float idleTimer;
    private bool isMoving = false;

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
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.forward = direction;

        SetState(NPCState.Running);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            isMoving = false;
            idleTimer = idleTime;
            SetState(NPCState.Idle);
        }
    }

    private void PickNewDestination()
    {
        Vector2 offset = Random.insideUnitCircle * wanderRadius;
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

        SetState(NPCState.Dying);

        if (npcType == NPCType.Alien)
        {
            scoreOnDeath = 5;
        }
        else
        {
            scoreOnDeath = -10;
        }

        base.Die();
    }

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
