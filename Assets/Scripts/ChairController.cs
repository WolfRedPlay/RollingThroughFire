using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    [SerializeField] HandInputHandler leftHandInput;
    [SerializeField] HandInputHandler RightHandInput;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;

    Coroutine rotation;
    Coroutine movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (leftHandInput.HandMovement != 0 || RightHandInput.HandMovement != 0)
        {
            Vector3 newRotation = transform.up * (leftHandInput.HandMovement - RightHandInput.HandMovement) * 50f;

            if (rotation != null) StopCoroutine(rotation);
            rotation = StartCoroutine(Rotation());


            if (movement != null) StopCoroutine(movement);
            movement = StartCoroutine(Movement());

        }

    }


    IEnumerator Rotation()
    {
        Vector3 strongestRotation = transform.up * (leftHandInput.HandMovement - RightHandInput.HandMovement) * rotationSpeed * 10f;
        
        float stage = 0f;
        
        Vector3 newRotation = new Vector3(strongestRotation.x, strongestRotation.y, strongestRotation.z);

        while (newRotation.y > 0.05f)
        {
            rb.AddTorque(newRotation, ForceMode.Impulse);
            newRotation = Vector3.Lerp(strongestRotation, Vector3.zero, stage);
            stage += Time.fixedDeltaTime;
            stage = Mathf.Clamp01(stage);
            yield return null;
        }
    }


    IEnumerator Movement()
    {
        Vector3 strongestForce = transform.forward * (leftHandInput.HandMovement + RightHandInput.HandMovement) * moveSpeed * 100f;

        float stage = 0f;

        Vector3 newForce = new Vector3(strongestForce.x, strongestForce.y, strongestForce.z);

        while (!newForce.Equals(Vector3.zero))
        {
            rb.AddForce(newForce);
            newForce = Vector3.Lerp(strongestForce, Vector3.zero, stage);
            stage += Time.fixedDeltaTime;
            stage = Mathf.Clamp01(stage);
            yield return null;
        }
    }
}
