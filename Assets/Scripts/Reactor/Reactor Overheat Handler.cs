using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


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
    [SerializeField] private GameObject normalPipe1; // The GameObject to toggle
    [SerializeField] private GameObject normalPipe2; // The GameObject to toggle
    [SerializeField] private GameObject normalPipe3; // The GameObject to toggle
    [SerializeField] private GameObject normalPipe4; // The GameObject to toggle
    [SerializeField] private GameObject destroyedPipe1; // The GameObject to toggle
    [SerializeField] private GameObject destroyedPipe2; // The GameObject to toggle
    [SerializeField] private GameObject destroyedPipe3; // The GameObject to toggle
    [SerializeField] private GameObject destroyedPipe4; // The GameObject to toggle

    [SerializeField] private GameObject explosionPlasma; // The GameObject to toggle
    [SerializeField] private GameObject explosionPlasma2; // The GameObject to toggle
    [SerializeField] private GameObject explosionFire; // The GameObject to toggle

    public float overheatThreshold = 1800f; // Temperature threshold for overheat
    [SerializeField] private float delay = 5f; // Delay in seconds for toggling objects
    [SerializeField] private float delay2 = 1f; // Delay in seconds for toggling objects
    [SerializeField] private float delay3 = 1.5f; // Delay in seconds for toggling objects

    [SerializeField] private GameObject prototypeFriendlyText;
    [SerializeField] private GameObject notPrototypeFriendlyText;

    [SerializeField] private GameObject notHotEnoughSystemMessage;
    [SerializeField] private GameObject warningSystemMessage;



    public UnityEvent OnReactorExploded;




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
            StartCoroutine(FirstExplosionDelay());
            explosionPlasma.SetActive(true);
            prototypeFriendlyText.SetActive(true);
            notPrototypeFriendlyText.SetActive(false);
            warningSystemMessage.SetActive(true) ;
            notHotEnoughSystemMessage.SetActive(false);




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
            StartCoroutine(SecondExplosionDelay());
        }
        else
        {
            Debug.Log("Reactor temperature is below the threshold. Action not performed.");
            notHotEnoughSystemMessage.SetActive(true);
        }
    }

    private IEnumerator FirstExplosionDelay()
    {
        yield return new WaitForSeconds(delay2);

        // Toggle specific objects
        brokenGlassReactorInside.SetActive(true);
        normalReactorGlass.SetActive(false);
        explosionPlasma2.SetActive(true);
        destroyedPipe1.SetActive(true);
        destroyedPipe2.SetActive(true);
        destroyedPipe3.SetActive(true);
        destroyedPipe4.SetActive(true);

        normalPipe1.SetActive(false);
        normalPipe2.SetActive(false);
        normalPipe3.SetActive(false);
        normalPipe4.SetActive(false);





        Debug.Log("Toggled objects after delay.");
    }
    private IEnumerator SecondExplosionDelay()
    {
        yield return new WaitForSeconds(delay);

        // Toggle specific objects
        normalGlassReactorOutside.SetActive(false);
        brokenGlassReactorOutside.SetActive(true);
        explosionFire.SetActive(true);

        Debug.Log("Toggled objects after delay.");
        StartCoroutine(DeactivateExplosionsDelay());

    }

    private IEnumerator DeactivateExplosionsDelay()
    {
        yield return new WaitForSeconds(delay3);

        // Toggle specific objects
        explosionFire.SetActive(false);

        OnReactorExploded?.Invoke();
        OnReactorExploded = null;


        Debug.Log("Toggled objects after delay.");
    }





}

