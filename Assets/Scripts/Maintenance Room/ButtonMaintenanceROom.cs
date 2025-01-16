using UnityEngine;
using TMPro;
using System.Xml;


public class ButtonMaintenanceROom : MonoBehaviour
{
    public Transform buttonElevatorTransform; // Assign button object transform in the Inspector
    public Vector3 pressedButtonElevatorPosition;  // Define the position when the button is pressed
    public Vector3 defaultButtonElevatorPosition;  // Define the position when the button is not pressed
    public float pressSpeedElevatorButtom = 5f;    // Speed of button movement
  

    [SerializeField] private TextMeshProUGUI elevatorOffText;
    [SerializeField] private TextMeshProUGUI elevatorOnText;
 

    private bool isElevatorButtonPressed = false;  // Tracks if the button is pressed

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a hand
        if (other.CompareTag("Throwable"))
        {
            isElevatorButtonPressed = true;
            Debug.Log("Elevator Button Pressed!");
            //destroyReactor.BreakGlass();
          






            // Add logic for what happens when the button is pressed
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset button when the hand leaves
        if (other.CompareTag("Throwable"))
        {
            isElevatorButtonPressed = false;
            Debug.Log("Elevator Button Released!");
        }
    }

    void Update()
    {
        // Smoothly move the button to the pressed or default position
        if (isElevatorButtonPressed)
        {
            buttonElevatorTransform.localPosition = Vector3.Lerp(buttonElevatorTransform.localPosition, pressedButtonElevatorPosition, Time.deltaTime * pressSpeedElevatorButtom);
        }
        else
        {
            buttonElevatorTransform.localPosition = Vector3.Lerp(buttonElevatorTransform.localPosition, defaultButtonElevatorPosition, Time.deltaTime * pressSpeedElevatorButtom);
        }
    }

}

