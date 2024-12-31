using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class WheelsAnimator : MonoBehaviour
{
    [SerializeField] Transform _leftWheel;
    [SerializeField] Transform _rightWheel;
    [SerializeField] float _wheelRadius = 1f;


    Rigidbody _rb;
    float _wheelsBase;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _wheelsBase = Mathf.Abs(_leftWheel.position.x - _rightWheel.position.x);
    }

    private void FixedUpdate()
    {
        float forwardSpeed = _rb.linearVelocity.magnitude * Mathf.Sign(Vector3.Dot(_rb.linearVelocity, _rb.transform.forward));


        float leftWheelSpeed = forwardSpeed + _rb.angularVelocity.y * (_wheelsBase / 2);
        float rightWheelSpeed = forwardSpeed - _rb.angularVelocity.y * (_wheelsBase / 2);


        float leftAngularSpeed = leftWheelSpeed / _wheelRadius;
        float rightAngularSpeed = rightWheelSpeed / _wheelRadius;
        

        RotateWheel(_leftWheel, leftAngularSpeed);
        RotateWheel(_rightWheel, rightAngularSpeed);
    }

    private void RotateWheel(Transform wheel, float angularSpeed)
    {
        if (!wheel) return;
        wheel.Rotate(Vector3.right, angularSpeed * Mathf.Rad2Deg * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        if (!_leftWheel || !_rightWheel) return;

        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_leftWheel.position, _wheelRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_rightWheel.position, _wheelRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftWheel.position, _rightWheel.position);



    }
}
