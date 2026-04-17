using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject lockpickUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lockpickUI.SetActive(true);
            Debug.Log("เริ่ม lockpick!");
        }
    }
}