using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    
    [Header("Events")]
    public UnityEvent onPlayerDeath;
    public UnityEvent onHealthChanged;
    public Animator animator;
    
    public AudioSource audioSource;
    public AudioClip damageClip;
    
    [Header("Death controller")]
    public DeathController deathController;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (onHealthChanged != null)
            onHealthChanged.Invoke(); 

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            deathController.OnDeath();
        }
        else
        {
            audioSource.PlayOneShot(damageClip);
            animator.SetTrigger("Damage");
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (onHealthChanged != null)
            onHealthChanged.Invoke();
    }
    
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        if (onHealthChanged != null)
            onHealthChanged.Invoke();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10f);
        }
    }
}