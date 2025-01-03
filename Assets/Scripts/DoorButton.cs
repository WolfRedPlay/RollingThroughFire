using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(HingeJoint))]
public class DoorButton : MonoBehaviour
{
    [SerializeField] HingeJoint _door;


    HingeJoint _buttonJoint;
    bool _doorIsOpening = false;
    Coroutine _cooldown = null;
    float _enginePowerAbs;

    private void Start()
    {
        _buttonJoint = GetComponent<HingeJoint>();
        _enginePowerAbs = Mathf.Abs(_buttonJoint.motor.targetVelocity);

        JointMotor motor = _buttonJoint.motor;
        motor.targetVelocity = -_enginePowerAbs;
        _buttonJoint.motor = motor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            JointMotor motor = _buttonJoint.motor;
            motor.targetVelocity = _enginePowerAbs;
            _buttonJoint.motor = motor;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            JointMotor motor = _buttonJoint.motor;
            motor.targetVelocity = -_enginePowerAbs;
            _buttonJoint.motor = motor;
        }
    }
    public void OpenDoor()
    {
        if (!_door) return;

        if (!_doorIsOpening)
        {
            _doorIsOpening = true;
            
            JointMotor motor = _door.motor;
            motor.targetVelocity *= -1;
            _door.motor = motor;
        }

        if (_cooldown != null)
        {
            StopCoroutine(_cooldown);
            _cooldown = null;
        }
    }

    private void Update()
    {
        if (_buttonJoint.limits.max- _buttonJoint.angle <= .01f) OpenDoor();

        if (_doorIsOpening)
        {
            if(Mathf.Abs(_door.limits.min - _door.angle) <= .01f)
            {
                if(_cooldown == null)        
                    _cooldown = StartCoroutine(DoorCooldown());
            }
        }
    }

    IEnumerator DoorCooldown()
    {
        yield return new WaitForSeconds(4f);

        JointMotor motor = _door.motor;
        motor.targetVelocity *= -1;
        _door.motor = motor;

        _doorIsOpening = false;
    }
}
