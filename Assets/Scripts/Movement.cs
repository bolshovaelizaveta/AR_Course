using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    // Скорость мыши
    public float mouseSensitivity = 150f; 

    private Renderer objRenderer;
    private Rigidbody rb;

    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
        
        // Скрыть курсор мыши
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void FixedUpdate() 
    {
        // Движение (W / S) 
        float moveInput = Input.GetAxis("Vertical"); 
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Вращение (Мышь)
        float mouseX = Input.GetAxis("Mouse X");
        
        // Умножаем сдвиг мыши на чувствительность
        float rotationAmount = mouseX * mouseSensitivity * Time.fixedDeltaTime;
        
        // Вращаем туловище
        Quaternion turnRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void Update()
    {
        // Смена цвета 
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColor();
        }
        
        // Освободить курсор по нажатию Escape
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //    Cursor.lockState = CursorLockMode.None;
        // }
    }

    void ChangeColor()
    {
        if (objRenderer != null)
        {
            objRenderer.material.color = Random.ColorHSV();
        }
    }
}