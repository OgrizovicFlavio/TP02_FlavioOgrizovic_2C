using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2.0f;

    private float target = 1;
    private Camera cam;

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        if (cam != null)
        {
            Vector3 dir = transform.position - cam.transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }

        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    public void SetHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        healthBarSprite.fillAmount = target;
    }

    public void SetCamera(Camera newCamera)
    {
        cam = newCamera;
    }
}
