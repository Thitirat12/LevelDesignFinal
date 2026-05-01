using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    public Rigidbody2D fallingBlock;
    public float fallDelay = 0.5f;

    [Header("Sound")]
    public AudioClip dropSound; // เสียงตอนเริ่มตก

    bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            Invoke(nameof(Drop), fallDelay);
        }
    }

    void Drop()
    {
        // เริ่มตก
        fallingBlock.gravityScale = 3f;

        // ?? เล่นเสียงที่ตำแหน่งบล็อค
        if (dropSound != null)
        {
            AudioSource.PlayClipAtPoint(dropSound, fallingBlock.position, 2f);
        }
    }
}