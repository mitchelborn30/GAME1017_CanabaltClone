using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 6f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Death")]
    [SerializeField] private float minYDeath = -10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead) return;

        CheckGrounded();

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            Jump();
        }

        if (transform.position.y < minYDeath)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2(forwardSpeed, rb.linearVelocity.y);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        GameManager.Instance.GameOver();
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}