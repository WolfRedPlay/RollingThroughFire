using UnityEngine;
using System.Collections;

public class DeathZoneController : MonoBehaviour
{
    [SerializeField, Tooltip("Layers that this death zone will be working with")] LayerMask Layers;
    [SerializeField] float Damage = 20;
    [SerializeField, Tooltip("Interval in seconds between each damage")] float Delay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if ((Layers.value & (1 << other.gameObject.layer)) != 0)
        {
            Health healthComponent = other.GetComponent<Health>() ?? other.GetComponentInParent<Health>() ?? other.GetComponentInChildren<Health>();
            if (healthComponent != null)
            {
                StartCoroutine(ApplyDamageOverTime(healthComponent));
            }
        }
    }

    private IEnumerator ApplyDamageOverTime(Health healthComponent)
    {
        while (healthComponent != null)
        {
            healthComponent.Damage(Damage);
            yield return new WaitForSeconds(Delay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }

        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
        }
    }
}
