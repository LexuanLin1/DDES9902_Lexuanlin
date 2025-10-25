using UnityEngine;

public class MemorySoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip happyClip;
    public AudioClip sadClip;
    public AudioClip calmClip;

    [Header("Audio Source")]
    public AudioSource audioSource;

    public void PlayHappy()
    {
        PlaySound(happyClip);
    }

    public void PlaySad()
    {
        PlaySound(sadClip);
    }

    public void PlayCalm()
    {
        PlaySound(calmClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        audioSource.clip = clip;
        audioSource.volume = 0.8f;
        audioSource.pitch = 1f;
        audioSource.Play();
    }
}

