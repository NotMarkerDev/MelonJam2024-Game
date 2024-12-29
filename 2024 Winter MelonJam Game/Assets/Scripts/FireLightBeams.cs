using UnityEngine;
using TMPro;

public class FireLightBeams : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("UI References")]
    [SerializeField] private TMP_Text ammoText;

    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentAmmo > 0)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        currentAmmo--;
        UpdateAmmoDisplay();

        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }

    public void IncreaseAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        UpdateAmmoDisplay();
    }

    private void UpdateAmmoDisplay()
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
