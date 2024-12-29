using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] private float throwForce = 6f;

    [Header("References")]
    [SerializeField] private LayerMask posMask;
    [SerializeField] private LayerMask negMask;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private TextMeshPro tutorialText;

    private bool canAttract;
    private bool canRepel;
    private bool isHoldingObject;
    private bool hasPickedUpPositiveBlock = false;

    private void Start()
    {
        tutorialText.text = "These are light blocks. Press E to switch between selecting light blocks and selecting dark blocks. When you see a sun icon appear, press LMB to pick up the light blocks.";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPositive = !isPositive;
            DisplaySign();
            source.PlayOneShot(clip);
        }

        if (Input.GetMouseButtonDown(0))
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

        if (isHoldingObject && Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }

        if (isHoldingObject && Input.GetKeyDown(KeyCode.E))
        {
            ThrowMagnet();
        }
    }

    private void FixedUpdate()
    {
        if (isPositive)
        {
            canAttract = Physics.Raycast(cam.position, cam.forward, out attractHit, maxDistance, negMask);
        }
        else
        {
            canAttract = Physics.Raycast(cam.position, cam.forward, out attractHit, maxDistance, posMask);
        }

        if (isPositive)
        {
            canRepel = Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, posMask);
        }
        else
        {
            canRepel = Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, negMask);
        }

        if (isHoldingObject && rb != null)
        {
            MoveObjectToGrabPoint();
        }
    }

    private void AttractObject()
    {
        rb = attractHit.transform.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            isHoldingObject = true;

            if (!isPositive && !hasPickedUpPositiveBlock)
            {
                hasPickedUpPositiveBlock = true;
                tutorialText.text = "Pressing Q on a light block when other light blocks are in range will bond them, increasing polarity(energy).";
            }
        }
    }

    private void RepelObject()
    {
        rb = hit.transform.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 repelDirection = (hit.transform.position - cam.position).normalized;
            rb.AddForce(repelDirection * repelForce * 5f, ForceMode.Impulse);
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
        float forceMagnitude = Mathf.Lerp(0, attractionForce, 1 - (distance / maxDistance));
        rb.AddForce(direction * forceMagnitude, ForceMode.Force);
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * maxVelocity, Time.deltaTime * 5f);
    }

    private void ReleaseObject()
    {
        if (rb != null)
        {
            rb.useGravity = true;
            rb = null;
        }

        isHoldingObject = false;
    }

    private void ThrowMagnet()
    {
        if (rb != null)
        {
            Vector3 throwDirection = cam.transform.forward.normalized;
            rb.AddForce(throwDirection * throwForce * 10f, ForceMode.Impulse);
            rb.useGravity = true;
            rb = null;
        }

        isHoldingObject = false;
    }

    private void DisplaySign()
    {
        positiveSign.gameObject.SetActive(isPositive);
        negativeSign.gameObject.SetActive(!isPositive);
    }
}
