using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour
{
    [SerializeField] private GameObject mfire;
    [SerializeField] private GameObject mfirePos;
    private Vector3 mspawnDirection = Vector3.forward;
    [SerializeField] private float mspawnInterval = 1f;
    [SerializeField] private float mspawnDistanceIncrement = 3f;
    [SerializeField] private int mmaxSpawnCount = 10;
    private Vector3 mcurrentSpawnPosition;
    [SerializeField] private int mspawnCount = 0;
    private Coroutine mspawnCoroutine;

    private void Awake()
    {
        mcurrentSpawnPosition = mfirePos.transform.position;
    }

    public void StartFire()
    {
        if (mspawnCoroutine == null)
        {
            StartCoroutine(SpawnFire());
        }
    }

    public void StopFire()
    {
        if (mspawnCoroutine  != null)
        {
            StopCoroutine(SpawnFire());
            mspawnCoroutine = null;
        }
    }

    private IEnumerator SpawnFire()
    {
        while (mspawnCount < mmaxSpawnCount)
        {
            Instantiate(mfire, mcurrentSpawnPosition, Quaternion.identity);

            mcurrentSpawnPosition += mspawnDirection.normalized * mspawnDistanceIncrement;

            mspawnCount++;

            yield return new WaitForSeconds(mspawnInterval);
        }

        mspawnCoroutine = null;
    }

    public void ResetFire()
    {
        mcurrentSpawnPosition = mfirePos.transform.position;
        mspawnCount = 0;
        mspawnCoroutine = null;
    }

    

}
