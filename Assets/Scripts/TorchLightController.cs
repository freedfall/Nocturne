using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLightController : MonoBehaviour
{
    public Light2D torchLight; 
    public CircleCollider2D torchCollider;
    public SpriteRenderer torchRenderer;
    public Sprite torchSpriteOn;
    public Sprite torchSpriteOff;

    public void TurnOff()
    {
        if (torchLight != null)
        {
            torchLight.enabled = false;
        }
        torchCollider.enabled = false;
        torchRenderer.sprite = torchSpriteOff;
    }
    
    public void TurnOn()
    {
        if (torchLight != null)
        {
            torchLight.enabled = true;
        }
    }
}