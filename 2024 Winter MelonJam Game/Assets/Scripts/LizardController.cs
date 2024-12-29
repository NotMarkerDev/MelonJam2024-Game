using UnityEngine;
using UnityEngine.AI;

public class LizardController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform playerPos;
    [SerializeField] Health playerHealth;

    Animator animator;

    [SerializeField] Torch torch;

    public bool shouldPathfind = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldPathfind)
        {
            agent.SetDestination(playerPos.position);
            animator.SetBool("isMoving", true);
        }

        if (Vector3.Distance(agent.gameObject.transform.position, playerPos.position) < 5)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("attackPlayer", true);
            playerHealth.Damage(100);
        }
        else
        {
            animator.SetBool("attackPlayer", false);
        }
        
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_Attack") && !animator.GetBool("isMoving"))
        {
            animator.SetBool("attackPlayer", false);
            shouldPathfind = true;
        }

        if(torch.hasLit)
        {
            Ray light = new Ray(torch.gameObject.transform.position, new Vector3(transform.position.x - torch.gameObject.transform.position.x, transform.position.y - torch.gameObject.transform.position.y, transform.position.z - torch.gameObject.transform.position.z).normalized);
            RaycastHit hit;
            if(Physics.Raycast(light, out hit, 100) && hit.transform.gameObject.name == "Jeff")
            {
                animator.SetBool("isMoving", false);
                shouldPathfind = false;
                agent.Stop();
            }
        }
        else
        {
            shouldPathfind = true;
        }
    }
}
