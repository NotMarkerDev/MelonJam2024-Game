using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectMagnet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float radius;
    [SerializeField] LayerMask canSuck;

    [Header("Attraction")]
    [SerializeField] float attractForceMagnitude = 10f;
    [SerializeField] float nearDrag = 12f;
    [SerializeField] float farDrag = 1.0f;

    private bool canAttract;
    
    private RaycastHit attractHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // attraction
        Collider[] attractionColliders = Physics.OverlapSphere(transform.position, radius, canSuck);

        if (attractionColliders.Length > 0)
        {
            foreach (Collider collider in attractionColliders)
            {
                Rigidbody targetRb = collider.GetComponent<Rigidbody>();
                if (targetRb != null)
                {
                    AttractObject(targetRb);
                }
            }
        }
    }

    private void AttractObject(Rigidbody targetRb)
    {
        Vector3 direction = transform.position - targetRb.position;
        float distance = direction.magnitude;

        if (distance < 1.5f) 
        {
            targetRb.linearDamping = nearDrag;
        }
        else
        {
            targetRb.linearDamping = farDrag;


            targetRb.AddForce(direction.normalized * attractForceMagnitude * 10, ForceMode.Force);
        }

        Debug.Log($"Attracted {targetRb.name}!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
