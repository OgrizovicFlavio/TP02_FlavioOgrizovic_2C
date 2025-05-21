using UnityEngine;

public class Coin : MonoBehaviour, IPooleable, IPickable
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private ParticleSystem coinEffect;
    [SerializeField] private Collider coinCollider;

    private Transform visual;

    private void Awake()
    {
        visual = transform.GetChild(0);
    }

    private void Update()
    {
        if (visual != null)
        {
            visual.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    public void OnPick()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoin(1);
        }

        PoolManager.Instance.ReturnToPool(this);
    }

    public void OnGetFromPool()
    {
        if (coinEffect != null && !coinEffect.isPlaying)
            coinEffect.Play();

        coinCollider.enabled = true;
        gameObject.SetActive(true);
    }

    public void OnReturnToPool()
    {
        if (coinEffect != null && coinEffect.isPlaying)
            coinEffect.Stop();

        coinCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void ResetToDefault()
    {

    }
}
