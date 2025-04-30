using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Walkable walkable;
    [SerializeField] private Transform visualModel;

    private Transform target;

    private void OnEnable()
    {
        StartCoroutine(WaitForTarget());
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;  //Calculo la direcci�n desde el enemigo hasta el objetivo.
                                                                   
        if (visualModel != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            visualModel.rotation = Quaternion.Slerp(visualModel.rotation, targetRotation, stats.rotationSpeed * Time.deltaTime);
        }

        if (direction.magnitude > stats.stoppingDistance) //Si est� m�s lejos que la distancia de parada, sigue movi�ndose.
        {
            walkable.MoveTo(direction.normalized); //Mueve al enemigo en la direcci�n normalizada.
        }
        else
        {
            walkable.Stop(); //Si est� cerca, se detiene.
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetTarget()
    {
        StopAllCoroutines();
        SetTarget(null);
        walkable.Stop();

        StartCoroutine(WaitForTarget());
    }

    private IEnumerator WaitForTarget()
    {
        // Espera hasta que el PlayerLocator tenga una referencia v�lida
        while (PlayerLocator.Instance == null || PlayerLocator.Instance.PlayerTransform == null)
            yield return null;

        SetTarget(PlayerLocator.Instance.PlayerTransform);

        EnemyShoot shooter = GetComponent<EnemyShoot>();
        if (shooter != null)
            shooter.SetTarget(PlayerLocator.Instance.PlayerTransform);
    }
}
