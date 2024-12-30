using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class LightPolarity : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject tutorial;

    [SerializeField] LayerMask lightBlocks;
    [SerializeField] float radius = 5f;
    [SerializeField] float polarity;

    [Header("Torch")]
    [SerializeField] Torch torch;
    [SerializeField] Transform cam;
    [SerializeField] private float rayLength = 5f;

    [Header("Light Beams")]
    [SerializeField] private FireLightBeams fireLightBeams;
    [SerializeField] private int ammoRechargeAmount = 1;
    [SerializeField] private float rechargeCooldown = 0.25f;

    private BondMaintain bondMaintain;
    private bool lightBlockInRange;
    private float lastRechargeTime;

    private bool torchLit = false;

    private bool isTextVisible = false;

    private bool lightBonded = false;

    void Start()
    {
        text.SetActive(false);
        lastRechargeTime = -rechargeCooldown;
    }

    void Update()
    {
        if (polarity >= 3 && lightBlockInRange)
        {
            // Debug.Log("Inside light polarity isBonded == " + bondMaintain.isBonded);
            // Debug.Log("Inside light polarity point 1 is " + bondMaintain.pointOne.transform.position);
            // Debug.Log("Inside light polarity point 2 is " + bondMaintain.pointTwo.transform.position);

            if (lightBonded)
            {
                Debug.Log("Conditions met for lighting the torch.");
                if (Time.time >= lastRechargeTime + rechargeCooldown)
                {
                    fireLightBeams.IncreaseAmmo(ammoRechargeAmount);
                    lastRechargeTime = Time.time;
                }

                if (!torch.hasLit)
                {
                    if (!isTextVisible)
                    {
                        text.SetActive(true);
                        isTextVisible = true;
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        StartCoroutine(torch.LitTorch());

                        if (!torchLit)
                        {
                            tutorial.GetComponent<TextMeshPro>().text = "You can also press F to fire a harmless light beam, which can bounce off mirrors.";
                            torchLit = true;
                        }

                    }
                }
            }
        }
        else
        {
            if (isTextVisible)
            {
                text.SetActive(false);
                isTextVisible = false;
            }
        }

        if (bondMaintain == null)
        {
            Debug.Log("BondMaintain object is null");
        }
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, radius))
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

    public void SetBonded(bool isBonded)
    {
        lightBonded = isBonded;
    }

    /*public bool GetBonded()
    {
        return lightBonded;
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
