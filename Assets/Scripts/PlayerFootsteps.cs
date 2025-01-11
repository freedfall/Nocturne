using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.4f;

    private float stepTimer;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool isRunning = anim.GetBool("IsRunning");
        if (isRunning)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                int index = Random.Range(0, footstepClips.Length);
                audioSource.PlayOneShot(footstepClips[index]);

                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}