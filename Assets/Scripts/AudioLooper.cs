using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLooper : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public float loopStart = 13f; // Start point of the loop in seconds
    public float loopEnd = 56.2f; // End point of the loop in seconds
    [Range(0f, 1f)] public float volume = 0.5f; // Volume control (0 to 1)

    void Update()
    {
        // Check if the current time of the audio is greater than the loop end
        if (audioSource.time >= loopEnd)
        {
            // If reached the end of the loop, set the time back to the loop start
            audioSource.time = loopStart;
        }

        // Continuously adjust the volume based on the value set in the Inspector
        audioSource.volume = volume;
    }

    void Start()
    {
        // Ensure the AudioSource is set to loop
        audioSource.loop = true;
        // Set the volume based on the Inspector's value
        audioSource.volume = volume;
        // Play the audio
        audioSource.Play();
    }
}
