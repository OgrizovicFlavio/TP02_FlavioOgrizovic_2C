using UnityEngine;

public abstract class Bullet : MonoBehaviour, IPooleable
{
    protected Rigidbody rb;
    protected Vector3 direction;
    protected float spawnTime;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * GetSpeed() * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public virtual void OnGetFromPool()
    {
        spawnTime = Time.time;
        direction = Vector3.zero;
        rb.Sleep();
    }

    public virtual void OnReturnToPool()
    {

    }

    public virtual void Disable()
    {
        rb.Sleep();
    }

    public virtual void ResetToDefault()
    {
        direction = Vector3.zero;
        spawnTime = 0f;
    }

    protected abstract float GetSpeed();
    protected abstract void HandleCollision(Collider other);
    protected abstract float GetCollisionDelay();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (Time.time - spawnTime < GetCollisionDelay())
            return;

        HandleCollision(other);
        PoolManager.Instance.ReturnToPool(this);
    }
}
