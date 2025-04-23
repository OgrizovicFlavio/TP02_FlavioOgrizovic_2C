using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour, IReusable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject deathEffect;

    private ScoreManager scoreManager;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        enemyHealthBar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        enemyHealthBar.SetHealthBar(maxHealth, currentHealth);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if(scoreManager != null)
        {
            scoreManager.AddScore(10);
        }

        GetComponent<Enemy>().ReturnToPool();
    }

    public void OnSpawn()
    {
        currentHealth = maxHealth;
        enemyHealthBar.SetHealthBar(maxHealth, currentHealth);
    }

    public void OnReturn() { }
}
