using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        // Faz com que a luz siga o jogador
        transform.position = player.position;
    }
}