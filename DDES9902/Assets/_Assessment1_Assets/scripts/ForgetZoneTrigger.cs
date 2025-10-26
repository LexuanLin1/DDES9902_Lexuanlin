using UnityEngine;
using TMPro;

public class ForgetZoneTrigger : MonoBehaviour
{
    [Header("Reference to Dog")]
    public GameObject currentDog;

    [Header("UI Text")]
    public GameObject displayTextObject;
    public TextMeshPro displayText;

    [TextArea(2, 5)]
    public string forgetMessage = "You chose to let go of this memory.";
    public float fadeDuration = 2f;

    [Header("Audio")]
    public AudioSource forget; 

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            
            if (forget != null && !forget.isPlaying)
            {
                forget.Play();
            }

            
            if (displayText != null)
            {
                displayText.text = forgetMessage;
                displayTextObject.SetActive(true);
            }

           
            if (currentDog != null)
            {
                StartCoroutine(FadeAndDestroyDog());
            }
        }
    }

    private System.Collections.IEnumerator FadeAndDestroyDog()
    {
        Renderer dogRenderer = currentDog.GetComponentInChildren<Renderer>();
        if (dogRenderer != null)
        {
            Material mat = dogRenderer.material;
            Color startColor = mat.color;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
                mat.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
        }

        Destroy(currentDog);
    }
}
