using UnityEngine;
using TMPro; // Certifique-se de usar isso para acessar o TextMeshProUGUI
using UnityEngine.Rendering.Universal; // Necessário para usar Light2D

public class CameraFollow : MonoBehaviour
{
    public Transform player; // O transform do player
    public Vector3 offset = new Vector3(0, 0, -15); // Distância entre a câmera e o player

    public Light2D pointLight; // Mude para Light2D
    public float duration = 60f; // Duração do timer em segundos
    public TextMeshProUGUI timerText; // Texto que mostrará o tempo na tela

    private float timeRemaining;

    void Start()
    {
        timeRemaining = duration; // Inicializa o tempo restante com a duração total
    }

    void Update()
    {
        // Atualiza a posição da câmera para seguir o player, mantendo o offset
        if (player != null)
        {
            transform.position = player.position + offset;
        }

        // Reduz o tempo restante e atualiza o raio da luz
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Calcula o novo raio da luz
            float lightRadius = Mathf.Lerp(0, 20, timeRemaining / duration); // Ajuste conforme necessário
            pointLight.pointLightOuterRadius = lightRadius; // Para Light2D

            // Atualiza o texto na tela com o tempo restante formatado
            UpdateTimerText();

            // Opcional: Apagar completamente quando o tempo acabar
            if (timeRemaining <= 0)
            {
                pointLight.enabled = false; // Desliga a luz
                timeRemaining = 0; // Garante que o tempo não vá para negativo
            }
        }
    }

    void UpdateTimerText()
    {
        // Formata o texto para mostrar apenas os segundos restantes
        int seconds = Mathf.CeilToInt(timeRemaining); // Arredonda para cima
        timerText.text = "Tempo: " + seconds.ToString() + "s";
    }
}
