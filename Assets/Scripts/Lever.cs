using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HingeJoint))]
public class Lever : MonoBehaviour
{
    private HingeJoint joint;
    private float max;
    private float min;
    private bool Side = true;
    private bool canPassHalf = true;
    public UnityEvent OnLeverPassesTheHalf;

    [SerializeField] bool OppositeDir = false;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
        max = joint.limits.max;
        min = joint.limits.min;

        if (OppositeDir)
        {
            Side = !Side;
            JointMotor jointMotor = joint.motor;
            jointMotor.targetVelocity = -jointMotor.targetVelocity;
            joint.motor = jointMotor;
        }
    }

    private void FixedUpdate()
    {
        if (HasPassedHalf())
        {
            OnLeverPassesTheHalf?.Invoke();
            ChangeTargetPosition();
            Debug.Log("Yes");
        }
    }

    public bool HasPassedHalf()
    {
        if (!canPassHalf) return false;

        float halfwayPoint = (max + min) / 2;
        float currentAngle = joint.angle;

        //Debug.Log(halfwayPoint + " :: " + currentAngle);

        if (currentAngle > halfwayPoint && !Side || currentAngle < halfwayPoint && Side)
        {
            Debug.Log("Passed the half");
            Side = !Side;
            StartCoroutine(RechargePassHalf(0.5f));
            return true;
        }

        return false;
    }

    private IEnumerator RechargePassHalf(float rechargeTime)
    {
        canPassHalf = false;
        yield return new WaitForSeconds(rechargeTime);
        canPassHalf = true;
    }

    public void ChangeTargetPosition()
    {
        float currentTarget = joint.spring.targetPosition;
        float newTarget = Mathf.Approximately(currentTarget, max) ? min : max;
        SetTargetPosition(newTarget);
    }

    private void SetTargetPosition(float target)
    {
        JointSpring jointSpring = joint.spring;
        jointSpring.targetPosition = target;
        joint.spring = jointSpring;

        JointMotor jointMotor = joint.motor;
        jointMotor.targetVelocity = -jointMotor.targetVelocity;
        joint.motor = jointMotor;
    }
}
