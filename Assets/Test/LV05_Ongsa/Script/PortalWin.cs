using UnityEngine;

public class PortalWin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("ชนแล้ว"); // 🔥 เช็คว่าชนไหม

        if (col.CompareTag("Player"))
        {
            Debug.Log("Player เข้า Portal");
            FindObjectOfType<GameManager>().Win();
        }
    }
}