using UnityEngine;
using UnityEngine.Events;

public class DarknessTimer : MonoBehaviour
{
    [Header("Darkness Settings")]
    public float gracePeriod = 5f;       
    public float damagePerTick = 10f;    
    public float tickInterval = 1f;      
    
    public UnityEvent onDarknessChanged;
    
    private float darknessTimer = 0f;
    private float tickTimer = 0f;
    
    
    private int lightZoneCount = 0;
    private bool isInLight = false;
    private bool lightIsOff = false;

    public PlayerHealth playerHealth;

    [Header("Player Light")]
    public UnityEngine.Rendering.Universal.Light2D playerLight;

    void Update()
    {
        if (!isInLight)
        {
            darknessTimer += Time.deltaTime;

            if (darknessTimer >= gracePeriod)
            {
                if (!lightIsOff)
                {
                    playerLight.enabled = false;
                    lightIsOff = true;
                }
                
                tickTimer += Time.deltaTime;
                if (tickTimer >= tickInterval)
                {
                    playerHealth.TakeDamage(damagePerTick);
                    tickTimer = 0f;
                }
            }
            
            if(onDarknessChanged != null) 
                onDarknessChanged.Invoke();
        }
        else
        {
            darknessTimer = 0f;
            tickTimer = 0f;
            
            if (lightIsOff)
            {
                playerLight.enabled = true;
                lightIsOff = false;
            }
            
            if(onDarknessChanged != null) 
                onDarknessChanged.Invoke();
        }
    }
    
    public void UpdateLightCount(int delta)
    {
        lightZoneCount += delta;
        if (lightZoneCount < 0) 
            lightZoneCount = 0;
    
        isInLight = (lightZoneCount > 0);
        if (isInLight)
        {
            darknessTimer = 0f;
            tickTimer = 0f;
            
            if (lightIsOff)
            {
                playerLight.enabled = true;
                lightIsOff = false;
            }
        }
    }
    
    public void SetInLight(bool value)
    {
        isInLight = value;
        if (value)
        {
            darknessTimer = 0f;
            tickTimer = 0f;
            if (lightIsOff)
            {
                playerLight.enabled = true;
                lightIsOff = false;
            }
        }
    }
    
    public bool IsInLight()
    {
        return isInLight; 
    }

    public float GetDarknessTimer()
    {
        return darknessTimer;
    }

    public void ResetDarknessTimer()
    {
        darknessTimer = 0f;
        tickTimer = 0f;
        
        if (lightIsOff)
        {
            playerLight.enabled = true;
            lightIsOff = false;
        }

        Debug.Log("Darkness timer has been reset.");
    }

}
