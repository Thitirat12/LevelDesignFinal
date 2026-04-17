using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    public GameObject enemySpawner;
    public LockPickSystem lockpick;

    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("เริ่มเหตุการณ์!");

            if (lockpick != null)
            {
                lockpick.BeginLockpickEncounter();
            }

            if (enemySpawner != null)
            {
                enemySpawner.SetActive(true);
            }
        }
    }
}
