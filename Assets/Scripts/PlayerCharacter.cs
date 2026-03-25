using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedMin = 4f;
    [SerializeField] private float speedMax = 9f;
    [SerializeField] private float acceleration = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 7f;
    [SerializeField] private float jumpFloatDuration = 0.3f;
    [SerializeField] private float jumpFloatForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Death")]
    [SerializeField] private float killHeight = -10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDead;
    private bool isJumpPressed;

    private float jumpFloatTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead) return;

        CheckGrounded();

        isJumpPressed = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded)
        {
            Jump();
        }

        jumpFloatTimer -= Time.deltaTime;
        if (jumpFloatTimer < 0f)
        {
            jumpFloatTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x + acceleration * Time.fixedDeltaTime, speedMin, speedMax),
            rb.linearVelocity.y
        );

        if (transform.position.y < killHeight)
        {
            isDead = true;
            GameManager.Instance.GameOver();
            return;
        }

        if (jumpFloatTimer > 0f && isJumpPressed)
        {
            Float();
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        isGrounded = false;
        jumpFloatTimer = jumpFloatDuration;
    }

    private void Float()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            rb.linearVelocity.y + jumpFloatForce * Time.fixedDeltaTime
        );
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        GameManager.Instance.GameOver();
    }
}