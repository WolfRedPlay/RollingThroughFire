using System;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] float _force;

    private void OnValidate()
    {
        _direction.x = Mathf.Round(Mathf.Clamp(_direction.x, -1, 1));
        _direction.y = Mathf.Round(Mathf.Clamp(_direction.y, -1, 1));
        _direction.z = Mathf.Round(Mathf.Clamp(_direction.z, -1, 1));
    }


    private void Start()
    {
        _direction = transform.InverseTransformDirection(_direction);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(_direction * _force * Time.fixedDeltaTime);
        }
    }


}
