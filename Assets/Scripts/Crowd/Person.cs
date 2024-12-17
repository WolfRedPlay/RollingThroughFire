using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(NavMeshAgent))]
public class Person : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] wayPoints;

    private int CurrentPoint = 0;
    private NavMeshAgent agent;
    private float StopDistance = 1.0f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        if (wayPoints.Length > 0)
        {
            agent.SetDestination(wayPoints[CurrentPoint]);
            RotateTowards(wayPoints[CurrentPoint]);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[CurrentPoint]) <= StopDistance)
        {
            CurrentPoint++;
            if (CurrentPoint < wayPoints.Length)
            {
                agent.SetDestination(wayPoints[CurrentPoint]);
                
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (CurrentPoint < wayPoints.Length)
        {
            RotateTowards(wayPoints[CurrentPoint]);
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
}
