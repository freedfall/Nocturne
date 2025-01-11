using UnityEngine;

public class TorchSwitchTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    public TorchLightController[] torchesToDisable;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisableSelectedTorches();
            audioSource.PlayOneShot(audioClip);
        }
    }

    void DisableSelectedTorches()
    {
        foreach (TorchLightController torch in torchesToDisable)
        {
            if (torch != null)
            {
                torch.TurnOff();
            }
        }
    }
}