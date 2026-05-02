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
            // 👉 เช็คก่อนว่าของครบมั้ย
            if (GameManagerN.instance.IsComplete())
            {
                ToggleState();
            }
            else
            {
                Debug.Log("ของยังไม่ครบ!");
            }
        }
    }

    void ToggleState()
    {
        if (isActivated) return; 

        isActivated = true;

        sr.color = pickedColor;
        door.OpenDoor();

        Debug.Log("เปิดประตูแล้ว!");
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