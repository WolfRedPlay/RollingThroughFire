using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public Transform buttonTransform; // Assign button object transform in the Inspector
    public Vector3 pressedPosition;  // Define the position when the button is pressed
    public Vector3 defaultPosition;  // Define the position when the button is not pressed
    public float pressSpeed = 5f;    // Speed of button movement
    public DestroyReactor destroyReactor;
    [SerializeField] private ReactorActivation reactorActivation;


    private bool isPressed = false;  // Tracks if the button is pressed

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a hand
        if (other.CompareTag("Hand"))
        {
            isPressed = true;
            Debug.Log("Button Pressed!");
            //destroyReactor.BreakGlass();
            reactorActivation.ActivateReactor();

            // Add logic for what happens when the button is pressed
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset button when the hand leaves
        if (other.CompareTag("Hand"))
        {
            isPressed = false;
            Debug.Log("Button Released!");
        }
    }

    void Update()
    {
        // Smoothly move the button to the pressed or default position
        if (isPressed)
        {
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, pressedPosition, Time.deltaTime * pressSpeed);
        }
        else
        {
            buttonTransform.localPosition = Vector3.Lerp(buttonTransform.localPosition, defaultPosition, Time.deltaTime * pressSpeed);
        }
    }

}
