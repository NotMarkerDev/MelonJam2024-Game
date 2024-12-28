using UnityEngine;

public class LightPolarity : MonoBehaviour
{
    [SerializeField] LayerMask lightBlocks;
    [SerializeField] float radius = 5f;
    [SerializeField] float polarity;

    [Header("Torch")]
    [SerializeField] Torch torch;
    [SerializeField] Transform cam;
    [SerializeField] private float rayLength = 5f;

    private bool lightBlockInRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && polarity >= 2 && !torch.hasLit && lightBlockInRange)
        {
            StartCoroutine(torch.LitTorch());
        }
    }

    private void FixedUpdate()
    {
        // checks for light blocks
        Collider[] attractionColliders = Physics.OverlapSphere(transform.position, radius, lightBlocks);
        lightBlockInRange = Physics.Raycast(cam.transform.position, cam.transform.forward, rayLength, lightBlocks);

        polarity = attractionColliders.Length;

        Debug.Log(polarity);
    }
}
