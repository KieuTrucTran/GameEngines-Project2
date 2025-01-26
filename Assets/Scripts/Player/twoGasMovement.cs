using UnityEngine;
using UnityEngine.InputSystem;

public class twoGasMovement : MonoBehaviour
{
    public float floatForce = 0.5f;         // Natural upward force
    public float horizontalSpeed = 3f;     // Horizontal movement speed
    public float upwardBoost = 2f;         // Controlled upward movement speed
    public float downwardForce = 2f;       // Controlled downward force
    public float maxVerticalSpeed = 3f;    // Maximum vertical speed for floating
    public float horizontalDrag = 0.95f;   // Drag effect for horizontal movement

    private Rigidbody2D rb;
    private Vector2 windForce;             // Wind force applied to the player
    private bool isInWindZone = false;      // Flag to check if the player is in a wind zone
    SpriteRenderer spriteRenderer;

    public Sprite normalSprite;
    public Sprite angrySprite;

    private float angryTimer = 0;
    private float angryTime = 2.0f;
    bool isAngry = false;

    private Animator animator;

    public InputActionAsset myInput;

    InputAction left;
    InputAction right;
    InputAction up;
    InputAction down;

    private void Awake()
    {
        right = myInput.FindActionMap("Player").FindAction("PressRight");
        left = myInput.FindActionMap("Player").FindAction("PressLeft");
        up = myInput.FindActionMap("Player").FindAction("PressUp");
        down = myInput.FindActionMap("Player").FindAction("PressDown");

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for gas
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ApplyFloatingForce();
        HandleMovement();
        ApplyHorizontalDrag();
        LimitVerticalSpeed();
        ApplyWindForce(); // Apply wind force if in wind zone

        if (isAngry)
        {
            if (angryTimer < angryTime)
            {
                angryTimer += Time.deltaTime;
            }
            else
            {
                angryTimer = 0;
                isAngry = false;
                spriteRenderer.sprite = normalSprite;
                animator.enabled = true;
            }

        }
    }

    private void ApplyFloatingForce()
    {
        // Apply a constant upward force for floating
        rb.AddForce(Vector2.up * floatForce, ForceMode2D.Force);
    }

    private void HandleMovement()
    {
        // Horizontal movement (A/D or Left/Right keys)
        float horizontalInput = 0;

        if(left.IsPressed())
        {
            horizontalInput = -1;
        }
        if (right.IsPressed())
        {
            horizontalInput = 1;
        }


        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        rb.AddForce(Vector2.right * horizontalInput * horizontalSpeed, ForceMode2D.Force);

        // Controlled upward boost (W or Up Arrow)
        if (up.IsPressed())
        {
            rb.AddForce(Vector2.up * upwardBoost, ForceMode2D.Force);
        }

        // Controlled downward movement (S or Down Arrow)
        if (down.IsPressed())
        {
            rb.AddForce(Vector2.down * downwardForce, ForceMode2D.Force);
        }
    }

    private void ApplyHorizontalDrag()
    {
        // Apply drag to horizontal movement for a smooth stopping effect
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * horizontalDrag, rb.linearVelocity.y);
    }

    private void LimitVerticalSpeed()
    {
        // Clamp the vertical velocity to create a smooth floating effect
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -maxVerticalSpeed, maxVerticalSpeed));
    }

    private void ApplyWindForce()
    {
        if (isInWindZone)
        {
            rb.AddForce(windForce, ForceMode2D.Force);
        }
    }

    public void SetWindForce(Vector2 force)
    {
        windForce = force;
    }

    public void SetInWindZone(bool value)
    {
        isInWindZone = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.enabled = false;
        spriteRenderer.sprite = angrySprite;
        isAngry = true;
    }
}