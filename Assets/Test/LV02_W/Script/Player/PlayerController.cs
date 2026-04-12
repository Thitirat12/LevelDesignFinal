using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float moveInput;

    [Header("Jump")]
    public float jumpForce;
    private bool isGrounded;

    [Header("Grounded")]
    public Transform groundCheck;
    public Vector2 checkRadius;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        CheckGround();
        HandleJump();
        Flip();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        moveInput = Input.GetAxis("Horizontal");    
    }

    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed,rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if (!Input.GetButtonDown("Jump")) return;

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
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, checkRadius);
    }

}
