using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Referência ao painel de pausa
    private bool isPaused = false;
    private static PauseManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Garante que só há um PauseManager
        }
    }

    void Update()
    {
        // Ativa/desativa o pause quando a tecla Esc é pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Oculta o menu de pausa
        Time.timeScale = 1f; // Descongela o jogo
        isPaused = false;
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Exibe o menu de pausa
        Time.timeScale = 0f; // Congela o jogo
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Certifique-se de descongelar o jogo antes de mudar de cena
        // Adicione a linha para carregar o menu principal, por exemplo:
        Destroy(gameObject);
        SceneManager.LoadScene("Menu");
    }
}

