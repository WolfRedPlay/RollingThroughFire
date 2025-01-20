using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneSpawnerRightHand : MonoBehaviour
{
  
    public GameObject phonePrefab; // The phone prefab
    public InputActionReference actionSpawnPhone; // Input action to spawn the phone
    public Transform spawnPoint; // Reference to the empty game object's transform

    private bool isPhoneActive = false; // Tracks whether the phone is currently active

    private void Start()
    {
        // Ensure the phonePrefab starts in the correct position and is initially hidden
        phonePrefab.transform.position = spawnPoint.position;
        phonePrefab.transform.rotation = spawnPoint.rotation;
        phonePrefab.SetActive(false);

        // Subscribe to the input action event
        actionSpawnPhone.action.performed += TogglePhone;
    }

    private void Update()
    {
        // If the phone is active, make it follow the spawn point
        if (isPhoneActive)
        {
            phonePrefab.transform.position = spawnPoint.position;
            phonePrefab.transform.rotation = spawnPoint.rotation;
        }
    }

    private void TogglePhone(InputAction.CallbackContext context)
    {
        // Toggle the active state of the phone
        isPhoneActive = !isPhoneActive;
        phonePrefab.SetActive(isPhoneActive);

        // Update the phone's position and rotation immediately when activated
        if (isPhoneActive)
        {
            phonePrefab.transform.position = spawnPoint.position;
            phonePrefab.transform.rotation = spawnPoint.rotation;
        }
    }
}


