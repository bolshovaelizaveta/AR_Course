using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float force = 100f; // Сила удара 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Если нажали клавишу E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Пинаем дверь
            rb.AddRelativeTorque(0, force, 0, ForceMode.Impulse);
        }
    }
}
