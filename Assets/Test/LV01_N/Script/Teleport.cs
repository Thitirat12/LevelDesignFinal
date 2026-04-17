using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform exitPoint;

    private bool playerInZone = false;
    private GameObject player;

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            player.transform.position = exitPoint.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInZone = true;
            player = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInZone = false;
            player = null;
        }
    }
}