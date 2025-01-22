using UnityEngine;
using TMPro;
using System.Xml;
using UnityEngine.Events;


public class ButtonPress : MonoBehaviour
{
    public Transform buttonTransform; // Assign button object transform in the Inspector
    public Vector3 pressedPosition;  // Define the position when the button is pressed
    public Vector3 defaultPosition;  // Define the position when the button is not pressed
    public float pressSpeed = 5f;    // Speed of button movement
    public ReactorTemperatureManager temperatureManager;
    [SerializeField] private ReactorActivation reactorActivation;

    [SerializeField] private TextMeshProUGUI reactorOffText;
    [SerializeField] private TextMeshProUGUI reactorOffText2;
    [SerializeField] private TextMeshProUGUI reactorOffText3;

    [SerializeField] private TextMeshProUGUI reactorOnText;
    [SerializeField] private TextMeshProUGUI reactorOnText2;
    [SerializeField] private TextMeshProUGUI reactorOnText3;
    public UnityEvent OnPressed; 




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
            temperatureManager.ActivateTemperatureManager();
            reactorOffText.gameObject.SetActive(false);

            reactorOnText.gameObject.SetActive(true);
            reactorOnText2.gameObject.SetActive(true);
            reactorOnText3.gameObject.SetActive(true);

            reactorOffText2.gameObject.SetActive(false);
            reactorOffText3.gameObject.SetActive(false);

            OnPressed?.Invoke();

            




            // Add logic for what happens when the button is pressed
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
