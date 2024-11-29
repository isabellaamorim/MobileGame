using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // NecessÃ¡rio para carregar cenas

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D rb; // ReferÃªncia ao Rigidbody2D
    public float moveSpeed = 5f; // Velocidade de movimento do jogador
    public Animator anim; // ReferÃªncia ao Animator para animaÃ§Ãµes
    public int maxGhosts = 1; // NÃºmero mÃ¡ximo de fantasmas ao redor permitido
    private Vector2 movement; // Vetor de movimento
    private int ghostCount = 0; // Contador de fantasmas ao redor do jogador
    public GameObject cutsceneCanvas; // ReferÃªncia ao Canvas para cutscenes

    void Start()
    {
        // Salva o Ã­ndice da fase atual no PlayerPrefs
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        PlayerPrefs.Save();

        // Inicializa o jogo em pausa para exibir a cutscene
        Time.timeScale = 0f;
        cutsceneCanvas.SetActive(true); // Ativa o Canvas da cutscene
    }

    void Update()
    {
        // Atualiza as animaÃ§Ãµes do jogador com base no vetor de movimento
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Move o jogador com base no vetor de movimento
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// MÃ©todo para receber o input de movimento de outro script (Movimento4.cs).
    /// </summary>
    /// <param name="input">Vetor de entrada do joystick ou teclado.</param>
    public void SetMovementInput(Vector2 input)
    {
        movement = input; // Atualiza o vetor de movimento
    }

    /// <summary>
    /// Incrementa a contagem de fantasmas ao redor do jogador.
    /// </summary>
    public void AddGhost()
    {
        ghostCount++; // Incrementa o contador de fantasmas
        Debug.Log("Fantasmas ao redor: " + ghostCount);

        if (ghostCount > maxGhosts) // Verifica se o limite foi excedido
        {
            EndGame(); // Chama o mÃ©todo de fim de jogo
        }
    }

    /// <summary>
    /// Retorna a contagem atual de fantasmas ao redor do jogador.
    /// </summary>
    public int GetGhostCount()
    {
        return ghostCount; // Retorna o contador de fantasmas
    }

    /// <summary>
    /// Finaliza o jogo e carrega a cena de Game Over.
    /// </summary>
    private void EndGame()
    {
        Debug.Log("Game Over! Carregando cena de fim de jogo...");
        SceneManager.LoadScene("GameOver"); // Certifique-se de que a cena "GameOver" estÃ¡ incluÃ­da no Build Settings
    }

    /// <summary>
    /// Finaliza a cutscene inicial e retoma o jogo.
    /// </summary>
    public void EndCutscene()
    {
        cutsceneCanvas.SetActive(false); // Oculta o Canvas da cutscene
        Time.timeScale = 1f; // Retoma o jogo
    }
}
