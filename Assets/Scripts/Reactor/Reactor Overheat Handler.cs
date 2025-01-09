using System.Collections;
using UnityEngine;

public class ReactorOverheatHandler : MonoBehaviour
{
    [Header("Reactor Settings")]
    [SerializeField] private ReactorTemperatureManager reactorTemperatureManager; // Reference to the temperature manager
    [SerializeField] private Material reactorMaterial; // The material to change
    [SerializeField] private string targetMaterialProperty = "_BaseColor"; // Shader property to change (e.g., _BaseColor)
    [SerializeField] private Color overheatedColor = Color.red; // The color to apply when overheated

    [Header("UI and GameObject Settings")]
    [SerializeField] private GameObject brokenGlassReactorInside; // The GameObject to toggle
    [SerializeField] private GameObject brokenGlassReactorOutside; // The GameObject to toggle
    [SerializeField] private GameObject normalGlassReactorOutside; // The GameObject to toggle
    [SerializeField] private GameObject normalReactorGlass; // The GameObject to toggle
    [SerializeField] private float overheatThreshold = 1800f; // Temperature threshold for overheat
    [SerializeField] private float delay = 5f; // Delay in seconds for toggling objects

   
    



    /// <summary>
    /// Called when the UI button is pressed.
    /// </summary>
    public void OnButtonPress()
    {
        if (reactorTemperatureManager == null || reactorMaterial == null || brokenGlassReactorInside == null)
        {
            Debug.LogError("Missing references. Please assign the required fields.");
            return;
        }

        // Check if the reactor temperature exceeds the threshold
        if (reactorTemperatureManager.CurrentTemperature > overheatThreshold)
        {
            // Immediate changes
            brokenGlassReactorInside.SetActive(true);
            normalReactorGlass.SetActive(false);


            // Change the reactor material color
            if (reactorMaterial.HasProperty(targetMaterialProperty))
            {
                reactorMaterial.SetColor(targetMaterialProperty, overheatedColor);
            }
            else
            {
                Debug.LogError($"Material does not have the property: {targetMaterialProperty}");
            }

            // Delayed changes for specific objects
            StartCoroutine(ToggleObjectsAfterDelay());
        }
        else
        {
            Debug.Log("Reactor temperature is below the threshold. Action not performed.");
        }
    }

    /// <summary>
    /// Coroutine to toggle specific objects after a delay.
    /// </summary>
    private IEnumerator ToggleObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Toggle specific objects
        normalGlassReactorOutside.SetActive(false);
        brokenGlassReactorOutside.SetActive(true);
        Debug.Log("Toggled objects after delay.");
    }

   

    


}

