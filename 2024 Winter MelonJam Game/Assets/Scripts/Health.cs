using UnityEngine;

public class Health : MonoBehaviour
{
    float playerHealth = 100;
    [SerializeField] GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0) 
        {
            gameManager.GameOver();
        }
    }

    public void Damage(float damage)
    {
        playerHealth -= damage;

        Debug.Log("Took " + damage + " damage");
    }
}
