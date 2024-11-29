using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaChave : InteractableBase
{
    public Sprite newSprite; // Sprite that will be displayed when activated
    private SpriteRenderer spriteRenderer;
    public AudioClip getWater; // Sound to play during interaction
    private AudioSource audioSource;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    protected override void OnInteract()
    {
        // Disable the Animator if it exists
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Set the new sprite if provided
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }

        // Play the interaction sound
        if (getWater != null)
        {
            audioSource.PlayOneShot(getWater);
        }

        // Spawn enemies if required
        for (int i = 0; i < enemySpawner.enemyCount; i++)
        {
            enemySpawner.nSpawnEnemy();
        }
    }
}
