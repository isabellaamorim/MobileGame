using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CandelabroLightGenerator : MonoBehaviour
{
    public Tilemap candelabroTilemap;  // Refer�ncia ao Tilemap de candelabros
    public GameObject lightPrefab;     // Prefab da Spotlight

    void Start()
    {
        // Obt�m os limites do Tilemap
        BoundsInt bounds = candelabroTilemap.cellBounds;

        // Itera sobre todas as c�lulas do Tilemap
        foreach (var pos in bounds.allPositionsWithin)
        {
            // Verifica se existe um tile nessa posi��o
            TileBase tile = candelabroTilemap.GetTile(pos);
            if (tile != null)
            {
                // Converte a posi��o do Tilemap para coordenadas do mundo
                Vector3 worldPos = candelabroTilemap.CellToWorld(pos);

                // Instancia a luz na posi��o do candelabro
                GameObject newLight = Instantiate(lightPrefab, worldPos, Quaternion.identity);

                // Opcional: ajuste a altura da luz se necess�rio
                newLight.transform.position += new Vector3(0, 0, 0); // Ajusta a altura da luz, se necess�rio
            }
        }
    }
}
