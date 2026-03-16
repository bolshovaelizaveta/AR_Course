using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float damageAmount = 20f; 
    public float damageCooldown = 1.0f; 
    private float lastDamageTime; 

    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player != null) agent.SetDestination(player.position);

        if (anim != null)
        {
            anim.SetBool("IsRunning", agent.velocity.magnitude > 0.1f);
        }
    }

    // Когда враг касается игрока
    void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > lastDamageTime + damageCooldown)
            {
                Health playerHealth = other.GetComponent<Health>();
                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    lastDamageTime = Time.time; 
                    Debug.Log("Враг укусил игрока!");
                }
            }
        }
    }
}