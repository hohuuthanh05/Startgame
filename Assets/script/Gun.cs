using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;

    [SerializeField] private int maxAmmo = 24;
    private int currentAmmo;
    private float nextShot;

    private Transform player;

    void Start()
    {
        currentAmmo = maxAmmo;
        player = transform.root; // lấy Player
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;

            // xác định hướng bắn theo hướng nhân vật
            float direction = player.localScale.x;

            Quaternion rot;
            if (direction > 0)
                rot = Quaternion.Euler(0, 0, 0);      // bắn sang phải
            else
                rot = Quaternion.Euler(0, 0, 180);    // bắn sang trái

            Instantiate(bulletPrefab, firePos.position, rot);
            currentAmmo--;
        }
    }

    void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}
