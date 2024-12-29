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

    [SerializeField] private FireLightBeams fireLightBeams;
    [SerializeField] private int ammoRechargeAmount = 1;
    [SerializeField] private float rechargeCooldown = 0.25f;

    private BondMaintain bondMaintain;
    private bool lightBlockInRange;
    private float lastRechargeTime;

    void Start()
    {
        text.SetActive(false);
        lastRechargeTime = -rechargeCooldown;
    }

    void Update()
    {
        if (polarity >= 3 && lightBlockInRange && bondMaintain != null && bondMaintain.isBonded)
        {
            if (Time.time >= lastRechargeTime + rechargeCooldown)
            {
                fireLightBeams.IncreaseAmmo(ammoRechargeAmount);
                lastRechargeTime = Time.time;
            }

            if (!torch.hasLit)
            {
                text.SetActive(true);

                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(torch.LitTorch());
                }
            }
        }
        else
        {
            text.SetActive(false);
        }

        if (bondMaintain == null)
        {
            Debug.Log("BondMaintain object is null");
        }
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, radius, player))
        {
            // Check for light blocks
            Collider[] attractionColliders = Physics.OverlapSphere(transform.position, radius, lightBlocks);
            lightBlockInRange = Physics.Raycast(cam.transform.position, cam.transform.forward, rayLength, lightBlocks);

            polarity = attractionColliders.Length;
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
