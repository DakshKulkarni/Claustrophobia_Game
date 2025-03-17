using UnityEngine;
using UnityEngine.AI;

public class SCP_ChasePlayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Automatically find the player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // SCP follows the player

            // Get movement speed and update Blend Tree parameter
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);

            // Sprint when close, walk when far
            if (Vector3.Distance(transform.position, player.position) < 10f)
            {
                agent.speed = 6f; // Sprint
            }
            else
            {
                agent.speed = 3.5f; // Walk
            }
        }
    }
}
