using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public GameObject keyObject; // O prefab da chave

    // Posições pré-definidas onde a chave pode aparecer dentro do chão
    private Vector2[] possiblePositions = new Vector2[]
    {
        new Vector2(60, 8),  // Posição 1
        new Vector2(80, -8),  // Posição 2
        new Vector2(-20, 12),  // Posição 3
        new Vector2(90, 12),  // Posição 4
        new Vector2(90, 5),  // Posição 5
    };

    private bool keySpawned = false; // Variável para controlar se a chave já foi gerada

    void Start()
    {
        SpawnKey(); // Gera a chave ao iniciar o jogo
    }

    void SpawnKey()
    {
        if (keySpawned) return; // Se a chave já foi gerada, interrompe a execução

        // Sorteia uma posição entre as pré-definidas
        Vector2 selectedPosition = possiblePositions[Random.Range(0, possiblePositions.Length)];

        // Posiciona a chave na posição selecionada
        keyObject.transform.position = new Vector3(selectedPosition.x, selectedPosition.y, 0f);
        keySpawned = true; // Define que a chave foi gerada
        Debug.Log($"Chave gerada na posição: {selectedPosition}");
    }
}
