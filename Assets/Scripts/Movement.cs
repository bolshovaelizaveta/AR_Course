using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Renderer objRenderer;
    private Rigidbody rb;

    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {
        // Движение
        float moveInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Вращение
        float rotateInput = Input.GetAxis("Horizontal");
        float rotationAmount = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void Update()
    {
        // Цвет
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (objRenderer != null)
        {
            objRenderer.material.color = Random.ColorHSV();
        }
    }
}