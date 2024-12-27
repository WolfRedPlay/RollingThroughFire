using UnityEngine;


//[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(HandInput))]
public class WheelAnimator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] Transform _wheel;

    HingeJoint _joint;
    HandInput _input;

    float _currentSpeed = 0f;

    private void Awake()
    {
        //_joint = GetComponent<HingeJoint>();
        _input = GetComponent<HandInput>();
    }

    //private void Start()
    //{
    //    _joint.useMotor = false;
    //}

    private void Update()
    {

        if (_input.IsBreak)
        {
            _currentSpeed = 0f;
        }

        if (_input.HandMovement != 0)
        {
            _currentSpeed = _rotationSpeed * _input.HandMovement;
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, .1f);
        }
        _wheel.Rotate(Vector3.right * _currentSpeed * Time.deltaTime);

    }
}
