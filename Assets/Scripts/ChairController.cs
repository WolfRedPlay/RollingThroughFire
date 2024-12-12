using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ChairController : MonoBehaviour
{
    [Header("Hand Inputs")]
    [SerializeField] HandInputHandler leftHandInput;
    [SerializeField] HandInputHandler rightHandInput;

    [Header("Speeds")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _breakingForce;

    [Header("Recentering")]
    [SerializeField] XROrigin _origin;
    [SerializeField] Transform _playerPosition;


    Rigidbody _rb;
    float _leftMultiplier = 1f;
    float _rightMultiplier = 1f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        yield return new WaitForSeconds(0f);


        _origin.MoveCameraToWorldLocation(_playerPosition.position);

    }

    private void FixedUpdate()
    {
        _leftMultiplier = 1f;
        _rightMultiplier = 1f;

        if (leftHandInput.IsBreak)
        {
            _rightMultiplier = 2f;
        }
        if (rightHandInput.IsBreak)
        {
            _leftMultiplier = 2f;
        }


        if (leftHandInput.HandMovement != 0f || rightHandInput.HandMovement != 0f)
        {
            Vector3 rotation = Vector3.up * (leftHandInput.HandMovement * _leftMultiplier - rightHandInput.HandMovement * _rightMultiplier) * _rotationSpeed * Time.fixedDeltaTime;
            _rb.AddTorque(rotation, ForceMode.Force);


            Debug.Log(leftHandInput.HandMovement);
            Debug.Log(rightHandInput.HandMovement);
            Vector3 movement = transform.forward * (leftHandInput.HandMovement + rightHandInput.HandMovement) * _moveSpeed * Time.fixedDeltaTime;
            _rb.linearVelocity += movement;

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
        if (_rb.linearVelocity.magnitude < 0.01f) return;



        Vector3 forwardVelocity = Vector3.Project(_rb.linearVelocity, transform.forward);
        Vector3 brakingForce = forwardVelocity * (_breakingForce * Time.fixedDeltaTime);
        
        _rb.linearVelocity -= brakingForce;

       
        if (_rb.linearVelocity.magnitude < 0.1f) _rb.linearVelocity = Vector3.zero;
    }

    void OnDrawGizmos()
    {
        if (_rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + transform.rotation * _rb.centerOfMass, 0.1f);
        }
    }
}
