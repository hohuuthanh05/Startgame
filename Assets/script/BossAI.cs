using UnityEngine;
using System;

public class BossAI : MonoBehaviour {
    private Animator animator;
    private float walkStartTime = 0;
    private Vector3 velocity = Vector3.zero;

    public Transform player;
    public float attackDistance = 1.5f;
    public float moveSpeed = 0.1f;
    private float nextAttackTime = 0;

    void Start () {
        animator = GetComponent<Animator>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update() {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackDistance) {
            MoveToPlayer();
        } else {
            if (Time.time >= nextAttackTime) {
                Attack();
                nextAttackTime = Time.time + 1.5f;
            }
            StopBoss();
        }
    }

    void MoveToPlayer() {
        if (walkStartTime == 0) walkStartTime = Time.time;
        animator.SetTrigger(Time.time - walkStartTime > 2.0f ? "run" : "walk");

        int direction = (player.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(direction, 1, 1);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + new Vector3(direction * 0.1f, 0, 0), ref velocity, moveSpeed);
    }

    void StopBoss() {
        walkStartTime = 0;
        animator.SetTrigger("idle_1");
    }

    void Attack() {
        animator.SetTrigger("skill_1");
        player.GetComponent<Health>()?.TakeDamage(10);
    }
}