using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 4f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // ยิงไปทางขวาด้วย physics จริง (ดันแรง)
        rb.velocity = Vector2.left * speed;

        // กันค้างในฉาก
        Destroy(gameObject, lifeTime);
    }
}