using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HandInputHandler : MonoBehaviour
{
    
    BoxCollider _triggerBox;
    Vector3 _startHandPosition;
    float positionThreshold = 0.005f;
    bool _isBreak = false;



    public bool IsBreak => _isBreak;
    public void SetBreak(bool value) {  _isBreak = value; }

    public float HandMovement { get; set; }




    private void Start()
    {
        _triggerBox = GetComponent<BoxCollider>();
        HandMovement = 0f;
        _isBreak = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            _startHandPosition = transform.InverseTransformPoint(other.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            
            Vector3 newPosition = transform.InverseTransformPoint(other.transform.position);

            if ((Mathf.Abs(newPosition.z - _startHandPosition.z) > positionThreshold) && !_isBreak)
            {
                HandMovement = (newPosition.z - _startHandPosition.z) * 100f;
                _startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
            else
            {
                HandMovement = 0f;
                _startHandPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
            HandMovement = 0f;
    }

}
