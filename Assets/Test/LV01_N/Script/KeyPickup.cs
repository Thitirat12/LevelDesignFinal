using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Door door;

    public Color normalColor = Color.white;
    public Color pickedColor = Color.gray;

    private bool playerInZone = false;
    private bool isActivated = false;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = normalColor;
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            ToggleState();
        }
    }

    void ToggleState()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            // เปิด
            sr.color = pickedColor;
            door.OpenDoor();
        }
        else
        {
            // ปิด
            sr.color = normalColor;
            door.CloseDoor();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}