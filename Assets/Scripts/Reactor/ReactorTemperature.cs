using UnityEngine;
public class ReactorTemperatureManager : MonoBehaviour
{
    [Header("Temperature Settings")]
    [SerializeField] private float minTemperature = 0f; // Minimum possible temperature
    [SerializeField] private float maxTemperature = 100f; // Maximum possible temperature
    [SerializeField] private float currentTemperature = 50f; // Starting temperature

    public float CurrentTemperature => currentTemperature; // Public getter for the current temperature
    public float MinTemperature => minTemperature; // Public getter for the minimum temperature
    public float MaxTemperature => maxTemperature; // Public getter for the maximum temperature

    private void Start()
    {
        // Clamp the starting temperature to ensure it's within bounds
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);
    }

    // Method to increase the temperature
    public void IncreaseTemperature(float amount)
    {
        currentTemperature = Mathf.Clamp(currentTemperature + amount, minTemperature, maxTemperature);
    }

    // Method to decrease the temperature
    public void DecreaseTemperature(float amount)
    {
        currentTemperature = Mathf.Clamp(currentTemperature - amount, minTemperature, maxTemperature);
    }

    // Optional: Reset the temperature to a default value
    public void ResetTemperature()
    {
        currentTemperature = Mathf.Clamp(currentTemperature, minTemperature, maxTemperature);
    }

    // Optional: Debugging output for testing
    private void Update()
    {
        Debug.Log($"Current Temperature: {currentTemperature}");
    }
}
