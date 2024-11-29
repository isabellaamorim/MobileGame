using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do jogador
    public Rigidbody2D rb; // Componente Rigidbody2D para movimentação
    private Vector2 movement; // Vetor de movimento
    public Animator animator; // Componente Animator para animações
    public TextMeshProUGUI chaveText; // Texto para exibir o estado da chave
    public TextMeshProUGUI timerText; // Texto para exibir o tempo restante
    public AudioSource chaveAudio; // Som da chave
    public AudioSource portaAudio; // Som da porta

    public float totalTime = 60f; // Tempo total em segundos
    private float remainingTime; // Tempo restante no jogo

    private bool hasKey = false; // Indica se o jogador possui a chave
    private bool isTransitioning = false; // Controla se há transição de cena em andamento

    private float targetX = 75.49525f; // Posição X para verificar o próximo nível

    void Start()
    {
        remainingTime = totalTime; // Inicializa o tempo restante com o tempo total
        UpdateKeyText(); // Atualiza o texto da chave
        UpdateTimerText(); // Atualiza o texto do timer

        // Salva o índice da fase atual no PlayerPrefs
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        PlayerPrefs.Save(); // Garante que o valor seja salvo imediatamente
    }

    void Update()
    {
        // Atualiza animações baseadas no movimento
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Verifica se o jogador alcançou a posição-alvo para o próximo nível
        if (hasKey && rb.position.x > targetX && !isTransitioning)
        {
            StartCoroutine(LoadNextScene());
        }

        // Atualiza a contagem regressiva do tempo
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver(); // Chama o game over quando o tempo acaba
            }
        }
    }

    void FixedUpdate()
    {
        // Move o jogador com base no input recebido
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chaves") && !hasKey)
        {
            hasKey = true;
            UpdateKeyText(); // Atualiza o texto da chave
            chaveAudio.Play(); // Toca o som da chave
            Destroy(collision.gameObject); // Remove a chave da cena
        }

        if (collision.gameObject.CompareTag("porta2") && hasKey && !isTransitioning)
        {
            StartCoroutine(PlayDoorSoundAndLoadScene());
        }
    }

    /// <summary>
    /// Método para capturar o input de movimento do jogador (usado pelo script Movimento.cs).
    /// </summary>
    /// <param name="input">Vetor de entrada do joystick ou teclado.</param>
    public void SetMovementInput(Vector2 input)
    {
        movement = input;
    }

    IEnumerator PlayDoorSoundAndLoadScene()
    {
        isTransitioning = true;
        portaAudio.Play(); // Toca o som da porta
        yield return new WaitForSeconds(portaAudio.clip.length);
        SceneManager.LoadSceneAsync(4); // Carrega a próxima cena
    }

    IEnumerator LoadNextScene()
    {
        isTransitioning = true;
        SceneManager.LoadSceneAsync(4); // Carrega a próxima cena
        yield return null;
    }

    void UpdateKeyText()
    {
        // Atualiza o texto da chave com base no estado
        if (hasKey)
        {
            chaveText.text = "Chave 1/1";
        }
        else
        {
            chaveText.text = "Chave 0/1";
        }
    }

    void UpdateTimerText()
    {
        // Atualiza o texto do timer com o tempo restante
        timerText.text = "Tempo: " + Mathf.CeilToInt(remainingTime).ToString();
    }

    void GameOver()
    {
        SceneManager.LoadScene(6); // Carrega a cena de game over
    }
}
