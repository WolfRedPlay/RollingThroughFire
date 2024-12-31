using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialTextDespawner : MonoBehaviour
{
    public GameObject tutorialTextPhone;
    public GameObject tutorialTextWheelLeft;
    public GameObject tutorialTextWheelRight;

    public InputActionReference despawnTextPhone;
    public InputActionReference despawnTutorialWheelLeft;
    public InputActionReference despawnTutorialWheelRight;

    void Start()
    {
        // Register callbacks for the input actions
        despawnTextPhone.action.performed += HideTutorialTextPhone;
        despawnTutorialWheelLeft.action.performed += HideTutorialTextWheelLeft;
        despawnTutorialWheelRight.action.performed += HideTutorialTextWheelRight;
    }

    private void HideTutorialTextPhone(InputAction.CallbackContext context)
    {
        if (tutorialTextPhone != null)
        {
            tutorialTextPhone.SetActive(false);
        }
    }

    private void HideTutorialTextWheelLeft(InputAction.CallbackContext context)
    {
        if (tutorialTextWheelLeft != null)
        {
            tutorialTextWheelLeft.SetActive(false);
        }
    }

    private void HideTutorialTextWheelRight(InputAction.CallbackContext context)
    {
        if (tutorialTextWheelRight != null)
        {
            tutorialTextWheelRight.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // Unregister callbacks when the object is destroyed
        despawnTextPhone.action.performed -= HideTutorialTextPhone;
        despawnTutorialWheelLeft.action.performed -= HideTutorialTextWheelLeft;
        despawnTutorialWheelRight.action.performed -= HideTutorialTextWheelRight;
    }
}
