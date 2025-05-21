using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static event Action OnDie;

    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("References")]
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask worldLayerMask;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private Image healthBar;

    private float currentHealth;
    private bool isInvulnerable;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isInvulnerable = false;
        currentHealth = stats.maxHealth;
        UpdateHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utilities.CheckLayerInMask(enemyLayerMask, other.gameObject.layer))
        {
            Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
            float damage = LevelManager.Instance.Current.playerCollisionDamage;
            TakeDamage(damage, knockbackDir * 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Utilities.CheckLayerInMask(worldLayerMask, collision.gameObject.layer))
        {
            Vector3 knockbackDir = (transform.position - collision.transform.position).normalized;
            float damage = LevelManager.Instance.Current.playerCollisionDamage;
            TakeDamage(damage, knockbackDir);
        }
    }

    public void TakeDamage(float amount, Vector3 knockbackDirection)
    {
        if (isInvulnerable) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, stats.maxHealth);

        UpdateHealthBar();

        if (cameraShake != null)
        {
            cameraShake.Shake();
        }

        rb.AddForce(knockbackDirection * stats.knockbackForce, ForceMode.Impulse);
        StartCoroutine(InvulnerabilityCoroutine());

        if (currentHealth <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void TakeDamage(float amount) //IDamageable
    {
        Vector3 noKnockback = Vector3.zero;
        TakeDamage(amount, noKnockback);
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float elapsed = 0f;
        bool visible = true;
        float blinkInterval = 0.1f;

        while (elapsed < stats.invulnerabilityTime)
        {
            visible = !visible;
            if (playerRenderer != null)
                playerRenderer.enabled = visible;

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (playerRenderer != null)
            playerRenderer.enabled = true;

        isInvulnerable = false;
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float fill = (float)currentHealth / stats.maxHealth;
            healthBar.fillAmount = fill;
        }
    }
}
