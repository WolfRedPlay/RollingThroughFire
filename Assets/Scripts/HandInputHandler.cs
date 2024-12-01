using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HandInputHandler : MonoBehaviour
{
    BoxCollider triggerBox;

    Vector3 startHandPosition;

    float positionTreshold = 0.01f;

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
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Hand"))
        {
            //Debug.Log(other.transform.position.x - startHandPosition.x);
            Vector3 newPosition = transform.InverseTransformPoint(other.transform.position);
            if (Mathf.Abs(newPosition.z - startHandPosition.z) > positionTreshold)
            {
                HandMovement = newPosition.z - startHandPosition.z;
                startHandPosition = newPosition;
            }
            else
            {
                HandMovement = 0f;
                startHandPosition = newPosition;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
            HandMovement = 0f;
    }

}
