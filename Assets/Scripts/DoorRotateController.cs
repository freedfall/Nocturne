using UnityEngine;

public class DoorRotateController : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isOpen = false;
    public bool openToLeft = false;
    public float openAngle = 90f;
    public Vector3 openOffsetLeft;
    public Vector3 openOffsetRight;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        
        float angle = openToLeft ? -openAngle : openAngle; 
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

        transform.rotation = targetRotation;
        
        Vector3 offset = openToLeft ? openOffsetLeft : openOffsetRight;
        transform.position = initialPosition + offset;
        
        var coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;
    }

    public void CloseDoor()
    {
        if (!isOpen) return;

        isOpen = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        var coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = true;
    }
}
