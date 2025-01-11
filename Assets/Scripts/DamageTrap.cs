using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageAmount = 10f;      
    public bool continuousDamage = false;     
    public float damageInterval = 1f;          
    
    private float damageTimer = 0f;
    [Header("Effects")]
    public AudioSource audioSource;
    public AudioClip clip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyDamage(other);
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (continuousDamage && other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                ApplyDamage(other);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (continuousDamage && other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }

    private void ApplyDamage(Collider2D playerCollider)
    {
        PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}