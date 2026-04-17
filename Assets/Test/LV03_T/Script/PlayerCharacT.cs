using UnityEngine;

public class PlayerCharacT : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    float moveInput;

    bool canHide = false;
    bool isHidden = false;

    public int normalOrder = 1;
    public int hideOrder = -1;

    float normalSpeed;

    void Start()
    {
        normalSpeed = moveSpeed;
    }

    void Update()
    {
        // 🔍 Debug: เช็คว่ากด F มั้ย
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("กด F แล้ว | canHide = " + canHide);
        }

        // กด F เข้า/ออกแอบ
        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            isHidden = !isHidden;

            Debug.Log("สลับโหมดแอบ: " + isHidden);

            if (isHidden)
            {
                moveSpeed = 0f;
                moveInput = 0f;
                rb.linearVelocity = Vector2.zero;

                sr.sortingOrder = hideOrder; // 👻 ไปหลัง
            }
            else
            {
                moveSpeed = normalSpeed;

                sr.sortingOrder = normalOrder; // 🔙 กลับหน้า
            }
        }

        // ถ้าแอบ = ห้ามขยับ
        if (isHidden)
        {
            moveInput = 0f;
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    void FixedUpdate()
    {
        if (isHidden)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;

        if (moveInput > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (moveInput < 0)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HideZone"))
        {
            Debug.Log("เข้า HideZone แล้ว!");
            canHide = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HideZone"))
        {
            Debug.Log("ออกจาก HideZone");
            canHide = false;

            if (isHidden)
            {
                isHidden = false;
                moveSpeed = normalSpeed;
                sr.sortingOrder = normalOrder;
            }
        }
    }
}