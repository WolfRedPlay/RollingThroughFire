using UnityEngine;

public class ObjectPusher : MonoBehaviour
{
    [SerializeField] private Rigidbody[] targetObjects; // Array of objects to push
    [SerializeField] private Vector3 pushDirection = new Vector3(0, 0, -1); // Direction to push the object
    [SerializeField] private float pushForce = 3f; // Magnitude of the force

    private void Start()
    {
        foreach (Rigidbody target in targetObjects)
        {
            if (target != null)
            {
                PushObject(target);
            }
            else
            {
                Debug.LogWarning("A null Rigidbody is in the targetObjects array.");
            }
        }
    }

    private void PushObject(Rigidbody target)
    {
        // Apply force to the object in the specified direction
        target.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
    }
}
