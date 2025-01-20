using UnityEngine;
using UnityEngine.Events;

public class ButtonElevator : MonoBehaviour
{
    public UnityEvent onButtonPressed;
    public Transform buttonTransform; // Assign button object transform in the Inspector
    public Vector3 pressedOffset = new Vector3(0, -0.1f, 0); // Offset for pressed position
    public float pressSpeed = 5f;    // Speed of button movement

    private Vector3 defaultPosition;  // Define the position when the button is not pressed
    private Vector3 pressedPosition;  // Define the position when the button is pressed
    private bool isPressed = false;

    void Start()
    {
        // Initialize positions
        if (buttonTransform == null)
        {
            buttonTransform = transform; // Use this GameObject's transform if none assigned
        }

        defaultPosition = buttonTransform.localPosition;
        pressedPosition = defaultPosition + pressedOffset;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isPressed = true;
            onButtonPressed?.Invoke(); // Invoke the event
            Debug.Log("Button Pressed Released!");

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            isPressed = false;
            Debug.Log("Button Released!");
        }
    }

    void Update()
    {
        // Smoothly move the button to the pressed or default position
        Vector3 targetPosition = isPressed ? pressedPosition : defaultPosition;
        buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, targetPosition, Time.deltaTime * pressSpeed);
    }
}
