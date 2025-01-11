using UnityEngine;
using TMPro; // или using UnityEngine.UI; если используете обычный UI Text

public class DarknessTimerUI : MonoBehaviour
{
    public DarknessTimer darknessTimer;
    public TMP_Text timerText;  

    [Header("UI Settings")]
    public bool showTimerOnlyWhenDark = true; 
    void Update()
    {
        if (darknessTimer == null) return;
        bool inDark = !darknessTimer.IsInLight();
        
        if (showTimerOnlyWhenDark)
        {
            timerText.gameObject.SetActive(inDark);
        }
        
        if (inDark)
        {
            float currentTime = darknessTimer.GetDarknessTimer();
            float gracePeriod = darknessTimer.gracePeriod;
            float timeLeft = Mathf.Clamp(gracePeriod - currentTime, 0, gracePeriod);

            timerText.text = $"Darkness: {timeLeft:0.0}s";
        }
    }
    public void ForceUpdateUI()
    {
        if (darknessTimer != null)
        {
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        float currentTime = darknessTimer.GetDarknessTimer();
        float gracePeriod = darknessTimer.gracePeriod;

        float timeLeft = Mathf.Clamp(gracePeriod - currentTime, 0, gracePeriod);
        timerText.text = $"Darkness: {timeLeft:0.0}s";
    }
}