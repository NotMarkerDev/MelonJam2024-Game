using UnityEngine;

public class IntroduceJeff : MonoBehaviour
{
    public Cue cue;
    public GameObject jeff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cue.isActivated) 
        {
            transform.position = transform.position - new Vector3(transform.position.x, -1.17f, transform.position.z);
            jeff.SetActive(true);
        }
    }


}
