using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

public class FireManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> fireStarterPrefab = new List<GameObject>(3);
    [SerializeField]private int randomSize = 0;
    public float fireStarterSize = 5f;
    public Vector3 fireStarterSpacing = new Vector3(5f, 5f, 5f);
    public float spawnDelay = 0.5f;

    private List<Vector3> firestarterPositions = new List<Vector3>();
    private HashSet<Vector3> availablePositions = new HashSet<Vector3>();
    private HashSet<Vector3> visitedPositions = new HashSet<Vector3>();

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(MapFirestarters(collision.collider));
    }

    private IEnumerator MapFirestarters(Collider collider)
    {
        Bounds bounds = collider.bounds;

        // Adjust spacing to ensure grid is smaller
        fireStarterSpacing = new Vector3(
            fireStarterSpacing.x * 10f,
            fireStarterSpacing.y * 10f,
            fireStarterSpacing.z * 10f
        );

        int countX = Mathf.Max(1, Mathf.FloorToInt(bounds.size.x / fireStarterSpacing.x));
        int countY = Mathf.Max(1, Mathf.FloorToInt(bounds.size.y / fireStarterSpacing.y));
        int countZ = Mathf.Max(1, Mathf.FloorToInt(bounds.size.z / fireStarterSpacing.z));

        Debug.Log($"Grid dimensions: {countX}x{countY}x{countZ}");

        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                for (int z = 0; z < countZ; z++)
                {
                    Vector3 position = bounds.min + new Vector3(
                        x * fireStarterSpacing.x + fireStarterSize / 2,
                        y * fireStarterSpacing.y + fireStarterSize / 2,
                        z * fireStarterSpacing.z + fireStarterSize / 2
                    );
                    availablePositions.Add(position);
                }
            }
        }

        if (availablePositions.Count == 0)
        {
            Debug.LogWarning($"No valid positions calculated for {collider.name}. Check fireStarterSize and object scale.");
            yield break;
        }

        randomSize = Random.Range(0, fireStarterPrefab.Count);
        Vector3 initialPosition = availablePositions.First();
        availablePositions.Remove(initialPosition);
        visitedPositions.Add(initialPosition);
        Instantiate(fireStarterPrefab[randomSize], initialPosition, Quaternion.identity);

        yield return StartCoroutine(RandomSpread(initialPosition));
    }

    private IEnumerator RandomSpread(Vector3 currentPosition)
    {
        while (availablePositions.Count > 0)
        {
            List<Vector3> neighbors = availablePositions
                .Where(pos => Vector3.Distance(pos, currentPosition) <= fireStarterSpacing.magnitude * 1.5f)
                .ToList();

            if (neighbors.Count == 0) break;

            Vector3 nextPosition = neighbors[Random.Range(0, neighbors.Count)];
            availablePositions.Remove(nextPosition);
            visitedPositions.Add(nextPosition);
            randomSize = Random.Range(0, fireStarterPrefab.Count);
            Instantiate(fireStarterPrefab[randomSize], nextPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);

            currentPosition = nextPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var position in visitedPositions)
        {
            Gizmos.DrawWireSphere(position, fireStarterSize / 4);
        }
    }
}