using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBond : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject lineRendPrefab;

    [SerializeField] private float radius;

    [SerializeField] private Transform playerPos;

    private int currentState = 0;

    private GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }

    private void DrawBond(GameObject go1, GameObject go2)
    {
        lineRenderer = Instantiate(lineRendPrefab, lineRendPrefab.transform.position, lineRendPrefab.transform.rotation).GetComponent<LineRenderer>();
        lineRenderer.gameObject.GetComponent<BondMaintain>().pointOne = go1;
        lineRenderer.gameObject.GetComponent<BondMaintain>().pointTwo = go2;
        lineRenderer.gameObject.GetComponent<BondMaintain>().radius = radius;
        lineRenderer.gameObject.GetComponent<BondMaintain>().playerPos = playerPos;
        //lineRenderer.SetPosition(0, transform.position);
        //lineRenderer.SetPosition(1, collider.GetComponent<Transform>().position);
    }
}