using UnityEngine;

public class ReactorActivation : MonoBehaviour
{
    [SerializeField] private Renderer reactorRenderer; // The renderer of the reactor object
    [SerializeField] private Material activatedMaterial; // The material to switch to when activated
    //[SerializeField] private Material defaultMaterial; // Optional: The default material
    [SerializeField] private GameObject reactorLight;
    //[SerializeField] private GameObject reactorLight2;
    [SerializeField] AudioSource audioSource;


    private bool isActivated = false;

    private void Start()
    {
       
        reactorLight.SetActive(false);
        //reactorLight2.SetActive(false);

    }

    // Method to be called when the button is pressed
    public void ActivateReactor()
    {
        if (!isActivated && reactorRenderer != null && activatedMaterial != null)
        {
            reactorRenderer.material = activatedMaterial;
            reactorLight.SetActive(true);
           // reactorLight2.SetActive(true);

            isActivated = true;
            audioSource.Play();
        }
    }

    // Optional: Method to reset the reactor
    public void ResetReactor()
    {
      
        reactorLight.SetActive(false);
        //reactorLight2.SetActive(false);


    }
}
