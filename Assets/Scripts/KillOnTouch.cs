using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerMovementController = collision.collider.GetComponent<PlayerMovementController>();
        if (playerMovementController == null) return;

        GameManager.Instance.KillPlayer(playerMovementController);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerMovementController = collision.GetComponent<PlayerMovementController>();
        if (playerMovementController == null) return;

        GameManager.Instance.KillPlayer(playerMovementController);
    }
}
