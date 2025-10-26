using UnityEngine;
using TMPro;
using System.Collections;

public class MemoryPetTrigger_Sad : MonoBehaviour
{
    [Header("Dog Settings")]
    public GameObject dogPrefab;           
    public Transform spawnPoint;           
    public float stayDuration = 6f;        

    [Header("Audio")]
    public AudioClip barkSound;            
    private AudioSource audioSource;

    [Header("Memory Text")]
    [TextArea(2, 5)]
    public string memoryText = "On a stormy night with thunder and lightning, your dog died.";
    public TextMeshProUGUI displayText;        
    public float textDuration = 5f;        

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
            StartCoroutine(ShowSadMemory());
        }
    }

    private IEnumerator ShowSadMemory()
    {
       
        if (dogPrefab && spawnPoint)
        {
         
            if (currentDog != null)
            {
                Destroy(currentDog);
            }

            currentDog = Instantiate(dogPrefab, spawnPoint.position, Quaternion.identity);
        }

      
        if (barkSound)
        {
            audioSource.PlayOneShot(barkSound);
        }

       
        if (displayText)
        {
            displayText.text = memoryText;
        }

    
        yield return new WaitForSeconds(1f);

        if (currentDog != null)
        {
            StartCoroutine(DogFallOver(currentDog.transform));
        }

     
        yield return new WaitForSeconds(textDuration);

        if (displayText)
        {
            displayText.text = "";
        }

      
        yield return new WaitForSeconds(stayDuration);

      
        if (currentDog != null)
        {
            Destroy(currentDog);
            currentDog = null;
        }

        hasTriggered = false;
    }



    private IEnumerator DogFallOver(Transform dog)
    {
        Quaternion startRot = dog.rotation;
        
        Quaternion endRot = Quaternion.Euler(dog.rotation.eulerAngles.x, dog.rotation.eulerAngles.y, dog.rotation.eulerAngles.z + 90f);

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / 2f; 
            dog.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}
