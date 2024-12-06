using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ChairController : MonoBehaviour
{
    [SerializeField] HandInputHandler leftHandInput;
    [SerializeField] HandInputHandler rightHandInput;


    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _breakingForce;

    Rigidbody rb;

    [SerializeField] XROrigin _origin;
    [SerializeField] Transform _playerPosition;


    float leftMultiplier = 1f;
    float rightMultiplier = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        yield return new WaitForSeconds(0f);


        _origin.MoveCameraToWorldLocation(_playerPosition.position);
        //_origin.MatchOriginUpCameraForward(_playerPosition.up, _playerPosition.forward);

    }

    private void FixedUpdate()
    {
        leftMultiplier = 1f;
        rightMultiplier = 1f;

        if (leftHandInput.IsBreak)
        {
            rightMultiplier = 2f;
        }
        if (rightHandInput.IsBreak)
        {
            leftMultiplier = 2f;
        }


        //Debug.Log(leftHandInput.HandMovement + "  " + RightHandInput.HandMovement);
        if (leftHandInput.HandMovement != 0f || rightHandInput.HandMovement != 0f)
        {
            Vector3 rotation = Vector3.up * (leftHandInput.HandMovement * leftMultiplier - rightHandInput.HandMovement * rightMultiplier) * _rotationSpeed * Time.fixedDeltaTime;
            rb.AddTorque(rotation, ForceMode.Force);


            Debug.Log(leftHandInput.HandMovement);
            Debug.Log(rightHandInput.HandMovement);
            Vector3 movement = transform.forward * (leftHandInput.HandMovement + rightHandInput.HandMovement) * _moveSpeed * Time.fixedDeltaTime;
            //rb.AddForce(movement, ForceMode.Force);
            rb.linearVelocity += movement;

            //Debug.Log(rb.linearVelocity);
        }

        if (leftHandInput.IsBreak)
        {
            ApplyBreak(true);
        }

        if (rightHandInput.IsBreak)
        {
            ApplyBreak(false);
        }

    }

    private void ApplyBreak(bool isLeft)
    {
        if (rb.linearVelocity.magnitude < 0.01f) return;



        Vector3 forwardVelocity = Vector3.Project(rb.linearVelocity, transform.forward);
        Vector3 brakingForce = forwardVelocity * (_breakingForce * Time.fixedDeltaTime);
        
        rb.linearVelocity -= brakingForce;

       
        if (rb.linearVelocity.magnitude < 0.1f) rb.linearVelocity = Vector3.zero;
    }

    void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + transform.rotation * rb.centerOfMass, 0.1f);
        }
    }
}
