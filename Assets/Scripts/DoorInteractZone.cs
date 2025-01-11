using UnityEngine;

public class DoorInteractZone : MonoBehaviour
{
    private DoorController doorController;

    void Start()
    {
        doorController = GetComponentInParent<DoorController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Если нажали E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Получаем инвентарь игрока
                PlayerInventory inventory = other.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    doorController.TryOpenDoor(inventory);
                }
            }
        }
    }
}