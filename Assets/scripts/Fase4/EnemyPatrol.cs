using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 direction;
    public Animator anim;

    public float orbitSpeed = 50f; // Velocidade de rotaÃ§Ã£o ao redor do jogador
    public float orbitDistance = 1.5f; // DistÃ¢ncia do inimigo ao jogador
    private Transform player;
    private bool isOrbiting = false;

    void Start()
    {
        // Define uma direÃ§Ã£o inicial aleatÃ³ria para a patrulha
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        if (isOrbiting && player != null)
        {
            // Quando em Ã³rbita, chama a funÃ§Ã£o de Ã³rbita
            OrbitAroundPlayer();
        }
        else
        {
            // Quando patrulhando, move o inimigo na direÃ§Ã£o definida e atualiza a animaÃ§Ã£o
            Patrol();
        }
    }

    private void Patrol()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        anim.SetFloat("Horizontal", direction.x);
    }

    private void OrbitAroundPlayer()
    {
        // Calcula o vetor de direÃ§Ã£o entre o inimigo e o jogador
        Vector3 directionToPlayer = (transform.position - player.position).normalized;
        // Define a posiÃ§Ã£o de Ã³rbita com a distÃ¢ncia constante
        Vector3 orbitPosition = player.position + directionToPlayer * orbitDistance;

        // Move o inimigo para a posiÃ§Ã£o de Ã³rbita
        transform.position = Vector3.MoveTowards(transform.position, orbitPosition, Time.deltaTime * speed);

        // Rotaciona ao redor do jogador sem mudar o eixo Z do prÃ³prio inimigo
        Vector3 offset = transform.position - player.position;
        offset = Quaternion.Euler(0, 0, orbitSpeed * Time.deltaTime) * offset;
        transform.position = player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Inverte a direÃ§Ã£o ao colidir com uma borda durante a patrulha
        if (!isOrbiting)
        {
            if (collision.CompareTag("BordaEsquerda") || collision.CompareTag("BordaDireita"))
            {
                direction.x = -direction.x; // Inverte apenas o eixo X
            }
            else if (collision.CompareTag("BordaSuperior") || collision.CompareTag("BordaInferior"))
            {
                direction.y = -direction.y; // Inverte apenas o eixo Y
            }
        }

        // Inicia a Ã³rbita ao colidir com o jogador
        if (collision.CompareTag("Player") && !isOrbiting)
        {
            player = collision.transform;
            isOrbiting = true; // Ativa a Ã³rbita e desativa a patrulha
            anim.SetFloat("Horizontal", 1f);
            speed = 3f;

            // Incrementa a contagem de fantasmas no controller do jogador
            Player_Movement playerController = player.GetComponent<Player_Movement>();
            if (playerController != null)
            {
                playerController.AddGhost();
            }
        }
    }
}
