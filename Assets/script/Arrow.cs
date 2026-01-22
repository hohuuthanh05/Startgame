using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float timeDestroy = 3f;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    void Update()
    {
        MoveArrow();
    }

    void MoveArrow()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
}
