using UnityEngine;

public class DestroyReactor : MonoBehaviour
{
    // References to the objects to enable and disable
    public GameObject reactorGlass;
    public GameObject reactorGlassBroken;

    // Method to enable one object and disable another
    public void BreakGlass()
    {
        if (reactorGlassBroken != null)
        {
            reactorGlassBroken.SetActive(true); // Enable the specified object
        }

        if (reactorGlass != null)
        {
            reactorGlass.SetActive(false); // Disable the specified object
        }
    }
}
