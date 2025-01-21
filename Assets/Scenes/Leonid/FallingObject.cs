using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Vector2 movementDirection;
    public Rigidbody2D rigidbody2D;
    [SerializeField]
    private float forceAmount;
    private float timeElapsed;
    private bool isInWater = false; // Flag to check if the object is in the water zone
    private bool isMoving = false;
    void Start()
    {
        rigidbody2D.linearVelocity = Vector3.down * forceAmount;
    }

    private void Update()
    {
        if (isInWater) // Only execute movement logic if the object is in the water zone
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                isMoving = true;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                isMoving = false;
            }

            if (isMoving)
            {
                rigidbody2D.linearVelocity = movementDirection * forceAmount;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("WaterZone"))
        {
            isInWater = true; // Set the flag to true when entering the water zone
            Debug.Log("Entered water zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("WaterZone"))
        {
            isInWater = false; // Reset the flag when leaving the water zone
            Debug.Log("Exited water zone");

            // Stop movement when leaving the water zone
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}