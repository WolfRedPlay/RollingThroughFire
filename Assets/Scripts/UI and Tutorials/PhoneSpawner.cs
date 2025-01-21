using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PhoneSpawner : MonoBehaviour
{
    public GameObject phonePrefab; // The phone prefab
    public InputActionReference actionSpawnPhoneLeft; // Input action to spawn the phone
    public InputActionReference actionSpawnPhoneRight; // Input action to spawn the phone
    public Transform spawnPointLeft; // Reference to the empty game object's transform
    public Transform spawnPointRight; // Reference to the empty game object's transform

    private bool isPhoneActive = false; // Tracks whether the phone is currently active
    private bool isPhoneLeft = false; // Tracks whether the phone is currently active

    private void Start()
    {
        // Ensure the phonePrefab starts in the correct position and is initially hidden
        phonePrefab.SetActive(false);

        // Subscribe to the input action event
        actionSpawnPhoneLeft.action.performed += TogglePhoneLeft;
        actionSpawnPhoneRight.action.performed += TogglePhoneRight;
    }

    private void TogglePhoneLeft(InputAction.CallbackContext context)
    {
        // Toggle the active state of the phone

        if (!isPhoneActive)
        {
            isPhoneActive = true;
            isPhoneLeft = true;
            phonePrefab.SetActive(true);
            phonePrefab.transform.SetParent(spawnPointLeft);
            phonePrefab.transform.localPosition = Vector3.zero;
            phonePrefab.transform.localRotation = Quaternion.identity;
        }
        else
        {
            if (isPhoneLeft)
            {
                phonePrefab.SetActive(false);
                isPhoneActive = false;
            }
            else
            {
                isPhoneLeft = true;
                phonePrefab.transform.SetParent(spawnPointLeft);
                phonePrefab.transform.localPosition = Vector3.zero;
                phonePrefab.transform.localRotation = Quaternion.identity;
            }
        }
    }
    private void TogglePhoneRight(InputAction.CallbackContext context)
    {
        if (!isPhoneActive)
        {
            isPhoneActive = true;
            isPhoneLeft = false;
            phonePrefab.SetActive(true);
            phonePrefab.transform.SetParent(spawnPointRight);
            phonePrefab.transform.localPosition = Vector3.zero;
            phonePrefab.transform.localRotation = Quaternion.identity;
        }
        else
        {
            if (!isPhoneLeft)
            {
                phonePrefab.SetActive(false);
                isPhoneActive = false;
            }
            else
            {
                isPhoneLeft = false;
                phonePrefab.transform.SetParent(spawnPointRight);
                phonePrefab.transform.localPosition = Vector3.zero;
                phonePrefab.transform.localRotation = Quaternion.identity;
            }
        }
    }
}

