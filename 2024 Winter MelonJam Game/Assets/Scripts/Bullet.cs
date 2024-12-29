using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Hit Effect")]
    [SerializeField] private GameObject hitParticlePrefab;

    [Header("Bounce Settings")]
    [SerializeField] private LayerMask mirrorLayer;
    [SerializeField] private int maxBounces = 3;

    private int bounceCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & mirrorLayer) != 0)
        {
            if (bounceCount < maxBounces)
            {
                // Reflect the bullet's direction
                Vector3 incomingDirection = GetComponent<Rigidbody>().linearVelocity.normalized;
                Vector3 normal = collision.contacts[0].normal;
                Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);

                // Update bullet's velocity
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.linearVelocity = reflectedDirection * rb.linearVelocity.magnitude;

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
