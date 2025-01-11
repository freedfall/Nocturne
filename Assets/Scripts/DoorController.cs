using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Sprites")]
    public Sprite doorClosedSprite;
    public Sprite doorOpenSprite;

    [Header("Door Settings")]
    public bool isLocked = false;
    public string requiredKeyName = "Key";
    public bool isOpen = false;
    public bool toRotate = false;
    public bool isFinalDoor = false;
    
    public DoorRotateController rotateController;
    public AudioSource audioSource;
    public AudioClip openClip;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider;
    
    public DeathController deathController;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        
        spriteRenderer.sprite = doorClosedSprite;
        doorCollider.enabled = true;
        isOpen = false;
    }
    public void TryOpenDoor(PlayerInventory playerInventory)
    {
        if (isOpen) return;
        
        if (isLocked)
        {
            if (!playerInventory.HasKey(requiredKeyName))
            {
                Debug.Log($"Door is locked. You need a {requiredKeyName} to open it.");
                return;
            }
            
            playerInventory.RemoveKey(requiredKeyName);
            Debug.Log($"Key {requiredKeyName} used to unlock the door.");
        }
        
        OpenDoor();
    }

    void OpenDoor()
    {
        Debug.Log("Дверь открыта!");

        isOpen = true;
        audioSource.PlayOneShot(openClip);
        if (isFinalDoor)
        {
            deathController.OnEndGame();
        }
        if (!toRotate)
        {
            spriteRenderer.sprite = doorOpenSprite;
            doorCollider.enabled = false;
        }
        else
        {
            rotateController.OpenDoor();
        }
    }
}
