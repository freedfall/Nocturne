using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string itemName = "HealthPotion";

    public Sprite itemSprite;
    public bool destroyOnPickUp = false;
    public int quantity = 1;
}