using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

<<<<<<< HEAD
    [Header("Jump")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private bool canDoubleJump;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private bool isDashing;
    private float dashTimeLeft;
    private float nextDashTime;

    private Rigidbody2D rb;
    private Animator animator;
=======
    private int jumpCount;
    private float moveInput;
    private GameManager gameManager;
>>>>>>> parent of a550c64 (Revert "Add: Trap, GameOvreUI")

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        animator = GetComponent<Animator>();
=======
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
>>>>>>> parent of a550c64 (Revert "Add: Trap, GameOvreUI")

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        }
    }

    void Update()
<<<<<<< HEAD
    {
        if (rb == null) return;

=======
    {   
        if (gameManager.IsGameOver()) return;
        moveInput = Input.GetAxis("Horizontal");
>>>>>>> parent of a550c64 (Revert "Add: Trap, GameOvreUI")
        CheckGround();
        HandleMovement();
        HandleJump();
        HandleDash();
        UpdateAnimation();
    }

    // ===================== GROUND CHECK =====================
    void CheckGround()
    {
<<<<<<< HEAD
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded)
            canDoubleJump = true;
=======
        if (gameManager.IsGameOver()) return;
        HandleMovement();
        BetterJumpPhysics();
>>>>>>> parent of a550c64 (Revert "Add: Trap, GameOvreUI")
    }

    // ===================== MOVE =====================
    void HandleMovement()
    {
        if (isDashing || rb == null) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // ===================== JUMP + DOUBLE JUMP =====================
    void HandleJump()
    {
        if (rb == null) return;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    // ===================== DASH =====================
    void HandleDash()
    {
        if (rb == null) return;

        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextDashTime)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            nextDashTime = Time.time + dashCooldown;
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;

            float dashDir = transform.localScale.x;
            rb.linearVelocity = new Vector2(dashDir * dashSpeed, 0);

            if (dashTimeLeft <= 0)
                isDashing = false;
        }
    }

    // ===================== ANIMATION =====================
    void UpdateAnimation()
    {
        if (animator == null || rb == null) return;

        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }

    // ===================== DEBUG =====================
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
