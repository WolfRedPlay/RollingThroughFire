using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Finding point in area around enemy", story: "Finding [point] in [area] around [enemy]", category: "Action", id: "f8463a008ee87c33981b2bc010cee664")]
public partial class PointInAreaOnNavMeshSurf : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> point;
    [SerializeReference] public BlackboardVariable<GameObject> Area;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<int> MaxAttempts = new BlackboardVariable<int>(10);
    [SerializeReference] public BlackboardVariable<float> MaxRadius = new BlackboardVariable<float>(10f);

    private Collider patrolCollider;
    private Transform enemyTransform;
    

    protected override Status OnStart()
    {
        
        if (Area.Value == null || Enemy.Value == null)
        {
            Debug.LogError("Area or Enemy is not assigned correctly.");
            return Status.Failure;
        }

        patrolCollider = Area.Value.GetComponent<Collider>();
        enemyTransform = Enemy.Value.transform;

        if (patrolCollider == null || !patrolCollider.isTrigger)
        {
            Debug.LogError("The patrol area must have a trigger collider attached.");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Debug.Log("404: progress started");
        if (point == null)
        {
            Debug.LogError("Point variable is not assigned.");
            return Status.Failure;
        }

        Vector3 patrolPoint = GetPatrolPoint();

        if (patrolPoint != Vector3.zero)
        {
            point.Value = patrolPoint;
            Debug.Log($"Patrol Point Found: {patrolPoint}");
            return Status.Success;
        }

        Debug.LogWarning("Failed to find a valid patrol point.");
        return Status.Failure;
    }

    private Vector3 GetPatrolPoint()
    {
        float patrolHeight = patrolCollider.bounds.max.y;

        for (int i = 0; i < MaxAttempts; i++)
        {
            // Generate a random point around the enemy at patrolHeight
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * MaxRadius;
            randomOffset.y = 0;
            Vector3 randomPoint = enemyTransform.position + randomOffset;
            randomPoint.y = patrolHeight;

            // Raycast downward to find the floor
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 pointOnFloor = hit.point;

                // Check if the point is within the patrol collider
                if (patrolCollider.bounds.Contains(pointOnFloor))
                {
                    Debug.Log("1");
                    // Validate if the point is on the NavMesh
                    if (NavMesh.SamplePosition(pointOnFloor, out NavMeshHit navHit, 3f, NavMesh.AllAreas))
                    {
                        Debug.Log("2");
                        return navHit.position;
                    }
                }
            }
        }

        return Vector3.zero;
    }

    protected override void OnEnd()
    {
        patrolCollider = null;
        enemyTransform = null;
    }
}
