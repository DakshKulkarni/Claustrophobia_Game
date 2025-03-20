using UnityEngine;
using UnityEngine.AI;

public class SCP_Trigger : MonoBehaviour
{
    public Transform player; 
    private NavMeshAgent agent;
    private Animator animator;
    private bool isProvoked = false;
    public float chaseSpeed = 3.5f;  // Just for navigation, animation controls visuals

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = chaseSpeed;
        agent.enabled = false;  // SCP-096 is disabled until triggered

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isProvoked && other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("SCP-096 has been provoked!");
            isProvoked = true;
            agent.enabled = true;
            animator.SetTrigger("TriggerSprint");  // Trigger the Sprint animation
        }
    }

    void Update()
    {
        if (isProvoked && player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
