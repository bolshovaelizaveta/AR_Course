using UnityEngine;
using UnityEngine.AI; // Библиотека с AI

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent; 
    private Camera cam;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main; 
    }

    void Update()
    {
        // Если нажали ЛЕВУЮ кнопку мыши
        if (Input.GetMouseButtonDown(0)) 
        {
            // Пускаем луч из камеры в точку клика
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Если луч попал во что-то (в пол)
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}