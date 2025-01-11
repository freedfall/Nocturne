using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;
    public Animator animator;
    public GameObject player;
    public bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(moveX, moveY);
        movement = inputVector.normalized;

        isMoving = (movement.sqrMagnitude > 0);
        animator.SetBool("IsRunning", isMoving);

        if (movement.x < 0)
            sr.flipX = true;
        else if (movement.x > 0)
            sr.flipX = false;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}