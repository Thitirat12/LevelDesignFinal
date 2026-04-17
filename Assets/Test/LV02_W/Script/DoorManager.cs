using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool red;
    public bool blue;
    public bool yellow;

    public GameObject door;

    public bool isOpen = false;
    private void Awake()
    {
        isOpen = false;
        door.SetActive(false);
    }

    public void RegisterUnlock(KeyType type)
    {
        switch (type)
        {
            case KeyType.Red:
                red = true;
                break;

            case KeyType.Blue:
                blue = true;
                break;

            case KeyType.Yellow:
                yellow = true;
                break;
        }

        CheckDoor();
    }

    void CheckDoor()
    {
        if (red && blue && yellow)
        {
            Debug.Log("DOOR OPEN");
            isOpen = true;
            door.SetActive(true);
        }
    }
}
