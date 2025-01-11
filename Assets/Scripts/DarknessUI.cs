using UnityEngine;
using UnityEngine.UI;

public class DarknessUI : MonoBehaviour
{
    public DarknessTimer darknessTimer; 
    public Slider darknessSlider;
    [SerializeField] private float lerpSpeed = 5f;

    private float targetValue;

    private void Start()
    {
        if (darknessTimer != null)
        {
            darknessSlider.maxValue = darknessTimer.gracePeriod;
            darknessSlider.value = darknessTimer.gracePeriod;
            targetValue = darknessSlider.value;
            
            darknessTimer.onDarknessChanged.AddListener(OnDarknessChanged);
        }
    }

    private void Update()
    {
        if (Mathf.Abs(darknessSlider.value - targetValue) > 0.001f)
        {
            darknessSlider.value = Mathf.Lerp(darknessSlider.value, targetValue, lerpSpeed * Time.deltaTime);
        }
        else
        {
            darknessSlider.value = targetValue;
        }
    }

    private void OnDarknessChanged()
    {
        if (darknessTimer == null) return;

        float currentTime = darknessTimer.GetDarknessTimer();
        float gracePeriod = darknessTimer.gracePeriod;
        
        targetValue = Mathf.Clamp(gracePeriod - currentTime, 0, gracePeriod);
    }
}