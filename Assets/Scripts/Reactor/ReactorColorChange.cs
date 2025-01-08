using UnityEngine;

public class ReactorMaterialController : MonoBehaviour
{
    [Header("Material and Shader Properties")]
    [SerializeField] private Renderer reactorRenderer; // Assign the reactor renderer here
    [SerializeField] private string deepWaterColorProperty; // Replace with correct property name
    [SerializeField] private string shallowWaterColorProperty; // Replace with correct property name
    [SerializeField] private string electricColorProperty; // Replace with correct property name
    [SerializeField] private string fresnelPower; // Replace with correct property name


    [Header("Temperature Settings")]
    [SerializeField] private ReactorTemperatureManager temperatureManager; // Reference to the temperature manager
    [SerializeField] private Color coldDeepColor = Color.cyan;
    [SerializeField] private Color mediumDeepColor = Color.cyan;
    [SerializeField] private Color hotDeepColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color coldShallowColor = Color.blue;
    [SerializeField] private Color mediumShallowColor = Color.cyan;
    [SerializeField] private Color hotShallowColor = Color.red;
    [SerializeField] private Color hotElectricColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color mediumElectricColor = Color.cyan;
    [SerializeField] private Color coldElectricColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private float minFresnelPower = 1f;
    [SerializeField] private float maxFresnelPower = 5f;

    [Header("Spotlight Settings")]
    [SerializeField] private Light reactorSpotlight; // Assign the spotlight here
    //[SerializeField] private Light reactorSpotlight2; // Assign the spotlight here
    [SerializeField] private Color coldLightColor = Color.blue;
    [SerializeField] private Color midLightColor = Color.yellow;
    [SerializeField] private Color hotLightColor = Color.red;
    [SerializeField] private float minLightIntensity = 1f;
    [SerializeField] private float maxLightIntensity = 5f;
    [SerializeField] private float minLightRange = 10f;
    [SerializeField] private float maxLightRange = 20f;

    private Material reactorMaterial;

    private void Start()
    {

        if (reactorSpotlight == null)
        {
            Debug.LogWarning("Spotlight is not assigned. Light adjustments will be skipped.");
        }
    }

    private void Update()
    {

        if (temperatureManager != null)
        {
            UpdateMaterialColors();
        }

        if (temperatureManager != null)
        {
            UpdateLightProperties();
        }

        if (temperatureManager != null)
        {
            UpdateFresnelPower();
        }
    }

    private void UpdateMaterialColors()
    {
        Material mat = reactorRenderer.material;

        float normalizedTemp = Mathf.InverseLerp(
            temperatureManager.MinTemperature,
            temperatureManager.MaxTemperature,
            temperatureManager.CurrentTemperature
        );

        // Determine if the normalized temperature is in the lower or upper half of the range
        float mediumThreshold = 0.5f;

        // Deep color interpolation
        Color currentDeepColor;
        if (normalizedTemp < mediumThreshold)
        {
            float t = Mathf.InverseLerp(0f, mediumThreshold, normalizedTemp);
            currentDeepColor = Color.Lerp(coldDeepColor, mediumDeepColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(mediumThreshold, 1f, normalizedTemp);
            currentDeepColor = Color.Lerp(mediumDeepColor, hotDeepColor, t);
        }

        // Shallow color interpolation
        Color currentShallowColor;
        if (normalizedTemp < mediumThreshold)
        {
            float t = Mathf.InverseLerp(0f, mediumThreshold, normalizedTemp);
            currentShallowColor = Color.Lerp(coldShallowColor, mediumShallowColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(mediumThreshold, 1f, normalizedTemp);
            currentShallowColor = Color.Lerp(mediumShallowColor, hotShallowColor, t);
        }

        // Electric color interpolation
        Color currentElectricColor;
        if (normalizedTemp < mediumThreshold)
        {
            float t = Mathf.InverseLerp(0f, mediumThreshold, normalizedTemp);
            currentElectricColor = Color.Lerp(coldElectricColor, mediumElectricColor, t);
        }
        else
        {
            float t = Mathf.InverseLerp(mediumThreshold, 1f, normalizedTemp);
            currentElectricColor = Color.Lerp(mediumElectricColor, hotElectricColor, t);
        }

        // Apply colors to material
        if (mat.HasProperty(deepWaterColorProperty))
        {
            mat.SetColor(deepWaterColorProperty, currentDeepColor);
        }

        if (mat.HasProperty(shallowWaterColorProperty))
        {
            mat.SetColor(shallowWaterColorProperty, currentShallowColor);
        }

        if (mat.HasProperty(electricColorProperty))
        {
            mat.SetColor(electricColorProperty, currentElectricColor);
        }
    }

    private void UpdateLightProperties()
    {
        float normalizedTemp = Mathf.InverseLerp(
            temperatureManager.MinTemperature,
            temperatureManager.MaxTemperature,
            temperatureManager.CurrentTemperature
        );

        // Interpolate between cold, mid, and hot colors
        Color currentLightColor;
        if (normalizedTemp < 0.5f)
        {
            currentLightColor = Color.Lerp(coldLightColor, midLightColor, normalizedTemp * 2f);
        }
        else
        {
            currentLightColor = Color.Lerp(midLightColor, hotLightColor, (normalizedTemp - 0.5f) * 2f);
        }
        reactorSpotlight.color = currentLightColor;
        //reactorSpotlight2.color = currentLightColor;


        // Adjust intensity
        reactorSpotlight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, normalizedTemp);
        //reactorSpotlight2.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, normalizedTemp);


        // Adjust range
        reactorSpotlight.range = Mathf.Lerp(minLightRange, maxLightRange, normalizedTemp);
       // reactorSpotlight2.range = Mathf.Lerp(minLightRange, maxLightRange, normalizedTemp);

    }
    private void UpdateFresnelPower()
    {
        Material mat = reactorRenderer.material;

        float normalizedTemp = Mathf.InverseLerp(
            temperatureManager.MinTemperature,
            temperatureManager.MaxTemperature,
            temperatureManager.CurrentTemperature
        );

        // Calculate Fresnel Power based on temperature
        float currentFresnelPower = Mathf.Lerp(minFresnelPower, maxFresnelPower, normalizedTemp);

        if (mat.HasProperty(fresnelPower))
        {
            mat.SetFloat(fresnelPower, currentFresnelPower);
        }
    }
}
