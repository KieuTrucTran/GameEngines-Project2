using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class twoSolidMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;

    //private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private Vector3 initialScale;


    public InputActionAsset myInput;

    InputAction left;
    InputAction right;

    private void OnEnable()
    {
        left.Enable();
        right.Enable();
    }


    private void Awake()
    {
        right = myInput.FindActionMap("Player").FindAction("PressRight");
        left = myInput.FindActionMap("Player").FindAction("PressLeft");

        body = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop rotation
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Check input and set horizontal movement
        if (left.IsPressed())
        {
            horizontalInput = -1;
        }
        else if (right.IsPressed())
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = 0; // Stop movement when no input is detected
        }

        // Set the character's velocity
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip the player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = initialScale;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);

        // Apply gravity
        body.gravityScale = 1;
    }



    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;

    }

}
