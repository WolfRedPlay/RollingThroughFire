using UnityEngine;

public class SpinningDoor : MonoBehaviour
{
    [Header("Spinning Settings")]
    [Tooltip("Speed of the door's rotation (degrees per second)."), Range(0f, 360f)]
    public float spinSpeed = 90f;

    [Tooltip("Direction of rotation: true for clockwise, false for counter-clockwise.")]
    public bool clockwise = true;

    private Vector3 rotationDirection;

    void Start()
    {
        // Set the initial rotation direction based on the clockwise property
        rotationDirection = clockwise ? Vector3.up : Vector3.down;
    }

    void Update()
    {
        // Rotate the door around the global vertical axis (Y-axis)
        transform.Rotate(Vector3.up * (clockwise ? 1 : -1) * spinSpeed * Time.deltaTime, Space.World);
    }

    // Public method to change the direction of rotation
    public void ToggleDirection()
    {
        clockwise = !clockwise;
    }

    // Public method to set the spin speed dynamically
    public void SetSpinSpeed(float newSpeed)
    {
        spinSpeed = Mathf.Clamp(newSpeed, 0f, 360f);
    }
}
