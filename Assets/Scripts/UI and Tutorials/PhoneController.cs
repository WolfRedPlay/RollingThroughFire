using UnityEngine;
using UnityEngine.UI;
public class LightToggle : MonoBehaviour
{
    public Light targetLight; // Assign your Light object here in the Inspector
    public Button toggleButton; // Assign your Button object here in the Inspector

    private bool isLightOn = false;

    void Start()
    {
        // Ensure the light starts as disabled
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }
        else
        {
            Debug.LogError("Light not assigned in the Inspector.");
        }

        // Ensure the button has an onClick event assigned
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleLight);
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector.");
        }
    }

    void ToggleLight()
    {
        if (targetLight != null)
        {
            isLightOn = !isLightOn;
            targetLight.enabled = isLightOn; // Enable or disable the light
        }
        else
        {
            Debug.LogError("Light not assigned in the Inspector.");
        }
    }
}
