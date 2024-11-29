using UnityEngine;
using UnityEngine.InputSystem;

public class Movimento2 : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f; // Velocidade do personagem
    private Vector2 myInput; // Input do joystick ou teclado
    private PlayerController playerController; // Referência ao PlayerController

    private void Awake()
    {
        // Faz a referência ao script PlayerController
        playerController = GetComponent<PlayerController>();
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
        if (playerController == null) return;

        // Passa o movimento para o PlayerController
        playerController.SetMovementInput(myInput);
    }
}
