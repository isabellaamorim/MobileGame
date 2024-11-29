using System;
using System.Diagnostics;
using UnityEngine;

public class TilemapRandomPosition : MonoBehaviour
{
    public GameObject velaTilemap; // O GameObject da vela
    public GameObject chaveTilemap; // O GameObject da chave

    public Collider2D wallCollider; // O Collider2D que cobre as paredes
    public Vector2 minPosition;     // Limites mínimos de posição
    public Vector2 maxPosition;     // Limites máximos de posição

    void Start()
    {
        RandomizePosition(velaTilemap);
        RandomizePosition(chaveTilemap);
    }

    void RandomizePosition(GameObject tilemap)
    {
        bool positionValid = false;
        Vector3 randomPosition = Vector3.zero;

        // Tentamos randomizar até encontrar uma posição válida
        for (int attempt = 0; attempt < 100; attempt++)
        {
            // Gera coordenadas aleatórias dentro dos limites fornecidos
            float randomX = UnityEngine.Random.Range(minPosition.x, maxPosition.x);
            float randomY = UnityEngine.Random.Range(minPosition.y, maxPosition.y);
            randomPosition = new Vector3(randomX, randomY, 0);

            // Verifica se a posição NÃO está colidindo com as paredes
            if (!Physics2D.OverlapCircle(randomPosition, 0.3f, LayerMask.GetMask("Wall")))
            {
                positionValid = true;
                break; // Encerra o loop após encontrar uma posição válida
            }
        }

        // Move o objeto (vela ou chave) para a posição válida, se encontrada
        if (positionValid)
        {
            tilemap.transform.position = randomPosition;
        }
        
    }
}
