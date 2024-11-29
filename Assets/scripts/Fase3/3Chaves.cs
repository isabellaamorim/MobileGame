using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaves : MonoBehaviour
{
    public GameObject[] keys; // Array das chaves
    public List<Transform> validPositions; // Lista de pontos válidos onde as chaves podem aparecer

    void Start()
    {
        SetRandomPositions();
    }

    void SetRandomPositions()
    {
        List<Transform> availablePositions = new List<Transform>(validPositions); // Cria uma cópia das posições válidas

        foreach (GameObject key in keys)
        {
            if (availablePositions.Count == 0) break; // Garante que não haverá erro caso o número de chaves exceda o número de posições

            // Escolhe uma posição aleatória da lista de posições disponíveis
            int randomIndex = Random.Range(0, availablePositions.Count);
            Transform selectedPosition = availablePositions[randomIndex];

            // Posiciona a chave e remove a posição da lista
            key.transform.position = selectedPosition.position;
            availablePositions.RemoveAt(randomIndex);
        }
    }
}
