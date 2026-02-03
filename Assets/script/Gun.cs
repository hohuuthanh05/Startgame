using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;

    [SerializeField] private int maxAmmo = 24;
    [SerializeField] private TextMeshProUGUI ammoText;
    private int currentAmmo;
    private float nextShot;

    private Transform player;

    void Start()
    {
        currentAmmo = maxAmmo;
        player = transform.root; // lấy Player
        UpdateAmmoText();
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
            UpdateAmmoText();
        }
    }

    void Reload()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateAmmoText();
        }
    }
    private void UpdateAmmoText()
{
    if (ammoText != null)
    {
        if (currentAmmo > 0)
        {
            ammoText.text = currentAmmo.ToString();
        }
        else
        {
            ammoText.text = "Empty";
        }
    }
}

}
