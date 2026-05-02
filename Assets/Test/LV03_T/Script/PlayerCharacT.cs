using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacT : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    float moveInput;

    [Header("Hide System")]
    bool canHide = false;
    bool isHidden = false;

    public int normalOrder = 1;
    public int hideOrder = -1;

    float normalSpeed;

    [Header("Health")]
    public int maxHP = 3;
    private int currentHP;

    public Collider2D cr;

    void Start()
    {
        normalSpeed = moveSpeed;
        currentHP = maxHP;

        cr = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        
        if (cr == null)
        {
            Debug.LogError("ไม่มี Collider!");
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("กด F แล้ว | canHide = " + canHide);
        }

        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            isHidden = !isHidden;

            Debug.Log("สลับโหมดแอบ: " + isHidden);

            if (isHidden)
            {
                moveSpeed = 0f;
                moveInput = 0f;
                rb.linearVelocity = Vector2.zero;
                sr.sortingOrder = hideOrder;

                cr.isTrigger = true;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                moveSpeed = normalSpeed;
                sr.sortingOrder = normalOrder;

                cr.isTrigger = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }

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

    
    public void TakeDamage(int damage)
    {
        if (isHidden)
        {
            Debug.Log("หลบอยู่ → ไม่โดนดาเมจ 😏");
            return;
        }

        currentHP -= damage;
        Debug.Log("โดนดาเมจ! HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        Debug.Log("ผู้เล่นตายแล้ว 💀");

        Invoke(nameof(RestartScene), 1f); 
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ===== Hide Zone =====
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

    public bool IsHidden()
    {
        return isHidden;
    }
}