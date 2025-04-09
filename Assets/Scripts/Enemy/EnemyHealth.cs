using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private GameObject deathEffect;

    private EnemyCounter enemyCounter;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyCounter = FindObjectOfType<EnemyCounter>();
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealthBar(maxHealth, currentHealth);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (enemyCounter != null)
        {
            enemyCounter.CountKill();
        }

        GetComponent<Enemy>().ReturnToPool();
    }
}
