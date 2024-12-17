using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class CrowdSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    bool EnableSpawner = true;

    [SerializeField, Tooltip("")]
    GameObject Person_prefab;

    [SerializeField, Tooltip("")]
    CrowdOperator CrowdOperator_prefab;

    [SerializeField, Tooltip("")]
    float SpawnFrequancy = 7f;
    float Spawn_timer = 0;

    [SerializeField, Tooltip("")]
    int GroupSize = 5;

    [SerializeField, Tooltip("")]
    List<Transform> WayPoints;


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

    private List<GameObject> SpawnCrowd(int groupSize, GameObject prefab)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit navHit, 5f, NavMesh.AllAreas))
        {
            for (int i = 0; i < groupSize; i++)
            {
                GameObject spawnedPerson = Instantiate(prefab, navHit.position + Vector3.up * 2, Quaternion.identity);
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
    }
}
