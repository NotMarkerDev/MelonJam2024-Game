using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Hit Effect")]
    [SerializeField] private GameObject hitParticlePrefab;

    [Header("Bounce Settings")]
    [SerializeField] private LayerMask mirrorLayer;
    [SerializeField] private int maxBounces = 3;

    private int bounceCount = 0;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & mirrorLayer) != 0)
        {
            if (bounceCount < maxBounces)
            {
                Transform mirrorTransform = collision.transform;
                rb.linearVelocity = mirrorTransform.forward * rb.linearVelocity.magnitude;
                bounceCount++;
                return;
            }
        }

        if (hitParticlePrefab != null)
        {
            Instantiate(hitParticlePrefab, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }

        Destroy(gameObject);
    }
}
