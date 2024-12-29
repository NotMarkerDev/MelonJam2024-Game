using UnityEngine;
using UnityEngine.AI;

public class LizardController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerPos;
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerPos.position);
    }
}
