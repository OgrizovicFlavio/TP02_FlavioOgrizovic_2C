using UnityEngine;

[CreateAssetMenu(fileName = "NPCStats", menuName = "Data/NPCStats", order = 1)]
public class NPCStats : ScriptableObject
{
    public string npcName;
    public float moveSpeed = 2f;
    public float wanderRadius = 15f;
    public float idleTime = 2f;
}
