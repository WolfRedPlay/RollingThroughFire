using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionForce = 500f; // The force of the explosion
    [SerializeField] private float explosionRadius = 10f; // The radius of the explosion
    [SerializeField] private Transform explosionOrigin; // The origin of the explosion
    public GameObject explosionEffect; // Reference to the explosion effect (e.g., particle system)
    public float upwardModifier = 1f; // Upward force to apply to affected objects
                                      // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject Particle_Destroy;
    [SerializeField] int TimeToDestory = 5;

    public void Start()
    {
        TriggerExplosion();
        Destroy(Particle_Destroy, TimeToDestory);
    }
    public void TriggerExplosion()
    {
        // Instantiate the explosion effect at the position of the empty GameObject (this.transform.position)
        Particle_Destroy = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Apply explosion force to nearby objects with Rigidbody components
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }
        Debug.Log("Explosion");
    }
}
