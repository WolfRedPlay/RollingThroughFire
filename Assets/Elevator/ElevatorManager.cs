using UnityEngine;
using UnityEngine.UI;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button levelChangeButton;

    private enum DoorState { Open, Closed };
    private DoorState currentState = DoorState.Closed;


    void Update()
    {
        PlayAnimation();
    }

    void PlayAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case DoorState.Open:
                    animator.Play("DoorsClose");
                    currentState = DoorState.Closed;
                    break;
                case DoorState.Closed:
                    animator.Play("DoorsAnim");
                    currentState = DoorState.Open;
                    break;
                default:
                    Debug.LogError("Unhandled door state: " + currentState);
                    break;
            }
        }
    }

    void ButtonPressed()
    {
        GameManager.LoadNextScene();
    }
}
