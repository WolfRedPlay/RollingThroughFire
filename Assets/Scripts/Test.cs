using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float power = 5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PushObjct()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float time = 0f;
        bool checker = true;
        while (checker)
        {
            time += Time.deltaTime;
            if (time >= 3f) checker = false;
            rb.AddRelativeTorque(Vector3.up * power);
            yield return null;
        }
    }
}
