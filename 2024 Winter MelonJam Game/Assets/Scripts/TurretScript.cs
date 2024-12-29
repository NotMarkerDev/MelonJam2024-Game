using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = -firePoint.right * bulletSpeed;
        }
    }
}
