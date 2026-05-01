using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject lockpickUI;
    public LockPickSystem lockpick;

    bool playerInside = false;

    void Update()
    {
        if (!playerInside) return;
        if (lockpick != null && lockpick.IsEncounterActive) return;
        if (lockpick != null && lockpick.LastCancelFrame == Time.frameCount) return;
        if (!Input.GetKeyDown(KeyCode.F)) return;

        if (lockpick != null)
        {
            lockpick.BeginLockpickEncounter();
        }
        else if (lockpickUI != null)
        {
            lockpickUI.SetActive(true);
        }

        Debug.Log("Start lockpick!");
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

            Debug.Log("Press F to start lockpick!");
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
