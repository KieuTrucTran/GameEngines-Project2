using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class twoFluidMovement : MonoBehaviour
{
    public float moveForce = 5.0f;
    public float slideDuration = 1.0f;
    public float slideDecayRate = 3.0f;
    public float moveForceUp = 5.0f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool canWallClimb = false;

    private Vector2 lastMovementDirection; // Stores the last movement direction
    private float slideTimeRemaining = 0.0f;

    public InputActionAsset myInput;

    InputAction left;
    InputAction right;



    private void Awake()
    {
        right = myInput.FindActionMap("Player").FindAction("PressRight");
        left = myInput.FindActionMap("Player").FindAction("PressLeft");
    }

    private void OnEnable()
    {
        left.Enable();
        right.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;

        // Handle horizontal movement
        if (right.IsPressed())
        {
            Debug.Log("right preorsd");
            movement.x = 1;
            sr.flipX = false;
            slideTimeRemaining = slideDuration;
            lastMovementDirection = Vector2.right;
        }
        else if (left.IsPressed())
        {
            movement.x = -1;
            sr.flipX = true;
            slideTimeRemaining = slideDuration;
            lastMovementDirection = Vector2.left;
        }

        if (movement != Vector2.zero)
        {
            transform.Translate(movement * Time.deltaTime * moveForce);
        }
        else if (slideTimeRemaining > 0)
        {
            transform.Translate(lastMovementDirection * Time.deltaTime * moveForce * (slideTimeRemaining / slideDuration));
            slideTimeRemaining -= Time.deltaTime * slideDecayRate;
        }


        if (Input.GetKey(KeyCode.W) && canWallClimb)
        {
            rb.gravityScale = -0.5f;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "WallClimb")
        {
            canWallClimb = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "WallClimb")
        {
            canWallClimb = false;
        }
    }
}
