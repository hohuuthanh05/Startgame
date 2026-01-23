using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;

    private int jumpCount;
    private float moveInput;
    private GameManager gameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {

    }

    void Update()
    {   
        if (gameManager.IsGameOver()) return;
        moveInput = Input.GetAxis("Horizontal");
        CheckGround();
        HandleJumpInput();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver()) return;
        HandleMovement();
        BetterJumpPhysics();
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded && !wasGrounded && rb.linearVelocity.y <= 0)
        {
            jumpCount = 0;
        }
        wasGrounded = isGrounded;
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {   
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void BetterJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }
}
