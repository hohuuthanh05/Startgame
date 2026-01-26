using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator m_animator;
    private bool m_isDead = false;

    void Start() {
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        if (m_isDead) return;

        currentHealth -= damage;

        if (gameObject.CompareTag("Player")) {
            m_animator.SetTrigger("Hurt");
        } else {
            m_animator.SetTrigger("hit_1"); 
        }

        if (currentHealth <= 0) Die();
    }

    void Die() {
        if (m_isDead) return;
        m_isDead = true;

        if (gameObject.CompareTag("Player")) {
            m_animator.SetTrigger("Death");
        } else {
            m_animator.SetTrigger("death");
        }

        MonoBehaviour control = GetComponent<HeroKnight>() as MonoBehaviour ?? GetComponent<BossAI>();
        if (control != null) control.enabled = false;
    }
}