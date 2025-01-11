using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LampLightArea : MonoBehaviour
{
    [Header("Healing Settings")]
    [Tooltip("Heal speed")]
    public float healRate = 5f;
    
    [Tooltip("Max healed percent")]
    public float healLimitPercent = 0.6f;

    private bool playerInside = false;
    private PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DarknessTimer dt = other.GetComponent<DarknessTimer>();
            if (dt != null)
            {
                dt.UpdateLightCount(+1);
            }
            
            playerInside = true;
            playerHealth = other.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DarknessTimer dt = other.GetComponent<DarknessTimer>();
            if (dt != null)
            {
                dt.UpdateLightCount(-1);
            }
            playerInside = false;
            playerHealth = null;
        }
    }

    private void Update()
    {
        if (playerInside && playerHealth != null)
        {
            float currentHP = playerHealth.currentHealth;
            float maxHP = playerHealth.maxHealth;
            float limitHP = maxHP * healLimitPercent;
            
            if (currentHP < limitHP)
            {
                float diff = limitHP - currentHP;
                
                float healThisFrame = healRate * Time.deltaTime;
                if (healThisFrame > diff)
                    healThisFrame = diff;
                playerHealth.Heal(healThisFrame);
            }
        }
    }
}
