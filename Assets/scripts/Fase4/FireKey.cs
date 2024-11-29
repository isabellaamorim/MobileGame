using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKey : InteractableBase
{

    private SpriteRenderer spriteRenderer;
    public AudioClip getFire; // Som de abertura da porta
    private AudioSource audioSource;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    protected override void OnInteract()
    {
        spriteRenderer.enabled = false;
        audioSource.PlayOneShot(getFire);
        for (int i = 0; i < enemySpawner.enemyCount; i++)
        {
            enemySpawner.nSpawnEnemy();
        }
    }
}