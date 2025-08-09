using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius = 0.2f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float jumpForce = 6f;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isWalking", moveInput != 0);
        if (crosshair != null)
        {
            if (transform.position.x > crosshair.transform.position.x)
                FlipPlayerRight(false);
            else
                FlipPlayerRight(true);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void SetMoveInput(float input)
    {
        moveInput = input;
    }

    public void Jump()
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void FlipPlayerRight(bool flipRight)
    {
        spriteRenderer.flipX = flipRight;
    }
}
