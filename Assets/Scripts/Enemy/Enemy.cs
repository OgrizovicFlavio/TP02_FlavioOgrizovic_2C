using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public IObjectPool<Enemy> Pool { get; set; }

    public void ReturnToPool()
    {
        Pool.Release(this);
    }
}
