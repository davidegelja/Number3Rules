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
            {
                FlipCharacterRight(false);
            }
            else
            {
                FlipCharacterRight(true);
            }
        }
        else
        {
            GunController gunController = GetComponentInChildren<GunController>();
            if (gunController == null)
            {
                return;
            }
            if (moveInput < 0)
            {
                FlipCharacterRight(false);
                gunController.AdjustGunAngle(180f);
            }
            else if (moveInput > 0)
            {
                FlipCharacterRight(true);
                gunController.AdjustGunAngle(0f);
            }
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

    private void FlipCharacterRight(bool flipRight)
    {
        spriteRenderer.flipX = flipRight;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetCrosshair(Transform newCrosshair)
    {
        crosshair = newCrosshair.gameObject;
    }
}
