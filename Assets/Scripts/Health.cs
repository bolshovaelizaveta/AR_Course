using UnityEngine;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Максимальное здоровье
    private float currentHealth;   // Текущее здоровье

    public Slider healthBar; 

    void Start()
    {
        currentHealth = maxHealth; // При старте здоровье полное

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        // Тестовый уронН: По нажатию на пробел
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f); // Наносим 10 урона
        }
    }

    // Функция получения урона
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Отнимаем здоровье
        
        // Mathf.Clamp не дает здоровью упасть ниже нуля или подняться выше максимума
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); 

        // Обновляем полоску на экране
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Если здоровье кончилось
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Игрок погиб!");
    }
}