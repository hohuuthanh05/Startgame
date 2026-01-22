using UnityEngine;

public class Bow : MonoBehaviour
{
    private float rotateOffset = 180f;
     [SerializeField] private Transform firePos;
    [SerializeField] private GameObject arrowPrefabs;
   [SerializeField] private float shotDelay = 0.8f;
    private float nextShot;
    

    [SerializeField] private int maxarrow = 15;
    public int currentArrow;
 
    void Start()
    {
        currentArrow = maxarrow;
    }


    void Update()
    {
        Shoot();
        Reload();
        RotateBow();
    }

    void RotateBow()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 displacement = transform.position - mouseWorldPos;

        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        // Xoay cung theo chuột
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        // Lật sprite khi quay sang trái / phải
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    void Shoot()
    {
       if (Input.GetMouseButtonDown(0) && currentArrow > 0 && Time.time > nextShot)
{
    nextShot = Time.time + shotDelay;
    Instantiate(arrowPrefabs, firePos.position, firePos.rotation);
    currentArrow--;
}
 
    }
    void Reload()
{
    if (Input.GetMouseButtonDown(1) && currentArrow < maxarrow)
    {
        currentArrow = maxarrow;
    }
}
}
