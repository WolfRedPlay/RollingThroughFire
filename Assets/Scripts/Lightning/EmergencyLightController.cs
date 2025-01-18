using UnityEngine;

public class EmergencyLightController : MonoBehaviour
{
    public Light emergencyLight; // The Light component to control
    public Color emergencyColor = Color.red; // Primary emergency color
    public Color fadeToColor = new Color(0.5f, 0, 0); // Secondary fade color
    public float fadeDuration = 0.5f; // Time for one fade cycle (in seconds)

    private bool isEmergencyActive = false;

    /// <summary>
    /// Triggers the emergency light effect for a given duration.
    /// </summary>
    /// <param name="duration">How long the emergency effect lasts (in seconds).</param>

   
    public void EmergencyLight(float duration)
    {
        if (emergencyLight == null)
        {
            Debug.LogError("EmergencyLightController: No light assigned.");
            return;
        }

        if (isEmergencyActive)
            return; // Prevent overlapping effects

        isEmergencyActive = true;
        StartCoroutine(EmergencyLightRoutine(duration));
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
