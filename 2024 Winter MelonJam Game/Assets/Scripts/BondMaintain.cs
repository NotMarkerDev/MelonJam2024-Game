using UnityEngine;

public class BondMaintain : MonoBehaviour
{
    public GameObject pointOne;
    public GameObject pointTwo;
    private LineRenderer lineRenderer;

    public float radius = 5;
    public Transform playerPos;

    private bool bondRequested = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && (Vector3.Distance(playerPos.position, pointOne.transform.position) < 3 || Vector3.Distance(playerPos.position, pointTwo.transform.position) < 3) && bondRequested)
        {
            bondRequested = false;
        }
        else if (Input.GetKeyDown("q") && (Vector3.Distance(playerPos.position, pointOne.transform.position) < 3 || Vector3.Distance(playerPos.position, pointTwo.transform.position) < 3)) 
        {
            bondRequested = true;
        }

        if(Vector3.Distance(pointOne.transform.position, pointTwo.transform.position) <= radius && bondRequested)
        {
            lineRenderer.SetPosition(0, pointOne.transform.position);
            lineRenderer.SetPosition(1, pointTwo.transform.position);
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.down * 100);
            lineRenderer.SetPosition(1, Vector3.down * 100);
        }
    }
}
