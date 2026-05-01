using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public Transform targetPoint;

    bool playerInRange = false;
    Transform player;

    static bool isTeleporting = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isTeleporting)
        {
            Teleport();
        }
    }

    void Teleport()
    {
        if (player == null || targetPoint == null) return;

        isTeleporting = true;

        player.position = targetPoint.position;

        Invoke(nameof(ResetTeleport), 0.5f);
    }

    void ResetTeleport()
    {
        isTeleporting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}