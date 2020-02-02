using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioSoundsList : MonoBehaviour
{
    public AudioClip[] audioSounds;

    [SerializeField]
    private bool isRandomSound = false;

    AudioSource audioSource;
    private int soundPlayIndex;
    private Random rng;

    public float minPitch = 0.1f;
    public float maxPitch = 1.5f;
    public float minVolume = .5f;
    public float maxVolume = 1.2f;

    void Start()
    {
        soundPlayIndex = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if(isRandomSound)
        {
            soundPlayIndex = (int)Random.Range(-1, audioSounds.Length);
        }
        else
        {
            soundPlayIndex = (soundPlayIndex + 1) % audioSounds.Length;
        }

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.PlayOneShot(audioSounds[soundPlayIndex]);
    }
}
