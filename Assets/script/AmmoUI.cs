using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public static AmmoUI instance;

    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowAmmo(int current, int max)
    {
        ammoText.gameObject.SetActive(true);
        ammoText.text = current + " / " + max;
    }

    public void HideAmmo()
    {
        ammoText.gameObject.SetActive(false);
    }
}
