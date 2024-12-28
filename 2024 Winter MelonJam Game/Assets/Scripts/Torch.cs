using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private float seconds;
    [SerializeField] Transform fire;

    [SerializeField] GameObject pointLight;

    public bool hasLit;

    private void Start()
    {
        hasLit = false;
    }

    public IEnumerator LitTorch()
    {
        if (hasLit) yield break;

        hasLit = true;

        fire.gameObject.SetActive(true);
        pointLight.gameObject.SetActive(true);
        Debug.Log("Flame is lit.");

        yield return new WaitForSeconds(seconds);


        KillFlame();
        Debug.Log("Flame is dead.");
    }

    private void KillFlame()
    {
        fire.gameObject.SetActive(false);
        pointLight.gameObject.SetActive(false);

        hasLit = false;
    }
}
