using UnityEngine;
using TMPro;
using System.Collections;

public class MemoryPetTrigger_WithText : MonoBehaviour
{
    [Header("Dog Settings")]
    public GameObject dogPrefab;          
    public Transform spawnPoint;           
    public float stayDuration = 5f;        

    [Header("Audio")]
    public AudioClip barkSound;
    private AudioSource audioSource;

    [Header("Memory Text")]
    [TextArea(2, 5)]
    public string memoryText = "A joyful memory of playing with my dog in the park.";
    public TextMeshProUGUI displayText;        
    public float textDuration = 4f;        

    private GameObject currentDog;
    private bool hasTriggered = false;

    void Start()
    {
      
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.8f;
        audioSource.spatialBlend = 0.5f;

       
        if (displayText != null)
        {
            displayText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(ShowMemoryEvent());
        }
    }

    private IEnumerator ShowMemoryEvent()
    {
       
        if (dogPrefab && spawnPoint)
        {
            currentDog = Instantiate(dogPrefab, spawnPoint.position, Quaternion.identity);
        }

       
        if (barkSound)
        {
            audioSource.PlayOneShot(barkSound);
        }

     
        if (displayText != null)
        {
            displayText.text = memoryText;
        }

        yield return new WaitForSeconds(textDuration);

  
        if (displayText != null)
        {
            displayText.text = "";
        }

        yield return new WaitForSeconds(stayDuration);
        if (currentDog)
        {
            Destroy(currentDog);
        }

        hasTriggered = false;
    }
}
