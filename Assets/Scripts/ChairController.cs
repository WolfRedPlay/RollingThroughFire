using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class ChairController : MonoBehaviour
{
    [SerializeField] HandInputHandler leftHandInput;
    [SerializeField] HandInputHandler RightHandInput;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;

    Coroutine rotation;
    Coroutine movement;


    [SerializeField] XROrigin _origin;
    [SerializeField] Transform _playerPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        yield return new WaitForSeconds(0f);


        _origin.MoveCameraToWorldLocation(_playerPosition.position);
        _origin.MatchOriginUpCameraForward(_playerPosition.up, _playerPosition.forward);


        //Vector3 camOffset = _origin.CameraInOriginSpacePos;

        //_origin.gameObject.transform.position = _playerPosition.position - camOffset;

        //_origin.gameObject.transform.rotation = _playerPosition.rotation;

    }

    XRInputSubsystem GetActiveXRInputSubsystem()
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);
        foreach (var subsystem in subsystems)
        {
            if (subsystem.running)
            {
                return subsystem;
            }
        }
        return null;
    }

    private void FixedUpdate()
    {
        //Debug.Log(leftHandInput.HandMovement + "  " + RightHandInput.HandMovement);

        if (leftHandInput.HandMovement != 0 || RightHandInput.HandMovement != 0)
        {
            Vector3 torque = transform.up * (leftHandInput.HandMovement - RightHandInput.HandMovement) * rotationSpeed * Time.fixedDeltaTime;

            //rb.angularVelocity += newRotation * Time.fixedDeltaTime;

            rb.AddTorque(torque, ForceMode.Impulse);

            Vector3 force = transform.forward * (leftHandInput.HandMovement + RightHandInput.HandMovement) * moveSpeed * 100f * Time.fixedDeltaTime;

            rb.AddForce(force, ForceMode.Impulse);

            //if (rotation != null) StopCoroutine(rotation);
            //rotation = StartCoroutine(Rotation());


            //if (movement != null) StopCoroutine(movement);
            //movement = StartCoroutine(Movement());

        }

        //Debug.Log(rb.angularVelocity);
        //Debug.Log(rb.linearVelocity);

    }
}
