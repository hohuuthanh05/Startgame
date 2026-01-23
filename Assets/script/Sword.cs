using UnityEngine;

public class Sword : MonoBehaviour
{
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
}
