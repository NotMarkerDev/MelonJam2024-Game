using UnityEngine;

public class BondMaintain : MonoBehaviour
{
    public GameObject pointOne;
    public GameObject pointTwo;
    private LineRenderer lineRenderer;

    public float radius = 5;
    public Transform playerPos;

    private bool bondRequested = false;
    private LightBond lightBond;

    private AudioSource source;
    [SerializeField] private AudioClip clip;

    public bool isBonded;

    private bool isTextVisible = false;

    private void Awake()
    {
        lightBond = GameObject.Find("Bond Manager").GetComponent<LightBond>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && (Vector3.Distance(playerPos.position, pointOne.transform.position) < 3 || Vector3.Distance(playerPos.position, pointTwo.transform.position) < 3) && bondRequested)
        {
            bondRequested = false;

            isBonded = false;
        }
        else if (Vector3.Distance(playerPos.position, pointOne.transform.position) < 3 || Vector3.Distance(playerPos.position, pointTwo.transform.position) < 3)
        {
            if (lightBond != null && Vector3.Distance(pointOne.transform.position, pointTwo.transform.position) <= radius)
            {
                if (!isTextVisible)
                {
                    lightBond.DisplayText();
                    isTextVisible = true;
                }
            }
            if (Input.GetKeyDown("q"))
            {
                bondRequested = true;

                isBonded = true;

                source.PlayOneShot(clip);
            }
        }
        else
        {
            if (isTextVisible)
            {
                lightBond.HideText();
                isTextVisible = false;
            }
        }

        if (Vector3.Distance(pointOne.transform.position, pointTwo.transform.position) <= radius && bondRequested)
        {
            lineRenderer.SetPosition(0, pointOne.transform.position);
            lineRenderer.SetPosition(1, pointTwo.transform.position);

            // isBonded = true;

            lightBond.CheckBondedBlocks();

            Debug.Log("IsBonded == " + isBonded);
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.down * 100);
            lineRenderer.SetPosition(1, Vector3.down * 100);

            // isBonded = false;
            Debug.Log("Point one position =" + pointOne.transform.position);
            Debug.Log("Point two position =" + pointTwo.transform.position);
            Debug.Log("Point's distance = " + Vector3.Distance(pointOne.transform.position, pointTwo.transform.position));
            Debug.Log("IsBonded set to " + isBonded);
        }
    }
}
