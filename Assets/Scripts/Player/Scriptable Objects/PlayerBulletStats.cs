using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletStats", menuName = "Data/PlayerBulletStats")]

public class PlayerBulletStats : ScriptableObject
{
    [Header("Stats")]
    public float speed = 30f;
    public float damage = 25f;
}
