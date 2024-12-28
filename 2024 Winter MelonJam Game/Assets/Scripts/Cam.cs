using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform head;

    [Header("Sensitivity")]
    [SerializeField] private float xSens;
    [SerializeField] private float ySens;
    [SerializeField] private float camSmoothing;

    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = head.transform.position;

        // get the mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;

        // calculate vertical rotation
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void LateUpdate()
    {
        Quaternion targetCamRotation = Quaternion.Euler(xRotation, yRotation, 0f); ;
        Quaternion targetOrientationRotation = Quaternion.Euler(0f, yRotation, 0f);

        cam.rotation = Quaternion.Lerp(cam.rotation, targetCamRotation, Time.deltaTime * camSmoothing);
        orientation.rotation = Quaternion.Lerp(orientation.rotation, targetOrientationRotation, Time.deltaTime * camSmoothing);
    }
}
