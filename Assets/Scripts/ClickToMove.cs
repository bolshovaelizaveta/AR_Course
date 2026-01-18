using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        // Логика клика
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        // Логика анимации
        if (anim != null)
        {
            // Скорость движения
            // Если скорость больше 0.1, значит мы движемся
            bool isMoving = agent.velocity.magnitude > 0.1f;
            anim.SetBool("IsRunning", isMoving);
        }
    }
}