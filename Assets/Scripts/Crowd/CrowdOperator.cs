using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CrowdOperator : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> Personas;

    private Vector3[] OffsetPoints;

    [HideInInspector]
    public List<Transform> WayPoints;

    [HideInInspector]
    public float RadiusOfCrowd = 3f;

    [HideInInspector]
    public float DistanceBetweenPersonas = 1f;

    [HideInInspector]
    public float MaxAttemps = 50;
    private float MagicFixxer = 1.2f;

    [HideInInspector]
    public float AwakeTimeBetweenPersonas = 0.5f;
    private float AwakeTimer = 0;
    private int currentPersonaIndex = 0;

    private void Start()
    {
        OffsetPoints = GeneratePoints(Personas.Count);
        AssignPointsToPersonas();
    }

    private void Update()
    {
        ActivatePersonasOneByOne();
        CheckForCompletionAndDestroy();
    }

    private void ActivatePersonasOneByOne()
    {
        if (currentPersonaIndex < Personas.Count)
        {
            AwakeTimer += Time.deltaTime;
            if (AwakeTimer >= AwakeTimeBetweenPersonas)
            {
                AwakeTimer = 0;
                if (Personas[currentPersonaIndex] != null)
                {
                    Personas[currentPersonaIndex].SetActive(true);
                }
                currentPersonaIndex++;
            }
        }
    }

    private void CheckForCompletionAndDestroy()
    {
        bool allDestroyed = true;
        foreach (GameObject persona in Personas)
        {
            if (persona != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void AssignPointsToPersonas()
    {
        for (int i = 0; i < Personas.Count; i++)
        {
            Person comp = Personas[i].GetComponent<Person>();

            Vector3[] points = new Vector3[WayPoints.Count];
            for (int t = 0; t < WayPoints.Count; t++)
            {
                points[t] = OffsetPoints[i] + WayPoints[t].position;
            }

            comp.OriginalWayPoints = WayPoints;
            comp.wayPoints = points;
            //Personas[i].SetActive(false);
        }
    }

    private Vector3[] GeneratePoints(int PointsToGenerate)
    {
        Vector3[] points = new Vector3[PointsToGenerate];

        for (int PointCounter = 0; PointCounter < points.Length; PointCounter++)
        {
            Vector3 Position = GetRandomPointOnCircle(RadiusOfCrowd);
            for (int i = 0; i <= MaxAttemps; i++)
            {
                if (i == MaxAttemps)
                {
                    RadiusOfCrowd /= MagicFixxer;
                }

                if (CheckDistanceToOtherPoints(Position, points))
                {
                    break;
                }
                else
                {
                    Position = GetRandomPointOnCircle(RadiusOfCrowd);
                }
            }
            points[PointCounter] = Position;
        }
        return points;
    }

    private Vector3 GetRandomPointOnCircle(float Radius)
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-Radius, Radius), 0, UnityEngine.Random.Range(-Radius, Radius));

        pos = new Vector3(pos.x * Mathf.Abs(pos.normalized.x), pos.y, pos.z * Mathf.Abs(pos.normalized.z));

        return pos;
    }

    private bool CheckDistanceToOtherPoints(Vector3 CurrentPoint, Vector3[] otherPoints)
    {
        bool bol = true;
        foreach (Vector3 Point in otherPoints)
        {
            if (Vector3.Distance(CurrentPoint, Point) < DistanceBetweenPersonas)
            {
                bol = false; break;
            }
        }
        return bol;
    }
}
