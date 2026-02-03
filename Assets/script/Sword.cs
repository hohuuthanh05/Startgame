using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Chuột trái
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }
     private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
}
