using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator anim; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        // Анимация
        if (anim != null)
        {
            float speed = agent.velocity.magnitude;
                if (speed > 0.1f) 
                {
                    anim.SetBool("IsRunning", true);
                }
                else if (speed < 0.05f) 
                {
                     anim.SetBool("IsRunning", false);
                }
        }
    }
}