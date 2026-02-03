using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        }

        // Auto-create groundCheck if not assigned
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheckObj.transform.SetParent(transform);
            groundCheckObj.transform.localPosition = new Vector3(0, -0.8f, 0);
            groundCheck = groundCheckObj.transform;
            Debug.LogWarning("GroundCheck was not assigned. Created automatically at position (0, -0.8, 0)");
        }
    }

    void Update()
    {
        if (rb == null) return;

        CheckGround();
        HandleMovement();
        HandleJump();
        HandleDash();
        UpdateAnimation();
    }

    // ===================== GROUND CHECK =====================
    void CheckGround()
    {
        // Kiểm tra an toàn trước khi dùng groundCheck
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck vẫn null! Vui lòng kiểm tra lại Inspector.");
            return;
        }

        // Kiểm tra groundLayer có được set chưa
        if (groundLayer == 0)
        {
            Debug.LogWarning("Ground Layer chưa được set! Vào Inspector chọn layer Ground.");
        }

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Debug log khi trạng thái thay đổi
        if (wasGrounded != isGrounded)
        {
            Debug.Log($"Ground Check: {(isGrounded ? "CHẠM ĐẤT" : "KHÔNG CHẠM ĐẤT")} - Position: {groundCheck.position}");
        }

        if (isGrounded)
            canDoubleJump = true;
    }

    // Vẽ Ground Check radius trong Scene view để debug
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
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
}
