using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviourSingleton<PoolManager>
{
    private Dictionary<Type, IPooleable> prefabLookup = new();
    private Dictionary<Type, Queue<IPooleable>> pool = new();
    private List<IPooleable> activeObjects = new();

    protected override void OnAwaken() {}

    public void InitializePool<T>(T prefab, int minSize = 10) where T : MonoBehaviour, IPooleable
    {
        Type type = typeof(T);

        if (!prefabLookup.ContainsKey(type))
            prefabLookup.Add(type, prefab);

        if (!pool.ContainsKey(type))
            pool.Add(type, new Queue<IPooleable>());

        for (int i = 0; i < minSize; i++)
        {
            T instance = Instantiate(prefab, transform);
            instance.ResetToDefault();
            instance.gameObject.SetActive(false);
            pool[type].Enqueue(instance);
        }
    }

    public T Get<T>(Vector3 position, Quaternion rotation) where T : MonoBehaviour, IPooleable
    {
        Type type = typeof(T);
        IPooleable obj;

        if (pool.ContainsKey(type) && pool[type].Count > 0)
        {
            obj = pool[type].Dequeue();
        }
        else if (prefabLookup.ContainsKey(type))
        {
            obj = Instantiate((prefabLookup[type] as MonoBehaviour).gameObject).GetComponent<IPooleable>();
        }
        else
        {
            return null;
        }

        var monobehaviour = obj as MonoBehaviour;

        if (monobehaviour == null || monobehaviour.gameObject == null)
        {
            Debug.LogWarning($"[PoolManager] Tried to use a pooled object of type {typeof(T).Name}, but it was destroyed.");
            return null;
        }

        var go = monobehaviour.gameObject;
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.SetActive(true);
        obj.OnGetFromPool();
        activeObjects.Add(obj);

        return obj as T;
    }

    public void ReturnToPool<T>(T obj) where T : MonoBehaviour, IPooleable
    {
        obj.OnReturnToPool();
        obj.Disable();

        GameObject go = (obj as MonoBehaviour).gameObject;
        go.SetActive(false);

        Type type = typeof(T);
        if (!pool.ContainsKey(type))
            pool[type] = new Queue<IPooleable>();

        pool[type].Enqueue(obj);
        activeObjects.Remove(obj);
    }

    public void ReturnAllActiveObjects()
    {
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            var obj = activeObjects[i];
            if (obj == null || (obj as MonoBehaviour) == null)
                continue;

            obj.OnReturnToPool();
            obj.Disable();
            (obj as MonoBehaviour).gameObject.SetActive(false);

            Type type = obj.GetType();
            if (!pool.ContainsKey(type))
                pool[type] = new Queue<IPooleable>();

            pool[type].Enqueue(obj);
        }

        activeObjects.Clear();
    }
}
