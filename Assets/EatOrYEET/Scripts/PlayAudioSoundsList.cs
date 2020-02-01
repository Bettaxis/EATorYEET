using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioSoundsList : MonoBehaviour
{
    public AudioClip[] audioSounds;

    [SerializeField]
    private bool isRandomSound;

    [SerializeField]
    AudioSource audioSource;

    private int soundPlayIndex;

    private System.Random rng;

    void Start()
    {
        soundPlayIndex = 0;
        rng = new System.Random();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if(isRandomSound)
        {
            soundPlayIndex = rng.Next(audioSounds.Length);
        }
        else
        {
            soundPlayIndex = (soundPlayIndex + 1) % audioSounds.Length;
        }

        audioSource.PlayOneShot(audioSounds[soundPlayIndex]);
    }
}
