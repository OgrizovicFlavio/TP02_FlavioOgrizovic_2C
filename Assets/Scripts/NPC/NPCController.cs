using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour, IReusable
{
    [SerializeField] private NPCType npcType = NPCType.Civilian;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float wanderRadius = 15f;
    [SerializeField] private float idleTime = 2f;

    private Vector3 targetPosition;
    private float idleTimer;
    private bool isMoving = false;
    private bool isDead = false;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
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
            {
                PickNewDestination();
            }
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
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        targetPosition = transform.position + new Vector3(randomOffset.x, 0, randomOffset.y);
        isMoving = true;
    }

    private void SetState(NPCState state)
    {
        animator.SetInteger("State", (int)state);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        SetState(NPCState.Dying);
        GetComponent<Collider>().enabled = false;

        if (scoreManager != null)
        {
            if (npcType == NPCType.Alien)
            {
                scoreManager.AddScore(5);
            }
            else if (npcType == NPCType.Civilian)
            {
                scoreManager.AddScore(-10);
            }
        }

        StartCoroutine(DelayedReturnToPool());
    }

    private IEnumerator DelayedReturnToPool()
    {
        yield return new WaitForSeconds(2f);
        PoolController.Instance.ReturnObjectToPool(gameObject, ObjectTypeFromType());
    }

    private ObjectType ObjectTypeFromType()
    {
        switch (npcType)
        {
            case NPCType.Alien: return ObjectType.AlienNPC;
            case NPCType.Civilian: return ObjectType.CivilianNPC;
            default: return ObjectType.CivilianNPC;
        }
    }

    public void OnSpawn()
    {
        isDead = false;
        GetComponent<Collider>().enabled = true;
        SetState(NPCState.Idle);
    }

    public void OnReturn()
    {
        isDead = true;
        SetState(NPCState.Idle);
    }
}
