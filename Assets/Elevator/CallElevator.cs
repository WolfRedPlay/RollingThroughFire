using UnityEngine;

public class CallElevator : MonoBehaviour
{
    public ElevatorManager melevatorManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            melevatorManager.PlayAnimation();
        }
    }
}
