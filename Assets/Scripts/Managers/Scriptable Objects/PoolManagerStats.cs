using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerStats", menuName = "Data/PoolManagerStats")]

public class PoolManagerStats : ScriptableObject
{
    [Serializable]
    public class PoolEntry
    {
        public MonoBehaviour prefab;
        public int count;
    }

    public List<PoolEntry> pools = new List<PoolEntry>();
}
