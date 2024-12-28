using UnityEngine;

public class TorchSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] float swayAmount = 0.1f;

    [Header("Bob Settings")]
    [SerializeField] float walkBobAmount = 0.05f;
    [SerializeField] float bobSpeed = 10f;

    [SerializeField] Transform playerCamera;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] float tiltAmount = 2f;

    private Vector3 initialPosition;
    private Vector3 currentRotation;
    private bool isJumping;
    private bool isLanding;
    private bool isWalking;
    private float jumpTime;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        HandleSway();
        HandleBob();
        HandleTilt();
    }

    private void HandleSway()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float swayX = mouseX * swayAmount;
        float swayY = mouseY * swayAmount;

        currentRotation = new Vector3(-swayY, swayX, 0);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void HandleBob()
    {
        float speed = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
        isJumping = Input.GetButton("Jump");
        isLanding = !isJumping && Mathf.Abs(speed) < 0.1f;

        isWalking = speed > 0.1f && !isJumping;

        float targetBobAmount = isWalking ? walkBobAmount : 0f;

        if (isJumping)
        {
            targetBobAmount = 0f;
            jumpTime = Time.time;
        }

        float bobAmount = Mathf.Lerp(walkBobAmount, targetBobAmount, Time.deltaTime * lerpSpeed);

        if (isLanding)
        {
            float landingTime = Mathf.Clamp01((Time.time - jumpTime) / 0.3f);
            bobAmount = Mathf.Lerp(bobAmount, 0f, landingTime);
        }

        float bobbingY = Mathf.Sin(Time.time * bobSpeed) * bobAmount;

        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + bobbingY, initialPosition.z);
    }

    private void HandleTilt()
    {
        if (isWalking && !isJumping)
        {
            float tiltInput = Input.GetAxis("Horizontal");
            float tilt = tiltInput * tiltAmount;

            Vector3 targetRotation = new Vector3(0, 0, -tilt);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * lerpSpeed);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * lerpSpeed);
        }
    }
}
