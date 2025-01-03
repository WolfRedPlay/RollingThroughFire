using UnityEngine;

public class ReactorActivation : MonoBehaviour
{
    [SerializeField] private Renderer reactorRenderer; // The renderer of the reactor object
    [SerializeField] private Material activatedMaterial; // The material to switch to when activated
    [SerializeField] private Material defaultMaterial; // Optional: The default material
    [SerializeField] private GameObject reactorLight;

    private bool isActivated = false;

    private void Start()
    {
        // Ensure the reactor starts with the default material
        if (reactorRenderer != null && defaultMaterial != null)
        {
            reactorRenderer.material = defaultMaterial;
        }
        reactorLight.SetActive(false);
    }

    // Method to be called when the button is pressed
    public void ActivateReactor()
    {
        if (!isActivated && reactorRenderer != null && activatedMaterial != null)
        {
            reactorRenderer.material = activatedMaterial;
            reactorLight.SetActive(true);
            isActivated = true;
        }
    }

    // Optional: Method to reset the reactor
    public void ResetReactor()
    {
        if (isActivated && reactorRenderer != null && defaultMaterial != null)
        {
            reactorRenderer.material = defaultMaterial;
            isActivated = false;
        }
        reactorLight.SetActive(false);

    }
}
