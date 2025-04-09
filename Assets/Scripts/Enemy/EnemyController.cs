using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Walkable walkable;
    [SerializeField] private float stoppingDistance = 1.5f;

    private Transform target;

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;  //Calculo la dirección desde el enemigo hasta el objetivo.      

        if (direction.magnitude > stoppingDistance) //Si está más lejos que la distancia de parada, sigue moviéndose.
        {
            walkable.MoveTo(direction.normalized); //Mueve al enemigo en la dirección normalizada.
        }
        else
        {
            walkable.Stop(); //Si está cerca, se detiene.
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
