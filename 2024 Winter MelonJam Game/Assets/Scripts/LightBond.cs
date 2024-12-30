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
    [SerializeField] private GameObject lightParent;

    private GameObject[] lights;
    private LightPolarity[] lightPolarity;
    private int bondedCount = 0;

    void Start()
    {
        text.SetActive(false);
        lights = GameObject.FindGameObjectsWithTag("Positive");
        lightPolarity = lightParent.GetComponentsInChildren<LightPolarity>();

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

        BondMaintain bondMaintain = lineRenderer.GetComponent<BondMaintain>();

        // lineRenderer.gameObject.GetComponent<BondMaintain>().pointOne = go1;
        // lineRenderer.gameObject.GetComponent<BondMaintain>().pointTwo = go2;
        // lineRenderer.gameObject.GetComponent<BondMaintain>().radius = radius;
        // lineRenderer.gameObject.GetComponent<BondMaintain>().playerPos = playerPos;

        bondMaintain.pointOne = go1;
        bondMaintain.pointTwo = go2;
        bondMaintain.radius = radius;
        bondMaintain.playerPos = playerPos;

        /*
        foreach (var polarity in lightPolarity)
        {
            polarity.SetBondMaintain(bondMaintain);
        }
        */

        foreach (var polarity in lightPolarity)
        {
            bool setAction = false;
            Debug.Log("Set polarity : position =" + polarity.gameObject.transform.position + " go1=" + go1.transform.position + " go2=" + go2.transform.position + " IsBonded == " + bondMaintain.isBonded);
            if (polarity.gameObject.transform.position == go1.transform.position || polarity.gameObject.transform.position == go2.transform.position)
            {
                Debug.Log("Set polarity: to pointOne or pointTwo. IsBonded == " + bondMaintain.isBonded);

                if (bondMaintain.isBonded)
                {
                    polarity.SetBonded(true);
                    setAction = true;

                    Debug.Log("SetBonded is true.");
                }
            }

            if (!setAction)
            {
                polarity.SetBonded(false);

                Debug.Log("SetBonded is false.");
            }
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
            if (textPrefab != null)
            {
                var textMeshPro = textPrefab.GetComponent<TextMeshPro>();
                if (textMeshPro != null)
                {
                    textMeshPro.text = "When 3 light blocks are bonded, you can right click on the blocks to light the torch.";
                }
            }
        }
    }
}
