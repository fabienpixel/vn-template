using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    public void Play(string soundName)
    {
        // Load and play the audio clip based on the soundName from the "Assets -> Sounds" folder
        AudioClip clipToPlay = Resources.Load<AudioClip>("Sounds/" + soundName);

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
            Debug.Log("Play Sound Function triggered for sound: " + soundName);
        }
        else
        {
            Debug.LogWarning("Audio clip not found for sound: " + soundName);
        }
    }
}
