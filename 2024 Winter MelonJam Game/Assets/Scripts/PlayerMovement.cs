using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] Transform orientation;

    [Header("Drag")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float airMultiplier;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] LayerMask groundMask;
    public bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlDrag();

        // input
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        direction = orientation.forward * yMovement + orientation.right * xMovement;

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        rb.AddForce(Vector3.down * 10, ForceMode.Force);

        // ground check
        Debug.DrawRay(groundCheck.position, Vector3.down * (groundDistance + 0.1f), Color.green);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }

        else
        {
            rb.linearDamping = airDrag;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce * 5, ForceMode.Impulse);
    }

    private void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(direction.normalized * speed * 10, ForceMode.Acceleration);
        }

        else
        {
            rb.AddForce(direction.normalized * speed * 10 * airMultiplier, ForceMode.Acceleration);
        }
    }
}
