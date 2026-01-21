using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon GameObjects")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject sword;

    [Header("Weapon Scripts (optional)")]
    [SerializeField] private MonoBehaviour gunScript;   // Gun.cs
    [SerializeField] private MonoBehaviour swordScript; // Sword.cs (nếu có)

    void Start()
    {
        EquipGun(); // mặc định cầm súng khi vào game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipGun();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipSword();
        }
    }

    void EquipGun()
    {
        // bật / tắt GameObject
        gun.SetActive(true);
        sword.SetActive(false);

        // bật / tắt script (RẤT QUAN TRỌNG)
        if (gunScript != null) gunScript.enabled = true;
        if (swordScript != null) swordScript.enabled = false;
    }

    void EquipSword()
    {
        gun.SetActive(false);
        sword.SetActive(true);

        if (gunScript != null) gunScript.enabled = false;
        if (swordScript != null) swordScript.enabled = true;
    }
}
