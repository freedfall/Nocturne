using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Slot")]
    public PickUpItem currentItem;
    public int currentItemQuantity = 0;
    
    [Header("Sounds")]
    public AudioClip matchesSound;
    public AudioSource audioSource;
    
    [Header("Keys Inventory")]
    public List<string> keys = new List<string>();
    public TextMeshProUGUI keysUI;
    public Image keyImage;
    public Sprite keySprite;
    
    [Header("Final key")]
    public Image finalKeyImage;
    public Sprite finalKeySprite;
    
    [Header("UI")]
    public Image itemSlotImage;
    public Sprite emptySlotSprite;
    public TextMeshProUGUI itemQuantityText;

    [Header("References")]
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public DarknessTimer darknessTimer;
    
    private bool canPickUp = false;
    private PickUpItem itemToPickUp; 
    private bool hasFinalKey = false;
    void Start()
    {
        currentItem = null;
        currentItemQuantity = 0;
        UpdateUI();
        UpdateKeysUI();
        updateItemsQuantityUI();
    }

    void Update()
    {
        if (canPickUp && itemToPickUp != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUp(itemToPickUp);
        }
        
        if (currentItem != null && currentItemQuantity > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            UseItem(currentItem);
        }
    }
    
    void UpdateKeysUI()
    {
        int keysCount = keys.Count;
        if (hasFinalKey)
        {
            keysCount -= 1;
        }
        if(keysUI != null && keysCount > 0)
        {
            keysUI.text = $"{keysCount}";
            keyImage.enabled = true;
            keyImage.sprite = keySprite;
        }

        if (keysUI != null && keysCount == 1)
        {
            keysUI.text = "";
            keyImage.enabled = true;
            keyImage.sprite = keySprite;
        }

        if (keysUI != null && keysCount == 0)
        {
            keyImage.enabled = false;
        }
    }

    void updateItemsQuantityUI()
    {
        if (itemQuantityText != null && currentItemQuantity > 1)
        {
            itemQuantityText.text = $"{currentItemQuantity}";
        }

        if (currentItemQuantity == 1)
        {
            itemQuantityText.text = $"";
        }
    }

    // Метод подбора
    void PickUp(PickUpItem item)
    {
        if (item.itemName == "Key")
        {
            keys.Add(item.itemName);
            Debug.Log($"Picked up a key: {item.itemName}");
            UpdateKeysUI();
        }

        else if (item.itemName == "FinalKey")
        {
            finalKeyImage.enabled = true;
            finalKeyImage.sprite = finalKeySprite;
            hasFinalKey = true;
            keys.Add(item.itemName);
        }
        else
        {
            if (currentItem == null)
            {
                currentItem = item;
                currentItemQuantity = item.quantity;
            }
            else if (currentItem.itemName == item.itemName)
            {
                currentItemQuantity += item.quantity;
            }
            else
            {
                Debug.LogWarning($"Cannot pick up {item.itemName}: inventory contains {currentItem.itemName}");
                return;
            }

            UpdateUI();
        }
        
        if (item.destroyOnPickUp)
        {
            Destroy(item.gameObject);
        }
        else
        {
            item.gameObject.SetActive(false);
        }
        updateItemsQuantityUI();
        Debug.Log($"Picked up: {item.itemName}, Total: {currentItemQuantity}");
    }
    
    void UseItem(PickUpItem item)
    {
        if (item.itemName == "HealthPotion")
        {
            if (playerHealth != null)
            {
                playerHealth.RestoreFullHealth();
                Debug.Log("Used potion: full HP restored!");
            }
        }

        if (item.itemName == "Matches")
        {
            darknessTimer = GetComponent<DarknessTimer>();
            audioSource.PlayOneShot(matchesSound);
            
            if (darknessTimer != null)
            {
                darknessTimer.ResetDarknessTimer();
                Debug.Log("Used Matches: Darkness timer reset, light extended!");

                DarknessTimerUI darknessTimerUI = FindObjectOfType<DarknessTimerUI>();
                if (darknessTimerUI != null)
                {
                    darknessTimerUI.ForceUpdateUI();
                }
            }
            else
            {
                Debug.LogWarning("DarknessTimer not found in the scene!");
            }
        }
        
        
        if (item.itemName == "LightGem")
        {
            GameObject newItem = Instantiate(item.gameObject);
            newItem.SetActive(true);

            newItem.transform.position = transform.position;
            
            PickUpItem newPickUpItem = newItem.GetComponent<PickUpItem>();
            if (newPickUpItem != null)
            {
                newPickUpItem.quantity = 1;
            }

            Debug.Log("Item placed at player's position: " + transform.position);
        }
        
        currentItemQuantity--;
        
        if (currentItemQuantity <= 0)
        {
            currentItem = null;
            currentItemQuantity = 0;
        }

        UpdateUI();
        updateItemsQuantityUI();
    }
    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    public void RemoveKey(string keyName)
    {
        if (keys.Contains(keyName))
        {
            keys.Remove(keyName);
            Debug.Log($"Key {keyName} has been removed from the inventory.");
            UpdateKeysUI();
        }
        else
        {
            Debug.LogWarning($"Key {keyName} not found in inventory!");
        }
    }
    
    void UpdateUI()
    {
        if (currentItem != null)
        {
            itemSlotImage.enabled = true;
            itemSlotImage.sprite = currentItem.itemSprite;
        }
        else
        {
            itemSlotImage.enabled = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PickUpItem pItem = other.GetComponent<PickUpItem>();
        if (pItem != null)
        {
            canPickUp = true;
            itemToPickUp = pItem;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        PickUpItem pItem = other.GetComponent<PickUpItem>();
        if (pItem == itemToPickUp)
        {
            canPickUp = false;
            itemToPickUp = null;
        }
    }
}
