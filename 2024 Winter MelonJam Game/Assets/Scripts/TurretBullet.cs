using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        Debug.Log(collision);
    }
}
