using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;

    private void Start()
    {
        if (playerHealth != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
            
            playerHealth.onHealthChanged.AddListener(UpdateHealthBar);
        }
    }
    
    public void UpdateHealthBar()
    {
        healthSlider.value = playerHealth.currentHealth;
    }
}