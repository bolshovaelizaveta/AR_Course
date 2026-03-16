using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    [Header("AR Settings")]
    public LayerMask groundLayer; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
                {
                    Debug.Log("Клик по острову. Точка: " + hit.point);
                    
                    if (agent != null)
                    {
                        if (!agent.isOnNavMesh)
                        {
                            NavMeshHit navHit;
                            if (NavMesh.SamplePosition(transform.position, out navHit, 5.0f, NavMesh.AllAreas))
                            {
                                agent.Warp(navHit.position);
                            }
                        }

                        agent.SetDestination(hit.point);
                    }
                }
            }
        }

        if (anim != null && agent != null && agent.isActiveAndEnabled)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            anim.SetBool("IsRunning", isMoving);
        }
    }
}