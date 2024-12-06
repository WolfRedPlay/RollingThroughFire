using UnityEngine;

public class FlickeringLightOnOff : MonoBehaviour
{
    public Light flickeringLight; // Reference to the Light component
    public Light flickeringLight2;
    public float minFlickerTime = 0.1f; // Minimum time between flickers
    public float maxFlickerTime = 2.0f; // Maximum time between flickers

    private float nextFlickerTime;

    void Start()
    {
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }
        ScheduleNextFlicker();
    }

    void Update()
    {
        if (Time.time >= nextFlickerTime)
        {
            ToggleLight();
            ScheduleNextFlicker();
        }
    }

    void ToggleLight()
    {
        flickeringLight.enabled = !flickeringLight.enabled; // Toggle the light on or off
    }

    void ScheduleNextFlicker()
    {
        nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
    }
}