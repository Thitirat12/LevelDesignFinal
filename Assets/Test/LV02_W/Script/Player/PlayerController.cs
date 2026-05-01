using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float moveInput;

    [Header("Jump")]
    public float jumpForce;
    private bool isGrounded;

    [Header("Grounded")]
    //private Collider2D col;
    public Transform groundCheck;
    public Vector2 checkRadius;
    public LayerMask groundLayer;

    [Header("Climb")]
    public float climbSpeed = 3f;
    private bool isClimbing;
    private float verticalInput;

    public Transform flashlight;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleInput();
        CheckGround();
        HandleJump();
        Flip();

        //rb.gravityScale = isClimbing ? 0f : 1f;
    }

    private void FixedUpdate()
    {
        HandleMovement();

        if (isClimbing)
            HandleClimb();
    }

    void HandleInput()
    {
        moveInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed,rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if (!Input.GetButtonDown("Jump")) return;

        if (isClimbing) return;

        if (Input.GetButtonDown("Jump") && isGrounded && rb.linearVelocity.y <= 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            Debug.Log("Jump");
        }

        else
        {
            Debug.Log("Already Jump");
        }
    }

    void HandleClimb()
    {
        rb.linearVelocity = new Vector2(
        moveInput * moveSpeed,          // 👈 เดินซ้ายขวาได้
        verticalInput * climbSpeed      // 👈 ปีนขึ้นลง
    );
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, checkRadius, 0f,groundLayer);
    }

    void Flip()
    {
        if (moveInput > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveInput < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // หมุน Flashlight counter ไม่ให้ถูก flip
        if (flashlight != null)
        {
            Vector3 fScale = flashlight.localScale;
            fScale.x *= -1;
            flashlight.localScale = fScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            TilemapRenderer tr = other.GetComponent<TilemapRenderer>();
            if (tr == null || !tr.enabled) return;

            isClimbing = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            //col.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            //TilemapRenderer tr = other.GetComponent<TilemapRenderer>();
            //if (tr == null || !tr.enabled) return;

            isClimbing = false;
            rb.gravityScale = 3f;
            //col.isTrigger = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, checkRadius);
    }

}
