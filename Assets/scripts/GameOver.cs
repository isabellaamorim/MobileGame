using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private int currentLevel;

    void Start()
    {
        // Carrega o n�vel salvo no PlayerPrefs para o Game Over
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // 1 � o valor padr�o
    }

    public void PlayGame()
    {
        // Carrega a fase em que o jogador estava
        SceneManager.LoadSceneAsync(currentLevel);
    }
}
