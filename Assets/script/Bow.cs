using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float shotDelay = 0.8f;

    [SerializeField] private int maxArrow = 15;
    private int currentArrow;
    private float nextShot;

    private Transform player;

    void Start()
    {
        currentArrow = maxArrow;
        player = transform.root; // lấy Player
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentArrow > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;

            // Lấy hướng bắn theo hướng nhân vật
            float direction = player.localScale.x;

            Quaternion rot;
            if (direction > 0)
                rot = Quaternion.Euler(0, 0, 0);      // bắn sang phải
            else
                rot = Quaternion.Euler(0, 0, 180);    // bắn sang trái

            Instantiate(arrowPrefab, firePos.position, rot);
            currentArrow--;
        }
    }

    void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentArrow < maxArrow)
        {
            currentArrow = maxArrow;
        }
    }
}
