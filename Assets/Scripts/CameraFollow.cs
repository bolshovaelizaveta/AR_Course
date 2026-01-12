using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; // Плавность слежения
    
    // Сдвиг камеры относительно игрока
    public Vector3 offset; 
    
    // Мы не используем автоматический расчет offset в Start, 
    // чтобы ты могла сама настроить его руками в Инспекторе, если захочешь.
    // Но для удобства сделаем так: если в инспекторе нули, вычислим сами.
    
    void Start()
    {
        // Текущая позиция камеры минус позиция пингвина
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return; // Если пингвин исчез/удален, выходим

        // Позиция пингвина + сдвиг
        Vector3 desiredPosition = target.position + offset;
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;

        // Заставляем камеру всегда смотреть на пингвина (поворот головы)
        transform.LookAt(target);
    }
}