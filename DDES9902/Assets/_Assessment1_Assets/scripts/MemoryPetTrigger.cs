using UnityEngine;

public class MemoryPetTrigger : MonoBehaviour
{
    [Header("Dog Settings")]
    public GameObject dogPrefab;       
    public Transform spawnPoint;      
    public float stayDuration = 5f;    

    [Header("Audio")]
    public AudioClip barkSound;
    private AudioSource audioSource;

    private GameObject currentDog;
    private bool hasSpawned = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.5f;
        audioSource.volume = 0.8f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            hasSpawned = true;

           
            if (dogPrefab && spawnPoint)
            {
                currentDog = Instantiate(dogPrefab, spawnPoint.position, Quaternion.identity);
            }

          
            if (barkSound)
            {
                audioSource.PlayOneShot(barkSound);
            }

            
            StartCoroutine(RemoveDogAfterDelay());
        }
    }

    private System.Collections.IEnumerator RemoveDogAfterDelay()
    {
        yield return new WaitForSeconds(stayDuration);

        if (currentDog)
        {
            Destroy(currentDog);
        }

        hasSpawned = false; 
    }
}

