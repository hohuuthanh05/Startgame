using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject gun;
    public GameObject bow;

    void Start()
    {
        EquipGun();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EquipGun();

        if (Input.GetKeyDown(KeyCode.E))
            EquipBow();
    }

    void EquipGun()
    {
        gun.SetActive(true);
        bow.SetActive(false);
        Debug.Log("Gun ON");
    }

    void EquipBow()
    {
        gun.SetActive(false);
        bow.SetActive(true);
        Debug.Log("Bow ON");
    }
}
