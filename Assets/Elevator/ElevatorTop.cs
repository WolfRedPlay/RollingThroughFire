using UnityEngine;

public class ElevatorTop : MonoBehaviour
{

    private ElevatorManager mElevatorManager;
    private void Awake()
    {
        mElevatorManager.PlayAnimation();
    }
}
