using UnityEngine;

public class Locomotive : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // Для постоянной силы 
    {
        // Пробел - поезд едет
        if (Input.GetKey(KeyCode.Space))
        {
            // Толкаем локомотив вперед
            rb.AddForce(transform.forward * speed);
        }
    }
}
