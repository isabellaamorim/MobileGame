using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOut : MonoBehaviour
{
    public InteractionManager interactionManager;
    public AudioClip winning; // Som de abertura da porta
    private AudioSource AudioSource;

    private void Start()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactionManager.conditionMet)
        {
            AudioSource.PlayOneShot(winning);
            SceneManager.LoadScene("WinScene");
        }
    }

}