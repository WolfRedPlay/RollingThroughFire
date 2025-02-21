using UnityEngine;
using TMPro;
using System.Numerics;

public class ConsoleDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI temperatureText; // Drag TMP text object here
    [SerializeField] private TextMeshProUGUI temperatureText2; // Drag TMP text object here
    [SerializeField] private TextMeshProUGUI powerText; // Drag TMP text object here


    [SerializeField] private ReactorTemperatureManager reactor; // Drag your reactor object here (or script managing the temperature)
    [SerializeField] private GameObject prototypeFriendlyText;
    [SerializeField] private GameObject notPrototypeFriendlyText;

    void Update()
    {
        // Assuming the reactor has a method or property to get the temperature
        float temperature = reactor.currentTemperature;
        float power = reactor.currentPower;


        // Update the TMP text with the current temperature value
        temperatureText.text = $"{temperature:F2}";
        temperatureText2.text = $"{temperature:F2}";
        powerText.text = $"{power:F2}";

        if (reactor.CurrentTemperature > 1800)
        {
            prototypeFriendlyText.SetActive(true);
            notPrototypeFriendlyText.SetActive(false);

        }
    }
}
