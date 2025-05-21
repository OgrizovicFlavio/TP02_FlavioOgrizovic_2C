using UnityEngine;

public class ImpactEffect : MonoBehaviour, IPooleable
{
    private ParticleSystem impactEffect;

    private void Awake()
    {
        impactEffect = GetComponent<ParticleSystem>();
        if (impactEffect != null)
        {
            var main = impactEffect.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.ReturnToPool(this);
    }

    public void OnGetFromPool()
    {
        if (impactEffect != null)
        {
            impactEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            impactEffect.Play();
        }
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

    public void ResetToDefault() { }
}
