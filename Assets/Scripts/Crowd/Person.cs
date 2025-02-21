using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Collections.Generic;


[RequireComponent(typeof(NavMeshAgent))]
public class Person : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] wayPoints;
    [HideInInspector]
    public List<Transform> OriginalWayPoints;

    private int CurrentPoint = 0;
    private NavMeshAgent agent;
    private float StopDistance = 1.5f;
    private float maxTimeToReachPoint = 10; // Max time to reach a point
    private float pointTimer = 0.0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            Debug.Log("Failed to find animator.");
        }

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        if (wayPoints.Length > 0)
        {
            if (NavMesh.SamplePosition(wayPoints[CurrentPoint], out NavMeshHit navHit, 3f, agent.areaMask))
            {
                agent.SetDestination(navHit.position);
                RotateTowards(navHit.position);
            }
            else
            {
                agent.SetDestination(OriginalWayPoints[CurrentPoint].position);
                RotateTowards(OriginalWayPoints[CurrentPoint].position);
            }
        }
    }

    void Update()
    {
        if (wayPoints.Length == 0)
        {
            return;
        }

        if (Vector3.Distance(transform.position, agent.destination) <= StopDistance)
        {
            MoveToNextPoint();
        }
        else
        {
            pointTimer += Time.deltaTime;
            if (pointTimer > maxTimeToReachPoint)
            {
                MoveToNextPoint();
            }
        }

        if (CurrentPoint < wayPoints.Length)
        {
            RotateTowards(wayPoints[CurrentPoint]);
        }
    }

    private void MoveToNextPoint()
    {
        pointTimer = 0.0f; 
        CurrentPoint++;
        if (CurrentPoint < wayPoints.Length)
        {
            if (NavMesh.SamplePosition(wayPoints[CurrentPoint], out NavMeshHit navHit, 3f, agent.areaMask))
            {
                agent.SetDestination(navHit.position);
            }
            else
            {
                agent.SetDestination(OriginalWayPoints[CurrentPoint].position);
            }
        }
        else
        {
            Destroy(gameObject);
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
