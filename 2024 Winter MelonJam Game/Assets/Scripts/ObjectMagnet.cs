using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectMagnet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float radius;
    [SerializeField] LayerMask positive;
    [SerializeField] LayerMask negative;

    [Header("Attraction")]
    [SerializeField] float attractForceMagnitude = 10f;

    [Header("Repulsion")]
    [SerializeField] float repelForceMagnitude = 10f;

    private bool canAttract;
    private bool isPositive;

    private RaycastHit attractHit;
    private RaycastHit repelHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isPositive = gameObject.layer == LayerMask.NameToLayer("Positive");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // attraction
        LayerMask attractionLayer = isPositive ? negative : positive;
        Collider[] attractionColliders = Physics.OverlapSphere(transform.position, radius, attractionLayer);

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

        // repulsion
        LayerMask repulsionLayer = isPositive ? positive : negative;
        Collider[] repulsionColliders = Physics.OverlapSphere(transform.position, radius, repulsionLayer);

        if (repulsionColliders.Length > 0)
        {
            foreach (Collider collider in repulsionColliders)
            {
                Rigidbody targetRb = collider.GetComponent<Rigidbody>();
                if (targetRb != null)
                {
                    RepelObject(targetRb);
                }
            }
        }
    }

    private void AttractObject(Rigidbody targetRb)
    {
        Vector3 direction = (transform.position - targetRb.position).normalized;
        targetRb.AddForce(direction * attractForceMagnitude * 10, ForceMode.Force);

        Debug.Log($"Attracted {targetRb.name}!");
    }

    private void RepelObject(Rigidbody targetRb)
    {
        Vector3 direction = (targetRb.position - transform.position).normalized;
        targetRb.AddForce(direction * repelForceMagnitude * 10, ForceMode.Force);

        Debug.Log($"Repelled {targetRb.name}!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
