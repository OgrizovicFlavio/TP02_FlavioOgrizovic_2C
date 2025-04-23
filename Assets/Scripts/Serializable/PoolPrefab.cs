using System;
using UnityEngine;

[Serializable]
public class PoolPrefab
{
    public GameObject prefab;
    public int initialCount;
    public ObjectType objectType;
}

public enum ObjectType
{
    PlayerBullet = 0,
    EnemyBullet = 1,
    EnemyDron = 2,
    AlienNPC = 3,
    CivilianNPC = 4
}
