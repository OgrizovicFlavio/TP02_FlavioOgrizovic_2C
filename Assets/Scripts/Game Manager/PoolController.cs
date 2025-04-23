using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public static PoolController Instance;

    [SerializeField] private List<PoolPrefab> poolPrefabs = new List<PoolPrefab>();
    private Dictionary<ObjectType, List<GameObject>> poolDeactivated = new Dictionary<ObjectType, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        foreach (PoolPrefab poolPrefab in poolPrefabs)
        {
            poolDeactivated[poolPrefab.objectType] = new List<GameObject>();

            for (int j = 0; j < poolPrefab.initialCount; j++)
            {
                CreateObject(poolPrefab);
            }
        }
    }

    private GameObject CreateObject(PoolPrefab poolPrefab)
    {
        GameObject go = Instantiate(poolPrefab.prefab);
        go.SetActive(false);
        poolDeactivated[poolPrefab.objectType].Add(go);
        return go;
    }

    public GameObject GetObjectFromPool(ObjectType type)
    {
        GameObject objectFromPool = null;

        if (poolDeactivated[type].Count > 0)
        {
            objectFromPool = poolDeactivated[type][0];
            poolDeactivated[type].RemoveAt(0);
        }
        else
        {
            PoolPrefab prefab = poolPrefabs.Find(p => p.objectType == type);
            objectFromPool = CreateObject(prefab);
        }

        objectFromPool.SetActive(true);

        if (objectFromPool.TryGetComponent<IReusable>(out var reusable))
        {
            reusable.OnSpawn();
        }

        return objectFromPool;
    }

    public void ReturnObjectToPool(GameObject objectToReturn, ObjectType type)
    {
        if (objectToReturn.TryGetComponent<IReusable>(out var reusable))
        {
            reusable.OnReturn();
        }

        objectToReturn.SetActive(false);
        poolDeactivated[type].Add(objectToReturn);
    }
}
