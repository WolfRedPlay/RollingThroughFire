using Unity.XR.CoreUtils;
using UnityEngine;

public class ChairControllerWithGrab : MonoBehaviour
{
    [Header("Hand Inputs")]
    [SerializeField] HandInputHandlerWithGrab leftHandInput;
    [SerializeField] HandInputHandlerWithGrab rightHandInput;

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
        _origin.MoveCameraToWorldLocation(_playerPosition.position);

        //need to test
        Camera camera = GameObject.FindAnyObjectByType<Camera>();
        camera.enabled = false;
        camera.transform.rotation = _playerPosition.rotation;
        camera.enabled = true;
        //

        //_origin.RotateAroundCameraPosition();
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
