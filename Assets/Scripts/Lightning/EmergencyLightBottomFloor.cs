using UnityEngine;


public class EmergencyLightControllerBottomFloor : MonoBehaviour
{
    public Light emergencyLight; // The Light component to control
    public Color emergencyColor = Color.red; // Primary emergency color
    public Color fadeToColor = new Color(0.5f, 0, 0); // Secondary fade color
    public float fadeDuration = 0.5f; // Time for one fade cycle (in seconds)
    public float emergencyDuration = 10f; // Total duration for the emergency light effect

    private bool isEmergencyActive = false;

    private void Start()
    {
        if (emergencyLight == null)
        {
            Debug.LogError("EmergencyLightController: No light assigned.");
            return;
        }

        // Automatically start the emergency light effect
        StartCoroutine(EmergencyLightRoutine(emergencyDuration));
    }

    private System.Collections.IEnumerator EmergencyLightRoutine(float duration)
    {
        float elapsedTime = 0f;
        bool toggle = false;

        while (elapsedTime < duration)
        {
            // Alternate between the colors
            emergencyLight.color = toggle ? emergencyColor : fadeToColor;
            toggle = !toggle;

            // Wait for fadeDuration and update elapsed time
            yield return new WaitForSeconds(fadeDuration);
            elapsedTime += fadeDuration;
        }

        // Reset the light to its default state
        emergencyLight.color = Color.white;
        isEmergencyActive = false;
    }
}
