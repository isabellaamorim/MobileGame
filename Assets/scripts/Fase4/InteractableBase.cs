using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public bool isActivated = false;
    public InteractionManager interactionManager;
    public GameObject UI;
    public EnemySpawner enemySpawner;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (UI != null && !isActivated)
            {
                UI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Saiu da Ã¡rea de interaÃ§Ã£o.");
            if (UI != null)
            {
                UI.SetActive(false);
            }
        }
    }

    public void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            OnInteract(); // Chama a lÃ³gica especÃ­fica do objeto
            interactionManager.CheckInteractions(); // Notifica o gerenciador
        }
    }

    // MÃ©todo para ser implementado nas subclasses, definindo a lÃ³gica especÃ­fica de cada objeto
    protected abstract void OnInteract();
}