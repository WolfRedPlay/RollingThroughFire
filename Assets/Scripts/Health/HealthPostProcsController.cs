using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthPostProcsController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float bounceRatio = 0.1f;
    private Health healthComp;
    private Vignette vignette;

    private void Start()
    {
        healthComp = GetComponent<Health>();

        
        if (volume.profile.TryGet<Vignette>(out var vignetteOverride))
        {
            vignette = vignetteOverride;
        }
        else
        {
            Debug.LogError("Vignette override is not set up in the Volume profile.");
        }
    }

    private void Update()
    {
        if (vignette != null && healthComp != null)
        {
            
            float healthPercentage = healthComp.CurrentHealth / healthComp.MaxHealth;

            if (healthPercentage > 0)
            {
                float bounceValue = Mathf.Sin(Time.time) * bounceRatio;

                healthPercentage = Mathf.Clamp01(healthPercentage + bounceValue);
                //healthPercentage = 1 - healthPercentage;
                vignette.intensity.value = Mathf.Lerp(1, 0, healthPercentage);
            }
            else
            {
                
                vignette.intensity.value = 0;
            }
        }
    }
}
