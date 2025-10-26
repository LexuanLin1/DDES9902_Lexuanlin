using UnityEngine;
using System.Collections;

public class ForgetZoneFade : MonoBehaviour
{
    [Header("Zone to Fade Out")]
    public GameObject sadZone;  

    [Header("Fade Settings")]
    public float fadeDuration = 2f; 

    private bool hasFaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasFaded) return;

        if (other.CompareTag("Player") && sadZone != null)
        {
            StartCoroutine(FadeOutZone());
            hasFaded = true;
        }
    }

    IEnumerator FadeOutZone()
    {
        
        Renderer[] renderers = sadZone.GetComponentsInChildren<Renderer>();

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            foreach (Renderer r in renderers)
            {
                if (r.material.HasProperty("_Color"))
                {
                    Color c = r.material.color;
                    c.a = alpha;
                    r.material.color = c;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

       
        sadZone.SetActive(false);
    }
}

