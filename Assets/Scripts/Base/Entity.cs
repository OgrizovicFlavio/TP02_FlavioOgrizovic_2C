using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour, IPooleable
{
    [Header("General")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected int scoreOnDeath = 0;

    protected bool isDead = false;
    protected ScoreManager scoreManager;

    public virtual void OnSpawn()
    {
        isDead = false;
        scoreManager = FindObjectOfType<ScoreManager>();
        EnableCollider(true);
    }

    public virtual void OnReturn()
    {
        isDead = true;
        EnableCollider(false);
    }

    public virtual void Die()
    {
        if (isDead) 
            return;

        isDead = true;

        EnableCollider(false);

        AddScore();

        HandleReturn();
    }

    protected virtual void AddScore()
    {
        if (scoreManager != null)
            scoreManager.AddScore(scoreOnDeath);
    }

    protected virtual void HandleReturn()
    {
        StartCoroutine(DelayedReturn(4f));
    }

    protected virtual IEnumerator DelayedReturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.ReturnToPool(this);
    }

    protected virtual void EnableCollider(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
            col.enabled = state;
    }

    public virtual void OnGetFromPool()
    {
        isDead = false;
        scoreManager = FindObjectOfType<ScoreManager>();
        EnableCollider(true);
    }

    public virtual void OnReturnToPool()
    {
        isDead = true;
        EnableCollider(false);
    }

    public virtual void Disable()
    {
        EnableCollider(false);
    }

    public virtual void ResetToDefault()
    {
        isDead = false;
        scoreManager = null;
        EnableCollider(false);
    }
}
