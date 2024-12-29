using UnityEngine;
using TMPro;

public class FireLightBeams : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("References")]
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private AudioClip clip;
    [SerializeField] private TMP_Text newText;

    private AudioSource source;
    private int currentAmmo;
    private bool bulletFired = false;

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

        source = bulletInstance.GetComponent<AudioSource>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        if (source != null)
        {
            source.PlayOneShot(clip);
        }

        if (!bulletFired)
        {
            bulletFired = true;
            newText.text = "The green object on the floor is a pressure plate. Try to use it to open the door.";
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
