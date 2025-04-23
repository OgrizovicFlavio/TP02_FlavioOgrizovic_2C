using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IReusable
{
    [SerializeField] private Walkable walkable;
    [SerializeField] private Transform visualModel;
    [SerializeField] private float stoppingDistance = 1.5f;
    [SerializeField] private float rotationSpeed = 5f;

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
            visualModel.rotation = Quaternion.Slerp(visualModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (direction.magnitude > stoppingDistance) //Si est� m�s lejos que la distancia de parada, sigue movi�ndose.
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

    public void OnSpawn()
    {

    }

    public void OnReturn()
    {
        SetTarget(null);
        walkable.Stop();
    }
}
