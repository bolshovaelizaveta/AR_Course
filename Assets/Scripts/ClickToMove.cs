using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Camera mainCam;

    [Header("Настройки движения")]
    public float keyboardSpeed = 5f;
    public LayerMask groundLayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        HandleKeyboard();
        HandleMouse();
        UpdateAnimations();
    }

    void HandleKeyboard()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            Vector3 moveDir = new Vector3(h, 0, v);
            agent.Move(moveDir * keyboardSpeed * Time.deltaTime);
            if (moveDir != Vector3.zero) 
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.15f);
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    public void MoveToARPoint(Vector3 worldPos)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(worldPos);
        }
    }

    void UpdateAnimations()
    {
        if (anim != null)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
            anim.SetBool("IsRunning", isMoving);
        }
    }
}