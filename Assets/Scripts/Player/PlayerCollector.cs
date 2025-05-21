using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private LayerMask coinLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (Utilities.CheckLayerInMask(coinLayer, other.gameObject.layer))
        {
            IPickable coin = other.GetComponent<IPickable>();
            if (coin != null)
            {
                coin.OnPick();
            }
        }
    }
}
