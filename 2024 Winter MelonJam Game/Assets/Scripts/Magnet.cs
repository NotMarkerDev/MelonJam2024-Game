using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private Rigidbody rb;
    private RaycastHit hit;
    private RaycastHit attractHit;

    [SerializeField] private bool isPositive = true;
    [SerializeField] private Transform positiveSign;
    [SerializeField] private Transform negativeSign;

    [Header("Repel & Attract")]
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float attractionForce = 50f;
    [SerializeField] private float repelForce = 5f;
    [SerializeField] private float maxVelocity = 5f;

    [Header("References")]
    [SerializeField] private LayerMask posMask;
    [SerializeField] private LayerMask negMask;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform grabPoint;

    private bool canAttract;
    private bool canRepel;
    private bool isHoldingObject;

    private void Start()
    {
        DisplaySign();
    }

    void Update()
    {
        // Switch between positive and negative
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPositive = !isPositive;
            DisplaySign();
        }

        // If mouse button is pressed and raycast hits an object
        if (Input.GetMouseButton(0))
        {
            if (canAttract && !isHoldingObject)
            {
                AttractObject();
            }
        }

        if (Input.GetMouseButtonDown(0) && !isHoldingObject)
        {
            if (canRepel)
            {
                RepelObject();
            }
        }

        // Release object if holding
        if (isHoldingObject && Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.E))
        {
            ReleaseObject();
        }
    }

    private void FixedUpdate()
    {
        // Perform raycast to check for attractable objects
        if (isPositive)
        {
            canAttract = Physics.Raycast(cam.position, cam.forward, out attractHit, maxDistance, negMask);
        }
        else
        {
            canAttract = Physics.Raycast(cam.position, cam.forward, out attractHit, maxDistance, posMask);
        }

        // Raycast for repelling objects
        if (isPositive)
        {
            canRepel = Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, posMask);
        }
        else
        {
            canRepel = Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, negMask);
        }

        // If holding an object, smoothly move it to the grab point
        if (isHoldingObject && rb != null)
        {
            MoveObjectToGrabPoint();
        }
    }

    private void AttractObject()
    {
        Debug.Log("Attracted object!");
        rb = attractHit.transform.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // disable gravity and set holding state
            rb.useGravity = false;
            rb.isKinematic = false;
            isHoldingObject = true;
        }
        else
        {
            Debug.LogWarning("No rigidbody found");
        }
    }

    private void RepelObject()
    {
        Debug.Log("Repelled");
        rb = hit.transform.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 repelDirection = (hit.transform.position - cam.position).normalized;
            rb.AddForce(repelDirection * repelForce * 5f, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("No rigidbody found");
        }
    }

    private void MoveObjectToGrabPoint()
    {
        Vector3 direction = grabPoint.position - rb.position;
        float distance = direction.magnitude;

        if (distance < 0.1f)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        direction.Normalize();

        // Apply smoother force
        float forceMagnitude = Mathf.Lerp(0, attractionForce, 1 - (distance / maxDistance));
        rb.AddForce(direction * forceMagnitude, ForceMode.Force);

        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * maxVelocity, Time.deltaTime * 5f);
    }


    private void ReleaseObject()
    {
        Debug.Log("Released object!");
        if (rb != null)
        {
            // re enable gravity
            rb.useGravity = true;
            rb = null;
        }

        isHoldingObject = false;
    }

    private void DisplaySign()
    {
        // displays the sign in the ui
        positiveSign.gameObject.SetActive(isPositive);
        negativeSign.gameObject.SetActive(!isPositive);
    }
}
