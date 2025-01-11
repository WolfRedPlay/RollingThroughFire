using UnityEngine;
public class ReactorTemperatureManager : MonoBehaviour
{
    [Header("Temperature Settings")]
    [SerializeField] private float minTemperature = 0f; // Minimum possible temperature
    [SerializeField] private float maxTemperature = 100f; // Maximum possible temperature
    [SerializeField] private float minPower = 1f; // Minimum possible temperature
    [SerializeField] private float maxPower = 16f; // Minimum possible temperature
    public float currentTemperature = 50f; // Starting temperature
    public float currentPower = 0f; // Starting temperature
    [SerializeField] private bool isActive = false; // Determines if temperature changes are active

    public float CurrentTemperature => currentTemperature; // Public getter for the current temperature
    public float CurrentPower => currentPower; // Public getter for the current temperature

    public float MinTemperature => minTemperature; // Public getter for the minimum temperature
    public float MinPower => minPower; // Public getter for the minimum temperature

    public float MaxTemperature => maxTemperature; // Public getter for the maximum temperature
    public float MaxPower => maxPower;

    private void Start()
    {
        // Clamp the starting temperature to ensure it's within bounds
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);
        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);

    }

    public void ActivateTemperatureManager()
    {
        isActive = true;
    }

    // Method to increase the temperature
    public void IncreaseTemperature(float amount)
    {
        if (!isActive) return; // Ensure temperature changes only if active
        currentTemperature = Mathf.Clamp(currentTemperature + amount, minTemperature, maxTemperature);

    }

    public void IncreasePower(float amount)
    {
        if (!isActive) return; // Ensure temperature changes only if active
        currentPower = Mathf.Clamp(currentPower + amount, minPower, maxPower);

    }

    // Method to decrease the temperature
    public void DecreaseTemperature(float amount)
    {
        if (!isActive) return; // Ensure temperature changes only if active
        currentTemperature = Mathf.Clamp(currentTemperature - amount, minTemperature, maxTemperature);

    }
    public void DecreasePower(float amount)
    {
        if (!isActive) return; // Ensure temperature changes only if active
        currentPower = Mathf.Clamp(currentPower - amount, minPower, maxPower);

    }

    // Optional: Reset the temperature to a default value
    public void ResetTemperature()
    {
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);
    }

    // Optional: Debugging output for testing
    //private void Update()
    //{
    //    Debug.Log($"Current Temperature: {currentTemperature}");
    //}
}
