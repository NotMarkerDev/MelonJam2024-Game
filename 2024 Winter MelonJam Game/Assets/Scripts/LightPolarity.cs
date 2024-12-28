using UnityEngine;

public class LightPolarity : MonoBehaviour
{
    [SerializeField] GameObject text;

    [SerializeField] LayerMask lightBlocks;
    [SerializeField] LayerMask player;
    [SerializeField] float radius = 5f;
    [SerializeField] float polarity;

    [Header("Torch")]
    [SerializeField] Torch torch;
    [SerializeField] Transform cam;
    [SerializeField] private float rayLength = 5f;

    private BondMaintain bondMaintain;
    private bool lightBlockInRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (polarity > 2 && !torch.hasLit && lightBlockInRange && bondMaintain != null && bondMaintain.isBonded)
        {
            text.SetActive(true);

            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(torch.LitTorch());
            }
        }

        else
        {
            text.SetActive(false);
        }

        if (bondMaintain == null)
        {
            Debug.Log("obj is null");
        }
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, radius, player))
        {
            // checks for light blocks
            Collider[] attractionColliders = Physics.OverlapSphere(transform.position, radius, lightBlocks);
            lightBlockInRange = Physics.Raycast(cam.transform.position, cam.transform.forward, rayLength, lightBlocks);

            polarity = attractionColliders.Length;

            Debug.Log(polarity);
        }
        else
        {
            polarity = 0;
        }
    }

    public void SetBondMaintain(BondMaintain bond)
    {
        bondMaintain = bond;
    }
}
