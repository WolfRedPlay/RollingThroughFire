using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TutorialTextDespawner : MonoBehaviour
{
    public GameObject tutorialTextPhone;
    public GameObject tutorialTextWheelLeft;
    public GameObject tutorialTextWheelRight;
    public GameObject tutorialTextBreakRight;
    public GameObject tutorialTextBreakLeft;

    public InputActionReference despawnTextPhone;
    [SerializeField] XRSimpleInteractable leftWheel;
    [SerializeField] XRSimpleInteractable rightWheel;

    void Start()
    {
        // Register callbacks for the input actions
        despawnTextPhone.action.performed += HideTutorialTextPhone;

        leftWheel.firstSelectEntered.AddListener(HideGrabTutorialLeft);
        rightWheel.firstSelectEntered.AddListener(HideGrabTutorialRight);

        leftWheel.activated.AddListener(HideBreakTutorialLeft);
        rightWheel.activated.AddListener(HideBreakTutorialRight);

    }

    private void HideBreakTutorialRight(ActivateEventArgs arg0)
    {
        if (tutorialTextBreakRight != null)
        {
            tutorialTextBreakRight.SetActive(false);
            rightWheel.activated.RemoveListener(HideBreakTutorialLeft);
        }
    }

    private void HideBreakTutorialLeft(ActivateEventArgs arg0)
    {
        if (tutorialTextBreakLeft != null)
        {
            tutorialTextBreakLeft.SetActive(false);
            leftWheel.activated.RemoveListener(HideBreakTutorialLeft);
        }
    }

    private void HideGrabTutorialRight(SelectEnterEventArgs arg0)
    {
        if (tutorialTextWheelRight != null && tutorialTextBreakRight != null)
        {
            tutorialTextWheelRight.SetActive(false);
            tutorialTextBreakRight.SetActive(true);
            rightWheel.firstSelectEntered.RemoveListener(HideGrabTutorialRight);
        }
    }

    private void HideGrabTutorialLeft(SelectEnterEventArgs arg0)
    {
        if (tutorialTextWheelLeft != null && tutorialTextBreakLeft != null)
        {
            tutorialTextWheelLeft.SetActive(false);
            tutorialTextBreakLeft.SetActive(true);
            leftWheel.firstSelectEntered.RemoveListener(HideGrabTutorialLeft);

        }
    }

    private void HideTutorialTextPhone(InputAction.CallbackContext context)
    {
        if (tutorialTextPhone != null)
        {
            tutorialTextPhone.SetActive(false);
            despawnTextPhone.action.performed -= HideTutorialTextPhone;
        }
    }

}
