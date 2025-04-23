using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectType type;

    public void ReturnToPool()
    {
        PoolController.Instance.ReturnObjectToPool(gameObject, type);
    }
}
