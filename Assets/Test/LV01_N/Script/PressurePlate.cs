using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Door door;
    private bool isActivated = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Box"))
        {
            isActivated = true;
            door.OpenDoor();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Box"))
        {
            isActivated = false;
            door.CloseDoor();
        }
    }
}