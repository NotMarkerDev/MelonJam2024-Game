using UnityEngine;

public class LightBond : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineRendPrefab;
    [SerializeField] private GameObject text;

    [SerializeField] private float radius;

    [SerializeField] private Transform playerPos;

    [SerializeField] LightPolarity[] lightPolarity;

    private GameObject[] lights;

    void Start()
    {
        text.SetActive(false);

        lights = GameObject.FindGameObjectsWithTag("Positive");

        if (lights.Length > 1)
        {
            foreach (GameObject go in lights)
            {
                foreach (GameObject go2 in lights)
                {
                    if (go != go2)
                    {
                        DrawBond(go, go2);
                    }
                }
            }
        }
    }

    private void DrawBond(GameObject go1, GameObject go2)
    {
        lineRenderer = Instantiate(lineRendPrefab, lineRendPrefab.transform.position, lineRendPrefab.transform.rotation).GetComponent<LineRenderer>();
        lineRenderer.gameObject.GetComponent<BondMaintain>().pointOne = go1;
        lineRenderer.gameObject.GetComponent<BondMaintain>().pointTwo = go2;
        lineRenderer.gameObject.GetComponent<BondMaintain>().radius = radius;
        lineRenderer.gameObject.GetComponent<BondMaintain>().playerPos = playerPos;

        BondMaintain bondMaintain = lineRenderer.GetComponent<BondMaintain>();

        foreach (var polarity in lightPolarity)
        {
            polarity.SetBondMaintain(bondMaintain);
        }
    }

    public void DisplayText()
    {
        text.SetActive(true);
    }

    public void HideText()
    {
        text.SetActive(false);
    }
}
