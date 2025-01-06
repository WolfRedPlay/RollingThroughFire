using System.Collections;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChairController : MonoBehaviour
{
    [Header("Hand Inputs")]
    [SerializeField] HandInput leftHandInput;
    [SerializeField] HandInput rightHandInput;

    [Space]
    [Header("Speeds")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _breakingForce;

    [Space]
    [Header("Recentering")]
    [SerializeField] XROrigin _origin;
    [SerializeField] Transform _playerPosition;


    Rigidbody _rb;
    float _leftMultiplier = 1f;
    float _rightMultiplier = 1f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        StartCoroutine(ResetPos());
        
    }


    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(.01f);
        _origin.MoveCameraToWorldLocation(_playerPosition.position);

        yield return new WaitForSeconds(.01f);

        Vector3 cameraForward = _origin.Camera.transform.forward;
        Vector3 targetForward = _playerPosition.forward;

        float angle = Vector3.Angle(cameraForward, targetForward);

        Vector3 cross = Vector3.Cross(cameraForward, targetForward);
        if (Vector3.Dot(cross, Vector3.up) < 0)
        {
            angle = -angle;
        }

        _origin.RotateAroundCameraUsingOriginUp(angle);
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
