using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] InputActionReference _grabValue;
    [SerializeField] InputActionReference _pointValue;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

        float grabValue = _grabValue.action.ReadValue<float>();
        float pointValue = _pointValue.action.ReadValue<float>();



        _animator.SetFloat("Grab", grabValue);
        _animator.SetFloat("Point", pointValue);
    }
}
