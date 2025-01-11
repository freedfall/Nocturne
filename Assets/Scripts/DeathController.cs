using UnityEngine;
using TMPro;

public class DeathController : MonoBehaviour
{
    public Canvas EndgameCanvas;
    public GameObject Player;
    public TextMeshProUGUI endgameText;
    public TextMeshProUGUI deathText;

    public void OnDeath()
    {
        EndgameCanvas.gameObject.SetActive(true);
        deathText.enabled = true;
        PlayerController controller = Player.GetComponent<PlayerController>();
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        Animator animator = Player.GetComponent<Animator>();
        PlayerFootsteps playerFootsteps = Player.GetComponent<PlayerFootsteps>();
        if(controller != null)
        {
            controller.enabled = false;
            controller.moveSpeed = 0;
            animator.SetBool("IsRunning", false);
            playerFootsteps.enabled = false;
            rb.linearVelocity = Vector2.zero;   
        }
    }

    public void OnEndGame()
    {
        EndgameCanvas.gameObject.SetActive(true);
        endgameText.enabled = true;
        PlayerController controller = Player.GetComponent<PlayerController>();
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        Animator animator = Player.GetComponent<Animator>();
        PlayerFootsteps playerFootsteps = Player.GetComponent<PlayerFootsteps>();
        if(controller != null)
        {
            controller.enabled = false;
            controller.moveSpeed = 0;
            animator.SetBool("IsRunning", false);
            playerFootsteps.enabled = false;
            rb.linearVelocity = Vector2.zero;   
        } 
    }
}
