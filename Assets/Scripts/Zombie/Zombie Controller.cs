using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
/*
#if UNITY_EDITOR
using UnityEditor;
#endif
*/

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    public enum Behavior
    {
        None,
        Waiting_onPoint,
        Patrolling,
        Chasing,
        Attaking,
        BroadCasting,
        Detecting_Player
    }
    public Behavior behavior = Behavior.None;


    [Header("General")]
    [SerializeField] LayerMask Zombie_Layer;
    [SerializeField] string Player_tag = "Player";
    private GameObject player;
    private NavMeshAgent agent;

    [Header("Broadcast")]
    [SerializeField, Tooltip("Will notify zombies in specific area that player was detected")] bool Should_Broadcast = false;
    [SerializeField, Tooltip("Radius of broadcast area")] float Broadcast_radius = 5.0f;
    private bool Ready_to_broadcast = true;
    //Temperal
    private float broadcast_time = 2;
    private float broadcast_timer = 0;
    //Temperal

    [Header("Detection")]
    [SerializeField, Tooltip("Distance on which zombie will see the player")] float Detection_Radius = 5.0f;
    [SerializeField, Range(1, 360), Tooltip("Angle in which zombie will see the player")] float Detection_Angle = 90f;
    [SerializeField, Tooltip("Will determain whether zombie will notice player instantly or after some time")] bool Should_detection_time = true;
    [SerializeField, Tooltip("Time for which player will have to stay in sight of zombie in order to be detected")] float Detection_time = 3.0f;
    private float Detection_timer = 0;

    [Header("Chasing")]
    [SerializeField, Tooltip("Time for which zombie will remeber player even if player exits the range of detection")] float Remember_time = 10.0f;
    [SerializeField, Tooltip("Rotation speed towards target when chasing")] float Chase_rotation_speed = 6f;
    public float Remember_timer = 0;

    [Header("Attack")]
    [SerializeField] float Attack_Distance = 2f;
    [SerializeField] float Delay_between_attacks = 1.0f;
    private bool Ready_to_attack = false;

    [Header("Patrolling")]
    [SerializeField] bool Should_Patrol = true;
    [SerializeField, Tooltip("Defines the patrol area")] Collider Patrol_Area;
    [SerializeField, Tooltip("Max distance on what patrolling point can be set")] float Max_Point_distance = 10f;
    [SerializeField, Tooltip("Rotation speed towards target when patrolling")] float Patrol_rotation_speed = 3f;
    [SerializeField] float onPoint_time = 2f;
    [SerializeField, Tooltip("Maximum attempts to find a valid patrol point")] int MaxAttempts = 50;
    private Vector3 Patrol_point = Vector3.zero;
    private bool Patrol_point_SET = false;
    private bool Agent_dest_SET = false;
    private float onPoint_timer = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag(Player_tag);
        if (player == null) Debug.LogError("Could not find the player");
        Routine();
        ResetDetectionTimer();
    }

    private void Update()
    {
        
        if (IfSeeThePlayer())
        {
            if (Should_detection_time && Detection_timer > 0 && Remember_timer <= 0)
            {
                behavior = Behavior.Detecting_Player;
            }
            else
            {
                if (Should_Broadcast && Ready_to_broadcast)
                {
                    //Ready_to_broadcast = false;
                    behavior = Behavior.BroadCasting;
                    broadcast_timer = broadcast_time;
                    FullStop();
                }
                else if (behavior < Behavior.Chasing || CheckOnTemperalBehaviors())
                {
                    FullStop();
                    behavior = Behavior.Chasing;
                }

                SetRemeberTimer();
            }

        }
        else
        {
            ResetDetectionTimer();

            if (Remember_timer > 0)
            {
                Remember_timer -= Time.deltaTime;
            }
            else if (behavior != Behavior.BroadCasting)
            {
                Ready_to_broadcast = true;
                ComeBackToRoutine();
            }
        }

        

        switch (behavior)
        {
            case Behavior.Waiting_onPoint:
                
                if (onPoint_timer <= 0)
                {
                    behavior = Behavior.Patrolling;
                    onPoint_timer = onPoint_time;
                }
                else
                {
                    onPoint_timer -= Time.deltaTime;
                }

                break;

            case Behavior.Patrolling:

                if (!Patrol_point_SET)
                {
                    Patrol_point = GetPatrolPoint();
                    break;
                }
                if (Agent_dest_SET)
                {
                    RotateTo(Patrol_point, Patrol_rotation_speed);

                    if(ReachedDestination(Patrol_point, 0.2f))
                    {
                        FullStop();
                        Patrol_point_SET = false;
                        behavior = Behavior.Waiting_onPoint;
                        break;
                    }
                }
                else
                {
                    MoveTo(Patrol_point);
                }

                break;

            case Behavior.BroadCasting:
                // Sound, possible animation of broadcasting

                if (broadcast_timer <= 0)
                {
                    Broadcast();
                    behavior = Behavior.Chasing;
                }
                else
                {
                    broadcast_timer -= Time.deltaTime;
                }

                break;

            case Behavior.Chasing:

                if (!ReachedDestination(player.transform.position, Attack_Distance / 2))
                {
                    MoveTo(player.transform.position);
                }
                else
                {
                    FullStop();
                }
                
                RotateTo(player.transform.position, Chase_rotation_speed);

                break;

            case Behavior.Attaking:




                break;

            case Behavior.Detecting_Player:

                FullStop();
                RotateTo(player.transform.position, Patrol_rotation_speed / 2);
                Detection_timer -= Time.deltaTime;

                break;
        }
    }

    private bool CheckOnTemperalBehaviors()
    {
        if (behavior >= Behavior.BroadCasting)
        {
            return true;
        }
        return false;
    }

    private void ComeBackToRoutine()
    {
        if (behavior > Behavior.Patrolling)
        {
            FullStop();
            Routine();
        }
    }

    private void ResetDetectionTimer()
    {
        if (Should_detection_time)
        {
            Detection_timer = Detection_time;
        }
    }

    private void Routine()
    {
        if (Should_Patrol)
        {
            behavior = Behavior.Patrolling;
        }
        else
        {
            behavior = Behavior.None;
        }
    }

    public void SetRemeberTimer()
    {
        Remember_timer = Remember_time;
        Ready_to_broadcast = false;
    }

    private void Broadcast()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Broadcast_radius, Zombie_Layer);
        foreach (var hitCollider in hitColliders)
        {
            ZombieController zombie = hitCollider.GetComponent<ZombieController>();
            if (zombie != null)
            {
                zombie.SetRemeberTimer();
            }
        }
        Ready_to_broadcast = false;
    }

    private bool IfSeeThePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        
        if (angleToPlayer <= Detection_Angle / 2)
        {
            
            Debug.DrawRay(transform.position, directionToPlayer * Detection_Radius, Color.red);
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, Detection_Radius))
            {
                if (hit.transform.gameObject == player)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private Vector3 GetPatrolPoint()
    {
        float patrolHeight = Patrol_Area.bounds.max.y;

        for (int i = 0; i < MaxAttempts; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * Max_Point_distance;
            randomOffset.y = 0;
            Vector3 randomPoint = transform.position + randomOffset;
            randomPoint.y = patrolHeight;

            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 pointOnFloor = hit.point;

                if (Patrol_Area.bounds.Contains(pointOnFloor))
                {
                    if (NavMesh.SamplePosition(pointOnFloor, out NavMeshHit navHit, 2f, NavMesh.AllAreas))
                    {
                        Patrol_point_SET = true;
                        return navHit.position;
                    }
                }
            }
        }

        return Vector3.zero;
    }

    private void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
        Agent_dest_SET = true;
    }

    private void FullStop()
    {
        if (agent.remainingDistance > 0.1f)
        {
            agent.SetDestination(transform.position);
            Agent_dest_SET = false;
        }
    }

    private void RotateTo(Vector3 destination, float rotationSpeed)
    {
        Vector3 direction = (destination - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private bool ReachedDestination(Vector3 dest, float dis_to_reach)
    {
        Vector3 point1 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 point2 = new Vector3(dest.x, 0, dest.z);

        if (Vector3.Distance(point1, point2) < dis_to_reach && Vector3.Distance(transform.position, dest) < 5)
        {
            return true;
        }

        return false;
    }

}
