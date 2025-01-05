using UnityEngine;

public class GasMovement : MonoBehaviour
{
    public float floatForce = 0.5f;         // Natural upward force
    public float horizontalSpeed = 3f;     // Horizontal movement speed
    public float upwardBoost = 2f;         // Controlled upward movement speed
    public float downwardForce = 2f;       // Controlled downward force
    public float maxVerticalSpeed = 3f;    // Maximum vertical speed for floating
    public float horizontalDrag = 0.95f;   // Drag effect for horizontal movement

    private Rigidbody2D rb;
    private Vector2 windForce;             // Wind force applied to the player
    private bool isInWindZone = false;     // Flag to check if the player is in a wind zone

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for gas
    }

    void FixedUpdate()
    {
        ApplyFloatingForce();
        HandleMovement();
        ApplyHorizontalDrag();
        LimitVerticalSpeed();
        ApplyWindForce(); // Apply wind force if in wind zone
    }

    private void ApplyFloatingForce()
    {
        // Apply a constant upward force for floating
        rb.AddForce(Vector2.up * floatForce, ForceMode2D.Force);
    }

    private void HandleMovement()
    {
        // Horizontal movement (A/D or Left/Right keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * horizontalInput * horizontalSpeed, ForceMode2D.Force);

        // Controlled upward boost (W or Up Arrow)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * upwardBoost, ForceMode2D.Force);
        }

        // Controlled downward movement (S or Down Arrow)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
}