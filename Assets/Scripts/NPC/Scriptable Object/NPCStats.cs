using UnityEngine;

[CreateAssetMenu(fileName = "NPCStats", menuName = "Data/NPCStats", order = 1)]
public class NPCStats : ScriptableObject
{
    [Header("Name")]
    public string npcName;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float wanderRadius = 15f;
    public float idleTime = 2f;

    [Header("Death")]
    public int killValue = 0;
}
