using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacT player = collision.gameObject.GetComponent<PlayerCharacT>();

            if (player != null)
            {
                // 🔥 เช็คว่าผู้เล่นแอบอยู่มั้ย
                if (player.IsHidden())
                {
                    Debug.Log("ศัตรูมองไม่เห็น (ผู้เล่นแอบอยู่)");
                    return;
                }

                player.TakeDamage(damage);
            }
        }
    }
}