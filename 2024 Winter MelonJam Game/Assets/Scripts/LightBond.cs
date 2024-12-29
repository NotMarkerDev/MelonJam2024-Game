using TMPro;
using UnityEngine;

public class LightBond : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineRendPrefab;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject textPrefab;

    [SerializeField] private float radius;

    [SerializeField] private Transform playerPos;

    [SerializeField] LightPolarity[] lightPolarity;

    private GameObject[] lights;
    private int bondedCount = 0;

    void Start()
    {
        text.SetActive(false);

        lights = GameObject.FindGameObjectsWithTag("Positive");

        if (lights.Length > 1)
        {
            int a = 0;
            foreach (GameObject go in lights)
            {
                for (int i = a; i < lights.Length; i++)
                {
                    if (go != lights[i])
                    {
                        DrawBond(go, lights[i]);
                    }
                }
                a++;
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

    public void CheckBondedBlocks()
    {
        bondedCount++;

        if (bondedCount == 3)
        {
            textPrefab.GetComponent<TextMeshPro>().text = "When 3 light blocks are bonded, you can right click on the blocks to light the torch.";
        }
    }
}
