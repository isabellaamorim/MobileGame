using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade do jogador
    private Rigidbody2D rb; // Referência ao Rigidbody2D
    public TextMeshProUGUI countKeysText; // Texto para exibir a contagem de chaves
    public TextMeshProUGUI winText; // Texto para exibir a mensagem de vitória
    public TextMeshProUGUI timerText; // Texto para exibir o timer
    public Animator animator; // Referência ao Animator para animações
    private Vector2 movement; // Vetor de movimento
    private int countKeys; // Contador de chaves coletadas
    private GameObject door; // Referência à porta no jogo
    private AudioManager3 audioManager; // Referência ao AudioManager para tocar sons

    private float timeRemaining = 180f; // Tempo total (3 minutos)
    private bool gameLost = false; // Flag para indicar se o jogo foi perdido

    private void Awake()
    {
        // Referencia o AudioManager pelo tag
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager3>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Referência ao Rigidbody2D
        countKeys = 0; // Inicializa a contagem de chaves

        // Busca a porta no jogo
        door = GameObject.FindWithTag("Door");

        winText.gameObject.SetActive(false); // Oculta o texto de vitória

        UpdateTimerText(); // Atualiza o texto do timer

        // Salva o índice da fase atual no PlayerPrefs
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        PlayerPrefs.Save(); // Garante que o valor seja salvo imediatamente
    }

    void Update()
    {
        if (!gameLost)
        {
            // Atualiza as animações com base no vetor de movimento
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            // Atualiza o timer
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                GameOver();
            }
        }
    }

    void FixedUpdate()
    {
        if (!gameLost)
        {
            // Move o jogador com base no vetor de movimento
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Coleta uma chave
        if (other.gameObject.CompareTag("Key"))
        {
            audioManager.playSFX(); // Toca o som da chave
            other.gameObject.SetActive(false); // Remove a chave do jogo
            countKeys += 1; // Incrementa a contagem de chaves
            SetCountText(); // Atualiza o texto da contagem de chaves
        }
        // Verifica se o jogador chegou à área final
        else if (other.gameObject.CompareTag("Chegada"))
        {
            if (countKeys == 4) // Verifica se todas as chaves foram coletadas
            {
                WinGame(); // Chama o método de vitória
            }
        }
    }

    /// <summary>
    /// Atualiza o vetor de movimento com os inputs recebidos do Movimento3.cs.
    /// </summary>
    /// <param name="input">Vetor de movimento do joystick/teclado.</param>
    public void SetMovementInput(Vector2 input)
    {
        movement = input; // Atualiza o vetor de movimento
    }

    /// <summary>
    /// Atualiza o texto da contagem de chaves.
    /// </summary>
    void SetCountText()
    {
        countKeysText.text = countKeys.ToString() + " / 4";

        if (countKeys == 4)
        {
            door.SetActive(false); // Remove a porta se todas as chaves foram coletadas
        }
    }

    /// <summary>
    /// Método chamado ao vencer o jogo.
    /// </summary>
    void WinGame()
    {
        SceneManager.LoadSceneAsync(5); // Carrega a cena de vitória
        gameLost = true; // Define que o jogo foi encerrado
    }

    /// <summary>
    /// Método chamado ao perder o jogo (tempo esgotado).
    /// </summary>
    void GameOver()
    {
        SceneManager.LoadScene(6); // Carrega a cena de game over
    }

    /// <summary>
    /// Atualiza o texto do timer com o tempo restante.
    /// </summary>
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
