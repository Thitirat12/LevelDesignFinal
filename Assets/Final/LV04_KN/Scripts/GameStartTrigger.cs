using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    public GameObject enemySpawner;
    public LockPickSystem lockpick;

    bool playerInside = false;
    bool triggered = false;

    void Update()
    {
        if (!playerInside) return;
        if (lockpick != null && lockpick.IsEncounterActive) return;
        if (lockpick != null && lockpick.LastCancelFrame == Time.frameCount) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("เริ่มเหตุการณ์!");

            if (lockpick != null)
            {
                lockpick.BeginLockpickEncounter();
            }

            if (!triggered && enemySpawner != null)
            {
                enemySpawner.SetActive(true);
            }

            triggered = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (lockpick != null)
            {
                lockpick.SetPlayer(other.transform);
            }

            Debug.Log("กด F เพื่อเริ่มสะเดาะกลอน");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
