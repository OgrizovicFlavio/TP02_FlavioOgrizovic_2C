using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Data/PlayerStats")]

public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float speed = 20f;
    public float maxSpeed = 20f;

    [Header("Hover")]
    public float hoverMinHeight = 1.5f;
    public float hoverForce = 10f;

    [Header("Limits")]
    public float xMin = -150f;
    public float xMax = 150f;
    public float yMin = 0f;
    public float yMax = 50f;
    public float zMin = -75f;
    public float zMax = 75f;

    [Header("Rotation")]
    public float verticalSensitivity = 2f;
    public float horizontalSensitivity = 2f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    [Header("Health")]
    public int maxLives = 3;
    public float invulnerabilityTime = 1f;
    public float knockbackForce = 10f;

    [Header("Shoot")]
    public float fireRate = 0.25f;
    public float explosiveCooldown = 2f;
    public float laserSightRange = 100f;
}
