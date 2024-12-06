using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandlerManager : MonoBehaviour
{
    [SerializeField] Transform _attachTransform;
    [SerializeField] GameObject _customHand;

    XRSimpleInteractable _interactable;
    bool _isBreak = false;
    public bool IsBreak => _isBreak;


    void Start()
    {
        _interactable = GetComponent<XRSimpleInteractable>();

        _interactable.selectEntered.AddListener(OnGrab);
        _interactable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs arg0)
    {
        //_custonHand.SetActive(false);
        
        _isBreak = false;
    }

    private void OnGrab(SelectEnterEventArgs arg)
    {

        //_custonHand.SetActive(true);
        _isBreak = true;
    }

}
