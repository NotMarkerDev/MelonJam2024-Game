using UnityEngine;

public class PolarityButton : MonoBehaviour
{
    public Transform door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        door.position = Vector3.up * 100;
    }
}
