using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private GameObject crosshair;
    public float groundCheckRadius = 0.2f;

    public float moveSpeed = 2f;
    public float jumpForce = 6f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move left/right
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isWalking", moveInput != 0);

        if (transform.position.x > crosshair.transform.position.x)
        {
            this.FlipPlayerRight(false);
        }
        else
        {
            this.FlipPlayerRight(true);
        }

        if (moveInput > 0.01f)
        {
            FlipPlayerRight(true);
        }
        else if (moveInput < -0.01f)
        {
            FlipPlayerRight(false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    public void FlipPlayerRight(bool flipRight)
    {
        spriteRenderer.flipX = flipRight; 
        // TODO weapon flip
    }
}
