using UnityEngine;

public class TorchLightArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DarknessTimer dt = other.GetComponent<DarknessTimer>();
            if (dt != null)
            {
                dt.UpdateLightCount(+1);
            }
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
        }
    }
}