using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            player.Die();
        }
    }
}
