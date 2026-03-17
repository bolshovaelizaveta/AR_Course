using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Camera mainCam;

    [Header("Настройки движения")]
    public float keyboardSpeed = 7f;
    public float rotationSpeed = 15f;
    public LayerMask groundLayer; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        mainCam = Camera.main;
        if (mainCam == null) mainCam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        HandleKeyboard();
        HandleMouse();
        UpdateAnimations();
    }

    // Тот самый метод для AutoContourTracker, который я забыл вернуть
    public void MoveToARPoint(Vector3 worldPos)
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(worldPos);
        }
    }

    void HandleKeyboard()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            Vector3 forward = mainCam.transform.forward;
            Vector3 right = mainCam.transform.right;
            forward.y = 0f; right.y = 0f;
            forward.Normalize(); right.Normalize();

            Vector3 moveDir = forward * v + right * h;
            agent.Move(moveDir * keyboardSpeed * Time.deltaTime);
            
            if (agent.hasPath) agent.ResetPath();

            if (moveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotationSpeed * Time.deltaTime);
            }
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            // Дистанция 1000f, чтобы луч точно доставал до пола при Scale 10
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void UpdateAnimations()
    {
        if (anim != null)
        {
            float speed = agent.velocity.magnitude;
            bool isInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
            anim.SetBool("IsRunning", speed > 0.1f || isInput);
        }
    }
}