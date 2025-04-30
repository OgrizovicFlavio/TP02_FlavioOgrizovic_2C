using UnityEngine;

public class ImpactEffect : MonoBehaviour, IPooleable
{
    private ParticleSystem impactEffect;

    private void Awake()
    {
        impactEffect = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (impactEffect != null && !impactEffect.IsAlive())
        {
            PoolManager.Instance.ReturnToPool(this);
        }
    }

    public void OnGetFromPool()
    {
        if (impactEffect != null)
            impactEffect.Play();
    }

    public void OnReturnToPool()
    {
        if (impactEffect != null)
            impactEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Disable()
    {
        if (impactEffect != null)
            impactEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void ResetToDefault() {}
}
