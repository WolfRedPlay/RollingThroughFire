using UnityEngine;

public class CloseDoors : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponentInParent<ElevatorManager>().PlayAnimation();
    }
    private void OnTriggerEnter(Collider other)
    {

        this.GetComponentInParent<ElevatorManager>().PlayAnimation();
    }
}