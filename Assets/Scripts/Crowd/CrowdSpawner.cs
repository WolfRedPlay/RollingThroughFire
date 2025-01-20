using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class CrowdSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]

    [SerializeField, Tooltip("Moving Object Prefab that has to have Person.cs on it")]
    List<GameObject> Person_prefab;
    int current_prefab = 0;

    [SerializeField, Tooltip("Empty object with CrowdOperator.cs on it")]
    CrowdOperator CrowdOperator_prefab;

    [SerializeField, Tooltip("Time perdiod with which it will be spawning waves of personas")]
    float SpawnFrequancy = 7f;
    float Spawn_timer = 0;

    [SerializeField, Tooltip("How many personas will be spawned per group")]
    int GroupSize = 5;

    [Header("Operator Settings")]

    [SerializeField, Tooltip("Way points that personas will be following one by one, on last one they will disappear")]
    List<Transform> WayPoints;

    [SerializeField, Tooltip("Radius of the crowd, as less the radius, as closer they will be to each other")]
    float RadiusOfCrowd = 3f;

    [SerializeField, Tooltip("Minimal distance between each person, if its larger than crowd radius it will be decressed automaticly")]
    float DistanceBetweenPersonas = 1f;

    [SerializeField, Tooltip("Max attemps to make a group pattern, after running out of the attemps it will decress distance between personas")]
    float MaxAttemps = 50;

    [SerializeField, Tooltip("Frequancy of spawning personas in seconds")]
    float AwakeTimeBetweenPersonas = 0.5f;

    [SerializeField]
    bool gizmos = true;

    private void Update()
    {
        if (Person_prefab != null && CrowdOperator_prefab != null)
        {
            Spawn_timer += Time.deltaTime;

            if (Spawn_timer > SpawnFrequancy)
            {
                Spawn_timer = 0;

                List<GameObject> crowd = SpawnCrowd(GroupSize, Person_prefab);

                SpawnOperator(crowd, WayPoints);
            }
        }
    }

    private List<GameObject> SpawnCrowd(int groupSize, List<GameObject> prefab)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit navHit, 5f, NavMesh.AllAreas))
        {
            for (int i = 0; i < groupSize; i++)
            {
                current_prefab++;
                if (current_prefab >= Person_prefab.Count) current_prefab = 0;

                GameObject spawnedPerson = Instantiate(prefab[current_prefab], navHit.position + Vector3.up * 2, Quaternion.identity);
                spawnedPerson.SetActive(false);
                spawnedObjects.Add(spawnedPerson);
            }
            return spawnedObjects;
        }
        else
        {
            Debug.Log("Couldnt find where to spawn");
        }
        return null;
    }

    private void SpawnOperator(List<GameObject> personas, List<Transform> waypoints)
    {
        CrowdOperator operatorInstance = Instantiate(CrowdOperator_prefab, transform.position, Quaternion.identity);

        operatorInstance.Personas = personas;
        operatorInstance.WayPoints = waypoints;

        operatorInstance.RadiusOfCrowd = RadiusOfCrowd;
        operatorInstance.DistanceBetweenPersonas = DistanceBetweenPersonas;
        operatorInstance.MaxAttemps = MaxAttemps;
        operatorInstance.AwakeTimeBetweenPersonas = AwakeTimeBetweenPersonas;
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            if (WayPoints != null && WayPoints.Count > 0)
            {
                Gizmos.color = Color.green;

                foreach (Transform waypoint in WayPoints)
                {
                    if (waypoint != null)
                    {
                        Gizmos.DrawWireSphere(waypoint.position, RadiusOfCrowd);
                    }
                }
            }
        }
    }
}