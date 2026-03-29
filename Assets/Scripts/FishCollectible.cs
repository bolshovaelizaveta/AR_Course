using UnityEngine;

public class FishCollectible : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Может съесть только Player
        if (other.CompareTag("Player"))
        {
            // В консоль вывод 
            Debug.Log("Рыбка съедена! Ням-ням.");

            Destroy(gameObject);
        }
    }
}