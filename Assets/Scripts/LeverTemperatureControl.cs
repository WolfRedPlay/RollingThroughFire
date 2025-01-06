using UnityEngine;

public class LeverTemperatureControl : MonoBehaviour
{
    [Header("Lever Settings")]
    [SerializeField] private Transform leverTransform; // The lever's Transform
    [SerializeField] private float minAngle = -45f; // Minimum lever angle
    [SerializeField] private float maxAngle = 45f; // Maximum lever angle

    [Header("Temperature Settings")]
    [SerializeField] private ReactorTemperatureManager reactorTemperatureManager; // Reference to the temperature manager
    [SerializeField] private float minTemperatureChange = 0f; // Minimum temperature change
    [SerializeField] private float maxTemperatureChange = 100f; // Maximum temperature change

    private void Update()
    {
        UpdateReactorTemperature();
    }

    private void UpdateReactorTemperature()
    {
        if (leverTransform == null || reactorTemperatureManager == null) return;

        // Calculate the current angle of the lever
        float currentAngle = leverTransform.localEulerAngles.x;

        // Normalize the angle to the range -180 to 180
        if (currentAngle > 180f)
        {
            currentAngle -= 360f;
        }

        // Clamp the angle to the specified min and max
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        // Map the angle to the temperature range
        float normalizedAngle = Mathf.InverseLerp(minAngle, maxAngle, currentAngle);
        float temperatureChange = Mathf.Lerp(minTemperatureChange, maxTemperatureChange, normalizedAngle);

        // Update the reactor temperature
        reactorTemperatureManager.IncreaseTemperature(temperatureChange * Time.deltaTime);
    }
}
