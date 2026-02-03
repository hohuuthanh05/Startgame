using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject bow;
    public GameObject sword;

    void Start()
    {
        EquipSword();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EquipGun();

        if (Input.GetKeyDown(KeyCode.E))
            EquipBow();
        if (Input.GetKeyDown(KeyCode.R))
            EquipSword();
    }
    void EquipSword()
    {
        gun.SetActive(false);
        sword.SetActive(true);
        bow.SetActive(false);
        Debug.Log("Sword ON");
    }

    void EquipGun()
    {
        gun.SetActive(true);
        sword.SetActive(false);
        bow.SetActive(false);
        Debug.Log("Gun ON");
    }

    void EquipBow()
    {
        gun.SetActive(false);
        sword.SetActive(false);
        bow.SetActive(true);
        Debug.Log("Bow ON");
    }
}
