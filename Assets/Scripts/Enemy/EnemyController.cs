using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Walkable walkable;
    [SerializeField] private float stoppingDistance = 1.5f;

    private Transform target;

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;  //Calculo la direcci�n desde el enemigo hasta el objetivo.      

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
}
