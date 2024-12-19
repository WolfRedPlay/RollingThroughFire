using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneSpawner : MonoBehaviour
{
    public GameObject phonePrefab;
    public InputActionReference actionSpawnPhone;

    void Start()
    {
        phonePrefab.SetActive(false);

        actionSpawnPhone.action.performed += AppearObject;
    }

    private void AppearObject(InputAction.CallbackContext context)
    {
        phonePrefab.SetActive(!phonePrefab.activeSelf);
    }

}
