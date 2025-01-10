using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideSpeed = 5f; // Speed at which the player slides
    public string playerTag = "Player"; // Tag used to identify the player
    public float rotationSmoothness = 5f; // How smoothly the player rotates to align with the slide

    private int contactCount = 0; 
    private bool hasEnteredSlide = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            

            if (playerRb != null)
            {
                // Stop the player's regular movement
                playerRb.linearVelocity = Vector2.zero;

                contactCount++;
                Debug.Log($"Player entered collision with slide. Contact count: {contactCount}");
                Debug.Log($"Player entered collision with slide. Contact count real: {collision.contacts.Length}");                   
                // Start sliding
                SlidePlayer(collision, playerRb);
                RotatePlayer(collision.gameObject, collision.contacts[0].normal, 170f);
            }
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Continuously apply sliding force while the player remains on the slide
        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                SlidePlayer(collision, playerRb);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Restore the player's normal orientation and movement when they leave the slide
        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero; // Reset velocity
            }

            // Reset player's rotation to upright
            //collision.gameObject.transform.rotation = Quaternion.identity;
    Debug.Log($"Player exit collision with slide. Contact count: {collision.contacts.Length}");
            RotatePlayer(collision.gameObject, Vector2.up, 170f);
        }
    }


    private void SlidePlayer(Collision2D collision, Rigidbody2D playerRb)
    {
        // Calculate the sliding direction based on the slide's surface
        Vector2 normal = collision.contacts[0].normal;
        Vector2 slideDirection = new Vector2(normal.y, -normal.x).normalized;

        // Apply velocity in the sliding direction
        playerRb.linearVelocity = slideDirection * slideSpeed;
    }

    private void RotatePlayer(GameObject player, Vector2 normal, float rotationAngle)
    {
        Vector3 slideDirection = Vector3.Cross(Vector3.forward,normal);
        player.transform.right = slideDirection;
        //player.transform.rotation = Quaternion.LookRotation(slideDirection,normal);

        //player.transform.up = -normal;
        //Quaternion adjustedRotation = Quaternion.LookRotation(normal);

        // Calculate the angle to rotate based on the slide's surface normal
        //float targetAngle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg + rotationAngle;

        // Smoothly rotate the player towards the target angle
        //Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }

   
}
