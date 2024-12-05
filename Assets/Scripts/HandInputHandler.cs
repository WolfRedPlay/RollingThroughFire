using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HandInputHandler : MonoBehaviour
{
    BoxCollider triggerBox;

    Vector3 startHandPosition;

    float positionThreshold = 0.005f;

    public float HandMovement { get; set; }

    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        HandMovement = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            startHandPosition = transform.InverseTransformPoint(other.transform.position);
            Debug.Log("Entered");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            
            Vector3 newPosition = transform.InverseTransformPoint(other.transform.position);

            Debug.Log(newPosition.z - startHandPosition.z);
            if (Mathf.Abs(newPosition.z - startHandPosition.z) > positionThreshold)
            {
                HandMovement = newPosition.z - startHandPosition.z;
                startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                HandMovement = 0f;
                startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }

            Debug.Log(HandMovement);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
            HandMovement = 0f;
    }

}
