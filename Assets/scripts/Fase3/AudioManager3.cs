using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager3 : MonoBehaviour
{
    [Header("------ Audio Sources -----")]
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioSource sfxSource;

    [Header("------ Audio Clips -----")]
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip key;

    private void Start() {
        audioSource1.clip = background1;
        audioSource1.Play();

        audioSource2.clip = background2;
        audioSource2.Play();
    }

    public void playSFX() {
        sfxSource.PlayOneShot(key);
    }
}
