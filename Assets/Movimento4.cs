using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento4 : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f; // Velocidade do personagem
    private Vector2 myInput; // Input do jogador (teclado ou joystick)
    private Player_Movement playerMovement; // Referência ao script Player_Movement

    private void Awake()
    {
        // Faz referência ao script Player_Movement
        playerMovement = GetComponent<Player_Movement>();
    }

    /// <summary>
    /// Captura os inputs do joystick ou teclado.
    /// </summary>
    /// <param name="value">Callback com os valores de entrada.</param>
    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (playerMovement == null) return;

        // Passa o movimento para o Player_Movement
        playerMovement.SetMovementInput(myInput);
    }
}

