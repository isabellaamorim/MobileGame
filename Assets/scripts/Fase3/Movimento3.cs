using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento3 : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f; // Velocidade do personagem

    private Vector2 myInput; // Input do jogador
    private PlayerMovement playerMovement; // Referência ao script PlayerMovement

    private void Awake()
    {
        // Faz a referência ao script PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
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

        // Passa o movimento para o PlayerMovement
        playerMovement.SetMovementInput(myInput);
    }
}
