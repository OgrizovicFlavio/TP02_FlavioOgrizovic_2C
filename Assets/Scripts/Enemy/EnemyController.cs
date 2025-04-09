using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public Walkable walkable;
    public float stoppingDistance = 1.5f;

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;       

        if (direction.magnitude > stoppingDistance)
        {
            walkable.MoveTo(direction.normalized);
        }
        else
        {
            walkable.Stop();
        }
    }
}
