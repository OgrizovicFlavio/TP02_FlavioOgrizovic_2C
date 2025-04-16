using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private float knockbackForce = 10f;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private PauseManager pauseManager;

    private int currentLives;
    private bool isInvulnerable;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isInvulnerable = false;
        currentLives = maxLives;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utilities.CheckLayerInMask(enemyLayerMask, other.gameObject.layer))
        {
            Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
            TakeDamage(1, knockbackDir);
        }
    }

    public void TakeDamage(int amount, Vector3 knockbackDirection)
    {
        if (isInvulnerable) return;

        currentLives -= amount;
        Debug.Log("¡Vida perdida! Vidas restantes: " + currentLives);
        UpdateUI();

        if (cameraShake != null)
        {
            cameraShake.Shake();
        }

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        StartCoroutine(InvulnerabilityCoroutine());

        if (currentLives <= 0)
        {
            pauseManager.GameOver();
        }
    }

    public void TakeDamage(int amount)
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

        while (elapsed < invulnerabilityTime)
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

    private void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = "LIVES: " + currentLives;
        }
    }
}
