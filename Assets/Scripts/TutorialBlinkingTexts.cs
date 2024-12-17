using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImages : MonoBehaviour
{
    public Image[] tutorialImages; // Array to hold multiple Image objects
    public float blinkSpeed = 1f;  // Speed of the fade-in and fade-out

    private Color[] originalColors;

    void Start()
    {
        if (tutorialImages == null || tutorialImages.Length == 0)
        {
            Debug.LogError("No tutorial images assigned.");
            return;
        }

        // Store the original colors of all images
        originalColors = new Color[tutorialImages.Length];
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            if (tutorialImages[i] != null)
                originalColors[i] = tutorialImages[i].color;
        }
    }

    void Update()
    {
        if (tutorialImages != null)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);

            // Apply blinking effect to each image
            for (int i = 0; i < tutorialImages.Length; i++)
            {
                if (tutorialImages[i] != null)
                {
                    tutorialImages[i].color = new Color(
                        originalColors[i].r,
                        originalColors[i].g,
                        originalColors[i].b,
                        alpha
                    );
                }
            }
        }
    }
}
