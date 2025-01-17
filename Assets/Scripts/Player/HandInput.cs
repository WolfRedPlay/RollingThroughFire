using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(XRSimpleInteractable))]
public class HandInput : MonoBehaviour
{
    const float positionThreshold = 0.005f;


    XRSimpleInteractable _interactable;
    bool _isActive;
    Vector3 _startHandPosition;
    Transform _handTransform;
    bool _isBreak;
    SettingsHandler _settingsHandler;

    public bool IsBreak => _isBreak;
    public void SetBreak(bool value) { _isBreak = value; }
    public float HandMovement { get; set; }


    void Start()
    {
        _settingsHandler = FindAnyObjectByType<SettingsHandler>(FindObjectsInactive.Include);

        _interactable = GetComponent<XRSimpleInteractable>();

        _interactable.firstSelectEntered.AddListener(OnGrabbed);
        _interactable.lastSelectExited.AddListener(OnReleased);

        _isActive = false;
        _isBreak = false;

        HandMovement = 0f;

    }

    private void OnReleased(SelectExitEventArgs arg0)
    {
        _isActive = false;
        _startHandPosition = Vector3.zero;
        _handTransform = null;
        HandMovement = 0f;

    }

    private void OnGrabbed(SelectEnterEventArgs arg0)
    {
        _isActive = true;
        _startHandPosition = transform.InverseTransformPoint(arg0.interactorObject.transform.position);
        _handTransform = arg0.interactorObject.transform;
    }

    void Update()
    {
        if (_isActive)
        {
            Vector3 newPosition = transform.InverseTransformPoint(_handTransform.position);

            if ((Mathf.Abs(newPosition.z - _startHandPosition.z) > positionThreshold) && !_isBreak)
            {
                HandMovement = (newPosition.z - _startHandPosition.z) * 80f;
                if (_settingsHandler.MovementHelper) HandMovement *= 1.5f;
                _startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                HandMovement = 0f;
                _startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
        }
    }



    private void OnDestroy()
    {
        _interactable.selectEntered.RemoveListener(OnGrabbed);
        _interactable.lastSelectExited.RemoveListener(OnReleased);
    }
}
