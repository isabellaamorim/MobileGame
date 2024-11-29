using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float velocidade = 4f; // Velocidade do personagem
    private Vector2 myInput; // Input do joystick
    private player_controller playerController; // Referência ao script player_controller

    private void Awake()
    {
        // Faz referência ao script player_controller
        playerController = GetComponent<player_controller>(); // Garante que player_controller está no mesmo GameObject
    }

    /// <summary>
    /// Método responsável por obter as entradas do joystick.
    /// </summary>
    /// <param name="value">Callback com as entradas do joystick, vindas do Input Actions.</param>
    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (playerController == null) return;

        // Move o personagem diretamente
        playerController.MovePlayer(myInput, velocidade);
    }
}
