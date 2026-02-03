using System;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;


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
    }
     void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
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
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded)
            canDoubleJump = true;
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
    // ===================== DAMAGE =====================
    public virtual void TakeDamage(float damage)
{
    currentHp -= damage;
    currentHp = Mathf.Max(currentHp, 0);

    UpdateHpBar();

    if (currentHp <= 0)
    {
        Die();
    }
}


    private void Die()
{
    Destroy(gameObject);
}
    private void UpdateHpBar()
{
    if (hpBar != null)
    {
        hpBar.fillAmount = currentHp / maxHp;
    }

}
public void Heal(float healValue)
{
    if (currentHp < maxHp)
    {
        currentHp += healValue;
        currentHp = Mathf.Min(currentHp, maxHp);

    UpdateHpBar();
    }
}

}