using UnityEngine;
using System.Collections; 

public class FishCollectible : MonoBehaviour
{
    public AudioClip coinSound; 
    private AudioSource audioSource;
    private MeshRenderer meshRenderer; 
    private Collider col;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; 
        audioSource.clip = coinSound;
        
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Рыбка съедена!");

            StartCoroutine(CollectSequence());
        }
    }

    IEnumerator CollectSequence()
    {
        if (coinSound != null)
        {
            audioSource.Play();
        }

        if (meshRenderer != null) meshRenderer.enabled = false;
        if (col != null) col.enabled = false;

        if (coinSound != null)
        {
            yield return new WaitForSeconds(coinSound.length);
        }
        else
        {
            yield return new WaitForSeconds(0.5f); 
        }
        
        Destroy(gameObject);
    }
}