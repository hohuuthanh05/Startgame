using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float enemyMoveSpeed = 1.5f;

    [Header("Chase Settings")]
    [SerializeField] protected float chaseRadius = 5f;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;

    protected PlayerController player;
    protected bool isChasing;

    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        currentHp = maxHp;
        UpdateHpBar();
    }

    protected virtual void Update()
    {
        CheckDistance();
        HandleMovement();
    }

    // ===================== CHECK DISTANCE =====================
    protected void CheckDistance()
    {
        if (player == null)
        {
            isChasing = false;
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.transform.position
        );

        isChasing = distance <= chaseRadius;
    }

    // ===================== MOVE =====================
    protected void HandleMovement()
    {
        if (!isChasing || player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            enemyMoveSpeed * Time.deltaTime
        );

        FlipEnemy();
    }

    // ===================== FLIP =====================
    protected void FlipEnemy()
    {
        float direction = player.transform.position.x - transform.position.x;

        if (direction != 0)
        {
            transform.localScale = new Vector3(
                direction < 0 ? -1 : 1,
                1,
                1
            );
        }
    }

    // ===================== DEBUG =====================
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
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


    protected virtual void Die()
{
    Destroy(gameObject);
}
public void SetChasing(bool value)
{
    isChasing = value;
}
protected void UpdateHpBar()
{
    if (hpBar != null)
    {
        hpBar.fillAmount = currentHp / maxHp;
    }


}
}
